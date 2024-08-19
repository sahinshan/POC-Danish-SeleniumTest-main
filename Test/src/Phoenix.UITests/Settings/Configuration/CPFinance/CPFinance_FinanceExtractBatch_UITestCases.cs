using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Settings.Configuration.CPFinanceAdmin
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Finance Extract Batch
    /// </summary>
    [TestClass]
    public class CPFinance_FinanceExtractBatch_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CP_FEB_BU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CP_FEB_Team";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "07790", "CPFEBTeam@careworkstempmail.com", _teamName, "910 123456");

                #endregion

                #region Create default system user

                _loginUsername = "CP_FEBUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CP_FEB", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-4621

        [TestProperty("JiraIssueID", "ACC-4647")]
        [Description("Test case for Automated FEB Creation + Scheduled Job - Test automation for Steps 1 to 9")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Extract Batches")]
        public void CPFinance_FinanceExtractBatch_UITestMethod001()
        {
            #region Step 1

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

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, commonMethodsHelper.GetCurrentDateWithoutCulture());
            var _cpExtractTypeId1 = commonMethodsDB.CreateCareProviderExtractType(_teamId, "CPET_A1_" + currentTimeString, newExtractTypeCode1, null, commonMethodsHelper.GetCurrentDateWithoutCulture());

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ValidateNoRecordsMessageVisible();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, commonMethodsHelper.GetDatePartWithoutCulture(), new TimeSpan(3, 0, 0), null, _cpExtractNameId1, 1, _cpExtractTypeId1, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, commonMethodsHelper.GetCurrentDateWithoutCulture());
            var _cpFinanceExtractBatchId = dbHelper
                .careProviderFinanceExtractBatch
                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatchId1 = _cpFinanceExtractBatchId[0];

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceExtractBatchId1.ToString(), true);

            #endregion

            #region Step 2
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickNewRecordButton();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDFieldIsDisplayed(true)
                .ValidateFinanceExtractBatchSetupLookupButtonIsDisplayed(true)
                .ValidateRunOnFieldIsDisplayed(true)
                .ValidateRunOnDatePickerIsDisplayed(true)
                .ValidateRunOn_TimeFieldDisabled(true)
                .ValidateRunOn_TimePickerIsDisplayed(true)
                .ValidateBatchStatusFieldIsDisplayed(true)
                .ValidateExtractNameLookupButtonIsDisplayed(true)
                .ValidateIsAdHocOptionsAreDisplayed(true)
                .ValidateInvoiceFilesFieldLabelIsDisplayed(true);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateNetBatchTotalFieldIsDisplayed(true)
                .ValidateGrossBatchTotalFieldIsDisplayed(true)
                .ValidateVatTotalFieldIsDisplayed(true)
                .ValidateTotalDebitsFieldIsDisplayed(true)
                .ValidateTotalCreditsFieldIsDisplayed(true)
                .ValidateNumberOfInvoicesExtractedFieldIsDisplayed(true)
                .ValidateNumberOfUniquePayersFieldIsDisplayed(true)
                .ValidateNumberOfInvoicesCancelledFieldIsDisplayed(true)
                .ValidateExtractYearFieldIsDisplayed(true)
                .ValidateExtractMonthFieldIsDisplayed(true)
                .ValidateExtractWeekFieldIsDisplayed(true)
                .ValidateExtractContentFieldLabelIsDisplayed(true)
                .ValidateIsDownloadedOptionsAreDisplayed(true)
                .ValidateCompletedOnFieldIsDisplayed(true)
                .ValidateCompletedOnDatePickerIsDisplayed(true)
                .ValidateCompletedOn_TimeFieldIsDisplayed(true)
                .ValidateCompletedOn_TimePickerIsDisplayed(true);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateResponsibleTeamLookupButtonIsDisplayed(true);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateMandatoryFieldIsDisplayed("Batch ID")
                .ValidateMandatoryFieldIsDisplayed("Finance Extract Batch Setup")
                .ValidateMandatoryFieldIsDisplayed("Run On")
                .ValidateMandatoryFieldIsDisplayed("Batch Status", false)
                .ValidateMandatoryFieldIsDisplayed("Extract Name")
                .ValidateMandatoryFieldIsDisplayed("Ad Hoc Batch?")
                .ValidateMandatoryFieldIsDisplayed("Invoice Files", false);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateMandatoryFieldIsDisplayed("Net Batch Total", false)
                .ValidateMandatoryFieldIsDisplayed("Gross Batch Total", false)
                .ValidateMandatoryFieldIsDisplayed("VAT Total", false)
                .ValidateMandatoryFieldIsDisplayed("Total Debits", false)
                .ValidateMandatoryFieldIsDisplayed("Total Credits", false)
                .ValidateMandatoryFieldIsDisplayed("Number Of Invoices Extracted", false)
                .ValidateMandatoryFieldIsDisplayed("Number Of Unique Payers", false)
                .ValidateMandatoryFieldIsDisplayed("Number Of Invoices Cancelled", false)
                .ValidateMandatoryFieldIsDisplayed("Extract Year", false)
                .ValidateMandatoryFieldIsDisplayed("Extract Month", false)
                .ValidateMandatoryFieldIsDisplayed("Extract Week", false)
                .ValidateMandatoryFieldIsDisplayed("Extract Content", false)
                .ValidateMandatoryFieldIsDisplayed("Downloaded?", true)
                .ValidateMandatoryFieldIsDisplayed("Completed On", true)
                .ValidateMandatoryFieldIsDisplayed("Responsible Team", true);

            #endregion

            #region Step 3
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchStatusSelectedText("New")
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ValidateIsdownloaded_NoOptionChecked()
                .ValidateIsdownloaded_YesOptionNotChecked();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDFieldDisabled(true)
                .ValidateFinanceExtractBatchSetupLookupButtonDisabled(false)
                .ValidateRunOnFieldDisabled(true)
                .ValidateRunOnDatePickerDisabled(true)
                .ValidateRunOn_TimeFieldDisabled(true)
                .ValidateRunon_TimePickerFieldDisabled(true)
                .ValidateBatchStatusFieldDisabled(true)
                .ValidateExtractNameLookupButtonDisabled(true)
                .ValidateIsAdHocOptionsDisabled(true)
                .ValidateNetBatchTotalDisabled(true)
                .ValidateGrossBatchTotalDisabled(true)
                .ValidateVatTotalDisabled(true)
                .ValidateTotalDebitsDisabled(true)
                .ValidateTotalCreditsDisabled(true)
                .ValidateNumberOfInvoicesExtractedDisabled(true)
                .ValidateNumberOfUniquePayersDisabled(true)
                .ValidateNumberOfInvoicesCancelledDisabled(true)
                .ValidateExtractYearDisabled(true)
                .ValidateExtractMonthDisabled(true)
                .ValidateExtractWeekDisabled(true)
                .ValidateIsDownloadedOptionsDisabled(true)
                .ValidateCompletedOnFieldDisabled(true)
                .ValidateCompletedOnDatePickerDisabled(true)
                .ValidateCompletedOn_TimeDisabled(true)
                .ValidateCompletedOn_TimePickerDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(false);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickFinanceExtractBatchSetupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CPEN_A1_" + currentTimeString)
                .TapSearchButton()
                .SelectResultElement(_cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDFieldDisabled(true)
                .ValidateFinanceExtractBatchSetupLookupButtonDisabled(false)
                .ValidateRunOnFieldDisabled(false)
                .ValidateRunOnDatePickerDisabled(false)
                .ValidateRunOn_TimeFieldDisabled(false)
                .ValidateRunon_TimePickerFieldDisabled(false)
                .ValidateExtractNameLinkText("CPEN_A1_" + currentTimeString);

            #endregion

            #region Step 4
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickRunOnDatePicker();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(commonMethodsHelper.GetCurrentDateWithoutCulture());

            var RunOnTime = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(1);
            var RunOnTimeTextInTitle = RunOnTime.ToString("HH:00:00");

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickRunOn_TimePicker();

            calendarTimePicker
                .WaitForCalendarTimePickerPopupToLoad()
                .SelectHour(RunOnTime.ToString("HH"))
                .SelectMinute(RunOnTime.ToString("00"));

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateFinanceExtractBatchRecordTitle("CPEN_A1_" + currentTimeString + " \\ " + DateTime.Now.ToString("dd'/'MM'/'yyyy") + " " + RunOnTimeTextInTitle)
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A1_" + currentTimeString + " \\ " + DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOnText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(RunOnTime.ToString("HH:00"))
                .ValidateExtractNameLinkText("CPEN_A1_" + currentTimeString)
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            _cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);

            Assert.AreEqual(2, _cpFinanceExtractBatchId.Count);
            #endregion

            #region Step 5

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);
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

            var _cpExtractNameId2 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A2_" + currentTimeString, newExtractNameCode2, null, commonMethodsHelper.GetCurrentDateWithoutCulture());
            var _cpExtractTypeId2 = commonMethodsDB.CreateCareProviderExtractType(_teamId, "CPET_A2_" + currentTimeString, newExtractTypeCode2, null, commonMethodsHelper.GetCurrentDateWithoutCulture());

            var _cpFinanceExtractBatchSetupId2 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2023, 9, 4), new TimeSpan(2, 0, 0), null, _cpExtractNameId2, 1, _cpExtractTypeId2, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId2, new DateTime(2023, 9, 4));

            _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId2, _cpExtractNameId2);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatchId2 = _cpFinanceExtractBatchId[0];

            var _cpFinanceExtractBatches = dbHelper.careProviderFinanceExtractBatch.GetAllCareProviderFinanceExtractBatch();
            int _cpFinanceExtractBatchIdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatchId2, "batchid")["batchid"];
            int _cpFinanceExtractBatchIdNumber2 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatchId1, "batchid")["batchid"];
            int _cpFinanceExtractBatchIdNumber3 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatches[1], "batchid")["batchid"];
            Assert.IsTrue(_cpFinanceExtractBatchIdNumber1 > _cpFinanceExtractBatchIdNumber2 || _cpFinanceExtractBatchIdNumber1.Equals(_cpFinanceExtractBatchIdNumber2 + 2));

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A2_" + currentTimeString)
                .ClickSearchButton()
                .OpenRecord(_cpFinanceExtractBatchId2); //Auto Generated Finance Extract Batch Record

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatchIdNumber1.ToString())
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A2_" + currentTimeString + " \\ 04/09/2023")
                .ValidateRunOnText("04/09/2023")
                .ValidateBatchStatusSelectedText("New")
                .ValidateExtractNameLinkText("CPEN_A2_" + currentTimeString)
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidateIsdownloaded_NoOptionChecked()
                .ValidateIsdownloaded_YesOptionNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateTotaldebitsText("")
                .ValidateTotalcreditsText("")
                .ValidateNumberofinvoicesextractedText("")
                .ValidateNumberofuniquepayersText("")
                .ValidateNumberofinvoicescancelledText("")
                .ValidateExtractyearText("")
                .ValidateExtractmonthText("")
                .ValidateExtractweekText("")
                .ValidateCompletedonText("")
                .ValidateCompletedon_TimeText("")
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .OpenRecord(_cpFinanceExtractBatches[1]); //Manually created Finance Extract Batch Record

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatchIdNumber3.ToString())
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A1_" + currentTimeString + " \\ " + DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickBackButton();

            #endregion

            #region Step 6 to Step 8
            //Process Auto Generated Batch 1
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickScheduledJobsLink();

            Guid processCpFinanceExtractsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process CP Finance Extract Batches")[0];

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad()
                .SearchRecord("Process CP Finance Extract Batches")
                .OpenRecord(processCpFinanceExtractsJobId.ToString());

            scheduleJobsRecordPage
                .WaitForScheduleJobsRecordPageToLoad()
                .TapExecuteJobButton()
                .WaitForScheduleJobsRecordPageToLoad()
                .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            dbHelper = new DBHelper.DatabaseHelper();
            //Wait for the Idle status
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processCpFinanceExtractsJobId);

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            //Completed Auto Generated Batch Record - Record A
            var _Completed_cpFinanceExtractBatchesForSetupId2 = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId2, _cpExtractNameId2, 2);
            //New Auto Generated Batch Record - Record B
            var _New_cpFinanceExtractBatchesForSetupId2 = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId2, _cpExtractNameId2);

            int _NewCpFinanceExtractBatchId2Number1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_New_cpFinanceExtractBatchesForSetupId2[0], "batchid")["batchid"];
            int _CompletedCpFinanceExtractBatchId2Number1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_Completed_cpFinanceExtractBatchesForSetupId2[0], "batchid")["batchid"];

            DateTime _CompletedCpFinanceExtractBatchId2_CompletedOnDate = (DateTime)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_Completed_cpFinanceExtractBatchesForSetupId2[0], "completedon")["completedon"];

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A2_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_New_cpFinanceExtractBatchesForSetupId2[0]);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_NewCpFinanceExtractBatchId2Number1.ToString())
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A2_" + currentTimeString + " \\ 04/09/2023")
                .ValidateRunOnText("11/09/2023")
                .ValidateRunOn_TimeText("03:00")
                .ValidateBatchStatusSelectedText("New")
                .ValidateExtractNameLinkText("CPEN_A2_" + currentTimeString)
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidateIsdownloaded_NoOptionChecked()
                .ValidateIsdownloaded_YesOptionNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateTotaldebitsText("")
                .ValidateTotalcreditsText("")
                .ValidateNumberofinvoicesextractedText("")
                .ValidateNumberofuniquepayersText("")
                .ValidateNumberofinvoicescancelledText("")
                .ValidateExtractyearText("")
                .ValidateExtractmonthText("")
                .ValidateExtractweekText("")
                .ValidateCompletedonText("")
                .ValidateCompletedon_TimeText("")

                .ValidateRunOnFieldDisabled(false)
                .ValidateRunOnDatePickerDisabled(false)
                .ValidateRunOn_TimeFieldDisabled(false)
                .ValidateRunon_TimePickerFieldDisabled(false)
                .ValidateBatchIDFieldDisabled(true)
                .ValidateFinanceExtractBatchSetupLookupButtonDisabled(true)
                .ValidateBatchStatusFieldDisabled(true)
                .ValidateExtractNameLookupButtonDisabled(true)
                .ValidateIsAdHocOptionsDisabled(true)
                .ValidateNetBatchTotalDisabled(true)
                .ValidateGrossBatchTotalDisabled(true)
                .ValidateVatTotalDisabled(true)
                .ValidateTotalDebitsDisabled(true)
                .ValidateTotalCreditsDisabled(true)
                .ValidateNumberOfInvoicesExtractedDisabled(true)
                .ValidateNumberOfUniquePayersDisabled(true)
                .ValidateNumberOfInvoicesCancelledDisabled(true)
                .ValidateExtractYearDisabled(true)
                .ValidateExtractMonthDisabled(true)
                .ValidateExtractWeekDisabled(true)
                .ValidateIsDownloadedOptionsDisabled(true)
                .ValidateCompletedOnFieldDisabled(true)
                .ValidateCompletedOnDatePickerDisabled(true)
                .ValidateCompletedOn_TimeDisabled(true)
                .ValidateCompletedOn_TimePickerDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_Completed_cpFinanceExtractBatchesForSetupId2[0]);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_CompletedCpFinanceExtractBatchId2Number1.ToString())
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A2_" + currentTimeString + " \\ 04/09/2023")
                .ValidateRunOnText("04/09/2023")
                .ValidateRunOn_TimeText("02:00")
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateExtractNameLinkText("CPEN_A2_" + currentTimeString)
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidateIsdownloaded_NoOptionChecked()
                .ValidateIsdownloaded_YesOptionNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateNetbatchtotalText("0.00")
                .ValidateGrossbatchtotalText("0.00")
                .ValidateVattotalText("0.00")
                .ValidateTotaldebitsText("0.00")
                .ValidateTotalcreditsText("0.00")
                .ValidateNumberofinvoicesextractedText("0")
                .ValidateNumberofuniquepayersText("0")
                .ValidateNumberofinvoicescancelledText("")
                .ValidateExtractyearText("")
                .ValidateExtractmonthText("")
                .ValidateExtractweekText("")
                .ValidateCompletedonText(_CompletedCpFinanceExtractBatchId2_CompletedOnDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateCompletedon_TimeText(_CompletedCpFinanceExtractBatchId2_CompletedOnDate.ToLocalTime().ToString("HH:mm"))
                .ValidateRunOnFieldDisabled(true)
                .ValidateRunOnDatePickerDisabled(true)
                .ClickBackButton();

            //Complete the New Auto Generated Batch Record - Record B
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickScheduledJobsLink();

            processCpFinanceExtractsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process CP Finance Extract Batches")[0];

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad()
                .SearchRecord("Process CP Finance Extract Batches")
                .OpenRecord(processCpFinanceExtractsJobId.ToString());

            scheduleJobsRecordPage
                .WaitForScheduleJobsRecordPageToLoad()
                .TapExecuteJobButton();

            var _Date = commonMethodsHelper.GetCurrentDateWithoutCulture();

            scheduleJobsRecordPage
                .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            dbHelper = new DBHelper.DatabaseHelper();
            //Wait for the Idle status
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processCpFinanceExtractsJobId);

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad();

            _Completed_cpFinanceExtractBatchesForSetupId2 = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId2, _cpExtractNameId2, 2);
            DateTime _CompletedCpFinanceExtractBatchId2_CompletedOnDate2 = (DateTime)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_Completed_cpFinanceExtractBatchesForSetupId2[0], "completedon")["completedon"];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A2_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_New_cpFinanceExtractBatchesForSetupId2[0]);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_NewCpFinanceExtractBatchId2Number1.ToString())
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A2_" + currentTimeString + " \\ 04/09/2023")
                .ValidateRunOnText("11/09/2023")
                .ValidateRunOn_TimeText("03:00")
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateExtractNameLinkText("CPEN_A2_" + currentTimeString)
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidateIsdownloaded_NoOptionChecked()
                .ValidateIsdownloaded_YesOptionNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateNetbatchtotalText("0.00")
                .ValidateGrossbatchtotalText("0.00")
                .ValidateVattotalText("0.00")
                .ValidateTotaldebitsText("0.00")
                .ValidateTotalcreditsText("0.00")
                .ValidateNumberofinvoicesextractedText("0")
                .ValidateNumberofuniquepayersText("0")
                .ValidateNumberofinvoicescancelledText("")
                .ValidateExtractyearText("")
                .ValidateExtractmonthText("")
                .ValidateExtractweekText("")
                .ValidateCompletedonText(_Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateCompletedon_TimeText(_Date.ToString("HH:mm"))

                .ValidateRunOnFieldDisabled(true)
                .ValidateRunOnDatePickerDisabled(true)
                .ValidateRunOn_TimeFieldDisabled(true)
                .ValidateRunon_TimePickerFieldDisabled(true)
                .ValidateBatchIDFieldDisabled(true)
                .ValidateFinanceExtractBatchSetupLookupButtonDisabled(true)
                .ValidateBatchStatusFieldDisabled(true)
                .ValidateExtractNameLookupButtonDisabled(true)
                .ValidateIsAdHocOptionsDisabled(true)
                .ValidateNetBatchTotalDisabled(true)
                .ValidateGrossBatchTotalDisabled(true)
                .ValidateVatTotalDisabled(true)
                .ValidateTotalDebitsDisabled(true)
                .ValidateTotalCreditsDisabled(true)
                .ValidateNumberOfInvoicesExtractedDisabled(true)
                .ValidateNumberOfUniquePayersDisabled(true)
                .ValidateNumberOfInvoicesCancelledDisabled(true)
                .ValidateExtractYearDisabled(true)
                .ValidateExtractMonthDisabled(true)
                .ValidateExtractWeekDisabled(true)
                .ValidateIsDownloadedOptionsDisabled(true)
                .ValidateCompletedOnFieldDisabled(true)
                .ValidateCompletedOnDatePickerDisabled(true)
                .ValidateCompletedOn_TimeDisabled(true)
                .ValidateCompletedOn_TimePickerDisabled(true)
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ClickBackButton();

            //New Auto Generated Batch Record - Record C
            _New_cpFinanceExtractBatchesForSetupId2 = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId2, _cpExtractNameId2);
            int _NewCpFinanceExtractBatchId2Number3 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_New_cpFinanceExtractBatchesForSetupId2[0], "batchid")["batchid"];

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A2_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_New_cpFinanceExtractBatchesForSetupId2[0]);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_NewCpFinanceExtractBatchId2Number3.ToString())
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A2_" + currentTimeString + " \\ 04/09/2023")
                .ValidateRunOnText("18/09/2023")
                .ValidateRunOn_TimeText("03:00")
                .ValidateBatchStatusSelectedText("New")
                .ValidateExtractNameLinkText("CPEN_A2_" + currentTimeString)
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidateIsdownloaded_NoOptionChecked()
                .ValidateIsdownloaded_YesOptionNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateTotaldebitsText("")
                .ValidateTotalcreditsText("")
                .ValidateNumberofinvoicesextractedText("")
                .ValidateNumberofuniquepayersText("")
                .ValidateNumberofinvoicescancelledText("")
                .ValidateExtractyearText("")
                .ValidateExtractmonthText("")
                .ValidateExtractweekText("")
                .ValidateCompletedonText("")
                .ValidateCompletedon_TimeText("");

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn(DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy"))
                .ClickRunOn_TimePicker();

            calendarTimePicker
                .WaitForCalendarTimePickerPopupToLoad()
                .SelectHour("08")
                .SelectMinute("30");

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateRunOnText(DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("08:30")
                .ValidateFinanceExtractBatchRecordTitle("CPEN_A2_" + currentTimeString + " \\ " + DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy") + " 08:30:00");

            #endregion

            #region Step 9
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time(DateTime.Now.AddHours(-1).ToString("HH:30"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("For this record to be saved successfully, the Run On date/time must be later than the current date/time. Please correct as necessary.")
                .TapCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4649

        [TestProperty("JiraIssueID", "ACC-4663")]
        [Description("Test case for Automated FEB Creation + Scheduled Job - Test automation for Steps 10 to 15")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Extract Batches")]
        [TestProperty("Screen2", "Advanced Search")]
        [TestProperty("Screen3", "Scheduled Jobs")]
        public void CPFinance_FinanceExtractBatch_UITestMethod002()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Step 10

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

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpExtractTypeId1, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickNewRecordButton();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickFinanceExtractBatchSetupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CPEN_A1_" + currentTimeString)
                .TapSearchButton()
                .SelectResultElement(_cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn(currentDate.ToString("dd'/'MM'/'yyyy"));

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            var RunOnTime1 = commonMethodsHelper.GetCurrentDateWithoutCulture(true).AddMinutes(1);
            var RunOnTimeTextInTitle1 = RunOnTime1.ToString("HH:mm:00");

            cpFinanceExtractBatchRecordPage
                .InsertTextOnRunon_Time(RunOnTime1.ToString("HH:mm"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateFinanceExtractBatchRecordTitle("CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy") + " " + RunOnTimeTextInTitle1)
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOnText(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(RunOnTime1.AddSeconds(2).ToString("HH:mm"))
                .ValidateExtractNameLinkText("CPEN_A1_" + currentTimeString)
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ClickBackButton();

            _cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);

            Assert.AreEqual(2, _cpFinanceExtractBatchId.Count);

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickNewRecordButton();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickFinanceExtractBatchSetupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CPEN_A1_" + currentTimeString)
                .TapSearchButton()
                .SelectResultElement(_cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn(currentDate.ToString("dd'/'MM'/'yyyy"));

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            var RunOnTime2 = commonMethodsHelper.GetCurrentDateWithoutCulture(true).AddMinutes(1);
            var RunOnTimeTextInTitle2 = RunOnTime2.ToString("HH:mm:00");
            cpFinanceExtractBatchRecordPage
                .InsertTextOnRunon_Time(RunOnTime2.AddSeconds(2).ToString("HH:mm"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateFinanceExtractBatchRecordTitle("CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy") + " " + RunOnTimeTextInTitle2)
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOnText(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(RunOnTime2.AddSeconds(2).ToString("HH:mm"))
                .ValidateExtractNameLinkText("CPEN_A1_" + currentTimeString)
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            _cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);

            Assert.AreEqual(3, _cpFinanceExtractBatchId.Count);
            #endregion

            #region Step 11

            //Step 11 is same as Step 6 - To Verify that the autogenerated FEB record has the values as given

            /* * Batch ID – the next ID available
               *Finance Extract Batch Setup – link to the FEBS record this record is being created from
               * Run On – based on Extract Frequency and Day fields
               * Batch Status – New
               * Extract Name – same as in FEBS
               * Ad Hoc batch? – No
               * Null for all other fields(Except below)
               * Downloaded - No
               * Responsible Team - Same as in FEBS
               */

            #endregion

            #region Step 12

            System.Threading.Thread.Sleep(60000);
            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            //Get the schedule job id
            Guid processCpFinanceExtractsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Process CP Finance Extract Batches")[0];

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            var _completed_cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1, 2);

            Assert.AreEqual(3, _completed_cpFinanceExtractBatchId.Count); //Completed Batch records after processing the above Finance Extract Batch Setup. There will be 3 records: 1 Adhoc record, 2 Non Adhoc records.

            _cpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1, 1);

            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count); //New Batch records after processing above Finance Extract Batch Setup. There will be only 1 record generated after processing batch record, this is for which IsAdhoc flag is No.

            var _new_IsAdhoc_CpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractNameAndIsAdhocBatch(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1, true, 1);

            Assert.AreEqual(0, _new_IsAdhoc_CpFinanceExtractBatchId.Count); //New Batch records for above Finance Extract Batch Setup. New batch record is not generated after processing batch recod for which IsAdhoc flag is Yes.

            var _completed_IsAdhoc_CpFinanceExtractBatchId = dbHelper
                                            .careProviderFinanceExtractBatch
                                            .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractNameAndIsAdhocBatch(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1, true, 2);
            Assert.AreEqual(2, _completed_IsAdhoc_CpFinanceExtractBatchId.Count); //Completed Batch record for above Finance Extract Batch Setup. for which IsAdhoc flag is Yes.

            #endregion

            #region Step 13

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ValidateNewRecordButtonIsDisplayed(true)
                .ValidateExportToExcelButtonIsDisplayed(true)
                .ValidateMailMergeButtonIsDisplayed(true);

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickNewRecordButton();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickFinanceExtractBatchSetupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CPEN_A1_" + currentTimeString)
                .TapSearchButton()
                .SelectResultElement(_cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn(currentDate.ToString("dd'/'MM'/'yyyy"));

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            var RunOnTime3 = commonMethodsHelper.GetCurrentDateWithoutCulture(true);
            var RunOnTimeTextInTitle3 = RunOnTime3.ToString("HH:mm:00");

            cpFinanceExtractBatchRecordPage
                .InsertTextOnRunon_Time(RunOnTime3.AddSeconds(1).ToString("HH:mm"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateFinanceExtractBatchRecordTitle("CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy") + " " + RunOnTimeTextInTitle3)
                .ValidateExtractNameLinkText("CPEN_A1_" + currentTimeString)
                .ValidateBatchStatusSelectedText("New")
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked();

            cpFinanceExtractBatchRecordPage
                .ValidateSaveButtonIsDisplayed(true)
                .ValidateSaveAndCloseButtonIsDisplayed(true)
                .ValidateAssignRecordButtonIsDisplayed(true)
                .ValidateRunExtractBatchButtonIsDisplayed(true)
                .ValidateDeleteButtonIsDisplayed(true)
                .ValidateCopyRecordLinkButtonIsDisplayed(true)
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_completed_IsAdhoc_CpFinanceExtractBatchId[0]);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateFinanceExtractBatchRecordTitle("CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy") + " " + RunOnTimeTextInTitle2)
                .ValidateExtractNameLinkText("CPEN_A1_" + currentTimeString)
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked();

            cpFinanceExtractBatchRecordPage
                .ValidateSaveButtonIsDisplayed(true)
                .ValidateSaveAndCloseButtonIsDisplayed(true)
                .ValidateAssignRecordButtonIsDisplayed(true)
                .ValidateRunExtractBatchButtonIsDisplayed(false)
                .ValidateDeleteButtonIsDisplayed(false)
                .ValidateCopyRecordLinkButtonIsDisplayed(true)
                .ClickBackButton();

            #endregion

            #region Step 14

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Extract Batches")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Batch ID")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Finance Extract Batch Setup")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Run On")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Batch Status")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Extract Name")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Ad Hoc Batch?")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Net Batch Total")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Completed On")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Downloaded?")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Responsible Team")
                .SelectFilter("1", "Name")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "CPEN_A1_" + currentTimeString + " \\ " + currentDate.ToString("dd'/'MM'/'yyyy") + " " + RunOnTimeTextInTitle1)
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_completed_IsAdhoc_CpFinanceExtractBatchId[1].ToString());

            advanceSearchPage
                .ResultsPageValidateHeaderCellText(2, "Extract Name")
                .ResultsPageValidateHeaderCellText(3, "Batch ID")
                .ResultsPageValidateHeaderCellText(4, "Batch Status")
                .ResultsPageValidateHeaderCellText(5, "Run On")
                .ResultsPageValidateHeaderCellText(6, "Ad Hoc Batch?")
                .ResultsPageValidateHeaderCellText(8, "Net Batch Total")
                .ResultsPageValidateHeaderCellText(9, "Gross Batch Total")
                .ResultsPageValidateHeaderCellText(10, "VAT Total")
                .ResultsPageValidateHeaderCellText(11, "Total Debits")
                .ResultsPageValidateHeaderCellText(12, "Total Credits")
                .ResultsPageValidateHeaderCellText(13, "Number Of Invoices Extracted")
                .ResultsPageValidateHeaderCellText(14, "Number Of Invoices Cancelled")
                .ResultsPageValidateHeaderCellText(15, "Number Of Unique Payers")
                .ResultsPageValidateHeaderCellText(17, "Completed On")
                .ResultsPageValidateHeaderCellText(18, "Finance Extract Batch Setup");

            advanceSearchPage
                .ClickBackButton_ResultsPage();

            #endregion

            #region Step 15

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickScheduledJobsLink();

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad()
                .SearchRecord("Process CP Finance Extract Batches")
                .ValidateNameCell(processCpFinanceExtractsJobId.ToString(), "Process CP Finance Extract Batches")
                .OpenRecord(processCpFinanceExtractsJobId.ToString());

            scheduleJobsRecordPage
                .WaitForScheduleJobsRecordPageToLoad()
                .ValidateInactiveNoOptionSelected(true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4650

        [TestProperty("JiraIssueID", "ACC-4697")]
        [Description("Test case for Automated FEB Creation + Scheduled Job - Test automation for Steps 16 to 20")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen", "Finance Extract Batches")]
        public void CPFinance_FinanceExtractBatch_UITestMethod003()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "ACC-4650_1 " + currentTimeString;
            var provider2Name = "ACC-4650_2 " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Extract Name, Extract Type, Finance Extract Batch Setup

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
            {
                newExtractNameCode1 = newExtractNameCode1 + 1;
            }

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type = Generic Extract File

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = currentDate;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(currentDate.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 5, 14);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(30);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId, runOn, new DateTime(2024, 5, 14), new DateTime(2024, 5, 21));

            Thread.Sleep(28000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Step 16 to Step 20

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch1Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateFinanceInvoiceTabIsDisplayed(false)
                .ValidateFinanceTransactionTabIsDisplayed(false)
                .ClickBackButton();

            #region Process CP Finance Extract Batches

            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            System.Threading.Thread.Sleep(1500);

            var fields = dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "runon", "completedon");
            DateTime RunOnTime1 = (DateTime)fields["runon"];
            DateTime CompletedOnTime1 = (DateTime)fields["completedon"];

            _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(2, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch2Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch2IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch2Id, "batchid")["batchid"];

            var runOnExpectedValue = DateTime.Now.IsDaylightSavingTime() ? RunOnTime1.AddDays(7).AddHours(1).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss") : RunOnTime1.AddDays(7).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            cpFinanceExtractBatchesPage
                .ValidateRecordIsPresent(_cpFinanceExtractBatch2Id.ToString(), true)
                .ValidateRecordCellContent(_cpFinanceExtractBatch2Id.ToString(), 3, _cpFinanceExtractBatch2IdNumber1.ToString())
                .ValidateRecordCellContent(_cpFinanceExtractBatch2Id.ToString(), 4, "New")
                .ValidateRecordCellContent(_cpFinanceExtractBatch2Id.ToString(), 5, runOnExpectedValue)
                .OpenRecord(_cpFinanceExtractBatch1Id);

            var _extractedFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByFinanceInvoiceStatusIdAndFinanceExtractBatchId(3, _cpFinanceExtractBatch1Id)[0];
            string _extractedInvoiceIdNumber = (string)dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_extractedFinanceInvoiceId, "invoicenumber")["invoicenumber"];

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch1IdNumber1.ToString())
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A1_" + currentTimeString + " \\ " + RunOnTime1.ToLocalTime().ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOnText(RunOnTime1.ToLocalTime().ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(RunOnTime1.ToLocalTime().ToString("HH:mm"))
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateExtractNameLinkText("CPEN_A1_" + currentTimeString)
                .ValidateInvoiceFiles_FileLinkText("ACC-4650_2 " + currentTimeString + " " + _extractedInvoiceIdNumber + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ValidateExtractcontent_FileLinkText("CPEN_A1_" + currentTimeString + " " + _cpFinanceExtractBatch1IdNumber1 + ".csv")
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidateIsdownloaded_NoOptionChecked()
                .ValidateIsdownloaded_YesOptionNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateNetbatchtotalText("10.00")
                .ValidateGrossbatchtotalText("12.00")
                .ValidateVattotalText("2.00")
                .ValidateTotaldebitsText("10.00")
                .ValidateTotalcreditsText("0.00")
                .ValidateNumberofinvoicesextractedText("1")
                .ValidateNumberofuniquepayersText("1")
                .ValidateNumberofinvoicescancelledText("")
                .ValidateExtractyearText("")
                .ValidateExtractmonthText("")
                .ValidateExtractweekText("")
                .ValidateCompletedonText(CompletedOnTime1.ToLocalTime().ToString("dd'/'MM'/'yyyy"))
                .ValidateCompletedon_TimeText(CompletedOnTime1.ToLocalTime().ToString("HH:mm"))
                .ValidateFinanceExtractBatchRecordTitle("CPEN_A1_" + currentTimeString + " \\ " + RunOnTime1.ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss"));

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateFinanceInvoiceTabIsDisplayed(true)
                .ValidateFinanceTransactionTabIsDisplayed(true);

            cpFinanceExtractBatchRecordPage
                .ClickFinanceInvoiceTab();

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad(_cpFinanceExtractBatch1Id.ToString())
                .ValidateRecordIsPresent(_cpFinanceInvoiceId.ToString(), true)
                .ValidateRecordCellContent(_cpFinanceInvoiceId.ToString(), 9, "Extracted");

            var _cpFinanceTransactionId = dbHelper.careProviderFinanceTransaction.GetFinanceTransactionByFinanceInvoiceIdAndFinanceExtractBatchIdAndStartDateAndEndDate(_cpFinanceInvoiceId, _cpFinanceExtractBatch1Id, new DateTime(2024, 5, 14), new DateTime(2024, 5, 20))[0];

            //wait and click finance transactions tab
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickFinanceTransactionTab();

            //validate cp finance transaction record is present
            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad(_cpFinanceExtractBatch1Id.ToString())
                .ValidateRecordIsPresent(_cpFinanceTransactionId.ToString(), true)
                .ValidateRecordCellContent(_cpFinanceTransactionId.ToString(), 21, "Extracted");

            //wait click back button in CP finance extract record page
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickBackButton();

            //wait open CP finance extract record 2
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch2Id);

            //insert run on_time in cp finance extract batch record page and click run extract batch
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunon_Time(RunOnTime1.AddMinutes(1).ToString("HH:mm"))
                .ClickRunExtractBatchButton();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("Please save changes to this record before trying to run the Extract Batch.")
                .TapCloseButton();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            //navigate to main menu finance invoice batches page
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            //dbhelper method to GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];
            var _cpFinanceInvoiceBatchId2 = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            //wait for finance invoice batches page to load and open finance invoice batch 2 record
            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .InsertSearchQuery(contractSchemeName)
                .ClickSearchButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2);

            //wait for finance invoice batch record page to load and insert period start date 10 days ahead
            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .InsertTextOnPeriodStartDate(currentDate.AddDays(10).ToString("dd'/'MM'/'yyyy"))
                .ClickRunInvoiceBatchButton();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("Please save changes to this record before trying to run the Invoice Batch.")
                .TapCloseButton();

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4789

        /*
         * The below tests updated due to changes being made to the application as per - Refer https://advancedcsg.atlassian.net/browse/ACC-2395
         * 
         */

        [TestProperty("JiraIssueID", "ACC-4801")]
        [Description("Test case for Automated FEB Creation + Scheduled Job - Test automation for Steps 21 to 24")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Extract Batches")]
        [TestProperty("Screen2", "Finance Invoices")]
        [TestProperty("Screen3", "Finance Transaction")]
        public void CPFinance_FinanceExtractBatch_UITestMethod004()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "ACC-4801_1 " + currentTimeString;
            var provider2Name = "ACC-4801_2 " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Extract Name, Extract Type, Finance Extract Batch Setup

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
            {
                newExtractNameCode1 = newExtractNameCode1 + 1;
            }

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type = Generic Extract File

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = currentDate;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(currentDate.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 5, 14);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(30);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId, runOn, new DateTime(2024, 5, 14), new DateTime(2024, 5, 21));
            Thread.Sleep(28000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch1Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateFinanceInvoiceTabIsDisplayed(false)
                .ValidateFinanceTransactionTabIsDisplayed(false)
                .ClickBackButton();

            #region Process CP Finance Extract Batches

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            DateTime RunOnTime1 = (DateTime)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "runon")["runon"];

            _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(2, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch2Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch2IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch2Id, "batchid")["batchid"];

            var runOnExpectedValue = DateTime.Now.IsDaylightSavingTime() ? RunOnTime1.ToLocalTime().AddDays(7).AddHours(1).ToString("dd'/'MM'/'yyyy HH:mm:ss") : RunOnTime1.ToLocalTime().AddDays(7).ToString("dd'/'MM'/'yyyy HH:mm:ss");

            cpFinanceExtractBatchesPage
                .ValidateRecordIsPresent(_cpFinanceExtractBatch2Id.ToString(), true)
                .ValidateRecordCellContent(_cpFinanceExtractBatch2Id.ToString(), 3, _cpFinanceExtractBatch2IdNumber1.ToString())
                .ValidateRecordCellContent(_cpFinanceExtractBatch2Id.ToString(), 4, "New")
                .ValidateRecordCellContent(_cpFinanceExtractBatch2Id.ToString(), 5, runOnExpectedValue)
                .OpenRecord(_cpFinanceExtractBatch1Id);


            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch1IdNumber1.ToString())
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A1_" + currentTimeString + " \\ " + RunOnTime1.ToLocalTime().ToString("dd'/'MM'/'yyyy"))
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateInvoiceFiles_FileLinkText("")
                .ValidateExtractcontent_FileLinkText("")
                .ClickFinanceInvoiceTab();

            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId, 1)[0];

            cpFinanceInvoicesPage
                .WaitForCPFinanceInvoicesPageToLoad(_cpFinanceExtractBatch1Id.ToString())
                .ValidateRecordIsPresent(_cpFinanceInvoiceId.ToString(), false)
                .ValidateNoRecordsMessageVisible();

            var _cpFinanceTransactionId = dbHelper.careProviderFinanceTransaction.GetFinanceTransactionByFinanceInvoiceIdAndStartDateAndEndDate(_cpFinanceInvoiceId, new DateTime(2024, 5, 14), new DateTime(2024, 5, 20))[0];

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickFinanceTransactionTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad(_cpFinanceExtractBatch1Id.ToString())
                .ValidateRecordIsPresent(_cpFinanceTransactionId.ToString(), false)
                .ValidateNoRecordsMessageVisible(true);

            var _cpFinanceTransactionIds2 = dbHelper.careProviderFinanceTransaction.GetFinanceTransactionByFinanceInvoiceIdAndFinanceExtractBatchIdAndStartDateAndEndDate(_cpFinanceInvoiceId, _cpFinanceExtractBatch1Id, new DateTime(2024, 5, 14), new DateTime(2024, 5, 21));
            Assert.AreEqual(0, _cpFinanceTransactionIds2.Count);

            #endregion

            #region Step 22

            #region Process CP Finance Extract Batches - update care provider finance extract batch record before processing

            var _cpFinanceInvoiceId1 = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId1, 2);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch2Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn(commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time(commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(30).ToString("HH:mm"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickRunExtractBatchButton()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            #endregion

            var _extractedFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByFinanceInvoiceStatusIdAndFinanceExtractBatchId(3, _cpFinanceExtractBatch2Id)[0];
            string _extractedInvoiceIdNumber = (string)dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_extractedFinanceInvoiceId, "invoicenumber")["invoicenumber"];

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch2IdNumber1.ToString())
                .ValidateBatchStatusSelectedText("Completed")
                //.ValidateInvoiceFiles_FileLinkText("CareProviderFinanceInvoiceGenericTemplate.pdf") //Updated as per changes made to the application as per ACC-2395
                .ValidateInvoiceFiles_FileLinkText("ACC-4801_2 " + currentTimeString + " " + _extractedInvoiceIdNumber + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ValidateExtractcontent_FileLinkText("CPEN_A1_" + currentTimeString + " " + _cpFinanceExtractBatch2IdNumber1 + ".csv");

            cpFinanceExtractBatchRecordPage
                .ClickBackButton();

            //wait for CP Finance Extract page to load and select CP Finance Extract Record 2 and click mail merge button
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .SelectRecord(_cpFinanceExtractBatch2Id)
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickMailMergeButton();

            //wait for create invoice file popup and lookup mail merge template lookup button
            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickMailMergeTemplateLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Care Provider Finance Invoice Generic Template", _invoiceGenerateTemplate_MailMergeId);

            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Invoice Files generated for 0 of 1 records.")
                .TapCloseButton();

            //wait for CP Finance Extract page to load and open CP Finance Extract Record 2 and click extract content button
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch2Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateInvoiceFiles_FileLinkText("ACC-4801_2 " + currentTimeString + " " + _extractedInvoiceIdNumber + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ValidateExtractcontent_FileLinkText("CPEN_A1_" + currentTimeString + " " + _cpFinanceExtractBatch2IdNumber1 + ".csv");

            #endregion

            #region Step 23
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickBackButton();

            _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1, 1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch3Id = _cpFinanceExtractBatchId[0];

            //wait for Finance Extract page to load and select CP Finance Extract Record 3 and click mail merge button
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickRefreshButton()
                .SelectRecord(_cpFinanceExtractBatch3Id)
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickMailMergeButton();

            //wait for create invoice file popup and lookup mail merge template lookup button
            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickMailMergeTemplateLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Care Provider Finance Invoice Generic Template", _invoiceGenerateTemplate_MailMergeId);

            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Invoice Files generated for 0 of 1 records.")
                .TapCloseButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch3Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateInvoiceFiles_FileLinkText("");


            #endregion

            #region Step 24            

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .SelectRecord(_cpFinanceExtractBatch2Id)
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickMailMergeButton();

            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickMailMergeTemplateLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Care Provider Finance Invoice Generic Template", _invoiceGenerateTemplate_MailMergeId);

            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Invoice Files generated for 0 of 1 records.")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4833")]
        [Description("Test case for Automated FEB Creation + Scheduled Job - Test automation for Steps 25 to 26")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Extract Batches")]
        [TestProperty("Screen2", "Finance Invoices")]
        [TestProperty("Screen3", "Finance Transaction")]
        public void CPFinance_FinanceExtractBatch_UITestMethod005()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "ACC-4833_1 " + currentTimeString;
            var provider2Name = "ACC-4833_2 " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Extract Name, Extract Type, Finance Extract Batch Setup

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
            {
                newExtractNameCode1 = newExtractNameCode1 + 1;
            }

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, currentDate);
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type = Generic Extract File

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, currentDate, new TimeSpan(2, 0, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, currentDate);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = DateTime.Now;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(DateTime.Now.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Care Provider Person Contract
            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches - 1

            var _cpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(2);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId, runOn, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-7));

            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);

            #endregion

            #region Step 25

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            #region Process CP Finance Extract Batches - 1

            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(2, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch2Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch2IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch2Id, "batchid")["batchid"];

            var _extractedFinanceInvoiceId =  dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByFinanceInvoiceStatusIdAndFinanceExtractBatchId(3, _cpFinanceExtractBatch1Id)[0];
            string _extractedInvoiceIdNumber = (string) dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_extractedFinanceInvoiceId, "invoicenumber")["invoicenumber"];

            cpFinanceExtractBatchesPage
                .ValidateRecordIsPresent(_cpFinanceExtractBatch2Id.ToString(), true)
                .OpenRecord(_cpFinanceExtractBatch1Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch1IdNumber1.ToString())
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateInvoiceFiles_FileLinkText("ACC-4833_2 "+ currentTimeString + " " + _extractedInvoiceIdNumber + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ClickBackButton();


            #region Process CP Finance Invoice Batches - 2

            var _cpFinanceInvoiceBatchId2 = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var _invoiceBatchRunOn = commonMethodsHelper.GetCurrentDateWithoutCulture().AddSeconds(2);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId2, _invoiceBatchRunOn, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-6), commonMethodsHelper.GetThisWeekFirstMonday());

            Thread.Sleep(3000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId, 2);
            Assert.AreEqual(2, completedFinanceInvoiceBatchIds.Count);

            #endregion

            #region Process CP Finance Extract Batches - 2 

            var _cpFinanceInvoiceId2 = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId2, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId2, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId2, 2);

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch2Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn(commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time(commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("HH:mm"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickRunExtractBatchButton()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            _cpFinanceExtractBatchId = dbHelper
                .careProviderFinanceExtractBatch
                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);

            Assert.AreEqual(3, _cpFinanceExtractBatchId.Count);

            var _extractedFinanceInvoiceId2 = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByFinanceInvoiceStatusIdAndFinanceExtractBatchId(3, _cpFinanceExtractBatch2Id)[0];
            string _extractedInvoiceId2Number = (string)dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_extractedFinanceInvoiceId2, "invoicenumber")["invoicenumber"];

            Guid _cpFinanceExtractBatch3Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch3IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch3Id, "batchid")["batchid"];

            #endregion

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch2IdNumber1.ToString())
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateInvoiceFiles_FileLinkText("ACC-4833_2 " + currentTimeString + " " + _extractedInvoiceId2Number + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf");

            cpFinanceExtractBatchRecordPage
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch3Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch3IdNumber1.ToString())
                .ValidateBatchStatusSelectedText("New")
                .ValidateInvoiceFiles_FileLinkText("")
                .ValidateExtractcontent_FileLinkText("")
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickMailMergeButton();

            //wait for create invoice file popup and lookup mail merge template lookup button
            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickAllRecordsFromSelectedViewRadioButton()
                .ClickMailMergeTemplateLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Care Provider Finance Invoice Generic Template", _invoiceGenerateTemplate_MailMergeId);

            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Invoice Files generated for 0 of 3 records.")
                .TapCloseButton();

            //wait for CP Finance Extract page to load and open CP Finance Extract Record 2 and click extract content button
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch1Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateInvoiceFiles_FileLinkText("ACC-4833_2 " + currentTimeString + " " + _extractedInvoiceIdNumber + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ValidateExtractcontent_FileLinkText("CPEN_A1_" + currentTimeString + " " + _cpFinanceExtractBatch1IdNumber1 + ".csv");

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch2Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateInvoiceFiles_FileLinkText("ACC-4833_2 " + currentTimeString + " " + _extractedInvoiceId2Number + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ValidateExtractcontent_FileLinkText("CPEN_A1_" + currentTimeString + " " + _cpFinanceExtractBatch2IdNumber1 + ".csv");

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickBackButton();

            #endregion

            #region Step 26

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickMailMergeButton();

            //wait for create invoice file popup and lookup mail merge template lookup button
            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickMailMergeTemplateLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Care Provider Finance Invoice Generic Template", _invoiceGenerateTemplate_MailMergeId);

            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Invoice Files generated for 0 of 3 records.")
                .TapCloseButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch3Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateInvoiceFiles_FileLinkText("");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4844

        /*
         * The test is updated due to changes being made to the application as per - Refer https://advancedcsg.atlassian.net/browse/ACC-2395
         * 
         */

        [TestProperty("JiraIssueID", "ACC-4845")]
        [Description("Test case for Automated FEB Creation + Scheduled Job - Test automation for Steps 27 to 31")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Extract Batches")]
        [TestProperty("Screen2", "Finance Extract Batch Setups")]
        public void CPFinance_FinanceExtractBatch_UITestMethod006()
        {
            #region VAT Code
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _careProviderVATCodeName = "Standard Rated";
            Guid _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName(_careProviderVATCodeName)[0];

            #endregion

            #region Care Provider Rate Unit

            string _careProviderRateUnitName = "Per Week Pro Rata";
            Guid _careProviderRateUnitId = dbHelper.careProviderRateUnit.GetByName(_careProviderRateUnitName)[0];

            #endregion

            #region Care Provider Batch Grouping

            string _careProviderBatchGroupingName = "Monthly";
            Guid _careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(_careProviderBatchGroupingName)[0];

            #endregion

            #region Provider

            var provider1Name = "ACC-4845_1 " + currentTimeString;
            var provider2Name = "ACC-4845_2 " + currentTimeString;


            var providerType1 = 13; //Residential Establishment
            var provider1ID = commonMethodsDB.CreateProvider(provider1Name, _teamId, providerType1, false);

            var providerType2 = 10; //Local Authority
            var provider2ID = commonMethodsDB.CreateProvider(provider2Name, _teamId, providerType2, false);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Aaron";
            var lastName = currentTimeString;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Care Provider Service

            var _careProviderServiceName = "Care Provider Service Record";
            var _careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, _careProviderServiceName, new DateTime(2020, 1, 1), 7895, null, false);

            #endregion

            #region Care Provider Service Detail

            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var careProviderServiceDetailName = "Care Provider Service Details Test 1";
            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, careProviderServiceDetailName, code, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(_careProviderServiceId, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Extract Name, Extract Type, Finance Extract Batch Setup

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            var ExtractNames = dbHelper.careProviderExtractName.GetAllCareProviderExtractName();
            int newExtractNameCode1 = ExtractNames.Count();

            while (dbHelper.careProviderExtractName.GetByCode(newExtractNameCode1).Any())
            {
                newExtractNameCode1 = newExtractNameCode1 + 1;
            }

            var _cpExtractNameId1 = commonMethodsDB.CreateCareProviderExtractName(_teamId, "CPEN_A1_" + currentTimeString, newExtractNameCode1, null, new DateTime(2023, 9, 1));
            var _cpGenericExtractTypeId = dbHelper.careProviderExtractType.GetByCode(1)[0]; //Extract Type: Generic Extract File

            Guid regardingId = _teamId;
            string regardingIdTableName = "team";
            string regardingIdName = _teamName;

            var _cpFinanceExtractBatchSetupId1 = commonMethodsDB.CreateCareProviderFinanceExtractBatchSetup(_teamId, new DateTime(2023, 9, 19), new TimeSpan(7, 30, 0), null, _cpExtractNameId1, 1, _cpGenericExtractTypeId, _invoiceGenerateTemplate_MailMergeId, regardingId, regardingIdTableName, regardingIdName);
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(_cpFinanceExtractBatchSetupId1, new DateTime(2023, 9, 19));
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, true);

            var _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(1, _cpFinanceExtractBatchId.Count);
            Guid _cpFinanceExtractBatch1Id = _cpFinanceExtractBatchId[0];
            int _cpFinanceExtractBatch1IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatch1Id, "batchid")["batchid"];

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "CS_" + currentTimeString;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetAll().Count + 1;
            while (dbHelper.careProviderContractScheme.GetByCode(contractSchemeCode).Any())
                contractSchemeCode = contractSchemeCode + 1;

            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider1ID, provider2ID);

            #endregion

            #region Care Provider Contract Service

            var CareProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", provider1ID, provider2ID, careProviderContractSchemeId, _careProviderServiceId, careProviderServiceDetailId, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.UpdateCareProviderServiceDetailId(CareProviderContractServiceId, careProviderServiceDetailId);
            var CareProviderContractServiceTitle = (dbHelper.careProviderContractService.GetCareProviderContractServiceByID(CareProviderContractServiceId, "title")["title"]).ToString();

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(CareProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup
            var todayDate = DateTime.Now;
            var invoicebyid = 2; //funder/service user
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(DateTime.Now.Year, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractSchemeId, _careProviderBatchGroupingId,
                commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                _cpExtractNameId1, false,
                _teamId);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 5, 14);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, provider1ID, careProviderContractSchemeId, provider2ID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractSchemeId, _careProviderServiceId, CareProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn = DateTime.Now.AddSeconds(2);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId, runOn, null, null);

            Thread.Sleep(5000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);

            #endregion

            #region Update invoice status to completed

            //update invoice status to completed
            var _cpFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId, 2);

            #endregion

            #region Step 27

            //login to the application, navigate to finance extract batches page, open finance extract batch record and click run on extract batch
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatch1Id);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch1IdNumber1.ToString())
                .ValidateBatchStatusSelectedText("New")
                .ClickRunExtractBatchButton()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();


            _cpFinanceExtractBatchId = dbHelper
                                .careProviderFinanceExtractBatch
                                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(2, _cpFinanceExtractBatchId.Count);

            var _extractedFinanceInvoiceId = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByFinanceInvoiceStatusIdAndFinanceExtractBatchId(3, _cpFinanceExtractBatch1Id)[0];
            string _extractedInvoiceIdNumber = (string)dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_extractedFinanceInvoiceId, "invoicenumber")["invoicenumber"];

            //wait for finance extract batch record page to load and validate batch status, net batch total, gross batch total, vat total, total debit, total credits, number of invoices, number of credit notes, number of debit notes, number of invoices extracted
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch1IdNumber1.ToString())
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateInvoiceFiles_FileLinkText("ACC-4845_2 " + currentTimeString + " " + _extractedInvoiceIdNumber + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ValidateExtractcontent_FileLinkText("CPEN_A1_" + currentTimeString + " " + _cpFinanceExtractBatch1IdNumber1 + ".csv")
                .ValidateNetbatchtotalText("10.00")
                .ValidateGrossbatchtotalText("12.00")
                .ValidateVattotalText("2.00")
                .ValidateTotaldebitsText("10.00")
                .ValidateTotalcreditsText("0.00")
                .ValidateNumberofinvoicesextractedText("1")
                .ValidateNumberofuniquepayersText("1");

            #endregion

            #region Step 28 and Step 29

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
                .WaitForCPFinanceExtractBatchSetupsPageToLoad()
                .OpenRecord(_cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchSetupRecordPage
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .InsertTextOnEndDate("11/10/2023")
                .ClickGenerateAndSendInvoicesAutomatically_NoOption()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceExtractBatchSetupRecordPageToLoad()
                .ValidateEndDateText("11/10/2023")
                .ClickBackButton();

            #region Process CP Finance Extract Batches - 1

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceExtractBatchesPage();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .InsertSearchQuery("CPEN_A1_" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            //click refresh button in finance extract batches page
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            _cpFinanceExtractBatchId = dbHelper
                    .careProviderFinanceExtractBatch
                    .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(3, _cpFinanceExtractBatchId.Count);

            cpFinanceExtractBatchesPage
                .OpenRecord(_cpFinanceExtractBatchId[0]);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn("04/10/2023")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("For this record to be saved successfully, the Run On date/time must be later than the current date/time. Please correct as necessary.")
                .TapCloseButton();

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            #region Process CP Finance Extract Batches - 2

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            //click refresh button in finance extract batches page
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            _cpFinanceExtractBatchId = dbHelper
                    .careProviderFinanceExtractBatch
                    .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(4, _cpFinanceExtractBatchId.Count);

            #region Process CP Finance Extract Batches - 3

            ProcessCPFinanceScheduledJob("Process CP Finance Extract Batches");

            #endregion

            //click refresh button in finance extract batches page
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceExtractBatchesPageToLoad();

            _cpFinanceExtractBatchId = dbHelper
                    .careProviderFinanceExtractBatch
                    .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(4, _cpFinanceExtractBatchId.Count);

            #endregion

            #region Step 30
            dbHelper.careProviderFinanceExtractBatchSetup.UpdateGenerateAndSendInvoicesAutomaticallyFlag(_cpFinanceExtractBatchSetupId1, false);

            //click new record button in finance extract batches page
            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickNewRecordButton();

            //create finance extract batch record
            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickFinanceExtractBatchSetupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("CPEN_A1_" + currentTimeString, _cpFinanceExtractBatchSetupId1);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .InsertTextOnRunOn(DateTime.Now.AddHours(1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time(DateTime.Now.ToString("HH:mm"))
                .ClickSaveButton()
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .WaitForRecordToBeSaved()
                .ValidateBatchStatusSelectedText("New")
                .ValidateFinanceExtractBatchSetupLinkText("CPEN_A1_" + currentTimeString + " \\ 19/09/2023")
                .ValidateExtractNameLinkText("CPEN_A1_" + currentTimeString)
                .ValidateInvoiceFiles_FileLinkText("")
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ClickBackButton();

            _cpFinanceExtractBatchId = dbHelper
                .careProviderFinanceExtractBatch
                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractName(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1);
            Assert.AreEqual(5, _cpFinanceExtractBatchId.Count);
            int _cpFinanceExtractBatch5IdNumber1 = (int)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_cpFinanceExtractBatchId[0], "batchid")["batchid"];

            #endregion

            #region Step 31

            #region Process CP Finance Invoice Batches - 2

            var _cpFinanceInvoiceBatchId2 = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId)[0];

            var runOn2 = DateTime.Now.AddMinutes(1);

            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId2, runOn2, null, null);

            Thread.Sleep(62000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId, 2);
            Assert.AreEqual(2, completedFinanceInvoiceBatchIds.Count);

            var _cpFinanceInvoiceId2 = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchIdAndInvoiceStatusId(_cpFinanceInvoiceBatchId2, 1)[0];
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId2, 20);
            dbHelper.careProviderFinanceInvoice.UpdateInvoiceStatus(_cpFinanceInvoiceId2, 2);

            #endregion

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatchId[0]);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ClickRunExtractBatchButton()
                .WaitForCPFinanceExtractBatchRecordPageToLoad();

            var _Completed_CpAdhocFinanceExtractBatchId = dbHelper
                .careProviderFinanceExtractBatch
                .GetCareProviderFinanceExtractBatchByFinanceExtractBatchSetupIdAndExtractNameAndIsAdhocBatch(_cpFinanceExtractBatchSetupId1, _cpExtractNameId1, true, 2);

            DateTime _CpFinanceExtractBatchId5_CompletedOn = (DateTime)dbHelper.careProviderFinanceExtractBatch.GetCareProviderFinanceExtractBatchById(_Completed_CpAdhocFinanceExtractBatchId[0], "completedon")["completedon"];

            var _extractedFinanceInvoiceId5 = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByFinanceInvoiceStatusIdAndFinanceExtractBatchId(3, _Completed_CpAdhocFinanceExtractBatchId[0])[0];
            string _extractedInvoiceIdNumber5 = (string)dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(_extractedFinanceInvoiceId5, "invoicenumber")["invoicenumber"];

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch5IdNumber1.ToString())
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateInvoiceFiles_FileLinkText("ACC-4845_2 " + currentTimeString + " " + _extractedInvoiceIdNumber5 + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ValidateNetbatchtotalText("10.00")
                .ValidateGrossbatchtotalText("12.00")
                .ValidateVattotalText("2.00")
                .ValidateTotaldebitsText("10.00")
                .ValidateTotalcreditsText("0.00")
                .ValidateNumberofinvoicesextractedText("1")
                .ValidateNumberofuniquepayersText("1")
                .ValidateNumberofinvoicescancelledText("0")
                .ValidateCompletedonText(_CpFinanceExtractBatchId5_CompletedOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateCompletedon_TimeText(_CpFinanceExtractBatchId5_CompletedOn.ToLocalTime().ToString("HH:mm"))
                .ClickBackButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .SelectFinanceInvoiceBatchSetupRecord(_cpFinanceExtractBatchId[0]);

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .ClickMailMergeButton();

            //wait for create invoice file popup and lookup mail merge template lookup button
            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickSelectedRecordsOnCurrentPageRadioButton()
                .ClickMailMergeTemplateLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Care Provider Finance Invoice Generic Template", _invoiceGenerateTemplate_MailMergeId);

            createInvoiceFilePopup
                .WaitForCreateInvoiceFilePopupToLoad()
                .ClickOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Invoice Files generated for 0 of 1 records.")
                .TapCloseButton();

            cpFinanceExtractBatchesPage
                .WaitForCPFinanceExtractBatchesPageToLoad()
                .OpenRecord(_cpFinanceExtractBatchId[0]);

            cpFinanceExtractBatchRecordPage
                .WaitForCPFinanceExtractBatchRecordPageToLoad()
                .ValidateBatchIDText(_cpFinanceExtractBatch5IdNumber1.ToString())
                .ValidateBatchStatusSelectedText("Completed")
                .ValidateInvoiceFiles_FileLinkText("ACC-4845_2 " + currentTimeString + " " + _extractedInvoiceIdNumber5 + " " + todayDate.ToString("dd'/'MM'/'yyyy") + ".pdf")
                .ValidateExtractcontent_FileLinkText("CPEN_A1_" + currentTimeString + " " + _cpFinanceExtractBatch5IdNumber1 + ".csv")
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ValidateNetbatchtotalText("10.00")
                .ValidateGrossbatchtotalText("12.00")
                .ValidateVattotalText("2.00")
                .ValidateTotaldebitsText("10.00")
                .ValidateTotalcreditsText("0.00")
                .ValidateNumberofinvoicesextractedText("1")
                .ValidateNumberofuniquepayersText("1")
                .ValidateNumberofinvoicescancelledText("0")
                .ValidateCompletedonText(_CpFinanceExtractBatchId5_CompletedOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateCompletedon_TimeText(_CpFinanceExtractBatchId5_CompletedOn.ToLocalTime().ToString("HH:mm"));

            #endregion

        }

        #endregion

    }
}

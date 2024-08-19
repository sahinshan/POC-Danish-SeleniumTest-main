using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.Settings.Configuration.CPFinanceAdmin
{
    /// <summary>
    /// This class contains Automated UI test scripts for CP Finance > Finance Invoice Batch
    /// </summary>
    [TestClass]
    public class CPFinance_FinanceInvoiceBatch_UITestCases : FunctionalTest
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

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

        #endregion

        #region Internal Methods

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId)
        {
            for (int i = 0; i < 7; i++)
            {
                var workScheduleDate = DateTime.Now.AddDays(i).Date;

                switch (workScheduleDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Monday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Tuesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Wednesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Thursday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Friday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Saturday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    default:
                        break;
                }
            }
        }

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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CP_FIB_BU");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CP_FIB_Team";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90400", "CareProvidersEB@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _loginUsername = "CP_FIBUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "CP_FIB", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-6803

        [TestProperty("JiraIssueID", "ACC-6913")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Steps 1 to 11")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 6; //Every Calendar Month (Date Specific)
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 2), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Figo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Step 1

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            //var _invoiceGenerateTemplate_MailMergeId = dbHelper.mailMerge.GetByName("Care Provider Finance Invoice Generic Template")[0];

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            cpFinanceAdminPage
                .WaitForCPFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingAreaButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordVisible(_careProviderFinanceInvoiceBatchSetupId);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateRecordIsPresent(_careProviderFinanceInvoiceBatchId[0].ToString(), true);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_careProviderFinanceInvoiceBatchId[0].ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchIDFieldIsDisplayed(true)
                .ValidateFinanceInvoiceBatchSetupLookupButtonIsDisplayed(true)
                .ValidateRunOnFieldIsDisplayed(true)
                .ValidateRunOnDatePickerIsDisplayed(true)
                .ValidateRunOn_TimeFieldIsDisplayed(true)
                .ValidateRunOn_TimePickerIsDisplayed(true)
                .ValidateBatchStatusFieldIsDisplayed(true)
                .ValidatePeriodStartDateFieldIsDisplayed(true)
                .ValidatePeriodEndDateFieldIsDisplayed(true)
                .ValidateWhenToBatchFinanceTransactionsPicklistFieldIsDisplayed(true);

            cpFinanceInvoiceBatchRecordPage
                .ValidateContractSchemeLookupButtonIsDisplayed(true)
                .ValidateBatchGroupingLookupButtonIsDisplayed(true)
                .ValidateIsAdHocOptionsAreDisplayed(true);

            cpFinanceInvoiceBatchRecordPage
                .ValidateNetBatchTotalFieldIsDisplayed(true)
                .ValidateGrossBatchTotalFieldIsDisplayed(true)
                .ValidateVatTotalFieldIsDisplayed(true)
                .ValidateNumberOfInvoicesCreatedFieldIsDisplayed(true)
                .ValidateNumberOfInvoicesCancelledFieldIsDisplayed(true)
                .ValidateUserFieldLookupButtonIsDisplayed(true);

            cpFinanceInvoiceBatchRecordPage
                .ValidateResponsibleTeamLookupButtonIsDisplayed(true);

            cpFinanceInvoiceBatchRecordPage
                .ValidatePeriodStartDateText("02/01/2023")
                .ValidatePeriodEndDateText("01/02/2023");

            cpFinanceInvoiceBatchRecordPage
                .ValidateBatchStatusText("New")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Default Care Provider Batch Grouping")
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .InsertTextOnRunOn("03/02/2023")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("For this record to be saved successfully, the Run On date/time must be later than the current date/time. Please correct as necessary.")
                .TapCloseButton();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLookupButtonDisabled(false)
                .ValidateResponsibleTeamLookupButtonIsDisabled(false)
                .ValidateBatchIDFieldDisabled(true)
                .ValidateRunOnFieldDisabled(true)
                .ValidateRunOnDatePickerDisabled(true)
                .ValidateRunOn_TimeFieldDisabled(true)
                .ValidateRunon_TimePickerFieldDisabled(true)
                .ValidateBatchStatusFieldDisabled(true)
                .ValidatePeriodStartDateFieldDisabled(false)
                .ValidatePeriodEndDateFieldDisabled(false)
                .ValidateWhenToBatchFinanceTransactionsPicklistFieldDisabled(true)
                .ValidateContractSchemeLookupButtonDisabled(true)
                .ValidateBatchGroupingLookupButtonDisabled(true)
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ValidatePersonFieldLookupButtonDisabled(true)
                .ValidateNetBatchTotalDisabled(true)
                .ValidateGrossBatchTotalDisabled(true)
                .ValidateVatTotalDisabled(true)
                .ValidateNumberOfInvoicesCreatedDisabled(true)
                .ValidateNumberOfInvoicesCancelledDisabled(true)
                .ValidateUserFieldLookupButtonDisabled(true)
                .ClickFinanceInvoiceBatchSetupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("*" + currentTimeString, _careProviderFinanceInvoiceBatchSetupId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLookupButtonDisabled(false)
                .ValidateResponsibleTeamLookupButtonIsDisabled(false)
                .ValidateBatchIDFieldDisabled(true)
                .ValidateBatchStatusFieldDisabled(true)
                .ValidateContractSchemeLookupButtonDisabled(true)
                .ValidateBatchGroupingLookupButtonDisabled(true)
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ValidateNetBatchTotalDisabled(true)
                .ValidateGrossBatchTotalDisabled(true)
                .ValidateVatTotalDisabled(true)
                .ValidateNumberOfInvoicesCreatedDisabled(true)
                .ValidateNumberOfInvoicesCancelledDisabled(true)
                .ValidateUserFieldLookupButtonDisabled(true)

                .ValidateRunOnFieldDisabled(false)
                .ValidateRunOnDatePickerDisabled(false)
                .ValidateRunOn_TimeFieldDisabled(false)
                .ValidateRunon_TimePickerFieldDisabled(false)
                .ValidatePeriodStartDateFieldDisabled(false)
                .ValidatePeriodEndDateFieldDisabled(false)
                .ValidateWhenToBatchFinanceTransactionsPicklistFieldDisabled(false)
                .ValidatePersonFieldLookupButtonDisabled(false);

            cpFinanceInvoiceBatchRecordPage
                .ValidateBatchStatusText("New")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Default Care Provider Batch Grouping")
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ValidateResponsibleTeamLinkText(_teamName);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .InsertTextOnRunOn("01/01/2023")
                .InsertTextOnPeriodStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPeriodEndDate(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("For this record to be saved successfully, the Run On date/time must be later than the current date/time. Please correct as necessary.")
                .TapCloseButton();

            string runOn_Time = DateTime.Now.AddHours(1).ToString("HH:mm");

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .InsertTextOnRunOn(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time(runOn_Time)
                .ClickPersonFieldLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("All Active People")
                .SearchAndSelectRecord("*" + currentTimeString, _personId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidatePersonFieldLinkText(firstName + " " + lastName)
                .InsertTextOnPeriodStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPeriodEndDate(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateRunOnText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn_Time)
                .ValidatePeriodStartDateText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidateBatchIDFieldDisabled(true)
                .ValidateFinanceInvoiceBatchSetupLookupButtonDisabled(true)
                .ValidateBatchStatusFieldDisabled(true)
                .ValidateContractSchemeLookupButtonDisabled(true)
                .ValidateBatchGroupingLookupButtonDisabled(true)
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ValidateNetBatchTotalDisabled(true)
                .ValidateGrossBatchTotalDisabled(true)
                .ValidateVatTotalDisabled(true)
                .ValidateNumberOfInvoicesCreatedDisabled(true)
                .ValidateNumberOfInvoicesCancelledDisabled(true)
                .ValidateUserFieldLookupButtonDisabled(true)

                .ValidateRunOnFieldDisabled(false)
                .ValidateRunOnDatePickerDisabled(false)
                .ValidateRunOn_TimeFieldDisabled(false)
                .ValidateRunon_TimePickerFieldDisabled(false)
                .ValidatePeriodStartDateFieldDisabled(false)
                .ValidatePeriodEndDateFieldDisabled(false)
                .ValidateWhenToBatchFinanceTransactionsPicklistFieldDisabled(false)
                .ValidatePersonFieldLookupButtonDisabled(false);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .InsertTextOnRunOn(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time("04:00")
                .InsertTextOnPeriodStartDate(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPeriodEndDate(todayDate.AddDays(13).ToString("dd'/'MM'/'yyyy"))
                .SelectWhenToBatchFinanceTransactionsPicklistValue("Not Confirmed")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId);
            Assert.AreEqual(2, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateRunOnText(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("04:00")
                .ValidatePeriodStartDateText(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(todayDate.AddDays(13).ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Not Confirmed");

            cpFinanceInvoiceBatchRecordPage
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(1500);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateRecordIsPresent(_careProviderFinanceInvoiceBatchId[0].ToString(), false);

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_careProviderFinanceInvoiceBatchId[0]);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateDeleteRecordButtonIsDisplayed(false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6866

        [TestProperty("JiraIssueID", "ACC-6914")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Steps 12 to 18")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod002()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);
            var _monthlyCareProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName("Monthly")[0];

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 6; //Every Calendar Month (Date Specific)
            var adhoccareproviderinvoicefrequencyid = 7; //Ad Hoc
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 2), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            var _adHocCareProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _monthlyCareProviderBatchGroupingId,
                new DateTime(2023, 1, 2), new TimeSpan(0, 0, 0),
                invoicebyid, adhoccareproviderinvoicefrequencyid, null, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            var _adhocCareProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_adHocCareProviderFinanceInvoiceBatchSetupId1);

            var _cpFibSetup1_BatchNumber1 = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(_careProviderFinanceInvoiceBatchId[0], "careproviderfinanceinvoicebatchnumber")["careproviderfinanceinvoicebatchnumber"];
            var _cpAdhocFibSetup1_BatchNumber1 = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(_adhocCareProviderFinanceInvoiceBatchId[0], "careproviderfinanceinvoicebatchnumber")["careproviderfinanceinvoicebatchnumber"];

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Figo";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Step 12 to Step 18

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);
            Assert.AreEqual(1, _adhocCareProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_careProviderFinanceInvoiceBatchId[0].ToString());

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateSaveButtonIsDisplayed(true)
                .ValidateSaveAndCloseButtonIsDisplayed(true)
                .ValidateAssignRecordButtonIsDisplayed(false)
                .ValidateRunInvoiceBatchButtonIsDisplayed(true)
                .ValidateDeleteRecordButtonIsDisplayed(false)
                .ValidateCopyRecordLinkButtonIsDisplayed(true);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)

                .ValidateBatchStatusText("New")
                .ValidateBatchIDText(_cpFibSetup1_BatchNumber1.ToString())
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Default Care Provider Batch Grouping")
                .ValidatePersonFieldLookupButtonIsDisplayed(false)

                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)

                .ValidateRunOnText("03/01/2023")
                .ValidateRunOn_TimeText("00:00")

                .InsertTextOnRunOn(todayDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time("01:00")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("For this record to be saved successfully, the Run On date/time must be later than the current date/time. Please correct as necessary.")
                .TapCloseButton();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidatePeriodStartDateText("02/01/2023")
                .ValidatePeriodEndDateText("01/02/2023")
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_adhocCareProviderFinanceInvoiceBatchId[0].ToString());

            string _adHocCpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_adHocCareProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateSaveButtonIsDisplayed(true)
                .ValidateSaveAndCloseButtonIsDisplayed(true)
                .ValidateAssignRecordButtonIsDisplayed(true)
                .ValidateRunInvoiceBatchButtonIsDisplayed(true)
                .ValidateDeleteRecordButtonIsDisplayed(true)
                .ValidateCopyRecordLinkButtonIsDisplayed(true);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateIsAdHoc_YesChecked()
                .ValidateIsAdHoc_NoNotChecked()
                .ValidateFinanceInvoiceBatchSetupLinkText(_adHocCpFinanceInvoiceBatchSetupName)

                .ValidateBatchStatusText("New")
                .ValidateBatchIDText(_cpAdhocFibSetup1_BatchNumber1.ToString())
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Monthly")
                .ValidatePersonFieldLookupButtonIsDisplayed(true)

                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)

                .ValidateRunOnText("")
                .ValidateRunOn_TimeText("")

                .InsertTextOnRunOn(todayDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time("02:00")
                .InsertTextOnPeriodStartDate(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPeriodEndDate(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("For this record to be saved successfully, the Run On date/time must be later than the current date/time. Please correct as necessary.")
                .TapCloseButton();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidatePeriodStartDateText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"));
            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6915")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Steps 19 to 25")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod003()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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
            var _monthlyCareProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName("Monthly")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 2; //Every 2 Weeks
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 2), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Step 19 to Step 25

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn = DateTime.Now.AddSeconds(2);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-7));
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            Thread.Sleep(2000);

            #endregion

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            int _fib1Status = (int)dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(_cpFinanceInvoiceBatchId1, "batchstatusid")["batchstatusid"];
            Assert.AreEqual(2, _fib1Status);

            var completedFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId1, 2);
            Assert.AreEqual(1, completedFinanceInvoiceBatchIds.Count);

            var _completedCpFinanceInvoiceBatchId1 = completedFinanceInvoiceBatchIds[0];
            Assert.AreEqual(_completedCpFinanceInvoiceBatchId1, _cpFinanceInvoiceBatchId1);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("10.00")
                .ValidateGrossbatchtotalText("12.00")
                .ValidateVattotalText("2.00")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13).ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-7).ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-5).ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("00:00")
                .ValidatePeriodStartDateText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-6).ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            //click run on button
            cpFinanceInvoiceBatchRecordPage
                .ClickRunInvoiceBatchButton();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("20.00")
                .ValidateGrossbatchtotalText("24.00")
                .ValidateVattotalText("4.00")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("CP_FIB User1")
                .ClickBackButton();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId3 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId3.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .InsertTextOnRunOn("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("For this record to be processed, the Run On date/time needs to be earlier than the current date/time. Please correct as necessary.")
                .TapCloseButton();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickNewRecordButton();

            //wait for Cp finance invoice batch record page to load, click on finance invoice batch setup lookup button, search and select the finance invoice batch setup record and click save button
            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickFinanceInvoiceBatchSetupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("*" + currentTimeString, _careProviderFinanceInvoiceBatchSetupId1);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .InsertTextOnPeriodStartDate("05/03/2024")
                .InsertTextOnPeriodEndDate("11/03/2024");

            int highestBatchNumber = dbHelper.careProviderFinanceInvoiceBatch.GetHighestBatchID();
            var RunOn2 = DateTime.Now.AddMinutes(1);

            cpFinanceInvoiceBatchRecordPage
                .InsertTextOnRunOn(RunOn2.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time(RunOn2.ToString("HH:mm"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickBackButton();

            Thread.Sleep(60000);

            var _careProviderFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupIDSortByBatchId(_careProviderFinanceInvoiceBatchSetupId1, "ascending");
            Assert.AreEqual(4, _careProviderFinanceInvoiceBatchIds.Count);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(_careProviderFinanceInvoiceBatchIds[0]);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("10.00")
                .ValidateGrossbatchtotalText("12.00")
                .ValidateVattotalText("2.00")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad();

            _careProviderFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupIDSortByBatchId(_careProviderFinanceInvoiceBatchSetupId1, "ascending");
            Assert.AreEqual(4, _careProviderFinanceInvoiceBatchIds.Count);

            var _cpFinanceTransactions = dbHelper.careProviderFinanceTransaction.GetByFinanceInvoiceBatchId(_careProviderFinanceInvoiceBatchIds[0]);
            Assert.AreEqual(1, _cpFinanceTransactions.Count);

            var _careProviderAdHocFinanceInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupIDAndIsAdHocBatch(_careProviderFinanceInvoiceBatchSetupId1, true);
            Assert.AreEqual(1, _careProviderAdHocFinanceInvoiceBatchIds.Count);
            Assert.AreEqual(_careProviderAdHocFinanceInvoiceBatchIds[0], _careProviderFinanceInvoiceBatchIds[3]);

            int _adHocManualCpFinanceInvoiceBatchId_Status = (int)dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(_careProviderFinanceInvoiceBatchIds[3], "batchstatusid")["batchstatusid"];
            Assert.AreEqual(2, _adHocManualCpFinanceInvoiceBatchId_Status); //Completed = 2

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(_careProviderFinanceInvoiceBatchIds[2]);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateBatchIDText(highestBatchNumber.ToString())
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Default Care Provider Batch Grouping")
                .ValidateIsAdHoc_NoChecked()
                .ValidateIsAdHoc_YesNotChecked()
                .ValidatePersonFieldLookupButtonIsDisplayed(false)
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidatePeriodStartDateText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(8).ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21).ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-6922")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 26")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod004()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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
            var _monthlyCareProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName("Monthly")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 2; //Every 2 Weeks
            var createbatchwithin = -10;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2019, 4, 1), new TimeSpan(2, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Step 26

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchButton();

            System.Threading.Thread.Sleep(1500);

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("0.00")
                .ValidateGrossbatchtotalText("0.00")
                .ValidateVattotalText("0.00")
                .ValidateNumberofInvoicesCreatedText("0")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("CP_FIB User1")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText("22/03/2019")
                .ValidateRunOn_TimeText("02:00")
                .ValidatePeriodStartDateText("01/04/2019")
                .ValidatePeriodEndDateText("14/04/2019")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText("05/04/2019")
                .ValidateRunOn_TimeText("02:00")
                .ValidatePeriodStartDateText("15/04/2019")
                .ValidatePeriodEndDateText("28/04/2019")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");


            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6953

        [TestProperty("JiraIssueID", "ACC-6968")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 27")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod005()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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
            var _monthlyCareProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName("Monthly")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 2; //Every 2 Weeks
            var adhoccareproviderinvoicefrequencyid = 7; //Ad Hoc
            var createbatchwithin = -10;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2019, 4, 1), new TimeSpan(2, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            var _adHocCareProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _monthlyCareProviderBatchGroupingId,
                new DateTime(2023, 1, 2), new TimeSpan(0, 0, 0),
                invoicebyid, adhoccareproviderinvoicefrequencyid, null, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _adHocCpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_adHocCareProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            var _adhocCareProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_adHocCareProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Step 27

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);
            Assert.AreEqual(1, _adhocCareProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];
            var _cpAdhocFinanceInvoiceBatchId1 = _adhocCareProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchButton();

            System.Threading.Thread.Sleep(1500);

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("0.00")
                .ValidateGrossbatchtotalText("0.00")
                .ValidateVattotalText("0.00")
                .ValidateNumberofInvoicesCreatedText("0")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("CP_FIB User1")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText("22/03/2019")
                .ValidateRunOn_TimeText("02:00")
                .ValidatePeriodStartDateText("01/04/2019")
                .ValidatePeriodEndDateText("14/04/2019")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText("05/04/2019")
                .ValidateRunOn_TimeText("02:00")
                .ValidatePeriodStartDateText("15/04/2019")
                .ValidatePeriodEndDateText("28/04/2019")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();


            //adhoc
            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpAdhocFinanceInvoiceBatchId1.ToString());


            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .InsertTextOnPeriodStartDate(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnPeriodEndDate(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(20).ToString("dd'/'MM'/'yyyy"));

            var runOn = DateTime.Now.AddSeconds(10);

            cpFinanceInvoiceBatchRecordPage
                .InsertTextOnRunOn(runOn.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnRunon_Time(runOn.ToString("HH:mm"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchButton();

            System.Threading.Thread.Sleep(1500);

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_adHocCareProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpAdhocFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_adHocCpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("0.00")
                .ValidateGrossbatchtotalText("0.00")
                .ValidateVattotalText("0.00")
                .ValidateNumberofInvoicesCreatedText("0")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("CP_FIB User1")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(20).ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpAdhocFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText("")
                .ValidateRunOn_TimeText("")
                .ValidatePeriodStartDateText("")
                .ValidatePeriodEndDateText("")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6992

        [TestProperty("JiraIssueID", "ACC-7012")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 28")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod006()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 4; //Set Day Every Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 1, 1);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn = DateTime.Now.AddSeconds(15);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn, null, null);

            System.Threading.Thread.Sleep(15000);

            var BatchRecord1PeriodStartDate = new DateTime(2024, 4, 15);
            var BatchRecord1PeriodEndDate = new DateTime(2024, 4, 28);

            var BatchRecord2PeriodStartDate = BatchRecord1PeriodEndDate.AddDays(1);
            var BatchRecord2PeriodEndDate = new DateTime(2024, 5, 26);

            var BatchRecord3PeriodStartDate = BatchRecord2PeriodEndDate.AddDays(1);
            var BatchRecord3PeriodEndDate = new DateTime(2024, 6, 30);

            #endregion

            #region Step 28

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchButton()
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("20.00")
                .ValidateGrossbatchtotalText("24.00")
                .ValidateVattotalText("4.00")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("CP_FIB User1")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(BatchRecord1PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord1PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];
            runOn = DateTime.Now.AddSeconds(30);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId2, runOn, null, null);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickRunInvoiceBatchButton()
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad();

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("40.00")
                .ValidateGrossbatchtotalText("48.00")
                .ValidateVattotalText("8.00")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("CP_FIB User1")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord2PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId3 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId3.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText("27/05/2024")
                .ValidateRunOn_TimeText("01:00")
                .ValidatePeriodStartDateText(BatchRecord3PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord3PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7013")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 29")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod007()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 1, 1);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn, null, null);
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var BatchRecord1PeriodStartDate = new DateTime(2024, 4, 15);
            var BatchRecord1PeriodEndDate = new DateTime(2024, 4, 30);

            var BatchRecord2PeriodStartDate = BatchRecord1PeriodEndDate.AddDays(1);
            var BatchRecord2PeriodEndDate = commonMethodsHelper.GetLastDayOfMonth(BatchRecord2PeriodStartDate);

            #endregion

            #region Step 29

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("22.86")
                .ValidateGrossbatchtotalText("27.43")
                .ValidateVattotalText("4.57")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(BatchRecord1PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord1PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("01:00")
                .ValidatePeriodStartDateText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord2PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7014")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 30")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod008()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 6; //Every Calendar Month (Date Specific)
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 1, 1);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn, null, null);
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var BatchRecord1PeriodStartDate = new DateTime(2024, 4, 15);
            var BatchRecord1PeriodEndDate = new DateTime(2024, 5, 14);

            var BatchRecord2PeriodStartDate = BatchRecord1PeriodEndDate.AddDays(1);
            var BatchRecord2PeriodEndDate = new DateTime(2024, 6, 14);

            #endregion

            #region Step 30

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("42.86")
                .ValidateGrossbatchtotalText("51.43")
                .ValidateVattotalText("8.57")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(BatchRecord1PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord1PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("01:00")
                .ValidatePeriodStartDateText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord2PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7015")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 31")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod009()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 8; //Every Quarter
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 4, 15);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn, null, null);
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var BatchRecord1PeriodStartDate = new DateTime(2024, 4, 15);
            var BatchRecord1PeriodEndDate = new DateTime(2024, 6, 30);

            var BatchRecord2PeriodStartDate = BatchRecord1PeriodEndDate.AddDays(1);
            var BatchRecord2PeriodEndDate = new DateTime(2024, 9, 30);

            #endregion

            #region Step 31

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("110.00")
                .ValidateGrossbatchtotalText("132.00")
                .ValidateVattotalText("22.00")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(BatchRecord1PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord1PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("01:00")
                .ValidatePeriodStartDateText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord2PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7016")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 31")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod010()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 9; //Every Quarter (Date Specific)
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 4, 15);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn, null, null);
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var BatchRecord1PeriodStartDate = new DateTime(2024, 4, 15);
            var BatchRecord1PeriodEndDate = new DateTime(2024, 7, 14);

            var BatchRecord2PeriodStartDate = BatchRecord1PeriodEndDate.AddDays(1);
            var BatchRecord2PeriodEndDate = new DateTime(2024, 10, 14);

            #endregion

            #region Step 31

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("130.00")
                .ValidateGrossbatchtotalText("156.00")
                .ValidateVattotalText("26.00")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(BatchRecord1PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord1PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("01:00")
                .ValidatePeriodStartDateText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord2PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7018")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 31")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod011()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 10; //Every Year
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 4, 15);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn, null, null);
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var BatchRecord1PeriodStartDate = new DateTime(2024, 4, 15);
            var BatchRecord1PeriodEndDate = new DateTime(2024, 12, 31);

            var BatchRecord2PeriodStartDate = BatchRecord1PeriodEndDate.AddDays(1);
            var BatchRecord2PeriodEndDate = new DateTime(2025, 12, 31);

            #endregion

            #region Step 31

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("372.86")
                .ValidateGrossbatchtotalText("447.43")
                .ValidateVattotalText("74.57")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(BatchRecord1PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord1PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("01:00")
                .ValidatePeriodStartDateText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord2PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7019")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 31")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod012()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 11; //Every Year (Date Specific)
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _cpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_careProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 1, 1);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn, null, null);
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            var BatchRecord1PeriodStartDate = new DateTime(2024, 4, 15);
            var BatchRecord1PeriodEndDate = new DateTime(2025, 4, 14);

            var BatchRecord2PeriodStartDate = BatchRecord1PeriodEndDate.AddDays(1);
            var BatchRecord2PeriodEndDate = new DateTime(2026, 4, 14);

            #endregion

            #region Step 31

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1.ToString());

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_cpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("521.43")
                .ValidateGrossbatchtotalText("625.72")
                .ValidateVattotalText("104.29")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn.ToString("HH:mm"))
                .ValidatePeriodStartDateText(BatchRecord1PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord1PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText("01:00")
                .ValidatePeriodStartDateText(BatchRecord2PeriodStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(BatchRecord2PeriodEndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter");

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6993

        [TestProperty("JiraIssueID", "ACC-7135")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 32 - 34")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod013()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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
            var _monthlyBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Monthly", new DateTime(2022, 8, 1), 12);
            var _standard4WeeklyBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Standard - 4 weekly", new DateTime(2022, 7, 27), 3);
            var _standardWeeklyBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Standard", new DateTime(2020, 1, 1), 4);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 11; //Every Year (Date Specific)
            var everyYear_careproviderinvoicefrequencyid = 10; //Every Year
            var everyQuarter_careproviderinvoicefrequencyid = 9; //Every Quarter
            var everyTwoWeeks_careproviderinvoicefrequencyid = 2; //Every Two Weeks
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            //create careproviderfinanceinvoicebatchsetup for monthly batch grouping, standard - 4 weekly batch grouping and for standard batch grouping
            var _careProviderFinanceInvoiceBatchSetupId2 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _monthlyBatchGroupingId,
                new DateTime(2024, 4, 15), new TimeSpan(1, 0, 0),
                invoicebyid, everyYear_careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            var _careProviderFinanceInvoiceBatchSetupId3 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _standard4WeeklyBatchGroupingId,
                new DateTime(2024, 4, 18), new TimeSpan(2, 0, 0),
                invoicebyid, everyQuarter_careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            var _careProviderFinanceInvoiceBatchSetupId4 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _standardWeeklyBatchGroupingId,
                new DateTime(2024, 4, 18), new TimeSpan(1, 0, 0),
                invoicebyid, everyTwoWeeks_careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            var _cpFibs2BatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId2);
            var _cpFibs3BatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId3);
            var _cpFibs4BatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId4);


            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddMinutes(1));
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");
            Thread.Sleep(1500);

            var runOn4 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddMinutes(1));
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFibs4BatchId[0], runOn4, null, null);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");
            Thread.Sleep(1500);

            var runOn2 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddMinutes(1));
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFibs2BatchId[0], runOn2, null, null);
            Thread.Sleep(1500);

            var runOn3 = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddMinutes(1));
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFibs3BatchId[0], runOn3, null, null);

            #endregion

            #region Step 32 - 34

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId1.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId2.ToString(), true);

            var _cpStandardFibsBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId4);
            var _cpStandardFinanceInvoiceBatchId1 = _cpStandardFibsBatchIds[0];

            var _completedStandardFibBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetFinanceInvoiceBatchBySetupIDAndStatusID(_careProviderFinanceInvoiceBatchSetupId4, 2);
            var _completedStandardFinanceInvoiceBatchId1 = _completedStandardFibBatchId[0];

            var _cpMonthlyFibsBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId2);
            var _cpStandard4WeeklyFibsBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId3);

            //cpFinanceInvoiceBatchesPage
            //    .ValidateRecordIsPresent(1, _cpStandardFinanceInvoiceBatchId1.ToString(), true) //New
            //    .ValidateRecordIsPresent(6, _completedStandardFinanceInvoiceBatchId1.ToString(), true); //Completed

            //cpFinanceInvoiceBatchesPage
            //    .ValidateRecordIsPresent(5, _cpFinanceInvoiceBatchId1.ToString(), true) //Completed
            //    .ValidateRecordIsPresent(4, _cpFinanceInvoiceBatchId2.ToString(), true); //New

            //cpFinanceInvoiceBatchesPage
            //    .ValidateRecordIsPresent(3, _cpMonthlyFibsBatchIds[0].ToString(), true) //New
            //    .ValidateRecordIsPresent(2, _cpStandard4WeeklyFibsBatchIds[0].ToString(), true); //New


            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ResultsPageValidateHeaderCellSortDescendingOrder(6) //Batch Status
                .ResultsPageValidateHeaderCellSortAscendingOrder(8); //Run On

            cpFinanceInvoiceBatchesPage
                .RecordsPageValidateHeaderCellText(2, "Contract Scheme")
                .RecordsPageValidateHeaderCellText(3, "Batch Grouping")
                .RecordsPageValidateHeaderCellText(4, "Person")
                .RecordsPageValidateHeaderCellText(5, "Batch ID")
                .RecordsPageValidateHeaderCellText(6, "Batch Status")
                .RecordsPageValidateHeaderCellText(7, "When to Batch Finance Transactions?")
                .RecordsPageValidateHeaderCellText(8, "Run On")
                .RecordsPageValidateHeaderCellText(9, "Period Start Date")
                .RecordsPageValidateHeaderCellText(10, "Period End Date")
                .RecordsPageValidateHeaderCellText(11, "Ad Hoc Batch?")
                .RecordsPageValidateHeaderCellText(12, "Net Batch Total")
                .RecordsPageValidateHeaderCellText(13, "Gross Batch Total")
                .RecordsPageValidateHeaderCellText(14, "VAT Total")
                .RecordsPageValidateHeaderCellText(15, "Number of Invoices Created")
                .RecordsPageValidateHeaderCellText(16, "Number Of Invoices Cancelled")
                .RecordsPageValidateHeaderCellText(17, "Finance Invoice Batch Setup");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoice Batches")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Contract Scheme")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Batch Grouping")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Person")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Batch ID")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Batch Status")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Run On")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Period Start Date")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Period End Date")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Ad Hoc Batch?")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Net Batch Total")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Gross Batch Total")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Number Of Invoices Cancelled")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Finance Invoice Batch")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Finance Invoice Batch Setup")
                .ValidateSelectFilterFieldOptionIsPresent("1", "User")
                .ValidateSelectFilterFieldOptionIsPresent("1", "When to Batch Finance Transactions?");

            advanceSearchPage
                .SelectFilter("1", "Contract Scheme")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_cpFinanceInvoiceBatchId1.ToString())
                .ValidateSearchResultRecordPresent(_cpFinanceInvoiceBatchId2.ToString())
                .ValidateSearchResultRecordPresent(_cpStandardFinanceInvoiceBatchId1.ToString())
                .ValidateSearchResultRecordPresent(_completedStandardFinanceInvoiceBatchId1.ToString())
                .ValidateSearchResultRecordPresent(_cpMonthlyFibsBatchIds[0].ToString())
                .ValidateSearchResultRecordPresent(_cpStandard4WeeklyFibsBatchIds[0].ToString());

            advanceSearchPage
                .ResultsPageValidateHeaderCellText(2, "Contract Scheme")
                .ResultsPageValidateHeaderCellText(3, "Batch Grouping")
                .ResultsPageValidateHeaderCellText(4, "Person")
                .ResultsPageValidateHeaderCellText(5, "Batch ID")
                .ResultsPageValidateHeaderCellText(6, "Batch Status")
                .ResultsPageValidateHeaderCellText(7, "When to Batch Finance Transactions?")
                .ResultsPageValidateHeaderCellText(8, "Run On")
                .ResultsPageValidateHeaderCellText(9, "Period Start Date")
                .ResultsPageValidateHeaderCellText(10, "Period End Date")
                .ResultsPageValidateHeaderCellText(11, "Ad Hoc Batch?")
                .ResultsPageValidateHeaderCellText(12, "Net Batch Total")
                .ResultsPageValidateHeaderCellText(13, "Gross Batch Total")
                .ResultsPageValidateHeaderCellText(14, "VAT Total")
                .ResultsPageValidateHeaderCellText(15, "Number of Invoices Created")
                .ResultsPageValidateHeaderCellText(16, "Number Of Invoices Cancelled")
                .ResultsPageValidateHeaderCellText(17, "Finance Invoice Batch Setup");

            advanceSearchPage
                .OpenRecord(_cpFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .InsertTextOnPeriodStartDate("15/04/2025")
                .InsertTextOnPeriodEndDate("14/04/2025")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Finance Invoice Batch start date must be before the end date.")
                .TapCloseButton();


            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-7136")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 35")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod014()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
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

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 4, 26);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Step 35

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            Assert.AreEqual(2, dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(_careProviderFinanceInvoiceBatchSetupId1).Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId1.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId2.ToString(), true);

            cpFinanceInvoiceBatchesPage
                .OpenRecord(_cpFinanceInvoiceBatchId1);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            var _financeTransactionIds = dbHelper.careProviderFinanceTransaction.GetByFinanceInvoiceBatchId(_cpFinanceInvoiceBatchId1);
            Assert.AreEqual(2, _financeTransactionIds.Count);

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderfinanceinvoicebatch", false)
                .ValidateRecordCellContent(_financeTransactionIds[1].ToString(), 9, "26/04/2024")
                .ValidateRecordCellContent(_financeTransactionIds[1].ToString(), 11, "26/04/2024")
                .ValidateRecordCellContent(_financeTransactionIds[1].ToString(), 13, "£1.43")
                .ValidateRecordCellContent(_financeTransactionIds[1].ToString(), 14, "£0.29")
                .ValidateRecordCellContent(_financeTransactionIds[1].ToString(), 15, "£1.72");


            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderfinanceinvoicebatch", false)
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 9, "27/04/2024")
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 11, "30/04/2024")
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 13, "£5.71")
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 14, "£1.14")
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 15, "£6.85");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7137")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 35")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod015()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var _standardWeeklyBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Standard", new DateTime(2020, 1, 1), 4);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _standardWeeklyBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 0;
            var chargetodayid = 3; //Wednesday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = false;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _standardWeeklyBatchGroupingId,
                new DateTime(2024, 4, 26), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 4, 30);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90            

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMinutes(1);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, null, null);
            Thread.Sleep(63000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Step 35

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            Assert.AreEqual(2, dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(_careProviderFinanceInvoiceBatchSetupId1).Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId1.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId2.ToString(), true);

            cpFinanceInvoiceBatchesPage
                .OpenRecord(_cpFinanceInvoiceBatchId1);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickFinanceTransactionsTab();

            var _financeTransactionIds = dbHelper.careProviderFinanceTransaction.GetByFinanceInvoiceBatchId(_cpFinanceInvoiceBatchId1);
            Assert.AreEqual(1, _financeTransactionIds.Count);

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderfinanceinvoicebatch", false)
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 9, "30/04/2024")
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 11, "01/05/2024")
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 13, "£2.86")
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 14, "£0.57")
                .ValidateRecordCellContent(_financeTransactionIds[0].ToString(), 15, "£3.43");
            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7138")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 36")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod016()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("6993BTC2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2Id, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6993", "99910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            var user1name = "FTTest" + currentTimeString;
            var user1FirstName = "FTTest";
            var user1LastName = "User1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, null, _bookingType2Id, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "6993RateUnit", new DateTime(2020, 1, 1), 8999999, true, false, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _bookingType2Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            var _cpTimebandSetId = commonMethodsDB.CreateTimebandSet("TBS6993", _teamId);
            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            commonMethodsDB.CreateTimeband(_teamId, _cpTimebandSetId, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var _cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.UpdateCPTimebandSet(_cpContractServiceRatePeriodId, _cpTimebandSetId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 1; //Sunday
            var whentobatchfinancetransactionsid = 1; //Confirmed
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var _careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate, null, true);

            #endregion

            #region Diary Booking

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2Id, providerId, todayDate, new TimeSpan(1, 0, 0), todayDate, new TimeSpan(4, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpDiaryBookingId1, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, cpDiaryBookingId1, _personId, _careProviderPersonContractId, careProviderContractServiceId);
            dbHelper.cPBookingDiary.UpdateConfirmed(cpDiaryBookingId1, true);

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2Id, providerId, todayDate, new TimeSpan(5, 0, 0), todayDate, new TimeSpan(6, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpDiaryBookingId2, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, cpDiaryBookingId2, _personId, _careProviderPersonContractId, careProviderContractServiceId);


            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, todayDate, todayDate.AddDays(2));
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Step 36

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            Assert.AreEqual(2, dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(_careProviderFinanceInvoiceBatchSetupId1).Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId1.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId2.ToString(), true);

            cpFinanceInvoiceBatchesPage
                .OpenRecord(_cpFinanceInvoiceBatchId1);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidatePeriodStartDateText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateBatchStatusText("Completed")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Confirmed")
                .ValidateNetbatchtotalText("30.00")
                .ValidateGrossbatchtotalText("36.00")
                .ValidateVattotalText("6.00")
                .ClickFinanceTransactionsTab();

            var _financeTransactionIds = dbHelper.careProviderFinanceTransaction.GetByFinanceInvoiceBatchId(_cpFinanceInvoiceBatchId1);
            Assert.AreEqual(1, _financeTransactionIds.Count);

            var _cpFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(_cpFinanceInvoiceBatchId1);
            Assert.AreEqual(1, _cpFinanceInvoices.Count);

            var _cpInvoice_FinanceTransactionIds = dbHelper.careProviderFinanceTransaction.GetFinanceTransactionByInvoiceID(_cpFinanceInvoices[0]);
            Assert.AreEqual(1, _cpInvoice_FinanceTransactionIds.Count);

            var _careProvider_financeTransactionIds = dbHelper.careProviderFinanceTransaction.GetByCareProviderContractService(careProviderContractServiceId);
            Assert.AreEqual(2, _careProvider_financeTransactionIds.Count);

            var _confirmedFtId = _careProvider_financeTransactionIds[1];
            var _unconfirmedFtId = _careProvider_financeTransactionIds[0];

            Assert.AreEqual(_cpInvoice_FinanceTransactionIds[0], _confirmedFtId);
            Assert.AreEqual(_financeTransactionIds[0], _confirmedFtId);

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderfinanceinvoicebatch", false)
                .ValidateRecordIsPresent(_confirmedFtId.ToString())
                .ValidateRecordIsPresent(_unconfirmedFtId.ToString(), false)
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 9, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 11, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 13, "£30.00")
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 14, "£6.00")
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 15, "£36.00");

            cpFinanceTransactionsPage
                .OpenRecord(_confirmedFtId.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateConfirmed_YesRadioButtonIsChecked()
                .ValidateConfirmed_NoRadioButtonIsNotChecked()
                .ValidateNetAmount("30.00")
                .ValidateVatAmount("6.00")
                .ValidateGrossAmount("36.00");

            dbHelper.careProviderFinanceInvoiceBatch.UpdateWhenToBatchFinanceTransactions(_cpFinanceInvoiceBatchId2, 2); // 2 = Not Confirmed

            runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId2, runOn1, todayDate, todayDate.AddDays(2));
            Thread.Sleep(4000);

            cpFinanceTransactionRecordPage
                .ClickBackButton();

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ClickRefreshButton()
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId2);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidatePeriodStartDateText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateBatchStatusText("Completed")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Not Confirmed")
                .ValidateNetbatchtotalText("10.00")
                .ValidateGrossbatchtotalText("12.00")
                .ValidateVattotalText("2.00")
                .ClickFinanceTransactionsTab();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderfinanceinvoicebatch", false)
                .ValidateRecordIsPresent(_confirmedFtId.ToString(), false)
                .ValidateRecordIsPresent(_unconfirmedFtId.ToString())
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 9, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 11, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 13, "£10.00")
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 14, "£2.00")
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 15, "£12.00");

            cpFinanceTransactionsPage
                .OpenRecord(_unconfirmedFtId.ToString());

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateConfirmed_NoRadioButtonIsChecked()
                .ValidateConfirmed_YesRadioButtonIsNotChecked()
                .ValidateNetAmount("10.00")
                .ValidateVatAmount("2.00")
                .ValidateGrossAmount("12.00");


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7139")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 36")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod017()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("6993BTC2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType2Id, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 6993", "99910", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User

            var user1name = "FTTest" + currentTimeString;
            var user1FirstName = "FTTest";
            var user1LastName = "User1 " + currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUser1Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUser1EmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUser1EmploymentContractId, _bookingType2Id);

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(systemUser1Id, _teamId, _systemUser1EmploymentContractId, _availabilityTypeId);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderServiceId1 = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

            #endregion

            #region Care Provider Service Mapping

            commonMethodsDB.CreateCareProviderServiceMapping(careProviderServiceId1, _teamId, null, _bookingType2Id, null, "");

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "6993RateUnit", new DateTime(2020, 1, 1), 8999999, true, false, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            var _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _bookingType2Id, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            var _cpTimebandSetId = commonMethodsDB.CreateTimebandSet("TBS6993", _teamId);
            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            commonMethodsDB.CreateTimeband(_teamId, _cpTimebandSetId, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var _cpContractServiceRatePeriodId = dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);
            dbHelper.careProviderContractServiceRatePeriod.UpdateCPTimebandSet(_cpContractServiceRatePeriodId, _cpTimebandSetId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 0;
            var chargetodayid = 1; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does not matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2025, 12, 31);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = commonMethodsHelper.GetThisWeekFirstMonday().AddDays(-13);

            var _careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate, null, true);

            #endregion

            #region Diary Booking

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2Id, providerId, todayDate, new TimeSpan(1, 0, 0), todayDate, new TimeSpan(4, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpDiaryBookingId1, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, cpDiaryBookingId1, _personId, _careProviderPersonContractId, careProviderContractServiceId);
            dbHelper.cPBookingDiary.UpdateConfirmed(cpDiaryBookingId1, true);

            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_teamId, _businessUnitId, "", _bookingType2Id, providerId, todayDate, new TimeSpan(5, 0, 0), todayDate, new TimeSpan(6, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_teamId, "", cpDiaryBookingId2, _systemUser1EmploymentContractId, systemUser1Id);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_teamId, cpDiaryBookingId2, _personId, _careProviderPersonContractId, careProviderContractServiceId);


            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, todayDate, todayDate.AddDays(2));
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Step 36

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad();

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            Assert.AreEqual(2, dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(_careProviderFinanceInvoiceBatchSetupId1).Count);

            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .OpenRecord(_cpFinanceInvoiceBatchId1);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidatePeriodStartDateText(todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidatePeriodEndDateText(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ValidateBatchStatusText("Completed")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ValidateNetbatchtotalText("40.00")
                .ValidateGrossbatchtotalText("48.00")
                .ValidateVattotalText("8.00")
                .ClickFinanceTransactionsTab();

            var _financeTransactionIds = dbHelper.careProviderFinanceTransaction.GetByFinanceInvoiceBatchId(_cpFinanceInvoiceBatchId1);
            Assert.AreEqual(2, _financeTransactionIds.Count);

            var _cpFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(_cpFinanceInvoiceBatchId1);
            Assert.AreEqual(1, _cpFinanceInvoices.Count);

            var _cpInvoice_FinanceTransactionIds = dbHelper.careProviderFinanceTransaction.GetFinanceTransactionByInvoiceID(_cpFinanceInvoices[0]);
            Assert.AreEqual(2, _cpInvoice_FinanceTransactionIds.Count);

            var _cpInvoice_confirmedFtId = dbHelper.careProviderFinanceTransaction.GetByInvoiceIDAndConfirmedValue(_cpFinanceInvoices[0], true);
            Assert.AreEqual(1, _cpInvoice_confirmedFtId.Count);

            var _cpInvoice_unconfirmedFtId = dbHelper.careProviderFinanceTransaction.GetByInvoiceIDAndConfirmedValue(_cpFinanceInvoices[0], false);
            Assert.AreEqual(1, _cpInvoice_unconfirmedFtId.Count);

            var _careProvider_financeTransactionIds = dbHelper.careProviderFinanceTransaction.GetByCareProviderContractService(careProviderContractServiceId);
            Assert.AreEqual(2, _careProvider_financeTransactionIds.Count);

            var _confirmedFtId = _cpInvoice_confirmedFtId[0];
            var _unconfirmedFtId = _cpInvoice_unconfirmedFtId[0];

            Assert.AreEqual(true, dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(_confirmedFtId, "confirmed")["confirmed"]);
            Assert.AreEqual(false, dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(_unconfirmedFtId, "confirmed")["confirmed"]);

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad("careproviderfinanceinvoicebatch", false)
                .ValidateRecordIsPresent(_confirmedFtId.ToString())
                .ValidateRecordIsPresent(_unconfirmedFtId.ToString())
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 9, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 11, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 13, "£30.00")
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 14, "£6.00")
                .ValidateRecordCellContent(_confirmedFtId.ToString(), 15, "£36.00");

            cpFinanceTransactionsPage
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 9, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 11, todayDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 13, "£10.00")
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 14, "£2.00")
                .ValidateRecordCellContent(_unconfirmedFtId.ToString(), 15, "£12.00");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6994

        [TestProperty("JiraIssueID", "ACC-7141")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 37 - 38")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod018()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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
            var _monthlyBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Monthly", new DateTime(2022, 8, 1), 12);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 2; //Every Two Weeks
            var createbatchwithin = 0;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 4, 3), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Audit Reasons

            var _auditReasonName = "Default Change Bulk Edit";
            var _auditReasonId = commonMethodsDB.CreateErrorManagementReason(_auditReasonName, new DateTime(2020, 1, 1), 3, _teamId, false);

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2023, 3, 1);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batches

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(3);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, new DateTime(2023, 4, 3), new DateTime(2023, 4, 16));
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            var _cpFinanceInvoiceBatchId2 = _careProviderFinanceInvoiceBatchId[0];

            var runOn2 = DateTime.Now.AddSeconds(3);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId2, runOn2, new DateTime(2023, 4, 17), new DateTime(2023, 4, 30));
            Thread.Sleep(5000);

            var runOn3 = DateTime.Now.AddSeconds(3);
            var _adhocCpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.CreateCareProviderFinanceInvoiceBatch(_teamId, _careProviderFinanceInvoiceBatchSetupId1,
                1, careProviderContractScheme1Id, true, _monthlyBatchGroupingId,
                runOn3, new DateTime(2023, 5, 1), new DateTime(2023, 5, 7), 3, null, null, null, null, null);

            #endregion

            #region Step 37 - 38

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            Thread.Sleep(20000);

            _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);
            var _cpFinanceInvoiceBatchId3 = _careProviderFinanceInvoiceBatchId[0];

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateSelectedSystemView("Active Records")
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateSelectedSystemView("Search Results")
                .ValidateSystemViewOptionPresent("Completed Batches")
                .ValidateSystemViewOptionPresent("New Batches")
                .ValidateSystemViewOptionPresent("New Batches where Ad Hoc = Yes")
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId1.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId2.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId3.ToString(), true)
                .ValidateRecordIsPresent(_adhocCpFinanceInvoiceBatchId.ToString(), true);

            cpFinanceInvoiceBatchesPage
                .SelectSystemView("Completed Batches")
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId1.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId2.ToString(), true)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId3.ToString(), false);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ResultsPageValidateHeaderCellSortDescendingOrder(9) //Period Start Date
                .ResultsPageValidateHeaderCellSortDescendingOrder(8); //Run On

            var _completedInvoiceBatchIds = dbHelper.careProviderFinanceInvoiceBatch.GetCompletedBatchedByFinanceInvoiceBatchSetupIDSortedByRunOn();

            cpFinanceInvoiceBatchesPage
                .ValidateRecordIsPresent(1, _completedInvoiceBatchIds[0].ToString(), true) //Completed
                .ValidateRecordIsPresent(2, _completedInvoiceBatchIds[1].ToString(), true); //Completed

            cpFinanceInvoiceBatchesPage
                .RecordsPageValidateHeaderCellText(2, "Contract Scheme")
                .RecordsPageValidateHeaderCellText(3, "Batch Grouping")
                .RecordsPageValidateHeaderCellText(4, "Person")
                .RecordsPageValidateHeaderCellText(5, "Batch ID")
                .RecordsPageValidateHeaderCellText(6, "Batch Status")
                .RecordsPageValidateHeaderCellText(7, "When to Batch Finance Transactions?")
                .RecordsPageValidateHeaderCellText(8, "Run On")
                .RecordsPageValidateHeaderCellText(9, "Period Start Date")
                .RecordsPageValidateHeaderCellText(10, "Period End Date")
                .RecordsPageValidateHeaderCellText(11, "Ad Hoc Batch?")
                .RecordsPageValidateHeaderCellText(12, "Net Batch Total")
                .RecordsPageValidateHeaderCellText(13, "Gross Batch Total")
                .RecordsPageValidateHeaderCellText(14, "VAT Total")
                .RecordsPageValidateHeaderCellText(15, "Number of Invoices Created")
                .RecordsPageValidateHeaderCellText(16, "Number Of Invoices Cancelled")
                .RecordsPageValidateHeaderCellText(17, "Finance Invoice Batch Setup");

            cpFinanceInvoiceBatchesPage
                .SelectSystemView("New Batches")
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId1.ToString(), false)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId2.ToString(), false)
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId3.ToString(), true);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ResultsPageValidateHeaderCellSortAscendingOrder(9) //Period Start Date
                .ResultsPageValidateHeaderCellSortAscendingOrder(8); //Run On

            cpFinanceInvoiceBatchesPage
                .ValidateRecordIsPresent(_cpFinanceInvoiceBatchId3.ToString(), true); //New                

            cpFinanceInvoiceBatchesPage
                .RecordsPageValidateHeaderCellText(2, "Contract Scheme")
                .RecordsPageValidateHeaderCellText(3, "Batch Grouping")
                .RecordsPageValidateHeaderCellText(4, "Person")
                .RecordsPageValidateHeaderCellText(5, "Batch ID")
                .RecordsPageValidateHeaderCellText(6, "Batch Status")
                .RecordsPageValidateHeaderCellText(7, "When to Batch Finance Transactions?")
                .RecordsPageValidateHeaderCellText(8, "Run On")
                .RecordsPageValidateHeaderCellText(9, "Period Start Date")
                .RecordsPageValidateHeaderCellText(10, "Period End Date")
                .RecordsPageValidateHeaderCellText(11, "Ad Hoc Batch?")
                .RecordsPageValidateHeaderCellText(12, "Net Batch Total")
                .RecordsPageValidateHeaderCellText(13, "Gross Batch Total")
                .RecordsPageValidateHeaderCellText(14, "VAT Total")
                .RecordsPageValidateHeaderCellText(15, "Number of Invoices Created")
                .RecordsPageValidateHeaderCellText(16, "Number Of Invoices Cancelled")
                .RecordsPageValidateHeaderCellText(17, "Finance Invoice Batch Setup");

            cpFinanceInvoiceBatchesPage
                .SelectSystemView("New Batches where Ad Hoc = Yes")
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateRecordIsPresent(_adhocCpFinanceInvoiceBatchId.ToString(), true);

            cpFinanceInvoiceBatchesPage
                .RecordsPageValidateHeaderCellText(2, "Contract Scheme")
                .RecordsPageValidateHeaderCellText(3, "Batch Grouping")
                .RecordsPageValidateHeaderCellText(4, "Person")
                .RecordsPageValidateHeaderCellText(5, "Batch ID")
                .RecordsPageValidateHeaderCellText(6, "Batch Status")
                .RecordsPageValidateHeaderCellText(7, "When to Batch Finance Transactions?")
                .RecordsPageValidateHeaderCellText(8, "Run On")
                .RecordsPageValidateHeaderCellText(9, "Period Start Date")
                .RecordsPageValidateHeaderCellText(10, "Period End Date")
                .RecordsPageValidateHeaderCellText(11, "Ad Hoc Batch?")
                .RecordsPageValidateHeaderCellText(12, "Net Batch Total")
                .RecordsPageValidateHeaderCellText(13, "Gross Batch Total")
                .RecordsPageValidateHeaderCellText(14, "VAT Total")
                .RecordsPageValidateHeaderCellText(15, "Number of Invoices Created")
                .RecordsPageValidateHeaderCellText(16, "Number Of Invoices Cancelled")
                .RecordsPageValidateHeaderCellText(17, "Finance Invoice Batch Setup");

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateRecordCellContent(_cpFinanceInvoiceBatchId3.ToString(), 9, "01/05/2023")
                .ValidateRecordCellContent(_cpFinanceInvoiceBatchId3.ToString(), 10, "14/05/2023")
                .ValidateRecordCellContent(_adhocCpFinanceInvoiceBatchId.ToString(), 9, "01/05/2023")
                .ValidateRecordCellContent(_adhocCpFinanceInvoiceBatchId.ToString(), 10, "07/05/2023")

                .SelectFinanceInvoiceBatchRecord(_cpFinanceInvoiceBatchId3)
                .SelectFinanceInvoiceBatchRecord(_adhocCpFinanceInvoiceBatchId)
                .ClickBulkEditButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2", true)
                .ClickUpdateCheckBox("periodstartdate")
                .InsertValueInInputField("periodstartdate", "02/05/2023")
                .ClickUpdateCheckBox("periodenddate")
                .InsertValueInInputField("periodenddate", "15/05/2023")
                .ClickAuditReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_auditReasonName).TapSearchButton().SelectResultElement(_auditReasonId);

            var updatedRunOn = DateTime.Now.AddSeconds(5);

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2", true)
                .ClickUpdateCheckBox("runon")
                .InsertValueInInputField("runon", updatedRunOn.ToString("dd'/'MM'/'yyyy"))
                .ClickTimePickerButton();

            calendarTimePicker
                .WaitForCalendarTimePickerPopupToLoad()
                .SelectHour(updatedRunOn.ToString("HH"))
                .SelectMinute(updatedRunOn.ToString("00"));

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2", true)
                .InsertValueInInputField("runon_Time", updatedRunOn.ToString("HH:mm"))
                .ClickUpdateButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateRecordCellContent(_cpFinanceInvoiceBatchId3.ToString(), 9, "02/05/2023")
                .ValidateRecordCellContent(_cpFinanceInvoiceBatchId3.ToString(), 10, "15/05/2023")
                .ValidateRecordCellContent(_adhocCpFinanceInvoiceBatchId.ToString(), 9, "02/05/2023")
                .ValidateRecordCellContent(_adhocCpFinanceInvoiceBatchId.ToString(), 10, "15/05/2023");

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .SelectFinanceInvoiceBatchRecord(_cpFinanceInvoiceBatchId1)
                .SelectFinanceInvoiceBatchRecord(_cpFinanceInvoiceBatchId2)
                .ClickBulkEditButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2", true)
                .ClickUpdateCheckBox("periodstartdate")
                .InsertValueInInputField("periodstartdate", "01/01/2023")
                .ClickAuditReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_auditReasonName).TapSearchButton().SelectResultElement(_auditReasonId);

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2", true)
                .ClickUpdateButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateRecordCellContent(_cpFinanceInvoiceBatchId1.ToString(), 18, "Operation only available for records with New status.")
                .ValidateRecordCellContent(_cpFinanceInvoiceBatchId2.ToString(), 18, "Operation only available for records with New status.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7142")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 39")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod019()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID, true);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 2; //funder/service
            var careproviderinvoicefrequencyid = 7; //Ad Hoc
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = false;
            var separateinvoices = false;

            var _sundryCareProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(true,
                careProviderContractScheme1Id, null,
                new DateTime(2023, 4, 3), new TimeSpan(1, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, null, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, null, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_sundryCareProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Step 39

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpFinanceInvoiceBatchId1);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateRunOnText("")
                .ValidateRunOn_TimeText("")
                .ValidatePeriodStartDateText("")
                .ValidatePeriodEndDateText("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7143")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Steps 40")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod020()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var _standardWeeklyBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Standard", new DateTime(2020, 1, 1), 4);
            var _monthlyBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Monthly", new DateTime(2022, 8, 1), 12);

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _standardWeeklyBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            //var adhoccareproviderinvoicefrequencyid = 7; //Ad Hoc
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _careProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _standardWeeklyBatchGroupingId,
                new DateTime(2023, 1, 2), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            #endregion

            #region Finance Invoice Batches

            var _careProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_careProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2023, 1, 2);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90            

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Step 40

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            var _cpFinanceInvoiceBatchId1 = _careProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpFinanceInvoiceBatchId1, runOn1, new DateTime(2023, 1, 2), new DateTime(2023, 1, 29));
            Thread.Sleep(4000);

            #region Create Ad Hoc FIB for FIBS where Invoice Frequency is not Ad Hoc
            var _adhocCpFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.CreateCareProviderFinanceInvoiceBatch(_teamId, _careProviderFinanceInvoiceBatchSetupId1,
                1, careProviderContractScheme1Id, true, _monthlyBatchGroupingId,
                runOn1, new DateTime(2023, 1, 30), new DateTime(2023, 1, 31), whentobatchfinancetransactionsid, null, null, null, null, null);

            var _adHocCpFibRecords = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupIDAndIsAdHocBatch(_careProviderFinanceInvoiceBatchSetupId1, true);
            Assert.AreEqual(1, _adHocCpFibRecords.Count);

            #endregion

            #region Process CP Finance Invoice Batches

            var runOn2 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_adhocCpFinanceInvoiceBatchId, runOn2, null, null);
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            Assert.AreEqual(1, _careProviderFinanceInvoiceBatchId.Count);

            //One Ad Hoc FIB record is present after processing the Ad Hoc FIB, it will not create a new FIB record.
            _adHocCpFibRecords = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupIDAndIsAdHocBatch(_careProviderFinanceInvoiceBatchSetupId1, true);
            Assert.AreEqual(1, _adHocCpFibRecords.Count);

            var _cpFibRecords = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(_careProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(3, _cpFibRecords.Count);

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateRecordIsPresent(_adHocCpFibRecords[0].ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7144")]
        [Description("Test case for Automated FIB Creation + Scheduled Job - Test automation for Step 42")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batches")]
        public void CPFinance_FinanceInvoiceBatch_UITestMethod021()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
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

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderServiceId1, null, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 7; //Ad Hoc
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = todayDate.AddYears(1);
            var separateinvoices = false;

            var _adHocCareProviderFinanceInvoiceBatchSetupId1 = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, _careProviderBatchGroupingId,
                new DateTime(2023, 1, 2), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, null, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractNameId, true,
                _teamId);

            string _adHocCpFinanceInvoiceBatchSetupName = (string)dbHelper.careProviderFinanceInvoiceBatchSetup.GetByID(_adHocCareProviderFinanceInvoiceBatchSetupId1, "name")["name"];

            #endregion

            #region Finance Invoice Batches

            var _adhocCareProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_adHocCareProviderFinanceInvoiceBatchSetupId1);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Luiz";
            var lastName = currentTimeString;
            var _personId = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Care Provider Person Contract
            var StartDate = new DateTime(2024, 1, 1);

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, StartDate);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContractId, _teamId,
                careProviderContractScheme1Id, careProviderServiceId1, careProviderContractServiceId,
                StartDate, 1, 1, _careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed = 90

            #endregion

            #region Process CP Finance Transaction Triggers

            ProcessCPFinanceScheduledJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Process CP Finance Invoice Batch

            var _cpAdhocFinanceInvoiceBatchId1 = _adhocCareProviderFinanceInvoiceBatchId[0];

            var runOn1 = DateTime.Now.AddSeconds(5);
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDates(_cpAdhocFinanceInvoiceBatchId1, runOn1, new DateTime(2024, 1, 1), new DateTime(2024, 1, 28));
            Thread.Sleep(4000);

            ProcessCPFinanceScheduledJob("Process CP Finance Invoice Batches");

            #endregion

            #region Step 42

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceInvoiceBatchesPage();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .ValidateToolbarOptionsAreVisible()
                .InsertSearchQuery("*" + currentTimeString)
                .ClickSearchButton();

            _adhocCareProviderFinanceInvoiceBatchId = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(_adHocCareProviderFinanceInvoiceBatchSetupId1);
            Assert.AreEqual(1, _adhocCareProviderFinanceInvoiceBatchId.Count);
            Assert.AreEqual(2, dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupIDAndIsAdHocBatch(_adHocCareProviderFinanceInvoiceBatchSetupId1, true).Count);

            var _cpAdhocFinanceInvoiceBatchId2 = _adhocCareProviderFinanceInvoiceBatchId[0];

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpAdhocFinanceInvoiceBatchId1.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateFinanceInvoiceBatchSetupLinkText(_adHocCpFinanceInvoiceBatchSetupName)
                .ValidateBatchStatusText("Completed")
                .ValidateNetbatchtotalText("40.00")
                .ValidateGrossbatchtotalText("48.00")
                .ValidateVattotalText("8.00")
                .ValidateNumberofInvoicesCreatedText("1")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText(runOn1.ToString("dd'/'MM'/'yyyy"))
                .ValidateRunOn_TimeText(runOn1.ToString("HH:mm"))
                .ValidatePeriodStartDateText("01/01/2024")
                .ValidatePeriodEndDateText("28/01/2024")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            cpFinanceInvoiceBatchesPage
                .WaitForCPFinanceInvoiceBatchesPageToLoad()
                .OpenRecord(_cpAdhocFinanceInvoiceBatchId2.ToString());

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()
                .ValidateBatchStatusText("New")
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateNumberOfInvoicesCancelledText("")
                .ValidateUserFieldLinkText("")
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateRunOnText("")
                .ValidateRunOn_TimeText("")
                .ValidatePeriodStartDateText("")
                .ValidatePeriodEndDateText("")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ClickBackButton();

            #endregion


        }

        #endregion

    }
}

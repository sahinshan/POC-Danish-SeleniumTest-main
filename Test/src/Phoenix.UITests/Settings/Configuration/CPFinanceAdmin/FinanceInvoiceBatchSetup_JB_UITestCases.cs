using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Type
    /// </summary>
    [TestClass]
    public class FinanceInvoiceBatchSetup_JB_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private string _teamName;
        private Guid _teamId;
        private Guid _defaultLoginUserID;
        private string _defaultLoginUserName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Care Providers Finance BU 1");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "Care Providers Finance Team 1";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90400", "CareProvidersFinanceTeam1@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _defaultLoginUserName = "CareProvidersFinanceUser_User01";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_defaultLoginUserName, "Care Providers Finance", "User_01", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        private void ExecuteScheduleJob(string ScheduleJobName)
        {
            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName(ScheduleJobName).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-6721

        [TestProperty("JiraIssueID", "ACC-7090")]
        [Description("Step(s) 1 to 4 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases001()
        {

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()

                .ValidateBatchingTypeSectionFieldsVisible()
                .ValidateBatchGroupingSectionFieldsVisible()
                .ValidateDatesSectionFieldsVisible()
                .ValidateBatchingCriteriaSectionFieldsVisible()
                .ValidateFinanceInvoiceTextSectionFieldsVisible()
                .ValidateOtherSettingsSectionFieldsVisible()
                .ValidateGeneralSectionFieldsVisible()

                .ValidateEndDateNotEditable()

                .ValidateCreateBatchWithinVisibility(true)
                .SelectInvoiceFrequency("Ad Hoc")
                .ValidateCreateBatchWithinVisibility(false)
                .SelectInvoiceFrequency("Every Week")
                .ValidateCreateBatchWithinVisibility(true);

            #endregion

            #region Step 2

            cpFinanceInvoiceBatchSetupRecordPage
                .ValidateIsThisASundryBatch_NoRadioButtonChecked()
                .ValidateStartTimeText("01:00")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Confirmed")
                .ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButtonChecked()
                .ValidateSeparateInvoices_NoRadioButtonChecked()
                .ValidateDebtorReferenceNumberRequired_YesRadioButtonChecked()
                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

            #region Step 3

            cpFinanceInvoiceBatchSetupRecordPage
                .ValidateInvoiceText_Text("Charge for services at {Establishment} up to {Charges Up To}")
                .ValidateTransactionTextStandardText("Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextContraText("Cancellation of Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextEndReasonText("Additional Charge for ending Service on {End Date}")
                .ValidateTransactionTextAdditionalText("Additional Charge")
                .ValidateTransactionTextNetIncomeText("Deduction of Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedPersonText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedFunderText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedProviderText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextAdjustmentText("Adjusting Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateReductionTransactionText_Text("Reduction to full weekly charge of {Full Net Amount} being paid by other Third Parties")
                .ValidateTransactionTextExpenseText("Ad Hoc Expense of {Expense} for {Person} on {Start Date}");

            #endregion

            #region Step 4

            cpFinanceInvoiceBatchSetupRecordPage
                .ClickIsThisASundryBatch_YesRadioButton()

                .ValidateBatchGroupingSectionFieldsVisible(true, false)

                .ValidateDatesSectionFieldsVisible()
                .ValidateStartDateText("")
                .ValidateEndDateText("")
                .ValidateEndDateNotEditable()
                .ValidateStartTimeText("01:00")

                .ValidateBatchingCriteriaSectionFieldsVisible(false, false)
                .ValidateInvoiceByNotEditable()
                .ValidateInvoiceBySelectedText("Funder / Service User")
                .ValidateInvoiceFrequencyNotEditable()
                .ValidateInvoiceFrequencySelectedText("Ad Hoc")
                .ValidateWhenToBatchFinanceTransactionsNotEditable()
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ValidateChargeToDaySelectedText("")
                .ValidateUseTransactionEndDateWhenBatchingFinanceTransactionsNotEditable()
                .ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_NoRadioButtonChecked()
                .ValidateSeparateInvoices_NoRadioButtonChecked()

                .ValidateFinanceInvoiceTextSectionFieldsVisible(false, false, true, false, false, false, false, false, false, false, true, false, true)
                .ValidateTransactionTextContraText("Cancellation of Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextSundryText("Sundry Expense of {Expense} for {Person} on {Start Date}")
                .ValidateReductionTransactionText_Text("Reduction to full weekly charge of {Full Net Amount} being paid by other Third Parties")

                .ValidateOtherSettingsSectionFieldsVisible()

                .ValidateGeneralSectionFieldsVisible();



            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6722

        [TestProperty("JiraIssueID", "ACC-7091")]
        [Description("Step(s) 5 to 11 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases002()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = contractScheme1Code + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, providerId, providerId);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtractName = "CPEN " + currentTimeString;
            var careProviderExtractNameCode = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion


            #region Step 5

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderBatchGroupingName, careProviderBatchGroupingId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickExtractNameLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderExtractName, careProviderExtractNameId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(_teamName, _teamId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 6

            cpFinanceInvoiceBatchSetupRecordPage
                .ValidateUseTransactionEndDateWhenBatchingFinanceTransactionsTooltip("Determines if:  = Yes, the End Date (regardless of Start date) of the Finance Transaction determines which charging week the Finance Transaction is associated to. Would be used for Per Week Services.   = No, the Start Date (regardless of End date) of the Finance Transaction determines which charging week the Finance Transaction is associated to. Could be used for scheduled care if the single visit straddles charging weeks.");

            #endregion

            #region Step 7

            cpFinanceInvoiceBatchSetupRecordPage
                .SelectInvoiceBy("Funder")
                .SelectInvoiceBy("Funder / Service User");

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme2Name, careProviderContractScheme2Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ValidateInvoiceByOptionDisabled("Funder") //Funder option is NOT available where for the Contract Scheme selected, for the value in the Funder field, its Provider Type has on Organisation Type, Establishment? = Yes
                .SelectInvoiceBy("Funder / Service User");

            #endregion

            #region Step 8

            cpFinanceInvoiceBatchSetupRecordPage
                .SelectInvoiceFrequency("Every Week")
                .SelectInvoiceFrequency("Every Week (by Month)")
                .SelectInvoiceFrequency("Every 2 Weeks")
                .SelectInvoiceFrequency("Every 2 Weeks (by Month)")
                .SelectInvoiceFrequency("Every 4 Weeks")
                .SelectInvoiceFrequency("Every 4 Weeks (by Month)")
                .SelectInvoiceFrequency("Set Day Every Month")
                .SelectInvoiceFrequency("Every Calendar Month")
                .SelectInvoiceFrequency("Every Calendar Month (Date Specific)")
                .SelectInvoiceFrequency("Ad Hoc")
                .SelectInvoiceFrequency("Every Quarter")
                .SelectInvoiceFrequency("Every Quarter (Date Specific)")
                .SelectInvoiceFrequency("Every Year")
                .SelectInvoiceFrequency("Every Year (Date Specific)");

            #endregion

            #region Step 9

            cpFinanceInvoiceBatchSetupRecordPage
                .SelectChargeToDay("Monday")
                .SelectChargeToDay("Tuesday")
                .SelectChargeToDay("Wednesday")
                .SelectChargeToDay("Thursday")
                .SelectChargeToDay("Friday")
                .SelectChargeToDay("Saturday")
                .SelectChargeToDay("Sunday");

            cpFinanceInvoiceBatchSetupRecordPage
                .InsertTextOnStartDate("25/02/2022")
                .SelectInvoiceFrequency("Every 2 Weeks")
                .ValidateChargeToDaySelectedText("Thursday");

            cpFinanceInvoiceBatchSetupRecordPage
                .SelectInvoiceFrequency("Every Calendar Month")
                .ValidateChargeToDaySelectedText("");

            #endregion

            #region Step 10

            cpFinanceInvoiceBatchSetupRecordPage
                .SelectWhenToBatchFinanceTransactions("Confirmed")
                .SelectWhenToBatchFinanceTransactions("Not Confirmed")
                .SelectWhenToBatchFinanceTransactions("Does Not Matter");

            #endregion

            #region Step 11

            cpFinanceInvoiceBatchSetupRecordPage
                .InsertTextOnCreateBatchWithin("0")
                .SelectChargeToDay("Monday")
                .InsertTextOnFinanceTransactionsUpTo("29/01/2054")
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme2Name)
                .ClickSearchButton();

            var cpFinanceInvoiceBatchSetupId = dbHelper.careProviderFinanceInvoiceBatchSetup.GetByContractScheme(careProviderContractScheme2Id).FirstOrDefault();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .OpenRecord(cpFinanceInvoiceBatchSetupId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ValidateEndDateNotEditable();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6998

        [TestProperty("JiraIssueID", "ACC-7092")]
        [Description("Step(s) 12 to 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases003()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN A " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            var careProviderExtract2Name = "CPEN B " + currentTimeString;
            var careProviderExtractName2Code = careProviderExtractName1Code + 1;
            var careProviderExtractName2Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract2Name, careProviderExtractName2Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion


            #region Step 12

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderBatchGroupingName, careProviderBatchGroupingId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .InsertTextOnStartTime("01:30")
                .SelectInvoiceBy("Funder")
                .SelectInvoiceFrequency("Every Week")
                .InsertTextOnCreateBatchWithin("0")
                .SelectChargeToDay("Sunday")
                .SelectWhenToBatchFinanceTransactions("Confirmed")
                .ClickUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButton()
                .InsertTextOnFinanceTransactionsUpTo("03/03/2024")
                .ClickSeparateInvoices_NoRadioButton()
                .ClickExtractNameLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderExtract1Name, careProviderExtractName1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var fibsRecords = dbHelper.careProviderFinanceInvoiceBatchSetup.GetByContractScheme(careProviderContractScheme1Id);
            Assert.AreEqual(1, fibsRecords.Count);
            var financeInvoiceBatchSetupId = fibsRecords.First();

            cpFinanceInvoiceBatchSetupsPage
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(financeInvoiceBatchSetupId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ValidateIsThisASundryBatch_NoRadioButtonChecked()

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText(careProviderBatchGroupingName)

                .ValidateStartDateText("01/01/2024")
                .ValidateStartTimeText("01:30")
                .ValidateEndDateText("")

                .ValidateInvoiceBySelectedText("Funder")
                .ValidateInvoiceFrequencySelectedText("Every Week")
                .ValidateCreateBatchWithinText("0")
                .ValidateChargeToDaySelectedText("Sunday")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Confirmed")
                .ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButtonChecked()
                .ValidateFinanceTransactionsUpToText("03/03/2024")
                .ValidateSeparateInvoices_NoRadioButtonChecked()

                .ValidateInvoiceText_Text("Charge for services at {Establishment} up to {Charges Up To}")
                .ValidateTransactionTextStandardText("Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextContraText("Cancellation of Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextEndReasonText("Additional Charge for ending Service on {End Date}")
                .ValidateTransactionTextAdditionalText("Additional Charge")
                .ValidateTransactionTextNetIncomeText("Deduction of Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedPersonText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedFunderText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedProviderText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextAdjustmentText("Adjusting Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateReductionTransactionText_Text("Reduction to full weekly charge of {Full Net Amount} being paid by other Third Parties")
                .ValidateTransactionTextExpenseText("Ad Hoc Expense of {Expense} for {Person} on {Start Date}")

                .ValidateExtractNameLinkText(careProviderExtract1Name)
                .ValidateDebtorReferenceNumberRequired_YesRadioButtonChecked()

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

            #region Step 13

            cpFinanceInvoiceBatchSetupRecordPage
                .SelectWhenToBatchFinanceTransactions("Does Not Matter")
                .ClickUseTransactionEndDateWhenBatchingFinanceTransactions_NoRadioButton()
                .InsertTextOnFinanceTransactionsUpTo("10/03/2024")
                .ClickSeparateInvoices_YesRadioButton()

                .InsertTextOnInvoiceText("Charge for services at {Establishment} up to {Charges Up To} - U")
                .InsertTextOnTransactionTextStandard("Charge for {Person} for the period {Start Date} to {End Date} - U")
                .InsertTextOnTransactionTextContra("Cancellation of Charge for {Person} for the period {Start Date} to {End Date} - U")
                .InsertTextOnTransactionTextEndReason("Additional Charge for ending Service on {End Date} - U")
                .InsertTextOnTransactionTextAdditional("Additional Charge - U")
                .InsertTextOnTransactionTextNetIncome("Deduction of Charge for {Person} for the period {Start Date} to {End Date} - U")
                .InsertTextOnTransactionTextApportionedPerson("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date} - U")
                .InsertTextOnTransactionTextApportionedFunder("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date} - U")
                .InsertTextOnTransactionTextApportionedProvider("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date} - U")
                .InsertTextOnTransactionTextAdjustment("Adjusting Charge for {Person} for the period {Start Date} to {End Date} - U")
                .InsertTextOnReductionTransactionText("Reduction to full weekly charge of {Full Net Amount} being paid by other Third Parties - U")
                .InsertTextOnTransactionTextExpense("Ad Hoc Expense of {Expense} for {Person} on {Start Date} - U")

                .ClickDebtorReferenceNumberRequired_NoRadioButton()
                .ClickExtractNameLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderExtract2Name, careProviderExtractName2Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(financeInvoiceBatchSetupId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()

                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")
                .ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_NoRadioButtonChecked()
                .ValidateFinanceTransactionsUpToText("10/03/2024")
                .ValidateSeparateInvoices_YesRadioButtonChecked()

                .ValidateInvoiceText_Text("Charge for services at {Establishment} up to {Charges Up To} - U")
                .ValidateTransactionTextStandardText("Charge for {Person} for the period {Start Date} to {End Date} - U")
                .ValidateTransactionTextContraText("Cancellation of Charge for {Person} for the period {Start Date} to {End Date} - U")
                .ValidateTransactionTextEndReasonText("Additional Charge for ending Service on {End Date} - U")
                .ValidateTransactionTextAdditionalText("Additional Charge - U")
                .ValidateTransactionTextNetIncomeText("Deduction of Charge for {Person} for the period {Start Date} to {End Date} - U")
                .ValidateTransactionTextApportionedPersonText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date} - U")
                .ValidateTransactionTextApportionedFunderText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date} - U")
                .ValidateTransactionTextApportionedProviderText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date} - U")
                .ValidateTransactionTextAdjustmentText("Adjusting Charge for {Person} for the period {Start Date} to {End Date} - U")
                .ValidateReductionTransactionText_Text("Reduction to full weekly charge of {Full Net Amount} being paid by other Third Parties - U")
                .ValidateTransactionTextExpenseText("Ad Hoc Expense of {Expense} for {Person} on {Start Date} - U")

                .ValidateExtractNameLinkText(careProviderExtract2Name)
                .ValidateDebtorReferenceNumberRequired_NoRadioButtonChecked();


            #endregion

            #region Step 14

            cpFinanceInvoiceBatchSetupRecordPage
                .ClickBackButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderBatchGroupingName, careProviderBatchGroupingId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .InsertTextOnStartTime("01:30")
                .SelectInvoiceBy("Funder")
                .SelectInvoiceFrequency("Every Week")
                .InsertTextOnCreateBatchWithin("0")
                .SelectChargeToDay("Sunday")
                .SelectWhenToBatchFinanceTransactions("Confirmed")
                .ClickUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButton()
                .InsertTextOnFinanceTransactionsUpTo("03/03/2024")
                .ClickSeparateInvoices_NoRadioButton()
                .ClickExtractNameLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderExtract1Name, careProviderExtractName1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("There is already a Finance Invoice Batch Setup record using these Batch Grouping selections for those dates. Please correct as necessary.").TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7093")]
        [Description("Step(s) 15 to 18 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases004()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.UpdateEndDate(fibsId, new DateTime(2024, 1, 31));

            #endregion

            #region Step 15

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderBatchGroupingName, careProviderBatchGroupingId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/02/2024")
                .InsertTextOnStartTime("01:30")
                .SelectInvoiceBy("Funder")
                .SelectInvoiceFrequency("Every Week")
                .InsertTextOnCreateBatchWithin("0")
                .SelectChargeToDay("Sunday")
                .SelectWhenToBatchFinanceTransactions("Confirmed")
                .ClickUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButton()
                .InsertTextOnFinanceTransactionsUpTo("03/03/2024")
                .ClickSeparateInvoices_NoRadioButton()
                .ClickExtractNameLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderExtract1Name, careProviderExtractName1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var fibsRecords = dbHelper.careProviderFinanceInvoiceBatchSetup.GetByContractScheme(careProviderContractScheme1Id);
            Assert.AreEqual(2, fibsRecords.Count);
            var financeInvoiceBatchSetupId = fibsRecords.Where(c => c != fibsId).First();

            cpFinanceInvoiceBatchSetupsPage
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(financeInvoiceBatchSetupId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ValidateIsThisASundryBatch_NoRadioButtonChecked()

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText(careProviderBatchGroupingName)

                .ValidateStartDateText("01/02/2024")
                .ValidateStartTimeText("01:30")
                .ValidateEndDateText("")

                .ValidateInvoiceBySelectedText("Funder")
                .ValidateInvoiceFrequencySelectedText("Every Week")
                .ValidateCreateBatchWithinText("0")
                .ValidateChargeToDaySelectedText("Sunday")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Confirmed")
                .ValidateUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButtonChecked()
                .ValidateFinanceTransactionsUpToText("03/03/2024")
                .ValidateSeparateInvoices_NoRadioButtonChecked()

                .ValidateInvoiceText_Text("Charge for services at {Establishment} up to {Charges Up To}")
                .ValidateTransactionTextStandardText("Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextContraText("Cancellation of Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextEndReasonText("Additional Charge for ending Service on {End Date}")
                .ValidateTransactionTextAdditionalText("Additional Charge")
                .ValidateTransactionTextNetIncomeText("Deduction of Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedPersonText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedFunderText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextApportionedProviderText("Charge of {Net Amount} from {Full Net Amount} for {Person} for the period {Start Date} to {End Date}")
                .ValidateTransactionTextAdjustmentText("Adjusting Charge for {Person} for the period {Start Date} to {End Date}")
                .ValidateReductionTransactionText_Text("Reduction to full weekly charge of {Full Net Amount} being paid by other Third Parties")
                .ValidateTransactionTextExpenseText("Ad Hoc Expense of {Expense} for {Person} on {Start Date}")

                .ValidateExtractNameLinkText(careProviderExtract1Name)
                .ValidateDebtorReferenceNumberRequired_YesRadioButtonChecked()

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

            #region Step 16

            //Any step related with ending FIBS will be ignored (as stated by Anitha)
            //As Ending FIBS is going to have multiple validations, it has been moved to a new test case separated from this one. We can ignore steps related to Ending FIBS in this test case and can automate once new test case is updated and reviewed.

            #endregion

            #region Step 17

            cpFinanceInvoiceBatchSetupRecordPage
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton();

            fibsRecords = dbHelper.careProviderFinanceInvoiceBatchSetup.GetByContractScheme(careProviderContractScheme1Id);
            Assert.AreEqual(1, fibsRecords.Count);

            #endregion

            #region Step 18

            var financeInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatches.Count);
            var fibId = financeInvoiceBatches.FirstOrDefault();
            dbHelper.careProviderFinanceInvoiceBatch.UpdateFinanceInvoiceBatchDatesAndStatus(2, fibId, new DateTime(2024, 1, 7), new DateTime(2024, 1, 1), new DateTime(2024, 1, 7)); //complete finance invoice batch record

            cpFinanceInvoiceBatchSetupsPage
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("This action can only be performed when no Finance Invoice Batch records linked to this setup record have a Batch Status of Completed.").TapCloseButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7094")]
        [Description("Step(s) 19 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases005()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN A " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Step 19

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickBatchGroupingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderBatchGroupingName, careProviderBatchGroupingId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("15/01/2024")
                .InsertTextOnStartTime("01:30")
                .SelectInvoiceBy("Funder")
                .SelectInvoiceFrequency("Every Week")
                .InsertTextOnCreateBatchWithin("0")
                .SelectChargeToDay("Sunday")
                .SelectWhenToBatchFinanceTransactions("Confirmed")
                .ClickUseTransactionEndDateWhenBatchingFinanceTransactions_YesRadioButton()
                .InsertTextOnFinanceTransactionsUpTo("08/01/2024")
                .ClickSeparateInvoices_NoRadioButton()
                .ClickExtractNameLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderExtract1Name, careProviderExtractName1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Finance Transactions Up To must be after Start Date and before or equal to End Date").TapCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad();

            var fibsRecords = dbHelper.careProviderFinanceInvoiceBatchSetup.GetByContractScheme(careProviderContractScheme1Id);
            Assert.AreEqual(0, fibsRecords.Count);


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7095")]
        [Description("Step(s) 20 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases006()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.UpdateEndDate(fibsId, new DateTime(2024, 1, 7));

            #endregion

            #region Step 20

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnFinanceTransactionsUpTo("08/01/2024")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Finance Transactions Up To must be after Start Date and before or equal to End Date").TapCloseButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7096")]
        [Description("Step(s) 22 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases007()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion


            #region Finance Transactions

            var fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(2, fibsTriggers.Count);

            var financeTransaction1 = fibsTriggers[0];
            var financeTransaction2 = fibsTriggers[1];

            #endregion


            #region Step 22

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnFinanceTransactionsUpTo("14/01/2024")
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();


            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transactions

            fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, fibsTriggers.Count);

            var financeTransaction3 = fibsTriggers.Where(c => c != financeTransaction1 && c != financeTransaction2).FirstOrDefault();

            #endregion


            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6999

        [TestProperty("JiraIssueID", "ACC-7097")]
        [Description("Step(s) 23 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases008()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            var contractScheme2Name = "CPCS_" + currentTimeString + "_B";
            var contractScheme2Code = contractScheme1Code + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2024, 1, 1), contractScheme2Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;

            var fibs1Id = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            var fibs2Id = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme2Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transactions

            var fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(2, fibsTriggers.Count);

            var financeTransaction1 = fibsTriggers[0];
            var financeTransaction2 = fibsTriggers[1];

            #endregion

            #region Audit Reasons

            var _auditReasonName = "Default Change Bulk Edit";
            var _auditReasonId = commonMethodsDB.CreateErrorManagementReason(_auditReasonName, new DateTime(2020, 1, 1), 3, _teamId, false);

            #endregion


            #region Step 23

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery("CPCS_" + currentTimeString)
                .ClickSearchButton()
                .SelectRecord(fibs1Id)
                .SelectRecord(fibs2Id)
                .ClickBulkUpdateButton();

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2", true)
                .ClickUpdateCheckBox("financetransactionsupto")
                .InsertValueInInputField("financetransactionsupto", "14/01/2024")
                .ClickAuditReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_auditReasonName).TapSearchButton().SelectResultElement(_auditReasonId);

            bulkEditDialogPopup
                .WaitForBulkEditDialogPopupToLoad("2", true)
                .ClickUpdateButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();


            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Finance Transactions

            fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(3, fibsTriggers.Count);

            var financeTransaction3 = fibsTriggers.Where(c => c != financeTransaction1 && c != financeTransaction2).FirstOrDefault();

            #endregion


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7098")]
        [Description("Step(s) 24 to 26 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases009()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 1; //Monday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 7);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.UpdateEndDate(fibsId, new DateTime(2024, 1, 31));

            #endregion


            #region Step 24

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()

                .ClickAdditionalItemsButton()
                .ValidateToolbarOptionsDisplayed()
                .ClickAdditionalItemsButton()

                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickAdditionalItemsButton()
                .ValidateToolbarOptionsDisplayed()
                .ClickBackButton();

            #endregion

            #region Step 25

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ValidateHeaderCellText(2, "Contract Scheme")
                .ValidateHeaderCellText(3, "Batch Grouping")
                .ValidateHeaderCellText(4, "Start Date")
                .ValidateHeaderCellText(5, "End Date")
                .ValidateHeaderCellText(6, "Invoice By")
                .ValidateHeaderCellText(7, "Invoice Frequency")
                .ValidateHeaderCellText(8, "Create Batch Within")
                .ValidateHeaderCellText(9, "Charge To Day")
                .ValidateHeaderCellText(10, "Separate Invoices?")
                .ValidateHeaderCellText(11, "Finance Transactions Up To")
                .ValidateHeaderCellText(12, "Extract Name")
                .ValidateHeaderCellText(13, "Debtor Reference Number Required?")

                .ValidateHeaderCellSortIcon(2, true)
                .ValidateHeaderCellSortIcon(3, true)
                ;

            #endregion

            #region Step 26

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoice Batch Setups")
                .WaitForAdvanceSearchPageToLoad()

                .SelectFilter("1", "Contract Scheme")
                .SelectFilter("1", "Batch Grouping")
                .SelectFilter("1", "Start Date")
                .SelectFilter("1", "End Date")
                .SelectFilter("1", "Invoice By")
                .SelectFilter("1", "Invoice Frequency")
                .SelectFilter("1", "Create Batch Within")
                .SelectFilter("1", "Charge To Day")
                .SelectFilter("1", "Separate Invoices?")
                .SelectFilter("1", "Extract Name")
                .SelectFilter("1", "Debtor Reference Number Required?")
                .SelectFilter("1", "Responsible Team")
                .SelectFilter("1", "Finance Transactions Up To")
                .SelectFilter("1", "When to Batch Finance Transactions?")
                .SelectFilter("1", "Use Transaction End Date when Batching Finance Transactions?");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7099")]
        [Description("Step(s) 27 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases010()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 3, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion


            #region Step 27

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord();

            var financeInvoiceBatchs = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatchs.Count);
            var financeInvoiceBatchId = financeInvoiceBatchs.First();
            var financeInvoiceBatchFields = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(financeInvoiceBatchId, "careproviderfinanceinvoicebatchnumber", "runon");
            var financeInvoiceBatchNumber = (financeInvoiceBatchFields["careproviderfinanceinvoicebatchnumber"]).ToString();
            var runOnDate = ((DateTime)(financeInvoiceBatchFields["runon"])).ToString("dd/MM/yyyy");

            cpFinanceInvoiceBatchesPage
                .OpenRecord(financeInvoiceBatchId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()

                .ValidateBatchIDText(financeInvoiceBatchNumber)
                .ValidateFinanceInvoiceBatchSetupLinkText(contractScheme1Name + " \\ Monthly \\ 01/01/2024")
                .ValidateRunOnText(runOnDate)
                .ValidateBatchStatusText("New")
                .ValidatePeriodStartDateText("01/01/2024")
                .ValidatePeriodEndDateText("07/01/2024")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Monthly")
                .ValidateIsAdHoc_NoChecked()
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateUserFieldLinkText("")
                .ValidateNumberOfInvoicesCancelledText("")

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7100")]
        [Description("Step(s) 28 from the original test (Frequency = Every Calendar Month)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases011()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 5; //Every Calendar Month
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 3, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion


            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord();

            var financeInvoiceBatchs = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatchs.Count);
            var financeInvoiceBatchId = financeInvoiceBatchs.First();
            var financeInvoiceBatchFields = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(financeInvoiceBatchId, "careproviderfinanceinvoicebatchnumber", "runon");
            var financeInvoiceBatchNumber = (financeInvoiceBatchFields["careproviderfinanceinvoicebatchnumber"]).ToString();
            var runOnDate = ((DateTime)(financeInvoiceBatchFields["runon"])).ToString("dd/MM/yyyy");

            cpFinanceInvoiceBatchesPage
                .OpenRecord(financeInvoiceBatchId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()

                .ValidateBatchIDText(financeInvoiceBatchNumber)
                .ValidateFinanceInvoiceBatchSetupLinkText(contractScheme1Name + " \\ Monthly \\ 01/01/2024")
                .ValidateRunOnText(runOnDate)
                .ValidateBatchStatusText("New")
                .ValidatePeriodStartDateText("01/01/2024")
                .ValidatePeriodEndDateText("31/01/2024")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Monthly")
                .ValidateIsAdHoc_NoChecked()
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateUserFieldLinkText("")
                .ValidateNumberOfInvoicesCancelledText("")

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7101")]
        [Description("Step(s) 28 from the original test (Frequency = Every Calendar Month (Date Specific))")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases012()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 6; //Every Calendar Month (Date Specific)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 3, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 18), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion


            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord();

            var financeInvoiceBatchs = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatchs.Count);
            var financeInvoiceBatchId = financeInvoiceBatchs.First();
            var financeInvoiceBatchFields = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(financeInvoiceBatchId, "careproviderfinanceinvoicebatchnumber", "runon");
            var financeInvoiceBatchNumber = (financeInvoiceBatchFields["careproviderfinanceinvoicebatchnumber"]).ToString();
            var runOnDate = ((DateTime)(financeInvoiceBatchFields["runon"])).ToString("dd/MM/yyyy");

            cpFinanceInvoiceBatchesPage
                .OpenRecord(financeInvoiceBatchId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()

                .ValidateBatchIDText(financeInvoiceBatchNumber)
                .ValidateFinanceInvoiceBatchSetupLinkText(contractScheme1Name + " \\ Monthly \\ 18/01/2024")
                .ValidateRunOnText(runOnDate)
                .ValidateBatchStatusText("New")
                .ValidatePeriodStartDateText("18/01/2024")
                .ValidatePeriodEndDateText("17/02/2024")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Monthly")
                .ValidateIsAdHoc_NoChecked()
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateUserFieldLinkText("")
                .ValidateNumberOfInvoicesCancelledText("")

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7102")]
        [Description("Step(s) 28 from the original test (Frequency = Every Quarter)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases013()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2022, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2022, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 8; //Every Quarter
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2022, 12, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2022, 4, 15), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion


            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord();

            var financeInvoiceBatchs = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatchs.Count);
            var financeInvoiceBatchId = financeInvoiceBatchs.First();
            var financeInvoiceBatchFields = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(financeInvoiceBatchId, "careproviderfinanceinvoicebatchnumber", "runon");

            cpFinanceInvoiceBatchesPage
                .OpenRecord(financeInvoiceBatchId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()

                .ValidateFinanceInvoiceBatchSetupLinkText(contractScheme1Name + " \\ Monthly \\ 15/04/2022")
                .ValidateBatchStatusText("New")
                .ValidatePeriodStartDateText("15/04/2022")
                .ValidatePeriodEndDateText("30/06/2022")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Monthly")
                .ValidateIsAdHoc_NoChecked()
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateUserFieldLinkText("")
                .ValidateNumberOfInvoicesCancelledText("")

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7103")]
        [Description("Step(s) 28 from the original test (Frequency = Every Quarter (Date Specific))")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases014()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2022, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2022, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 9; //Every Quarter (Date Specific)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2022, 12, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2022, 4, 15), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion


            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord();

            var financeInvoiceBatchs = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatchs.Count);
            var financeInvoiceBatchId = financeInvoiceBatchs.First();
            var financeInvoiceBatchFields = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(financeInvoiceBatchId, "careproviderfinanceinvoicebatchnumber", "runon");

            cpFinanceInvoiceBatchesPage
                .OpenRecord(financeInvoiceBatchId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()

                .ValidateFinanceInvoiceBatchSetupLinkText(contractScheme1Name + " \\ Monthly \\ 15/04/2022")
                .ValidateBatchStatusText("New")
                .ValidatePeriodStartDateText("15/04/2022")
                .ValidatePeriodEndDateText("14/07/2022")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Monthly")
                .ValidateIsAdHoc_NoChecked()
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateUserFieldLinkText("")
                .ValidateNumberOfInvoicesCancelledText("")

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7104")]
        [Description("Step(s) 28 from the original test (Frequency = Every Year)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases015()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2022, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2022, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 10; //Every Year
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2023, 12, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2022, 10, 10), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion


            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord();

            var financeInvoiceBatchs = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatchs.Count);
            var financeInvoiceBatchId = financeInvoiceBatchs.First();
            var financeInvoiceBatchFields = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(financeInvoiceBatchId, "careproviderfinanceinvoicebatchnumber", "runon");

            cpFinanceInvoiceBatchesPage
                .OpenRecord(financeInvoiceBatchId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()

                .ValidateFinanceInvoiceBatchSetupLinkText(contractScheme1Name + " \\ Monthly \\ 10/10/2022")
                .ValidateBatchStatusText("New")
                .ValidatePeriodStartDateText("10/10/2022")
                .ValidatePeriodEndDateText("31/12/2022")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Monthly")
                .ValidateIsAdHoc_NoChecked()
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateUserFieldLinkText("")
                .ValidateNumberOfInvoicesCancelledText("")

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7105")]
        [Description("Step(s) 28 from the original test (Frequency = Every Year (Date Specific))")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases016()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2020, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 11; //Every Year (Date Specific)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2023, 12, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2021, 12, 15), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion


            #region Step 28

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord();

            var financeInvoiceBatchs = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatchs.Count);
            var financeInvoiceBatchId = financeInvoiceBatchs.First();
            var financeInvoiceBatchFields = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchId(financeInvoiceBatchId, "careproviderfinanceinvoicebatchnumber", "runon");

            cpFinanceInvoiceBatchesPage
                .OpenRecord(financeInvoiceBatchId);

            cpFinanceInvoiceBatchRecordPage
                .WaitForCPFinanceInvoiceBatchRecordPageToLoad()

                .ValidateFinanceInvoiceBatchSetupLinkText(contractScheme1Name + " \\ Monthly \\ 15/12/2021")
                .ValidateBatchStatusText("New")
                .ValidatePeriodStartDateText("15/12/2021")
                .ValidatePeriodEndDateText("14/12/2022")
                .ValidateWhenToBatchFinanceTransactionsSelectedText("Does Not Matter")

                .ValidateContractSchemeLinkText(contractScheme1Name)
                .ValidateBatchGroupingLinkText("Monthly")
                .ValidateIsAdHoc_NoChecked()
                .ValidateNetbatchtotalText("")
                .ValidateGrossbatchtotalText("")
                .ValidateVattotalText("")
                .ValidateNumberofInvoicesCreatedText("")
                .ValidateUserFieldLinkText("")
                .ValidateNumberOfInvoicesCancelledText("")

                .ValidateResponsibleTeamLinkText(_teamName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7106")]
        [Description("Step(s) 30 to 32 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases017()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2020, 1, 1), null, false, false);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 11; //Every Year (Date Specific)
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2023, 12, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2021, 12, 15), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion


            #region Step 30

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickFinanceInvoiceBatchesTab();

            cpFinanceInvoiceBatchesPage
                .WaitForPageToLoad_FromFIBSRecord();

            var financeInvoiceBatchs = dbHelper.careProviderFinanceInvoiceBatch.GetActiveFinanceInvoiceBatchBySetupID(fibsId);
            Assert.AreEqual(1, financeInvoiceBatchs.Count);
            var financeInvoiceBatchId = financeInvoiceBatchs.First();

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

            #endregion

            #region Step 31

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Finance Invoice Batches")
                .WaitForAdvanceSearchPageToLoad()

                .SelectFilter("1", "Contract Scheme")
                .SelectFilter("1", "Batch Grouping")
                .SelectFilter("1", "Person")
                .SelectFilter("1", "Batch ID")
                .SelectFilter("1", "Batch Status")
                .SelectFilter("1", "Run On")
                .SelectFilter("1", "Period Start Date")
                .SelectFilter("1", "Period End Date")
                .SelectFilter("1", "Ad Hoc Batch?")
                .SelectFilter("1", "Net Batch Total")
                .SelectFilter("1", "Gross Batch Total")
                .SelectFilter("1", "Number Of Invoices Cancelled")
                .SelectFilter("1", "Finance Invoice Batch")
                .SelectFilter("1", "Finance Invoice Batch Setup")
                .SelectFilter("1", "User");

            #endregion

            #region Step 32

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()

                .ValidateInvoiceTextTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice, will be 250")
                .ValidateTransactionTextContraTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100")
                .ValidateTransactionTextAdditionalTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100")
                .ValidateTransactionTextApportionedPersonTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100")
                .ValidateTransactionTextApportionedProviderTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100")

                .ValidateTransactionTextStandardTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100")
                .ValidateTransactionTextEndReasonTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100")
                .ValidateTransactionTextNetIncomeTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100")
                .ValidateTransactionTextApportionedFunderTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100")
                .ValidateTransactionTextExpenseTooltip("The maximum number of Characters that will be generated and show on a Finance Invoice against each Finance Transaction, will be 100");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7107")]
        [Description("Step(s) 33 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases018()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();
            var financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("Charge for services at " + providerName + " up to 07/01/2024", financeInvoice_InvoiceText);

            #endregion

            #region Care Provider Finance Transactions

            var allFinanceTransactions = dbHelper.careProviderFinanceTransaction.GetFinanceTransactionByInvoiceID(financeInvoiceId);
            Assert.AreEqual(1, allFinanceTransactions.Count);
            var financeTransactionId = allFinanceTransactions.First();
            var financeTransaction_TransactionText = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "transactiontext")["transactiontext"]).ToString();
            Assert.AreEqual("Charge for " + personFullName + " for the period 01/01/2024 to 07/01/2024", financeTransaction_TransactionText);

            #endregion


            #region Step 33

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnInvoiceText("Charge for services at {Establishment} up to {Charges Up To} - Updated")
                .InsertTextOnTransactionTextStandard("Charge for {Person} for the period {Start Date} to {End Date} - Updated")
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            #region Care Provider Finance Invoice batch

            allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(2, allFinanceInvoiceBatches.Count);
            FinanceInvoicebatchId = allFinanceInvoiceBatches.Where(c => c != FinanceInvoicebatchId).First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            financeInvoiceId = allFinanceInvoices.First();
            financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("Charge for services at " + providerName + " up to 14/01/2024 - Updated", financeInvoice_InvoiceText);

            #endregion

            #region Care Provider Finance Transactions

            allFinanceTransactions = dbHelper.careProviderFinanceTransaction.GetFinanceTransactionByInvoiceID(financeInvoiceId);
            Assert.AreEqual(1, allFinanceTransactions.Count);
            financeTransactionId = allFinanceTransactions.First();
            financeTransaction_TransactionText = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "transactiontext")["transactiontext"]).ToString();

            //NOTE: the finance transaction was created before the FIBS record was updated, therefore it will not be regenerated again
            Assert.AreEqual("Charge for " + personFullName + " for the period 08/01/2024 to 14/01/2024", financeTransaction_TransactionText);

            #endregion

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7108")]
        [Description("Step(s) 34 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases019()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();
            var financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("Charge for services at " + providerName + " up to 07/01/2024", financeInvoice_InvoiceText);

            #endregion


            #region Step 34

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnInvoiceText("Charge for services at {Invalid} up to {Also Invalid}")
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            #region Care Provider Finance Invoice batch

            allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(2, allFinanceInvoiceBatches.Count);
            FinanceInvoicebatchId = allFinanceInvoiceBatches.Where(c => c != FinanceInvoicebatchId).First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            financeInvoiceId = allFinanceInvoices.First();
            financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("Charge for services at {Invalid} up to {Also Invalid}", financeInvoice_InvoiceText);

            #endregion

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7109")]
        [Description("Step(s) 35 from the original test (Fields from Finance Invoice)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases020()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();
            var financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("Charge for services at " + providerName + " up to 07/01/2024", financeInvoice_InvoiceText);

            #endregion


            #region Step 35

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnInvoiceText("{Payer} - {Person} - {Contract Scheme} - {Establishment} - {Funder} - {Charges Up To}")
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            #region Care Provider Finance Invoice batch

            allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(2, allFinanceInvoiceBatches.Count);
            FinanceInvoicebatchId = allFinanceInvoiceBatches.Where(c => c != FinanceInvoicebatchId).First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            financeInvoiceId = allFinanceInvoices.First();
            financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual(funderProviderName + " -  - " + contractScheme1Name + " - " + providerName + " - " + funderProviderName + " - 14/01/2024", financeInvoice_InvoiceText);

            #endregion

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7110")]
        [Description("Step(s) 35 from the original test (Fields from Finance Transaction)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases021()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();
            var financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("Charge for services at " + providerName + " up to 07/01/2024", financeInvoice_InvoiceText);

            #endregion


            #region Step 35

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnInvoiceText("{Earliest Charge Start} - {Latest Charge End}")
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            #region Care Provider Finance Invoice batch

            allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(2, allFinanceInvoiceBatches.Count);
            FinanceInvoicebatchId = allFinanceInvoiceBatches.Where(c => c != FinanceInvoicebatchId).First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            financeInvoiceId = allFinanceInvoices.First();
            financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("08/01/2024 - 14/01/2024", financeInvoice_InvoiceText);

            #endregion

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7111")]
        [Description("Step(s) 35 from the original test (Fields from Finance Invoice Batch)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases022()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();
            var financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("Charge for services at " + providerName + " up to 07/01/2024", financeInvoice_InvoiceText);

            #endregion


            #region Step 35

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnInvoiceText("{Period Start}")
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            #region Care Provider Finance Invoice batch

            allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(2, allFinanceInvoiceBatches.Count);
            FinanceInvoicebatchId = allFinanceInvoiceBatches.Where(c => c != FinanceInvoicebatchId).First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            financeInvoiceId = allFinanceInvoices.First();
            financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("08/01/2024", financeInvoice_InvoiceText);

            #endregion

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7112")]
        [Description("Step(s) 35 from the original test (Fields from FI Person field)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases023()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 2; //Funder / Service User
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);
            dbHelper.person.UpdateNHSNumber(_personID, "987 654 3210");
            dbHelper.person.UpdateLASocialCareRef(_personID, "a1b2c3d4");
            dbHelper.person.UpdateNationalInsuranceNumber(_personID, "AA 000000");


            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();
            var financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual("Charge for services at " + providerName + " up to 07/01/2024", financeInvoice_InvoiceText);

            #endregion


            #region Step 35

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(fibsId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnInvoiceText("{Person Id} - {Person Id NHS} - {Person Id LA} - {Person Id NI}")
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            #region Care Provider Finance Invoice batch

            allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(2, allFinanceInvoiceBatches.Count);
            FinanceInvoicebatchId = allFinanceInvoiceBatches.Where(c => c != FinanceInvoicebatchId).First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            financeInvoiceId = allFinanceInvoices.First();
            financeInvoice_InvoiceText = (dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceById(financeInvoiceId, "invoicetext")["invoicetext"]).ToString();
            Assert.AreEqual(_personNumber + " - 987 654 3210 - a1b2c3d4 - AA 000000", financeInvoice_InvoiceText);

            #endregion

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7113")]
        [Description("Step(s) 36 from the original test (Part 1)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases024()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.UpdateTransactionTextStandard(fibsId, "{Person} - {Contract Scheme} - {Establishment}");

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();

            #endregion

            #region Finance Transactions

            var fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(5, fibsTriggers.Count);

            var financeTransactionId = fibsTriggers[0];
            var financeTransactionNo = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion


            #region Step 36

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceTransactionsSection();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(financeTransactionNo)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .OpenRecord(financeTransactionId);


            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionText(personFullName + " - " + contractScheme1Name + " - " + providerName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7114")]
        [Description("Step(s) 36 from the original test (Part 2)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases025()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.UpdateTransactionTextStandard(fibsId, "{Funder} - {Service} - {Rate Unit}");

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();

            #endregion

            #region Finance Transactions

            var fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(5, fibsTriggers.Count);

            var financeTransactionId = fibsTriggers[0];
            var financeTransactionNo = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion


            #region Step 36

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceTransactionsSection();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(financeTransactionNo)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .OpenRecord(financeTransactionId);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionText(funderProviderName + " - " + careProviderServiceName + " - Default Care Provider Rate Unit");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7115")]
        [Description("Step(s) 36 from the original test (Part 3)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases026()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.UpdateTransactionTextStandard(fibsId,
                "{Total Units} - {Contract Type} - {Start Date} - {End Date} - {Gross Amount} - {VAT Amount} - {Net Amount} - {Start Time} - {End Time}");

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();

            #endregion

            #region Finance Transactions

            var fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(5, fibsTriggers.Count);

            var financeTransactionId = fibsTriggers[4];
            var financeTransactionNo = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion


            #region Step 36

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceTransactionsSection();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(financeTransactionNo)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .OpenRecord(financeTransactionId);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionText("1 - Spot - 01/01/2024 - 07/01/2024 - £12.00 - £2.00 - £10.00 -  -");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7116")]
        [Description("Step(s) 36 from the original test (Part 4)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Transactions")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases027()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC ACC-891 T2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Service

            var careProviderServiceName = "CPS A " + currentTimeString;
            var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
            var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_teamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_" + currentTimeString + "_A";
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, providerId, funderProviderID);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

            #endregion

            #region VAT Code

            var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Rate Unit

            var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Service Detail

            var Code1 = dbHelper.careProviderServiceDetail.GetAllCareProviderServiceDetail().Count + 1;
            while (dbHelper.careProviderServiceDetail.GetByCode(Code1).Any())
                Code1 = Code1 + 1;

            var careProviderServiceDetailId = commonMethodsDB.CreateCareProviderServiceDetail(_teamId, "CPSD ACC-891", Code1, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Service Mapping

            var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _teamId, careProviderServiceDetailId, null, null, "");

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _defaultLoginUserID, _businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, careProviderServiceDetailId, null, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Contract Service Rate Period

            dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2024, 1, 1), _careProviderRateUnitId, 10, _teamId);

            #endregion

            #region Finance Invoice Batch Setup

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var invoicebyid = 1; //funder
            var careproviderinvoicefrequencyid = 1; //Every Week
            var createbatchwithin = 1;
            var chargetodayid = 7; //Sunday
            var whentobatchfinancetransactionsid = 3; //Does Not Matter
            var useenddatewhenbatchingfinancetransactions = true;
            var financetransactionsupto = new DateTime(2024, 1, 31);
            var separateinvoices = false;

            var fibsId = dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                careProviderContractScheme1Id, careProviderBatchGroupingId,
                new DateTime(2024, 1, 1), new TimeSpan(0, 0, 0),
                invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                careProviderExtractName1Id, true,
                _teamId);

            dbHelper.careProviderFinanceInvoiceBatchSetup.UpdateTransactionTextStandard(fibsId,
                "{Booking Type} - {Confirmed} - {Expense} - {Payer} - {Full Charge} - {Apportioned}");

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName = "Carlos";
            var lastName = currentTimeString;
            var personFullName = firstName + " " + currentTimeString;
            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, new DateTime(2024, 1, 1), null, true);

            #endregion

            #region Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, _teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, new DateTime(2024, 1, 1), 1, 1, _careProviderRateUnitId);
            dbHelper.careProviderPersonContractService.UpdateStatus(careProviderPersonContractServiceId, 90); //Completed

            #endregion

            #region Schdeduled job for "Process CP Finance Transaction Triggers"

            ExecuteScheduleJob("Process CP Finance Transaction Triggers");

            #endregion

            #region Care Provider Finance Invoice batch

            var allFinanceInvoiceBatches = dbHelper.careProviderFinanceInvoiceBatch.GetByFinanceInvoiceBatchSetupID(fibsId);
            Assert.AreEqual(1, allFinanceInvoiceBatches.Count);
            var FinanceInvoicebatchId = allFinanceInvoiceBatches.First();

            #endregion

            #region Schdeduled job for "Process CP Finance Invoice Batches"

            ExecuteScheduleJob("Process CP Finance Invoice Batches");

            #endregion

            #region Care Provider Finance Invoices

            var allFinanceInvoices = dbHelper.careProviderFinanceInvoice.GetCareProviderFinanceInvoiceByInvoiceBatchId(FinanceInvoicebatchId);
            Assert.AreEqual(1, allFinanceInvoices.Count);
            var financeInvoiceId = allFinanceInvoices.First();

            #endregion

            #region Finance Transactions

            var fibsTriggers = dbHelper.careProviderFinanceTransaction.GetByPersonContractService(careProviderPersonContractServiceId);
            Assert.AreEqual(5, fibsTriggers.Count);

            var financeTransactionId = fibsTriggers[4];
            var financeTransactionNo = (dbHelper.careProviderFinanceTransaction.GetFinanceTransactionById(financeTransactionId, "careproviderfinancetransactionnumber")["careproviderfinancetransactionnumber"]).ToString();

            #endregion


            #region Step 36

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceTransactionsSection();

            cpFinanceTransactionsPage
                .WaitForCPFinanceTransactionsPageToLoad()
                .InsertSearchQuery(financeTransactionNo)
                .ClickSearchButton()
                .WaitForCPFinanceTransactionsPageToLoad()
                .OpenRecord(financeTransactionId);

            cpFinanceTransactionRecordPage
                .WaitForPageToLoad()
                .ValidateTransactionText("- No -  - " + funderProviderName + " - {Full Charge} - No");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7117")]
        [Description("Step(s) 39 to 41 from the original test ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Invoicing")]
        [TestProperty("Screen1", "Finance Invoice Batch Setups")]
        public void FinanceInvoiceBatchSetup_ACC_891_UITestCases028()
        {
            #region Provider

            var providerName = "Provider " + currentTimeString;
            var addressType = 10; //Home
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);
            dbHelper.careProviderContractScheme.UpdateSundriesApply(careProviderContractScheme1Id, true);

            #endregion

            #region Care Provider Batch Grouping

            string careProviderBatchGroupingName = "Monthly";
            Guid careProviderBatchGroupingId = dbHelper.careProviderBatchGrouping.GetByName(careProviderBatchGroupingName)[0];

            #endregion

            #region Care Provider Extract Name

            var careProviderExtract1Name = "CPEN A " + currentTimeString;
            var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
            var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2023, 1, 1), null, false, false);

            var careProviderExtract2Name = "CPEN B " + currentTimeString;
            var careProviderExtractName2Code = careProviderExtractName1Code + 1;
            var careProviderExtractName2Id = commonMethodsDB.CreateCareProviderExtractName(_teamId, careProviderExtract2Name, careProviderExtractName2Code, null, new DateTime(2023, 1, 1), null, false, false);

            #endregion


            #region Step 39

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderInvoicingExpandButton()
                .ClickFinanceInvoiceBatchSetupsButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickIsThisASundryBatch_YesRadioButton()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .InsertTextOnStartTime("01:30")
                .SelectChargeToDay("Sunday")
                .ClickSeparateInvoices_NoRadioButton()
                .ClickExtractNameLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderExtract1Name, careProviderExtractName1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad();

            var fibsRecords = dbHelper.careProviderFinanceInvoiceBatchSetup.GetByContractScheme(careProviderContractScheme1Id);
            Assert.AreEqual(1, fibsRecords.Count);
            var financeInvoiceBatchSetupId = fibsRecords.First();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickIsThisASundryBatch_YesRadioButton()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractScheme1Name, careProviderContractScheme1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .InsertTextOnStartTime("01:30")
                .SelectChargeToDay("Sunday")
                .ClickSeparateInvoices_NoRadioButton()
                .ClickExtractNameLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(careProviderExtract1Name, careProviderExtractName1Id);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("The same Contract Scheme can not have more than one record where it is a Sundry Batch. Please correct as necessary.").TapCloseButton();

            #endregion

            #region Step 40

            cpFinanceInvoiceBatchSetupRecordPage
                   .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            cpFinanceInvoiceBatchSetupsPage
                .WaitForPageToLoad()
                .SelectSystemView("Sundry Batches")
                .WaitForPageToLoad()
                .SelectSystemView("Non Sundry Batches")
                .WaitForPageToLoad();

            #endregion

            #region Step 41

            cpFinanceInvoiceBatchSetupsPage
                .InsertSearchQuery(contractScheme1Name)
                .ClickSearchButton()
                .WaitForPageToLoad()
                .OpenRecord(financeInvoiceBatchSetupId);

            cpFinanceInvoiceBatchSetupRecordPage
                .WaitForPageToLoad()
                .ValidateIsThisASundryBatchFieldEnabled(false);

            #endregion

        }

        #endregion

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;


namespace Phoenix.UITests.Settings.Configuration
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Applicant_UITestCases4 : FunctionalTest
    {
        #region properties

        private string _environmentName;
        private string _tenantName;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private Guid _applicantId;
        private Guid _loginUserId;
        private string _applicantFirstName;
        private string _applicantLastName;

        #endregion

        [TestInitialize()]
        public void TestMethod_Setup()
        {
            #region Settings

            _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
            _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
            dbHelper = new DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            #endregion

            #region Internal

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

            #endregion

            #region Language

            _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Business Unit

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("Recruitment Document BU");

            #endregion

            #region Team

            _teamId = commonMethodsDB.CreateTeam("Recruitment Document Team", null, _businessUnitId, "907678", "RecruitmentDocumentTeam@careworkstempmail.com", "Recruitment Document Team", "020 123456");

            #endregion

            #region System User RecruitmentDocumentUser1

            _loginUserId = commonMethodsDB.CreateSystemUserRecord("RecruitmentDocumentUser1", "RecruitmentDocument", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Applicant

            _applicantFirstName = "Patrick";
            _applicantLastName = "" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(_applicantFirstName, _applicantLastName, _teamId);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-23595

        [TestProperty("JiraIssueID", "ACC-3361")]
        [Description("Step(s) 5 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod001()
        {
            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "RI_" + commonMethodsHelper.GetCurrentDateTimeString();
            var _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Compliance 

            var complianceId = dbHelper.compliance.CreateCompliance(_teamId, _applicantId, "applicant", _applicantFirstName + " " + _applicantLastName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1);

            #endregion

            #region Steps 5

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecruitmentDocumentsRecordIsPresent(complianceId.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3362")]
        [Description("Step(s) 6 to 7 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod002()
        {
            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "RI_" + commonMethodsHelper.GetCurrentDateTimeString();
            int _complianceRecurrenceId = 1; //Weekly
            var _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1), _complianceRecurrenceId);

            #endregion

            #region Steps 6

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItemName).TapSearchButton().SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue("01/02/2023")
                .ValidateCompletedDateFieldDisabled(false)
                .ValidateValidFromDateFieldDisabled(true)
                .ValidateValidToDateFieldDisabled(true);

            #endregion

            #region Step 7

            var todaysDate = commonMethodsHelper.GetCurrentDate();
            var expectedValidToDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7).ToString("dd'/'MM'/'yyyy");

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue(todaysDate)
                .InsertReferenceNumber("1A")
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            System.Threading.Thread.Sleep(3000);
            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);
            var newComplianceRecordId = complianceRecords[0];

            recruitmentDocumentsPage
                .OpenRecruitmentDocumentsRecord(newComplianceRecordId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ValidateRequestedDateField("01/02/2023")
                .ValidateCompletedDateField(todaysDate)
                .ValidateValidToDateField(expectedValidToDate);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3363")]
        [Description("Step(s) 8 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod003()
        {
            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "RI_" + commonMethodsHelper.GetCurrentDateTimeString();
            int? _complianceRecurrenceId = null;
            var _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1), _complianceRecurrenceId);

            #endregion

            var todaysDate = commonMethodsHelper.GetCurrentDate();

            #region Steps 8

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItemName).TapSearchButton().SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertReferenceNumber("1A")
                .InsertRequestedDateValue("01/02/2023")
                .InsertCompletedDateValue(todaysDate)
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            System.Threading.Thread.Sleep(3000);
            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);
            var newComplianceRecordId = complianceRecords[0];

            recruitmentDocumentsPage
                .OpenRecruitmentDocumentsRecord(newComplianceRecordId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ValidateRequestedDateField("01/02/2023")
                .ValidateCompletedDateField(todaysDate)
                .ValidateValidToDateField("");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3364")]
        [Description("Step(s) 9 and 10 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod004()
        {
            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "RI_" + commonMethodsHelper.GetCurrentDateTimeString();
            int? _complianceRecurrenceId = null;
            var _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1), _complianceRecurrenceId);

            #endregion

            var todaysDate = commonMethodsHelper.GetCurrentDate();
            var futureDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(3).ToString("dd'/'MM'/'yyyy");

            #region Steps 9

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItemName).TapSearchButton().SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertReferenceNumber("1A")
                .InsertRequestedDateValue("01/02/2023")
                .InsertCompletedDateValue(futureDate);

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Completed Date cannot be a future date").TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Completed Date cannot be a future date").TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad();

            #endregion

            #region Steps 10

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue(todaysDate)
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            System.Threading.Thread.Sleep(3000);
            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);
            var newComplianceRecordId = complianceRecords[0];

            recruitmentDocumentsPage
                .OpenRecruitmentDocumentsRecord(newComplianceRecordId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ValidateRequestedDateField("01/02/2023")
                .ValidateCompletedDateField(todaysDate);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3365")]
        [Description("Step(s) 11 to 15 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod005()
        {
            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "RI_" + commonMethodsHelper.GetCurrentDateTimeString();
            int? _complianceRecurrenceId = 3; //Monthly
            var _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1), _complianceRecurrenceId);

            #endregion

            var todaysDate = commonMethodsHelper.GetCurrentDate();
            var invalidPastDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2).ToString("dd'/'MM'/'yyyy");
            var validFutureDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(15).ToString("dd'/'MM'/'yyyy");
            var invalidFutureDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(35).ToString("dd'/'MM'/'yyyy");
            var maxAllowedDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(1).ToString("dd'/'MM'/'yyyy");

            #region Steps 11

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItemName).TapSearchButton().SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertReferenceNumber("1A")
                .InsertRequestedDateValue("01/02/2023")
                .InsertCompletedDateValue(todaysDate)
                .ValidateValidFromDateFieldDisabled(false)
                .InsertValidToDateValue(validFutureDate)
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            System.Threading.Thread.Sleep(3000);
            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);
            var newComplianceRecordId = complianceRecords[0];

            recruitmentDocumentsPage
                .OpenRecruitmentDocumentsRecord(newComplianceRecordId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ValidateRequestedDateField("01/02/2023")
                .ValidateCompletedDateField(todaysDate)
                .ValidateValidToDateField(validFutureDate);

            #endregion

            #region Step 12

            recruitmentDocumentsRecordPage
                .InsertValidToDateValue(invalidFutureDate);

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Valid To Date cannot be after " + maxAllowedDate + " - the Completed Date plus the recurrence period (Monthly)").TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Valid To Date cannot be after " + maxAllowedDate + " - the Completed Date plus the recurrence period (Monthly)").TapCloseButton();

            #endregion

            #region Step 13

            recruitmentDocumentsRecordPage
                .InsertValidToDateValue(invalidPastDate);

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Valid To Date cannot be before Completed Date.").TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Valid To Date cannot be before Completed Date.").TapCloseButton();


            #endregion

            #region Step 14

            //Repetition of step 11. We already guarantee that we can change the Vadit To Date as long as is in a valid range

            #endregion

            #region Step 15

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateValidFromDateFieldDisabled(false); //We also perform this validation on step 11

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3366")]
        [Description("Step(s) 16 to 18 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod006()
        {
            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "RI_" + commonMethodsHelper.GetCurrentDateTimeString();
            int? _complianceRecurrenceId = 3; //Monthly
            var _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1), _complianceRecurrenceId);

            #endregion

            var todaysDate = commonMethodsHelper.GetCurrentDate();
            var validFromDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(10).ToString("dd'/'MM'/'yyyy");
            var validToDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(15).ToString("dd'/'MM'/'yyyy");

            #region Steps 16

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItemName).TapSearchButton().SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertReferenceNumber("1A")
                .InsertRequestedDateValue("01/02/2023")
                .InsertCompletedDateValue(todaysDate)
                .InsertValidFromDateValue(validFromDate)
                .InsertValidToDateValue(validToDate)
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            System.Threading.Thread.Sleep(3000);
            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);
            var newComplianceRecordId = complianceRecords[0];

            recruitmentDocumentsPage
                .OpenRecruitmentDocumentsRecord(newComplianceRecordId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ValidateRequestedDateField("01/02/2023")
                .ValidateCompletedDateField(todaysDate)
                .ValidateValidFromDateField(validFromDate)
                .ValidateValidToDateField(validToDate);

            #endregion

            #region Step 17

            validFromDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(17).ToString("dd'/'MM'/'yyyy");
            validToDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(15).ToString("dd'/'MM'/'yyyy");

            recruitmentDocumentsRecordPage
                .InsertValidFromDateValue(validFromDate)
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Valid To Date cannot be before Valid From Date").TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad();

            #endregion

            #region Step 18

            var newCompletedDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3).ToString("dd'/'MM'/'yyyy");
            var newExpectedValidToDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3).AddMonths(1).ToString("dd'/'MM'/'yyyy");

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue(newCompletedDate)
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(newComplianceRecordId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateValidToDateField(newExpectedValidToDate);


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3367")]
        [Description("Step(s) 19 to 24 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod007()
        {
            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "RI_" + commonMethodsHelper.GetCurrentDateTimeString();
            int? _complianceRecurrenceId = 3; //Monthly
            var _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1), _complianceRecurrenceId);

            #endregion

            #region Compliance 

            DateTime? requesteddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-13);
            DateTime? completeddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            DateTime? validfromdate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            DateTime? validtodate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);

            var compliance1Id = dbHelper.compliance.CreateCompliance(_teamId, _applicantId, "applicant", _applicantFirstName + " " + _applicantLastName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1, "1A", requesteddate, completeddate, validfromdate, validtodate, _loginUserId, _loginUserId);

            requesteddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            completeddate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            validfromdate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            validtodate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7);

            var compliance2Id = dbHelper.compliance.CreateCompliance(_teamId, _applicantId, "applicant", _applicantFirstName + " " + _applicantLastName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1, "1A", requesteddate, completeddate, validfromdate, validtodate, _loginUserId, _loginUserId);

            requesteddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            completeddate = null;
            validfromdate = null;
            validtodate = null;

            var compliance3Id = dbHelper.compliance.CreateCompliance(_teamId, _applicantId, "applicant", _applicantFirstName + " " + _applicantLastName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1, "1A", requesteddate, completeddate, validfromdate, validtodate, _loginUserId, _loginUserId);

            requesteddate = null;
            completeddate = null;
            validfromdate = null;
            validtodate = null;

            var compliance4Id = dbHelper.compliance.CreateCompliance(_teamId, _applicantId, "applicant", _applicantFirstName + " " + _applicantLastName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1, "1A", requesteddate, completeddate, validfromdate, validtodate, _loginUserId, _loginUserId);



            #endregion


            #region Steps 19 and 20

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .SelectView("Expired")
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance1Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance2Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance3Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance4Id.ToString());

            #endregion

            #region Step 21

            recruitmentDocumentsPage
                .SelectView("Completed")
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance1Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance2Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance3Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance4Id.ToString());

            #endregion

            #region Step 22

            recruitmentDocumentsPage
                .SelectView("Requested")
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance1Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance2Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance3Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance4Id.ToString());

            #endregion

            #region Step 23

            recruitmentDocumentsPage
                .SelectView("Outstanding")
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance1Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance2Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsNotPresent(compliance3Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance4Id.ToString());

            #endregion

            #region Step 24

            recruitmentDocumentsPage
                .SelectView("Related Records")
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance1Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance2Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance3Id.ToString())
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance4Id.ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1604

        [TestProperty("JiraIssueID", "ACC-3368")]
        [Description("Step(s) 1 to 9 from the original test method CDV6-14671")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod008()
        {
            #region System User RecruitmentDocumentUser2

            var _user2Id = commonMethodsDB.CreateSystemUserRecord("RecruitmentDocumentUser2", "RecruitmentDocument", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Recruitment Item (Driving Licence)

            var compliancerecurrenceid = 6; //Annually
            var _staffRecruitmentItem1Id = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, "Driving Licence", new DateTime(2022, 1, 1), compliancerecurrenceid);

            #endregion

            #region Staff Recruitment Item (DBS Enhanced Level)

            var _staffRecruitmentItem2Id = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, "DBS Enhanced Level", new DateTime(2022, 1, 1), null);

            #endregion

            #region Recruitment Document

            var recruitmentDocument1Id = dbHelper.compliance.CreateCompliance(_teamId, _applicantId, "applicant", "Patrick " + _applicantLastName, _staffRecruitmentItem1Id, "StaffRecruitmentItem", "Driving Licence", 1);

            #endregion


            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(recruitmentDocument1Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRequestedByFieldDisabled(true)
                .ValidateCompletedDateFieldDisabled(true)
                .ValidateCompletedByFieldDisabled(true)
                .ValidateValidFromDateFieldDisabled(true)
                .ValidateValidToDateFieldDisabled(true);

            #endregion

            #region Step 2

            var StartDate = DateTime.Now.AddDays(-10).ToString("dd'/'MM'/'yyyy");
            var CompletedDate = DateTime.Now.AddDays(-3).ToString("dd'/'MM'/'yyyy");

            //DateTime Test = DateTime.Now.AddDays(-3);

            var ValidToDate = DateTime.Now.AddDays(-3).AddYears(1).ToString("dd'/'MM'/'yyyy");

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(StartDate)
                .ValidateRequestedByLinkText("RecruitmentDocument User1")
                .ValidateCompletedDateFieldDisabled(false)
                .ClickRequestedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("RecruitmentDocumentUser2").TapSearchButton().SelectResultElement(_user2Id);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad();

            #endregion

            #region Step 3

            recruitmentDocumentsRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(recruitmentDocument1Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Requested");

            #endregion

            #region Step 4

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue(CompletedDate)
                .ValidateCompletedByLinkText("RecruitmentDocument User1")
                .ValidateValidFromDateFieldDisabled(false)
                .ValidateValidToDateFieldDisabled(false)
                .ClickCompletedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("RecruitmentDocumentUser2").TapSearchButton().SelectResultElement(_user2Id);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad();

            #endregion

            #region Step 5

            recruitmentDocumentsRecordPage
                .InsertReferenceNumber("123321")
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(recruitmentDocument1Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed");

            #endregion

            #region Step 6

            recruitmentDocumentsRecordPage
                .ValidateValidToDateField(ValidToDate);

            #endregion

            #region Step 7

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue("")
                .ValidateCompletedByLinkText("")
                .ValidateValidFromDateField("")
                .ValidateValidToDateField("")
                .ValidateValidFromDateFieldDisabled(true)
                .ValidateValidToDateFieldDisabled(true);

            #endregion

            #region Step 8

            recruitmentDocumentsRecordPage
                .InsertRequestedDateValue("")
                .ValidateRequestedByLinkText("")
                .ValidateCompletedDateFieldDisabled(true)
                .ValidateCompletedByFieldDisabled(true);

            #endregion

            #region Step 9

            recruitmentDocumentsRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(recruitmentDocument1Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue("01'/'01'/'2023")
                .InsertCompletedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertValidFromDateValue("03'/'01'/'2023")

                .InsertRequestedDateValue("")

                .ValidateRequestedByLinkText("")
                .ValidateCompletedDateField("")
                .ValidateCompletedByLinkText("")
                .ValidateValidFromDateField("")
                .ValidateValidToDateField("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3369")]
        [Description("Step(s) 2 to 11 from the original test method CDV6-14671 (using the 2nd recruitment document mentioned in the requirements)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestMethod009()
        {
            #region System User RecruitmentDocumentUser2

            var _user2Id = commonMethodsDB.CreateSystemUserRecord("RecruitmentDocumentUser2", "RecruitmentDocument", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Recruitment Item (Driving Licence)

            var compliancerecurrenceid = 6; //Annually
            var _staffRecruitmentItem1Id = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, "Driving Licence", new DateTime(2022, 1, 1), compliancerecurrenceid);

            #endregion

            #region Staff Recruitment Item (DBS Enhanced Level)

            var _staffRecruitmentItem2Id = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, "DBS Enhanced Level", new DateTime(2022, 1, 1), null);

            #endregion

            #region Recruitment Document

            var recruitmentDocument2Id = dbHelper.compliance.CreateCompliance(_teamId, _applicantId, "applicant", "Patrick " + _applicantLastName, _staffRecruitmentItem2Id, "StaffRecruitmentItem", "DBS Enhanced Level", 1);

            #endregion


            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login("RecruitmentDocumentUser1", "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + _applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(recruitmentDocument2Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRequestedByFieldDisabled(true)
                .ValidateCompletedDateFieldDisabled(true)
                .ValidateCompletedByFieldDisabled(true)
                .ValidateValidFromDateFieldDisabled(true)
                .ValidateValidToDateFieldDisabled(true);

            #endregion

            #region Step 2

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue("01'/'01'/'2023")
                .ValidateRequestedByLinkText("RecruitmentDocument User1")
                .ValidateCompletedDateFieldDisabled(false)
                .ClickRequestedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("RecruitmentDocumentUser2").TapSearchButton().SelectResultElement(_user2Id);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad();

            #endregion

            #region Step 3

            recruitmentDocumentsRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(recruitmentDocument2Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Requested");

            #endregion

            #region Step 4

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue("02'/'01'/'2023")
                .ValidateCompletedByLinkText("RecruitmentDocument User1")
                .ValidateValidFromDateFieldDisabled(false)
                .ValidateValidToDateFieldDisabled(false)
                .ClickCompletedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").TypeSearchQuery("RecruitmentDocumentUser2").TapSearchButton().SelectResultElement(_user2Id);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad();

            #endregion

            #region Step 5

            recruitmentDocumentsRecordPage
                .InsertReferenceNumber("123321")
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(recruitmentDocument2Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed");

            #endregion

            #region Step 6

            recruitmentDocumentsRecordPage
                .ValidateValidToDateField("");

            #endregion

            #region Step 7

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue("")
                .ValidateCompletedByLinkText("")
                .ValidateValidFromDateField("")
                .ValidateValidToDateField("")
                .ValidateValidFromDateFieldDisabled(true)
                .ValidateValidToDateFieldDisabled(true);

            #endregion

            #region Step 8

            recruitmentDocumentsRecordPage
                .InsertRequestedDateValue("")
                .ValidateRequestedByLinkText("")
                .ValidateCompletedDateFieldDisabled(true)
                .ValidateCompletedByFieldDisabled(true);

            #endregion

            #region Step 11

            recruitmentDocumentsRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(recruitmentDocument2Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue("01'/'01'/'2023")
                .InsertCompletedDateValue("02'/'01'/'2023")
                .InsertValidFromDateValue("03'/'01'/'2023")
                .InsertValidToDateValue("02'/'01'/'2024")

                .InsertRequestedDateValue("")

                .ValidateRequestedByLinkText("")
                .ValidateCompletedDateField("")
                .ValidateCompletedByLinkText("")
                .ValidateValidFromDateField("")
                .ValidateValidToDateField("");

            #endregion

        }

        #endregion





    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for
    /// </summary>
    [TestClass]
    public class RecruitmentDocuments_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _applicantId;
        private string _teamName;
        private string _applicantFullName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _loginUsername = "Login_User_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        internal Guid _staffRecruitmentItemId;
        private string _staffRecruitmentItemName;
        private Guid _defaultCareProviderStaffRoleTypeid;
        private string tenantName;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CareProviders";
                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Create default system user

                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "Login_", "Automation_User", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region Care provider staff role type

                _defaultCareProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Default CPSRT", null, null, new DateTime(2021, 1, 1), "Default CPSRT ...");

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-20846

        [TestProperty("JiraIssueID", "ACC-3500")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application" +
                     "Create Recruitment Documents and fill different diffrent fields and verify Recruitment Documents Satus after saving record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestCases001()
        {
            #region Staff Recruitment Item

            _staffRecruitmentItemName = "CDV6_20846_Item_1_" + currentDateTime;
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Create Applicant 

            var applicantFirstName = "CDV6_20846_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #endregion

            #region Step 3

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName(_teamName)
                .ValidateStatusSelectedText("Outstanding")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Outstanding");

            #endregion

            #region Step 4

            recruitmentDocumentsRecordPage
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .InsertRequestedDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Requested");

            #endregion

            #region Step 5

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .InsertRequestedDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertReferenceNumber("11")
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Completed");

            #endregion

            #region Step 6

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .InsertRequestedDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertReferenceNumber("20846")
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Completed");

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue("")
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Requested");

            #endregion

            #region Step 7

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .InsertRequestedDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertRefereeName("Automation")
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Completed");

            recruitmentDocumentsRecordPage
                .InsertRequestedDateValue("")
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Outstanding");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23666

        [TestProperty("JiraIssueID", "ACC-3501")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Recruitment Documents " +
                     "Open Existing Recruitment Document and validate Status and mandatory Fields." +
                     "Create new Recruitment Document and validate status and mandatory fields.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestCases002()
        {
            #region Create Applicant 

            var applicantFirstName = "CDV6_23758_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "RI_1_" + commonMethodsHelper.GetCurrentDateTimeString();
            int? _complianceRecurrenceId = 3; //Monthly
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2021, 1, 1), _complianceRecurrenceId);

            var _staffRecruitmentItemName2 = "RI_2_" + commonMethodsHelper.GetCurrentDateTimeString();
            var _staffRecruitmentItemId2 = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2021, 1, 1), _complianceRecurrenceId);

            #endregion

            #region Compliance 

            DateTime? requesteddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-13);
            DateTime? completeddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            DateTime? validfromdate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            DateTime? validtodate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);

            var compliance1Id = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1, "1A", requesteddate, completeddate, validfromdate, validtodate, _defaultLoginUserID, _defaultLoginUserID);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(compliance1Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName(_teamName)
                .ValidateComplianceItemName(_staffRecruitmentItemName)
                .ValidateStatusSelectedText("Expired")
                .ClickBackButton();

            #endregion

            #region Step 3

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName(_teamName)
                .ValidateStatusSelectedText("Outstanding")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName2)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName2)
                .InsertRequestedDateValue(DateTime.Now.Date.AddDays(-10).ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(DateTime.Now.Date.AddDays(-5).ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDateValue(DateTime.Now.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .InsertReferenceNumber("11")
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Expired")
                .ClickBackButton();

            #endregion

            #region Step 4

            var complicanceId2 = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName2).FirstOrDefault();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(complicanceId2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertValidToDateValue(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Completed")
                .ClickBackButton();

            #endregion

            #region Step 5

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(complicanceId2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertValidToDateValue(DateTime.Now.Date.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton().WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Completed");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3502")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Recruitment Documents " +
                     "Open Existing Recruitment Document and validate Status." +
                     "Select Options from System view and validate record status." +
                     "Validate Field disable or not and alert message.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        public void RecruitmentDocuments_UITestCases003()
        {
            #region Create Applicant 

            var applicantFirstName = "CDV6_23759_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "RI_1_" + commonMethodsHelper.GetCurrentDateTimeString();
            int? _complianceRecurrenceId = 3; //Monthly
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2021, 1, 1), _complianceRecurrenceId);

            var _staffRecruitmentItemName2 = "RI_2_" + commonMethodsHelper.GetCurrentDateTimeString();
            var _staffRecruitmentItemId2 = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2021, 1, 1), _complianceRecurrenceId);

            #endregion

            #region Compliance 

            DateTime? requesteddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-13);
            DateTime? completeddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            DateTime? validfromdate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            DateTime? validtodate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);

            var compliance1Id = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1, "1A", requesteddate, completeddate, validfromdate, validtodate, _defaultLoginUserID, _defaultLoginUserID);

            requesteddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            completeddate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            validfromdate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            validtodate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7);

            var compliance2Id = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId2, "StaffRecruitmentItem", _staffRecruitmentItemName, 1, "2A", requesteddate, completeddate, validfromdate, validtodate, _defaultLoginUserID, _defaultLoginUserID);

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(compliance2Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ClickBackButton();

            #endregion

            #region Step 7 & 8

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .SelectView("Expired")
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance1Id.ToString())
                .SelectView("Completed")
                .ValidateRecruitmentDocumentsRecordIsPresent(compliance2Id.ToString());

            #endregion

            #region Step 9

            recruitmentDocumentsPage
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRegardingApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName(_teamName)
                .ValidateStatusSelectedText("Outstanding")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName2)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateComplianceItemName(_staffRecruitmentItemName2)
                .InsertReferenceNumber("23759")

                .InsertRequestedDateValue(DateTime.Now.Date.AddDays(-5).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidFromDateFieldDisabled(true)
                .ValidateValidToDateFieldDisabled(true)

                .InsertCompletedDateValue(DateTime.Now.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .ValidateValidToDate_MandatoryFieldVisibility(true)
                .ValidateValidFromDateFieldDisabled(false)
                .ValidateValidToDateFieldDisabled(false);

            #endregion

            #region Step 10

            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue(DateTime.Now.Date.AddDays(3).ToString("dd'/'MM'/'yyyy"));

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Completed Date cannot be a future date")
                .TapCloseButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3701

        [TestProperty("JiraIssueID", "ACC-1159")]
        [Description("All test steps automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Recruitment Documents Management")]
        public void RecruitmentDocumentManagement_UITestCases001()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleName = "Junior Producer";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Cook ...");

            #endregion

            #region Employment Contract Type

            commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ECT_1159", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_1159_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_1159_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_1159_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item


            _staffRecruitmentItemName = "SRI_1159";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecReq_1159";
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Rocky";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents
            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);
            var complianceId = complianceRecords[0];

            #endregion

            #region Requirement Last Chased Outcome

            var requirementLastChasedOutcomeId = commonMethodsDB.CreateRequirementLastChasedOutcome("Default RLCO Outcome", _careProviders_TeamId, new DateTime(2020, 1, 1), null);

            #endregion


            #region Step 1 to 3

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(complianceId.ToString());

            #endregion

            #region Step 4

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad();

            recruitmentDocumentsManagementPage
                .WaitForRecruitmentDocumentsManagementAreaToLoadInsideRecruitmentDocument()
                .ClickNewRecordButton();

            #endregion

            #region Step 5

            //CREATING RECORD 1
            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertChasedDateValue("01/08/2020")
                .ClickOutcomeFieldLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default RLCO Outcome", requirementLastChasedOutcomeId);

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickChasedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord(_loginUsername, _defaultLoginUserID);

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickSaveAndCloseButton();

            recruitmentDocumentsManagementPage
                .WaitForRecruitmentDocumentsManagementAreaToLoadInsideRecruitmentDocument();

            var recruitmentDocumentManagement1Id = dbHelper.complianceManagement.GetByComplienceId(complianceId).First();


            //CREATING RECORD 2
            recruitmentDocumentsManagementPage
                .ClickNewRecordButton();

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertChasedDateValue("01/02/2020")
                .ClickOutcomeFieldLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default RLCO Outcome", requirementLastChasedOutcomeId);

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickChasedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord(_loginUsername, _defaultLoginUserID);

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickSaveAndCloseButton();

            recruitmentDocumentsManagementPage
                .WaitForRecruitmentDocumentsManagementAreaToLoadInsideRecruitmentDocument();

            var recruitmentDocumentManagement2Id = dbHelper.complianceManagement.GetByComplienceId(complianceId).Where(c => c != recruitmentDocumentManagement1Id).First();

            //CREATING RECORD 3
            recruitmentDocumentsManagementPage
                .ClickNewRecordButton();

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertChasedDateValue("01/05/2020")
                .ClickOutcomeFieldLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default RLCO Outcome", requirementLastChasedOutcomeId);

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickChasedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord(_loginUsername, _defaultLoginUserID);

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickSaveAndCloseButton();

            recruitmentDocumentsManagementPage
                .WaitForRecruitmentDocumentsManagementAreaToLoadInsideRecruitmentDocument();

            var recruitmentDocumentManagement3Id = dbHelper.complianceManagement.GetByComplienceId(complianceId).Where(c => c != recruitmentDocumentManagement1Id && c != recruitmentDocumentManagement2Id).First();

            recruitmentDocumentsManagementPage
                .ValidateCellText(1, 2, "01/08/2020")
                .ValidateCellText(2, 2, "01/05/2020")
                .ValidateCellText(3, 2, "01/02/2020");

            #endregion

            #region Step 6

            recruitmentDocumentsManagementPage
                .OpenRecord(recruitmentDocumentManagement3Id.ToString());

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .InsertChasedDateValue("01/10/2020")
                .ClickOutcomeFieldLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default RLCO Outcome", requirementLastChasedOutcomeId);

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickChasedByLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord(_loginUsername, _defaultLoginUserID);

            recruitmentDocumentManagementRecordPage
                .WaitForRecruitmentDocumentManagementRecordPageToLoad()
                .ClickSaveAndCloseButton();

            recruitmentDocumentsManagementPage
                .WaitForRecruitmentDocumentsManagementAreaToLoadInsideRecruitmentDocument()
                .ValidateCellText(1, 2, "01/10/2020")
                .ValidateCellText(2, 2, "01/08/2020")
                .ValidateCellText(3, 2, "01/02/2020");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3745

        [TestProperty("JiraIssueID", "ACC-3744")]
        [Description("All test steps automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Advanced Search")]
        public void RecruitmentDocument_UITestCases001()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleName = "Media Director";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Cook ...");

            #endregion

            #region Employment Contract Type

            commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ECT_3744", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3744_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3744_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_3744_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI_3744";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });


            var _recruitmentRequirementName = "RecReq_3744";
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Rocky";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);
            var complianceId = complianceRecords[0];

            #endregion

            #region Requirement Last Chased Outcome

            var requirementLastChasedOutcomeId = commonMethodsDB.CreateRequirementLastChasedOutcome("Default RLCO Outcome", _careProviders_TeamId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Recruitment Document Management

            DateTime chasedDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var recruitmentDocumentManagementId = dbHelper.complianceManagement.CreateComplianceManagement(complianceId, chasedDate, requirementLastChasedOutcomeId, _defaultLoginUserID, _careProviders_TeamId);

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            #endregion

            #region Step 3

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Recruitment Documents")
                .SelectFilter("1", "Last Chased Date")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", chasedDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(complianceId.ToString());

            #endregion

            #region Step 4

            advanceSearchPage
                .OpenRecord(complianceId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoadFromAdvancedSearch();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3770

        [TestProperty("JiraIssueID", "ACC-1179")]
        [Description("All test steps automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Applicant Dashboard")]
        public void RecruitmentDocument_UITestCases002()
        {
            #region Care provider staff role type
            dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var _careProviderStaffRoleName = "Focus Puller";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Cook ...");

            #endregion

            #region Employment Contract Type

            commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ECT_1179", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_1179_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            foreach (var trsId in dbHelper.trainingRequirementSetup.GetByAllRoles(true))
                dbHelper.trainingRequirementSetup.UpdateAllRoles(trsId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_1179_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_1179_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI_1179";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrsId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrsId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });


            var _recruitmentRequirementName = "RecReq_1179";
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Rocky";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);
            var complianceId = complianceRecords[0];

            #endregion


            #region Step 1 to 5 

            /*NOTE: the creation of the applicant and role application are performed in the setup phase. Therefore the test steps 1 to 5 will be ingnored*/

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateRoleApplicationTitleText(1, "Focus Puller")
                .ValidateInductionReadinessPercentageText(1, "0%")
                .ValidateFullyAcceptedPercentageText(1, "0%");

            #endregion

            #region Step 6

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            #endregion

            #region Step 7

            recruitmentDocumentsPage
                .OpenRecruitmentDocumentsRecord(complianceId.ToString());

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertReferenceNumber("1A")
                .InsertRequestedDateValue(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateRoleApplicationTitleText(1, "Focus Puller")
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%");

            #endregion

            #region Step 8

            applicantDashboardPage
                .ClickProceedToInductionButton(1);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapCancelButton();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateRoleApplicationNotVisible(1);

            #endregion

            #region Step 9

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad();

            var newSystemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());

            systemUsersPage
                .InsertName(_applicantFullName)
                .ClickSearchButton()
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(newSystemUserId);

            #endregion

            #region Step 10

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDocumentsSubPage();

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad()
                .ValidateRecordVisible(complianceId.ToString());

            #endregion

            #region Step 11

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString());

            #endregion

            #region Step 12

            var contracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(newSystemUserId);
            Assert.AreEqual(1, contracts.Count);
            var contractId = contracts[0];

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(contractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateRole_LinkField("Focus Puller")
                .ValidateType_LinkField("Contracted")
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            #endregion

            #region Step 13

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            var trainings = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(newSystemUserId);
            Assert.AreEqual(1, trainings.Count);
            var trainingId = trainings[0];

            systemUserTrainingPage
                .OpenRecord(trainingId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingItemLinkFieldText("STI_1179_A")
                .ValidateStatusFieldSelectedText("Outstanding");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3739

        [TestProperty("JiraIssueID", "ACC-3793")]
        [Description("Test automation for original Jira test ACC-3737")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        public void RewordingFields_UITestCases001()
        {
            #region Applicant

            var applicantFirstName = "Rocky";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Step 2 and Step 3

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickNewRecordButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusLabel("Progress towards induction status [%]")
                .ValidateProgressTowardsFullyAcceptedLabel("Progress towards fully accepted [%]");

            #endregion

            #region Step 7 and Step 12

            roleApplicationRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateVariationFieldContainsOption("Ad-hoc - additional requirement")
                .ValidateVariationFieldContainsOption("Override - doc not required");

            #endregion

            #region Step 9

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Override - doc not required")
                .ValidateReferenceNumberFieldVisible(false)
                .ValidateRefereeNameFieldVisible(false)
                .ValidateRefereePhoneFieldVisible(false)
                .ValidateRefereeAddressFieldVisible(false)
                .ValidateRefereeEmailFieldVisible(false)
                .ValidateRequestedDateFieldVisible(false)
                .ValidateRequestedByLookupFieldVisible(false)
                .ValidateCompletedDateFieldVisible(false)
                .ValidateCompletedByLookupFieldVisible(false)
                .ValidateValidFromDateFieldVisible(false)
                .ValidateValidToDateFieldVisible(false);

            #endregion

            #region Step 10

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateOverrideDetailsSectionFieldsVisible(true);

            #endregion

            #region Step 13

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Ad-hoc - additional requirement")
                .ValidateReferenceNumberFieldVisible(true)
                .ValidateRefereeNameFieldVisible(true)
                .ValidateRefereePhoneFieldVisible(true)
                .ValidateRefereeAddressFieldVisible(true)
                .ValidateRefereeEmailFieldVisible(true)
                .ValidateRequestedDateFieldVisible(true)
                .ValidateRequestedByLookupFieldVisible(true)
                .ValidateCompletedDateFieldVisible(true)
                .ValidateCompletedByLookupFieldVisible(true)
                .ValidateValidFromDateFieldVisible(true)
                .ValidateValidToDateFieldVisible(true);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateOverrideDetailsSectionFieldsVisible(false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3794")]
        [Description("Test automation for original Jira test ACC-3737")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        public void OverrideAndAdHocRequirement_UITestCases001()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleName = "CPSR 1";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), _careProviderStaffRoleName);

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3744_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3737_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_3737_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI3737_A";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            var _staffRecruitmentItemName2 = "SRI3737_B";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2023, 1, 1));

            var _staffRecruitmentItemName3 = "SRI3737_C";
            var _staffRecruitmentItemId3 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName3, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup
            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            foreach (var trsId in dbHelper.trainingRequirementSetup.GetByAllRoles(true))
                if (!trsId.Equals(null))
                    dbHelper.trainingRequirementSetup.UpdateAllRoles(trsId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });


            var _recruitmentRequirementName1 = "RR3737_A";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName1, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            var _recruitmentRequirementName2 = "RR3737_B";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            var _recruitmentRequirementName3 = "RR3737_C";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName3, _staffRecruitmentItemId3, "staffrecruitmentitem", _staffRecruitmentItemName3, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Max";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            dbHelper.applicant.UpdateAvailableFrom(_applicantId, new DateTime(2020, 1, 1));

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(3, complianceRecords.Count);
            var complianceRecordA = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId);
            var complianceId1 = complianceRecordA[0];
            dbHelper.compliance.UpdateCompliance(complianceId1, 1, "closure1", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID);

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("33")
                .ValidateProgressTowardsFullyAcceptedFieldValue("25");

            roleApplicationRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Override - doc not required")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName2)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId2.ToString())
                .SelectResultElement(_staffRecruitmentItemId2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertOverrideReason("Test 3737 " + currentDateTime)
                .ClickSaveButton();

            recruitmentDocumentsRecordPage
                .WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Completed")
                .ValidateVariationSelectedText("Override - doc not required")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("50")
                .ValidateProgressTowardsFullyAcceptedFieldValue("33");

            #endregion

            #region Step 14 and Step 16

            roleApplicationRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Ad-hoc - additional requirement")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName3)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId3.ToString())
                .SelectResultElement(_staffRecruitmentItemId3.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickRequestedDateCalendar();

            calendarDatePicker
                .WaitForCalendarPickerPopupToLoad()
                .SelectCalendarDate(DateTime.Now);

            System.Threading.Thread.Sleep(10000);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You added an ad-hoc document which will be shown as an additional requirement for every Application of this person. Please confirm the change.")
                .TapOKButton();

            recruitmentDocumentsRecordPage
                .WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Requested")
                .ValidateVariationSelectedText("Ad-hoc - additional requirement")
                .ClickBackButton();

            var adhocComplianceId = dbHelper.compliance.GetComplianceByRegardingIdAndAdditionalAttributeId(_applicantId, 2)[0]; //Ad-hoc - additional requirement = 2

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("50")
                .ValidateProgressTowardsFullyAcceptedFieldValue("25");

            #endregion

            #region Step 15

            roleApplicationRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(adhocComplianceId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertCompletedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertReferenceNumber("1234")
                .InsertRefereeName("complete adhoc doc ")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Completed");

            #endregion

            #region Step 18

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("50")
                .ValidateProgressTowardsFullyAcceptedFieldValue("50");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3771

        [TestProperty("JiraIssueID", "ACC-3819")]
        [Description("All test steps automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]
        public void RecruitmentDocument_UITestCases003()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleName = "Gaffer Assistant";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Cook ...");

            #endregion

            #region Employment Contract Type

            commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ECT_3819", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3819_A", new DateTime(2022, 1, 1));
            Guid staffTrainingItem2Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3819_B", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3819_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3819_B", staffTrainingItem2Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_3819_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);
            commonMethodsDB.CreateTrainingRequirement("TC_3819_B", _careProviders_TeamId, staffTrainingItem2Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItem1Name = "SRI_3819_A";
            var _staffRecruitmentItem2Name = "SRI_3819_B";
            var _staffRecruitmentItem1Id = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItem1Name, new DateTime(2023, 1, 1));
            var _staffRecruitmentItem2Id = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItem2Name, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RecReq_3819_A", _staffRecruitmentItem1Id, "staffrecruitmentitem", _staffRecruitmentItem1Name, false, 2, 3, 3, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RecReq_3819_B", _staffRecruitmentItem2Id, "staffrecruitmentitem", _staffRecruitmentItem2Name, false, 1, 3, 2, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Maria";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItem1Id);
            Assert.AreEqual(3, complianceRecords.Count);

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItem2Id);
            Assert.AreEqual(2, complianceRecords.Count);
            var compliance4Id = complianceRecords[0];

            #endregion


            #region Step 2 and 3

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldDisabled()
                .ValidateProgressTowardsFullyAcceptedFieldDisabled()
                .ValidateProgressTowardsInductionStatusFieldValue("0")
                .ValidateProgressTowardsFullyAcceptedFieldValue("0")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(compliance4Id.ToString());

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertReferenceNumber("1234")
                .InsertRequestedDateValue(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldDisabled()
                .ValidateProgressTowardsFullyAcceptedFieldDisabled()
                .ValidateProgressTowardsInductionStatusFieldValue("33")
                .ValidateProgressTowardsFullyAcceptedFieldValue("14")
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 4

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            var compliance6Id = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItem1Id, "StaffRecruitmentItem", _staffRecruitmentItem1Name, 1, "", null, null, null, null, _defaultLoginUserID, _defaultLoginUserID);

            roleApplicationsPage
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldDisabled()
                .ValidateProgressTowardsFullyAcceptedFieldDisabled()
                .ValidateProgressTowardsInductionStatusFieldValue("33")
                .ValidateProgressTowardsFullyAcceptedFieldValue("14")
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 5

            dbHelper.compliance.UpdateCompliance(compliance6Id, 1233, "Maria", currentDate, currentDate);

            roleApplicationsPage
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldDisabled()
                .ValidateProgressTowardsFullyAcceptedFieldDisabled()
                .ValidateProgressTowardsInductionStatusFieldValue("67")
                .ValidateProgressTowardsFullyAcceptedFieldValue("29")
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton();

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-3828")]
        [Description("All test steps automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        public void RecruitmentDocument_UITestCases004()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleName = "Gaffer Assistant";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Cook ...");

            #endregion

            #region Employment Contract Type

            commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ECT_3819", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3819_A", new DateTime(2022, 1, 1));
            Guid staffTrainingItem2Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3819_B", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3819_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3819_B", staffTrainingItem2Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_3819_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);
            commonMethodsDB.CreateTrainingRequirement("TC_3819_B", _careProviders_TeamId, staffTrainingItem2Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItem1Name = "SRI_3819_A";
            var _staffRecruitmentItem2Name = "SRI_3819_B";
            var _staffRecruitmentItem1Id = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItem1Name, new DateTime(2023, 1, 1));
            var _staffRecruitmentItem2Id = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItem2Name, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RecReq_3819_A", _staffRecruitmentItem1Id, "staffrecruitmentitem", _staffRecruitmentItem1Name, false, 2, 3, 3, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RecReq_3819_B", _staffRecruitmentItem2Id, "staffrecruitmentitem", _staffRecruitmentItem2Name, false, 1, 3, 2, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Maria";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItem1Id);
            Assert.AreEqual(3, complianceRecords.Count);
            var compliance1Id = complianceRecords[0];
            var compliance2Id = complianceRecords[1];
            var compliance3Id = complianceRecords[2];

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItem2Id);
            Assert.AreEqual(2, complianceRecords.Count);
            var compliance4Id = complianceRecords[0];
            var compliance5Id = complianceRecords[1];

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            dbHelper.compliance.UpdateCompliance(compliance1Id, 123, "Maria", currentDate, currentDate);
            dbHelper.compliance.UpdateCompliance(compliance2Id, 123, "Maria", currentDate, currentDate);
            dbHelper.compliance.UpdateCompliance(compliance4Id, 123, "Maria", currentDate, currentDate);
            dbHelper.compliance.UpdateCompliance(compliance5Id, 123, "Maria", currentDate, currentDate);

            #endregion


            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldDisabled()
                .ValidateProgressTowardsFullyAcceptedFieldDisabled()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("57")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .SelectRecord(compliance1Id.ToString())
                .SelectRecord(compliance4Id.ToString())
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("2 item(s) deleted.").TapOKButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldDisabled()
                .ValidateProgressTowardsFullyAcceptedFieldDisabled()
                .ValidateProgressTowardsInductionStatusFieldValue("67")
                .ValidateProgressTowardsFullyAcceptedFieldValue("29")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            #endregion

            #region Step 7

            applicantRecordPage
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(compliance2Id.ToString());

            var pastDate = currentDate.AddDays(-2);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(pastDate.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(pastDate.ToString("dd'/'MM'/'yyyy"))
                .InsertValidFromDateValue(pastDate.ToString("dd'/'MM'/'yyyy"))
                .InsertValidToDateValue(pastDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldDisabled()
                .ValidateProgressTowardsFullyAcceptedFieldDisabled()
                .ValidateProgressTowardsInductionStatusFieldValue("33")
                .ValidateProgressTowardsFullyAcceptedFieldValue("14")
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 8

            dbHelper.compliance.UpdateCompliance(compliance3Id, 123, "Maria", currentDate, currentDate);

            var compliance6Id = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItem1Id, "StaffRecruitmentItem", _staffRecruitmentItem1Name, 1, "1234", currentDate, currentDate, null, null, _defaultLoginUserID, _defaultLoginUserID);
            var compliance7Id = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItem1Id, "StaffRecruitmentItem", _staffRecruitmentItem1Name, 1, "1234", currentDate, currentDate, null, null, _defaultLoginUserID, _defaultLoginUserID);
            var compliance8Id = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItem2Id, "StaffRecruitmentItem", _staffRecruitmentItem2Name, 1, "1234", currentDate, currentDate, null, null, _defaultLoginUserID, _defaultLoginUserID);


            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldDisabled()
                .ValidateProgressTowardsFullyAcceptedFieldDisabled()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("71");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3840
        [TestProperty("JiraIssueID", "ACC-3839")]
        [Description("All test steps automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        public void RecruitmentDocument_UITestCases005()
        {
            #region Step 1 to Step 3

            #region Care provider staff role type

            var _careProviderStaffRoleName = "Head Gaffer";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Gaffer ...");

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3839_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3839_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_3839_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI_3839_A";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RecReq_3840_A", _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Ana";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId);
            Assert.AreEqual(1, complianceRecords.Count);
            var compliance1Id = complianceRecords[0];

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 123, "Ana", currentDate, currentDate);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(compliance1Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateVariationSelectedText("")
                .ValidateVariationFieldDoesNotContainOption("Ad-hoc - additional requirement");

            #endregion

            #region Step 4

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateVariationSelectedText("")
                .ValidateVariationFieldContainsOption("Ad-hoc - additional requirement");

            #endregion

            #region Step 5

            recruitmentDocumentsRecordPage
                .SelectVariationOption("Ad-hoc - additional requirement")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId.ToString())
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            System.Threading.Thread.Sleep(5000);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You added an ad-hoc document which will be shown as an additional requirement for every Application of this person. Please confirm the change.")
                .TapOKButton();

            recruitmentDocumentsRecordPage
                .WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Outstanding")
                .ValidateVariationSelectedText("Ad-hoc - additional requirement");

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "33%");

            #endregion

            #region Step 6

            var adhocComplianceId = dbHelper.compliance.GetComplianceByRegardingIdAndAdditionalAttributeId(_applicantId, 2)[0]; //Ad-hoc - additional requirement = 2

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(adhocComplianceId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertReferenceNumber("3840")
                .InsertRequestedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertRefereeName("complete adhoc doc from outstanding")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateStatusSelectedText("Completed");

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "67%");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3900
        [TestProperty("JiraIssueID", "ACC-3916")]
        [Description("Test steps 1 to 12 automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        public void RecruitmentDocument_UITestCases006()
        {
            #region Step 1 to Step 4

            #region Non-admin user

            var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First();
            var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First();
            var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First();
            var securityProfileId4 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First();
            var securityProfileId5 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)").First();
            var securityProfileId6 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Recruitment (Edit)").First();
            var securityProfileId8 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Training (Edit)").First();
            var securityProfileId9 = dbHelper.securityProfile.GetSecurityProfileByName("Training Setup (Edit)").First();
            var securityProfileId10 = dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)").First();
            var securityProfileId11 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First();
            var securityProfileId12 = dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access").First();
            var securityProfileId13 = dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First();
            var securityProfileId14 = dbHelper.securityProfile.GetSecurityProfileByName("Team Membership (Edit)").First();
            var securityProfileId15 = dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)").First();
            var securityProfileId16 = dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel").First();
            var securityProfileId17 = dbHelper.securityProfile.GetSecurityProfileByName("Fully Accept Recruitment Application").First();
            var securityProfileId18 = dbHelper.securityProfile.GetSecurityProfileByName("Set Override Recruitment Document Attribute").First();

            var securityProfilesList = new List<Guid> {securityProfileId1, securityProfileId2, securityProfileId3, securityProfileId4, securityProfileId5,
                securityProfileId6, securityProfileId8, securityProfileId9, securityProfileId10,
                securityProfileId11, securityProfileId12, securityProfileId13, securityProfileId14, securityProfileId15, securityProfileId16,
                securityProfileId17 };

            var NonAdminLoginUser = "NonAdminUser3889";
            var NonAdminUserId = commonMethodsDB.CreateSystemUserRecord(NonAdminLoginUser, "NonAdmin", "User3899", "Passw0rd_!",
                _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid, securityProfilesList);

            var securityProfileExists = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(NonAdminUserId, securityProfileId18).Any();
            if (securityProfileExists)
            {
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(dbHelper.userSecurityProfile.GetByUserIDAndProfileId(NonAdminUserId, securityProfileId18).First());
            }

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleName = "Assoc Head Nurse";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Assoc Nurse ...");

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3889_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3889_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_3889_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI_3889_A";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            var _staffRecruitmentItemName2 = "SRI_3889_B";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2023, 1, 1));

            var _staffRecruitmentItemName3 = "SRI_3889_C";
            var _staffRecruitmentItemId3 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName3, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RecReq_3889_A", _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "A3899";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateVariationFieldContainsOption("Ad-hoc - additional requirement")
                .ValidateVariationFieldContainsOption("Override - doc not required")
                .SelectVariationOption("Override - doc not required")
                .ValidateReferenceNumberFieldVisible(false)
                .ValidateRefereeNameFieldVisible(false)
                .ValidateRefereePhoneFieldVisible(false)
                .ValidateRefereeAddressFieldVisible(false)
                .ValidateRefereeEmailFieldVisible(false)
                .ValidateRequestedDateFieldVisible(false)
                .ValidateRequestedByLookupFieldVisible(false)
                .ValidateCompletedDateFieldVisible(false)
                .ValidateCompletedByLookupFieldVisible(false)
                .ValidateValidFromDateFieldVisible(false)
                .ValidateValidToDateFieldVisible(false);

            recruitmentDocumentsRecordPage
                .SelectVariationOption("Override - doc not required")
                .ValidateOverrideDetailsSectionFieldsVisible(true);

            recruitmentDocumentsRecordPage
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            System.Threading.Thread.Sleep(5000);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You are missing privilege to manage Override attribute")
                .TapCloseButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 5, 6

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .ClickSignOutButton();

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(NonAdminUserId, securityProfileId18);

            loginPage
                .GoToLoginPage()
                .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Override - doc not required")
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName2)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId2.ToString());

            System.Threading.Thread.Sleep(5000);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ValidateVariationSelectedText("Override - doc not required");
            #endregion

            #region Step 7

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            var _complianceDocumentId3 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId2, "StaffRecruitmentItem", _staffRecruitmentItemName2, 1, "3889", null, null, null, null, null, null);
            dbHelper.compliance.UpdateAdditionalAttributeId(_complianceDocumentId3, 1);
            dbHelper.compliance.UpdateOverrideDateField(_complianceDocumentId3, DateTime.Now);
            dbHelper.compliance.UpdateOverrideByField(_complianceDocumentId3, NonAdminUserId);

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(_complianceDocumentId3.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ValidateVariationSelectedText("Override - doc not required");

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.IsTrue(complianceRecords.Count >= 3);

            #endregion

            #region Step 8

            recruitmentDocumentsRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageDoesNotContainText(_staffRecruitmentItemName2)
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("0")
                .ValidateProgressTowardsFullyAcceptedFieldValue("0");

            #endregion

            #region Step 9

            roleApplicationRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            foreach (Guid complianceId in complianceRecords)
                dbHelper.compliance.UpdateCompliance(complianceId, 123, "3899", currentDate, currentDate);

            var _complianceDocumentId4a = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId3, "StaffRecruitmentItem", _staffRecruitmentItemName3, 1, "3889", null, null, null, null, null, null);
            dbHelper.compliance.UpdateAdditionalAttributeId(_complianceDocumentId4a, 1);
            dbHelper.compliance.UpdateOverrideDateField(_complianceDocumentId4a, DateTime.Now);
            dbHelper.compliance.UpdateOverrideByField(_complianceDocumentId4a, NonAdminUserId);
            var _complianceDocumentId4b = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId3, "StaffRecruitmentItem", _staffRecruitmentItemName3, 1, "3889", null, null, null, null, null, null);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad();

            complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.IsTrue(complianceRecords.Count >= 5);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageDoesNotContainText(_staffRecruitmentItemName2)
                .ValidateMessageDoesNotContainText(_staffRecruitmentItemName3)
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("50")
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .ClickSignOutButton();

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .SelectRecord(_complianceDocumentId4a.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageDoesNotContainText(_staffRecruitmentItemName2)
                .ValidateMessageDoesNotContainText(_staffRecruitmentItemName3)
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("50");

            #endregion

            #region Step 11, 12

            var _complianceDocumentId5 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1, "3889", null, null, null, null, null, null);
            dbHelper.compliance.UpdateAdditionalAttributeId(_complianceDocumentId5, 1);
            dbHelper.compliance.UpdateOverrideDateField(_complianceDocumentId5, DateTime.Now);
            dbHelper.compliance.UpdateOverrideByField(_complianceDocumentId5, _defaultLoginUserID);

            roleApplicationRecordPage
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("0")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(_complianceDocumentId5.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("")
                .InsertRequestedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Requested")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageDoesNotContainText(_staffRecruitmentItemName)
                .ValidateMessageDoesNotContainText(_staffRecruitmentItemName2)
                .ValidateMessageDoesNotContainText(_staffRecruitmentItemName3)
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("50");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3918")]
        [Description("Test steps 13 to 15 automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Advanced Search")]
        public void RecruitmentDocument_UITestCases007()
        {
            #region Step 13 to Step 15

            #region Care provider staff role type

            var _careProviderStaffRoleName = "Head Nurse";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Nurse ...");

            #endregion

            #region Staff Recruitment Item
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            _staffRecruitmentItemName = "SRI_3918_A";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RecReq_3918_A", _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "A3918";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Recruitment Documents")
                .ClickDeleteButton()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoadFromAdvancedSearch()
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .TapSearchButton()
                .SelectResultElement(_applicantId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoadFromAdvancedSearch()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            System.Threading.Thread.Sleep(2000);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoadFromAdvancedSearch()
                .SelectVariationOption("Override - doc not required")
                .InsertOverrideDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidatePageHeaderTitle(_staffRecruitmentItemName);

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.IsTrue(complianceRecords.Count >= 1);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecruitmentDocumentsRecordIsPresent(complianceRecords[0].ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Recruitment Documents")
                .WaitForAdvanceSearchPageToLoad()
                .SelectFilter("1", "Variation")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            var _additionalAttributeOptionSetId = dbHelper.optionSet.GetOptionSetIdByName("RecruitmentDocumentAdditionalAttribute")[0];
            var _overrideAdditionalAttributeId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(_additionalAttributeOptionSetId, "Override - doc not required")[0];

            lookupPopup.WaitForOptionSetLookupPopupToLoad().SelectResultElement(_overrideAdditionalAttributeId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(2000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(4)
                .ClickColumnHeader(4)
                .ValidateSearchResultRecordPresent(complianceRecords[0].ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3954

        [TestProperty("JiraIssueID", "ACC-3952")]
        [Description("All test steps automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        public void RecruitmentDocument_UITestCases008()
        {
            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ACC-3952", null, null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "ACC-3952_Role_Type";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3952_1_RI";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_ACC-3952_" + currentDateTime;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);

            #endregion            

            #region Create Role Applicant

            dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Steps

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId);
            var complianceId = complianceRecords[0];

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(complianceId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateRelatedItems_DocumentManagementIcon();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3972

        [TestProperty("JiraIssueID", "ACC-3970")]
        [Description("All test steps automated inside this method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        [TestProperty("Screen3", "System User Training")]
        public void RecruitmentDocument_UITestCases009()
        {
            #region Step 1, Step 2

            #region Care provider staff role type

            var _careProviderStaffRoleName = "Head Recruiter";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Recruiter ...");

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_3970_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3970_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            var _trainingCourseId = commonMethodsDB.CreateTrainingRequirement("TC_3970_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI_3970_A";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RecReq_3970_A", _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "A3970";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId);
            Assert.AreEqual(1, complianceRecords.Count);
            var complianceRecordId1 = complianceRecords[0];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceRecordId1.ToString(), 3, "Outstanding");

            #endregion

            #region Step 3
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 3970, "Referee", currentDate, currentDate);

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceRecordId1.ToString(), 3, "Completed");

            #endregion

            #region Step 4            

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ValidateRecruitmentStatusFieldAlertMessage("All recruitment requirements have been met for the status Induction.")
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Induction");

            #endregion

            #region Step 7

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateSystemUserFieldLinkText(_applicantFullName)
                .ClickSystemUserFieldLinkText();

            lookupViewPopup
                .WaitForLookupViewPopupToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateSystemUserRecordTitle(_applicantFullName);

            #endregion

            #region Step 5

            systemUserRecordPage
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRecruitmentStatusSelectedText("Induction")
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("50")
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:\r\n" +
                "STI_3970_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickBackButton();

            #endregion

            #region Step 6
            //After changing status from Induction to Fully Accepted, we cannot validate Recruitment Requirements (as they are satisfied to progress in Induction)
            #endregion

            #region Step 8, Step 9

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateEmploymentSubMenuItems();

            #endregion

            #region Step 10 to Step 12

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickTrainingSubPageLink();

            var newSystemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());
            var systemUserTrainings = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItem1Id, newSystemUserId);
            Assert.AreEqual(1, systemUserTrainings.Count);
            var systemUserTraining1Id = systemUserTrainings[0];

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(systemUserTraining1Id.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("TC_3970_A").TapSearchButton().SelectResultElement(_trainingCourseId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertTrainingCourseFinishDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .SelectOutcome("Pass")
                .InsertReferenceNumber("3970")
                .InsertNotes("Notes ...")
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .ValidateRecordCellText(systemUserTraining1Id.ToString(), 6, "Current");

            #endregion

            #region Step 13
            //Step 12 and Step 13 are the same.Completing of Training Requirements.
            #endregion

            #region Step 14

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Induction")
                .ValidateRecruitmentStatusFieldAlertMessage("All recruitment requirements have been met for the status Fully Accepted.");

            #endregion

            #region Step 15

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("100")
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateRecruitmentStatusSelectedText("Fully Accepted");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3867
        [TestProperty("JiraIssueID", "ACC-4027")]
        [Description("Test automation for original Jira test ACC-4011")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        public void RecruitmentDocument_UITestCases010()
        {
            #region Non-admin user
            var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First();
            var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First();
            var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First();
            var securityProfileId4 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First();
            var securityProfileId5 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)").First();
            var securityProfileId6 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Recruitment (Edit)").First();
            var securityProfileId7 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Training (Edit)").First();
            var securityProfileId8 = dbHelper.securityProfile.GetSecurityProfileByName("Training Setup (Edit)").First();
            var securityProfileId9 = dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)").First();
            var securityProfileId10 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First();
            var securityProfileId11 = dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access").First();
            var securityProfileId12 = dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First();
            var securityProfileId13 = dbHelper.securityProfile.GetSecurityProfileByName("Team Membership (Edit)").First();
            var securityProfileId14 = dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)").First();
            var securityProfileId15 = dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel").First();
            var securityProfileId16 = dbHelper.securityProfile.GetSecurityProfileByName("Fully Accept Recruitment Application").First();
            var securityProfileId17 = dbHelper.securityProfile.GetSecurityProfileByName("Set Override Recruitment Document Attribute").First();

            var securityProfilesList = new List<Guid> {securityProfileId1, securityProfileId2, securityProfileId3, securityProfileId4, securityProfileId5,
                securityProfileId6, securityProfileId7, securityProfileId8, securityProfileId9, securityProfileId10,
                securityProfileId11, securityProfileId12, securityProfileId13, securityProfileId14, securityProfileId15,
                securityProfileId16 };

            var NonAdminLoginUser = "NonAdminUser4011";
            var NonAdminUserId = commonMethodsDB.CreateSystemUserRecord(NonAdminLoginUser, "NonAdmin", "User3899", "Passw0rd_!",
                _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid, securityProfilesList);

            var securityProfileExists = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(NonAdminUserId, securityProfileId17).Any();
            if (securityProfileExists)
            {
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(dbHelper.userSecurityProfile.GetByUserIDAndProfileId(NonAdminUserId, securityProfileId17).First());
            }

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleName = "Adhoc Carer";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), _careProviderStaffRoleName);

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_4011_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_4011_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_4011_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI4011_A";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            var _staffRecruitmentItemName2 = "SRI4011_B";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2023, 1, 1));

            var _staffRecruitmentItemName3 = "SRI4011_C";
            var _staffRecruitmentItemId3 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName3, new DateTime(2023, 1, 1));

            #endregion

            #region 

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });


            var _recruitmentRequirementName1 = "RR4011_A";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName1, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            var _recruitmentRequirementName2 = "RR4011_B";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            var _recruitmentRequirementName3 = "RR4011_C";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName3, _staffRecruitmentItemId3, "staffrecruitmentitem", _staffRecruitmentItemName3, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Aaby";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            dbHelper.applicant.UpdateAvailableFrom(_applicantId, new DateTime(2020, 1, 1));

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(3, complianceRecords.Count);
            var complianceId1 = complianceRecords[0];
            dbHelper.compliance.UpdateCompliance(complianceId1, 1, "closure1", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID);

            #endregion

            #region Step 1 to Step 3

            loginPage
                .GoToLoginPage()
                .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("33")
                .ValidateProgressTowardsFullyAcceptedFieldValue("25");

            roleApplicationRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickAddButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateVariationFieldContainsOption("Ad-hoc - additional requirement")
                .ValidateVariationFieldContainsOption("Override - doc not required")
                .SelectVariationOption("Ad-hoc - additional requirement")
                .ValidateReferenceNumberFieldVisible(true)
                .ValidateRefereeNameFieldVisible(true)
                .ValidateRefereePhoneFieldVisible(true)
                .ValidateRefereeAddressFieldVisible(true)
                .ValidateRefereeEmailFieldVisible(true)
                .ValidateRequestedDateFieldVisible(true)
                .ValidateRequestedByLookupFieldVisible(true)
                .ValidateCompletedDateFieldVisible(true)
                .ValidateCompletedByLookupFieldVisible(true)
                .ValidateValidFromDateFieldVisible(true)
                .ValidateValidToDateFieldVisible(true)
                .ValidateOverrideDetailsSectionFieldsVisible(false)
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName2)
                .TapSearchButton()
                .ValidateResultElementPresent(_staffRecruitmentItemId2.ToString())
                .SelectResultElement(_staffRecruitmentItemId2.ToString());

            System.Threading.Thread.Sleep(5000);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You added an ad-hoc document which will be shown as an additional requirement for every Application of this person. Please confirm the change.")
                .TapOKButton();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Outstanding")
                .ValidateVariationSelectedText("Ad-hoc - additional requirement")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(4, recruitmentDocuments.Count);

            var adhocComplianceId = dbHelper.compliance.GetComplianceByRegardingIdAndAdditionalAttributeId(_applicantId, 2); //Ad-hoc - additional requirement = 2
            Assert.AreEqual(1, adhocComplianceId.Count);
            var adHocrecruitmentDocument1Id = adhocComplianceId[0];

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("33")
                .ValidateProgressTowardsFullyAcceptedFieldValue("20")
                .ClickBackButton();

            #endregion

            #region Step 4

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(adHocrecruitmentDocument1Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Requested");

            #endregion

            #region Step 5
            recruitmentDocumentsRecordPage
                .InsertCompletedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertReferenceNumber("3867")
                .ClickSaveButton()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ClickBackButton();

            #endregion

            #region Step 6
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("33")
                .ValidateProgressTowardsFullyAcceptedFieldValue("40");

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Induction requirements are unmet. Missing:\r\n" +
                "SRI4011_B - 1 item is not Complete\r\n" +
                "SRI4011_C - 1 item is not Complete\r\n\r\n" +
                "Fully Accepted requirements are unmet. Missing:\r\n" +
                "SRI4011_B - 1 item is not Complete\r\n" +
                "SRI4011_C - 1 item is not Complete\r\n" +
                "STI_4011_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickBackButton();

            #endregion

            #region Step 7
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(adHocrecruitmentDocument1Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("")
                .ClickSaveButton()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ClickBackButton();

            #endregion

            #region Step 8

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("67")
                .ValidateProgressTowardsFullyAcceptedFieldValue("50");

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Induction requirements are unmet. Missing:\r\n" +
                "SRI4011_C - 1 item is not Complete\r\n\r\n" +
                "Fully Accepted requirements are unmet. Missing:\r\n" +
                "SRI4011_C - 1 item is not Complete\r\n" +
                "STI_4011_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickBackButton();

            #endregion

            #region Step 9

            var adHocrecruitmentDocument2Id = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId3, "StaffRecruitmentItem", _staffRecruitmentItemName3, 1, "4011ADH", null, null, null, null, null, null, 2);
            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(5, recruitmentDocuments.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("67")
                .ValidateProgressTowardsFullyAcceptedFieldValue("40");

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Induction requirements are unmet. Missing:\r\n" +
                "SRI4011_C - 1 item is not Complete\r\n\r\n" +
                "Fully Accepted requirements are unmet. Missing:\r\n" +
                "SRI4011_C - 2 items are not Complete\r\n" +
                "STI_4011_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickBackButton();

            #endregion

            #region Step 10

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(adHocrecruitmentDocument2Id.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .InsertRequestedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ClickBackButton();


            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("67")
                .ValidateProgressTowardsFullyAcceptedFieldValue("60");

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Induction requirements are unmet. Missing:\r\n" +
                "SRI4011_C - 1 item is not Complete\r\n\r\n" +
                "Fully Accepted requirements are unmet. Missing:\r\n" +
                "SRI4011_C - 1 item is not Complete\r\n" +
                "STI_4011_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickBackButton();
            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4028")]
        [Description("Test automation for original Jira test ACC-4011")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Advanced Search")]
        [TestProperty("Screen3", "Role Applications")]
        public void RecruitmentDocument_UITestCases011()
        {
            #region Step 13

            #region Care provider staff role type

            var _careProviderStaffRoleName = "Adhoc Care Provider";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), _careProviderStaffRoleName);

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "STI_4028_A", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_4028_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_4028_A", _careProviders_TeamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "SRI4028_A";
            _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            var _staffRecruitmentItemName2 = "SRI4028_B";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2023, 1, 1));

            var _staffRecruitmentItemName3 = "SRI4028_C";
            var _staffRecruitmentItemId3 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName3, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });


            var _recruitmentRequirementName1 = "RR4028_A";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName1, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            var _recruitmentRequirementName2 = "RR4028_B";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            var _recruitmentRequirementName3 = "RR4028_C";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName3, _staffRecruitmentItemId3, "staffrecruitmentitem", _staffRecruitmentItemName3, false, 1, 3, 1, 3, DateTime.Now, null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Aaby2";
            var applicantLastName = currentDateTime;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            dbHelper.applicant.UpdateAvailableFrom(_applicantId, new DateTime(2020, 1, 1));

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(3, complianceRecords.Count);
            var complianceId1 = complianceRecords[0];
            dbHelper.compliance.UpdateCompliance(complianceId1, 1, "closure1", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID);

            var adHocrecruitmentDocumentId = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId3, "StaffRecruitmentItem", _staffRecruitmentItemName3, 1, "4011ADH", null, null, null, null, null, null, 2);
            var nonAdHocrecruitmentDocumentId = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId3, "StaffRecruitmentItem", _staffRecruitmentItemName3, 1, "4011ADH", null, null, null, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("33")
                .ValidateProgressTowardsFullyAcceptedFieldValue("20");

            roleApplicationRecordPage
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(5, recruitmentDocuments.Count);

            var adhocComplianceId = dbHelper.compliance.GetComplianceByRegardingIdAndAdditionalAttributeId(_applicantId, 2); //Ad-hoc - additional requirement = 2
            Assert.AreEqual(1, adhocComplianceId.Count);
            dbHelper.compliance.UpdateCompliance(adHocrecruitmentDocumentId, 4028, "4028AdHoc", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID);
            dbHelper.compliance.UpdateCompliance(nonAdHocrecruitmentDocumentId, 4028, "4028", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID);

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("67")
                .ValidateProgressTowardsFullyAcceptedFieldValue("60");

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Induction requirements are unmet. Missing:\r\n" +
                "SRI4028_B - 1 item is not Complete\r\n\r\n" +
                "Fully Accepted requirements are unmet. Missing:\r\n" +
                "SRI4028_B - 1 item is not Complete\r\n" +
                "STI_4028_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickBackButton();

            #endregion

            #region Step 14

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .SelectRecord(adHocrecruitmentDocumentId.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("67")
                .ValidateProgressTowardsFullyAcceptedFieldValue("50");

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Induction requirements are unmet. Missing:\r\n" +
                "SRI4028_B - 1 item is not Complete\r\n\r\n" +
                "Fully Accepted requirements are unmet. Missing:\r\n" +
                "SRI4028_B - 1 item is not Complete\r\n" +
                "STI_4028_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickBackButton();

            #endregion

            #region Step 15

            var adHocrecruitmentDocumentId2 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId2, "StaffRecruitmentItem", _staffRecruitmentItemName2, 1, "4028ADHB", null, null, null, null, null, null, 2);
            dbHelper.compliance.UpdateCompliance(adHocrecruitmentDocumentId2, 4028, "4028AdHoc2", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("67")
                .ValidateProgressTowardsFullyAcceptedFieldValue("60");

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Induction requirements are unmet. Missing:\r\n" +
                "SRI4028_B - 1 item is not Complete\r\n\r\n" +
                "Fully Accepted requirements are unmet. Missing:\r\n" +
                "SRI4028_B - 1 item is not Complete\r\n" +
                "STI_4028_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(adHocrecruitmentDocumentId2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateVariationSelectedText("")
                .ClickBackButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("75");

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Fully Accepted requirements are unmet. Missing:\r\n" +
                "STI_4028_A - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickBackButton();

            #endregion

            #region Step 16 to Step 18

            #region Applicant

            var applicantFirstName2 = "A4028";
            var applicantLastName2 = currentDateTime;
            var _applicantId2 = dbHelper.applicant.CreateApplicant(applicantFirstName2, applicantLastName2, _careProviders_TeamId);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Recruitment Documents")
                .ClickDeleteButton()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoadFromAdvancedSearch()
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("*" + applicantLastName2 + "*")
                .TapSearchButton()
                .SelectResultElement(_applicantId2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoadFromAdvancedSearch()
                .ClickComplianceItemLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffRecruitmentItemName)
                .TapSearchButton()
                .SelectResultElement(_staffRecruitmentItemId.ToString());

            System.Threading.Thread.Sleep(2000);

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoadFromAdvancedSearch()
                .SelectVariationOption("Ad-hoc - additional requirement")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You added an ad-hoc document which will be shown as an additional requirement for every Application of this person. Please confirm the change.")
                .TapOKButton();

            recruitmentDocumentsRecordPage
                .WaitForRecordToBeSaved()
                .ValidatePageHeaderTitle(_staffRecruitmentItemName);

            var complianceRecords2 = dbHelper.compliance.GetComplianceByRegardingId(_applicantId2);
            Assert.IsTrue(complianceRecords.Count >= 1);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName2 + "*")
                .OpenApplicantRecord(_applicantId2.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecruitmentDocumentsRecordIsPresent(complianceRecords2[0].ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Recruitment Documents")
                .WaitForAdvanceSearchPageToLoad()
                .SelectFilter("1", "Variation")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            var _additionalAttributeOptionSetId = dbHelper.optionSet.GetOptionSetIdByName("RecruitmentDocumentAdditionalAttribute")[0];
            var _adHocAdditionalAttributeId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(_additionalAttributeOptionSetId, "Ad-hoc - additional requirement")[0];

            lookupPopup.WaitForOptionSetLookupPopupToLoad().SelectResultElement(_adHocAdditionalAttributeId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(2000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ClickColumnHeader(4)
                .ClickColumnHeader(4)
                .ValidateSearchResultRecordPresent(complianceRecords2[0].ToString());


            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3987

        [TestProperty("JiraIssueID", "ACC-4233")]
        [Description("Step(s) 3 to 5 from the original test method ACC-3986")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        public void RecruitmentDocument_ACC_3986_UITestCases001()
        {
            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ACC-3987", null, null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "Carer_Role";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName1 = "Proof of ID Doc";
            var _staffRecruitmentItemId1 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName1, new DateTime(2020, 1, 1));

            var _staffRecruitmentItemName2 = "Photo Uploaded Doc";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2020, 1, 1));

            var _staffRecruitmentItemName3 = "Right to Work Doc";
            var _staffRecruitmentItemId3 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName3, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName1, _staffRecruitmentItemId1, "staffrecruitmentitem", _staffRecruitmentItemName1, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName3, _staffRecruitmentItemId3, "staffrecruitmentitem", _staffRecruitmentItemName3, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Create First Applicant 

            var applicantFirstName = "ACC-4233_1_" + currentDateTime;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            // Step 1 & 2 already covered in another test

            #region Steps 3

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var complianceId1_Applicant1 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId1, "StaffRecruitmentItem", _staffRecruitmentItemName1, 1);

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant1.ToString(), 3, "Outstanding");

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceItems.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Create Role Applicant

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Compliance Items

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId1);
            Assert.AreEqual(1, complianceRecords.Count);

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(3, complianceItems.Count);

            var complianceId2_Applicant1 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId2);
            var complianceId3_Applicant1 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId3);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant1.ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceId2_Applicant1[0].ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceId3_Applicant1[0].ToString(), 3, "Outstanding");

            #endregion

            #region Steps 4

            #region Create Second Applicant 

            var applicantFirstName2 = "ACC-4233_2_" + currentDateTime;
            var _applicantId2 = dbHelper.applicant.CreateApplicant(applicantFirstName2, "Applicant_02", _careProviders_TeamId);
            var _applicantFullName2 = (string)dbHelper.applicant.GetApplicantByID(_applicantId2, "fullname")["fullname"];

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName2)
                .OpenApplicantRecord(_applicantId2.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var complianceId2_Applicant2 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId2, "applicant", _applicantFullName2, _staffRecruitmentItemId2, "StaffRecruitmentItem", _staffRecruitmentItemName2, 1);
            dbHelper.compliance.UpdateCompliance(complianceId2_Applicant2, 1233, "Request", DateTime.Now, _defaultLoginUserID, null, null, null, null);

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId2_Applicant2.ToString(), 3, "Requested");

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId2);
            Assert.AreEqual(1, complianceItems.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Create Role Applicant

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId2, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName2, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Compliance Items

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId2, _staffRecruitmentItemId2);
            Assert.AreEqual(1, complianceRecords.Count);

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId2);
            Assert.AreEqual(3, complianceItems.Count);

            var complianceId1_Applicant2 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId2, _staffRecruitmentItemId1);
            var complianceId3_Applicant2 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId2, _staffRecruitmentItemId3);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant2[0].ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceId2_Applicant2.ToString(), 3, "Requested")
                .ValidateRecordCellContent(complianceId3_Applicant2[0].ToString(), 3, "Outstanding");

            #endregion

            #region Steps 5

            #region Create Third Applicant 

            var applicantFirstName3 = "ACC-4233_3_" + currentDateTime;
            var _applicantId3 = dbHelper.applicant.CreateApplicant(applicantFirstName3, "Applicant_03", _careProviders_TeamId);
            var _applicantFullName3 = (string)dbHelper.applicant.GetApplicantByID(_applicantId3, "fullname")["fullname"];

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName3)
                .OpenApplicantRecord(_applicantId3.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var complianceId3_Applicant3 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId3, "applicant", _applicantFullName3, _staffRecruitmentItemId3, "StaffRecruitmentItem", _staffRecruitmentItemName3, 1);
            dbHelper.compliance.UpdateCompliance(complianceId3_Applicant3, 1233, "Completed", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID, null, null);

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId3_Applicant3.ToString(), 3, "Completed");

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId3);
            Assert.AreEqual(1, complianceItems.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Create Role Applicant

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId3, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName3, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Compliance Items

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId3, _staffRecruitmentItemId3);
            Assert.AreEqual(1, complianceRecords.Count);

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId3);
            Assert.AreEqual(3, complianceItems.Count);

            var complianceId1_Applicant3 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId3, _staffRecruitmentItemId1);
            var complianceId2_Applicant3 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId3, _staffRecruitmentItemId2);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant3[0].ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceId2_Applicant3[0].ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceId3_Applicant3.ToString(), 3, "Completed");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4234")]
        [Description("Step(s) 6 to 9 from the original test method ACC-3986")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        public void RecruitmentDocument_ACC_3986_UITestCases002()
        {
            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ACC-3987", null, null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "Carer_Role_Licence";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            string careProviderRoleName2 = "Carer_Role_Proof";
            var _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName2, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName1 = "Driving Lic. Document";
            var _staffRecruitmentItemId1 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName1, new DateTime(2020, 1, 1));

            var _staffRecruitmentItemName2 = "Proof of ID Document";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName1, _staffRecruitmentItemId1, "staffrecruitmentitem", _staffRecruitmentItemName1, false, 2, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            List<Guid> careProviderStaffRoleTypeIds2 = new List<Guid>() { _careProviderStaffRoleTypeid2 };
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 3, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds2);

            #endregion

            #region Create First Applicant 

            var applicantFirstName = "ACC-4234_1_" + currentDateTime;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Steps 6 & 7 (Step 7 is same as step 6)

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Create Compliance Items

            var complianceId1_Applicant1 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId1, "StaffRecruitmentItem", _staffRecruitmentItemName1, 1);
            dbHelper.compliance.UpdateCompliance(complianceId1_Applicant1, 1233, "Expired", DateTime.Now.AddDays(-4), _defaultLoginUserID, DateTime.Now.AddDays(-3), _defaultLoginUserID, null, DateTime.Now.AddDays(-2));

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant1.ToString(), 3, "Expired");

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceItems.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Create Role Applicant

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Get Compliance Items

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId1);
            Assert.AreEqual(3, complianceRecords.Count);

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(3, complianceItems.Count);

            var complianceRecord_Applicant1 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId1);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant1.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceRecord_Applicant1[0].ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceRecord_Applicant1[1].ToString(), 3, "Outstanding");

            #endregion

            #region Steps 8

            #region Create Second Applicant 

            var applicantFirstName2 = "ACC-4234_2_" + currentDateTime;
            var _applicantId2 = dbHelper.applicant.CreateApplicant(applicantFirstName2, "Applicant_02", _careProviders_TeamId);
            var _applicantFullName2 = (string)dbHelper.applicant.GetApplicantByID(_applicantId2, "fullname")["fullname"];

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName2)
                .OpenApplicantRecord(_applicantId2.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Create Compliance Items

            var complianceId1_Applicant2 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId2, "applicant", _applicantFullName2, _staffRecruitmentItemId2, "StaffRecruitmentItem", _staffRecruitmentItemName2, 1);
            dbHelper.compliance.UpdateCompliance(complianceId1_Applicant2, 111, "Expired", DateTime.Now.AddDays(-5), _defaultLoginUserID, DateTime.Now.AddDays(-5), _defaultLoginUserID, null, DateTime.Now.AddDays(-4));

            var complianceId2_Applicant2 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId2, "applicant", _applicantFullName2, _staffRecruitmentItemId2, "StaffRecruitmentItem", _staffRecruitmentItemName2, 1);
            dbHelper.compliance.UpdateCompliance(complianceId2_Applicant2, 222, "Expired", DateTime.Now.AddDays(-3), _defaultLoginUserID, DateTime.Now.AddDays(-3), _defaultLoginUserID, null, DateTime.Now.AddDays(-2));

            var complianceId3_Applicant2 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId2, "applicant", _applicantFullName2, _staffRecruitmentItemId2, "StaffRecruitmentItem", _staffRecruitmentItemName2, 1);
            dbHelper.compliance.UpdateCompliance(complianceId3_Applicant2, 333, "Complete", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID, null, null);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant2.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceId2_Applicant2.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceId3_Applicant2.ToString(), 3, "Completed");

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId2);
            Assert.AreEqual(3, complianceItems.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Create Role Applicant

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId2, _careProviderStaffRoleTypeid2, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName2, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Get Compliance Item

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId2, _staffRecruitmentItemId2);
            Assert.AreEqual(5, complianceRecords.Count);

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId2);
            Assert.AreEqual(5, complianceItems.Count);

            var complianceRecord_Applicant2 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId2, _staffRecruitmentItemId2);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant2.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceId2_Applicant2.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceId3_Applicant2.ToString(), 3, "Completed")
                .ValidateRecordCellContent(complianceRecord_Applicant2[0].ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceRecord_Applicant2[1].ToString(), 3, "Outstanding");

            #endregion

            #region Steps 9

            #region Create Third Applicant 

            var applicantFirstName3 = "ACC-4234_3_" + currentDateTime;
            var _applicantId3 = dbHelper.applicant.CreateApplicant(applicantFirstName3, "Applicant_03", _careProviders_TeamId);
            var _applicantFullName3 = (string)dbHelper.applicant.GetApplicantByID(_applicantId3, "fullname")["fullname"];

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName3)
                .OpenApplicantRecord(_applicantId3.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Create Compliance Items

            var complianceId1_Applicant3 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId3, "applicant", _applicantFullName3, _staffRecruitmentItemId1, "StaffRecruitmentItem", _staffRecruitmentItemName1, 1);
            dbHelper.compliance.UpdateCompliance(complianceId1_Applicant3, 111, "Expired", DateTime.Now.AddDays(-5), _defaultLoginUserID, DateTime.Now.AddDays(-5), _defaultLoginUserID, null, DateTime.Now.AddDays(-4));

            var complianceId2_Applicant3 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId3, "applicant", _applicantFullName3, _staffRecruitmentItemId1, "StaffRecruitmentItem", _staffRecruitmentItemName1, 1);
            dbHelper.compliance.UpdateCompliance(complianceId2_Applicant3, 222, "Expired", DateTime.Now.AddDays(-3), _defaultLoginUserID, DateTime.Now.AddDays(-3), _defaultLoginUserID, null, DateTime.Now.AddDays(-2));

            var complianceId3_Applicant3 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId3, "applicant", _applicantFullName3, _staffRecruitmentItemId1, "StaffRecruitmentItem", _staffRecruitmentItemName1, 1);
            dbHelper.compliance.UpdateCompliance(complianceId3_Applicant3, 333, "Complete", DateTime.Now, _defaultLoginUserID, DateTime.Now, _defaultLoginUserID, null, null);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant3.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceId2_Applicant3.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceId3_Applicant3.ToString(), 3, "Completed");

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId3);
            Assert.AreEqual(3, complianceItems.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Create Role Applicant

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId3, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName3, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Get Compliance Item

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId3, _staffRecruitmentItemId1);
            Assert.AreEqual(4, complianceRecords.Count);

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId3);
            Assert.AreEqual(4, complianceItems.Count);

            var complianceRecord_Applicant3 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId3, _staffRecruitmentItemId1);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant3.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceId2_Applicant3.ToString(), 3, "Expired")
                .ValidateRecordCellContent(complianceId3_Applicant3.ToString(), 3, "Completed")
                .ValidateRecordCellContent(complianceRecord_Applicant3[0].ToString(), 3, "Outstanding");

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-4235")]
        [Description("Step(s) 10 to 11 from the original test method ACC-3986")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        public void RecruitmentDocument_ACC_3986_UITestCases003()
        {
            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "ACC-3987", null, null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "Carer_Role_Licence";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName1 = "Driving Lic. Document";
            var _staffRecruitmentItemId1 = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName1, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName1, _staffRecruitmentItemId1, "staffrecruitmentitem", _staffRecruitmentItemName1, false, 2, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Create First Applicant 

            var applicantFirstName = "ACC-4235_1_" + currentDateTime;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Steps 10

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Create Compliance Items

            var complianceId1_Applicant1 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId1, "StaffRecruitmentItem", _staffRecruitmentItemName1, 1, "", null, null, null, null, null, null);
            dbHelper.compliance.UpdateAdditionalAttributeId(complianceId1_Applicant1, 1);
            dbHelper.compliance.UpdateOverrideDateField(complianceId1_Applicant1, DateTime.Now);
            dbHelper.compliance.UpdateOverrideByField(complianceId1_Applicant1, _defaultLoginUserID);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant1.ToString(), 3, "Completed");

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceItems.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Create Role Applicant

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Get Compliance Items

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId1);
            Assert.AreEqual(2, complianceRecords.Count);

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(2, complianceItems.Count);

            var complianceRecord_Applicant1 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId1);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant1.ToString(), 3, "Completed")
                .ValidateRecordCellContent(complianceRecord_Applicant1[0].ToString(), 3, "Outstanding");

            #endregion

            #region Steps 11

            #region Create Second Applicant 

            var applicantFirstName2 = "ACC-4235_2_" + currentDateTime;
            var _applicantId2 = dbHelper.applicant.CreateApplicant(applicantFirstName2, "Applicant_02", _careProviders_TeamId);
            var _applicantFullName2 = (string)dbHelper.applicant.GetApplicantByID(_applicantId2, "fullname")["fullname"];

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName2)
                .OpenApplicantRecord(_applicantId2.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Create Compliance Items

            var complianceId1_Applicant2 = dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId2, "applicant", _applicantFullName2, _staffRecruitmentItemId1, "StaffRecruitmentItem", _staffRecruitmentItemName1, 1, null, null, null, null, null, null, null, 2);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton()
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant2.ToString(), 3, "Outstanding");

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId2);
            Assert.AreEqual(1, complianceItems.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Create Role Applicant

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId2, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName2, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Pending");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Get Compliance Item

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId2, _staffRecruitmentItemId1);
            Assert.AreEqual(3, complianceRecords.Count);

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId2);
            Assert.AreEqual(3, complianceItems.Count);

            var complianceRecord_Applicant2 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId2, _staffRecruitmentItemId1);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceId1_Applicant2.ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceRecord_Applicant2[0].ToString(), 3, "Outstanding")
                .ValidateRecordCellContent(complianceRecord_Applicant2[1].ToString(), 3, "Outstanding");

            #endregion

            // Step 12 is already covered in previous steps

        }

        #endregion
    }
}
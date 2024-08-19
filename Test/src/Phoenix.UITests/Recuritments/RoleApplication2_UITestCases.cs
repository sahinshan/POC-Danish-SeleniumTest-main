using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for Role Application UI Page
    /// </summary>
    [TestClass]
    public class RoleApplication2_UITestCases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _defaultLoginUserID;
        private Guid staffReviewsTeamEditSecurityProfileId;
        private Guid _applicantId;
        private string _applicantFullName;
        private Guid _defaultCareProviderStaffRoleTypeid;
        private Guid _careProviderStaffRoleTypeid;
        private string _careProviderStaffRoleName;
        private Guid _employmentContractTypeid;
        public Guid environmentid;
        private Guid _roleApplicationID;
        private string _loginUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _nonAdminUserId;
        private string _nonAdminLoginUser;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
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

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion Authentication

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "CareProviders";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Security Profiles

                staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();

                #endregion

                #region Create default system user

                _loginUsername = "Login_User" + currentTimeString;
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "Login_", "Automation_User_" + currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion

                #region User Security Profile

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);

                #endregion

                #region Care provider staff role type

                _defaultCareProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Default CPSRT", null, null, new DateTime(2021, 1, 1), "Default CPSRT ...");

                #endregion

                #region Recruitment Requirement Setup

                foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                    dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

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

        #region https://advancedcsg.atlassian.net/browse/ACC-3040

        [TestProperty("JiraIssueID", "ACC-3040")]
        [Description("Promote an applicant to system user")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Worker Contracts")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "System Users")]
        public void PromoteApplicantToSystemUser_UITestCases001()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Associate Producer";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Cook ...");

            #endregion

            #region Employment Contract Type

            commonMethodsDB.CreateEmploymentContractType(_teamId, "ECT_3040", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_teamId, "STI_3040_A", new DateTime(2022, 1, 1));
            Guid staffTrainingItem2Id = commonMethodsDB.CreateStaffTrainingItem(_teamId, "STI_3040_B", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3040_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3040_B", staffTrainingItem2Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_3040_A", _teamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);
            commonMethodsDB.CreateTrainingRequirement("TC_3040_B", _teamId, staffTrainingItem2Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "SRI_3040";

            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecReq_3040";
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Rocky";
            var applicantLastName = currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid recruitmentDocumentId in recruitmentDocuments)
                dbHelper.compliance.UpdateCompliance(recruitmentDocumentId, 1234, "Ref Name", currentDate, currentDate);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

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
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickProceedToInductionButton(1);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            #endregion

            #region Step 3

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            var systemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());
            var systemUserEmploymentContracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(systemUserId);
            Assert.AreEqual(1, systemUserEmploymentContracts.Count);
            var systemUserEmploymentContractId = systemUserEmploymentContracts[0];

            systemUserEmploymentContractsPage
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateResponsibleTeamLinkFieldText(_teamName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3631")]
        [Description("Promote an applicant to system user")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "System Users")]
        [TestProperty("Screen3", "System User Training")]
        [TestProperty("Screen4", "Recruitment Dashboard")]
        [TestProperty("Screen5", "Applicant Dashboard")]
        public void PromoteApplicantToSystemUser_UITestCases002()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Associate Producer";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Cook ...");

            #endregion

            #region Employment Contract Type

            commonMethodsDB.CreateEmploymentContractType(_teamId, "ECT_3040", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_teamId, "STI_3040_A", new DateTime(2022, 1, 1));
            Guid staffTrainingItem2Id = commonMethodsDB.CreateStaffTrainingItem(_teamId, "STI_3040_B", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3040_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3040_B", staffTrainingItem2Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            var trainingCourses1Id = commonMethodsDB.CreateTrainingRequirement("TC_3040_A", _teamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);
            var trainingCourses2Id = commonMethodsDB.CreateTrainingRequirement("TC_3040_B", _teamId, staffTrainingItem2Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item


            var _staffRecruitmentItemName = "SRI_3040";

            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            var _recruitmentRequirementName = "RecReq_3040";
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Rocky";
            var applicantLastName = currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid recruitmentDocumentId in recruitmentDocuments)
                dbHelper.compliance.UpdateCompliance(recruitmentDocumentId, 1234, "Ref Name", currentDate, currentDate);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

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
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickProceedToInductionButton(1);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            #endregion

            #region Step 4

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            var systemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());

            var systemUserTrainings = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItem1Id, systemUserId);
            Assert.AreEqual(1, systemUserTrainings.Count);
            var systemUserTraining1Id = systemUserTrainings[0];

            systemUserTrainings = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItem2Id, systemUserId);
            Assert.AreEqual(1, systemUserTrainings.Count);
            var systemUserTraining2Id = systemUserTrainings[0];

            #endregion

            #region Step 5

            systemUserTrainingPage
                .OpenRecord(systemUserTraining1Id.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(trainingCourses1Id);

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTrainingCourseFinishDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .SelectOutcome("Pass")
                .InsertExpiryDate(currentDate.AddDays(30).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDashboardSubPage();

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ValidateFullyAcceptedPercentageText(1, "67%");

            #endregion

            #region Step 6

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(systemUserTraining2Id.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(trainingCourses2Id);

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTrainingCourseFinishDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .SelectOutcome("Pass")
                .InsertExpiryDate(currentDate.AddDays(30).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDashboardSubPage();

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ValidateFullyAcceptedPercentageText(1, "100%");

            #endregion

            #region Step 7

            systemUserRecruitmentDashboardPage
                .ClickProceedToFullyAcceptedButton(1);

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ValidateStatusFieldText(1, "FULLY ACCEPTED");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3055

        [TestProperty("JiraIssueID", "ACC-3047")]
        [Description("Step 4 from the original test ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "System Users")]
        [TestProperty("Screen3", "System User Training")]
        [TestProperty("Screen4", "Recruitment Dashboard")]
        [TestProperty("Screen5", "Applicant Dashboard")]
        public void PromoteApplicantToSystemUser_UITestCases003()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Second Producer";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Cook ...");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "ECT_3047", null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem1Id = commonMethodsDB.CreateStaffTrainingItem(_teamId, "STI_3047_A", new DateTime(2022, 1, 1));
            Guid staffTrainingItem2Id = commonMethodsDB.CreateStaffTrainingItem(_teamId, "STI_3047_B", new DateTime(2022, 1, 1));

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3047_A", staffTrainingItem1Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirementSetup("TRS_3047_B", staffTrainingItem2Id, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Courses

            commonMethodsDB.CreateTrainingRequirement("TC_3047_A", _teamId, staffTrainingItem1Id, new DateTime(2022, 1, 1), null, 6, 1);
            commonMethodsDB.CreateTrainingRequirement("TC_3047_B", _teamId, staffTrainingItem2Id, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItem1Name = "SRI_3047_A";
            var _staffRecruitmentItem1Id = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItem1Name, new DateTime(2023, 1, 1));

            var _staffRecruitmentItem2Name = "SRI_3047_B";
            var _staffRecruitmentItem2Id = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItem2Name, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirement1Name = "RecReq_3047_A";
            var recruitmentRequirement1Id = commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirement1Name, _staffRecruitmentItem1Id, "staffrecruitmentitem", _staffRecruitmentItem1Name, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            var _recruitmentRequirement2Name = "RecReq_3047_B";
            var recruitmentRequirement2Id = commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirement2Name, _staffRecruitmentItem2Id, "staffrecruitmentitem", _staffRecruitmentItem2Name, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Mathew";
            var applicantLastName = currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Step 4

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
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickNewApplicationButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordToLoadFromApplicantDashboardPage()
                .ClickRoleLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(_careProviderStaffRoleName, _careProviderStaffRoleTypeid);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordToLoadFromApplicantDashboardPage()
                .ClickTargetTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord(_teamName, _teamId);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordToLoadFromApplicantDashboardPage()
                .ClickContractTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("ECT_3047", employmentContractTypeId);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordToLoadFromApplicantDashboardPage()
                .ClickSaveAndCloseButton();

            #endregion

            #region Step 5

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateRoleApplicationTitleText(1, _careProviderStaffRoleName)
                .ValidateInductionReadinessPercentageText(1, "0%")
                .ValidateFullyAcceptedPercentageText(1, "0%");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad();

            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(2, recruitmentDocuments.Count);
            var recruitmentDocument1Id = recruitmentDocuments[0];
            var recruitmentDocument2Id = recruitmentDocuments[1];

            #endregion

            #region Step 6

            recruitmentDocumentsPage
                .OpenRecruitmentDocumentsRecord(recruitmentDocument1Id.ToString());

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Outstanding")

                .InsertReferenceNumber("12345")
                .InsertRequestedDateValue(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertCompletedDateValue(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()

                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ValidateStatusSelectedText("Completed")
                .ClickBackButton();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateRoleApplicationTitleText(1, _careProviderStaffRoleName)
                .ValidateInductionReadinessPercentageText(1, "50%")
                .ValidateFullyAcceptedPercentageText(1, "25%");

            #endregion

            #region Step 7

            dbHelper.compliance.UpdateCompliance(recruitmentDocument2Id, 4321, "Ref Name ...", currentDate, currentDate);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateRoleApplicationTitleText(1, _careProviderStaffRoleName)
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%");

            #endregion

            #region Step 8

            applicantDashboardPage
                .ClickProceedToInductionButton(1);

            #endregion

            #region Step 9

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            systemUserRecordPage
                .SwitchToNewTab("Recruitment Widget");

            systemUserRecruitmentDashboardPage
                .WaitForRecruitmentWidgetPageToLoad()
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%");

            #endregion

            #region Step 10

            systemUserRecordPage
                .SwitchToPreviousTab();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            var systemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());
            var systemUserEmploymentContracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(systemUserId);
            Assert.AreEqual(1, systemUserEmploymentContracts.Count);
            var systemUserEmploymentContractId = systemUserEmploymentContracts[0];

            systemUserEmploymentContractsPage
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStartDate_FieldText("")
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #endregion

            #region Step 11

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            var systemUserTrainings = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItem1Id, systemUserId);
            Assert.AreEqual(1, systemUserTrainings.Count);
            var systemUserTraining1Id = systemUserTrainings[0];

            systemUserTrainings = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItem2Id, systemUserId);
            Assert.AreEqual(1, systemUserTrainings.Count);
            var systemUserTraining2Id = systemUserTrainings[0];

            systemUserTrainingPage
                .ValidateRecordCellText(systemUserTraining1Id.ToString(), 6, "Outstanding")
                .ValidateRecordCellText(systemUserTraining2Id.ToString(), 6, "Outstanding");

            #endregion

            #region Step 12

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDocumentsSubPage();

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad();

            recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(systemUserId);
            Assert.AreEqual(2, recruitmentDocuments.Count);
            recruitmentDocument1Id = recruitmentDocuments[0];
            recruitmentDocument2Id = recruitmentDocuments[1];

            systemUserRecruitmentDocumentsPage
                .ValidateRecordCellText(recruitmentDocument1Id.ToString(), 3, "Completed")
                .ValidateRecordCellText(recruitmentDocument1Id.ToString(), 3, "Completed");

            #endregion

            #region Step 13

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad();

            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(systemUserId);
            Assert.AreEqual(1, roleApplications.Count);
            var roleApplicationId = roleApplications[0];

            roleApplicationsPage
                .OpenRoleApplicationRecord(roleApplicationId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRoleName(_careProviderStaffRoleName)
                .ValidateRecruitmentStatusSelectedText("Induction")
                .ValidateTargetTeamName(_teamName);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3694

        [TestProperty("JiraIssueID", "ACC-3692")]
        [Description("Set System User's Employee Type when Applicant is promoted")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "System Users")]
        [TestProperty("Screen3", "System User Training")]
        [TestProperty("Screen4", "Recruitment Dashboard")]
        [TestProperty("Screen5", "Applicant Dashboard")]
        public void PromoteApplicantToSystemUser_UITestCases004()
        {
            #region Care provider staff role type
            _careProviderStaffRoleName = "Director";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Director ...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Employment Contract Type            
            commonMethodsDB.CreateEmploymentContractType(_teamId, "ECT_3692", null, null, new DateTime(2020, 1, 1));

            #endregion            

            #region Staff Recruitment Item
            var _staffRecruitmentItemName = "SRI_3692";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecReq_3692";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Applicant

            var applicantFirstName = "Rocky";
            var applicantLastName = currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Role Application

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid complianceId in recruitmentDocuments)
            {
                dbHelper.compliance.UpdateCompliance(complianceId, 3694, "RefNameTest", currentDate, currentDate);
            }

            #endregion            

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2, 3

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + applicantLastName + "*")
                .OpenApplicantRecord(_applicantId.ToString());

            #endregion

            #region Step 4

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            #endregion

            #region Step 5

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickProceedToInductionButton(1);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            #endregion

            #region Step 6, 7

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(applicantFirstName + " " + applicantLastName)
                .ValidateSelectedEmployeeTypeValue("Rostered System User");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3702

        [TestProperty("JiraIssueID", "ACC-1177")]
        [Description("Working Time Directive Opt Out Options")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "System Users")]
        public void WorkingTimeDirectiveOptOut_UITestCases005()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Carer Associate";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Carer Associate...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Employment Contract Type            
            commonMethodsDB.CreateEmploymentContractType(_teamId, "ECT_3702", null, null, new DateTime(2020, 1, 1));

            #endregion            

            #region Staff Recruitment Item
            var _staffRecruitmentItemName = "SRI_3702";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecReq_3702";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Applicant

            var applicantFirstName = "John";
            var applicantLastName = currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2, 3

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            #endregion

            #region Step 4, 5

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateWorkingTimeDirectiveOptOutValue("Not known")
                .ValidateWorkingTimeDirectiveOptOutFieldOption("Not known")
                .ValidateWorkingTimeDirectiveOptOutFieldOption("No")
                .ValidateWorkingTimeDirectiveOptOutFieldOption("Yes")
                .SelectWorkingTimeDirectiveOptOutValue("Yes")
                .InsertFirstName(applicantFirstName)
                .InsertLastName(applicantLastName)
                .InsertAvailableFromDateField("01/01/2020")
                .ClickSaveButton()
                .WaitForApplicantRecordPagePageToLoad();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateWorkingTimeDirectiveOptOutValue("Yes");

            var _newApplicantId = dbHelper.applicant.GetByLastName(applicantLastName)[0];
            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_newApplicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #region Recruitment Documents
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_newApplicantId);

            foreach (Guid complianceId in recruitmentDocuments)
                dbHelper.compliance.UpdateCompliance(complianceId, 3702, "RefNameTest", currentDate, currentDate);

            #endregion  

            dbHelper.recruitmentRoleApplicant.UpdateRecruitmentStatus(_roleApplicationID, 2);

            applicantRecordPage
                .NavigateToApplicantDashboardTab();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_applicantFullName)
                .ValidateOptedOutOfWorkingTimeDirective("Yes");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3716

        [TestProperty("JiraIssueID", "ACC-3715")]
        [Description("GDPR consent for Applicant and System User")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "System Users")]
        public void AllowDataUseInAccordanceWithGDPR_UITestCases006()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Head Carer";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Head Carer...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Employment Contract Type            
            commonMethodsDB.CreateEmploymentContractType(_teamId, "ECT_3715", null, null, new DateTime(2020, 1, 1));

            #endregion            

            #region Staff Recruitment Item
            var _staffRecruitmentItemName = "SRI_3715";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecReq_3715";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Applicant

            var applicantFirstName = "Mason";
            var applicantLastName = "GW_" + currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Step 1, 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(applicantFirstName)
                .InsertLastName(applicantLastName)
                .InsertAvailableFromDateField("01/01/2020")
                .ValidateAllowToUseGdprFieldIsVisible(true)
                .ValidateAllowToUseGdprField_NoOptionSelected()
                .ClickAllowToUseGdpr_YesOption()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateAllowToUseGdprField_YesOptionSelected();

            var _newApplicantId = dbHelper.applicant.GetByLastName(applicantLastName)[0];
            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_newApplicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Step 3, 4, 5

            #region Recruitment Documents

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(_newApplicantId);

            foreach (Guid complianceId in recruitmentDocuments)
                dbHelper.compliance.UpdateCompliance(complianceId, 3715, "RefNameTest", currentDate, currentDate);

            #endregion

            dbHelper.recruitmentRoleApplicant.UpdateRecruitmentStatus(_roleApplicationID, 2);

            applicantRecordPage
                .NavigateToApplicantDashboardTab();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_applicantFullName)
                .ValidateAllowToUseGdprFieldIsVisible(true)
                .ValidateAllowToUseGdprField_YesOptionSelected();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3705

        [TestProperty("JiraIssueID", "ACC-1181")]
        [Description("Recruitment Documents and Role Applications are transferred over to System User")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "System Users")]
        [TestProperty("Screen3", "Recruitment Documents")]
        [TestProperty("Screen4", "Role Applications")]
        [TestProperty("Screen5", "Applicant Dashboard")]
        public void PromoteApplicantToSystemUser_UITestCases007()
        {
            #region Step 1 to Step 7

            #region Care provider staff role type
            _careProviderStaffRoleName = "Test Applicant";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Test Applicant...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Employment Contract Type            
            commonMethodsDB.CreateEmploymentContractType(_teamId, "ECT_3705", null, null, new DateTime(2020, 1, 1));

            #endregion            

            #region Staff Recruitment Item
            var _staffRecruitmentItemName = "SRI_3705";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecReq_3705";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Applicant

            var applicantFirstName = "Aaron";
            var applicantLastName = "Aa_" + currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Recruitment Documents

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var recruitmentDocumentsForApplicant = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.IsTrue(recruitmentDocumentsForApplicant.Count >= 1);
            var recruitmentDocument = recruitmentDocumentsForApplicant[0];

            foreach (Guid complianceId in recruitmentDocumentsForApplicant)
                dbHelper.compliance.UpdateCompliance(complianceId, 3705, "RefNameTest", currentDate, currentDate);

            dbHelper.recruitmentRoleApplicant.UpdateRecruitmentStatus(_roleApplicationID, 2);

            #endregion

            #endregion

            #region Step 8

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_applicantFullName)
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDocumentsSubPage();

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad()
                .ValidateRecordVisible(recruitmentDocument.ToString());

            var promotedSystemUserId = dbHelper.systemUser.GetSystemUserByUserNameContains(applicantLastName)[0];
            var recruitmentDocumentsForSystemUser = dbHelper.compliance.GetComplianceByRegardingId(promotedSystemUserId);
            Assert.IsTrue(recruitmentDocumentsForSystemUser.Count >= 1);

            #endregion

            #region Step 9

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .ValidateApplicantActiveLabelText("No")
                .NavigateToRoleApplicationsPage();

            var roleApplicationsForApplicant = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(0, roleApplicationsForApplicant.Count);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecruitmentDocumentsRecordIsNotPresent(recruitmentDocument.ToString());

            recruitmentDocumentsForApplicant = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(0, recruitmentDocumentsForApplicant.Count);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3706

        [TestProperty("JiraIssueID", "ACC-1174")]
        [Description("Test steps of Original test ACC-1174")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "System Users")]
        [TestProperty("Screen3", "Recruitment Documents")]
        [TestProperty("Screen4", "System User Training")]
        [TestProperty("Screen5", "Applicant Dashboard")]
        public void ApplicantDashboard_UITestCases008()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "ACC-1174_Staff";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "ACC-1174...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-1174";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup
            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            var _recruitmentRequirementName = "RecACC-1174_3715";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Applicant

            var applicantFirstName = "ACC_1174_Applicant";
            var applicantLastName = "LN_" + currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_teamId, "ACC-1174_Training", new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup("ACC-1174_Training", staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement("ACC-1174_Training - Internal", _teamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Step 1 to 9

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(applicantFirstName)
                .InsertLastName(applicantLastName)
                .InsertAvailableFromDateField("01/01/2020")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            #region Recruitment Role Applicant / Compliance Record

            _applicantId = dbHelper.applicant.GetByLastName(applicantLastName)[0];
            dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            var newComplianceRecordId = complianceRecords[0];

            #endregion

            applicantRecordPage
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateUnsatisfiedRequirementsForInductionLabel()
                .ExpandUnsatisfiedRequirementsForInductionSection()
                .ValidateUnsatisfiedLinkVisible(newComplianceRecordId.ToString(), true)
                .ValidateUnsatisfiedText(newComplianceRecordId.ToString(), _staffRecruitmentItemName + " - 1 item is not Complete")
                .ClickUnsatisfiedLinkText(newComplianceRecordId.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoadForApplicant()
                .ValidatePageHeaderTitle(_staffRecruitmentItemName)
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ExpandUnsatisfiedRequirementsForInductionSection()
                .ValidateUnsatisfiedLinkVisible(newComplianceRecordId.ToString(), false);

            #region Compliance 

            dbHelper.compliance.CreateCompliance(_teamId, _applicantId, "applicant", applicantFirstName + " " + applicantLastName, _staffRecruitmentItemId, "StaffRecruitmentItem", _staffRecruitmentItemName, 1);

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));

            #endregion

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickProceedToInductionButton(1);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            #region System User & System User Training

            var systemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());
            var systemUserTrainings = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItem, systemUserId);
            var systemUserTrainingId = systemUserTrainings[0];

            #endregion

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(systemUserTrainingId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Outstanding");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3766

        [TestProperty("JiraIssueID", "ACC-3787")]
        [Description("Test steps 1 to 5 & 9 to 10 of Original test ACC-3765")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "All Role Applications")]
        public void Applicant_SystemUser_UITestCases001()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "ACC-3765_Staff";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "ACC-3765...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3765";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            var _recruitmentRequirementName = "RecACC-3765";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Applicant

            var applicantFirstName = "001_ACC_3765_Applicant";
            var applicantLastName = "LN_" + currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_teamId, "ACC-3765_Training", new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup("ACC-3765_Training", staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement("ACC-3765_Training - Internal", _teamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Step 1 to 5 & 9 to 10

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
                .WaitForAllRoleApplicationsToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(applicantFirstName)
                .InsertLastName(applicantLastName)
                .InsertAvailableFromDateField("01/01/2020")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            #region Applicant Recrod

            _applicantId = dbHelper.applicant.GetByLastName(applicantLastName)[0];

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
                .WaitForAllRoleApplicationsToLoad()
                .SwitchToApplicantsPendingFrame()
                .ValidateApplicantFullNameInApplicantsPendingSection(_applicantFullName, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .TypeSearchQuery(applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Recruitment Role Applicant

            var _recruitmentRoleApplicantId = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateRoleApplicationRecordIsPresent(_recruitmentRoleApplicantId.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
                .WaitForAllRoleApplicationsToLoad()
                .SwitchToApplicantsPendingFrame()
                .ValidateApplicantFullNameInApplicantsPendingSection(_applicantFullName, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .OpenApplicantRecord(_applicantId.ToString());

            #region Compliance 

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", DateTime.Now, DateTime.Now);

            #endregion

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickProceedToInductionButton(1);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
                .WaitForAllRoleApplicationsToLoad()
                .SwitchToApplicantsPendingFrame()
                .ValidateApplicantFullNameInApplicantsPendingSection(_applicantFullName, false)

                .WaitForAllRoleApplicationsToLoad()
                .SwitchToReadyForInductionFrame()
                .ValidateSystemUserFullNameInReadyForInductionSection(_applicantFullName, true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3788")]
        [Description("Test steps 6 to 8 & 11 of Original test ACC-3765")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "All Role Applications")]
        public void Applicant_SystemUser_UITestCases002()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "ACC-3765_Staff";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "ACC-3765...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3765";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecACC-3765";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Applicant

            var systemUserFirstName = "001_ACC_3765_User";
            var systemUserLastName = "LN_" + currentTimeString;
            var _systemUserFullName = systemUserFirstName + " " + systemUserLastName;

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_teamId, "ACC-3765_Training", new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup("ACC-3765_Training", staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement("ACC-3765_Training - Internal", _teamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Step 6 to 8 & 11

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectEmployeeType("System Administrator")
                .InsertFirstName(systemUserFirstName)
                .InsertLastName(systemUserLastName)
                .InsertWorkEmail(systemUserLastName + "@test.com")
                .SelectBusinessUnitByText("CareProviders")
                .ClickDefaultTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_teamName)
                .TapSearchButton()
                .SelectResultElement(_teamId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertUserName(systemUserLastName)
                .InsertPassword("Passw0rd_!")
                .InsertAvailableFromDateField("01/01/2022")
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            var _newSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(systemUserLastName).FirstOrDefault();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
                .WaitForAllRoleApplicationsToLoad()
                .SwitchToReadyForInductionFrame()
                .ValidateApplicantFullNameInApplicantsPendingSection(_systemUserFullName, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(systemUserLastName)
                .ClickSearchButton()
                .OpenRecord(_newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region Recruitment Role Applicant

            var _recruitmentRoleApplicantId = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_newSystemUserId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "systemuser", _systemUserFullName, _employmentContractTypeid);

            #endregion

            roleApplicationsPage
                .ClickRefreshButton()
                .ValidateRoleApplicationRecordIsPresent(_recruitmentRoleApplicantId.ToString());

            #region Completed Compliance for System User

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_newSystemUserId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", DateTime.Now, DateTime.Now);

            #endregion

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDashboardSubPage();

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ClickProcessToInductionButton(1);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
                .WaitForAllRoleApplicationsToLoad()
                .SwitchToReadyForInductionFrame()
                .ValidateApplicantFullNameInApplicantsPendingSection(_systemUserFullName, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(systemUserLastName)
                .ClickSearchButton()
                .OpenRecord(_newSystemUserId.ToString());

            #region Update System User Training Status

            var systemUserTrainingItems = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(_newSystemUserId);

            foreach (Guid systemUserTrainingId in systemUserTrainingItems)
            {
                Guid trainingCourse = dbHelper.trainingRequirement.GetTrainingRequirementByTrainingItem(staffTrainingItem).First();
                dbHelper.systemUserTraining.UpdateSystemUserTraining(systemUserTrainingId, trainingCourse, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            }

            #endregion

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDashboardSubPage();

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ClickProceedToFullyAcceptedButton(1);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
                .WaitForAllRoleApplicationsToLoad()
                .SwitchToReadyForInductionFrame()
                .ValidateApplicantFullNameInApplicantsPendingSection(_systemUserFullName, false);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3774

        [TestProperty("JiraIssueID", "ACC-3800")]
        [Description("Test steps of Original test ACC-3800")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Role Applications")]
        [TestProperty("Screen3", "System User Training")]
        public void Applicant_SystemUser_UITestCases003()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "ACC-3800_Staff";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "ACC-3800...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3800";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecACC-3800";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Training Item

            var trainingItemName = "ACC-3800_Training";
            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_teamId, trainingItemName, new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup(trainingItemName, staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement(trainingItemName + " - Internal", _teamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Applicant

            var applicantFirstName = "ACC-3800";
            var applicantLastName = "Applicant_" + currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _teamId);

            #endregion

            #region Recruitment Role Applicant

            var _recruitmentRoleApplicantId = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion


            #region Step 1 to 6

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_recruitmentRoleApplicantId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Induction requirements are unmet. Missing:\r\n")
                .ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            #region Completed Compliance for Applicant

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", DateTime.Now, DateTime.Now);

            #endregion

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(2000);
            recruitmentDocumentsPage
                .ValidateRecordCellContent(complianceItems[0].ToString(), 3, "Completed");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_recruitmentRoleApplicantId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusFieldAlertMessage("All recruitment requirements have been met for the status Induction.");

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:\r\n")
                .ValidateMessageContainsText(trainingItemName + " - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            var systemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());

            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(systemUserId);
            Assert.AreEqual(1, roleApplications.Count);
            var roleApplicationId = roleApplications[0];

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(roleApplicationId.ToString())
                .OpenRoleApplicationRecord(roleApplicationId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:\r\n")
                .ValidateMessageContainsText(trainingItemName + " - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            #region Update System User Training Status

            var systemUserTrainingItems = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);

            foreach (Guid systemUserTrainingId in systemUserTrainingItems)
            {
                Guid trainingCourse = dbHelper.trainingRequirement.GetTrainingRequirementByTrainingItem(staffTrainingItem).First();
                dbHelper.systemUserTraining.UpdateSystemUserTraining(systemUserTrainingId, trainingCourse, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            }

            #endregion

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton();

            System.Threading.Thread.Sleep(2000);
            systemUserTrainingPage
                .ValidateRecordVisible(systemUserTrainingItems[0].ToString())
                .ValidateTableHeaderCellText(6, "Status")
                .ValidateRecordCellText(systemUserTrainingItems[0].ToString(), 6, "Current");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(roleApplicationId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusFieldAlertMessage("All recruitment requirements have been met for the status Fully Accepted.")
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton()
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Fully Accepted");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3768

        [TestProperty("JiraIssueID", "ACC-3767")]
        [Description("All the test steps from the test case automated")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Recruitment Documents")]
        [TestProperty("Screen3", "Applicant Dashboard")]
        [TestProperty("Screen4", "Recruitment Dashboard")]
        [TestProperty("Screen5", "System User Employment Contracts")]
        [TestProperty("Screen6", "System User Training")]
        public void ApplicantToSystemUserDashboard_UITestCase001()
        {
            #region Step 1

            #region Care provider staff role type

            _careProviderStaffRoleName = "A3767_Staff";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "A3767...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3767";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            var _recruitmentRequirementName = "RR-3767";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Applicant

            var _applicantFirstName = "Axel";
            var _applicantLastName = "Aa_" + currentTimeString;
            var _applicantFullName = _applicantFirstName + " " + _applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(_applicantFirstName, _applicantLastName, _teamId);
            dbHelper.applicant.UpdateAvailableFrom(_applicantId, new DateTime(2023, 1, 1));
            #endregion

            #region Staff Training Item

            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_teamId, "A3767_Training", new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup("A3767_Training", staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement("A3767_Training - Internal", _teamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Role Application

            var _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(
                _applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 5

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord("*" + _applicantLastName + "*")
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var complianceRecords = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            Assert.AreEqual(1, complianceRecords.Count);

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecordCellContent(complianceRecords[0].ToString(), 3, "Outstanding");

            #endregion

            #region Step 6

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateRoleApplicationTitleText(1, _careProviderStaffRoleName)
                .ValidateInductionReadinessPercentageText(1, "0%")
                .ValidateFullyAcceptedPercentageText(1, "0%")
                .ValidateProceedToInductionButtonIsDisabled(1, true);

            #endregion

            #region Step 7

            foreach (Guid complianceId in complianceRecords)
                dbHelper.compliance.UpdateCompliance(complianceId, 3767, "Test", DateTime.Now, DateTime.Now);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateProceedToInductionButtonIsDisabled(1, false)
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%");

            #endregion

            #region Step 8
            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickProceedToInductionButton(1);

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapCancelButton();

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ValidateRefreshButtonVisible(true)
                .ValidateApplicationsAreaVisible()
                .ValidateRoleApplicationNotVisible(1);

            #endregion

            #region Step 9

            applicantDashboardPage
                .WaitForApplicantDashboardPageToLoad()
                .ClickRefreshButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            systemUserRecordPage
                .SwitchToNewTab("Recruitment Widget");

            systemUserRecruitmentDashboardPage
                .WaitForRecruitmentWidgetPageToLoad()
                .ValidateRoleApplicationTitleText(1, _careProviderStaffRoleName)
                .ValidateStatusFieldText(1, "INDUCTION")
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%");

            #endregion

            #region Step 10

            var newSystemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());

            var contracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(newSystemUserId);
            Assert.AreEqual(1, contracts.Count);
            var contractId = contracts[0];

            systemUserRecordPage
                .SwitchToPreviousTab();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordCellText(contractId.ToString(), 3, "");

            #endregion

            #region Step 11

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            var trainings = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(newSystemUserId);
            Assert.AreEqual(1, trainings.Count);
            var trainingId = trainings[0];

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateRecordCellText(trainingId.ToString(), 6, "Outstanding");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3776

        [TestProperty("JiraIssueID", "ACC-3832")]
        [Description("Test steps of Original test ACC-3832")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Recruitment Dashboard")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void SystemUser_Dashboard_UITestCases001()
        {
            if (dbHelper.recruitmentRequirement.GetByName("RecACC-3832_2").Any())
            {
                var RecruitmentRequirementId_ACC_3832_2 = dbHelper.recruitmentRequirement.GetByName("RecACC-3832_2");
                dbHelper.recruitmentRequirement.DeleteRecruitmentRequirement(RecruitmentRequirementId_ACC_3832_2[0]);
            }

            #region Care provider staff role type

            _careProviderStaffRoleName = "ACC-3832_Staff";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "ACC-3832...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3832";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecACC-3832";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User

            var systemUserFirstName = "ACC_3832_User";
            var systemUserLastName = "LN_" + currentTimeString;
            var _systemUserFullName = systemUserFirstName + " " + systemUserLastName;
            var _systemUserID = commonMethodsDB.CreateSystemUserRecord(systemUserFirstName + "_" + systemUserLastName, systemUserFirstName, systemUserLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemName = "ACC-3832_Training";
            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName, new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup(staffTrainingItemName, staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement(staffTrainingItemName + " - Internal", _teamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Recruitment Role Applicant for System User

            dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_systemUserID, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "systemuser", _systemUserFullName, _employmentContractTypeid);

            #endregion

            #region Complete Compliances for System User

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_systemUserID);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", DateTime.Now, DateTime.Now);

            #endregion

            // Steps 1 to 7 Already covered in another tests

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("*" + systemUserLastName + "*")
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDashboardSubPage();

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%")
                .ClickProcessToInductionButton(1);

            System.Threading.Thread.Sleep(2000);

            #region System User Training

            var systemUserTrainings = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItem, _systemUserID);
            var systemUserTrainingId = systemUserTrainings[0];

            #endregion

            systemUserRecruitmentDashboardPage
                .ExpandUnsatisfiedRequirementsForFullyAcceptedSection()
                .ValidateUnsatisfiedLinkVisible(systemUserTrainingId.ToString(), true)
                .ValidateUnsatisfiedText(systemUserTrainingId.ToString(), staffTrainingItemName + " - 1 item is not Current");

            #region Staff Recruitment Item

            var _staffRecruitmentItemName2 = "ACC-3832_2";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName2, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName2 = "RecACC-3832_2";
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Execute Job 'Create Missing Recruitment Documents'

            Guid CreateMissingRecruitmentDocumentsId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Recruitment Documents")[0];
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(CreateMissingRecruitmentDocumentsId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(CreateMissingRecruitmentDocumentsId);
            System.Threading.Thread.Sleep(2000);
            #endregion

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(2000);

            systemUserRecruitmentDashboardPage
                .ValidateInductionReadinessPercentageText(1, "50%")
                .ValidateFullyAcceptedPercentageText(1, "33%");

            var complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_systemUserID, _staffRecruitmentItemId2);
            var complianceId_2 = complianceRecords[0];

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDocumentsSubPage();

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad()
                .OpenRecord(complianceId_2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #region Delete Recruitment Requirement

            if (dbHelper.recruitmentRequirement.GetByName("RecACC-3832_2").Any())
            {
                var RecruitmentRequirementId_ACC_3832_2 = dbHelper.recruitmentRequirement.GetByName("RecACC-3832_2");
                dbHelper.recruitmentRequirement.DeleteRecruitmentRequirement(RecruitmentRequirementId_ACC_3832_2[0]);
            }

            #endregion

            #region Execute Job 'Create Missing Recruitment Documents'

            CreateMissingRecruitmentDocumentsId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Recruitment Documents")[0];
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(CreateMissingRecruitmentDocumentsId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(CreateMissingRecruitmentDocumentsId);
            System.Threading.Thread.Sleep(2000);
            #endregion

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad();

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_systemUserID, _staffRecruitmentItemId2);
            Assert.AreEqual(0, complianceRecords.Count);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDashboardSubPage();

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(2000);

            systemUserRecruitmentDashboardPage
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%");

            #region Staff Recruitment Item

            commonMethodsDB = new CommonMethodsDB(dbHelper);
            _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName2, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _recruitmentRequirementName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Execute Job 'Create Missing Recruitment Documents'

            dbHelper = new DBHelper.DatabaseHelper();
            CreateMissingRecruitmentDocumentsId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Recruitment Documents")[0];
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(CreateMissingRecruitmentDocumentsId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(CreateMissingRecruitmentDocumentsId);
            System.Threading.Thread.Sleep(2000);
            #endregion

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(2000);

            systemUserRecruitmentDashboardPage
                .ValidateInductionReadinessPercentageText(1, "50%")
                .ValidateFullyAcceptedPercentageText(1, "33%");

            complianceRecords = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_systemUserID, _staffRecruitmentItemId2);
            complianceId_2 = complianceRecords[0];

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDocumentsSubPage();

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad()
                .OpenRecord(complianceId_2.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Override - doc not required")
                .ClickSaveAndCloseButton();

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDashboardSubPage();

            systemUserRecruitmentDashboardPage
                .WaitForSystemUserRecruitmentDashboardPagePageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(2000);
            systemUserRecruitmentDashboardPage
                .ValidateInductionReadinessPercentageText(1, "100%")
                .ValidateFullyAcceptedPercentageText(1, "50%");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3898

        [TestProperty("JiraIssueID", "ACC-3924")]
        [Description("Steps 1 to7 from original test ACC-3914.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Role Applications")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void ACC_3914_UITestCases001()
        {
            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "ACC-3924", null, null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "ACC-3924_Role_Type";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3924_1_RI";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            var _staffRecruitmentItemName2 = "ACC-3924_2_RI";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName2, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _staffRecruitmentItemName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _staffRecruitmentItemName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_ACC-3924_" + currentTimeString;
            var _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _teamId);

            _applicantFullName = applicantFirstName + " Applicant_01";

            #endregion            

            #region Staff Training Item

            var trainingItemName = "ACC-3924_Training";
            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_teamId, trainingItemName, new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup(trainingItemName, staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement(trainingItemName + " - Internal", _teamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Create Role Applicant

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Non-admin user

            CreateNonAdminUser();

            #endregion

            #region Steps

            loginPage
                .GoToLoginPage()
                .Login(_nonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            #region Complete Compliances for System User

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", DateTime.Now, DateTime.Now);

            #endregion

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton()
                .WaitForRoleApplicationRecordPageToLoad()
                .WaitForRecordToBeSaved();

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:\r\n")
                .ValidateMessageContainsText(trainingItemName + " - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToApplicantDashboardTab();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("You moved the Applicant to the Induction phase, so they are promoted to System User.\r\nDo you want to move to the newly created System User?")
                .TapOKButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .SwitchToNewTab("Recruitment Widget")
                .SwitchToPreviousTab();

            var securityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First(); //this is the only profile that allows the deletion of Recruitment Documents
            var userSecurityProfileId = commonMethodsDB.CreateUserSecurityProfile(_nonAdminUserId, securityProfileId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDocumentsSubPage();

            var systemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId, "systemuserid")["systemuserid"]).ToString());

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad();

            var recruitmentDocuments = dbHelper.compliance.GetComplianceByRegardingId(systemUserId);
            Assert.AreEqual(2, recruitmentDocuments.Count);
            var recruitmentDocument1Id = recruitmentDocuments[0];
            var recruitmentDocument2Id = recruitmentDocuments[1];

            systemUserRecruitmentDocumentsPage
                .ValidateRecordCellText(recruitmentDocument1Id.ToString(), 3, "Completed")
                .ValidateRecordCellText(recruitmentDocument2Id.ToString(), 3, "Completed");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRecruitmentDocumentsSubPage();

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad()
                .SelectRecord(recruitmentDocument1Id.ToString())
                .SelectRecord(recruitmentDocument2Id.ToString())
                .ClickDeletedButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId); //admin profile is no longer needed (delete opperation was completed)

            systemUserRecruitmentDocumentsPage
                .WaitForSystemUserRecruitmentDocumentsPageToLoad()
                .ClickRefreshButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString())
                .ValidateRecruitmentStatusCellText(_roleApplicationID.ToString(), "Induction")
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRecruitmentStatusSelectedText("Induction")
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();


            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()

                .ValidateMessageContainsText("Induction requirements are unmet. Missing:\r\n")
                .ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete")
                .ValidateMessageContainsText(_staffRecruitmentItemName2 + " - 1 item is not Complete")

                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:\r\n")
                .ValidateMessageContainsText(trainingItemName + " - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #region Execute Job 'Create Missing Recruitment Documents'

            var CreateMissingRecruitmentDocumentsId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Recruitment Documents")[0];
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(CreateMissingRecruitmentDocumentsId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(CreateMissingRecruitmentDocumentsId);
            System.Threading.Thread.Sleep(2000);

            #endregion

            complianceItems = dbHelper.compliance.GetComplianceByRegardingId(systemUserId);
            foreach (Guid complianceId in complianceItems)
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", DateTime.Now, DateTime.Now);

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:\r\n")
                .ValidateMessageContainsText(trainingItemName + " - 1 item is not Current")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3925")]
        [Description("Steps 8 & 9 from original test ACC-3914.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Role Applications")]
        [TestProperty("Screen2", "Recruitment Documents")]
        public void ACC_3914_UITestCases002()
        {
            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "ACC-3925", null, null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "ACC-3925_Role_Type";
            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3925_1_RI";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            var _staffRecruitmentItemName2 = "ACC-3925_2_RI";
            var _staffRecruitmentItemId2 = commonMethodsDB.CreateStaffRecruitmentItem(_teamId, _staffRecruitmentItemName2, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _staffRecruitmentItemName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 0, null, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateRecruitmentRequirement(_teamId, _staffRecruitmentItemName2, _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, false, 0, null, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_ACC-3925_" + currentTimeString;
            var _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _teamId);

            _applicantFullName = applicantFirstName + " Applicant_01";

            #endregion            

            #region Create Role Applicant

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _teamId, DateTime.Now, _teamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Non-admin user

            CreateNonAdminUser();

            #endregion

            #region Compliance 

            var complianceRecords_1 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId);
            var complianceId_1 = complianceRecords_1[0];
            dbHelper.compliance.UpdateCompliance(complianceId_1, 1593, "Test", DateTime.Now, _nonAdminUserId, null, null, null, null);

            var complianceRecords_2 = dbHelper.compliance.GetByRegardingIdAndComplianceItemId(_applicantId, _staffRecruitmentItemId2);
            var complianceId_2 = complianceRecords_2[0];
            dbHelper.compliance.UpdateCompliance(complianceId_2, 1593, "Test", DateTime.Now.AddDays(-5), _nonAdminUserId, DateTime.Now.AddDays(-4), _nonAdminUserId, null, DateTime.Now.AddDays(-2));

            #endregion

            #region Steps

            loginPage
                .GoToLoginPage()
                .Login(_nonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .ValidateRecruitmentDocumentsRecordIsPresent(complianceId_1.ToString())
                .ValidateRecordCellContent(complianceId_1.ToString(), 3, "Requested")

                .ValidateRecruitmentDocumentsRecordIsPresent(complianceId_2.ToString())
                .ValidateRecordCellContent(complianceId_2.ToString(), 3, "Expired");

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:\r\n")
                .ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete")
                .ValidateMessageContainsText(_staffRecruitmentItemName2 + " - 1 item is not Complete")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad();

            #endregion

        }

        #endregion

        public void CreateNonAdminUser()
        {
            #region Non-admin user

            var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First();
            var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First();
            var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First();
            var securityProfileId4 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First();
            var securityProfileId5 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)").First();
            var securityProfileId6 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Recruitment (Edit)").First();
            //var securityProfileId7 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Scheduling (Edit)").First();
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

            var securityProfilesList = new List<Guid> {securityProfileId1, securityProfileId2, securityProfileId3, securityProfileId4, securityProfileId5,
                securityProfileId6, securityProfileId8, securityProfileId9, securityProfileId10,
                securityProfileId11, securityProfileId12, securityProfileId13, securityProfileId14, securityProfileId15, securityProfileId16 };

            _nonAdminLoginUser = "ACC_3901_User1_" + currentTimeString;
            _nonAdminUserId = commonMethodsDB.CreateSystemUserRecord(_nonAdminLoginUser, "ACC-3901-User1", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid, securityProfilesList, 2);

            #endregion

        }

    }
}

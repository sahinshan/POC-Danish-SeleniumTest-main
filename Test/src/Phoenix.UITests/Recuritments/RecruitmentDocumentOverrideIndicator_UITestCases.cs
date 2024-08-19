using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// When User is permitted:can assign or remove this attribute on the Recruitment Documents
    /// </summary>
    [TestClass]
    public class RecruitmentDocumentOverrideIndicator_UITestCases : FunctionalTest
    {

        #region Properties
        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private string _careProvider_TeamName;
        private Guid _defaultLoginUserID;
        private Guid staffReviewsTeamEditSecurityProfileId;
        private Guid _applicantId;
        private string _applicantFullName;
        private string _careProviderStaffRoleName;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid;
        private string _employmentContractTypeName;
        public Guid environmentid;
        private string _loginUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _staffRecruitmentItemName;
        private Guid _staffRecruitmentItemId;
        private string _teamName;

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

                #region Authentication

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion Authentication

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProvider_TeamName = (string)dbHelper.team.GetTeamByID(_careProviders_TeamId, "name")["name"];

                #endregion

                #region Security Profiles

                staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();

                #endregion

                #region Createdefault system user

                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord("ACC_Test_User2", "Login_", "Automation_User", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];

                #endregion

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _teamName = (string)dbHelper.team.GetTeamByID(_careProviders_TeamId, "name")["name"];

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

        #region https: //advancedcsg.atlassian.net/browse/ACC-3915

        [TestProperty("JiraIssueID", "ACC-3915")]
        [Description("When User is permitted:can assign or remove this attribute on the Recruitment Documents When User is not permitted:can’t select value Override from Additional attributes can’t change the value Overriden, when it’s selected")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Role Applications")]
        public void RestrictedAccessToOverrideAttribute_UITestMethod001()
        {

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Care provider staff role type

            _careProviderStaffRoleName = "ACC-3915";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "CDV6-17752...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> {
                                            _careProviderStaffRoleTypeid
                                             };

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3915";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecACC-3915";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeName = "ACC-3915_Contract";
            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, _employmentContractTypeName, "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "ACC-3915_Training", new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup("ACC-3915_Training", staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement("ACC-3915_Training - Internal", _careProviders_TeamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Non - admin user

            var securityProfilesList = new List<Guid>();
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Recruitment (Edit)").First());
            //securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Scheduling (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Training (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Training Setup (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Team Membership (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Fully Accept Recruitment Application").First());
            securityProfilesList.Add(dbHelper.securityProfile.GetSecurityProfileByName("Set Override Recruitment Document Attribute").First());

            var NonAdminLoginUser = "ACC-3915-User1";
            var NonAdminUserId = commonMethodsDB.CreateSystemUserRecord(NonAdminLoginUser, "ACC-3915", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid, securityProfilesList, 2);

            #endregion

            #region Step 1 open applicant and open role application

            loginPage
              .GoToLoginPage()
              .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            var applicantFirstName = "NonAdminUser3915_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var applicantLastName = "LN_Applicant01";

            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            mainMenu
              .WaitForMainMenuToLoad(true, false, true, true, true, true)
              .NavigateToApplicantsPage();

            applicantPage
              .WaitForApplicantsPageToLoad()
              .SearchApplicantRecord(applicantFirstName)
              .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
              .WaitForApplicantRecordPagePageToLoad()
              .NavigateToRoleApplicationsPage();

            #endregion

            #region Step 2 verify recruitment document status outstanding

            roleApplicationsPage
              .WaitForRoleApplicationsPageToLoad()
              .ClickNewRecordButton();

            roleApplicationRecordPage
              .WaitForRoleApplicationRecordPageToLoad()
              .ValidateApplicantName(_applicantFullName)
              .ValidateRecruitmentStatusSelectedText("Pending")
              .ClickRoleLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery(_careProviderStaffRoleName)
              .TapSearchButton()
              .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            roleApplicationRecordPage
              .WaitForRoleApplicationRecordPageToLoad()
              .ClickTargetTeamLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery(_teamName)
              .TapSearchButton()
              .SelectResultElement(_careProviders_TeamId.ToString());

            roleApplicationRecordPage
              .WaitForRoleApplicationRecordPageToLoad()
              .ClickContractTypeLookupButton();

            lookupPopup
              .WaitForLookupPopupToLoad()
              .TypeSearchQuery(_employmentContractTypeName)
              .TapSearchButton()
              .SelectResultElement(_employmentContractTypeid.ToString());

            roleApplicationRecordPage
              .WaitForRoleApplicationRecordPageToLoad()
              .ClickSaveButton()
              .WaitForRoleApplicationRecordPageToLoad()
              .ValidateApplicantName(_applicantFullName)
              .ValidateRecruitmentStatusSelectedText("Pending")
              .ValidateProgressTowardsFullyAcceptedFieldValue("0")
              .ValidateProgressTowardsInductionStatusFieldValue("0")
              .ClickBackButton();

            #endregion

            #region Step 3 Navigate to Recruitment documents and changes status to override

            applicantRecordPage
              .WaitForApplicantRecordPagePageToLoad()
              .NavigateToRecruitmentDocumentsTab();

            var complicanceId1 = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName)[0];

            recruitmentDocumentsPage
              .WaitForRecruitmentDocumentsPageToLoad()
              .OpenRecruitmentDocumentsRecord(complicanceId1.ToString());

            recruitmentDocumentsRecordPage
              .WaitForRecruitmentDocumentsRecordPageToLoad()
              .ValidateStatusSelectedText("Outstanding")
              .SelectVariationOption("Override - doc not required")
              .ClickSaveButton()
              .WaitForRecruitmentDocumentsRecordPageToLoad()
              .ValidateVariationSelectedText("Override - doc not required");

            #endregion

            #region Step 4 recruitment document should be complete the status by updating the Override

            recruitmentDocumentsRecordPage
              .ValidateStatusSelectedText("Completed")
              .ClickBackButton();

            #endregion

            #region Step 5 verify Role Application Induction and fully accepted of status value should be Zero

            applicantRecordPage
              .WaitForApplicantRecordPagePageToLoad()
              .NavigateToRoleApplicationsPage();

            var roleApplicationsForApplicant = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId).FirstOrDefault();

            roleApplicationsPage
              .WaitForRoleApplicationsPageToLoad()
              .OpenRoleApplicationRecord(roleApplicationsForApplicant.ToString());

            roleApplicationRecordPage
              .WaitForRoleApplicationRecordPageToLoad()
              .ValidateProgressTowardsFullyAcceptedFieldValue("0")
              .ValidateProgressTowardsInductionStatusFieldValue("100");

            #endregion

        }
    }
}
#endregion
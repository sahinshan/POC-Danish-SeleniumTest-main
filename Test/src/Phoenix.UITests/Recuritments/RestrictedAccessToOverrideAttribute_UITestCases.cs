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
    public class RestrictedAccessToOverrideAttribute_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private string _teamName;
        private Guid _defaultLoginUserID;
        private Guid _applicantId;
        private Guid _applicantId2;
        private string _applicantFullName;
        private string _careProviderStaffRoleName;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid;
        private string _employmentContractTypeName;
        private string _loginUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string applicantLastName2;
        private string _applicantFullName2;
        private Guid _applicantId3;
        private string _applicantFullName3;

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

                _teamName = "CareProviders";
                _careProviders_TeamId = commonMethodsDB.CreateTeam(_teamName, null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Createdefault system user

                _loginUsername = "ACC_Test_User2";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "Login_", "Automation_User", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

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

        #region https: //advancedcsg.atlassian.net/browse/ACC-3838

        [TestProperty("JiraIssueID", "ACC-3838")]
        [Description("When User is permitted:can assign or remove this attribute on the Recruitment Documents When User is not permitted:can’t select value Override from Additional attributes can’t change the value Overriden, when it’s selected")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Role Applications")]
        [TestProperty("Screen3", "Recruitment Documents")]
        public void RestrictedAccessToOverrideAttribute_UITestMethod001()
        {

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Care provider staff role type

            _careProviderStaffRoleName = "ACC-3838";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "CDV6-17752...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> {
            _careProviderStaffRoleTypeid
            };

            #endregion

            #region Staff Recruitment Item

            var _staffRecruitmentItemName = "ACC-3838";
            var _staffRecruitmentItemId = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2023, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            var _recruitmentRequirementName = "RecACC-3838";
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _recruitmentRequirementName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Employment Contract Type

            _employmentContractTypeName = "ACC-3838_Contract";
            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, _employmentContractTypeName, "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Training Item

            Guid staffTrainingItem = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, "ACC-3838_Training", new DateTime(2022, 1, 1));

            commonMethodsDB.CreateTrainingRequirementSetup("ACC-3838_Training", staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement("ACC-3838_Training - Internal", _careProviders_TeamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Non - admin user

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

            var securityProfilesList = new List<Guid> { securityProfileId1, securityProfileId2, securityProfileId3, securityProfileId4,
                                                        securityProfileId5, securityProfileId6, securityProfileId7, securityProfileId8,
                                                        securityProfileId9, securityProfileId10, securityProfileId11, securityProfileId12,
                                                        securityProfileId13, securityProfileId14, securityProfileId15};

            var NonAdminLoginUser = "ACC_3838_User1_" + currentTimeString;
            var NonAdminUserId = commonMethodsDB.CreateSystemUserRecord(NonAdminLoginUser, "ACC-3838-User1", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid, securityProfilesList, 2);


            //var NonAdminLoginUser = "ACC-3838-User1";
            //var NonAdminUserId = commonMethodsDB.CreateSystemUserRecord(NonAdminLoginUser, "ACC-3901", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            //dbHelper.systemUser.UpdateEmployeeTypeId(NonAdminUserId, 2);

            //var securityProfileExists = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(NonAdminUserId, securityProfileId18).Any();

            //if (securityProfileExists)
            //    dbHelper.userSecurityProfile.DeleteUserSecurityProfile(dbHelper.userSecurityProfile.GetByUserIDAndProfileId(NonAdminUserId, securityProfileId18).First());

            //foreach (var userid in dbHelper.systemUser.GetSystemUserByUserName(NonAdminLoginUser))
            //    dbHelper.userSecurityProfile.CreateMultipleUserSecurityProfile(NonAdminUserId, securityProfilesList);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true);

            #endregion

            #region Step 2 create applicant and add role application

            var applicantFirstName = "NonAdminUser3_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var applicantLastName = "LN_Applicant";

            mainMenu
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(applicantFirstName)
                .InsertLastName(applicantLastName)
                .InsertAvailableFromDateField(DateTime.Now.AddYears(-30).ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton();

            System.Threading.Thread.Sleep(2000);
            _applicantFullName = applicantFirstName + " " + applicantLastName;

            applicantRecordPage
                .NavigateToRoleApplicationsPage();

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
                .ValidateApplicantName(_applicantFullName)
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ClickBackButton();

            _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            #endregion

            #region Step 3 Navigate to Recruitment documents

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var complicanceId1 = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName).FirstOrDefault();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(complicanceId1.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Override - doc not required")
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

            #region step 4 navigate already overridden documnet

            //create applicant2
            var applicantFirstName2 = "NonAdmin02";
            applicantLastName2 = "User" + currentTimeString;
            _applicantId2 = dbHelper.applicant.CreateApplicant(applicantFirstName2, applicantLastName2, _careProviders_TeamId);
            _applicantFullName2 = (string)dbHelper.applicant.GetApplicantByID(_applicantId2, "fullname")["fullname"];
            dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId2, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName2);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName2)
                .OpenApplicantRecord(_applicantId2.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var complicanceId2 = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId2, _staffRecruitmentItemName);
            foreach (Guid complianceId in complicanceId2)
                dbHelper.compliance.UpdateAdditionalAttributeId(complianceId, 1);

            var complicanceIdNew = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId2, _staffRecruitmentItemName).FirstOrDefault();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(complicanceIdNew.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Document unsuitable")
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

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .ClickSignOutButton();

            #endregion

            #region Step 5 Create an system user and repeat steps 3 to 4

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true);

            #endregion

            #region Step 2 create an Applicant

            var applicantFirstName3 = "AdminUser3_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var applicantLastName3 = "LN_Applicant";

            mainMenu
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ClickAddButton();

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .InsertFirstName(applicantFirstName3)
                .InsertLastName(applicantLastName3)
                .InsertAvailableFromDateField(DateTime.Now.AddYears(-30).ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            _applicantFullName3 = applicantFirstName3 + " " + applicantLastName3;

            applicantRecordPage
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickNewRecordButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName3)
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
                .ValidateApplicantName(_applicantFullName3)
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ClickBackButton();

            _applicantId3 = dbHelper.applicant.GetByFirstName(applicantFirstName3)[0];

            #endregion

            #region Step 3 Navigate to Recruitment documents

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRecruitmentDocumentsTab();

            var complicanceId3 = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId3, _staffRecruitmentItemName).FirstOrDefault();

            recruitmentDocumentsPage
                .WaitForRecruitmentDocumentsPageToLoad()
                .OpenRecruitmentDocumentsRecord(complicanceId3.ToString());

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Override - doc not required")
                .ClickSaveButton();

            #endregion

            #region Step 4 Navigate to already overridden documnet 

            recruitmentDocumentsRecordPage
                .WaitForRecruitmentDocumentsRecordPageToLoad()
                .SelectVariationOption("Document unsuitable")
                .ClickSaveButton();

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .ClickSignOutButton();

            #endregion

        }

        #endregion
    }
}

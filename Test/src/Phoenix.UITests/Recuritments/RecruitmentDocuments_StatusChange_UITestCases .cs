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
    public class RecruitmentDocuments_StatusChange_UITestCases : FunctionalTest
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
        private string _loginUsername;
        private string _applicantFullName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _careProviderStaffRoleTypeName;
        private Guid _careProviderStaffRoleTypeId;
        private string _staffRecruitmentItem1Name;
        private Guid _staffRecruitmentItem1Id;
        private String CurrentDateValue = DateTime.Today.ToString("dd'/'MM'/'yyyy");
        private Guid _defaultCareProviderStaffRoleTypeid;

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

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _teamName = (string)dbHelper.team.GetTeamByID(_careProviders_TeamId, "name")["name"];

                #endregion

                #region Create default system user

                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord("ACC_Test_User2", "Login_", "Automation_User", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);
                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];

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

        #region https: //advancedcsg.atlassian.net/browse/ACC-3719

        [TestProperty("JiraIssueID", "ACC-3719")]
        [Description("Login CD >Workplace >Recruitment > open Active Applicant record > In Role Application tab section click on + button and Open New  Role Application and Select the Role and Enter the all mandatory fields hit the save button and System is creating Default Recruitment Document in Outstanding status ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Documents")]
        [TestProperty("Screen2", "Recruitment Document Attachments")]
        public void RecruitmentDocuments_StatusChange01()
        {
            #region role creation

            _careProviderStaffRoleTypeName = "SRT_3719";
            _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleTypeName, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItem1Name = "SRI_3719";
            _staffRecruitmentItem1Id = commonMethodsDB.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItem1Name, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            foreach (var rrId in dbHelper.recruitmentRequirement.GetByAllRoles(true))
                dbHelper.recruitmentRequirement.UpdateAllRoles(rrId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeId };
            var recruitmentRequirementId = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItem1Name, _staffRecruitmentItem1Id, "staffrecruitmentitem", _staffRecruitmentItem1Name, false, 1, 3, 1, 3, DateTime.Now.Date, null, careProviderStaffRoleTypeIds);

            #endregion

            #region Create Applicant

            var applicantFirstName = "ACC_3719";
            var applicantLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            _applicantFullName = applicantFirstName + " " + applicantLastName;

            #endregion

            #region Step 1

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
              .TypeSearchQuery(_careProviderStaffRoleTypeName)
              .TapSearchButton()
              .SelectResultElement(_careProviderStaffRoleTypeId.ToString());

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
              .ValidateRecruitmentStatusSelectedText("Pending")
              .ClickSaveButton()
              .WaitForRecordToBeSaved()
              .ValidateApplicantName(_applicantFullName)
              .ValidateRecruitmentStatusSelectedText("Pending")
              .ClickBackButton();

            #endregion

            #region Step 2

            applicantRecordPage
              .WaitForApplicantRecordPagePageToLoad()
              .NavigateToRecruitmentDocumentsTab();

            var complicanceId2 = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItem1Name).FirstOrDefault();

            recruitmentDocumentsPage
              .WaitForRecruitmentDocumentsPageToLoad()
              .OpenRecruitmentDocumentsRecord(complicanceId2.ToString());

            recruitmentDocumentsRecordPage
              .WaitForRecruitmentDocumentsRecordPageToLoad()
              .InsertRequestedDateValue(CurrentDateValue)
              .InsertCompletedDateValue(CurrentDateValue)
              .InsertReferenceNumber("RefNo ACC-3719")
              .InsertRefereeName("Suchi")
              .InsertRefereePhone("9988776611")
              .InsertRefereeEmail("mail01@somemail.com")
              .InsertRefereeAddress("No 11, Street S, Ch-90")
              .ClickSaveButton()
              .WaitForRecruitmentDocumentsRecordPageToLoad()
              .WaitForRecordToBeSaved()
              .ValidateComplianceItemName(_staffRecruitmentItem1Name)
              .ValidateStatusSelectedText("Completed")
              .ValidateResponsibleTeamName("CareProviders")
              .ValidateRegardingApplicantName(_applicantFullName)
              .ValidateReferenceNumberFieldValue("RefNo ACC-3719")
              .ValidateRefereeNameFieldValue("Suchi")
              .ValidateRefereePhoneFieldValue("9988776611")
              .ValidateRefereeEmailFieldValue("mail01@somemail.com")
              .ValidateRefereeAddressFieldValue("No 11, Street S, Ch-90")
              .ClickBackButton();

            #endregion

            #region Step 3

            applicantRecordPage
              .WaitForApplicantRecordPagePageToLoad()
              .NavigateToRoleApplicationsPage();

            var roleApplicationsForApplicant = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId).FirstOrDefault();

            roleApplicationsPage
              .WaitForRoleApplicationsPageToLoad()
              .ValidateRoleApplicationRecordIsPresent(roleApplicationsForApplicant.ToString())
              .OpenRoleApplicationRecord(roleApplicationsForApplicant.ToString());

            roleApplicationRecordPage
              .WaitForRoleApplicationRecordPageToLoad()
              .ValidateRecruitmentStatusFieldAlertMessage("All recruitment requirements have been met for the status Fully Accepted.");

            #endregion

        }

        #endregion
    }

}
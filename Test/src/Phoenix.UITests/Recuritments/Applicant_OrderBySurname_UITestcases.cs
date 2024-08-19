using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts forAll Applicants view - order by the surname
    /// </summary>
    [TestClass]
    public class Applicant_OrderBySurname_UITestcases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc"); //Internal
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _applicantId1;
        private Guid _applicantId2;
        private string applicantLastName1;
        private string applicantLastName2;
        private Guid _careProviderStaffRoleTypeid;
        public Guid environmentid;
        private string _loginUsername;
        private Guid _recurrencePatternId;
        private Guid _recurrencePatternId_every2Weeks;
        private string _currentDayOfTheWeek;
        private string _applicantFullName1;
        private string _applicantFullName2;
        private Guid _roleApplicationID1;
        private Guid _roleApplicationID2;
        private Guid _employmentContractTypeid;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Create  default system user

                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord("ACC_AdminUser_3483", "CW", "Admin_Test_User_1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
                _loginUsername = "ACC_AdminUser_3483";

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region Applicant

                //create applicant1
                var applicantFirstName1 = "000TC_3483";
                applicantLastName1 = "0AaUsser_" + currentTimeString;
                _applicantId1 = dbHelper.applicant.CreateApplicant(applicantFirstName1, applicantLastName1, _careProviders_TeamId);
                _applicantFullName1 = (string)dbHelper.applicant.GetApplicantByID(_applicantId1, "fullname")["fullname"];

                //create applicant2
                var applicantFirstName2 = "000TC_3483";
                applicantLastName2 = "0AaUser" + currentTimeString;
                _applicantId2 = dbHelper.applicant.CreateApplicant(applicantFirstName2, applicantLastName2, _careProviders_TeamId);
                _applicantFullName2 = (string)dbHelper.applicant.GetApplicantByID(_applicantId2, "fullname")["fullname"];

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type

                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 20509 " + currentTimeString, "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region complianceItems

                var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId1);

                foreach (Guid complianceId in complianceItems)
                    dbHelper.compliance.UpdateCompliance(complianceId, 2999, "Test", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultLoginUserID, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultLoginUserID);

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

        #region https: //advancedcsg.atlassian.net/browse/ACC-3483

        [TestProperty("JiraIssueID", "ACC-3483")]
        [Description("Applicants pending and ready for induction view must be displayedNames should be sorted by Surname")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "All Role Applications")]
        public void Applicant_RoleApplication()
        {
            _roleApplicationID1 = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId1, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName1);
            _roleApplicationID2 = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId2, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName2);

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
              .WaitForAllRoleApplicationsToLoad()
              .SwitchToApplicantsPendingFrame()
              .ValidateApplicantFullNameInApplicantsPendingSection(_applicantFullName1, true)
              .ValidateApplicantFullNameInApplicantsPendingSection(_applicantFullName2, true);

            #endregion

            UpdateApplicantToInduction(applicantLastName1, _applicantId1, _roleApplicationID1);
            UpdateApplicantToInduction(applicantLastName2, _applicantId2, _roleApplicationID2);

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToAllRoleApplicationsPage();

            allRoleApplicationsPage
              .WaitForAllRoleApplicationsToLoad()
              .SwitchToReadyForInductionFrame()
              .ValidateSystemUserFullNameInReadyForInductionSection(_applicantFullName1, true)
              .ValidateSystemUserFullNameInReadyForInductionSection(_applicantFullName2, true);

            #region Step 3

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToSystemUserSection();

            var newSystemUserId = new Guid((dbHelper.applicant.GetApplicantByID(_applicantId1, "systemuserid")["systemuserid"]).ToString());

            systemUsersPage
              .WaitForSystemUsersPageToLoad()
              .InsertName(_applicantFullName1)
              .ClickSearchButton()
              .OpenRecord(newSystemUserId.ToString());

            systemUserRecordPage
              .WaitForSystemUserRecordPageToLoad()
              .NavigateToRollApplicationsSubPage();

            systemUserRecordPage
              .WaitForSystemUserRecordPageToLoad()
              .NavigateToRecruitmentDocumentsSubPage();

            #endregion
        }

        #endregion

        private void UpdateApplicantToInduction(string applicantLastName, Guid _applicantId, Guid _roleApplicationID)
        {
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
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString())
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickContractTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Employment Contract Type 20509 " + currentTimeString)
                .SelectResultElement(_employmentContractTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

        }
    }
}
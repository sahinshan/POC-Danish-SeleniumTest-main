using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for
    /// </summary>
    [TestClass]
    public class RoleApplications_UITestCases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _careProviders_ResponsibleTeam;
        private Guid _careProviders_TargetTeam;
        private Guid _defaultLoginUserID;
        private Guid _loginUserID;
        private Guid _applicantId;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _roleApplicant;
        private Guid systemAdministratorSecurityProfileId;
        private Guid systemUserSecureFieldsSecurityProfileId;
        private Guid _applicationSourceId;
        private Guid _rejectedReasonId;
        private string _loginUsername;
        private string _loginUserFullName;
        private string _applicantFullName;
        private string _userName = "Testing_CDV6_20507_User_01";
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
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

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Security Profiles

                systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                #endregion

                #region Create default system user

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName(_userName).Any();
                if (!defaultLoginUserExists)
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(_userName, "Testing", "CDV6_20507", "Testing CDV6_20507", "Passw0rd_!", "Testing_CDV6_20507@somemail.com", "Testing_CDV6_20507@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4);

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName(_userName).FirstOrDefault();

                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];
                _loginUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-20507

        [TestProperty("JiraIssueID", "ACC-3517")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application" +
                     "Validate Mandatory Fields, Blank Text and Field Link Text.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]
        public void RoleApplications_UITestCases001()
        {
            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_20507_01";
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant("Testing_CDV6_20507_01", "Applicant_01", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion 

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
                .ValidateMandatoryFields()
                .ValidateBlankFields()
                .ValidateRecruitmentStatus_Field_Disabled(true)
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ValidateApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateResponsibleRecruiterName(_loginUserFullName);

        }

        [TestProperty("JiraIssueID", "ACC-3518")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application" +
                     "Click on All Lookup button and Validate Lookup field Value & Validate Future Date Alert message.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]
        public void RoleApplications_UITestCases002()
        {
            #region Create default system user

            _userName = "Testing_CDV6_20507_User_02";

            var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName(_userName).Any();
            if (!defaultLoginUserExists)
                _loginUserID = dbHelper.systemUser.CreateSystemUser(_userName, "Testing", "CDV6_20507", "Testing CDV6_20507", "Passw0rd_!", "Testing_CDV6_20507@somemail.com", "Testing_CDV6_20507@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4);

            if (Guid.Empty == _loginUserID)
                _loginUserID = dbHelper.systemUser.GetSystemUserByUserName(_userName).FirstOrDefault();

            _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_loginUserID, "username")["username"];
            _loginUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_loginUserID, "fullname")["fullname"];
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_loginUserID, DateTime.Now.Date);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_20507_02";
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant("Testing_CDV6_20507_02", "Applicant_02", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_20507_Role_2" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            var careProviderStaffRoleTypeExists = dbHelper.careProviderStaffRoleType.GetByName(careProviderRoleName).Any();
            if (!careProviderStaffRoleTypeExists)
            {
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);
            }
            if (_careProviderStaffRoleTypeid == Guid.Empty)
            {
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.GetByName(careProviderRoleName).FirstOrDefault();
            }

            #endregion

            #region Create Responsible Team

            string responsibleTeamName = "CDV6_20507_Responsible_Team";
            var teamsExist = dbHelper.team.GetTeamIdByName(responsibleTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(responsibleTeamName, null, _careProviders_BusinessUnitId, "90400", responsibleTeamName + "@careworkstempmail.com", responsibleTeamName, "020 123456");
            _careProviders_ResponsibleTeam = dbHelper.team.GetTeamIdByName(responsibleTeamName)[0];

            #endregion

            #region Create Target Team

            string TargetTeamName = "CDV6_20507_Target_Team";
            teamsExist = dbHelper.team.GetTeamIdByName(TargetTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "90400", TargetTeamName + "@careworkstempmail.com", TargetTeamName, "020 123456");
            _careProviders_TargetTeam = dbHelper.team.GetTeamIdByName(TargetTeamName)[0];

            #endregion

            #region Create Application Source

            string ApplicationSourceName = "CDV6_20507_Application_Source";
            var ApplicationSourceNameExist = dbHelper.applicationSource.GetApplicationSourceIdByName(ApplicationSourceName).Any();
            if (!ApplicationSourceNameExist)
                dbHelper.applicationSource.CreateApplicationSource(ApplicationSourceName, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);
            _applicationSourceId = dbHelper.applicationSource.GetApplicationSourceIdByName(ApplicationSourceName)[0];

            #endregion


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
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateResponsibleRecruiterName(_loginUserFullName)
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderRoleName)
                .TapSearchButton()
                .ValidateResultElementPresent(_careProviderStaffRoleTypeid.ToString())
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRoleName(careProviderRoleName)
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(responsibleTeamName)
                .TapSearchButton()
                .ValidateResultElementPresent(_careProviders_ResponsibleTeam.ToString())
                .SelectResultElement(_careProviders_ResponsibleTeam.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateResponsibleTeamName(responsibleTeamName)
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName)
                .TapSearchButton()
                .ValidateResultElementPresent(_careProviders_TargetTeam.ToString())
                .SelectResultElement(_careProviders_TargetTeam.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateTargetTeamName(TargetTeamName)
                .ClickApplicationSourceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ApplicationSourceName)
                .TapSearchButton()
                .ValidateResultElementPresent(_applicationSourceId.ToString())
                .SelectResultElement(_applicationSourceId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicationSourceName(ApplicationSourceName);

            roleApplicationRecordPage
                .InsertApplicationDateValue(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton(); System.Threading.Thread.Sleep(3000);

            dynamicDialogPopup
              .WaitForDynamicDialogPopupToLoad()
              .ValidateMessage("Application Date cannot be a future date.")
              .TapCloseButton();


        }

        [TestProperty("JiraIssueID", "ACC-3519")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> Create Role Application and Change Status to Rejected" +
                     "Validate Rejected Reason & Additional Comment section" +
                     "Click on Rejected Reason Lookup button and Validate Rejected Reason and Save Role Application" +
                     "Validate Saved Records with Rejected Status.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]

        public void RoleApplications_UITestCases003()
        {
            #region Create default system user

            _userName = "Testing_CDV6_20507_User_03";

            var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName(_userName).Any();
            if (!defaultLoginUserExists)
                _loginUserID = dbHelper.systemUser.CreateSystemUser(_userName, "Testing", "CDV6_20507", "Testing CDV6_20507", "Passw0rd_!", "Testing_CDV6_20507@somemail.com", "Testing_CDV6_20507@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4);

            if (Guid.Empty == _loginUserID)
                _loginUserID = dbHelper.systemUser.GetSystemUserByUserName(_userName).FirstOrDefault();

            _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_loginUserID, "username")["username"];
            _loginUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_loginUserID, "fullname")["fullname"];
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_loginUserID, DateTime.Now.Date);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_20507_03";
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant("Testing_CDV6_20507_03", "Applicant_03", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_20507_Role_3" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create Target Team

            string TargetTeamName = "CDV6_20507_Target_Team";
            var teamsExist = dbHelper.team.GetTeamIdByName(TargetTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "90400", TargetTeamName + "@careworkstempmail.com", TargetTeamName, "020 123456");
            _careProviders_TargetTeam = dbHelper.team.GetTeamIdByName(TargetTeamName)[0];

            #endregion

            #region Create Rejected Reason

            string RejectedReason = "Not Available";
            var rejectedReasonExist = dbHelper.rejectedReason.GetByName(RejectedReason).Any();
            if (!rejectedReasonExist)
                dbHelper.rejectedReason.CreateRejectedReason(RejectedReason, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);
            _rejectedReasonId = dbHelper.rejectedReason.GetByName(RejectedReason)[0];

            #endregion

            #region Delete Role Application

            foreach (var roleApplicantId in dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId))
            {
                foreach (var workSheduleId in dbHelper.userWorkSchedule.GetUserRoleApplicantID(_applicantId))
                    dbHelper.userWorkSchedule.DeleteUserWorkSchedule(workSheduleId);

                dbHelper.recruitmentRoleApplicant.DeleteRecruitmentRoleApplicant(roleApplicantId);
            }

            #endregion

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
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateResponsibleRecruiterName(_loginUserFullName)
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderRoleName)
                .TapSearchButton()
                .ValidateResultElementPresent(_careProviderStaffRoleTypeid.ToString())
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRoleName(careProviderRoleName)
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName)
                .TapSearchButton()
                .ValidateResultElementPresent(_careProviders_TargetTeam.ToString())
                .SelectResultElement(_careProviders_TargetTeam.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateTargetTeamName(TargetTeamName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved(); System.Threading.Thread.Sleep(3000);

            roleApplicationRecordPage
                .SelectRecruitmentStatus("Rejected")
                .ValidateRejectedReason_Mandatory_Field()
                .ValidateAdditonalComments_Field()
                .ClickRejectedReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(RejectedReason)
                .TapSearchButton()
                .ValidateResultElementPresent(_rejectedReasonId.ToString())
                .SelectResultElement(_rejectedReasonId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRejectedReasonName(RejectedReason)
                .InsertRejctionAdditonalComments("Not Available")
                .ClickSaveAndCloseButton(); System.Threading.Thread.Sleep(3000);

            _roleApplicant = dbHelper.recruitmentRoleApplicant.GetByName(careProviderRoleName).FirstOrDefault();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateRecruitmentStatusCellText(_roleApplicant.ToString(), "Rejected");

        }

        [TestProperty("JiraIssueID", "ACC-3520")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> Create new role application" +
                     "Validate Created recods fields through Advanced Search of Role Application")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Advanced Search")]

        public void RoleApplications_UITestCases004()
        {
            #region Create default system user

            _userName = "Testing_CDV6_20507_User_04";

            var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName(_userName).Any();
            if (!defaultLoginUserExists)
                _loginUserID = dbHelper.systemUser.CreateSystemUser(_userName, "Testing", "CDV6_20507", "Testing CDV6_20507", "Passw0rd_!", "Testing_CDV6_20507@somemail.com", "Testing_CDV6_20507@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4);

            if (Guid.Empty == _loginUserID)
                _loginUserID = dbHelper.systemUser.GetSystemUserByUserName(_userName).FirstOrDefault();

            _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_loginUserID, "username")["username"];
            _loginUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_loginUserID, "fullname")["fullname"];
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_loginUserID, DateTime.Now.Date);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_20507_04";
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant("Testing_CDV6_20507_04", "Applicant_04", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_20507_Role_4" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create Target Team

            string TargetTeamName = "CDV6_20507_Target_Team";
            var teamsExist = dbHelper.team.GetTeamIdByName(TargetTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "90400", TargetTeamName + "@careworkstempmail.com", TargetTeamName, "020 123456");
            _careProviders_TargetTeam = dbHelper.team.GetTeamIdByName(TargetTeamName)[0];

            #endregion

            #region Create Role Applicant

            _roleApplicant = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _loginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TargetTeam, 1, "applicant", _applicantFullName);

            #endregion

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("Role Applications")

               .SelectFilter("1", "Applicant")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(applicantFirstName).TapSearchButton().SelectResultElement(_applicantId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_roleApplicant.ToString())
                .ValidateSearchResultRecordCellContent(_roleApplicant.ToString(), 2, _applicantFullName)
                .ValidateSearchResultRecordCellContent(_roleApplicant.ToString(), 3, careProviderRoleName)
                .ValidateSearchResultRecordCellContent(_roleApplicant.ToString(), 4, TargetTeamName)
                .ValidateSearchResultRecordCellContent(_roleApplicant.ToString(), 5, DateTime.Now.ToString("dd\\/MM\\/yyyy"))
                .ValidateSearchResultRecordCellContent(_roleApplicant.ToString(), 6, "Pending")
                .ValidateSearchResultRecordCellContent(_roleApplicant.ToString(), 7, _loginUserFullName);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-20845

        [TestProperty("JiraIssueID", "ACC-3521")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application" +
                     "Validate Status of Role Application on Create.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]

        public void RoleApplications_UITestCases005()
        {
            #region Create default system user

            _userName = "Testing_CDV6_12793_User";

            var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName(_userName).Any();
            if (!defaultLoginUserExists)
                _loginUserID = dbHelper.systemUser.CreateSystemUser(_userName, "Testing", "CDV6_12793", "Testing CDV6_12793", "Passw0rd_!", "Testing_CDV6_12793@somemail.com", "Testing_CDV6_12793@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4, null, DateTime.Now.Date);

            if (Guid.Empty == _loginUserID)
                _loginUserID = dbHelper.systemUser.GetSystemUserByUserName(_userName).FirstOrDefault();

            _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_loginUserID, "username")["username"];
            _loginUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_loginUserID, "fullname")["fullname"];
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_loginUserID, DateTime.Now.Date);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_12793";
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant("Testing_CDV6_12793", "Applicant_01", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion 

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_12793_Role" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create Target Team

            string TargetTeamName = "CDV6_12793_Target_Team";
            var teamsExist = dbHelper.team.GetTeamIdByName(TargetTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "90400", TargetTeamName + "@careworkstempmail.com", TargetTeamName, "020 123456");
            _careProviders_TargetTeam = dbHelper.team.GetTeamIdByName(TargetTeamName)[0];

            #endregion

            #region Delete Role Application

            foreach (var roleApplicantId in dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId))
            {
                foreach (var workSheduleId in dbHelper.userWorkSchedule.GetUserRoleApplicantID(_applicantId))
                    dbHelper.userWorkSchedule.DeleteUserWorkSchedule(workSheduleId);

                dbHelper.recruitmentRoleApplicant.DeleteRecruitmentRoleApplicant(roleApplicantId);
            }

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
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickNewRecordButton();

            #endregion

            #region Step 3 & 4

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatus_Field_Disabled(true)
                .ValidateRecruitmentStatusSelectedText("Pending");

            #endregion 

            #region Step 5

            roleApplicationRecordPage
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderRoleName)
                .TapSearchButton()
                .ValidateResultElementPresent(_careProviderStaffRoleTypeid.ToString())
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRoleName(careProviderRoleName)
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName)
                .TapSearchButton()
                .ValidateResultElementPresent(_careProviders_TargetTeam.ToString())
                .SelectResultElement(_careProviders_TargetTeam.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateTargetTeamName(TargetTeamName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved(); System.Threading.Thread.Sleep(3000);

            roleApplicationRecordPage
                .ValidateRecruitmentStatus_Field_ReadOnly(false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3868

        [TestProperty("JiraIssueID", "ACC-3879")]
        [Description("Add Target Team to Application")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]
        [TestProperty("Screen2", "Advanced Search")]
        public void RoleApplications_UITestCases006()
        {
            #region Create default system user
            dbHelper = new DBHelper.DatabaseHelper(tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            _userName = "ACC3868User";
            _loginUserID = commonMethodsDB.CreateSystemUserRecord(_userName, "ACC3879", "User", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);
            #endregion

            #region Create Applicant 

            var applicantFirstName = "ACC3879";
            var applicantLastName = "LN_" + currentTimeString;
            _applicantFullName = applicantFirstName + " " + applicantLastName;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            #endregion

            #region Care provider staff role type
            string careProviderStaffRoleName = "Associate 3879";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Associate ...");

            string careProviderStaffRoleName2 = "Staff 3879";
            var _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderStaffRoleName2, null, null, new DateTime(2021, 1, 1), "Staff ...");

            #endregion

            #region Create Target Team

            string TargetTeamName = "3879TargetTeam";
            _careProviders_TargetTeam = commonMethodsDB.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "93879", "3879TargetTeam@careworkstempmail.com", "3879TargetTeam", "012 123456");

            string TargetTeamName2 = "3879TargetTeamB";
            var _careProviders_TargetTeam2 = commonMethodsDB.CreateTeam(TargetTeamName2, null, _careProviders_BusinessUnitId, "938791", "3879TargetTeamB@careworkstempmail.com", "3879BTargetTeam", "012 123456");

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Step 1 & 2

            loginPage
              .GoToLoginPage()
              .Login(_userName, "Passw0rd_!", EnvironmentName);

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
                .ClickNewRecordButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateTargetBusinessUnitFieldIsNotDisplayed()
                .ValidateTargetTeamFieldLabelIsVisible(true)
                .ValidateTargetTeam_Mandatory_Field()
                .ValidateTargetTeamLookupButtonIsEnabled(true)
                .ValidateTargetTeamFieldsAreVisible(true);

            roleApplicationRecordPage
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderStaffRoleName)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton()
                .ValidateTargetTeamFieldFormError("Please fill out this field.");

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .ValidateResultElementPresent(_careProviders_TeamId.ToString())
                .SelectResultElement(_careProviders_TeamId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateTargetTeamName("CareProviders")
                .ClickContractTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Contracted")
                .TapSearchButton()
                .SelectResultElement(_employmentContractTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateRoleName(careProviderStaffRoleName)
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateTargetTeamName("CareProviders");

            #endregion

            #region Step 3
            roleApplicationRecordPage
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickNewRecordButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderStaffRoleName)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is not possible to have duplicate applications. " + _applicantFullName + " has an active application for " + careProviderStaffRoleName + " and CareProviders.")
                .TapCloseButton();

            #endregion

            #region Step 4
            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TargetTeam.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateTargetTeamName(TargetTeamName);

            #endregion

            #region Step 5
            roleApplicationRecordPage
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickNewRecordButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderStaffRoleName2)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid2.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName2)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TargetTeam2.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateTargetTeamName(TargetTeamName2);


            #endregion

            #region Step 7
            roleApplicationRecordPage
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickNewRecordButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderStaffRoleName2)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid2.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TargetTeam.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateTargetTeamName(TargetTeamName);
            #endregion

            #region Step 8
            roleApplicationRecordPage
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickNewRecordButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderStaffRoleName)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName2)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TargetTeam2.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateTargetTeamName(TargetTeamName2);

            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.IsTrue(roleApplications.Count >= 5);

            #endregion

            #region Step 9

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Role Applications")
                .SelectFilter("1", "Applicant")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(applicantLastName).TapSearchButton().SelectResultElement(_applicantId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();
            System.Threading.Thread.Sleep(3000);

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(roleApplications[4].ToString())
                .ClickNewRecordButton_ResultsPage();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ClickApplicantLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(applicantLastName)
                .TapSearchButton()
                .SelectResultElement(_applicantId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderStaffRoleName2)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid2.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName2)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TargetTeam2.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is not possible to have duplicate applications. " + _applicantFullName + " has an active application for " + careProviderStaffRoleName2 + " and " + TargetTeamName2 + ".")
                .TapCloseButton();
            #endregion

            #region Step 10
            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("CareProviders")
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateResponsibleTeamName("CareProviders")
                .ValidateTargetTeamName("CareProviders");


            #endregion
        }

        #endregion

    }
}
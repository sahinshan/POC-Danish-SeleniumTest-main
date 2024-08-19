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
    public class RoleApplication_UITestCases : FunctionalTest
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
        private Guid _applicantId2;
        private Guid promotedSystemUserId;
        private string _applicantFullName;
        private string _applicant2FullName;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _careProviderStaffRoleTypeid2;
        private string _careProviderStaffRoleName;
        private Guid _employmentContractTypeid;
        private Guid _rejectedReasonId;
        private string _rejectedReasonName;
        private Guid _rejectedReason2Id;
        private string _rejectedReason2Name;
        public Guid environmentid;
        private Guid _roleApplicationID;
        private string _loginUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _targetTeam_TeamId;
        private string _targetTeam_TeamName;
        private Guid _careProviderStaffRoleType2id;
        private string _careProviderStaffRoleName2;
        private Guid _careProviders_ResponsibleTeam;
        private Guid _careProviders_TargetTeam;
        private Guid _applicationSourceId;
        private Guid _roleApplicant;
        private Guid _roleApplicant2;
        private string _staffRecruitmentItemName;
        private string _staffRecruitmentItemName2;
        private string _staffRecruitmentItemName3;
        private string _staffRecruitmentItemName4;
        private string _staffRecruitmentItemName5;
        private Guid _staffRecruitmentItemId;
        private Guid _staffRecruitmentItemId2;
        private Guid _staffRecruitmentItemId3;
        private Guid _staffRecruitmentItemId4;
        private Guid _staffRecruitmentItemId5;
        private string _staffTrainingItemName;
        private Guid _staffTrainingItemId;

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

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion Authentication

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language      

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _careProvider_TeamName = "CareProviders";
                _careProviders_TeamId = commonMethodsDB.CreateTeam(_careProvider_TeamName, null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Security Profiles

                staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();

                #endregion

                #region Create default system user

                _loginUsername = "Login_User" + currentTimeString;
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "Login_", "Automation_User_" + currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);


                #endregion

                #region Care provider staff role type

                var _defaultCareProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Default CPSRT", null, null, new DateTime(2021, 1, 1), "Default CPSRT ...");

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-20509

        [TestProperty("JiraIssueID", "ACC-3506")]
        [Description("Login CD as a Care Provider.  As a  Recruiter, Go to Workplace - Recruitment - Role Applications. Check that list of existing applicants is getting displayed if any.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        public void RoleApplication_UITestCases001()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Carer Associate";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Applicant_Role");

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 20509 " + currentTimeString, "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Rejected Reason

            _rejectedReasonName = "CDV6_20509_" + currentTimeString;
            _rejectedReasonId = commonMethodsDB.CreateRejectedReason(_rejectedReasonName, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            #region Rejected Reason 2 

            _rejectedReason2Name = "RejectionReason_" + currentTimeString;
            _rejectedReason2Id = commonMethodsDB.CreateRejectedReason(_rejectedReason2Name, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            var applicantFirstName = "Testing_CDV6_20560";
            var applicantLastName = "User_" + currentTimeString;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName);

            applicantPage
                .WaitForApplicantsPageToLoad()
                .ValidateApplicantRecordIsPresent(_applicantId.ToString());

        }

        [TestProperty("JiraIssueID", "ACC-3507")]
        [Description("Login CD as a Care Provider.  As a  Recruiter, Go to Workplace - Recruitment - Role Applications. Open a existing Applicant. " +
            "Click on add button and Create a new application for Applicant one, Role one, Target Team one - Status - pending and hit save button." +
            "A new Role Application record should get created. And navigate to previous page where the newly created record should get listed in the grid.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Role Applications")]
        public void RoleApplication_UITestCases002()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Carer Associate";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Applicant_Role");

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 20509 " + currentTimeString, "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Rejected Reason

            _rejectedReasonName = "CDV6_20509_" + currentTimeString;
            _rejectedReasonId = commonMethodsDB.CreateRejectedReason(_rejectedReasonName, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            #region Rejected Reason 2 

            _rejectedReason2Name = "RejectionReason_" + currentTimeString;
            _rejectedReason2Id = commonMethodsDB.CreateRejectedReason(_rejectedReason2Name, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            var applicantFirstName = "Testing_CDV6_20573";
            var applicantLastName = "User_" + currentTimeString;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName)
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
                .TypeSearchQuery(_careProvider_TeamName)
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

            System.Threading.Thread.Sleep(1000);

            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(1, roleApplications.Count);

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(roleApplications[0].ToString());
        }

        [TestProperty("JiraIssueID", "ACC-3508")]
        [Description("Login CD as a Care Provider.  As a  Recruiter, Go to Workplace - Recruitment - Role Applications. Open a existing Applicant. " +
            "Click on add button and Create a new application for Applicant one, Role one, Target Team one - Status - pending and hit save button." +
            "Click on add button and create new application with same details Applicant one, Role one, Target Team one - status - pending and hit save button" +
            "An alert should get displayed 'It is not possible to have duplicate applications. {Applicant} has an active application for {role} and {target team}.'")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Role Applications")]
        public void RoleApplication_UITestCases003()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Carer Associate";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Applicant_Role");

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 20509 " + currentTimeString, "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Rejected Reason

            _rejectedReasonName = "CDV6_20509_" + currentTimeString;
            _rejectedReasonId = commonMethodsDB.CreateRejectedReason(_rejectedReasonName, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            #region Rejected Reason 2 

            _rejectedReason2Name = "RejectionReason_" + currentTimeString;
            _rejectedReason2Id = commonMethodsDB.CreateRejectedReason(_rejectedReason2Name, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            var applicantFirstName = "Testing_CDV6_20574";
            var applicantLastName = "User_" + currentTimeString;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName)
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
                .TypeSearchQuery(_careProvider_TeamName)
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

            System.Threading.Thread.Sleep(1000);

            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(1, roleApplications.Count);

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
                .TypeSearchQuery(_careProvider_TeamName)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is not possible to have duplicate applications. " + _applicantFullName + " has an active application for " + _careProviderStaffRoleName + " and " + _careProvider_TeamName + ".")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(1000);

            roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(1, roleApplications.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3509")]
        [Description("Open existing record, change the status to - Induction and save. Create a new application. Applicant one, Role one, Target Team one - status - pending and hit save button" +
                     "A new Role Application record should get created. And navigate to previous page where the newly created record should get listed in the grid. Alert message should not be rendered")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Role Applications")]
        public void RoleApplication_UITestCases004()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Carer Associate";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Applicant_Role");

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 20509 " + currentTimeString, "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Rejected Reason

            _rejectedReasonName = "CDV6_20509_" + currentTimeString;
            _rejectedReasonId = commonMethodsDB.CreateRejectedReason(_rejectedReasonName, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            #region Rejected Reason 2 

            _rejectedReason2Name = "RejectionReason_" + currentTimeString;
            _rejectedReason2Id = commonMethodsDB.CreateRejectedReason(_rejectedReason2Name, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            string applicantFirstName = "Testing_CDV6_20577";
            string applicantLastName = "User_" + currentTimeString;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName)
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            System.Threading.Thread.Sleep(5000);

            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(1, roleApplications.Count);


            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString())
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            //complete all induction requirements for the applicant record
            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid complianceId in complianceItems)
            {
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            }

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRecruitmentStatusSelectedText("Pending")
                .SelectRecruitmentStatus("Induction")
                .ClickContractTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Employment Contract Type 20509 " + currentTimeString)
                .SelectResultElement(_employmentContractTypeid.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton()
                .ValidateRoleApplicationRecordIsNotPresent(_roleApplicationID.ToString());

            System.Threading.Thread.Sleep(1000);

            roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(0, roleApplications.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            promotedSystemUserId = dbHelper.systemUser.GetSystemUserByUserNameContains(applicantLastName).First();
            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("*" + applicantLastName + "*")
                .ClickSearchButton()
                .ValidateRecordIsPresent(promotedSystemUserId.ToString())
                .OpenRecord(promotedSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ClickRefreshButton()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString())
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRecruitmentStatusSelectedText("Induction");
        }

        [TestProperty("JiraIssueID", "ACC-3510")]
        [Description("Open existing record, change the status to - Fully accepted and save. Create a new application Applicant one, Role one, Target Team one - status - pending" +
                     "A new Role Application record is not created. Alert message is rendered")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Role Applications")]
        public void RoleApplication_UITestCases005()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Carer Associate";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Applicant_Role");

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 20509 " + currentTimeString, "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Rejected Reason

            _rejectedReasonName = "CDV6_20509_" + currentTimeString;
            _rejectedReasonId = commonMethodsDB.CreateRejectedReason(_rejectedReasonName, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            #region Rejected Reason 2 

            _rejectedReason2Name = "RejectionReason_" + currentTimeString;
            _rejectedReason2Id = commonMethodsDB.CreateRejectedReason(_rejectedReason2Name, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            Guid staffTrainingItem = dbHelper.staffTrainingItem.CreateStaffTrainingItem(_careProviders_TeamId, "Item_20607_" + currentTimeString, new DateTime(2022, 1, 1));

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("Training_20607_" + currentTimeString, staffTrainingItem, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            dbHelper.trainingRequirement.CreateTrainingRequirement("Item_20607_" + currentTimeString + " - Internal", _careProviders_TeamId, staffTrainingItem, new DateTime(2022, 1, 1), null, 6, 1);

            #region Staff Recruitment Item
            _staffRecruitmentItemName = "CDV6_23658_1_" + currentTimeString;
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds2 = new List<Guid>() { _careProviderStaffRoleTypeid };
            var recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds2);

            #endregion

            string applicantFirstName = "Testing_CDV6_20607";
            string applicantLastName = "Applicant_" + currentTimeString;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName)
                .ValidateApplicantRecordIsPresent(_applicantId.ToString())
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(1, roleApplications.Count);

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString())
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());

            //complete all induction requirements for the applicant record
            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid complianceId in complianceItems)
            {
                dbHelper.compliance.UpdateCompliance(complianceId, 1593, "Test", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            }

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRecruitmentStatusSelectedText("Pending")
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

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            promotedSystemUserId = dbHelper.systemUser.GetSystemUserByUserNameContains(applicantLastName).First();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("*" + applicantLastName + "*")
                .ClickSearchButton()
                .OpenRecord(promotedSystemUserId.ToString());


            //complete system user training for system user                      
            var systemUserTrainingItems = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(promotedSystemUserId);

            foreach (Guid systemUserTrainingId in systemUserTrainingItems)
            {
                Guid trainingCourse = dbHelper.trainingRequirement.GetTrainingRequirementByTrainingItem(staffTrainingItem).First();

                dbHelper.systemUserTraining.UpdateSystemUserTraining(systemUserTrainingId, trainingCourse, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 1);
            }

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString())
                .OpenRoleApplicationRecord(_roleApplicationID.ToString());


            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRecruitmentStatusSelectedText("Induction")
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateRecruitmentStatusSelectedText("Fully Accepted");

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickBackButton();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ClickRefreshButton()
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ValidateRoleApplicationRecordIsPresent(_roleApplicationID.ToString());

            System.Threading.Thread.Sleep(1000);

            roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(0, roleApplications.Count);


            roleApplicationsPage
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
                .TypeSearchQuery(_careProvider_TeamName)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is not possible to have duplicate applications. " + _applicantFullName + " has an active application for " + _careProviderStaffRoleName + " and " + _careProvider_TeamName + ".");

            roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(promotedSystemUserId);
            Assert.AreEqual(1, roleApplications.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3511")]
        [Description("Open existing record in pending status, change the status to - Rejected and save. Create a new application Applicant one, Role one, Target Team one - status - pending" +
                     "A new Role Application record should get created. And navigate to previous page where the newly created record should get listed in the grid. Alert message should not be rendered." +
                     "Create a new application Applicant one, Role one, BU one - status - pending and hit save Icon" +
                     "An alert should get displayed 'It is not possible to have duplicate applications. {Applicant} has an active application for {role} and {target team}.'" +
                     "When User clicks on the cancel button in the alert dialog and change the BU one to BU two and hit save Icon" +
                     "A new Role Application record should get created. And navigate to previous page where the newly created record should get listed in the grid.Alert message should not be rendered" +
                     "Create a new application Applicant one, Role one, BU one - status - pending and hit save Icon" +
                     "When User clicks on the cancel button in the Alert dialog  and change the Role one to Role two and hit save Icon" +
                     "A new Role Application record should get created. And navigate to previous page where the newly created record should get listed in the grid. Alert message should not be rendered")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Applicants")]
        [TestProperty("Screen2", "Role Applications")]
        public void RoleApplication_UITestCases006()
        {
            #region Care provider staff role type

            _careProviderStaffRoleName = "Carer Associate";
            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleName, null, null, new DateTime(2021, 1, 1), "Applicant_Role");

            #endregion

            #region Employment Contract Type

            _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 20509 " + currentTimeString, "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Rejected Reason

            _rejectedReasonName = "CDV6_20509_" + currentTimeString;
            _rejectedReasonId = commonMethodsDB.CreateRejectedReason(_rejectedReasonName, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            #region Rejected Reason 2 

            _rejectedReason2Name = "RejectionReason_" + currentTimeString;
            _rejectedReason2Id = commonMethodsDB.CreateRejectedReason(_rejectedReason2Name, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            string applicantFirstName = "Testing_CDV6_20653";
            string applicantLastName = "Applicant_" + currentTimeString;

            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, applicantLastName, _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            _targetTeam_TeamId = dbHelper.team.CreateTeam("Team_20653_" + currentTimeString, null, _careProviders_BusinessUnitId, "90909", "team20653@careworkstempmail.com", "Team_20653_" + currentTimeString, "030 123456");
            _targetTeam_TeamName = (string)dbHelper.team.GetTeamByID(_targetTeam_TeamId, "name")["name"];

            dbHelper.teamMember.CreateTeamMember(_targetTeam_TeamId, _defaultLoginUserID, new DateTime(2020, 1, 1), null);
            _careProviderStaffRoleType2id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Applicant_Role_2" + currentTimeString, null, null, new DateTime(2021, 1, 1), "Applicant_Role");
            _careProviderStaffRoleName2 = (string)dbHelper.careProviderStaffRoleType.GetCareProviderStaffRoleTypeByID(_careProviderStaffRoleType2id, "name")["name"];

            _roleApplicationID = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantLastName)
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
                .ValidateApplicantName(_applicantFullName)
                .SelectRecruitmentStatus("Rejected")
                .ClickRejectedReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_rejectedReasonName)
                .TapSearchButton()
                .SelectResultElement(_rejectedReasonId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .InsertRejctionAdditonalComments("Test: " + currentTimeString)
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateRecruitmentStatusSelectedText("Rejected")
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton()
                .ClickNewRecordButton();

            dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName);
            var roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(2, roleApplications.Count);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
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
                .TypeSearchQuery(_careProvider_TeamName)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is not possible to have duplicate applications. " + _applicantFullName + " has an active application for " + _careProviderStaffRoleName + " and " + _careProvider_TeamName + ".")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_targetTeam_TeamName)
                .TapSearchButton()
                .SelectResultElement(_targetTeam_TeamId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRoleName(_careProviderStaffRoleName)
                .ValidateTargetTeamName(_targetTeam_TeamName)
                .ClickBackButton();

            roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(3, roleApplications.Count);

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton()
                .ClickNewRecordButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
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
                .TypeSearchQuery(_careProvider_TeamName)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TeamId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is not possible to have duplicate applications. " + _applicantFullName + " has an active application for " + _careProviderStaffRoleName + " and " + _careProvider_TeamName + ".")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_careProviderStaffRoleName2)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleType2id.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateApplicantName(_applicantFullName)
                .ValidateRoleName(_careProviderStaffRoleName2)
                .ValidateTargetTeamName(_careProvider_TeamName)
                .ClickBackButton();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ClickRefreshButton();

            roleApplications = dbHelper.recruitmentRoleApplicant.GetByApplicantId(_applicantId);
            Assert.AreEqual(4, roleApplications.Count);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-21094

        [TestProperty("JiraIssueID", "ACC-3512")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> create new Role Application" +
                     "Click on All Lookup button and Validate Lookup field Value & Fill all details and Save the Record." +
                     "Open Record and Change the Recruitment Status to Rejected and check that the fields Rejected Reason and Additional comments are getting displayed." +
                     "Validate Rejected Reason loads the data from the Reference data Fill the Rejected Reason and Addition Commnent and Save Record and Validate Saved Record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]
        public void RoleApplication_UITestCases007()
        {
            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_21186";
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant("Testing_CDV6_21186", "Applicant_01", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_21186_Role" + DateTime.Now.ToString("ddMMyyyyHHmmss");

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

            string responsibleTeamName = "CDV6_21186_Responsible_Team";
            var teamsExist = dbHelper.team.GetTeamIdByName(responsibleTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(responsibleTeamName, null, _careProviders_BusinessUnitId, "90400", responsibleTeamName + "@careworkstempmail.com", responsibleTeamName, "020 123456");
            _careProviders_ResponsibleTeam = dbHelper.team.GetTeamIdByName(responsibleTeamName)[0];

            #endregion

            #region Create Target Team

            string TargetTeamName = "CDV6_21186_Target_Team";
            teamsExist = dbHelper.team.GetTeamIdByName(TargetTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "90400", TargetTeamName + "@careworkstempmail.com", TargetTeamName, "020 123456");
            _careProviders_TargetTeam = dbHelper.team.GetTeamIdByName(TargetTeamName)[0];

            #endregion

            #region Create Application Source

            string ApplicationSourceName = "CDV6_21186_Application_Source";
            var ApplicationSourceNameExist = dbHelper.applicationSource.GetApplicationSourceIdByName(ApplicationSourceName).Any();
            if (!ApplicationSourceNameExist)
                dbHelper.applicationSource.CreateApplicationSource(ApplicationSourceName, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);
            _applicationSourceId = dbHelper.applicationSource.GetApplicationSourceIdByName(ApplicationSourceName)[0];

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
                .ValidateMandatoryFields()
                .ValidateBlankFields()
                .ValidateRecruitmentStatus_Field_Disabled(true)
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ValidateApplicantName(_applicantFullName)
                .ValidateResponsibleTeamName(_careProvider_TeamName)
                .ValidateResponsibleRecruiterName("Login_ Automation_User_" + currentTimeString)
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
                .InsertApplicationDateValue(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton(); System.Threading.Thread.Sleep(3000);

            _roleApplicant = dbHelper.recruitmentRoleApplicant.GetByName(careProviderRoleName).FirstOrDefault();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateRecruitmentStatusCellText(_roleApplicant.ToString(), "Pending")
                .ValidateRoleCellText(_roleApplicant.ToString(), careProviderRoleName)
                .ValidateTargetTeamCellText(_roleApplicant.ToString(), TargetTeamName)
                .OpenRoleApplicationRecord(_roleApplicant.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateTargetTeamName(TargetTeamName)
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

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .ValidateRecruitmentStatusCellText(_roleApplicant.ToString(), "Rejected");


        }

        [TestProperty("JiraIssueID", "ACC-3513")]
        [Description("Login CD as a Care Provider  -> Work Place  -> My Work -> Applicants -> Role Application -> Create new role application" +
                     "Validate Created recods fields through Advanced Search of Role Application")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]

        public void RoleApplication_UITestCases008()
        {
            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_21187" + currentTimeString;
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_21187_Role" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            string careProviderRoleName2 = "CDV6_21187_Role_2" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);
            _careProviderStaffRoleTypeid2 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName2, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create Target Team

            string TargetTeamName = "CDV6_21187_Target_Team";
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

            #region Create Role Applicant

            _roleApplicant = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TargetTeam, 1, "applicant", _applicantFullName);

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
                .ValidateSearchResultRecordCellContent(_roleApplicant.ToString(), 7, "Login_ Automation_User_" + currentTimeString);

            advanceSearchPage
                .ClickNewRecordButton_ResultsPage();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ClickApplicantLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(applicantFirstName)
                .TapSearchButton()
                .SelectResultElement(_applicantId.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ValidateApplicantName(_applicantFullName)
                .ClickRoleLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderRoleName2)
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid2.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ValidateRoleName(careProviderRoleName2)
                .ClickTargetTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(TargetTeamName)
                .TapSearchButton()
                .SelectResultElement(_careProviders_TargetTeam.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoadFromAdvancedSearch()
                .ValidateTargetTeamName(TargetTeamName)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            _roleApplicant2 = dbHelper.recruitmentRoleApplicant.GetByName(careProviderRoleName2).FirstOrDefault();

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
                .ValidateRoleCellText(_roleApplicant2.ToString(), careProviderRoleName2)
                .ValidateTargetTeamCellText(_roleApplicant2.ToString(), TargetTeamName)
                .ValidateRecruitmentStatusCellText(_roleApplicant2.ToString(), "Pending");
        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23658

        [TestProperty("JiraIssueID", "ACC-3514")]
        [Description("Step 1 to 6 from original test - This test case is to check whether the user is allowed to change the status with proper permissions, " +
            "and if the conditions are not met, then valid alert message is getting displayed or not.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]
        public void RoleApplication_UITestCases009()
        {
            #region Employment Contract Type

            _employmentContractTypeid = dbHelper.employmentContractType.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 23568 " + currentTimeString, null, null, new DateTime(2020, 1, 1));

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_23658_Role" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, "2", null, new DateTime(2020, 1, 1), null);

            string careProviderRoleName2 = "CDV6_23658_Role_2" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _careProviderStaffRoleTypeid2 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName2, "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "CDV6_23658_1_" + currentTimeString;
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            _staffTrainingItemName = "CDV6_23658_1_" + currentTimeString;
            _staffTrainingItemId = dbHelper.staffTrainingItem.CreateStaffTrainingItem(_careProviders_TeamId, _staffTrainingItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            var recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Requirement Setup

            foreach (var trId in dbHelper.trainingRequirementSetup.GetByAllRoles(true))
                dbHelper.trainingRequirementSetup.UpdateAllRoles(trId, false, careProviderStaffRoleTypeIds);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_23568" + currentTimeString;
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            _applicantId2 = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_02", _careProviders_TeamId);
            _applicant2FullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId2, "fullname")["fullname"];

            #endregion            

            #region Create Target Team

            string TargetTeamName = "CDV6_23658_Target_Team";
            var teamsExist = dbHelper.team.GetTeamIdByName(TargetTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "90459", TargetTeamName + "@careworkstempmail.com", TargetTeamName, "020 123456");
            _careProviders_TargetTeam = dbHelper.team.GetTeamIdByName(TargetTeamName)[0];

            #endregion

            #region Create Role Applicant

            _roleApplicant = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            _roleApplicant2 = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId2, _careProviderStaffRoleTypeid2, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicant2FullName, _employmentContractTypeid);

            #endregion

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

            var NonAdminLoginUser = "CDV6_23658_User1_" + currentTimeString;
            var NonAdminUserId = commonMethodsDB.CreateSystemUserRecord(NonAdminLoginUser, "CDV6-23658-User1", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid, securityProfilesList, 2);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true);

            #endregion

            #region Step 2

            mainMenu
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicant.ToString());

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid complianceId in complianceItems)
            {
                dbHelper.compliance.UpdateCompliance(complianceId, 1999, "Test", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), NonAdminUserId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), NonAdminUserId);
            }

            #endregion

            #region Step 3

            System.Threading.Thread.Sleep(2000);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("All recruitment requirements have been met for the status Fully Accepted. However, you do not have the feature privilege 'Fully Accept Recruitment Application' to set the Applicant’s recruitment status to Fully Accepted.")
                .TapCloseButton();

            #endregion

            #region Step 4

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton()
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusFieldAlertMessage("All recruitment requirements have been met for the status Fully Accepted. You do not have permission to set this status to the Applicant.");

            #endregion

            #region Step 5

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(NonAdminUserId, securityProfileId17);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .ClickSignOutButton();

            loginPage
              .GoToLoginPage()
              .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToSystemUserSection();

            promotedSystemUserId = dbHelper.systemUser.GetSystemUserByUserNameContains(applicantFirstName).First();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertName("*" + applicantFirstName + "*")
                .ClickSearchButton()
                .OpenRecord(promotedSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .NavigateToRollApplicationsSubPage();

            roleApplicationsPage
                .WaitForSystemUserRoleApplicationsPageToLoad()
                .ClickRefreshButton()
                .OpenRoleApplicationRecord(_roleApplicant.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateApplicantName(_applicantFullName)
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton()
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Fully Accepted")
                .ValidateApplicantName(_applicantFullName);

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId2.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicant2.ToString());

            System.Threading.Thread.Sleep(2000);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton()
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Fully Accepted")
                .ValidateProgressTowardsInductionStatusFieldValue("100")
                .ValidateProgressTowardsFullyAcceptedFieldValue("100")
                .ValidateApplicantName(_applicant2FullName);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3515")]
        [Description("Step 7 to 9 from original test - This test case is to check whether the user is allowed to change the status with proper permissions, " +
            "and if the conditions are not met, then valid alert message is getting displayed or not.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]
        public void RoleApplication_UITestCases010()
        {
            #region Employment Contract Type

            _employmentContractTypeid = dbHelper.employmentContractType.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 23568 " + currentTimeString, "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_23658_Role" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItemName = "CDV6_23658_1_" + currentTimeString;
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Staff Training Item

            _staffTrainingItemName = "CDV6_23658_1_" + currentTimeString;
            _staffTrainingItemId = dbHelper.staffTrainingItem.CreateStaffTrainingItem(_careProviders_TeamId, _staffTrainingItemName, new DateTime(2020, 1, 1));

            #endregion

            #region Recruitment Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            var recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);

            #endregion

            #region Training Requirement Setup
            var trainingRequirementId = dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup(_staffTrainingItemName, _staffTrainingItemId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "Item_23658_" + currentTimeString + " - Internal";
            var StaffTrainingItemId = dbHelper.trainingRequirement.CreateTrainingRequirement(trainingTitle, _careProviders_TeamId, _staffTrainingItemId, new DateTime(2022, 1, 1), null, 3, 1);

            #endregion

            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_23568" + currentTimeString;
            _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];

            #endregion            

            #region Create Target Team

            string TargetTeamName = "CDV6_23658_Target_Team";
            var teamsExist = dbHelper.team.GetTeamIdByName(TargetTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "90459", TargetTeamName + "@careworkstempmail.com", TargetTeamName, "020 123456");
            _careProviders_TargetTeam = dbHelper.team.GetTeamIdByName(TargetTeamName)[0];

            #endregion

            #region Rejected Reason

            _rejectedReason2Name = "RejectionReason_" + currentTimeString;
            _rejectedReason2Id = commonMethodsDB.CreateRejectedReason(_rejectedReason2Name, _careProviders_TeamId, _careProviders_BusinessUnitId, DateTime.Now.Date);

            #endregion

            #region Create Role Applicant

            _roleApplicant = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);

            #endregion

            #region Non-admin user
            var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First();
            var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First();
            var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First();
            var securityProfileId4 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First();
            var securityProfileId5 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)").First();
            var securityProfileId6 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Recruitment (Edit)").First();
            //var securityProfileId7 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Scheduling (Edit)").First();
            var securityProfileId9 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Training (Edit)").First();
            var securityProfileId10 = dbHelper.securityProfile.GetSecurityProfileByName("Training Setup (Edit)").First();
            var securityProfileId11 = dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)").First();
            var securityProfileId12 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First();
            var securityProfileId13 = dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access").First();
            var securityProfileId14 = dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First();
            var securityProfileId15 = dbHelper.securityProfile.GetSecurityProfileByName("Team Membership (Edit)").First();
            var securityProfileId16 = dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)").First();
            var securityProfileId17 = dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel").First();
            var securityProfileId18 = dbHelper.securityProfile.GetSecurityProfileByName("Fully Accept Recruitment Application").First();

            var securityProfilesList = new List<Guid> {securityProfileId1, securityProfileId2, securityProfileId3, securityProfileId4, securityProfileId5,
                securityProfileId6, securityProfileId9, securityProfileId10,
                securityProfileId11, securityProfileId12, securityProfileId13, securityProfileId14, securityProfileId15, securityProfileId16,
                securityProfileId17 };

            var NonAdminLoginUser = "CDV6_23658_User1_" + currentTimeString;
            var NonAdminUserId = commonMethodsDB.CreateSystemUserRecord(NonAdminLoginUser, "CDV6-23658-User1", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid, securityProfilesList, 2);

            #endregion

            #region Step 7

            loginPage
              .GoToLoginPage()
              .Login(NonAdminLoginUser, "Passw0rd_!", EnvironmentName);

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

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicant.ToString());

            System.Threading.Thread.Sleep(2000);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad();
            dynamicDialogPopup.ValidateMessageContainsText("Induction requirements are unmet. Missing:\r\n");
            dynamicDialogPopup.ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete");
            dynamicDialogPopup.TapCloseButton();

            #endregion

            #region Step 8

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad();
            dynamicDialogPopup.ValidateMessageContainsText("Induction requirements are unmet. Missing:\r\n");
            dynamicDialogPopup.ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete");
            dynamicDialogPopup.ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:\r\n");
            dynamicDialogPopup.ValidateMessageContainsText(_staffTrainingItemName + " - 1 item is not Current");
            dynamicDialogPopup.TapCloseButton();

            #endregion

            #region Step 9

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Rejected")
                .ClickRejectedReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_rejectedReason2Name)
                .TapSearchButton()
                .SelectResultElement(_rejectedReason2Id.ToString());

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .InsertRejctionAdditonalComments("CDV6-23658")
                .ClickSaveButton()
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Rejected")
                .ValidateRejectedReasonName(_rejectedReason2Name)
                .ValidateAdditonalComments_FieldValue("CDV6-23658");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3516")]
        [Description("Step 10 to 12 from original test - This test case is to check whether the user is allowed to change the status with proper permissions, " +
            "and if the conditions are not met, then valid alert message is getting displayed or not.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Role Applications")]
        public void RoleApplication_UITestCases011()
        {
            #region Employment Contract Type
            string employmentContractTypeName = "Employment Contract Type 23568";
            if (!dbHelper.employmentContractType.GetByName(employmentContractTypeName).Any())
            {
                dbHelper.employmentContractType.CreateEmploymentContractType(_careProviders_TeamId, employmentContractTypeName, "2", null, new DateTime(2020, 1, 1));
            }
            _employmentContractTypeid = dbHelper.employmentContractType.GetByName(employmentContractTypeName).First();

            #endregion

            #region Care provider staff role type

            string careProviderRoleName = "CDV6_23658_Role011";
            if (!dbHelper.careProviderStaffRoleType.GetByName(careProviderRoleName).Any())
            {
                dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, careProviderRoleName, null, null, new DateTime(2020, 1, 1), null);
            }
            _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.GetByName(careProviderRoleName).First();

            #endregion

            #region Staff Recruitment Item
            _staffRecruitmentItemName = "CDV6_23658_A";
            var staffRecruitmentItem = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName).Any();
            if (!staffRecruitmentItem)
            {
                dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName, new DateTime(2020, 1, 1));
            }
            _staffRecruitmentItemId = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName).First();

            #endregion

            #region Recruitment Requirement Setup
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleTypeid };
            var recruitmentRequirementSetup = dbHelper.recruitmentRequirement.GetByName(_staffRecruitmentItemName).Any();
            if (!recruitmentRequirementSetup)
            {
                dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careProviders_TeamId, _staffRecruitmentItemName, _staffRecruitmentItemId, "staffrecruitmentitem", _staffRecruitmentItemName, false, 1, 3, 1, 3, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, careProviderStaffRoleTypeIds);
            }

            var recruitmentRequirementId = dbHelper.recruitmentRequirement.GetByName(_staffRecruitmentItemName).First();

            #endregion



            #region Create Applicant 

            var applicantFirstName = "Testing_CDV6_23568" + currentTimeString;
            var newApplicant = dbHelper.applicant.GetByFirstName(applicantFirstName).Any();
            if (!newApplicant)
                _applicantId = dbHelper.applicant.CreateApplicant(applicantFirstName, "Applicant_01", _careProviders_TeamId);
            if (_applicantId == Guid.Empty)
                _applicantId = dbHelper.applicant.GetByFirstName(applicantFirstName)[0];

            _applicantFullName = (string)dbHelper.applicant.GetApplicantByID(_applicantId, "fullname")["fullname"];
            #endregion            

            #region Create Target Team

            string TargetTeamName = "CDV6_23658_Target_Team";
            var teamsExist = dbHelper.team.GetTeamIdByName(TargetTeamName).Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam(TargetTeamName, null, _careProviders_BusinessUnitId, "90459", TargetTeamName + "@careworkstempmail.com", TargetTeamName, "020 123456");
            _careProviders_TargetTeam = dbHelper.team.GetTeamIdByName(TargetTeamName)[0];

            #endregion

            #region Create Role Applicant

            _roleApplicant = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _defaultLoginUserID, _careProviders_TeamId, DateTime.Now, _careProviders_TeamId, 1, "applicant", _applicantFullName, _employmentContractTypeid);


            #endregion            

            #region Step 10

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            #region New Recruitment Requirement Setup            

            _staffRecruitmentItemName2 = "CDV6_23658_B";
            staffRecruitmentItem = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName2).Any();
            if (!staffRecruitmentItem)
            {
                dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName2, new DateTime(2020, 1, 1));
            }
            _staffRecruitmentItemId2 = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName2).First();

            _staffRecruitmentItemName3 = "CDV6_23658_C";
            staffRecruitmentItem = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName3).Any();
            if (!staffRecruitmentItem)
            {
                dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName3, new DateTime(2020, 1, 1));
            }
            _staffRecruitmentItemId3 = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName3).First();

            _staffRecruitmentItemName4 = "CDV6_23658_D";
            staffRecruitmentItem = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName4).Any();
            if (!staffRecruitmentItem)
            {
                dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName4, new DateTime(2020, 1, 1));
            }
            _staffRecruitmentItemId4 = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName4).First();

            _staffRecruitmentItemName5 = "CDV6_23658_E";
            staffRecruitmentItem = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName5).Any();
            if (!staffRecruitmentItem)
            {
                dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItemName5, new DateTime(2020, 1, 1));
            }
            _staffRecruitmentItemId5 = dbHelper.staffRecruitmentItem.GetByName(_staffRecruitmentItemName5).First();


            var recruitmentRequirementId2 = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RequirementA", _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, true, 1, 3, 0, null, new DateTime(2023, 1, 1), null, null);
            var recruitmentRequirementId3 = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RequirementB", _staffRecruitmentItemId3, "staffrecruitmentitem", _staffRecruitmentItemName3, true, 1, 2, 0, null, new DateTime(2023, 1, 1), null, null);
            var recruitmentRequirementId4 = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RequirementC", _staffRecruitmentItemId4, "staffrecruitmentitem", _staffRecruitmentItemName4, true, 0, null, 1, 3, new DateTime(2023, 1, 1), null, null);
            var recruitmentRequirementId5 = commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RequirementD", _staffRecruitmentItemId5, "staffrecruitmentitem", _staffRecruitmentItemName5, true, 0, null, 1, 2, new DateTime(2023, 1, 1), null, null);


            if (!dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName2).Any())
            {
                dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId2, "StaffRecruitmentItem", _staffRecruitmentItemName2, 1);
            }
            var complianceItemA = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName2).First();

            if (!dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName3).Any())
            {
                dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId3, "StaffRecruitmentItem", _staffRecruitmentItemName3, 1);
            }
            var complianceItemB = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName3).First();

            if (!dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName4).Any())
            {
                dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId4, "StaffRecruitmentItem", _staffRecruitmentItemName4, 1);
            }
            var complianceItemC = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName4).First();

            if (!dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName5).Any())
            {
                dbHelper.compliance.CreateCompliance(_careProviders_TeamId, _applicantId, "applicant", _applicantFullName, _staffRecruitmentItemId5, "StaffRecruitmentItem", _staffRecruitmentItemName5, 1);
            }
            var complianceItemD = dbHelper.compliance.GetComplianceByApplicantIdAndComplianceName(_applicantId, _staffRecruitmentItemName5).First();

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToApplicantsPage();

            applicantPage
                .WaitForApplicantsPageToLoad()
                .SearchApplicantRecord(applicantFirstName)
                .OpenApplicantRecord(_applicantId.ToString());

            applicantRecordPage
                .WaitForApplicantRecordPagePageToLoad()
                .NavigateToRoleApplicationsPage();

            roleApplicationsPage
                .WaitForRoleApplicationsPageToLoad()
                .OpenRoleApplicationRecord(_roleApplicant.ToString());


            System.Threading.Thread.Sleep(2000);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Induction requirements are unmet. Missing:")
                .ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete")
                .ValidateMessageContainsText(_staffRecruitmentItemName2 + " - 1 item is not Complete")
                .ValidateMessageContainsText(_staffRecruitmentItemName3 + " - 1 item is not Requested")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Induction requirements are unmet. Missing:")
                .ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete")
                .ValidateMessageContainsText(_staffRecruitmentItemName2 + " - 1 item is not Complete")
                .ValidateMessageContainsText(_staffRecruitmentItemName3 + " - 1 item is not Requested")
                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:")
                .ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete")
                .ValidateMessageContainsText(_staffRecruitmentItemName5 + " - 1 item is not Requested")
                .ValidateMessageContainsText(_staffRecruitmentItemName4 + " - 1 item is not Complete")
                .TapCloseButton();

            #endregion

            #region Step 11

            dbHelper.recruitmentRequirement.DeleteRecruitmentRequirement(recruitmentRequirementId2);
            dbHelper.recruitmentRequirement.DeleteRecruitmentRequirement(recruitmentRequirementId3);
            dbHelper.recruitmentRequirement.DeleteRecruitmentRequirement(recruitmentRequirementId5);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Induction")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Induction requirements are unmet. Missing:\r\n" + _staffRecruitmentItemName + " - 1 item is not Complete")
                .ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete")
                .TapCloseButton();

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Induction requirements are unmet. Missing:")
                .ValidateMessageContainsText(_staffRecruitmentItemName + " - 1 item is not Complete")
                .ValidateMessageContainsText("Fully Accepted requirements are unmet. Missing:")
                .ValidateMessageContainsText(_staffRecruitmentItemName4 + " - 1 item is not Complete")
                .TapCloseButton();

            #endregion

            #region Step 12

            var complianceItems = dbHelper.compliance.GetComplianceByRegardingId(_applicantId);

            foreach (Guid complianceId in complianceItems)
            {
                dbHelper.compliance.UpdateCompliance(complianceId, 2999, "Test", new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultLoginUserID, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), _defaultLoginUserID);
            }

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton()
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Fully Accepted");

            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RequirementA", _staffRecruitmentItemId2, "staffrecruitmentitem", _staffRecruitmentItemName2, true, 1, 3, 0, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);
            commonMethodsDB.CreateRecruitmentRequirement(_careProviders_TeamId, "RequirementD", _staffRecruitmentItemId5, "staffrecruitmentitem", _staffRecruitmentItemName5, true, 0, null, 1, 2, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Pending")
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Moving an Application to Pending can cause new Recruitment Documents creation according to the current configuration. Do you confirm?")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            roleApplicationRecordPage
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Pending")
                .ValidateRecruitmentStatusFieldAlertMessage("All recruitment requirements have been met for the status Fully Accepted.")
                .WaitForRoleApplicationRecordPageToLoad()
                .SelectRecruitmentStatus("Fully Accepted")
                .ClickSaveButton()
                .WaitForRoleApplicationRecordPageToLoad()
                .ValidateRecruitmentStatusSelectedText("Fully Accepted");

            #endregion

        }


        #endregion


    }
}

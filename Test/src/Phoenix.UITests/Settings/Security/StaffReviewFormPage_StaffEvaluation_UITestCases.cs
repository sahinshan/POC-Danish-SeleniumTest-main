using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    [DeploymentItem("Files\\StaffEvaluationForm.zip"), DeploymentItem("chromedriver.exe")]
    public class StaffReviewFormPage_StaffEvaluation_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _defaultLoginUserID;
        private string _defaultLoginUserName;
        private Guid _systemUserId;
        private Guid _authenticationproviderid;
        private Guid _maritalStatusId;
        private Guid _ethnicityId;
        private Guid _transportTypeId;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid;
        private Guid _roleid;
        private Guid _documentId;
        private Guid _staffReviewSetupid;
        private Guid _reviewedBySystemUserId;
        public Guid _userWorkSchedule;
        public Guid _userWorkSchedule1;
        private string EnvironmentName;
        private string _systemUserFirstName;
        private string _systemUserLastName;
        private string _systemUserName;

        #endregion

        [TestInitialize()]
        public void SystemUserSatffReview_Setup()
        {
            try
            {

                #region Connection to database

                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Environment Name
                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];


                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careDirectorQA_BusinessUnitId, null, "CareDirectorQA@careworkstempmail.com", "Default team for business unit", null);
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Marital Status

                var maritalStatusExist = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").Any();
                if (!maritalStatusExist)
                {
                    _maritalStatusId = dbHelper.maritalStatus.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);
                }
                if (_maritalStatusId == Guid.Empty)
                {
                    _maritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Civil Partner").FirstOrDefault();
                }
                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                {
                    _languageId = dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                }
                if (_languageId == Guid.Empty)
                {
                    _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").FirstOrDefault();
                }
                #endregion Lanuage

                #region Ethnicity

                var ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("Asian or Asian British - Indian").Any();
                if (!ethnicity)
                {
                    _ethnicityId = dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Asian or Asian British - Indian", DateTime.Now);
                }
                if (_ethnicityId == Guid.Empty)
                {
                    _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Asian or Asian British - Indian").FirstOrDefault();
                }

                #endregion Ethnicity

                #region TransportType

                var transportType = dbHelper.transportType.GetTransportTypeByName("TransportTest").Any();
                if (!transportType)
                {
                    _transportTypeId = dbHelper.transportType.CreateTransportType(_careDirectorQA_TeamId, "TransportTest", DateTime.Now, 1, "50", 5);
                }
                if (_transportTypeId == Guid.Empty)
                {
                    _transportTypeId = dbHelper.transportType.GetTransportTypeByName("TransportTest").FirstOrDefault();
                }
                #endregion TransportType

                #region Create default system user

                _defaultLoginUserName = "CW_Admin_Test_User_2";
                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName(_defaultLoginUserName).Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(_defaultLoginUserName, "CW", "Admin_Test_User_2", "CW Admin Test User 2", "Passw0rd_!", _defaultLoginUserName + "@somemail.com", _defaultLoginUserName + "@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                    var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Org Edit)").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsMyRecordsSecurityProfileId);

                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName(_defaultLoginUserName).FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion  Create default system user

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careDirectorQA_TeamId, _defaultLoginUserID);

                #endregion

                #region Create SystemUser Record

                _systemUserFirstName = "Test_User_CDV6_13573_";
                _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserName = "Test_User_CDV6_13573_" + _systemUserLastName;
                _systemUserId = dbHelper.systemUser.CreateSystemUser(_systemUserName, _systemUserFirstName, _systemUserLastName, _systemUserFirstName + _systemUserLastName, "Passw0rd_!", _systemUserName + "@somemail.com", _systemUserName + "@securemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                if (_systemUserId == Guid.Empty)
                {
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName(_systemUserName).FirstOrDefault();
                }

                #endregion  Create SystemUser Record

                #region Staff Reviewed by 

                var reviewedByExists = dbHelper.systemUser.GetSystemUserByUserName("StaffEvaluationForm_CDV6-13573_User").Any();
                if (!reviewedByExists)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.CreateSystemUser("StaffEvaluationForm_CDV6-13573_User", "AAAA", "CDV6-13573_User", "AAAA CDV6-13573_User", "Passw0rd_!", "StaffEvaluationForm_CDV6-13573_User@somemail.com", "StaffEvaluationForm_CDV6-13573_User@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }

                if (_reviewedBySystemUserId == Guid.Empty)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.GetSystemUserByUserName("StaffEvaluationForm_CDV6-13573_User").FirstOrDefault();
                }
                #endregion

                #region Care provider staff role type

                var careProviderStaffRoleTypeExists = dbHelper.careProviderStaffRoleType.GetByName("Helper").Any();
                if (!careProviderStaffRoleTypeExists)
                {
                    _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
                }
                if (_careProviderStaffRoleTypeid == Guid.Empty)
                {
                    _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.GetByName("Helper").FirstOrDefault();
                }

                #endregion

                #region Employment Contract Type

                var employmentContractTypeExists = dbHelper.employmentContractType.GetByName("Full Time Employee Contract").Any();
                if (!employmentContractTypeExists)
                {
                    _employmentContractTypeid = dbHelper.employmentContractType.CreateEmploymentContractType(_careDirectorQA_TeamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));
                }
                if (_employmentContractTypeid == Guid.Empty)
                {
                    _employmentContractTypeid = dbHelper.employmentContractType.GetByName("Full Time Employee Contract").FirstOrDefault();
                }

                #endregion

                #region system User Employment Contract

                var roleid = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId).Any();
                if (!roleid)
                {
                    _roleid = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid);
                }
                if (_roleid == Guid.Empty)
                {
                    _roleid = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId).FirstOrDefault();
                }
                #endregion

                #region Document

                var documentExists = dbHelper.document.GetDocumentByName("Staff Evaluation form").Any();
                if (!documentExists)


                {
                    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\StaffEvaluationForm.Zip");


                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, "StaffEvaluationForm.Zip");

                    _documentId = dbHelper.document.GetDocumentByName("Staff Evaluation form").FirstOrDefault();

                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

                }
                if (_documentId == Guid.Empty)
                {
                    _documentId = dbHelper.document.GetDocumentByName("Staff Evaluation form")[0];

                }

                #endregion

                #region Staff Review Setup Record

                var staffReviewSetupExists = dbHelper.staffReviewSetup.GetByName("Staff Evaluation form").Any();
                if (!staffReviewSetupExists)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup("Staff Evaluation form", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false);
                }
                if (_staffReviewSetupid == Guid.Empty)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.GetByName("Staff Review form").FirstOrDefault();
                }
                #endregion


            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-13573

        [TestProperty("JiraIssueID", "ACC-3437")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Evaluation From record -> Click Edit icon" +
            " Should display list of questions related to Staff Evaluation Form" +
            "Verify the Questions .Should display all the questions as per the User Story CDV6-13573")]
        [DeploymentItem("Files\\StaffEvaluationForm.Zip")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_VerifyFieldsOfStaffEvaluationForm()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffEvaluationForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                 .ClickRefreshButton()
                .OpenRecord(_staffEvaluationForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffEvaluationFormQuestionsRecordPage
              .WaitForEditAssessment_StaffEvaluationFormPageToLoad()
              .Validate_TimeKeeping_FieldOption("Excellent Performance")
              .Validate_TimeKeeping_FieldOption("Good Performance")
              .Validate_TimeKeeping_FieldOption("Meets basic job requirements")
              .Validate_TimeKeeping_FieldOption("Needs improvement")
              .Validate_TimeKeeping_FieldOption("Not acceptable")
              .Validate_SicknessRAbsence_FieldOption("Excellent Performance")
              .Validate_SicknessRAbsence_FieldOption("Good Performance")
              .Validate_SicknessRAbsence_FieldOption("Meets basic job requirements")
              .Validate_SicknessRAbsence_FieldOption("Needs improvement")
              .Validate_SicknessRAbsence_FieldOption("Not acceptable")
              .Validate_AuthorisedAbsence_FieldOption("Excellent Performance")
              .Validate_AuthorisedAbsence_FieldOption("Good Performance")
              .Validate_AuthorisedAbsence_FieldOption("Meets basic job requirements")
              .Validate_AuthorisedAbsence_FieldOption("Needs improvement")
              .Validate_AuthorisedAbsence_FieldOption("Not acceptable")
              .Validate_AppearanceRSmartness_FieldOption("Excellent Performance")
              .Validate_AppearanceRSmartness_FieldOption("Good Performance")
              .Validate_AppearanceRSmartness_FieldOption("Meets basic job requirements")
              .Validate_AppearanceRSmartness_FieldOption("Needs improvement")
              .Validate_AppearanceRSmartness_FieldOption("Not acceptable")
              .Validate_MannerPoliteness_FieldOption("Excellent Performance")
              .Validate_MannerPoliteness_FieldOption("Good Performance")
              .Validate_MannerPoliteness_FieldOption("Meets basic job requirements")
              .Validate_MannerPoliteness_FieldOption("Needs improvement")
              .Validate_MannerPoliteness_FieldOption("Not acceptable")
              .Validate_CompassionAtitude_FieldOption("Excellent Performance")
              .Validate_CompassionAtitude_FieldOption("Good Performance")
              .Validate_CompassionAtitude_FieldOption("Meets basic job requirements")
              .Validate_CompassionAtitude_FieldOption("Needs improvement")
              .Validate_CompassionAtitude_FieldOption("Not acceptable")
              .Validate_HonestyIntegrity_FieldOption("Excellent Performance")
              .Validate_HonestyIntegrity_FieldOption("Good Performance")
              .Validate_HonestyIntegrity_FieldOption("Meets basic job requirements")
              .Validate_HonestyIntegrity_FieldOption("Needs improvement")
              .Validate_HonestyIntegrity_FieldOption("Not acceptable")
              .Validate_TechnicalKnowledge_FieldOption("Excellent Performance")
              .Validate_TechnicalKnowledge_FieldOption("Good Performance")
              .Validate_TechnicalKnowledge_FieldOption("Meets basic job requirements")
              .Validate_TechnicalKnowledge_FieldOption("Needs improvement")
              .Validate_TechnicalKnowledge_FieldOption("Not acceptable")
              .Validate_QualityOfWork_FieldOption("Excellent Performance")
              .Validate_QualityOfWork_FieldOption("Good Performance")
              .Validate_QualityOfWork_FieldOption("Meets basic job requirements")
              .Validate_QualityOfWork_FieldOption("Needs improvement")
              .Validate_QualityOfWork_FieldOption("Not acceptable")
              .Validate_FlexibilityTowardsDuties_FieldOption("Excellent Performance")
              .Validate_FlexibilityTowardsDuties_FieldOption("Good Performance")
              .Validate_FlexibilityTowardsDuties_FieldOption("Meets basic job requirements")
              .Validate_FlexibilityTowardsDuties_FieldOption("Needs improvement")
              .Validate_FlexibilityTowardsDuties_FieldOption("Not acceptable")
              .Validate_AbilityToWorkOnOwnInitiative_FieldOption("Excellent Performance")
              .Validate_AbilityToWorkOnOwnInitiative_FieldOption("Good Performance")
              .Validate_AbilityToWorkOnOwnInitiative_FieldOption("Meets basic job requirements")
              .Validate_AbilityToWorkOnOwnInitiative_FieldOption("Needs improvement")
              .Validate_AbilityToWorkOnOwnInitiative_FieldOption("Not acceptable")
              .Validate_AbilityToWorkInTeams_FieldOption("Excellent Performance")
              .Validate_AbilityToWorkInTeams_FieldOption("Good Performance")
              .Validate_AbilityToWorkInTeams_FieldOption("Meets basic job requirements")
              .Validate_AbilityToWorkInTeams_FieldOption("Needs improvement")
              .Validate_AbilityToWorkInTeams_FieldOption("Not acceptable")
              .Validate_AbilityToDealWithProblems_FieldOption("Excellent Performance")
              .Validate_AbilityToDealWithProblems_FieldOption("Good Performance")
              .Validate_AbilityToDealWithProblems_FieldOption("Meets basic job requirements")
              .Validate_AbilityToDealWithProblems_FieldOption("Needs improvement")
              .Validate_AbilityToDealWithProblems_FieldOption("Not acceptable")
              .Validate_WillingnessToLearn_FieldOption("Excellent Performance")
              .Validate_WillingnessToLearn_FieldOption("Good Performance")
              .Validate_WillingnessToLearn_FieldOption("Meets basic job requirements")
              .Validate_WillingnessToLearn_FieldOption("Needs improvement")
              .Validate_WillingnessToLearn_FieldOption("Not acceptable")
              .Validate_AbilityToFollowInstructions_FieldOption("Excellent Performance")
              .Validate_AbilityToFollowInstructions_FieldOption("Good Performance")
              .Validate_AbilityToFollowInstructions_FieldOption("Meets basic job requirements")
              .Validate_AbilityToFollowInstructions_FieldOption("Needs improvement")
              .Validate_AbilityToFollowInstructions_FieldOption("Not acceptable")
              .Validate_AchievmentsOfTargets_FieldOption("Excellent Performance")
              .Validate_AchievmentsOfTargets_FieldOption("Good Performance")
              .Validate_AchievmentsOfTargets_FieldOption("Meets basic job requirements")
              .Validate_AchievmentsOfTargets_FieldOption("Needs improvement")
              .Validate_AchievmentsOfTargets_FieldOption("Not acceptable")
              .Validate_AcceptanceOfResponsibilities_FieldOption("Excellent Performance")
              .Validate_AcceptanceOfResponsibilities_FieldOption("Good Performance")
              .Validate_AcceptanceOfResponsibilities_FieldOption("Meets basic job requirements")
              .Validate_AcceptanceOfResponsibilities_FieldOption("Needs improvement")
              .Validate_AcceptanceOfResponsibilities_FieldOption("Not acceptable")
              .Validate_AttitudeTowardsStaffNSeniors_FieldOption("Excellent Performance")
              .Validate_AttitudeTowardsStaffNSeniors_FieldOption("Good Performance")
              .Validate_AttitudeTowardsStaffNSeniors_FieldOption("Meets basic job requirements")
              .Validate_AttitudeTowardsStaffNSeniors_FieldOption("Needs improvement")
              .Validate_AttitudeTowardsStaffNSeniors_FieldOption("Not acceptable")
              .Validate_KnowledgeOfJobRelatedPolicies_FieldOption("Excellent Performance")
              .Validate_KnowledgeOfJobRelatedPolicies_FieldOption("Good Performance")
              .Validate_KnowledgeOfJobRelatedPolicies_FieldOption("Meets basic job requirements")
              .Validate_KnowledgeOfJobRelatedPolicies_FieldOption("Needs improvement")
              .Validate_KnowledgeOfJobRelatedPolicies_FieldOption("Not acceptable");
        }

        [TestProperty("JiraIssueID", "ACC-3438")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Evaluation From record -> Click Edit icon" +
           " Should display list of questions related to Staff Review Form" +
           "Leave all the mandatory fields as blank and try to save the form.Should display mandatory field error message against all mandatory fields.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_VerifyMandatoryFieldsOfStaffEvaluationForm()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffEvaluationForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(_staffEvaluationForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffEvaluationFormQuestionsRecordPage
                .WaitForEditAssessment_StaffEvaluationFormPageToLoad()
                .ClickEditAssessmentSaveAndCloseButton()
                .ValidateMandatoryErrorMsgVisible(true);
        }

        [TestProperty("JiraIssueID", "ACC-3439")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Review From record -> Click Edit icon" +
         " Should display list of questions related to Staff Evaluation Form" +
         "Answer all the questions and Save the form.Form should get saved with all the entered / selected answers.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_VerifyEditAndSaveFieldsOfStaffEvaluationForm()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffEvaluationForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(_staffEvaluationForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffEvaluationFormQuestionsRecordPage
                .WaitForEditAssessment_StaffEvaluationFormPageToLoad()
                .Select_TimeKeeping_FieldOption("Excellent Performance")
                .Select_SicknessRAbsence_FieldOption("Good Performance")
                .Select_AuthorisedAbsence_FieldOption("Meets basic job requirements")
                .Select_AppearanceRSmartness_FieldOption("Needs improvement")
                .Select_MannerPoliteness_FieldOption("Not acceptable")
                .Select_CompassionAtitude_FieldOption("Not acceptable")
                .Select_HonestyIntegrity_FieldOption("Not acceptable")
                .Select_TechnicalKnowledge_FieldOption("Excellent Performance")
                .Select_QualityOfWork_FieldOption("Meets basic job requirements")
                .Select_FlexibilityTowardsDuties_FieldOption("Not acceptable")
                .Select_AbilityToWorkOnOwnInitiative_FieldOption("Not acceptable")
                .Select_AbilityToWorkInTeams_FieldOption("Not acceptable")
                .Select_AbilityToDealWithProblems_FieldOption("Excellent Performance")
                .Select_WillingnessToLearn_FieldOption("Good Performance")
                .Select_AbilityToFollowInstructions_FieldOption("Excellent Performance")
                .Select_AchievmentsOfTargets_FieldOption("Excellent Performance")
                .Select_AcceptanceOfResponsibilities_FieldOption("Excellent Performance")
                .Select_AttitudeTowardsStaffNSeniors_FieldOption("Excellent Performance")
                .Select_KnowledgeOfJobRelatedPolicies_FieldOption("Excellent Performance")
                .ClickEditAssessmentSaveAndCloseButton();

            staffReviewFormsRecordPage
               .WaitForStaffReviewFormsRecordPageToLoad();
        }

        [TestProperty("JiraIssueID", "ACC-3440")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> -> Click + Icon and Try to Create New From with document Type Staff Evaluation Form" +
        " Should allow to create multiple Open Forms.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_VerifyMultipleOpenFormsOfStaffEvaluationForm()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);
            var date = DateTime.Now.Date;

            loginPage
                .GoToLoginPage()
                .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
               .ClickCreateRecordButton();

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickDocumentIdLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Staff Evaluation Form")
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            staffReviewFormsRecordPage
               .WaitForStaffReviewFormsRecordPageToLoad()
               .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveAndCloseButton();

            staffReviewFormsPage
               .WaitForStaffReviewFormsPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var staffReviewForms = dbHelper.staffReviewForm.GetByStaffReviewId(_staffReviewId01);
            Assert.AreEqual(2, staffReviewForms.Count);
        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


    }
}

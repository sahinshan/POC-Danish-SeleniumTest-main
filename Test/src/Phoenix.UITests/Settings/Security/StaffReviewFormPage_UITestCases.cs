using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\StaffReviewFormDocument.zip")]
    [DeploymentItem("Files\\StaffReviewFormRules.Zip")]
    public class StaffReviewFormPage_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _defaultLoginUserID;
        private string _defaultLoginUserName;
        private Guid _systemUserId;
        private Guid _authenticationproviderid;
        private Guid _demographicsTitleId;
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

                #region Title

                var demographicsTitle = dbHelper.demographicsTitle.GetByName("Dr.").Any();
                if (!demographicsTitle)
                {
                    _demographicsTitleId = dbHelper.demographicsTitle.CreateDemographicsTitle("Dr", DateTime.Now, _careDirectorQA_TeamId);
                }
                if (_demographicsTitleId == Guid.Empty)
                {
                    _demographicsTitleId = dbHelper.demographicsTitle.GetByName("Dr.").FirstOrDefault();
                }
                #endregion Title

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

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("StaffReviewForm_CDV6_13557").Any();
                if (!newSystemUser)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser("StaffReviewForm_CDV6_13557", "StaffReviewForm", "CDV6_13557", "StaffReviewForm CDV6_13557", "Passw0rd_!", "StaffReviewForm_CDV6_13557@somemail.com", "StaffReviewForm_CDV6_13557@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }
                if (_systemUserId == Guid.Empty)
                {
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("StaffReviewForm_CDV6_13557").FirstOrDefault();
                }

                #endregion  Create SystemUser Record

                #region Staff Reviewed by 

                var reviewedByExists = dbHelper.systemUser.GetSystemUserByUserName("StaffAppraisalForm_CDV6-13557_User").Any();
                if (!reviewedByExists)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.CreateSystemUser("StaffAppraisalForm_CDV6-13557_User", "AAAA", "CDV6-13557_User", "AAAA CDV6-13557_User", "Passw0rd_!", "StaffAppraisalForm_CDV6-13557_User@somemail.com", "StaffAppraisalForm_CDV6-13557_User@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }

                if (_reviewedBySystemUserId == Guid.Empty)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.GetSystemUserByUserName("StaffAppraisalForm_CDV6-13557_User").FirstOrDefault();
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

                var documentExists = dbHelper.document.GetDocumentByName("Staff Review form").Any();
                if (!documentExists)
                {
                    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\StaffReviewFormDocument.Zip");
                    var documentRulesByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\StaffReviewFormRules.zip");

                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, "StaffReviewFormDocument.Zip");
                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentRulesByteArray, "StaffReviewFormRules.zip");

                    _documentId = dbHelper.document.GetDocumentByName("Staff Review form").FirstOrDefault();

                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

                }
                if (_documentId == Guid.Empty)
                {
                    _documentId = dbHelper.document.GetDocumentByName("Staff Review form")[0];

                }

                #endregion

                #region Staff Review Setup Record

                var staffReviewSetupExists = dbHelper.staffReviewSetup.GetByName("Staff Review form").Any();
                if (!staffReviewSetupExists)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup("Staff Review form", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false);
                }
                if (_staffReviewSetupid == Guid.Empty)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.GetByName("Staff Review form").FirstOrDefault();
                }
                #endregion



                #region Staff Review

                // delete all existining records

                foreach (var staffReviewId in dbHelper.staffReview.GetBySystemUserId(_systemUserId))
                {
                    foreach (var staffReviewAttachmentId in dbHelper.staffReviewAttachment.GetByStaffReviewId(staffReviewId))
                        dbHelper.staffReviewAttachment.DeleteStaffReviewAttachment(staffReviewAttachmentId);

                    foreach (var staffReviewForm in dbHelper.staffReviewForm.GetByStaffReviewId(staffReviewId))
                        dbHelper.staffReviewForm.DeleteStaffReviewForm(staffReviewForm);

                    dbHelper.staffReview.DeleteStaffReview(staffReviewId);
                }

                #endregion Staff Review
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }



        #region https://advancedcsg.atlassian.net/browse/CDV6-13589

        [TestProperty("JiraIssueID", "ACC-3441")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Review From record -> Click Edit icon" +
            "Should display list of questions related to Staff Review Form" +
            "Verify the Questions .Should display all the questions as per the User Story CDV6-13557")]
        [DeploymentItem("Files\\StaffReviewFormDocument.Zip"), DeploymentItem("chromedriver.exe")]
        [DeploymentItem("Files\\StaffReviewFormRules.Zip"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_VerifyFieldsOfStaffReviewForm()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_defaultLoginUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_CDV6_13557")
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
                .OpenRecord(_staffReviewForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffReviewFormQuestionsRecordPage
              .WaitForEditAssessment_StaffReviewFormPageToLoad()
              .ValidateJobDescription()
              .ValidatePersonalQualities_TimeKeepigShiftHrs_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_TimeKeepigShiftHrs_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_TimeKeepigShiftHrs_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_TimeKeepigShiftHrs_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_TimeKeepigShiftHrs_RadioButtonVisibility("Unsatisfactory", true)
              .ValidateManagerCommentsTextArea()
              .ValidatePersonalQualities_Punctuality_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_Punctuality_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_Punctuality_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_Punctuality_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_Punctuality_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_Appearance_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_Appearance_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_Appearance_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_Appearance_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_Appearance_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_MannerPoliteness_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_ComparisionTowardsGuests_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_ComparisionTowardsGuests_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_ComparisionTowardsGuests_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_ComparisionTowardsGuests_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_ComparisionTowardsGuests_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_WillingnessToLearn_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_WillingnessToLearn_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_WillingnessToLearn_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_WillingnessToLearn_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_WillingnessToLearn_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_Dependability_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_Dependability_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_Dependability_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_Dependability_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_Dependability_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_AbilityToUseOwnInitiative_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_TeamWork_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_TeamWork_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_TeamWork_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_TeamWork_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_TeamWork_RadioButtonVisibility("Unsatisfactory", true)
              .ValidatePersonalQualities_Communication_RadioButtonVisibility("Excellent", true)
              .ValidatePersonalQualities_Communication_RadioButtonVisibility("Good", true)
              .ValidatePersonalQualities_Communication_RadioButtonVisibility("Fair", true)
              .ValidatePersonalQualities_Communication_RadioButtonVisibility("Poor", true)
              .ValidatePersonalQualities_Communication_RadioButtonVisibility("Unsatisfactory", true)
              .ValidateAchievmentsObstaclesAndActionsTextArea()
              .ValidateAdditionalTrainingTextArea()
              .ValidateActionPlanSignOffTextArea()
              .ValidateActionPlanNSignOff_ReviewHasBeenAgreed_RadioButtonVisibility("Yes, I confirm the review has been agreed and signed off by the relevant parties", true);
        }

        [TestProperty("JiraIssueID", "ACC-3442")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Review From record -> Click Edit icon" +
           "Should display list of questions related to Staff Review Form" +
           "Leave all the mandatory fields as blank and try to save the form.Should display mandatory field error message against all mandatory fields.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_VerifyMandatoryFieldsOfStaffReviewForm()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_defaultLoginUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_CDV6_13557")
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
                .OpenRecord(_staffReviewForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffReviewFormQuestionsRecordPage
            .WaitForEditAssessment_StaffReviewFormPageToLoad()
            .ClickEditAssessmentSaveAndCloseButton()
            .ValidateMandatoryErrorMsgVisible(true);
        }

        [TestProperty("JiraIssueID", "ACC-3443")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Review From record -> Click Edit icon" +
            "Should display list of questions related to Staff Review Form" +
            "Leave all the mandatory fields as blank and try to save the form.Should display mandatory field error message against all mandatory fields.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_VerifyEditAndSaveFieldsOfStaffReviewForm()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);
            var date = DateTime.Now.Date;

            loginPage
                .GoToLoginPage()
                .Login(_defaultLoginUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_CDV6_13557")
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
                .OpenRecord(_staffReviewForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffReviewFormQuestionsRecordPage
           .WaitForEditAssessment_StaffReviewFormPageToLoad()
           .InsertJobDescription_EmployeeJobTitle("title")
           .InsertJobDescription_EmployementStartDate(date.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
           .InsertJobDescription_JobDescription("Job description")
           .SelectPersonalQualities_TimeKeepigShiftHrs_RadioButton("Excellent")
           .SelectPersonalQualities_Punctuality_RadioButton("Poor")
           .SelectPersonalQualities_Appearance_RadioButton("Fair")
           .SelectPersonalQualities_MannerPoliteness_RadioButton("Good")
           .SelectPersonalQualities_ComparisionTowardsGuests_RadioButton("Unsatisfactory")
           .SelectPersonalQualities_WillingnessToLearn_RadioButton("Excellent")
           .SelectPersonalQualities_Dependability_RadioButton("Good")
           .SelectPersonalQualities_AbilityToUseOwnInitiative_RadioButton("Fair")
           .SelectPersonalQualities_TeamWork_RadioButton("Good")
           .SelectPersonalQualities_Communication_RadioButton("Excellent")
           .InsertAcheivmentsObstaclesAndActions_StaffMemberAchievmentsTextArea("StaffMemberachievments")
           .InsertAcheivmentsObstaclesAndActions_WhyTextArea("Why test")
           .InsertAcheivmentsObstaclesAndActions_DifficultiesMemberStaffEncounteredTextArea("difficultiesstaffmemberencountered")
           .InsertAcheivmentsObstaclesAndActions_ActionToBeTakenTextArea("Action to be taken")
           .InsertAdditionalTraining_WatDoIDoBestTextArea("WatTodoidobest")
           .InsertAdditionalTraining_WatDoIDoLessWellTextArea("watdoidolesswell")
           .InsertAdditionalTraining_WatDoIHaveDifficultyWithTextArea("WatDoIHaveDifficultyWith")
           .InsertAdditionalTraining_WatDoIFailToEnjoyTextArea("WatDoIFailToEnjoy")
           .InsertAdditionalTraining_WatWouldMemberstaffToAchieveTextArea("WatWouldMemberstaffToAchieve")
           .InsertAdditionalTraining_AdditionalTrainingWhyTextArea("AdditionalTrainingWhy")
           .InsertAdditionalTraining_WatAdditionalTrainingRequiredTextArea("WatAdditionalTrainingRequired")
           .InsertAdditionalTraining_HowWillThisBeAchievedTextArea("HowWillThisBeAchieved")
           .InsertActionPlanSignOffTextArea("Action", "123")
           .SelectActionPlanNSignOff_ReviewHasBeenAgreed_RadioButton("Yes, I confirm the review has been agreed and signed off by the relevant parties")
           .ClickEditAssessmentSaveAndCloseButton();

            staffReviewFormsRecordPage
               .WaitForStaffReviewFormsRecordPageToLoad();
        }

        [TestProperty("JiraIssueID", "ACC-3444")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> -> Click + Icon and Try to Create New From with document Type Staff Review Form" +
            "Should allow to create multiple Open Forms.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_VerifyMultipleOpenFormsOfStaffReviewForm()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);
            var date = DateTime.Now.Date;

            loginPage
                .GoToLoginPage()
                .Login(_defaultLoginUserName, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_CDV6_13557")
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
                .TypeSearchQuery("Staff Review Form")
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

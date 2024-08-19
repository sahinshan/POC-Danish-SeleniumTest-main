using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [DeploymentItem("Files\\StaffSupervision.Zip"), DeploymentItem("chromedriver.exe")]
    [TestClass]
    public class StaffReview_StaffSupervisionForms_UITestCases : FunctionalTest
    {

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

        [TestInitialize()]
        public void SystemUserStaffSupervision_Setup()
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

                _systemUserFirstName = "Test_User_CDV6_13588_";
                _systemUserLastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserName = "Test_User_CDV6_13588_" + _systemUserLastName;
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

                var documentExists = dbHelper.document.GetDocumentByName("Staff Supervision").Any();
                if (!documentExists)
                {
                    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\StaffSupervision.Zip");
                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, "StaffSupervision.Zip");
                    _documentId = dbHelper.document.GetDocumentByName("Staff Supervision").FirstOrDefault();
                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published
                }
                if (_documentId == Guid.Empty)
                {
                    _documentId = dbHelper.document.GetDocumentByName("Staff Supervision")[0];

                }

                #region Staff Review Setup Record

                var staffReviewSetupExists = dbHelper.staffReviewSetup.GetByName("Staff Supervision").Any();
                if (!staffReviewSetupExists)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup("Staff Supervision", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false);
                }
                if (_staffReviewSetupid == Guid.Empty)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.GetByName("Staff Supervision").FirstOrDefault();
                }

                #endregion

                #endregion


            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13588

        [TestProperty("JiraIssueID", "ACC-3274")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Supervision From record -> Click Edit icon" +
            "Should display list of questions related to Staff Supervision Form.Verify the below questions Strengths,Weaknessesss,Targets achieved,Problems encountered,Training requirements,Objective set for next supervision")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffSupervisionForms_UITestMethod001()
        {

            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffSupervisionForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

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

            System.Threading.Thread.Sleep(1000);

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
                .OpenRecord(_staffSupervisionForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffReviewStaffSupervisionFormRecordPage
                .WaitForEditAssessment_StaffSupervisionFormRecordPageToLoad()
                .VerifyStrengths_TextAreaVisible()
                .VerifyWeakness_TextAreaVisible()
                .VerifyTargetsAchieved_TextAreaVisible()
                .VerifyProblemsEncountered_TextAreaVisible()
                .VerifyTrainingRequirements_TextAreaVisible()
                .VerifyObjectsetForNextSupervision_TextAreaVisible();

        }

        [TestProperty("JiraIssueID", "ACC-3275")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Supervision From record -> Click Edit icon" +
            "Answer all the questions and Save the form.Form should get saved with all the entered / selected answers.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffSupervisionForms_UITestMethod002()
        {

            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffSupervisionForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

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
                .OpenRecord(_staffSupervisionForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffReviewStaffSupervisionFormRecordPage
                .WaitForEditAssessment_StaffSupervisionFormRecordPageToLoad()
                .InsertStrengths_TextArea("Strength test")
                .InsertWeakness_TextArea("Weakness test")
                .InsertTargetsAchieved_TextArea("Targets Achieved Test")
                .InsertProblemsEncountered_TextArea("ProblemsEncountered Test")
                .InsertTrainingRequirements_TextArea("Training requirements Test")
                .InsertObjectsetForNextSupervision_TextArea("Object set for next supervision")
                .ClickEditAssessmentSaveAndCloseButton();

        }

        [TestProperty("JiraIssueID", "ACC-3276")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Click + Icon and Try to Create New From with document Type Staff Supervision Form" +
            "Should allow to create multiple Open Forms.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffSupervisionForms_UITestMethod003()
        {

            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffSupervisionForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

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

            System.Threading.Thread.Sleep(2000);

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
                .TypeSearchQuery("Staff Supervision")
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

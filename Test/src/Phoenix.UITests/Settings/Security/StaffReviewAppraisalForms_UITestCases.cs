using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{

    [DeploymentItem("Files\\StaffAppraisalFormDocument.Zip"), DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\StaffAppraisalFormDocRules.Zip"), DeploymentItem("chromedriver.exe")]

    [TestClass]
    public class StaffReviewAppraisalForms_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _defaultLoginUserID;
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
        private Guid _questionCatalogueId01;
        private Guid _questionCatalogueId02;
        private Guid _questionCatalogueId03;
        private Guid _staffReviewSetupid01;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
        private Guid _reviewedBySystemUserId;
        public Guid _userWorkSchedule;
        public Guid _userWorkSchedule1;
        private string EnvironmentName;
        private string _loginUsername = "CW_Admin_Test_User_2";

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
                    _demographicsTitleId = dbHelper.demographicsTitle.CreateDemographicsTitle("Dr.", DateTime.Now, _careDirectorQA_TeamId);
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

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName(_loginUsername).Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(_loginUsername, "CW", "Admin_Test_User_2", "CW Admin Test User 2", "Passw0rd_!", _loginUsername + "@somemail.com", _loginUsername + "@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                    var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Org Edit)").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsMyRecordsSecurityProfileId);

                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName(_loginUsername).FirstOrDefault();

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

                var documentExists = dbHelper.document.GetDocumentByName("Staff appraisal form").Any();
                if (!documentExists)


                {
                    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\StaffAppraisalFormDocument.Zip");
                    var documentRulesByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\StaffAppraisalFormDocRules.zip");


                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, "StaffAppraisalFormDocument.Zip");
                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentRulesByteArray, "StaffAppraisalFormDocRules.zip");

                    _documentId = dbHelper.document.GetDocumentByName("Staff appraisal form").FirstOrDefault();

                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

                }
                _documentId = dbHelper.document.GetDocumentByName("Staff appraisal form").FirstOrDefault();
                dbHelper.document.UpdateAllowMultipleForms(_documentId, true);
                if (_documentId == Guid.Empty)
                    _documentId = dbHelper.document.GetDocumentByName("Staff appraisal form")[0];

                #endregion

                #region Staff Review Setup Record

                var staffReviewSetupExists = dbHelper.staffReviewSetup.GetByName("Staff appraisal form").Any();
                if (!staffReviewSetupExists)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup("Staff appraisal form", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false);
                }
                if (_staffReviewSetupid == Guid.Empty)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.GetByName("Staff appraisal form").FirstOrDefault();
                }
                #endregion

                #region Staff Review Setup Record

                var staffReviewSetup01Exists = dbHelper.staffReviewSetup.GetByName("Staff appraisal form").Any();
                if (!staffReviewSetup01Exists)
                {
                    _staffReviewSetupid01 = dbHelper.staffReviewSetup.CreateStaffReviewSetup("Staff appraisal form", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false);
                }
                if (_staffReviewSetupid01 == Guid.Empty)
                {
                    _staffReviewSetupid01 = dbHelper.staffReviewSetup.GetByName("Staff appraisal form").FirstOrDefault();

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



        #region https://advancedcsg.atlassian.net/browse/CDV6-13557

        [TestProperty("JiraIssueID", "ACC-3333")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Appraisal From record -> Click Edit icon" +
            "Should display list of questions related to Staff Appraisal Form " +
            "Leave all the mandatory fields as blank and try to save the form.Should display mandatory field error message against all mandatory fields.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod001()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
               .ClickEditAssessmentButton()
               .WaitForEditAssessmentPageToLoad()
               .ClickEditAssessmentSaveAndCloseButton()
               .ValidateMandatoryErrorMsg(true);


        }

        [TestProperty("JiraIssueID", "ACC-3334")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Appraisal From record -> Click Edit icon" +
           "Should display list of questions related to Staff Appraisal Form" +
           "Answer all the questions and Save the form." +
           "Form should get saved with all the entered / selected answers.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod002()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
               .WaitForStaffReviewFormsRecordPageToLoad();
            System.Threading.Thread.Sleep(2000);

            staffReviewFormsRecordPage
               .ClickEditAssessmentButton()
               .WaitForEditAssessmentPageToLoad()
               .WaitForEditAssessmentPageToLoad()
               .ClickYesRadioButton("Do you consider yourself a team player?")
               .ClickEditAssessmentSaveAndCloseButton();


            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad();


        }

        [TestProperty("JiraIssueID", "ACC-3335")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> " +
            "Relates Items -> Forms (Staff Review ) -> Click + Icon and Try to Create New From with document Type Staff Appraisal Form. " +
            "Should allow to create multiple Open Forms.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod003()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
                .TypeSearchQuery("Staff Appraisal Form")
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

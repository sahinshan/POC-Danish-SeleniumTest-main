using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [DeploymentItem("Files\\SpotCheckFormDocument.Zip"), DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\SpotCheckFormDocRules.Zip"), DeploymentItem("chromedriver.exe")]

    [TestClass]
    public class StaffReview_StaffSpotCheckForms_UITestCases : FunctionalTest
    {

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
        private Guid _staffReviewSetupid01;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
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

                var documentExists = dbHelper.document.GetDocumentByName("Staff spot check form").Any();
                if (!documentExists)
                {
                    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\SpotCheckFormDocument.Zip");
                    var documentByteArray1 = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\SpotCheckFormDocRules.Zip");

                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, "SpotCheckFormDocument.Zip");
                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray1, "SpotCheckFormDocRules.Zip");

                    _documentId = dbHelper.document.GetDocumentByName("Staff spot check form").FirstOrDefault();
                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published
                }
                if (_documentId == Guid.Empty)
                {
                    _documentId = dbHelper.document.GetDocumentByName("Staff spot check form")[0];

                }

                #region Staff Review Setup Record

                var staffReviewSetupExists = dbHelper.staffReviewSetup.GetByName("Staff spot check form").Any();
                if (!staffReviewSetupExists)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup("Staff spot check form", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false);
                }
                if (_staffReviewSetupid == Guid.Empty)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.GetByName("Staff spot check form").FirstOrDefault();
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

        #region https://advancedcsg.atlassian.net/browse/CDV6-13572

        [TestProperty("JiraIssueID", "ACC-3262")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Spot Check From record -> Click Edit icon->Should display list of questions related to Staff Sport Check Form" +
            "Verify the below questions Carer is dressed in clean and ironed uniform,ID badge is worn,Care plan is being followed, Equipment(Hoist etc) is being used correctly,Carer carries out safe and appropriate moving and handling techniques,Carer is vigilant for hazards" +
            "Carer visually checks straps for wear before using,Carer puts sling on correctly,Carer attaches straps correctly,Carer checks straps as hoist is raised,Carer pumps up hoist in a calm and steady manner" +
            "Carer communicates with the resident the tasks to be completedCarers communicate with each other the tasks to be completedCarer communicates with resident in a respectful mannerCarer ensures that the resident is in the correct and safe position before lowering" +
            "Carer removes hoist and straps in the correct mannerCarer removes sling from resident correctlyCarer leaves resident in a comfortable positionCarer listens to residentCarer stores hoist and sling in correct and safe position when finished withCarer is wearing appropriate PPE's")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffSpotCheckForms_UITestMethod001()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffSpotCheckForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

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

            System.Threading.Thread.Sleep(2000);

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                 .ClickRefreshButton()
                .OpenRecord(_staffSpotCheckForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffReviewStaffSpotFormRecordPage
                .WaitForEditAssessment_StaffReviewStaffSpotFormRecordPageRecordPageToLoad()
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer is dressed in clean and ironed uniform")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("ID badge is worn")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Care plan is being followed")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Equipment (Hoist etc) is being used correctly")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer carries out safe and appropriate moving and handling techniques")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer is vigilant for hazards")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer visually checks straps for wear before using")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer puts sling on correctly")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer attaches straps correctly")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer checks straps as hoist is raised")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer pumps up hoist in a calm and steady manner")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer communicates with the resident the tasks to be completed")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carers communicate with each other the tasks to be completed")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer communicates with resident in a respectful manner")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer ensures that the resident is in the correct and safe position before lowering")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer removes hoist and straps in the correct manner")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer removes sling from resident correctly")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer leaves resident in a comfortable position")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer listens to resident")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer stores hoist and sling in correct and safe position when finished with")
                .ValidateSpotCheckFormGeneral_RadioBtn_Visible("Carer is wearing appropriate PPE");

        }

        [TestProperty("JiraIssueID", "ACC-3263")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Spot Check From record -> Click Edit icon->Should display list of questions related to Staff Sport Check Form" +
                    "Leave all the mandatory fields as blank and try to save the form.Should display mandatory field error message against all mandatory fields.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffSpotCheckForms_UITestMethod002()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffSpotCheckForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

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

            System.Threading.Thread.Sleep(2000);

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(_staffSpotCheckForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffReviewStaffSpotFormRecordPage
                .WaitForEditAssessment_StaffReviewStaffSpotFormRecordPageRecordPageToLoad()
                .ClickEditAssessmentSaveButton()
                .ValidateMandatoryErrorMsgVisible(true);

        }

        [TestProperty("JiraIssueID", "ACC-3264")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Open Staff Spot Check From record -> Click Edit icon->Should display list of questions related to Staff Sport Check Form" +
                    "Answer all the questions and Save the form.Form should get saved with all the entered / selected answers.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffSpotCheckForms_UITestMethod003()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffSpotCheckForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

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

            System.Threading.Thread.Sleep(2000);

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(_staffSpotCheckForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickEditAssessmentButton();

            staffReviewStaffSpotFormRecordPage
                .WaitForEditAssessment_StaffReviewStaffSpotFormRecordPageRecordPageToLoad()
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer is dressed in clean and ironed uniform")
                .ClickSpotCheckFormGeneral_NoRadioBtn("ID badge is worn")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Care plan is being followed")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Equipment (Hoist etc) is being used correctly")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carer carries out safe and appropriate moving and handling techniques")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer is vigilant for hazards")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer visually checks straps for wear before using")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer puts sling on correctly")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carer attaches straps correctly")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer checks straps as hoist is raised")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carer pumps up hoist in a calm and steady manner")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer communicates with the resident the tasks to be completed")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carers communicate with each other the tasks to be completed")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carer communicates with resident in a respectful manner")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer ensures that the resident is in the correct and safe position before lowering")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carer removes hoist and straps in the correct manner")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer removes sling from resident correctly")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carer leaves resident in a comfortable position")
                .ClickSpotCheckFormGeneral_YesRadioBtn("Carer listens to resident")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carer stores hoist and sling in correct and safe position when finished with")
                .ClickSpotCheckFormGeneral_NoRadioBtn("Carer is wearing appropriate PPE")
                .ClickEditAssessmentSaveAndCloseButton();

            staffReviewFormsRecordPage
               .WaitForStaffReviewFormsRecordPageToLoad();

        }


        [TestProperty("JiraIssueID", "ACC-3265")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Relates Items -> Forms (Staff Review ) -> Click + Icon and Try to Create New From with document Type Staff Spot Check Form" +
                    "Should allow to create multiple Open Forms.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffSpotCheckForms_UITestMethod004()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffSpotCheckForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

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

            System.Threading.Thread.Sleep(2000);

            staffReviewFormsPage
               .WaitForStaffReviewFormsPageToLoad()
               .ClickCreateRecordButton();

            staffReviewFormsRecordPage
               .WaitForStaffReviewFormsRecordPageToLoad()
               .ClickDocumentIdLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Staff spot check form")
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

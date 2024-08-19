using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class StaffReview_WorkPlace_UITestCases : FunctionalTest
    {

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid _authenticationproviderid;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid;
        private Guid _roleid;
        private Guid _questionCatalogueId;
        private Guid _documentId;
        private Guid _documentSectionId;
        private Guid _documentSectionQuestionId;
        private Guid _staffReviewSetupid;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
        private Guid _reviewedBySystemUserId;
        public Guid _staffReviewForm;
        private string EnvironmentName;
        private string _loginUsername;
        private string partialStringSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void SystemUserSatffReview_Setup()
        {
            try
            {

                #region Connection to database

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
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

                #region Create default system user

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_01").Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_01", "CW", "Admin_Test_User_01", "CW Admin Test User 01", "Passw0rd_!", "CW_Admin_Test_User_01@somemail.com", "CW_Admin_Test_User_01@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
                    var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (BU Edit)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsMyRecordsSecurityProfileId);
                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_01").FirstOrDefault();

                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion  Create default system user

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careDirectorQA_TeamId, _defaultLoginUserID);

                #endregion

                #region Create Login User
                _loginUsername = "CW_Admin_Test_User_01" + partialStringSuffix;
                Guid _defaultUserId = Guid.Empty;


                _defaultUserId = dbHelper.systemUser.CreateSystemUser(_loginUsername, "CW", "Admin_Test_User_1" + partialStringSuffix, "CW Admin_Test_User_1" + partialStringSuffix, "Passw0rd_!", "CW_Admin_Test_User_1@somemail.com", "CW_Admin_Test_User_1@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

                //remove any existing profile from the user
                //foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_defaultUserId))
                //    dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

                //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultUserId, systemAdministratorSecurityProfileId);
                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultUserId, systemUserSecureFieldsSecurityProfileId);


                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultUserId, DateTime.Now.Date);

                #endregion

                #region Create SystemUser Record

                _systemUserName = "CDV6_14187_" + partialStringSuffix;
                if (!dbHelper.systemUser.GetSystemUserByUserName(_systemUserName).Any())
                    _systemUserId = dbHelper.systemUser.CreateSystemUser(_systemUserName, "CDV6_14187", partialStringSuffix, "CDV6_14187 " + partialStringSuffix, "Passw0rd_!", "CDV6_14187_" + partialStringSuffix + "@somemail.com", "CDV6_14187_" + partialStringSuffix + "@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                if (_systemUserId == Guid.Empty)
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName(_systemUserName).FirstOrDefault();

                #endregion  Create SystemUser Record

                #region Staff Reviewed by 

                var reviewedByExists = dbHelper.systemUser.GetSystemUserByUserName("00011AAAA_CDV6_14187_User2").Any();
                if (!reviewedByExists)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.CreateSystemUser("00011AAAA_CDV6_14187_User2", "AAAA", "CDV6_14187_User2", "AAAA CDV6_14187_User2", "Passw0rd_!", "00011AAAA_CDV6_14187_User2@somemail.com", "00011AAAA_CDV6_14187_User2@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }

                if (_reviewedBySystemUserId == Guid.Empty)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.GetSystemUserByUserName("00011AAAA_CDV6_14187_User2").FirstOrDefault();
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

                #region Question Catalogue

                var questionCatalogueExists = dbHelper.questionCatalogue.GetByQuestionName("Strengths", 8).Any();
                if (!questionCatalogueExists)
                    dbHelper.questionCatalogue.CreateNumericQuestion("Strengths", "");
                _questionCatalogueId = dbHelper.questionCatalogue.GetByQuestionName("Strengths", 8).First();

                #endregion

                #region Document

                #region Document

                var documentExists = dbHelper.document.GetDocumentByName("Staff Supervision 01").Any();
                if (!documentExists)
                {
                    var documentCategoryId = dbHelper.documentCategory.GetByName("Staff Review Form")[0];
                    var documentTypeId = dbHelper.documentType.GetByName("Initial Assessment")[0];

                    _documentId = dbHelper.document.CreateDocument("Staff Supervision 01", documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                    _documentSectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", _documentId);
                    _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

                }
                if (_documentId == Guid.Empty)
                {
                    _documentId = dbHelper.document.GetDocumentByName("Staff Supervision 01")[0];
                    _documentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "Section 1")[0];
                    //_documentSectionQuestionId = dbHelper.documentSectionQuestion.GetBySectionIdAndQuestionCatalogueId(_documentSectionId, _questionCatalogueId)[0];

                }

                #endregion


                #endregion

                #region Staff Review Setup Record

                var staffReviewSetupExists = dbHelper.staffReviewSetup.GetByName("Staff Supervision 01").Any();
                if (!staffReviewSetupExists)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup("Staff Supervision 01", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false);
                }
                if (_staffReviewSetupid == Guid.Empty)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.GetByName("Staff Supervision 01").FirstOrDefault();
                }
                #endregion

                #region Create Staff Review


                // delete all existining records

                foreach (var staffReviewId in dbHelper.staffReview.GetBySystemUserId(_systemUserId))
                {
                    foreach (var staffReviewAttachmentId in dbHelper.staffReviewAttachment.GetByStaffReviewId(staffReviewId))
                        dbHelper.staffReviewAttachment.DeleteStaffReviewAttachment(staffReviewAttachmentId);

                    foreach (var staffReviewForm in dbHelper.staffReviewForm.GetByStaffReviewId(staffReviewId))
                        dbHelper.staffReviewForm.DeleteStaffReviewForm(staffReviewForm);

                    dbHelper.staffReview.DeleteStaffReview(staffReviewId);
                }

                //var newStaffReviewId01 = dbHelper.staffReview.GetBySystemUserId(_systemUserId).Any();

                //if (!newStaffReviewId01)
                //{
                //    _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, DateTime.Now, null, null, null, null, null, _careDirectorQA_TeamId);
                //}
                //if (_staffReviewId01 == Guid.Empty)
                //{
                //    _staffReviewId01 = dbHelper.staffReview.GetBySystemUserId(_systemUserId).FirstOrDefault();
                //}

                #endregion

                #region Attach Document Type

                var attachDocumentTypeExists = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary").Any();
                if (!attachDocumentTypeExists)
                    dbHelper.attachDocumentType.CreateAttachDocumentType(_careDirectorQA_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));
                _documenttypeid = dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName("Discharge Summary")[0];

                #endregion

                #region Attach Document Sub Type

                var attachDocumentSubTypeExists = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result").Any();
                if (!attachDocumentSubTypeExists)
                    dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);
                _documentsubtypeid = dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName("Confirmed Result")[0];

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13472 (Automation Task ID: https://advancedcsg.atlassian.net/browse/CDV6-14665)

        [TestProperty("JiraIssueID", "ACC-3297")]
        [Description("Login CD -> Work Place -> My work ->  Staff Reviews -> Should take user to Staff Review page and " +
        "display list of existing Staff Review records conducted by the logged in user's team / business unit" +
        "Verify the Filter option in the View -> Verify the Filter " +
        "option in the View(Excellent Performance,Good Performance,Meet basic job requirements ,Needs Improvement,Not acceptable)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod001()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 1, DateTime.Now, null, null, null, null, 1, _careDirectorQA_TeamId);


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Excellent Performance")
                .ClickSortByRagarding()
                .ValidateCellText(1, 9, "Excellent Performance");

        }

        [TestProperty("JiraIssueID", "ACC-3298")]
        [Description("Login CD -> Work Place -> My Work -> Staff Reviews -> Should take user to Staff Review page and display list of " +
         "existing Staff Review records conducted by the logged in user's team / business unit -> Select the option Completed in the View" +
            "Should display only the Staff Review records where the completed date is before or equal to the current date.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod002()
        {
            var _staffReviewId05 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 3, DateTime.Now.Date, null, null, null, null, 1, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewId05.ToString());
        }

        [TestProperty("JiraIssueID", "ACC-3299")]
        [Description("Login CD -> Work Place -> My Work -> Staff Reviews -> Should take user to Staff Review page and " +
            "display list of existing Staff Review records conducted by the logged in user's team / business unit" +
            "Verify the columns of the Staff Review records -> Below columns should be displayed with right values" +
             "Care worker name, Team, Business unit, Review type, Conducted by, Created on, Next review date, Outcome)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod003()
        {
            var _staffReviewId07 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, DateTime.Now, null, DateTime.Parse("12/03/2022"), null, null, 1, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .ClickSortByCreatedOn()
                .ClickSortByCreatedOn()
                .validateRecordAllFieldStatus(_staffReviewId07.ToString(), "Staff Supervision 01", "CareProviders", "CareProviders", "AAAA CDV6_14187_User2", "12/03/2022", "Excellent Performance");

        }

        //in the new menu structure the Workplace menu no longer exists
        //[TestProperty("JiraIssueID", "ACC-3300")]
        //[Description("Login CD -> Work Place -> My Work -> Staff Reviews -> Staff Reviews should be displayed ")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        //[TestProperty("Screen1", "Staff Reviews")]
        //public void StaffReview_WorkPlace_UITestMethod004()
        //{
        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .ClickWorkplaceMenu()
        //        .ValidateStaffReviewsText("Staff Reviews");
        //}

        [TestProperty("JiraIssueID", "ACC-3301")]
        [Description("Login CD -> Work Place -> My Work -> Staff Reviews -> Should take user to Staff Review page and display " +
            "list of existing Staff Review records conducted by the logged in user's team / business unit" +
            "Try to filter the record using Team , Outcome -> Should display results as per the entered value in the Search Box")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod005()
        {
            var _staffReviewId05 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 3, DateTime.Now, null, null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .ClickSortByRagarding()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareProviders").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId);

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickSearchButton()
                .WaitForStaffReviewPageToLoad()
                .ValidateFilterByUsingTeam("CareProviders");

            //.InsertUserName("Needs improvement")  //Quick Search box not filtering outcome column
            //.ClickSearchButton()
            //.ValidateFilterByOutCome ("Needs improvement");
        }

        [TestProperty("JiraIssueID", "ACC-3302")]
        [Description("Login CD -> Work Place -> My Work -> Staff Reviews -> Should take user to Staff Review page and" +
            " display list of existing Staff Review records conducted by the logged in user's team / business unit" +
          "Select the option Outstanding in the View-> Should display only the Staff Review records where reviews with" +
            " a due date after and including the current date and where the completed date is null")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod006()
        {
            var _staffReviewId07 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 1, null, DateTime.Today.AddDays(5), null, null, null, null, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Outstanding")
                .ClickSortByRagarding();

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "regardinguserid", "statusid", "duedate");
            Assert.AreEqual(_systemUserId.ToString(), staffreviewFiels["regardinguserid"].ToString());
            Assert.AreEqual(1, staffreviewFiels["statusid"]);
            Assert.AreEqual(DateTime.Today.AddDays(5).Date, staffreviewFiels["duedate"]);

        }

        [TestProperty("JiraIssueID", "ACC-3303")]
        [Description("Login CD -> Work Place -> My Work -> Staff Reviews -> Should take user to Staff Review page and" +
          " display list of existing Staff Review records conducted by the logged in user's team / business unit" +
        "Select the option Overdue in the View-> Should display only the Staff Review records where reviews with a due date BEFORE " +
          "the current date and the completed date is null")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod007()
        {
            var _staffReviewId05 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("Overdue")
                .ClickSortByRagarding();

            // .ValidateRegardindUserIdField("AAAA CDV6_14187")

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "regardinguserid", "statusid", "duedate");
            Assert.AreEqual(_systemUserId.ToString(), staffreviewFiels["regardinguserid"].ToString());
            Assert.AreEqual(2, staffreviewFiels["statusid"]);
            Assert.AreEqual(DateTime.Now.Date.AddDays(-3).Date, staffreviewFiels["duedate"]);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14457

        [TestProperty("JiraIssueID", "ACC-3304")]
        [Description("Login CD Care Provider->Work Place->My Work->Staff Reviews->Select In Progress in view drop down" +
            "Should display all the previously created Staff Reviews with Status as In Progress")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod008()
        {
            var _staffReviewId = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
               .GoToLoginPage()
               .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
               .WaitForStaffReviewPageToLoad()
               .SelectSystemViewsOption("In Progress")
               .ClickSortByRagarding()
               .OpenRecord(_staffReviewId.ToString());

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "statusid");
            Assert.AreEqual(2, staffreviewFiels["statusid"]);

        }

        [TestProperty("JiraIssueID", "ACC-3305")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Select Completed in view drop down" +
          "Should display all the previously created Staff Reviews with Status as Completed")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod009()
        {
            var _staffReviewId = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 3, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
               .GoToLoginPage()
               .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
               .WaitForStaffReviewPageToLoad()
               .SelectSystemViewsOption("Completed")
               .ClickSortByRagarding()
               .OpenRecord(_staffReviewId.ToString());

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "statusid");
            Assert.AreEqual(3, staffreviewFiels["statusid"]);

        }

        [TestProperty("JiraIssueID", "ACC-3306")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Select Out Standing in view drop down" +
         "Should display all the previously created Staff Reviews with Status as Outstanding")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod010()
        {
            var _staffReviewId = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 1, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
               .GoToLoginPage()
               .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
               .WaitForStaffReviewPageToLoad()
               .SelectSystemViewsOption("Outstanding")
               .ClickSearchAllRecordsCheckbox()
               .ClickRoleLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(_systemUserName).TapSearchButton().SelectResultElement(_roleid);

            staffReviewPage
               .WaitForStaffReviewPageToLoad()
               .ClickSearchButton()
               .WaitForStaffReviewPageToLoad()
               .OpenRecord(_staffReviewId.ToString());

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "statusid");
            Assert.AreEqual(1, staffreviewFiels["statusid"]);

        }

        [TestProperty("JiraIssueID", "ACC-3307")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Select Overdue in view drop down" +
        "Should display only the Staff Review records where reviews with a due date BEFORE the current date and the completed date is null")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod011()
        {
            DateTime dt = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(-3);
            var _staffReviewId = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 1, null, new DateTime(dt.Year, dt.Month, dt.Day), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
               .GoToLoginPage()
               .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
               .WaitForStaffReviewPageToLoad()
               .SelectSystemViewsOption("Outstanding")
               .ClickSortByRagarding()
               .ClickUseRangeCheckbox()
               .InsertDueDateFrom(dt.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertDueDateTo(dt.AddDays(1).ToString("dd'/'MM'/'yyyy"))
               .ClickSearchButton()
               .WaitForStaffReviewPageToLoad()
               .OpenRecord(_staffReviewId.ToString());

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "duedate");
            Assert.AreEqual(new DateTime(dt.Year, dt.Month, dt.Day), staffreviewFiels["duedate"]);
        }

        [TestProperty("JiraIssueID", "ACC-3308")]
        [Description("Login CD Care Provider -> Settings -> Security -> System Users -> Select any existing user -> Menu -> Employment -> My Staff Reviews ->  " +
            "Select Overdue in view dropdown ->Should display only the Staff Review records where reviews with a due date BEFORE the current date and the completed date is null")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod012()
        {
            DateTime dt = DateTime.Now.Date.AddDays(-3);
            var _staffReviewId = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 1, null, new DateTime(dt.Year, dt.Month, dt.Day), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

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
                .SelectSystemViewsOption("Overdue")
                .OpenRecord(_staffReviewId.ToString());

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "duedate");
            Assert.AreEqual(DateTime.Now.Date.AddDays(-3), staffreviewFiels["duedate"]);

        }

        [TestProperty("JiraIssueID", "ACC-3309")]
        [Description("Login CD Care Provider -> Settings -> Security -> System Users -> Select any existing user -> Menu -> Employment -> My Staff Reviews -> " +
            "Select Out Standing in view dropdowm -> Should display all the previously created Staff Reviews with Status as Out Standing")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod013()
        {
            var _staffReviewId = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 1, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                 .GoToLoginPage()
                 .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

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
                .SelectSystemViewsOption("Outstanding")
                .OpenRecord(_staffReviewId.ToString());

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "statusid");
            Assert.AreEqual(1, staffreviewFiels["statusid"]);
        }

        [TestProperty("JiraIssueID", "ACC-3310")]
        [Description("Login CD Care Provider -> Settings -> Security -> System Users -> Select any existing user -> Menu ->" +
        " Employment -> My Staff Reviews ->  Select completed in view dropdown-> " +
        "Should display all the previously created Staff Reviews with Status as Completed")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod015()
        {
            var _staffReviewId = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 3, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                 .GoToLoginPage()
                 .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

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
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewId.ToString());

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "statusid");
            Assert.AreEqual(3, staffreviewFiels["statusid"]);

        }

        [TestProperty("JiraIssueID", "ACC-3311")]
        [Description("Login CD Care Provider -> Settings -> Security -> System Users -> Select any existing user -> Menu " +
        "-> Employment -> My Staff Reviews -> Select In Progress in view dropdown -> Should display all the previously created Staff Reviews with Status as In Progress")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod016()
        {
            var _staffReviewId = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                 .GoToLoginPage()
                 .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

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
                .OpenRecord(_staffReviewId.ToString());

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffreviewFiels = dbHelper.staffReview.GetStaffReviewByID(staffReviews[0], "statusid");
            Assert.AreEqual(2, staffreviewFiels["statusid"]);
        }

        [TestProperty("JiraIssueID", "ACC-3312")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Click + icon to add new record -> Select Completed as status" +
            "Should display a mandatory editable field Completed Date with prefilled value of current date on UI")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod017()
        {
            loginPage
                 .GoToLoginPage()
                 .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

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
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .SelectStatusOption("Completed")
                .ValidateCompletedEditable();
        }

        [TestProperty("JiraIssueID", "ACC-3313")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Click + icon to add new record-> Should display a new mandatory field Status" +
            "Verify the dropdown options of Status field->Should display below fields Completed, In Progress and Outstanding")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod018()
        {
            loginPage
             .GoToLoginPage()
             .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ValidateStatusField()
                .ValidateCompletedOption()
                .ValidateInProgressOption()
                .ValidateOutstandingOption();
        }

        [TestProperty("JiraIssueID", "ACC-3314")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Click + icon to add new record -> Select Out Standing as status" +
        "Completed Date field should be hidden from UI->Fill all mandatory details and Save the record-> Should save the record successfully without  any error for completed date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod019()
        {
            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .SelectStatusOption("Outstanding")
                .ClickRegardinguserLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_systemUserName)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewNewRecordCreatePageToLoad()
               .ClickReviewTypeIdLookUp();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Staff Supervision 01")
               .TapSearchButton()
               .SelectResultElement(_staffReviewSetupid.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewNewRecordCreatePageToLoad()
               .ClickSaveAndCloseButton();
        }

        [TestProperty("JiraIssueID", "ACC-3315")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Click + icon to add new record -> Select In Progress as status" +
        "Completed Date field should be hidden from UI->Fill all mandatory details and Save the record-> Should save the record successfully without  any error for completed date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod020()
        {
            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .SelectStatusOption("In progress")
                .ClickRegardinguserLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("Lookup View")
                .TypeSearchQuery(_systemUserName)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickReviewTypeIdLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Staff Supervision 01")
                .TapSearchButton()
                .SelectResultElement(_staffReviewSetupid.ToString());

            staffReviewRecordPage
                 .WaitForStaffReviewNewRecordCreatePageToLoad()
                 .ClickSaveAndCloseButton();
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14218

        [TestProperty("JiraIssueID", "ACC-3316")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews ->  Select existing Staff Review record -> Related Items -> Attachments" +
            "Should display new option to upload documents in bulk-> Click Upload Multiple Files option -> Choose Document Type and Sub Type - > Select multiple files and Click Start Upload" +
            "Should get success alert for uploaded files And also records should get created under attachment section for each uploaded files with appropriate values")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("Files\\Document2.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReview_WorkPlace_UITestMethod021()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .ValidateUploadMultipleButton("Upload Multiple Files")
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
               .WaitForCreateBulkAttachmentsPopupToLoad()
               .ClickDocumentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Discharge Summary").TapSearchButton().SelectResultElement(_documenttypeid.ToString());

            createBulkAttachmentsPopup
               .WaitForCreateBulkAttachmentsPopupToReload()
               .ClickDocumentSubTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Confirmed Result").TapSearchButton().SelectResultElement(_documentsubtypeid.ToString());

            createBulkAttachmentsPopup
               .WaitForCreateBulkAttachmentsPopupToReload()

               .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document.txt")
               .SelectFileToUpload(TestContext.DeploymentDirectory + "\\Document2.txt")

               .ValidateAttachedFileNameVisibility(1, true)
               .ValidateAttachedFileNameVisibility(2, true)
               .ClickStartUploadButton();

            staffReviewAttachmentsPage
              .WaitForStaffReviewAttachmentsPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffReviewAttachments = dbHelper.staffReviewAttachment.GetByStaffReviewId(staffReviews[0]);
            Assert.AreEqual(2, staffReviewAttachments.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3317")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews ->  Select existing Staff Review record -> Related Items -> Attachments" +
            "Should display new option to upload documents in bulk-> Click Upload Multiple Files option -> Leave either one or all mandatory fields as blank /" +
            " not select any file to upload and  Click Start Upload ->Should get proper alert to fill all the required details , new attachment records should not get created")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReview_WorkPlace_UITestMethod022()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .ValidateUploadMultipleButton("Upload Multiple Files")
                .ClickBulkCreateButton();

            createBulkAttachmentsPopup
                .WaitForCreateBulkAttachmentsPopupToLoad()
                .ClickStartUploadButton()
                .ValidateDocumentTypeErrorLabelText("Please fill out this field.")
                .ValidateDocumentSubTypeErrorLabelText("Please fill out this field.");
        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14295

        [TestProperty("JiraIssueID", "ACC-3318")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Click + icon to add new record-> Should not display field Name" +
            "Fill all the mandatory details and click Save-> Should save the record successfully without any error to fill name")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_WorkPlace_UITestMethod023()
        {
            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ValidateNameFieldNotPresent()
                .ClickReviewTypeIdLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Staff Supervision 01")
                .TapSearchButton()
                .SelectResultElement(_staffReviewSetupid.ToString());

            staffReviewRecordPage
              .WaitForStaffReviewNewRecordCreatePageToLoad()
              .SelectStatusOption("Outstanding")
              .ClickSaveAndCloseButton();
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


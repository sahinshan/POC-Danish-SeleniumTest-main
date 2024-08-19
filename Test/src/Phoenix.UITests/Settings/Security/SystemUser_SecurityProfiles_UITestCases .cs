using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_SecurityProfiles_UITestCases : FunctionalTest
    {
        private string tenantName;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
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
        private Guid _staffReviewId;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
        private string EnvironmentName;


        [TestInitialize()]
        public void SystemUserSatffReview_Setup()
        {
            try
            {

                #region Connection to database

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careDirectorQA_BusinessUnitId, null, "CareDirectorQA@careworkstempmail.com", "Default team for business unit", null);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                #region Create SystemUser Record

                _systemUserName = "CDV6_15660_SecurityProfiles";
                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName(_systemUserName).Any();
                if (!newSystemUser)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser(_systemUserName, "CDV6", "15660-SecurityProfiles", "CDV6 15660 SecurityProfiles", "Smart867@", _systemUserName + "@somemail.com", _systemUserName + "@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true, 1);

                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (BU View)").First();

                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, staffReviewsTeamEditSecurityProfileId);
                }

                if (Guid.Empty == _systemUserId)
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName(_systemUserName).FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);

                #endregion  Create SystemUser Record

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type

                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region system User Employment Contract

                _roleid = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid);

                #endregion

                #region Question Catalogue

                _questionCatalogueId = commonMethodsDB.CreateNumericQuestion("Strengths", "");

                #endregion

                #region Document

                var documentExists = dbHelper.document.GetDocumentByName("Staff Supervision").Any();
                if (!documentExists)
                {
                    var documentCategoryId = dbHelper.documentCategory.GetByName("Staff Review Form")[0];
                    var documentTypeId = dbHelper.documentType.GetByName("Initial Assessment")[0];

                    _documentId = dbHelper.document.CreateDocument("Staff Supervision", documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                    _documentSectionId = dbHelper.documentSection.CreateDocumentSection("General", _documentId);
                    _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

                }
                if (_documentId == Guid.Empty)
                {
                    _documentId = dbHelper.document.GetDocumentByName("Staff Supervision")[0];
                    //  _documentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "General")[0];
                    _documentSectionId = dbHelper.documentSection.GetByDocumentIdAndName(_documentId, "General").FirstOrDefault();
                    _documentSectionQuestionId = dbHelper.documentSectionQuestion.GetBySectionIdAndQuestionCatalogueId(_documentSectionId, _questionCatalogueId)[0];
                }

                #endregion

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

                #region Attach Document Type

                _documenttypeid = commonMethodsDB.CreateAttachDocumentType(_careDirectorQA_TeamId, "Discharge Summary", new DateTime(2020, 1, 1));

                #endregion

                #region Attach Document Sub Type

                _documentsubtypeid = commonMethodsDB.CreateAttachDocumentSubType(_careDirectorQA_TeamId, "Confirmed Result", new DateTime(2020, 1, 1), _documenttypeid);

                #endregion

                #region  Staff Review Attachment

                //delete all existining records
                foreach (var staffReviewAttachmentId in dbHelper.staffReviewAttachment.GetByStaffReviewId(_staffReviewId))
                    dbHelper.staffReviewAttachment.DeleteStaffReviewAttachment(staffReviewAttachmentId);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13507

        [TestProperty("JiraIssueID", "ACC-3266")]
        [Description("Login CD Care Provider -> Work Place -> My Work -> Staff Reviews -> Should display only the list of his own " +
            "staff review records if any exist for the selected view Should not display any other users Staff Review records" +
            "Open any of the Staff Review record -> All the fields should be in disabled mode")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_SecurityProfiles_UITestMethod001()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Smart867@", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .ClickSearchButton()
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ValidateAllFieldsDisableMode();

        }

        [TestProperty("JiraIssueID", "ACC-3267")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Attachments " +
           "Should display list of existing attachments if any -> Open the any of the attachment record -> All the fields should be in disabled mode")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_SecurityProfiles_UITestMethod002()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _attachmentId = dbHelper.staffReviewAttachment.CreateStaffReviewAttachment(_careDirectorQA_TeamId, _staffReviewId01, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt", ".txt", "Document");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Smart867@", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .ClickSearchButton()
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .OpenRecord(_attachmentId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffreviewattachment")
                .ClickOnExpandIcon();

            staffReviewAttchmentsRecordPage
                .WaitForStaffReviewAttchmentsRecordPageToLoad()
                .ValidateAllFieldsDisableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3268")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Attachments " +
            "Should not display + icon and bulk upload icon")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        [TestProperty("Screen2", "Attachments For Staff Review")]
        public void StaffReview_SecurityProfiles_UITestMethod003()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Smart867@", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .ClickSearchButton()
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .ValidateUploadMultipleButtonNotDisplay();

        }

        [TestProperty("JiraIssueID", "ACC-3269")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Attachments " +
            "Should not display delete icon in both summary page and details page of attachment record")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        [TestProperty("Screen2", "Attachments For Staff Review")]
        public void StaffReview_SecurityProfiles_UITestMethod004()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _attachmentId = dbHelper.staffReviewAttachment.CreateStaffReviewAttachment(_careDirectorQA_TeamId, _staffReviewId01, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt", ".txt", "Document");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Smart867@", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
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
                .ValidateDeleteButtonNotDisplay()
                .OpenRecord(_attachmentId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffreviewattachment")
                .ClickOnExpandIcon();

            staffReviewAttchmentsRecordPage
                .WaitForStaffReviewAttchmentsRecordPageToLoad()
                .ValidateDeleteButtonNotDisplay();
        }

        [TestProperty("JiraIssueID", "ACC-3270")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Forms" +
            "Should display list of existing Forms if any -> Open the any of the Form record -> All the fields should be in disabled mode Questions(Edit the form) of the form also should be in disabled mode")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        [TestProperty("Screen2", "Forms Staff Review")]
        public void StaffReview_SecurityProfiles_UITestMethod005()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Smart867@", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .ClickSearchButton()
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
                .ValidateAllFieldsDisableMode();
        }

        [TestProperty("JiraIssueID", "ACC-3271")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Forms" +
            "Should not display + icon to add new form.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        [TestProperty("Screen2", "Forms Staff Review")]
        public void StaffReview_SecurityProfiles_UITestMethod006()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Smart867@", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .ClickSearchButton()
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ValidateCreateRecordButtonNotDisplay();
        }

        [TestProperty("JiraIssueID", "ACC-3272")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Forms" +
            "Should not display delete icon in both summary page and details page of forms record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        [TestProperty("Screen2", "Forms Staff Review")]
        public void StaffReview_SecurityProfiles_UITestMethod007()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Smart867@", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .ClickSearchButton()
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ValidateCreateRecordButtonNotDisplay()
                .OpenRecord(_staffReviewForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ValidateDeleteRecordButtonNotDisplay();
        }

        [TestProperty("JiraIssueID", "ACC-3273")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews ->  Should not display + icon to create new Staff Review record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_SecurityProfiles_UITestMethod008()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Smart867@", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, false, false, true, true)
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ValidateCreateRecordButtonNotDisplay();
        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


        [TestCleanup()]
        public virtual void InternalTestCleanup()
        {
            driver.Quit();
        }
    }
}


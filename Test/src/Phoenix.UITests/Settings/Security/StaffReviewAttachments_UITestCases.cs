using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class StaffReviewAttachments_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserId;
        private Guid _authenticationproviderid;
        private Guid _maritalStatusId;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid;
        private Guid _roleid;
        private Guid _questionCatalogueId;
        private Guid _documentId;
        private Guid _documentSectionId;
        private Guid _documentSectionQuestionId;
        private Guid _staffReviewSetupid;
        private Guid _staffReviewId01;
        private Guid _attachmentId;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
        private Guid _reviewedBySystemUserId;
        private Guid _recurrencePatternId;
        public Guid _userWorkSchedule;
        public Guid _userWorkSchedule1;
        private string loginUsername;
        private string partialStringSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        [TestInitialize()]
        public void StaffReviewAttachments_SetupMethod()
        {
            try
            {

                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);

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

                #region Recurrence pattern

                var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
                if (!recurrencePatternExists)
                    _recurrencePatternId = dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

                if (_recurrencePatternId == Guid.Empty)
                    _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

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

                #region Create default system user
                loginUsername = "CW_Admin_Test_User_01" + partialStringSuffix;
                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName(loginUsername).Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(loginUsername, "CW", "Admin_Test_User_01" + partialStringSuffix, "CW Admin Test User 01" + partialStringSuffix, "Passw0rd_!", "CW_Admin_Test_User_01@somemail.com", "CW_Admin_Test_User_01@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
                    //var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (BU Edit)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsMyRecordsSecurityProfileId);
                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName(loginUsername).FirstOrDefault();



                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careDirectorQA_TeamId, _defaultLoginUserID);

                #endregion

                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_CDV6_13800").Any();
                if (!newSystemUser)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser("CW_Forms_Test_User_CDV6_13800", "CW", "Forms_Test_User_CDV6_13800", "CW" + "Kumar", "Passw0rd_!", "CW_Forms_Test_User_CDV6_13800@somemail.com", "CW_Forms_Test_User_CDV6_13800@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }
                if (_systemUserId == Guid.Empty)
                {
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_CDV6_13800").FirstOrDefault();
                }
                #endregion

                #region Staff Reviewed by 

                var reviewedByExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13800_User2").Any();
                if (!reviewedByExists)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.CreateSystemUser("CW_Test_User_CDV6_13800_User2", "CW", "Test_User_2", "CW" + "Test_User_2", "Passw0rd_!", "CW_Test_User_CDV6_13800_User2@somemail.com", "CW_Test_User_CDV6_13800_User2@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }

                if (_reviewedBySystemUserId == Guid.Empty)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13800_User2").FirstOrDefault();
                }

                #endregion

                #region Create New User WorkSchedule

                var newUserWorkSchedule = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserId).Any();

                if (!newUserWorkSchedule)
                {
                    _userWorkSchedule = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default Schedule", _systemUserId, _careDirectorQA_TeamId, _recurrencePatternId, DateTime.Now.AddDays(-15), null, TimeSpan.FromHours(10), TimeSpan.FromHours(18));

                }
                if (_userWorkSchedule == Guid.Empty)
                {
                    _userWorkSchedule = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserId).FirstOrDefault();
                }

                var newUserWorkSchedule1 = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_reviewedBySystemUserId).Any();
                if (!newUserWorkSchedule1)
                {
                    _userWorkSchedule1 = dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default Schedule", _reviewedBySystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, DateTime.Now.AddDays(-15), null, TimeSpan.FromHours(10), TimeSpan.FromHours(18));

                }
                if (_userWorkSchedule1 == Guid.Empty)
                {
                    _userWorkSchedule1 = dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_reviewedBySystemUserId).FirstOrDefault();

                }

                #endregion Create New User WorkSchedule

                dbHelper = new DBHelper.DatabaseHelper(loginUsername, "Passw0rd_!", tenantName);

                #region care provider staff role type

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

                var newStaffReviewId01 = dbHelper.staffReview.GetBySystemUserId(_systemUserId).Any();

                if (!newStaffReviewId01)
                {
                    _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, DateTime.Now, null, null, null, null, null, _careDirectorQA_TeamId);
                }
                if (_staffReviewId01 == Guid.Empty)
                {
                    _staffReviewId01 = dbHelper.staffReview.GetBySystemUserId(_systemUserId).FirstOrDefault();
                }

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

                dbHelper = new DBHelper.DatabaseHelper(tenantName);
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13800

        [TestProperty("JiraIssueID", "ACC-3336")]
        [Description("Navigate to the Systemuser record page -> open the systemuser record -> select submenu navigate to staffreview page -> Open existing staffreview record" +
            "select related items attachments-> validate attachments displayed -> click attachments -> validate  + icon to add new attachments ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewAttachments_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CW_Forms_Test_User_CDV6_13800")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ValidateAttachmentMenu("Attachments")
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .ValidateAttachmentDisplayed("Attachments (For Staff Review)")
                .ValidateNewRecordCreateButton("New");
        }

        [TestProperty("JiraIssueID", "ACC-3337")]
        [Description("Navigate to the Systemuser record page -> open the systemuser record -> select submenu navigate to staffreview page -> Open existing staffreview record" +
            "select related items attachments -> click + icon to add new record -> Should take user to fill the details related to attachment and to upload the file" +
            "Fill all mandatory and non mandatory fields and upload a valid file -> Click Save -> " +
            "validate File should be uploaded successfully and new attachment record should get displayed under the Attachment Section with correct values. ")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewAttachments_UITestMethod02()
        {

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CW_Forms_Test_User_CDV6_13800")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .ClickCreateNewRecord();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffreviewattachment")
                .ClickOnExpandIcon();

            staffReviewAttchmentsRecordPage
              .WaitForStaffReviewAttchmentsRecordPageToLoad()
              .InsertTitle("test")
              .InsertDate(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "20:30")
              .ClickDocumentType_LookUp();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Discharge Summary").TapSearchButton().SelectResultElement(_documenttypeid.ToString());

            staffReviewAttchmentsRecordPage
                .WaitForStaffReviewAttchmentsRecordPageToLoad()
                .ClickSubDocumentType_LookUp();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Confirmed Result").TapSearchButton().SelectResultElement(_documentsubtypeid.ToString());

            staffReviewAttchmentsRecordPage
                .WaitForStaffReviewAttchmentsRecordPageToLoad()
                .FileUpload(TestContext.DeploymentDirectory + "\\Document.txt")
                .ClickSaveAndCloseButton();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var staffReviewAppointments = dbHelper.staffReviewAttachment.GetByStaffReviewId(_staffReviewId01);
            Assert.AreEqual(1, staffReviewAppointments.Count);

            staffReviewAttachmentsPage
                .OpenRecord(staffReviewAppointments[0].ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffreviewattachment")
                .ClickOnExpandIcon();

            staffReviewAttchmentsRecordPage
              .WaitForStaffReviewAttchmentsRecordPageToLoad()
              .ValidateTitle("test")
              .ValidateDate(DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "20:30")
              .ValidateDocumentTypeLinkFieldText("Discharge Summary")
              .ValidateDocumentSubTypeLinkFieldText("Confirmed Result")
              .ValidateFileLinkText("Document.txt");
        }

        [TestProperty("JiraIssueID", "ACC-3338")]
        [Description("Navigate to the Systemuser record page->open the systemuser record->select submenu navigate to staffreview page->Open existing staffreview record" +
            "select related items attachments -> click + icon to add new record -> Should take user to fill the details related to attachment and to upload the file" +
            "Leave all mandatory fields as blank -> Click Save -> validate Error message should be displayed against each mandatory field and record should not get created.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewAttachments_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CW_Forms_Test_User_CDV6_13800")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .ClickCreateNewRecord();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffreviewattachment")
                .ClickOnExpandIcon();

            staffReviewAttchmentsRecordPage
                .WaitForStaffReviewAttchmentsRecordPageToLoad()
                .ClickSaveAndCloseButton()
                .ValidateTitleFieldErrormessage("Please fill out this field.")
                .ValidateDateFieldErrormessage("Please fill out this field.")
                .ValidateTimeFieldErrormessage("Please fill out this field.")
                .ValidateDocumentTypeErrormessage("Please fill out this field.")
                .ValidateDocumentSubTypeErrormessage("Please fill out this field.")
                .ValidateFileErrormessage("Please fill out this field.");
        }

        [TestProperty("JiraIssueID", "ACC-3339")]
        [Description("Navigate to the Systemuser record page->open the systemuser record->select submenu navigate to staffreview page->Open existing staffreview record" +
             "Staff review record->Related Items->Attachments->  Click on Delete Icon and give Yes in Confirmation Pop up for deletion -> Attachment record should be deleted successfully..")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewAttachments_UITestMethod04()
        {
            _attachmentId = dbHelper.staffReviewAttachment.CreateStaffReviewAttachment(_careDirectorQA_TeamId, _staffReviewId01, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt", ".txt", "Document");

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CW_Forms_Test_User_CDV6_13800")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .SelectAttchmentRecord(_attachmentId.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("1 item(s) deleted.")
               .TapOKButton();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad();

            var attachmentId = dbHelper.staffReviewAttachment.GetByStaffReviewId(_staffReviewId01);
            Assert.AreEqual(0, attachmentId.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3340")]
        [Description("Navigate to the Systemuser record page->open the systemuser record->select submenu navigate to staffreview page->Open existing staffreview record" +
            "select related items attachments ->  Open any of existing record and Click on Delete Icon and give Yes in Confirmation Pop up for deletion." +
            "Attachment record should be deleted successfully")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewAttachments_UITestMethod05()
        {
            _attachmentId = dbHelper.staffReviewAttachment.CreateStaffReviewAttachment(_careDirectorQA_TeamId, _staffReviewId01, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt", ".txt", "Document");

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CW_Forms_Test_User_CDV6_13800")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
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
                .ClickAdditionalItemsMenuButton()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .ClickRefreshButton();

            var attachmentId = dbHelper.staffReviewAttachment.GetByStaffReviewId(_staffReviewId01);
            Assert.AreEqual(0, attachmentId.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3341")]
        [Description("Navigate to the Systemuser record page->open the systemuser record->select submenu navigate to staffreview page->Open existing staffreview record" +
            "select related items attachments -> validate Try to filter the record using Title , Document Type , Document Sub Type -> validate display results as per the entered value in the Search Box")]
        [DeploymentItem("Files\\Document.txt"), DeploymentItem("chromedriver.exe")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewAttachments_UITestMethod06()
        {
            _attachmentId = dbHelper.staffReviewAttachment.CreateStaffReviewAttachment(_careDirectorQA_TeamId, _staffReviewId01, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt", ".txt", "Document");

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CW_Forms_Test_User_CDV6_13800")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickAttachmentLink();

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .InsertQuickSearchText("test")
                .ClickQuickSearchButton()
                .ValidateTitleCellText("test");

            staffReviewAttachmentsPage
                .WaitForStaffReviewAttachmentsPageToLoad()
                .InsertQuickSearchText("Discharge Summary")
                .ClickQuickSearchButton()
                .ValidateDocumentTypeCellText("Discharge Summary");

            staffReviewAttachmentsPage
                .InsertQuickSearchText("Confirmed Result")
                .ClickQuickSearchButton()
                .ValidateSubDocumentTypeCellText("Confirmed Result");
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

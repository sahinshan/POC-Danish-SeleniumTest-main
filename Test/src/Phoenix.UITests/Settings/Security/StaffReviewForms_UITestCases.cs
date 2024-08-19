using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [DeploymentItem("Files\\StaffReviewFormDocument.Zip"), DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\StaffReviewFormRules.Zip"), DeploymentItem("chromedriver.exe")]
    [TestClass]
    public class StaffReviewForms_UITestCases : FunctionalTest
    {

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserId;
        private string _systemUsername;
        private Guid _authenticationproviderid;
        private Guid _demographicsTitleId;
        private Guid _maritalStatusId;
        private Guid _ethnicityId;
        private Guid _transportTypeId;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid;
        private Guid _roleid;
        private Guid _questionCatalogueId;
        private Guid _documentId;
        private Guid _StaffReviewdocumentId;
        private Guid _documentSectionId;
        private Guid _documentSectionQuestionId;
        private Guid _staffReviewSetupid;
        private Guid _questionCatalogueId01;
        private Guid _questionCatalogueId02;
        private Guid _questionCatalogueId03;
        private Guid _documentId01;
        private string _documentName01;
        private Guid _documentSectionId01;
        private Guid _documentSectionQuestionId01;
        private Guid _documentSectionQuestionId02;
        private Guid _documentSectionQuestionId03;
        private Guid _staffReviewSetupid01;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
        private Guid _reviewedBySystemUserId;
        public Guid _userWorkSchedule;
        public Guid _userWorkSchedule1;
        private string EnvironmentName;
        private Guid documentQuesIdentifier1;
        private Guid documentQuesIdentifier2;
        private Guid documentQuesIdentifier3;
        private string _loginUsername;
        private string partialStringSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                _loginUsername = "Admin_Test_User_2_" + partialStringSuffix;
                _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(_loginUsername, "Admin", "Test_User_2_" + partialStringSuffix, "Admin Test User 2 " + partialStringSuffix, "Passw0rd_!", "Admin_Test_User_2@somemail.com", "Admin_Test_User_2@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();
                //var staffReviewsMyRecordsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Org Edit)").First();
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsMyRecordsSecurityProfileId);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careDirectorQA_TeamId, _defaultLoginUserID);

                #endregion

                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("StaffReviewForm_CDV6_14187").Any();
                if (!newSystemUser)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser("StaffReviewForm_CDV6_14187", "StaffReviewForm", "CDV6_14187", "StaffReviewForm CDV6_14187", "Passw0rd_!", "StaffReviewForm_CDV6_14187@somemail.com", "StaffReviewForm_CDV6_14187@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }
                if (_systemUserId == Guid.Empty)
                {
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("StaffReviewForm_CDV6_14187").FirstOrDefault();
                    _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "username")["username"];
                }

                #endregion  Create SystemUser Record

                #region Staff Reviewed by 

                var reviewedByExists = dbHelper.systemUser.GetSystemUserByUserName("StaffReviewForm_CDV6_14187_User2").Any();
                if (!reviewedByExists)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.CreateSystemUser("StaffReviewForm_CDV6_14187_User2", "AAAA", "CDV6_14187_User2", "AAAA CDV6_14187_User2", "Passw0rd_!", "StaffReviewForm_CDV6_14187_User2@somemail.com", "StaffReviewForm_CDV6_14187_User2@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }

                if (_reviewedBySystemUserId == Guid.Empty)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.GetSystemUserByUserName("StaffReviewForm_CDV6_14187_User2").FirstOrDefault();
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

                #region create staff review form document
                var staffreviewformdocumentExists = dbHelper.document.GetDocumentByName("Staff Review form").Any();
                if (!staffreviewformdocumentExists)


                {
                    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\StaffReviewFormDocument.Zip");
                    var documentRulesByteArray = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\StaffReviewFormRules.zip");

                    //  var documentCategoryId = dbHelper.documentCategory.GetByName("Staff Review Form")[0];
                    //  var documentTypeId = dbHelper.documentType.GetByName("Initial Assessment")[0];

                    // _documentId = dbHelper.document.CreateDocument("Staff appraisal form", documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                    //  _documentSectionId = dbHelper.documentSection.CreateDocumentSection("General", _documentId);
                    //  _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);


                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, "StaffReviewFormDocument.Zip");
                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentRulesByteArray, "StaffReviewFormRules.zip");

                    _StaffReviewdocumentId = dbHelper.document.GetDocumentByName("Staff Review form").FirstOrDefault();

                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

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

                #region Question Catalogue

                var questionCatalogue01Exists = dbHelper.questionCatalogue.GetByQuestionName("Process being observed").Any();
                if (!questionCatalogue01Exists)
                    dbHelper.questionCatalogue.CreateQuestion("Process being observed", "", 6);
                _questionCatalogueId01 = dbHelper.questionCatalogue.GetByQuestionName("Process being observed").First();

                var questionCatalogue02Exists = dbHelper.questionCatalogue.GetByQuestionName("Location").Any();
                if (!questionCatalogue02Exists)
                    dbHelper.questionCatalogue.CreateQuestion("Location", "", 6);
                _questionCatalogueId02 = dbHelper.questionCatalogue.GetByQuestionName("Location").First();

                var questionCatalogue03Exists = dbHelper.questionCatalogue.GetByQuestionName("Notes").Any();
                if (!questionCatalogue03Exists)
                    dbHelper.questionCatalogue.CreateQuestion("Notes", "", 6);
                _questionCatalogueId03 = dbHelper.questionCatalogue.GetByQuestionName("Notes").First();

                #endregion

                #region Document

                var document01Exists = dbHelper.document.GetDocumentByName("Staff Shadowing Observations Form").Any();
                if (!document01Exists)
                {
                    var documentCategoryId01 = dbHelper.documentCategory.GetByName("Staff Review Form")[0];
                    var documentTypeId01 = dbHelper.documentType.GetByName("Initial Assessment")[0];

                    _documentId01 = dbHelper.document.CreateDocument("Staff Shadowing Observations Form", documentCategoryId01, documentTypeId01, _careDirectorQA_TeamId, 1);
                    _documentSectionId01 = dbHelper.documentSection.CreateDocumentSection("Staff Shadowing Observations", _documentId01);
                    _documentSectionQuestionId01 = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId01, _documentSectionId01);
                    _documentSectionQuestionId02 = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId02, _documentSectionId01);
                    _documentSectionQuestionId03 = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId03, _documentSectionId01);
                    documentQuesIdentifier2 = dbHelper.documentQuestionIdentifier.GetByDocumentSectionQuestionId(_documentSectionQuestionId02).First();
                    documentQuesIdentifier1 = dbHelper.documentQuestionIdentifier.GetByDocumentSectionQuestionId(_documentSectionQuestionId01).First();
                    documentQuesIdentifier3 = dbHelper.documentQuestionIdentifier.GetByDocumentSectionQuestionId(_documentSectionQuestionId03).First();
                    dbHelper.document.UpdateStatus(_documentId01, 100000000); //Set the status to published                    

                }
                if (_documentId01 == Guid.Empty)
                {
                    _documentId01 = dbHelper.document.GetDocumentByName("Staff Shadowing Observations Form")[0];
                    _documentSectionId01 = dbHelper.documentSection.GetByDocumentIdAndName(_documentId01, "Staff Shadowing Observations")[0];
                    _documentSectionQuestionId01 = dbHelper.documentSectionQuestion.GetBySectionIdAndQuestionCatalogueId(_documentSectionId01, _questionCatalogueId01)[0];
                    _documentSectionQuestionId02 = dbHelper.documentSectionQuestion.GetBySectionIdAndQuestionCatalogueId(_documentSectionId01, _questionCatalogueId02)[0];
                    _documentSectionQuestionId03 = dbHelper.documentSectionQuestion.GetBySectionIdAndQuestionCatalogueId(_documentSectionId01, _questionCatalogueId03)[0];
                    documentQuesIdentifier1 = dbHelper.documentQuestionIdentifier.GetByDocumentSectionQuestionId(_documentSectionQuestionId01).First();
                    documentQuesIdentifier2 = dbHelper.documentQuestionIdentifier.GetByDocumentSectionQuestionId(_documentSectionQuestionId02).First();
                    documentQuesIdentifier3 = dbHelper.documentQuestionIdentifier.GetByDocumentSectionQuestionId(_documentSectionQuestionId03).First();
                }
                _documentName01 = (string)dbHelper.document.GetDocumentByID(_documentId01, "name")["name"];

                #endregion

                #region Staff Review Setup Record

                var staffReviewSetup01Exists = dbHelper.staffReviewSetup.GetByName("Staff Shadowing Observations Form").Any();
                if (!staffReviewSetup01Exists)
                {
                    _staffReviewSetupid01 = dbHelper.staffReviewSetup.CreateStaffReviewSetup("Staff Shadowing Observations Form", _documentId01, new DateTime(2021, 1, 1), "for automation", true, true, false);
                }
                if (_staffReviewSetupid01 == Guid.Empty)
                {
                    _staffReviewSetupid01 = dbHelper.staffReviewSetup.GetByName("Staff Shadowing Observations Form").FirstOrDefault();

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

        #region https://advancedcsg.atlassian.net/browse/CDV6-13506

        [TestProperty("JiraIssueID", "ACC-3446")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Forms" +
            "Should display + icon to add new form -> Form should get added successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod001()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
                .ValidateDisplayedFormsLink("Forms (Staff Review)")
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ValidateCreateRecordButton("New")
                .ClickCreateRecordButton();

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ClickDocumentIdLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Staff Shadowing Observations Form")
                .TapSearchButton()
                .SelectResultElement(_documentId01.ToString());

            staffReviewFormsRecordPage
               .WaitForStaffReviewFormsRecordPageToLoad()
               .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveAndCloseButton();

            staffReviewFormsPage
               .WaitForStaffReviewFormsPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var staffReviewForms = dbHelper.staffReviewForm.GetByStaffReviewId(_staffReviewId01);
            Assert.AreEqual(1, staffReviewForms.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3447")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Forms" +
            "Should display delete icon in both summary page and details page of forms record -> Try to delete the record from Summary and Details page" +
            "Record should be deleted successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod002()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm01 = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId01, 1, DateTime.Now, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ValidateDeleteRecordButton("Delete")
                .SelectRecord(_staffReviewForm01.ToString())
                .ClickDeleteRecordButton();

            alertPopup
              .WaitForAlertPopupToLoad()
              .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
              .TapOKButton();

            alertPopup
              .WaitForAlertPopupToLoad()
              .ValidateAlertText("1 item(s) deleted.")
              .TapOKButton();


            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .OpenRecord(_staffReviewForm.ToString());

            staffReviewFormsRecordPage
               .WaitForStaffReviewFormsRecordPageToLoad()
               .ClickAdditionalItemsMenuButton()
               .ValidateDeleteRecordButton("Delete")
               .ClickDeleteRecordButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
               .TapOKButton();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .ClickRefreshButton();

            var staffReviewForms = dbHelper.staffReviewForm.GetByStaffReviewId(_staffReviewId01);
            Assert.AreEqual(0, staffReviewForms.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3448")]
        [DeploymentItem("Files\\Document.txt")]
        [DeploymentItem("chromedriver.exe")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Attachments " +
            "Should display list of existing attachments if any -> Open the any of the attachment record ->Record should be in editable mode")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewForms_UITestMethod003()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _attachmentId = dbHelper.staffReviewAttachment.CreateStaffReviewAttachment(_careDirectorQA_TeamId, _staffReviewId01, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt", ".txt", "Document");

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
                .OpenRecord(_attachmentId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffreviewattachment")
                .ClickOnExpandIcon();

            staffReviewAttchmentsRecordPage
                .WaitForStaffReviewAttchmentsRecordPageToLoad()
                .ValidateAttachmentRecordEditAble();
        }

        [TestProperty("JiraIssueID", "ACC-3449")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Forms" +
            "Should display list of existing Forms if any -> Open the any of the Form record -> Record should be in editable mode also should be able to answers the " +
            "Questions(Edit the form) of the form")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod004()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad()
                .OpenRecord(_staffReviewForm.ToString());

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad()
                .ValidateExistsFormRecordEditable();
        }

        [TestProperty("JiraIssueID", "ACC-3450")]
        [DeploymentItem("Files\\Document.txt")]
        [DeploymentItem("Files\\Document2.txt")]
        [DeploymentItem("chromedriver.exe")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Attachments " +
          "Should display + icon and bulk upload icon -> Try to upload attachments using + icon and bulk upload icon -> Should allow manger to upload new attachments")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewForms_UITestMethod005()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            var attachmentDate = DateTime.Now.AddDays(-3);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
                .ValidateNewRecordCreateButton("New")
                .ValidateUploadMultipleButton("Upload Multiple Files")
                .ClickCreateNewRecord();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("staffreviewattachment")
                .ClickOnExpandIcon();

            staffReviewAttchmentsRecordPage
              .WaitForStaffReviewAttchmentsRecordPageToLoad()
              .InsertTitle("test")
              .InsertDate(attachmentDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "20:30")
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
               .WaitForStaffReviewAttachmentsPageToLoad()
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

            System.Threading.Thread.Sleep(1200);

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReviews.Count);

            var staffReviewAttachments = dbHelper.staffReviewAttachment.GetByStaffReviewId(staffReviews[0]);
            Assert.AreEqual(3, staffReviewAttachments.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3451")]
        [DeploymentItem("Files\\Document.txt")]
        [DeploymentItem("chromedriver.exe")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of his own the Staff Review record -> Menu -> Related Items -> Attachments " +
            "Should display delete icon in both summary page and details page of attachment record -> Try to delete the existing attachments from summary and details page -> Record should be deleted successfully.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Attachments For Staff Review")]
        public void StaffReviewForms_UITestMethod006()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _attachmentId = dbHelper.staffReviewAttachment.CreateStaffReviewAttachment(_careDirectorQA_TeamId, _staffReviewId01, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt", ".txt", "Document");
            var _attachmentId01 = dbHelper.staffReviewAttachment.CreateStaffReviewAttachment(_careDirectorQA_TeamId, _staffReviewId01, "test", DateTime.Now, _documenttypeid, _documentsubtypeid, TestContext.DeploymentDirectory + "\\Document.txt", ".txt", "Document");

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

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
                .SelectAttchmentRecord(_attachmentId01.ToString())
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

        [TestProperty("JiraIssueID", "ACC-3452")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews -> Open any of the Staff Review record ->  Record should be in editable mode")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReviewForms_UITestMethod007()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .SelectSystemViewsOption("In Progress")
                .OpenRecord(_staffReviewId01.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ValidateExsistingRecordEditable();
        }

        [TestProperty("JiraIssueID", "ACC-3453")]
        [Description("Login CD as a Staff User -> Work Place -> My Work -> Staff Reviews ->  Should display + icon to create new Staff Review record" +
            "Try to create new Staff Review Record -> Staff Review record should be created successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReviewForms_UITestMethod008()
        {
            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
               .WaitForStaffReviewNewRecordCreatePageToLoad()
               .ClickRegardinguserLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("StaffReviewForm_CDV6_14187")
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewNewRecordCreatePageToLoad()
               .ClickReviewTypeIdLookUp();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Staff Shadowing Observations Form")
               .TapSearchButton()
               .SelectResultElement(_staffReviewSetupid01.ToString());

            staffReviewRecordPage
               .WaitForStaffReviewNewRecordCreatePageToLoad()
               .SelectStatusOption("Outstanding")
               .ClickSaveAndCloseButton();

            staffReviewPage
              .WaitForStaffReviewPageToLoad();

            System.Threading.Thread.Sleep(1500);

            var staffReview = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, staffReview.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15049

        [TestProperty("JiraIssueID", "ACC-3454")]
        [Description("Login Care Provider -> Settings -> System User -> Select any existing System User who has Staff Review and a Form tagged to it" +
            " -> Menu -> Employment -> Staff Reviews -> Select any existing record which has Staff Review Form tagged to it" +
            "Should take to Staff Review details page In same screen should display a section for Staff Review Form and its existing record " +
            "details User should be able to select and work on existing Staff review form record Addition of new record and Delete also should work as usual")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod009()
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
                .InsertUserName(_systemUsername)
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
               .ClickSaveAndCloseButton();

            staffReviewFormsPage
               .WaitForStaffReviewFormsPageToLoad()
               .SelectRecord(_staffReviewForm.ToString())
               .ClickDeleteRecordButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
               .TapOKButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("1 item(s) deleted.")
               .TapOKButton();

            var staffReviewForms = dbHelper.staffReviewForm.GetByStaffReviewId(_staffReviewId01);
            Assert.AreEqual(0, staffReviewForms.Count);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15192

        [TestProperty("JiraIssueID", "ACC-3455")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Go to Forms section at bottom  ->" +
            "Click + Icon and Try to Create New From with document Type Staff Shadowing Observations form ->Should allow to create multiple Open Forms")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod010()
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
                .InsertUserName(_systemUsername)
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
                .TypeSearchQuery(_documentName01)
                .TapSearchButton()
                .SelectResultElement(_documentId01.ToString());

            staffReviewFormsRecordPage
               .WaitForStaffReviewFormsRecordPageToLoad()
               .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
               .ClickSaveAndCloseButton();

            staffReviewRecordPage
                .WaitForStaffReviewRecordPageToLoad()
                .ClickSubMenu()
                .ClickFormsLink();

            staffReviewFormsPage
                .WaitForStaffReviewFormsPageToLoad();

        }

        [TestProperty("JiraIssueID", "ACC-3456")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> " +
            "Go Form section at bottom -> Open Staff Shadowing Observations form record -> Click Edit icon -> Should display list of questions related to Staff Shadowing Observations form" +
            "Answer all the questions and Save the form ->Form should get saved with all the entered / selected answers")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod011()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId01, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_CDV6_14187")
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
                .InsertQuestionTextArea(documentQuesIdentifier1.ToString(), "Testing")
                .InsertQuestionTextArea(documentQuesIdentifier2.ToString(), "Chennai")
                .InsertQuestionTextArea(documentQuesIdentifier3.ToString(), "someting")
                .ClickEditAssessmentSaveAndCloseButton();

            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad();
        }

        [TestProperty("JiraIssueID", "ACC-3457")]
        [Description("Login CD -> Select any existing System User -> Related Items - > Staff Reviews -> Select existing Staff Review record -> Go Form section at bottom -> " +
            "Open Staff Shadowing Observations form record -> Click Edit icon -> Should display list of questions related to Staff Shadowing Observations form-> Verify the questions" +
            "Should display all the below questions in respective format -> Process being observed - Free text ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Forms Staff Review")]
        public void StaffReviewForms_UITestMethod013()
        {
            var _staffReviewId01 = dbHelper.staffReview.CreateStaffReview(_systemUserId, _roleid, _staffReviewSetupid, null, 2, null, DateTime.Now.Date.AddDays(-3), null, null, null, 5, _careDirectorQA_TeamId);
            var _staffReviewForm = dbHelper.staffReviewForm.CreateStaffReviewForms(_staffReviewId01, _documentId01, 2, DateTime.Now, _careDirectorQA_TeamId);

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUsername)
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
                .ValidateQuestionTextAreaLabel(documentQuesIdentifier1.ToString(), "Process being observed")
                .ValidateQuestionTextAreaLabel(documentQuesIdentifier2.ToString(), "Location")
                .ValidateQuestionTextAreaLabel(documentQuesIdentifier3.ToString(), "Notes");


            staffReviewFormsRecordPage
                .WaitForStaffReviewFormsRecordPageToLoad();
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

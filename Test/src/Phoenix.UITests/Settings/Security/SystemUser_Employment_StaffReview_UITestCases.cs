using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_Employment_StaffReview_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserID;
        private string _systemUsername;
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
        private Guid _staffReviewIdId;
        private Guid _documenttypeid;
        private Guid _documentsubtypeid;
        private Guid _reviewedBySystemUserId;
        public string EnvironmentName;
        public Guid environmentid;
        private string _loginUser;
        public string _employmentContractName;
        public string _systemUserFullName;
        private Guid _bookingTypeId;
        private Guid _providerid;
        private string _providerName = "Staff Reviews Provider " + DateTime.Now.ToString("yyyyMMddHHmmss");


        [TestInitialize()]
        public void SystemUserStaffReview_Setup()
        {
            try
            {
                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

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

                #region Create default system user

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_3", "CW", "Admin_Test_User_3", "CW Admin Test User 3", "Passw0rd_!", "CW_Admin_Test_User_3@somemail.com", "CW_Admin_Test_User_3@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Edit)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, staffReviewsTeamEditSecurityProfileId);
                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").FirstOrDefault();

                _loginUser = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion  Create default system user

                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("StaffReviewForm_Test001").Any();
                if (!newSystemUser)
                {
                    _systemUserID = dbHelper.systemUser.CreateSystemUser("StaffReviewForm_Test001", "StaffReviewForm", "Test001", "StaffReviewForm_Test001", "Passw0rd_!", "StaffReviewForm_Test001@somemail.com", "StaffReviewForm_Test001@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }
                if (_systemUserID == Guid.Empty)
                {

                    _systemUserID = dbHelper.systemUser.GetSystemUserByUserName("StaffReviewForm_Test001").FirstOrDefault();
                    _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserID, "username")["username"];
                }

                #endregion

                #region Staff Reviewed by 

                var reviewedByExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797_User2").Any();
                if (!reviewedByExists)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.CreateSystemUser("CW_Test_User_CDV6_13797_User2", "CW", "Test_User_CDV6_13797", "CW Test_User_CDV6_13797_User2", "Passw0rd_!", "CW_Test_User_CDV6_13797_User2@somemail.com", "CW_Test_User_CDV6_13797_User2@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);
                }

                if (_reviewedBySystemUserId == Guid.Empty)
                {
                    _reviewedBySystemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Test_User_CDV6_13797_User2").FirstOrDefault();
                }
                #endregion

                #region Care provider staff role type

                var careProviderStaffRoleTypeExists = dbHelper.careProviderStaffRoleType.GetByName("Helper1").Any();
                if (!careProviderStaffRoleTypeExists)
                {
                    _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Helper1", "2", null, new DateTime(2020, 1, 1), null);
                }
                if (_careProviderStaffRoleTypeid == Guid.Empty)
                {
                    _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.GetByName("Helper1").FirstOrDefault();
                }

                #endregion

                #region Booking Type 1 -> "Booking (to location)" & "Count full booking length"

                if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 4").Any())
                    _bookingTypeId = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 4", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, false, false, false, false, false);

                if (_bookingTypeId == Guid.Empty)
                    _bookingTypeId = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 4").First();

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

                var roleid = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserID).Any();
                if (!roleid)
                {
                    _roleid = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                  DateTime.Now, _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid, null);
                }
                if (_roleid == Guid.Empty)
                {
                    _roleid = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserID).FirstOrDefault();
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
                    _documentSectionQuestionId = dbHelper.documentSectionQuestion.GetBySectionIdAndQuestionCatalogueId(_documentSectionId, _questionCatalogueId)[0];
                }

                #endregion

                #region Staff Review Setup Record

                var staffReviewSetupExists = dbHelper.staffReviewSetup.GetActiveRecordsByDocument(_documentId, true, true).Any();
                if (!staffReviewSetupExists)
                {
                    // _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup("", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false);
                    _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup("", _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false, _bookingTypeId);

                }
                if (_staffReviewSetupid == Guid.Empty)
                {
                    _staffReviewSetupid = dbHelper.staffReviewSetup.GetActiveRecordsByDocument(_documentId, true, true).FirstOrDefault();

                }
                #endregion

                #region Staff Review

                foreach (var staffReviewId in dbHelper.staffReview.GetBySystemUserId(_systemUserID))
                    dbHelper.staffReview.DeleteStaffReview(staffReviewId);

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

                #region Authentication Provider

                _providerid = dbHelper.provider.CreateProvider(_providerName, _careDirectorQA_TeamId, 3, true);

                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providerid, _bookingTypeId, true);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13471


        [TestProperty("JiraIssueID", "ACC-3426")]
        [Description("Login CD -> Settings -> Security -> System Users ->  Select any existing user ->  Related Items -> Staff Reviews" +
            "Should display list of existing Staff Reviews if any and a + icon to create new Staff Review record." +
            "Leave all the Mandatory fields as blank and Click Save.Record should not be saved , error should be displayed against all the Mandatory fields .")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_Appoinments_UITestMethod002()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUser, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_Test001")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickSaveAndCloseButton()
                .ValidateTopAreaWarningMessage("Some data is not correct. Please review the data in the Form.");

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
            Assert.AreEqual(0, staffReviews.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3427")]
        [Description("Login CD -> Settings -> Security -> System Users ->  Select any existing user ->  Related Items -> Staff Reviews" +
           "Should display list of existing Staff Reviews if any and a + icon to create new Staff Review record." +
           "Fill all the Mandatory fields and Leave all the Non Mandatory fields as blank and Click Save.Record should be saved successfully with out any error for Non mandatory field.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_Appoinments_UITestMethod003()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUser, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_Test001")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickReviewTypeIdLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Staff Supervision")
                .TapSearchButton()
                .SelectResultElement(_staffReviewSetupid.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .SelectStatusOption("Completed")
                .ClickSaveAndCloseButton();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed");

            System.Threading.Thread.Sleep(3000);

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
            Assert.AreEqual(1, staffReviews.Count);


        }

        [TestProperty("JiraIssueID", "ACC-3428")]
        [Description("Login CD -> Settings -> Security -> System Users ->  Select any existing user ->  Related Items -> Staff Reviews" +
            "Should display list of existing Staff Reviews if any and a + icon to create new Staff Review record." +
            "Verify the fields of Staff Review record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_Appoinments_UITestMethod004()
        {
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_roleid, "name")["name"];
            _systemUserFullName = "StaffReviewForm Test001";

            loginPage
                .GoToLoginPage()
                .Login(_loginUser, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_Test001")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ValidateRegardingUserFieldText(_systemUserFullName)
                .ValidateLocationFieldText("")
                .ValidateDueDateFieldText("")
                .ValidateRoleIdFieldText(_employmentContractName)
                .ValidateReviewTypeFieldText("")
                .ValidateReviewedByFieldText("")
                .ValidateOutcomeTypeFieldText("")
                .ValidateGeneralCommentFieldText("");

        }

        [TestProperty("JiraIssueID", "ACC-3429")]
        [Description("Login CD -> Settings -> Security -> System Users ->  Select any existing user ->  Related Items -> Staff Reviews" +
            "Should display list of existing Staff Reviews if any " +
            "Open any of the record.Record details page should be displayed ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_Appoinments_UITestMethod005()
        {
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUser, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_Test001")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad();

            System.Threading.Thread.Sleep(3000);

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
            Assert.AreEqual(1, staffReviews.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3430")]
        [Description("Login CD -> Settings -> Security -> System Users ->  Select any existing user ->  Related Items -> Staff Reviews" +
          "Should display list of existing Staff Reviews if any " +
          "Open any of the record and modify all the editable fields and click Save.Record should get saved successfully with all the modified details.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_Appoinments_UITestMethod006()
        {
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUser, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_Test001")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .InsertCompletedDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
            Assert.AreEqual(1, staffReviews.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3431")]
        [Description("Login CD -> Settings -> Security -> System Users ->  Select any existing user ->  Related Items -> Staff Reviews" +
         "Should display list of existing Staff Reviews if any " +
         "Select any or all record and click Delete icon.Should display a confirmation pup up to delete the record.Click Yes to confirm the delete action.Record should be deleted ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_Appoinments_UITestMethod007()
        {
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUser, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_Test001")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .SelectStaffReviewRecord(_staffReviewIdId.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
            Assert.AreEqual(0, staffReviews.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3432")]
        [Description("Login CD -> Settings -> Security -> System Users ->  Select any existing user ->  Related Items -> Staff Reviews" +
            "Should display list of existing Staff Reviews if any " +
            "Open any of the recordand click Delete icon.Should display a confirmation pup up to delete the record.Click Yes to confirm the delete action.Record should be deleted ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_Appoinments_UITestMethod008()
        {
            _staffReviewIdId = dbHelper.staffReview.CreateStaffReview(_systemUserID, _roleid, _staffReviewSetupid, _reviewedBySystemUserId, 3, _careDirectorQA_TeamId, DateTime.Now.Date);

            loginPage
                .GoToLoginPage()
                .Login(_loginUser, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("StaffReviewForm_Test001")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .SelectSystemViewsOption("Completed")
                .OpenRecord(_staffReviewIdId.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserID);
            Assert.AreEqual(0, staffReviews.Count);

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15045

        [TestProperty("JiraIssueID", "ACC-3433")]
        [Description("Login CD -> Settings -> Security -> System Users ->  Select any existing user ->  Related Items -> Staff Reviews" +
            "Should display newly added columns in below order along with existing columns with respective values" +
        "Staff Review Form before Review Type and the Start Time, End Time fields to the right of Next Review date.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void VerifyStaffReview_UITestMethod001()
        {
            _systemUserFullName = "StaffReviewForm Test001";

            loginPage
                .GoToLoginPage()
                .Login(_loginUser, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
               .ValidateStaffReviewForm_Header("Review Type");

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

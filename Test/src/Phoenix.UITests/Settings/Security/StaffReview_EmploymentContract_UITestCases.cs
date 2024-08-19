using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_EmploymentContract_UITestCases : FunctionalTest
    {
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
        private Guid _careProviderStaffRoleTypeid01;
        private string EnvironmentName;
        private Guid _questionCatalogueId;
        private string documentName;
        private Guid _documentId;
        private Guid _documentSectionId;
        private Guid _documentSectionQuestionId;
        private Guid _staffReviewSetupid;
        private string _loginUsername;
        private string currentTimeSuffix = DateTime.Now.ToString("ddMMyyyyHHmmss_FFFF");


        [TestInitialize()]
        public void SystemUserSatffReview_Setup()
        {
            try
            {

                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);


                #region Environment Name
                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careDirectorQA_BusinessUnitId, null, "CareDirectorQA@careworkstempmail.com", "Default team for business unit", null);
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

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

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").Any();
                if (!defaultLoginUserExists)
                {
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_3", "CW", "Admin_Test_User_3", "CW Admin Test User 1", "Passw0rd_!", "CW_Admin_Test_User_3@somemail.com", "CW_Admin_Test_User_3@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_defaultLoginUserID, systemUserSecureFieldsSecurityProfileId);

                }

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_3").FirstOrDefault();
                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);


                #endregion  Create default system user

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careDirectorQA_TeamId, _defaultLoginUserID);

                #endregion

                #region Create SystemUser Record

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_16262_EmploymentContract").Any();
                if (!newSystemUser)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser("CDV6_16262_EmploymentContract", "CDV6", "16262_EmploymentContract", "CDV6 16262 EmploymentContract", "Passw0rd_!", "CDV6_16262_EmploymentContract@somemail.com", "CDV6_16262_EmploymentContract@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true);

                    //var staffReviewsTeamEditSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (Org View)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, staffReviewsTeamEditSecurityProfileId);
                }

                if (Guid.Empty == _systemUserId)
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CDV6_16262_EmploymentContract").FirstOrDefault();

                #endregion  Create SystemUser Record

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

                #region Care provider staff role type01

                var careProviderStaffRoleTypeExists01 = dbHelper.careProviderStaffRoleType.GetByName("Ste_RustyDroids_Test").Any();
                if (!careProviderStaffRoleTypeExists01)
                {
                    _careProviderStaffRoleTypeid01 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Ste_RustyDroids_Test", "2", null, new DateTime(2020, 1, 1), null);
                }
                if (_careProviderStaffRoleTypeid01 == Guid.Empty)
                {
                    _careProviderStaffRoleTypeid01 = dbHelper.careProviderStaffRoleType.GetByName("Ste_RustyDroids_Test").FirstOrDefault();
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

                #region create employment contract

                //delete all existining records

                foreach (var employmentContractId in dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId))
                {
                    foreach (var staffReviewId in dbHelper.staffReview.GetByRoleId(employmentContractId))
                        dbHelper.staffReview.DeleteStaffReview(staffReviewId);

                    dbHelper.systemUserEmploymentContract.DeleteSystemUserEmploymentContract(employmentContractId);
                }

                #endregion

                #region Question Catalogue

                _questionCatalogueId = dbHelper.questionCatalogue.CreateNumericQuestion("Strengths" + currentTimeSuffix, "");

                #endregion


                #region Document
                documentName = "Staff Supervision Document" + currentTimeSuffix;
                var documentCategoryId = dbHelper.documentCategory.GetByName("Staff Review Form")[0];
                var documentTypeId = dbHelper.documentType.GetByName("Initial Assessment")[0];

                _documentId = dbHelper.document.CreateDocument(documentName, documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                _documentSectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", _documentId);
                _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
                dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

                #endregion

                #region Staff Review Setup Record

                _staffReviewSetupid = dbHelper.staffReviewSetup.CreateStaffReviewSetup(documentName, _documentId, new DateTime(2021, 1, 1), "for automation" + currentTimeSuffix, true, true, false);
                dbHelper.staffReviewSetup.UpdateAllFields(_staffReviewSetupid, false, new DateTime(2021, 1, 1), null, true, true, null, null, null, false);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13735

        //[TestProperty("JiraIssueID", "CDV6-15720")]
        //[Description("Login CD -> System User -> Select any existing user -> Menu -> Employment -> System User Employment Contracts -> Create a new record " +
        //    "( Give Role and Responsible Team which satisfy at least 1 existing active Staff Review Requirement record , Select Start Date , Fill all other mandatory details) -> Click Save ->" +
        //    "Only System User Employment Contracts should get created not Staff Review record for that user")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //public void SystemUser_EmploymentContract_UITestMethod001()
        //{
        //    Assert.Inconclusive("This test cannot run because it requiures that the Care Provider Staff Reviews Business Module is disabled. We should never enable/disable modules via code");

        //    loginPage
        //         .GoToLoginPage()
        //         .Login("CW_Admin_Test_User_3", "Passw0rd_!", "Care Providers")
        //         .WaitFormHomePageToLoad(true, false, true);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToSystemUserSection();

        //    systemUsersPage
        //        .WaitForSystemUsersPageToLoad()
        //        .InsertUserName("CDV6_16262_EmploymentContract")
        //        .ClickSearchButton()
        //        .WaitForResultsGridToLoad()
        //        .OpenRecord(_systemUserId.ToString());

        //    systemUserRecordPage
        //        .WaitForSystemUserRecordPageToLoad()
        //        .NavigateToEmploymentContractsSubPage();

        //    systemUserEmploymentContractsPage
        //        .WaitForSystemUserEmploymentContractsPageToLoad()
        //        .ClickAddNewButton();

        //    systemUserEmploymentContractsRecordPage
        //        .WaitForSystemUserEmploymentContractsRecordPageToLoad()
        //        .ClickRoleLoopUpButton();

        //    lookupPopup
        //        .WaitForLookupPopupToLoad()
        //        .TypeSearchQuery("Helper")
        //        .TapSearchButton()
        //        .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

        //    systemUserEmploymentContractsRecordPage
        //       .WaitForSystemUserEmploymentContractsRecordPageToLoad()
        //       .ClickTypeLookUpButton();

        //    lookupPopup
        //        .WaitForLookupPopupToLoad()
        //        .TypeSearchQuery("Full Time Employee Contract")
        //        .TapSearchButton()
        //        .SelectResultElement(_employmentContractTypeid.ToString());

        //    systemUserEmploymentContractsRecordPage
        //       .WaitForSystemUserEmploymentContractsRecordPageToLoad()
        //       .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:05")
        //       .ClickSaveAndCloseButton()
        //       .WaitForStaffReviewRequrimentsLookUP()
        //       .StaffReviewRequrimentSelection("Staff Supervision 01")
        //       .ClickSavePopUpButton()
        //       .ClickOkButton();

        //    systemUserEmploymentContractsPage
        //       .WaitForSystemUserEmploymentContractsPageToLoad();

        //    var employmentContractRecords = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
        //    Assert.AreEqual(1, employmentContractRecords.Count);

        //    var staffReviews = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
        //    Assert.AreEqual(0, staffReviews.Count);

        //}

        [TestProperty("JiraIssueID", "ACC-3342")]
        [Description("Login CD -> System User -> Select any existing user -> Menu -> Employment -> System User Employment Contracts -> Create a new record ( Give Role and Responsible Team which " +
            "satisfy at least 1 existing active Staff Review Requirement record, Select Start Date , Fill all other mandatory details ) -> Click Save ->Should not display any pop up for requirement selection " +
            "Should not display any pop up for requirement selection -> Only System User Employment Contracts should get created not Staff Review record for that user")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SystemUser_EmploymentContract_UITestMethod002()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CDV6_16262_EmploymentContract")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickRoleLoopUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Helper")
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Full Time Employee Contract")
                .TapSearchButton()
                .SelectResultElement(_employmentContractTypeid.ToString());

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:05")
               .ClickResponsibleTeamLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareProviders").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertDescription("Automation Test Employee Contract")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .InsertFixedWorkingPatternCycle("1")
                .ClickSaveAndCloseButton()
                .WaitForStaffReviewRequrimentsLookUP();

            systemUserEmploymentContractsRecordPage
                .StaffReviewRequrimentSelection(documentName)
                .ClickSavePopUpButton()
                .ClickOkButton();

            systemUserEmploymentContractsPage
               .WaitForSystemUserEmploymentContractsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var employmentContractRecords = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, employmentContractRecords.Count);

        }

        [TestProperty("JiraIssueID", "ACC-3343")]
        [Description("Login CD -> System User -> Select any existing user -> Menu -> Employment -> System User Employment Contracts -> Create a new record " +
        "( Give Role and Responsible Team which satisfy only the existing inactive Staff Review Requirement record , Select Start Date , Fill all other mandatory details) -> Click Save -> " +
            "Only System User Employment Contracts should get created not Staff Review record for that user")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SystemUser_EmploymentContract_UITestMethod003()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CDV6_16262_EmploymentContract")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickRoleLoopUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Ste_RustyDroids_Test")
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid01.ToString());

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Full Time Employee Contract")
                .TapSearchButton()
                .SelectResultElement(_employmentContractTypeid.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:05")
                .ClickResponsibleTeamLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareProviders").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertDescription("Automation Test Employee Contract")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton()
                .WaitForStaffReviewRequrimentsLookUP()
                .ClickSavePopUpButton()
                .ClickOkButton();

            systemUserEmploymentContractsPage
               .WaitForSystemUserEmploymentContractsPageToLoad();


            System.Threading.Thread.Sleep(3000);

            var employmentContractRecords = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, employmentContractRecords.Count);

        }

        //[TestProperty("JiraIssueID", "CDV6-15724")]
        //[Description("Login CD with Manager credentials -> My Work -> My Staff Reviews -> Open any record created automatically through Contract creation -> Verify the regarding field" +
        //    "Should display the user for whom the contact was created -> Verify the Role -> Should display the Role which was selected during contract creation -> Verify the Form and Review Type" +
        //    "Should display the Form of the Staff Review Requirement record which satisfied the BU and Role of the contract -> Verify the Due Date -> Due Date = Contract Start Date + First Occurrence Frequency")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //public void SystemUser_EmploymentContract_UITestMethod004()
        //{
        //    _roleid = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid);

        //    loginPage
        //        .GoToLoginPage()
        //        .Login("CW_Admin_Test_User_3", "Passw0rd_!", "Care Providers")
        //        .WaitFormHomePageToLoad(true, false, true);

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToSystemUserSection();

        //    systemUsersPage
        //        .WaitForSystemUsersPageToLoad()
        //        .InsertUserName("CDV6_16262_EmploymentContract")
        //        .ClickSearchButton()
        //        .WaitForResultsGridToLoad()
        //        .OpenRecord(_systemUserId.ToString());

        //    systemUserRecordPage
        //        .WaitForSystemUserRecordPageToLoad()
        //        .NavigateToStaffReviewSubPage();

        //    var employmentContractRecords = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
        //    Assert.AreEqual(1, employmentContractRecords.Count);

        //    var staffReviewRecords = dbHelper.staffReview.GetBySystemUserId(_systemUserId);

        //    var staffReviewFields = dbHelper.staffReview.GetStaffReviewByID(staffReviewRecords[0], "regardinguserid", "roleid", "reviewtypeid", "duedate");
        //    Assert.AreEqual(_systemUserId, staffReviewFields["regardinguserid"]);
        //    //Assert.AreEqual(_careProviderStaffRoleTypeid, staffReviewFields["roleid"]);
        //    //Assert.AreEqual(_employmentContractTypeid, staffReviewFields["reviewtypeid"]);
        //    Assert.AreEqual(DateTime.Now.Date.AddMonths(6), staffReviewFields["duedate"]);

        //}


        [TestProperty("JiraIssueID", "ACC-3344")]
        [Description("Login CD -> System User -> Select any existing user -> Menu -> Employment -> System User Employment Contracts -> Create a new record " +
            "( Give Role and Responsible Team which satisfy at least 1 existing active Staff Review Requirement record  and Don't fill Start Date ,Fill all other mandatory details) -> Click Save" +
            "Only System User Employment Contracts should be created not Staff Review record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SystemUser_EmploymentContract_UITestMethod005()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CDV6_16262_EmploymentContract")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickRoleLoopUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Helper")
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Full Time Employee Contract")
                .TapSearchButton()
                .SelectResultElement(_employmentContractTypeid.ToString());

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ClickResponsibleTeamLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareProviders").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertDescription("Automation Test Employee Contract")
                .InsertStartDate("", "")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
               .WaitForSystemUserEmploymentContractsPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var employmentContractRecords = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, employmentContractRecords.Count);

            var staffReviewRecords = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(0, staffReviewRecords.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3345")]
        [Description("Login CD -> System User -> Select any existing user -> Menu -> Employment -> System User Employment Contracts -> Create a new record " +
            "( Give Role and Responsible Team which doesn't satisfy any of the existing active Staff Review Requirement record, Select Start Date , Fill all other mandatory details ) -> Click Save" +
            "System User Employment Contracts should be created successfully but not Staff Review record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SystemUser_EmploymentContract_UITestMethod006()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("CDV6_16262_EmploymentContract")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickRoleLoopUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Helper")
                .TapSearchButton()
                .SelectResultElement(_careProviderStaffRoleTypeid.ToString());

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Full Time Employee Contract")
                .TapSearchButton()
                .SelectResultElement(_employmentContractTypeid.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "00:05")
                .ClickResponsibleTeamLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CareProviders").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertDescription("Automation Test Employee Contract")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton()
                .WaitForStaffReviewRequrimentsLookUP()
                .ClickSavePopUpButton()
                .ClickOkButton();

            systemUserEmploymentContractsPage
               .WaitForSystemUserEmploymentContractsPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var employmentContractRecords = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(1, employmentContractRecords.Count);

            var staffReviewRecords = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            Assert.AreEqual(0, staffReviewRecords.Count);
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

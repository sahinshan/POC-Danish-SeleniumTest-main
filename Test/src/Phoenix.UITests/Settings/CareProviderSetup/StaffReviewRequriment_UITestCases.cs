using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class StaffReviewRequriment_UITestCases : FunctionalTest
    {
        private string EnvironmentName;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _languageId;
        private Guid _systemUserId;
        private Guid _authenticationproviderid;
        private Guid _demographicsTitleId;
        private Guid _maritalStatusId;
        private Guid _ethnicityId;
        private Guid _transportTypeId;
        private Guid _careProviderStaffRoleTypeid;
        private string careProviderStaffRoleTypeName;
        private Guid _employmentContractTypeid;
        private Guid _roleid;
        private Guid _questionCatalogueId;
        private Guid _documentId;
        private Guid _documentId2;
        private Guid _documentId3;
        private string _documentName = "Staff Supervision 02 Automation_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _documentName2 = "Staff Appraisal Automation_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _documentName3 = "Staff Review Automation_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _username = "CW_Admin_Test_User_2_" + DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _documentSectionId;
        private Guid _documentSectionQuestionId;
        private Guid _staffReviewSetupid;
        private Guid _bookingTypeId;
        private string _bookingTypeName;
        private Guid documentCategoryId;
        private Guid documentTypeId;



        [TestInitialize()]
        public void StaffReviewRequriment_SetupMethod()
        {
            try
            {
                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careDirectorQA_BusinessUnitId, null, "CareDirectorQA@careworkstempmail.com", "Default team for business unit", null);

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Marital Status

                _maritalStatusId = commonMethodsDB.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion

                #region Title

                _demographicsTitleId = commonMethodsDB.CreateDemographicsTitle("Dr", DateTime.Now, _careDirectorQA_TeamId);

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Asian or Asian British - Indian", DateTime.Now);

                #endregion Ethnicity

                #region TransportType

                _transportTypeId = commonMethodsDB.CreateTransportType(_careDirectorQA_TeamId, "TransportTest", DateTime.Now, 1, "50", 5);

                #endregion

                #region Create SystemUser Record

                _systemUserId = dbHelper.systemUser.CreateSystemUser(_username, "CW", "Admin_Test_User_2", _username, "Passw0rd_!", "CW_Admin_Test_User_2@somemail.com", "CW_Admin_Test_User_2@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careDirectorQA_TeamId, _systemUserId);

                #endregion

                #region care provider staff role type

                careProviderStaffRoleTypeName = "Helper Automation_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, careProviderStaffRoleTypeName, "2", null, new DateTime(2020, 1, 1), null);


                #endregion

                #region Employment Contract Type

                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region system User Employment Contract

                _roleid = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid);

                #endregion

                #region Question Catalogue

                _questionCatalogueId = commonMethodsDB.CreateNumericQuestion("Strengths", "");

                #endregion

                #region Document

                documentCategoryId = dbHelper.documentCategory.GetByName("Staff Review Form")[0];
                documentTypeId = dbHelper.documentType.GetByName("Initial Assessment")[0];


                _documentId = dbHelper.document.CreateDocument(_documentName, documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                _documentSectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", _documentId);
                _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
                dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published

                #endregion

                #region Document - Staff Appraisal

                _documentId2 = dbHelper.document.CreateDocument(_documentName2, documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                _documentSectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", _documentId2);
                _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
                dbHelper.document.UpdateStatus(_documentId2, 100000000); //Set the status to published

                #endregion

                #region Document - Staff Review Record 3

                _documentId3 = dbHelper.document.CreateDocument(_documentName3, documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
                _documentSectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", _documentId3);
                _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
                dbHelper.document.UpdateStatus(_documentId3, 100000000); //Set the status to published

                #endregion

                #region Booking Type

                _bookingTypeName = "CDV6_Automation_" + DateTime.Now.ToString("yyyyMMddHHmmss_FFFFFF");
                _bookingTypeId = dbHelper.cpBookingType.CreateBookingType(_bookingTypeName, 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, true, false, false, false, true, false);

                #endregion

                #region Staff Review Setup Record

                _staffReviewSetupid = commonMethodsDB.CreateStaffReviewSetup(_documentName, _documentId, new DateTime(2021, 1, 1), "for automation", true, true, false, _bookingTypeId);
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

        #region https://advancedcsg.atlassian.net/browse/CDV6-15178

        [TestProperty("JiraIssueID", "ACC-3537")]
        [Description("Login CD -> Settings -> Care Provider setup -> Staff review requirements -> Open any exiting record and give give Valid To Date" +
        "( Make sure not trying to create any duplicate matching with existing Staff Review Requirement ) and click Save -> Should not throw any prompt for Staff Review Creation")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Review Requirements")]
        public void StaffReviewRequriment_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewRequirementsSubLink();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad()
                .ClickHeaderCell(10)
                .WaitForStaffReviewRequirementsPageToLoad()
                .ClickHeaderCell(10)
                .WaitForStaffReviewRequirementsPageToLoad()
                .OpenRecord(_staffReviewSetupid.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .InsertValidTo(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveAndCloseButton();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad();
        }

        [TestProperty("JiraIssueID", "ACC-3538")]
        [Description("Login CD -> Settings -> Care Provider setup -> Staff review requirements -> Open any exiting record and modify any field except Valid To Date" +
            "( Make sure not trying to create any duplicate matching with existing Staff Review Requirement ) and click Save User to be prompted Review requirement updated -" +
            " would you like to create review entries for all affected active staff members, where they do not currently exist? Option for Yes, No, Cancel ->Click Yes ->" +
            "Should Create a new review for all staff within the scope of the Business unit and role configuration that have a one or more current active contracts)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Review Requirements")]

        public void StaffReviewRequriment_UITestMethod02()
        {

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewRequirementsSubLink();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad()
                .InsertQuickSearchText(_documentName)
                .ClickQuickSearchButton()
                .OpenRecord(_staffReviewSetupid.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .InsertDescriptionForm("For Testing")
                .ClickSaveAndCloseButton();

            staffReviewRequirementsPage
            .WaitForStaffReviewRequirementsPageToLoad();

        }

        [TestProperty("JiraIssueID", "ACC-3539")]
        [Description("Login CD -> Settings -> Care Provider setup -> Staff review requirements -> Click + icon -> Fill all mandatory details ( Make sure not trying to create any duplicate matching with existing Staff Review Requirement ) and click Save" +
            "Should Create a new review for all staff within the scope of the Business unit and role configuration that have a one or more current active contracts. ( Means , Whomever the active system user has the Role , Role's Responsible Team as per " +
            "the Role and BU selected in the 1st step new Staff review record should be created only when the Staff Review record of Form Type and Role not exist)")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Review Requirements")]

        public void StaffReviewRequriment_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewRequirementsSubLink();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad()
                .ClickNewRecordButton();

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickStaffReviewFormIdLookUP();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName3)
                .TapSearchButton()
                .SelectResultElement(_documentId3.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickBookingTypeLookup();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_bookingTypeName)
                .TapSearchButton()
                .SelectResultElement(_bookingTypeId.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickAllRolesRadioButton()
                .InsertDescriptionForm("testing")
                .InsertValidateForm(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidatStaffReviewRequirementsRecordPageTitle(_documentName3)
                .ClickSaveAndCloseButton();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad();


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14582

        [TestProperty("JiraIssueID", "ACC-3540")]
        [Description("Login Care Provider -> Settings -> Care Provider Setup -> Verify the menu name for Staff Review Setup -> Should display the name as Staff Review Requirements not as Staff Review Setup" +
            "Open Staff Review Requirements -> Should display the title of the page as Staff Review Requirements not as Staff Review Setup")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Review Requirements")]
        public void StaffReviewRequriment_UITestMethod04()
        {
            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .ValidateToStaffReviewRequirementsSubLink("Staff Review Requirements")
                .NavigateToStaffReviewRequirementsSubLink();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad()
                .ValidateStaffReviewRequrimentDisplayed("Staff Review Requirements");
        }

        [TestProperty("JiraIssueID", "ACC-3541")]
        [Description("Login Care Provider -> Settings -> Care Provider Setup -> Open Staff Review Requirements -> Create new record -> Along with existing fields should display " +
            "new section called Validity with below fieldsValid From(Mandatory, editable, pre filled with today's date )Valid To - Non mandatory -> Fill all values and Save the record" +
            " A record should be created successfully")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Review Requirements")]
        public void StaffReviewRequriment_UITestMethod05()
        {
            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewRequirementsSubLink();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad()
                .ClickNewRecordButton();

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .Validate_ValidateFormEditable()
                .Validate_ValidateFormFieldMandatory(true)
                .Validate_ValidToFieldNonMandatory(false)
                .ClickStaffReviewFormIdLookUP();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickAllRolesRadioButton()
                .InsertDescriptionForm("testing")
                .ClickSaveAndCloseButton();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad();

        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-14295

        [TestProperty("JiraIssueID", "ACC-3542")]
        [Description("Login CD Care Provider -> Settings -> Security -> System Users -> Select any existing user -> Menu -> Employment -> My Staff Reviews -> Click + icon to add new record" +
            "Should not display field Name -> Fill all the mandatory details and click Save -> Should save the record successfully without any error to fill name.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReviewRequriment_UITestMethod06()
        {

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_username)
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
                .ValidateNameFieldNotPresent()
                .ClickReviewTypeIdLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_staffReviewSetupid.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .SelectStatusOption("Outstanding")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidatStaffReviewRecordPageTitle(_documentName);
        }

        //[TestProperty("JiraIssueID", "ACC-3543")]
        //[Description("Login CD Care Provider -> Settings -> Care Provider Setup->Should display a menu Staff Review Setup not Staff Review Setups")]
        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        //[TestProperty("Screen1", "Staff Review Requirements")]
        //public void StaffReviewRequriment_UITestMethod07()
        //{
        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_username, "Passw0rd_!", "Care Providers");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .ValidateToStaffReviewRequirementsSubLink("Staff Review Requirements");
        //}

        [TestProperty("JiraIssueID", "ACC-3544")]
        [Description("Login CD Care Provider -> Settings -> Security -> System Users -> Select any existing user -> Menu -> Employment -> Check the menu name of Staff Review" +
            "Should not display field Name")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReview_UITestMethod03()
        {

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_username)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ClickSearchButton()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateToStaffReviewSubPage("My Staff Reviews");
        }

        [TestProperty("JiraIssueID", "ACC-3545")]
        [Description("Login CD Care Provider -> Settings -> Security -> System Users -> Select any existing user -> Menu -> Employment -> My Staff Reviews -> Check the column headers" +
            "Should not display column Name")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReviewRequriment_UITestMethod08()
        {

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_username)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ClickSearchButton()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .ValidateShouldnotdisplaycolumnName();
        }

        [TestProperty("JiraIssueID", "ACC-3546")]
        [Description("Login CD Care Provider -> Settings -> Care Provider Setup -> Staff Review Setup -> Check the column headers" +
            "Should not display column Name")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Review Requirements")]
        public void StaffReviewRequriment_UITestMethod09()
        {
            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewRequirementsSubLink();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad()
                .ValidateShouldnotdisplaycolumnName();
        }

        [TestProperty("JiraIssueID", "ACC-3547")]
        [Description("Login CD Care Provider -> Settings -> Security -> Care Provider Setup -> Staff Review Setup -> Click + icon to add new record" +
            "Should not display field Name-> Fill all the mandatory details and click Save -> Should save the record successfully without any error to fill name")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Review Requirements")]
        public void StaffReviewRequriment_UITestMethod010()
        {
            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewRequirementsSubLink();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad()
                .ClickNewRecordButton();

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ValidateNameFieldNotPresent()
                .ClickStaffReviewFormIdLookUP();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_documentId.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickRolesLookUP();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(careProviderStaffRoleTypeName)
                .TapSearchButton()
                .ClickAddSelectedButton(_careProviderStaffRoleTypeid.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .InsertValidateForm(DateTime.Now.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDescriptionForm("Test")
                .ClickBookingTypeLookup();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_bookingTypeName)
                .TapSearchButton()
                .SelectResultElement(_bookingTypeId.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateBookingTypeFieldLinkText(_bookingTypeName)
                .ValidatStaffReviewRequirementsRecordPageTitle(_documentName);
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15683
        [TestProperty("JiraIssueID", "ACC-3548")]
        [Description("Login CD -> Settings -> Care Provider setup -> Staff review requirements -> Click + icon -> Fill all mandatory details ( Make sure not trying to create any duplicate matching with existing Staff Review Requirement ) and click Save" +
            "Should not create Staff Review immediately instead , once the Job schedule run after 15 minutes system should create a new review for the user who has the role as per the requirement record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Review Requirements")]
        public void StaffReviewRequrimentCreation_UITestMethod01()
        {

            #region Document - Staff Review Record 4
            string _documentNameScheduledJob = "Scheduled Job Automation_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Guid _documentId4 = dbHelper.document.CreateDocument(_documentNameScheduledJob, documentCategoryId, documentTypeId, _careDirectorQA_TeamId, 1);
            _documentSectionId = dbHelper.documentSection.CreateDocumentSection("Section 1", _documentId4);
            _documentSectionQuestionId = dbHelper.documentSectionQuestion.CreateDocumentSectionQuestion(_questionCatalogueId, _documentSectionId);
            dbHelper.document.UpdateStatus(_documentId4, 100000000); //Set the status to published

            #endregion

            var scheduleJobId = new Guid("29cfe828-9290-ec11-a350-0050569231cf"); //Auto Creation Staff Reviews from Staff Review Requirements

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewRequirementsSubLink();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad()
                .ClickNewRecordButton();

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickStaffReviewFormIdLookUP();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_documentNameScheduledJob).TapSearchButton().SelectResultElement(_documentId4.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .InsertDescriptionForm("Description")
                .ClickRolesLookUP();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(careProviderStaffRoleTypeName).TapSearchButton().ClickAddSelectedButton(_careProviderStaffRoleTypeid.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickBookingTypeLookup();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_bookingTypeName).TapSearchButton().SelectResultElement(_bookingTypeId.ToString());

            staffReviewRequirementsRecordPage
                .WaitForStaffReviewRequirementsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            staffReviewRequirementsPage
                .WaitForStaffReviewRequirementsPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickScheduledJobsLink();

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad()
                .SearchRecord("Auto Creation Staff Reviews from Staff Review Requirements")
                .OpenRecord(scheduleJobId.ToString());

            scheduleJobsRecordPage
                .WaitForScheduleJobsRecordPageToLoad()
                .TapExecuteJobButton()
                .TapSaveAndCloseButton();

            scheduleJobsPage
                .WaitForScheduleJobsPageToLoad();

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            dbHelper = new DBHelper.DatabaseHelper();
            //Wait for the Idle status
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_username)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToStaffReviewSubPage();

            systemUserStaffReviewPage
                .WaitForSystemUserStaffReviewPageToLoad()
                .ClickSearchButton();

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Auto Creation Staff Reviews from Staff Review Requirements" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            var staffReviewRecords = dbHelper.staffReview.GetBySystemUserId(_systemUserId);
            List<string> StaffReviewsNames = new List<string>();
            foreach (var staffReviewRecordId in staffReviewRecords)
                StaffReviewsNames.Add((string)dbHelper.staffReview.GetStaffReviewByID(staffReviewRecordId, "name")["name"]);

            Assert.IsTrue(StaffReviewsNames.Contains(_documentNameScheduledJob));
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-24255

        [TestProperty("JiraIssueID", "ACC-3550")]
        [Description("Login CD Care Provider -> Verify the Staff Review duplicate validation")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Reviews")]
        [TestProperty("Screen1", "Staff Reviews")]
        public void StaffReviewRequriment_UITestMethod11()
        {

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            var testUser = commonMethodsDB.CreateSystemUserRecord("TestUser24255", "Test", "User24255", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            Guid testUserEmploymentContract = Guid.Empty;

            var testUserEmploymentContractExists = dbHelper.systemUserEmploymentContract.GetBySystemUserId(testUser).Any();
            if (!testUserEmploymentContractExists)
            {
                testUserEmploymentContract = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(testUser, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid, null, "Description");
            }
            if (testUserEmploymentContract == Guid.Empty)
            {
                testUserEmploymentContract = dbHelper.systemUserEmploymentContract.GetBySystemUserId(testUser).FirstOrDefault();
            }

            dbHelper.staffReview.CreateStaffReview(testUser, testUserEmploymentContract, _staffReviewSetupid, _systemUserId, 1, _careDirectorQA_TeamId);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("TestUser24255")
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(testUser.ToString());

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
                .TypeSearchQuery(_documentName)
                .TapSearchButton()
                .SelectResultElement(_staffReviewSetupid.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickRoleLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(testUserEmploymentContract.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickReviewedByIdLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_username)
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .SelectStatusOption("Outstanding")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Staff Reviews already exist for this contract so will not be created")
                .TapCloseButton();
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

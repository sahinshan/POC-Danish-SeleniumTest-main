using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;


namespace Phoenix.UITests.Settings.Configuration
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class ReferenceData_UITestCases : FunctionalTest
    {
        #region properties

        private Guid _systemUserID;
        private Guid _languageId;
        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private string EnvironmentName;
        private Guid _reportableEventInjurySeverity_Recordwithenddate;
        private Guid _reportableEventRole_Recordwithenddate;
        private Guid _reportableEventSubCategory_Recordwithenddate;
        private Guid _reportableEventCategory_Id;
        String ReportableEventSubCategory = "CDV6-16024" + DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _reportableEventRole_Record1;
        private Guid _reportableEventRole_Record2;
        #endregion

        [TestInitialize()]
        public void TestMethod_Setup()
        {
            #region Connection to database

            string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
            dbHelper = new DBHelper.DatabaseHelper(tenantName);

            #endregion

            #region Environment Name
            EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];

            #endregion

            #region Business Unit
            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];


            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careProviders_BusinessUnitId, "90400", "CareDirectorQA@careworkstempmail.com", "CareProviders", "020 123456");
            _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Create SystemUser Record

            var currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var username = "Test_User_CDV6_17291" + currentDateTime;
            _systemUserID = dbHelper.systemUser.CreateSystemUser(username, "CW", "Test_User_CDV6_17291", "CW Test_User_CDV6_17291", "Passw0rd_!", "CW_Test_User_CDV6_16296@somemail.com", "CW_Test_User_CDV6_17291@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

            #endregion  Create SystemUser Record

            #region reportable event category

            var CategoryExist = dbHelper.careProviderReportableEventCategory.GetByName("Injuries").Any();
            if (!CategoryExist)
                dbHelper.careProviderReportableEventCategory.CreateCareProviderReportableEventCategoryRecord("Injuries", DateTime.Now, _careProviders_TeamId);
            _reportableEventCategory_Id = dbHelper.careProviderReportableEventCategory.GetByName("Injuries")[0];

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8545

        [TestProperty("JiraIssueID", "CDV6-25011")]
        [Description("Navigate to the Reference Data page - validate that the page displays all elements collapsed")]
        [TestMethod, TestCategory("UITest")]
        public void SearchingReferenceDataScreen_UITestMethod001()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .ValidateReferenceDataMainHeaderVisibility("Activity", true)
                .ValidateReferenceDataElementVisibility("Activity Categories", false)
                .ValidateReferenceDataElementVisibility("Activity Outcome Types", false)

                .ValidateReferenceDataMainHeaderVisibility("Address", true)
                .ValidateReferenceDataElementVisibility("Address Borough", false)
                .ValidateReferenceDataElementVisibility("Address Property Types", false);
        }

        [TestProperty("JiraIssueID", "CDV6-25012")]
        [Description("Navigate to the Reference Data page - Click on the Activity main Header - " +
            "Validate that the Activity reference data elements get visible")]
        [TestMethod, TestCategory("UITest")]
        public void SearchingReferenceDataScreen_UITestMethod002()
        {


            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()

                .ClickReferenceDataMainHeader("Activity")

                .ValidateReferenceDataMainHeaderVisibility("Activity", true)
                .ValidateReferenceDataElementVisibility("Activity Categories", true)
                .ValidateReferenceDataElementVisibility("Activity Outcome Types", true)
                .ValidateReferenceDataElementVisibility("Appointment Types", true)

                .ValidateReferenceDataMainHeaderVisibility("Address", true)
                .ValidateReferenceDataElementVisibility("Address Borough", false)
                .ValidateReferenceDataElementVisibility("Address Property Types", false);
        }

        [TestProperty("JiraIssueID", "CDV6-25013")]
        [Description("Navigate to the Reference Data page - Perform a quick search using the 'Activity' query - " +
            "Validate that only the reference data elements with 'Activity' in the name are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void SearchingReferenceDataScreen_UITestMethod003()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()

                .InsertSearchQuery("Activity")
                .TapSearchButton()

                .ValidateReferenceDataMainHeaderVisibility("Activity", true)
                .ClickReferenceDataMainHeader("Activity")
                .ValidateReferenceDataElementVisibility("Activity Categories", true)
                .ValidateReferenceDataElementVisibility("Activity Outcome Types", true)
                .ValidateReferenceDataElementVisibility("Appointment Types", false) //this element do not match the query

                .ValidateReferenceDataMainHeaderVisibility("Address", false)
                .ValidateReferenceDataElementVisibility("Address Borough", false)
                .ValidateReferenceDataElementVisibility("Address Property Types", false)

                .ValidateReferenceDataMainHeaderVisibility("Adult Safeguarding", false)
                .ValidateReferenceDataElementVisibility("Adult Safeguarding Background Factor Flags", false)
                .ValidateReferenceDataElementVisibility("Adult Safeguarding Categories of Abuse", false)

                .ValidateReferenceDataMainHeaderVisibility("SALT", true)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-25014")]
        [Description("Navigate to the Reference Data page - Perform a quick search using the 'Appointment Types' query - " +
            "Validate that only the reference data elements with 'Appointment Types' in the name are displayed")]
        [TestMethod, TestCategory("UITest")]
        public void SearchingReferenceDataScreen_UITestMethod004()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()

                .InsertSearchQuery("Appointment Types")
                .TapSearchButton()

                .ValidateReferenceDataMainHeaderVisibility("Activity", true)
                .ClickReferenceDataMainHeader("Activity")
                .ValidateReferenceDataElementVisibility("Activity Categories", false)
                .ValidateReferenceDataElementVisibility("Activity Outcome Types", false)
                .ValidateReferenceDataElementVisibility("Appointment Types", true)

                .ValidateReferenceDataMainHeaderVisibility("Address", false)
                .ValidateReferenceDataElementVisibility("Address Borough", false)
                .ValidateReferenceDataElementVisibility("Address Property Types", false)

                .ValidateReferenceDataMainHeaderVisibility("Adult Safeguarding", false)
                .ValidateReferenceDataElementVisibility("Adult Safeguarding Background Factor Flags", false)
                .ValidateReferenceDataElementVisibility("Adult Safeguarding Categories of Abuse", false)
                ;
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-10369

        [TestProperty("JiraIssueID", "CDV6-25015")]
        [Description("Navigate to the Reference Data page - Perform a quick search using the 'Address Property Types' query - " +
            "Click on the Address main header - Click on the Address Property Types reference data element - Wait for the Address Property Types page to load - " +
            "Select the Inactive records view - Validate that inactive records are displayed in this view")]
        [TestMethod, TestCategory("UITest")]
        public void ViewAddressPropertyTypesWithEndDates_UITestMethod001()
        {
            var inactiveRecord = new Guid("b85ec313-18c2-eb11-a323-005056926fe4"); //CDV6-10369 Scenario 3

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Address Property Types")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Address")
                .ClickReferenceDataElement("Address Property Types");

            addressPropertyTypesPage
                .WaitForAddressPropertyTypesPageToLoad()
                .SelectView("Inactive Records")
                .ValidateRecordVisible(inactiveRecord.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-25016")]
        [Description("Navigate to the Reference Data page - Perform a quick search using the 'Address Property Types' query - " +
            "Click on the Address main header - Click on the Address Property Types reference data element - Wait for the Address Property Types page to load - " +
            "Select the Inactive records view - Validate that records with future start dates are displayed in this view")]
        [TestMethod, TestCategory("UITest")]
        public void ViewAddressPropertyTypesWithEndDates_UITestMethod002()
        {
            var inactiveRecord = new Guid("b36670e8-17c2-eb11-a323-005056926fe4"); //CDV6-10369 Scenario 1

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Address Property Types")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Address")
                .ClickReferenceDataElement("Address Property Types");

            addressPropertyTypesPage
                .WaitForAddressPropertyTypesPageToLoad()
                .SelectView("Inactive Records")
                .ValidateRecordVisible(inactiveRecord.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-25017")]
        [Description("Navigate to the Reference Data page - Perform a quick search using the 'Address Property Types' query - " +
            "Click on the Address main header - Click on the Address Property Types reference data element - Wait for the Address Property Types page to load - " +
            "Select the Inactive records view - Validate that records with past end dates are displayed in this view")]
        [TestMethod, TestCategory("UITest")]
        public void ViewAddressPropertyTypesWithEndDates_UITestMethod003()
        {
            var inactiveRecord = new Guid("127c4107-18c2-eb11-a323-005056926fe4"); //CDV6-10369 Scenario 2

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Address Property Types")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Address")
                .ClickReferenceDataElement("Address Property Types");

            addressPropertyTypesPage
                .WaitForAddressPropertyTypesPageToLoad()
                .SelectView("Inactive Records")
                .ValidateRecordVisible(inactiveRecord.ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15877

        [TestProperty("JiraIssueID", "CDV6-16060")]
        [Description("Verify the new record creation of Reference Data Reportable Event Injury Severities in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyNewRecordCreation_UITestMethod001()
        {
            var reportableEventInjurySeverityId = dbHelper.careproviderReportableEventInjurySeverity.GetIdByName("NameCDV6-16060").FirstOrDefault();
            if (Guid.Empty != reportableEventInjurySeverityId)
                dbHelper.careproviderReportableEventInjurySeverity.DeleteCareproviderReportableEventInjurySeverity(reportableEventInjurySeverityId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
               .WaitForReferenceDataPageToLoad()
               .InsertSearchQuery("Reportable Event Injury Severities")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Quality and Compliance")
                .ClickReferenceDataElement("Reportable Event Injury Severities");

            reportableEventInjuritySeveritiesPage
                .WaitForReportableEventInjurySeverityPageToLoad()
                .ClickCreateNewRecord();

            reportableEventInjuritySeveritiesRecordPage
                .WaitForReportableEventInjurySeverityRecordPageToLoad("New")
                .ValidateReportableEventInjurieSeverityNameField(true)
                .ValidateReportableEventInjurieSeverityCodeField(true)
                .ValidateReportableEventInjurieSeverityGovCodeField(true)
                .ValidateReportableEventInjurieSeverityInactiveRadioBtnField(false)
                .ValidateStartDateFieldText(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .ValidateReportableEventInjurieSeverityEndDateField(true)
                .ValidateReportableEventInjurieSeverityValidForExportRadioBtnField(false)
                .ValidateReportableEventInjurieSeverityResponsibleTeamField(true)
                .ValidateReportableEventInjurieSeverityNotesTextField(true)
                .InsertReportableEventInjurieSeverityNameTextField("NameCDV6-16060")
                .InsertReportableEventInjurieSeverityCodeTextField("12345678")
                .InsertReportableEventInjurieSeverityGovCodeTextField("Govcode-001")
                .InsertReportableEventInjurieSeverityNotesTextField("Notes")
                .ClickSaveAndCloseButton();

            var reportableEventInjurySeverityExists = dbHelper.careproviderReportableEventInjurySeverity.GetIdByName("NameCDV6-16060").Any();

            reportableEventInjuritySeveritiesPage
                  .WaitForReportableEventInjurySeverityPageToLoad()
                  .SelectView("Active Records")
                  .SearchReportableEventInjurySeverityRecord("NameCDV6-16060");

            Assert.AreEqual(true, reportableEventInjurySeverityExists);

        }



        [TestProperty("JiraIssueID", "CDV6-16061")]
        [Description("Verify the default views columns of Reference Data Reportable Event Injury Severities  in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyColumnHeaderRefData_UITestMethod002()
        {
            var reportableEventInjurySeverityId = dbHelper.careproviderReportableEventInjurySeverity.GetIdByName("abc-001").FirstOrDefault();
            if (Guid.Empty != reportableEventInjurySeverityId)
                dbHelper.careproviderReportableEventInjurySeverity.DeleteCareproviderReportableEventInjurySeverity(reportableEventInjurySeverityId);

            reportableEventInjurySeverityId = dbHelper.careproviderReportableEventInjurySeverity.GetIdByName("bcd-001").FirstOrDefault();
            if (Guid.Empty != reportableEventInjurySeverityId)
                dbHelper.careproviderReportableEventInjurySeverity.DeleteCareproviderReportableEventInjurySeverity(reportableEventInjurySeverityId);

            var _reportableEventInjurySeverity_Record1 = dbHelper.careproviderReportableEventInjurySeverity.CreateCareproviderReportableEventInjurySeverityRecord("abc-001", DateTime.Now, _careProviders_TeamId, false);
            var _reportableEventInjurySeverity_Record2 = dbHelper.careproviderReportableEventInjurySeverity.CreateCareproviderReportableEventInjurySeverityRecord("bcd-001", DateTime.Now, _careProviders_TeamId, false);



            loginPage
                .GoToLoginPage()
                        .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                        .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReferenceDataSection();

            referenceDataPage
              .WaitForReferenceDataPageToLoad()
              .InsertSearchQuery("Reportable Event Injury Severities")
               .TapSearchButton()
               .ClickReferenceDataMainHeader("Quality and Compliance")
               .ClickReferenceDataElement("Reportable Event Injury Severities");

            reportableEventInjuritySeveritiesPage
                .WaitForReportableEventInjurySeverityPageToLoad()
                .ValidateReportableEventInjuSevNameHeader("Name")
                .ValidateReportableEventInjuSevCodeHeader("Code")
                .ValidateReportableEventInjuSevGovCodeHeader("Gov Code")
                .ValidateReportableEventInjuSevStartDateHeader("Start Date")
                .ValidateReportableEventInjuSevEndDateHeader("End Date")
                .ValidateReportableEventInjuSevValidForExportHeader("Valid For Export")
                .ValidateReportableEventInjuSevCreatedByHeader("Created By")
                .ValidateReportableEventInjuSevCreatedOnHeader("Created On")
                .ValidateReportableEventInjuSevModifiedByHeader("Modified By")
                .ValidateReportableEventInjuSevModifiedOnHeader("Modified On")
                .ValidateRecordInPosition(1, _reportableEventInjurySeverity_Record1.ToString())
                .ValidateRecordInPosition(2, _reportableEventInjurySeverity_Record2.ToString());


            //Should display the records in the alphabetical order:Need Db Access

        }



        [TestProperty("JiraIssueID", "CDV6-16062")]
        [Description("Verify the duplication of Reference Data Reportable Event Injury Severities  in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyDuplicationRefData_UITestMethod003()
        {
            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReferenceDataSection();

            referenceDataPage
               .WaitForReferenceDataPageToLoad()
               .InsertSearchQuery("Reportable Event Injury Severities")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Quality and Compliance")
                .ClickReferenceDataElement("Reportable Event Injury Severities");

            reportableEventInjuritySeveritiesPage
                .WaitForReportableEventInjurySeverityPageToLoad()
                .ClickCreateNewRecord();

            reportableEventInjuritySeveritiesRecordPage
                .WaitForReportableEventInjurySeverityRecordPageToLoad("New")
                 .InsertReportableEventInjurieSeverityNameTextField("NameCDV6-16060")
                 .InsertReportableEventInjurieSeverityCodeTextField("12345678")
                 .InsertReportableEventInjurieSeverityGovCodeTextField("Govcode-001")
                 .InsertReportableEventInjurieSeverityNotesTextField("Notes")
                  .ClickSaveButton();
            // .WaitForAlertsSectionToLoad()
            //.ValidateAlertText("Reportable Event Injury Severity with same combination already exist: Name = NameCDV6-16060.")
            //.ClickCloseBtn();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Reportable Event Injury Severity with same combination already exist: Name = NameCDV6-16060.").TapCloseButton();


        }

        [TestProperty("JiraIssueID", "CDV6-16063")]
        [Description("Verify the Active and Inactive records of Reference Data Reportable Event Injury Severities  in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyActiveNInactiveRecordRefData_UITestMethod004()
        {
            Guid _reportableEventInjurySeverity_Recordwithstartdate = Guid.Empty;

            if (!dbHelper.careproviderReportableEventInjurySeverity.GetIdByName("startdate1").Any())
                _reportableEventInjurySeverity_Recordwithstartdate = dbHelper.careproviderReportableEventInjurySeverity.CreateCareproviderReportableEventInjurySeverityRecord("startdate1", DateTime.Now, _careProviders_TeamId, false);

            if (!dbHelper.careproviderReportableEventInjurySeverity.GetIdByName("enddate").Any())
                _reportableEventInjurySeverity_Recordwithenddate = dbHelper.careproviderReportableEventInjurySeverity.CreateCareproviderReportableEventInjurySeverityRecord("enddate", DateTime.Now, _careProviders_TeamId, false, DateTime.Now);

            if (_reportableEventInjurySeverity_Recordwithstartdate == Guid.Empty)
                _reportableEventInjurySeverity_Recordwithstartdate = dbHelper.careproviderReportableEventInjurySeverity.GetIdByName("startdate1").First();

            if (_reportableEventInjurySeverity_Recordwithenddate == Guid.Empty)
                _reportableEventInjurySeverity_Recordwithenddate = dbHelper.careproviderReportableEventInjurySeverity.GetIdByName("enddate").First();

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                      .WaitForMainMenuToLoad()
                      .NavigateToReferenceDataSection();

            referenceDataPage
              .WaitForReferenceDataPageToLoad()
              .InsertSearchQuery("Reportable Event Injury Severities")
               .TapSearchButton()
               .ClickReferenceDataMainHeader("Quality and Compliance")
               .ClickReferenceDataElement("Reportable Event Injury Severities");

            reportableEventInjuritySeveritiesPage
             .WaitForReportableEventInjurySeverityPageToLoad()
             .OpenReportableEventInjurySeverityRecord(_reportableEventInjurySeverity_Recordwithstartdate.ToString());

            reportableEventInjuritySeveritiesRecordPage
                    .WaitForReportableEventInjurySeverityRecordPageToLoad("startdate1")
                .InsertReportableEventInjurieSeverityNotesTextField("Notes")
                .ClickSaveAndCloseButton();

            reportableEventInjuritySeveritiesPage
               .WaitForReportableEventInjurySeverityPageToLoad()
               .SelectView("Inactive Records")
               .OpenReportableEventInjurySeverityRecord(_reportableEventInjurySeverity_Recordwithenddate.ToString());

            reportableEventInjuritySeveritiesRecordPage
                    .WaitForReportableEventInjurySeverityRecordPageToLoad("enddate")
                     .ValidateRepoInjurySeverityNameFieldDisabled(true);


        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15872

        [TestProperty("JiraIssueID", "CDV6-16056")]
        [Description("Verify the new record creation of Reference Data Reportable Event Role in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyNewReportableeventRoleRecordCreation_UITestMethod001()
        {
            var ReportableEventrole = "CDV6-16056" + DateTime.Now.ToString("yyyyMMddHHmmss");
            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
               .WaitForReferenceDataPageToLoad()
               .InsertSearchQuery("Reportable Event Roles")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Quality and Compliance")
                .ClickReferenceDataElement("Reportable Event Roles");

            reportableEventRolesPage
            .WaitForReportableEventRolesPageToLoad()
            .ClickCreateNewRecord();

            reportableEventRolesRecordPage
                .WaitForReportableEventRolesRecordPageToLoad("New")
                .ValidateReportableEventRolesNameField(true)
                .ValidateReportableEventRolesCodeField(true)
                .ValidateReportableEventRolesGovCodeField(true)
                .ValidateReportableEventRolesInactiveRadioBtnField(false)
                .ValidateStartDateFieldText(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .ValidateReportableEventRoleEndDateField(true)
                .ValidateReportableEventRoleValidForExportRadioBtnField(false)
                .ValidateReportableEventRoleResponsibleTeamField(true)
                .ValidateReportableEventRoleResponsibleTeamField(true)
                .InsertReportableEventRoleNameTextField(ReportableEventrole)
                .InsertReportableEventRoleCodeTextField("12345678")
                .InsertReportableEventRoleGovCodeTextField("Govcode-001")
                .InsertReportableEventRoleNotesTextField("Notes")
                .ClickSaveAndCloseButton();

            var reportableEventRoleId = dbHelper.careproviderReportableEventRole.GetByName(ReportableEventrole).Any();

            reportableEventRolesPage
                  .WaitForReportableEventRolesPageToLoad()
                  .SelectView("Active Records")
                  .SearchReportableEventRolesRecord(ReportableEventrole);
            Assert.AreEqual(true, reportableEventRoleId);

        }


        [TestProperty("JiraIssueID", "CDV6-16057")]
        [Description("Verify the default views columns of Reference Data Reportable Event Roles in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyReportableeventRoleColumnHeaderRefData_UITestMethod002()
        {

            var ReportableEventRoleRecordExists = dbHelper.careProviderReportableEventRole.GetByName("abc-001").Any();
            if (!ReportableEventRoleRecordExists)
                _reportableEventRole_Record1 = dbHelper.careproviderReportableEventRole.CreateCareProviderReportableEventRoleRecord("abc-001", DateTime.Now, _careProviders_TeamId);
            _reportableEventRole_Record1 = dbHelper.careProviderReportableEventRole.GetByName("abc-001").FirstOrDefault();

            ReportableEventRoleRecordExists = dbHelper.careProviderReportableEventRole.GetByName("bcd-001").Any();
            if (!ReportableEventRoleRecordExists)
                _reportableEventRole_Record2 = dbHelper.careproviderReportableEventRole.CreateCareProviderReportableEventRoleRecord("bcd-001", DateTime.Now, _careProviders_TeamId);
            _reportableEventRole_Record2 = dbHelper.careProviderReportableEventRole.GetByName("bcd-001").FirstOrDefault();


            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReferenceDataSection();

            referenceDataPage
              .WaitForReferenceDataPageToLoad()
               .InsertSearchQuery("Reportable Event Roles")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Quality and Compliance")
                .ClickReferenceDataElement("Reportable Event Roles");

            reportableEventRolesPage
                .WaitForReportableEventRolesPageToLoad()
                .ValidateReportableEventRolesNameHeader("Name")
                .ValidateReportableEvenRolesCodeHeader("Code")
                .ValidateReportableEventRolesGovCodeHeader("Gov Code")
                .ValidateReportableEventRolesStartDateHeader("Start Date")
                .ValidateReportableEventRolesEndDateHeader("End Date")
                .ValidateReportableEventRolesValidForExportHeader("Valid For Export")
                .ValidateReportableEventRolesCreatedByHeader("Created By")
                .ValidateReportableEventRolesCreatedOnHeader("Created On")
                .ValidateReportableEventRolesModifiedByHeader("Modified By")
                .ValidateReportableEventRolesModifiedOnHeader("Modified On")
                .ValidateRecordInPosition(1, _reportableEventRole_Record1.ToString())
                .ValidateRecordInPosition(2, _reportableEventRole_Record2.ToString());


            //Should display the records in the alphabetical order:Need Db Access

        }


        [TestProperty("JiraIssueID", "CDV6-16058")]
        [Description("Verify the duplication of Reference Data Reportable Event Roles  in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyReportableEventRoleDuplicationRefData_UITestMethod003()
        {
            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReferenceDataSection();

            referenceDataPage
               .WaitForReferenceDataPageToLoad()
               .InsertSearchQuery("Reportable Event Roles")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Quality and Compliance")
                .ClickReferenceDataElement("Reportable Event Roles");

            reportableEventRolesPage
                .WaitForReportableEventRolesPageToLoad()
                .ClickCreateNewRecord();

            reportableEventRolesRecordPage
                .WaitForReportableEventRolesRecordPageToLoad("New")
                 .InsertReportableEventRoleNameTextField("abc-001")
                 .InsertReportableEventRoleCodeTextField("12345678")
                 .InsertReportableEventRoleGovCodeTextField("Govcode-001")
                 .InsertReportableEventRoleNotesTextField("Notes")
                  .ClickSaveButton();
            // .WaitForAlertsSectionToLoad()
            //.ValidateAlertText("Reportable Event Role with same combination already exist: Name = abc-001.")
            //.ClickCloseBtn();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Reportable Event Role with same combination already exist: Name = abc-001.").TapCloseButton();


        }

        [TestProperty("JiraIssueID", "CDV6-16059")]
        [Description("Verify the Active and Inactive records of Reference Data Reportable Event Roles  in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyReportableEveRoleActiveNInactiveRecordRefData_UITestMethod004()
        {
            Guid _reportableEventRole_Recordwithstartdate = Guid.Empty;

            if (!dbHelper.careproviderReportableEventRole.GetByName("startdate1").Any())
                _reportableEventRole_Recordwithstartdate = dbHelper.careProviderReportableEventRole.CreateCareProviderReportableEventRoleRecord("startdate1", DateTime.Now, _careProviders_TeamId);

            if (!dbHelper.careproviderReportableEventRole.GetByName("enddate").Any())
                _reportableEventRole_Recordwithenddate = dbHelper.careProviderReportableEventRole.CreateCareProviderReportableEventRoleRecord("enddate", DateTime.Now, _careProviders_TeamId, DateTime.Now);

            _reportableEventRole_Recordwithstartdate = dbHelper.careproviderReportableEventRole.GetByName("startdate1").FirstOrDefault();
            _reportableEventRole_Recordwithenddate = dbHelper.careproviderReportableEventRole.GetByName("enddate").FirstOrDefault();

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Reportable Event Roles")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Quality and Compliance")
                .ClickReferenceDataElement("Reportable Event Roles");

            reportableEventRolesPage
                 .WaitForReportableEventRolesPageToLoad()
                 .OpenReportableEventRolesRecord(_reportableEventRole_Recordwithstartdate.ToString());

            reportableEventRolesRecordPage
                .WaitForReportableEventRolesRecordPageToLoad("startdate1")
                .InsertReportableEventRoleNotesTextField("Notes")
                .ClickSaveAndCloseButton();

            reportableEventRolesPage
                .WaitForReportableEventRolesPageToLoad()
                .SelectView("Inactive Records")
                .OpenReportableEventRolesRecord(_reportableEventRole_Recordwithenddate.ToString());

            reportableEventRolesRecordPage
                .WaitForReportableEventRolesRecordPageToLoad("enddate")
                .ValidateRepoRoleNameFieldDisabled(true);


        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-15833

        [TestProperty("JiraIssueID", "CDV6-16024")]
        [Description("Verify the new record creation of Reference Data Reportable Event Subcategories in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyNewReportableeventSubCategoryRecordCreation_UITestMethod001()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
               .WaitForReferenceDataPageToLoad()
               .InsertSearchQuery("Reportable Event Subcategories")
                .TapSearchButton()
                .ClickReferenceDataMainHeader("Quality and Compliance")
                .ClickReferenceDataElement("Reportable Event Subcategories");

            reportableEventSubcategoriesPage
           .WaitForReportableEventSubCategoriesPageToLoad()
           .ClickCreateNewRecord();

            reportableEventSubcategoriesRecordPage
                .WaitForReportableEventSubCategoriesRecordPageToLoad("New")
                .ValidateReportableEventSubCategoriesNameField(true)
                .ValidateReportableEventSubCategoriesCodeField(true)
                .ValidateReportableEventSubCategoriesGovCodeField(true)
                .ValidateReportableEventSubCategoriesInactiveRadioBtnField(false)
                .ValidateStartDateFieldText(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture))
                .ValidateReportableEventSubCategoriesEndDateField(true)
                .ValidateReportableEventSubCategoriesValidForExportRadioBtnField(false)
                .ValidateReportableEventSubCategoriesCategoryField(true)
                .InsertReportableEventSubCategoriesNameTextField(ReportableEventSubCategory)
                .InsertReportableEventSubCategoriesCodeTextField("12345678")
                .InsertReportableEventSubCategoriesGovCodeTextField("Govcode-001")
                .InsertReportableEventSubCategoriesNotesTextField("Notes")
                .TapReportableEventSubCategoriesCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Injuries").TapSearchButton().SelectResultElement(_reportableEventCategory_Id.ToString());

            reportableEventSubcategoriesRecordPage
                           .WaitForReportableEventSubCategoriesRecordPageToLoad("New")
                           .ClickSaveAndCloseButton();


            var reportableEventsubCategoryId = dbHelper.careProviderReportableEventSubCategory.GetByName(ReportableEventSubCategory).Any();

            reportableEventSubcategoriesPage
                      .WaitForReportableEventSubCategoriesPageToLoad()
                      .SelectView("Active Records")
                      .SearchReportableEventSubCategoriesRecord(ReportableEventSubCategory);
            Assert.AreEqual(true, reportableEventsubCategoryId);


        }


        [TestProperty("JiraIssueID", "CDV6-16025")]
        [Description("Verify the duplication of Reference Data Reportable Event Subcategories in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyReportableEventSubCategoriesDuplicationRefData_UITestMethod002()
        {
            if (!dbHelper.careProviderReportableEventSubCategory.GetByName("duplicaterecord").Any())
                dbHelper.careProviderReportableEventSubCategory.CreateCareProviderReportableEventSubCategoryRecord("duplicaterecord", DateTime.Now, _reportableEventCategory_Id, _careProviders_TeamId);

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Reportable Event Subcategories")
                 .TapSearchButton()
                 .ClickReferenceDataMainHeader("Quality and Compliance")
                 .ClickReferenceDataElement("Reportable Event Subcategories");

            reportableEventSubcategoriesPage
           .WaitForReportableEventSubCategoriesPageToLoad()
           .ClickCreateNewRecord();


            reportableEventSubcategoriesRecordPage
                .WaitForReportableEventSubCategoriesRecordPageToLoad("New")
                 .InsertReportableEventSubCategoriesNameTextField("duplicaterecord")
                 .InsertReportableEventSubCategoriesCodeTextField("12345678")
                .InsertReportableEventSubCategoriesGovCodeTextField("Govcode-001")
                .InsertReportableEventSubCategoriesNotesTextField("Notes")
                .TapReportableEventSubCategoriesCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup Records").TypeSearchQuery("Injuries").TapSearchButton().SelectResultElement(_reportableEventCategory_Id.ToString());
            reportableEventSubcategoriesRecordPage
                .WaitForReportableEventSubCategoriesRecordPageToLoad("New")
                .ClickSaveButton();


            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Reportable Event Subcategory with same combination already exist: Name = duplicaterecord AND Category = Injuries.").TapCloseButton();
        }

        [TestProperty("JiraIssueID", "CDV6-16026")]
        [Description("Verify the default views columns of Reference Data Reportable Event Subcategories in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyReportableEventSubcategoriesColumnHeaderRefData_UITestMethod003()
        {
            Guid _reportableEventSubCategory_Record1 = Guid.Empty;
            Guid _reportableEventSubCategory_Record2 = Guid.Empty;

            if (!dbHelper.careProviderReportableEventSubCategory.GetByName("aac-001").Any())
                _reportableEventSubCategory_Record1 = dbHelper.careProviderReportableEventSubCategory.CreateCareProviderReportableEventSubCategoryRecord("aac-001", DateTime.Now, _reportableEventCategory_Id, _careProviders_TeamId);

            if (!dbHelper.careProviderReportableEventSubCategory.GetByName("abf-001").Any())
                _reportableEventSubCategory_Record2 = dbHelper.careProviderReportableEventSubCategory.CreateCareProviderReportableEventSubCategoryRecord("abf-001", DateTime.Now, _reportableEventCategory_Id, _careProviders_TeamId);

            if (_reportableEventSubCategory_Record1 == Guid.Empty)
                _reportableEventSubCategory_Record1 = dbHelper.careProviderReportableEventSubCategory.GetByName("aac-001").First();

            if (_reportableEventSubCategory_Record2 == Guid.Empty)
                _reportableEventSubCategory_Record2 = dbHelper.careProviderReportableEventSubCategory.GetByName("abf-001").First();

            loginPage
                .GoToLoginPage()
                        .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                        .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Reportable Event Subcategories")
                 .TapSearchButton()
                 .ClickReferenceDataMainHeader("Quality and Compliance")
                 .ClickReferenceDataElement("Reportable Event Subcategories");

            reportableEventSubcategoriesPage
                .WaitForReportableEventSubCategoriesPageToLoad()
               .ValidateReportableEventSubCategoriesNameHeader("Name")
                .ValidateReportableEvenSubCategoriesCodeHeader("Code")
                .ValidateReportableEventSubCategoriesGovCodeHeader("Gov Code")
                .ValidateReportableEvenSubCategoriesCategoryHeader("Category")
                .ValidateReportableEventSubCategoriesStartDateHeader("Start Date")
                .ValidateReportableEventSubCategoriesEndDateHeader("End Date")
                .ValidateReportableEventSubCategoriesValidForExportHeader("Valid For Export")
                .ValidateReportableEventSubCategoriesCreatedByHeader("Created By")
                .ValidateReportableEventSubCategoriesCreatedOnHeader("Created On")
                .ValidateReportableEventSubCategoriesModifiedByHeader("Modified By")
                .ValidateReportableEventSubCategoriesModifiedOnHeader("Modified On")
                .ValidateRecordInPosition(1, _reportableEventSubCategory_Record1.ToString())
                .ValidateRecordInPosition(2, _reportableEventSubCategory_Record2.ToString());


            //Should display the records in the alphabetical order:Need Db Access

        }

        [TestProperty("JiraIssueID", "CDV6-16040")]
        [Description("Verify the Active and Inactive records of Reference Data Reportable Event SubCategories  in Quality and Compliance")]
        [TestMethod, TestCategory("UITest")]
        public void VerifyReportableEveSubCategoriesActiveNInactiveRecordRefData_UITestMethod004()
        {
            Guid _reportableEventSubCategory_Recordwithstartdate = Guid.Empty;

            if (!dbHelper.careProviderReportableEventSubCategory.GetByName("startdate").Any())
                _reportableEventSubCategory_Recordwithstartdate = dbHelper.careProviderReportableEventSubCategory.CreateCareProviderReportableEventSubCategoryRecord("startdate", DateTime.Now, _reportableEventCategory_Id, _careProviders_TeamId);

            if (!dbHelper.careProviderReportableEventSubCategory.GetByName("enddate").Any())
                _reportableEventSubCategory_Recordwithenddate = dbHelper.careProviderReportableEventSubCategory.CreateCareProviderReportableSubEventCategoryRecord("enddate", DateTime.Now, _reportableEventCategory_Id, _careProviders_TeamId, DateTime.Now);


            if (_reportableEventSubCategory_Recordwithstartdate == Guid.Empty)
                _reportableEventSubCategory_Recordwithstartdate = dbHelper.careProviderReportableEventSubCategory.GetByName("startdate").First();

            if (_reportableEventSubCategory_Recordwithenddate == Guid.Empty)
                _reportableEventSubCategory_Recordwithenddate = dbHelper.careProviderReportableEventSubCategory.GetByName("enddate").First();

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Reportable Event Subcategories")
                 .TapSearchButton()
                 .ClickReferenceDataMainHeader("Quality and Compliance")
                 .ClickReferenceDataElement("Reportable Event Subcategories");

            reportableEventSubcategoriesPage
                .WaitForReportableEventSubCategoriesPageToLoad()
             .OpenReportableEventSubCategoriesRecord(_reportableEventSubCategory_Recordwithstartdate.ToString());

            reportableEventSubcategoriesRecordPage
                    .WaitForReportableEventSubCategoriesRecordPageToLoad("startdate")
                    .InsertReportableEventSubCategoriesNotesTextField("Notes")
                    .ClickSaveAndCloseButton();

            reportableEventSubcategoriesPage
                .WaitForReportableEventSubCategoriesPageToLoad()
               .SelectView("Inactive Records")
                .OpenReportableEventSubCategoriesRecord(_reportableEventSubCategory_Recordwithenddate.ToString());

            reportableEventSubcategoriesRecordPage
                    .WaitForReportableEventSubCategoriesRecordPageToLoad("enddate")
                   .ValidateRepoEvevntSubCategoryNameFieldDisabled(true);


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

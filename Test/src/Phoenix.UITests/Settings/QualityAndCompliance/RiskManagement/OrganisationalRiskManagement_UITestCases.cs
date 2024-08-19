using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.FormsManagement
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class OrganisationalRiskManagement_UITestCases : FunctionalTest
    {


        #region Properties
        private string EnvironmentName;
        private Guid _environmentId;
        private Guid _systemUserId;
        private Guid _systemUser1Id;
        public Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _riskTypeId;
        private Guid riskMangId;
        private Guid adminUserId;
        private string currentTimeSuffix = DateTime.Now.ToString("ddMMyyyyHHmmss_FFFF");
        private string loginUsername;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Authentication

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Lanuage

                #region System User OrgRiskManagementUser1

                commonMethodsDB.CreateSystemUserRecord("OrgRiskManagementUser1", "OrgRiskManagement", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                #endregion             

                #region Environment

                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper("OrgRiskManagementUser1", "Passw0rd_!", tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);
                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];

                #endregion

                #region Create Admin user

                loginUsername = "CW_Admin_Test_User_2_" + currentTimeSuffix;
                adminUserId = dbHelper.systemUser.CreateSystemUser(loginUsername, "CW", "Admin_Test_User_2_" + currentTimeSuffix, "CW Admin_Test_User_2_" + currentTimeSuffix, "Passw0rd_!", "CW_Admin_Test_User_2_" + currentTimeSuffix + "@somemail.com", "CW_Admin_Test_User_2_" + currentTimeSuffix + "@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);

                #endregion

                #region System User

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_13709_User_01").Any();
                if (!newSystemUser)
                    dbHelper.systemUser.CreateSystemUser("Testing_CDV6_13709_User_01", "Testing", "CDV6_13709_User_01", "Testing CDV6_13709_User_01", "Summer2013@",
                        "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null,
                        _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_13709_User_01")[0];



                #endregion

                #region Risk Type

                var riskTypeExists = dbHelper.organisationalRiskType.GetRiskTypeIdByName("Automation risk").Any();
                if (!riskTypeExists)
                    dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "Automation risk", new DateTime(2020, 1, 1));
                _riskTypeId = dbHelper.organisationalRiskType.GetRiskTypeIdByName("Automation risk")[0];



                #endregion Risk Type

                dbHelper = new Phoenix.DBHelper.DatabaseHelper();

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-13709


        [TestProperty("JiraIssueID", "ACC-3214")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+' ->Click on Save and close Button" +
            "validate the system displayes the error Message upon saving the record without entering any values in any of the fields  ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod001()
        {
            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickSaveAndCloseButton()
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateRiskTypeFieldErrorLabelText("Please fill out this field.")
                .ValidateCorporateRiskRegisterFieldErrorLabelText("Please fill out this field.");

        }

        //Bug :CDV6-15515

        [TestProperty("JiraIssueID", "ACC-3215")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+' ->Click on Save Button" +
            "validate that list of risk reports is displaying in the Risk Management" + "Vaidate the Risk Section details fields,Intial Risk Score fields and Residual risk score fields" +
            "Validate all the Mandatory fields" + "Enter all the Mandatory and Optional fields and save the Record" + "Validate the created record" + "Click Save and close Button" +
            "Search the created record and validate No record lable is not visible.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod002()
        {
            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management Automation"))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }

            var nextReviewDate = DateTime.Now.AddDays(5).Date;
            string riskDescription = "Org Risk Management Automation" + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");

            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ValidateResponsibleTeamMandatoryFieldSignVisible(true)
                .WaitForRiskRecordPageToLoad()
                .ValidateRiskTypeMandatoryFieldSignVisible(true)
                .ValidateRiskIdentificationDateMandatoryFieldSignVisible(true)
                .ValidateStatusMandatoryFieldSignVisible(true)
                .ValidateCorporateRiskRegisterMandatoryFieldSignVisible(true);

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ValidateRiskDetailsSectionFields()
                .ValidateIntialRiskScoreSectionFields()
                .ValidateResidualRiskScoreSectionFields()
                .ValidateConsequenceMinMaxScore("Min 1 - Max 5")
                .ValidateLikelihoodMinMaxScore("Min 1 - Max 5")
                .ValidateResidualConsequenceMinMaxScore("Min 1 - Max 5")
                .ValidateResidualLikelihoodMinMaxScore("Min 1 - Max 5");

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .ClickRiskTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation risk")
               .TapSearchButton()
               .SelectResultElement(_riskTypeId.ToString());


            organisationalRiskManagementRecordPage
              .WaitForRiskRecordPageToLoad()
              .ClickResponsibleUserLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup View")
               .TypeSearchQuery("Testing CDV6_13709_User_01")
               .TapSearchButton()
               .SelectResultElement(_systemUserId.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription(riskDescription)
                .SelectCorporateRiskRegisterValue("Yes")
                .InsertNextReviewDate(nextReviewDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertRiskConsequenceValue("2")
                .InsertRiskLikelihoodValue("2")
                .InsertResidualConsequenceValue("3")
                .InsertResidualLikelihoodValue("3")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(5000);

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad();

            var riskManagement = dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription(riskDescription);
            Assert.AreEqual(1, riskManagement.Count);

            var riskManagementFields = dbHelper.organisationalRisk.GetByOrganisationalRiskID(riskManagement[0], "consequences", "corporateriskregisterid", "nextreviewdate",
                "residualconsequences", "residuallikelihood", "RiskDescription", "ResponsibleUserId");


            Assert.AreEqual(2, riskManagementFields["consequences"]);
            Assert.AreEqual(2, riskManagementFields["corporateriskregisterid"]);
            Assert.AreEqual(nextReviewDate, riskManagementFields["nextreviewdate"]);
            Assert.AreEqual(3, riskManagementFields["residualconsequences"]);
            Assert.AreEqual(3, riskManagementFields["residuallikelihood"]);
            Assert.AreEqual(riskDescription, riskManagementFields["riskdescription"]);
            Assert.AreEqual(_systemUserId.ToString(), riskManagementFields["responsibleuserid"].ToString());



            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .ClickSaveAndCloseButton();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                 .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .TypeSearchQuery(riskDescription)
                .ValidateNoRecordMessageVisibile(false);

        }


        [TestProperty("JiraIssueID", "ACC-3216")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+'" +
            "validate that list of risk reports is displaying in the Risk Management" + "Validate the Grid header name" + "Validate the Select view options")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod003()
        {
            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management Automation"))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }

            //In progress Status with Corporate risk as No option
            dbHelper.organisationalRisk.CreateOrgRecord(_careProviders_TeamId, _careProviders_BusinessUnitId, 1, 1, null, 1, 1, DateTime.Now.Date, "Org Risk Management Automation", _systemUserId, 1, _riskTypeId, 1);

            //Completed Status with Corporate risk as No option
            dbHelper.organisationalRisk.CreateOrgRecord(_careProviders_TeamId, _careProviders_BusinessUnitId, 1, 1, null, 1, 1, DateTime.Now.Date, "Org Risk Management Automation", _systemUserId, 1, _riskTypeId, 3);

            //In progress Status with Corporate risk as Yes option
            dbHelper.organisationalRisk.CreateOrgRecord(_careProviders_TeamId, _careProviders_BusinessUnitId, 1, 1, null, 1, 1, DateTime.Now.Date, "Org Risk Management Automation", _systemUserId, 2, _riskTypeId, 1);

            //Completed Status with Corporate risk as Yes option
            dbHelper.organisationalRisk.CreateOrgRecord(_careProviders_TeamId, _careProviders_BusinessUnitId, 1, 1, null, 1, 1, DateTime.Now.Date, "Org Risk Management Automation", _systemUserId, 2, _riskTypeId, 3);



            loginPage
              .GoToLoginPage()
              .Login(loginUsername, "Passw0rd_!", EnvironmentName)
              .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .TypeSearchQuery("Testing CDV6_13709_User_01")
                .WaitForOrganisationalRisksPageToLoad();

            System.Threading.Thread.Sleep(3000);

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ValidateRecordCellText(2, "Risk Number")
                .ValidateRecordCellText(3, "Status")
                .ValidateRecordCellText(4, "Organisational Risk Type")
                .ValidateRecordCellText(5, "Risk Identification Date")
                .ValidateRecordCellText(6, "Responsible Team")
                .ValidateRecordCellText(7, "Responsible User")
                .ValidateRecordCellText(8, "Corporate Risk Register")
                .ValidateRecordCellText(9, "Next Review Date")
                .ValidateRecordCellText(10, "Initial Risk Category")
                .ValidateRecordCellText(11, "Residual Risk Category")
                .ValidateRecordCellText(12, "Modified On")
                .ValidateRecordCellText(13, "Modified By");


            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .SelectAvailableViewByText("Active Risk")
                .ValidateNoRecordMessageVisibile(false)
                .SelectAvailableViewByText("Inactive Risk")
                .ValidateNoRecordMessageVisibile(false)
                .SelectAvailableViewByText("Active Corporate Risks")
                .ValidateNoRecordMessageVisibile(false)
                .SelectAvailableViewByText("Inactive Corporate Risks")
                .ValidateNoRecordMessageVisibile(false);
        }

        [TestProperty("JiraIssueID", "ACC-3217")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+' ->Click on Save Button" +
            " validate that list of risk reports is displaying in the Risk Management" + "Click on Add new record" + "Change the Risk status to Completed and Validate the Risk closed Date" +
            "Change the Risk status to Cancelled and Validate the Risk closed Date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod004()
        {
            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickRiskTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Automation risk")
                .TapSearchButton()
                .SelectResultElement(_riskTypeId.ToString());


            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .SelectRiskStatus("Completed")
                .WaitForRiskRecordPageToLoad()
                .ValidateRiskClosedDateEnabled()
                .SelectRiskStatus("Cancelled")
                .ValidateRiskClosedDateEnabled();
        }


        [TestProperty("JiraIssueID", "ACC-3218")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+' ->Click on Save Button" +
            " validate that list of risk reports is displaying in the Risk Management" + "Click on Add new record" + "Select the status to Completed and create a record and validate the record in Organisational risk management Page")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod005()
        {
            string riskDescription = "Org Risk Management Automation" + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");

            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management Automation"))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }

            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickRiskTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation risk")
               .TapSearchButton()
               .SelectResultElement(_riskTypeId.ToString());


            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription(riskDescription)
                .SelectCorporateRiskRegisterValue("Yes")
                .SelectRiskStatus("Completed")
                .ClickSaveButton();

            organisationalRiskManagementRecordPage
                .WaitForDisabledRiskRecordPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var riskManagement = dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription(riskDescription);
            Assert.AreEqual(1, riskManagement.Count);

            var riskManagementFields = dbHelper.organisationalRisk.GetByOrganisationalRiskID(riskManagement[0], "inactive", "riskstatusid", "riskcloseddate");


            Assert.AreEqual(true, riskManagementFields["inactive"]);
            Assert.AreEqual(3, riskManagementFields["riskstatusid"]);
            Assert.AreEqual(DateTime.Now.Date, riskManagementFields["riskcloseddate"]);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToOrganisationalRiskManagementPage();

            System.Threading.Thread.Sleep(3000);

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .SelectAvailableViewByText("Inactive Risk")
                .WaitForOrganisationalRisksPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateRecordPresent(riskManagement[0].ToString());
        }

        [TestProperty("JiraIssueID", "ACC-3219")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+' ->Click on Save Button" +
            "validate that list of risk reports is displaying in the Risk Management" + "Click on Add new record" + "Select the status to Cancelled and create a record and validate the record in Organisational risk management Page")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod006()
        {
            string riskDescription = "Org Risk Management Automation" + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");

            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management Automation"))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }


            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickRiskTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation risk")
               .TapSearchButton()
               .SelectResultElement(_riskTypeId.ToString());


            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription(riskDescription)
                .SelectCorporateRiskRegisterValue("Yes")
                .SelectRiskStatus("Cancelled")
                .ClickSaveButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var riskManagement = dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription(riskDescription);
            Assert.AreEqual(1, riskManagement.Count);

            var riskManagementFields = dbHelper.organisationalRisk.GetByOrganisationalRiskID(riskManagement[0], "inactive", "riskstatusid", "riskcloseddate");


            Assert.AreEqual(true, riskManagementFields["inactive"]);
            Assert.AreEqual(4, riskManagementFields["riskstatusid"]);
            Assert.AreEqual(DateTime.Now.Date, riskManagementFields["riskcloseddate"]);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToOrganisationalRiskManagementPage();

            System.Threading.Thread.Sleep(3000);

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .SelectAvailableViewByText("Inactive Risk")
                .WaitForOrganisationalRisksPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateRecordPresent(riskManagement[0].ToString());

        }


        [TestProperty("JiraIssueID", "ACC-3220")]
        [Description("Login to CD -> Navigate to Advanced Search" + "Select Record type as Organizational Risks" +
            "Click on Delete button on the filters" + "Click on Advanced search button " + "Validate the list of Organisational Risk Management grids are displayed." +
            "Click on add new record button" + "Enter all the mandatory fields and optional fields and save the record." +
            "Validate the fields and click on Save and close button" + "Search the created record in Organisational Risk Management page.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod007()
        {
            string riskDescription = "Org Risk Management Automation" + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");
            var nextReviewDate = DateTime.Now.AddDays(5).Date;

            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management Automation"))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }


            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickAdvancedSearchButton();

            advanceSearchPage
              .WaitForAdvanceSearchPageToLoad()
              .SelectRecordType("Organisational Risks")
              .ClickDeleteButton()
              .ClickSearchButton();

            organisationalRiskManagementPage
              .WaitForOrganisationalRiskManagementPageToLoadFromAdvancedSearch()
              .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
              .WaitForOrganisationalRiskManagementRecordPageToLoadFromAdvancedSearch()
              .ClickRiskTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation risk")
               .TapSearchButton()
               .SelectResultElement(_riskTypeId.ToString());


            organisationalRiskManagementRecordPage
              .WaitForOrganisationalRiskManagementRecordPageToLoadFromAdvancedSearch()
              .ClickResponsibleUserLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup View")
               .TypeSearchQuery("Testing CDV6_13709_User_01")
               .TapSearchButton()
               .SelectResultElement(_systemUserId.ToString());

            organisationalRiskManagementRecordPage
                .WaitForOrganisationalRiskManagementRecordPageToLoadFromAdvancedSearch()
                .InsertRiskDescription(riskDescription)
                .SelectCorporateRiskRegisterValue("Yes")
                .InsertNextReviewDate(nextReviewDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertRiskConsequenceValue("2")
                .InsertRiskLikelihoodValue("2")
                .InsertResidualConsequenceValue("3")
                .InsertResidualLikelihoodValue("3")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(5000);

            organisationalRiskManagementRecordPage
               .WaitForOrganisationalRiskManagementRecordPageToLoadFromAdvancedSearch();

            var riskManagement = dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription(riskDescription);
            Assert.AreEqual(1, riskManagement.Count);

            var riskManagementFields = dbHelper.organisationalRisk.GetByOrganisationalRiskID(riskManagement[0], "consequences", "corporateriskregisterid", "nextreviewdate",
                "residualconsequences", "residuallikelihood", "RiskDescription", "ResponsibleUserId");


            Assert.AreEqual(2, riskManagementFields["consequences"]);
            Assert.AreEqual(2, riskManagementFields["corporateriskregisterid"]);
            Assert.AreEqual(nextReviewDate, riskManagementFields["nextreviewdate"]);
            Assert.AreEqual(3, riskManagementFields["residualconsequences"]);
            Assert.AreEqual(3, riskManagementFields["residuallikelihood"]);
            Assert.AreEqual(riskDescription, riskManagementFields["riskdescription"]);
            Assert.AreEqual(_systemUserId.ToString(), riskManagementFields["responsibleuserid"].ToString());



            organisationalRiskManagementRecordPage
               .WaitForOrganisationalRiskManagementRecordPageToLoadFromAdvancedSearch()
               .ClickSaveAndCloseButton();

            organisationalRiskManagementPage
                .WaitForOrganisationalRiskManagementPageToLoadFromAdvancedSearch();

            System.Threading.Thread.Sleep(3000);

            mainMenu
                .WaitForMainMenuToLoad()
                 .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .TypeSearchQuery(riskDescription)
                .ValidateNoRecordMessageVisibile(false);

        }

        [TestProperty("JiraIssueID", "ACC-3221")]
        [Description("Login to CD -> Navigate to Advanced Search. ->Select the record type as Organisational risks." +
            "Select the Inactive status equals Yes " + "Click on search button" + "Validate the list of inactive records are available" +
            "Select the Status equals In progress and Validate the list of inprogress records are availble")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod008()
        {
            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management Automation"))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }

            //In progress Status with Corporate risk as No option
            dbHelper.organisationalRisk.CreateOrgRecord(_careProviders_TeamId, _careProviders_BusinessUnitId, 1, 1, null, 1, 1, DateTime.Now.Date, "Org Risk Management Automation", _systemUserId, 1, _riskTypeId, 2);


            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Organisational Risks")
                .SelectFilter("1", "Inactive")
                .SelectOperator("1", "Equals")
                .SelectYesNoOperator("1", "No")
                .ClickSearchButton();

            organisationalRiskManagementPage
                .WaitForOrganisationalRiskManagementPageToLoadFromAdvancedSearch()
                .ValidateNoRecordMessageVisibile(false);


            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Organisational Risks")
                .SelectFilter("1", "Status")
                .ClickRuleValueLookupButton("1");


            lookUpRecordsPopUp.WaitForLookUpRecordsPopUpToLoad().TypeSearchQuery("In progress").SelectResult("In progress");

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            organisationalRiskManagementPage
                 .WaitForOrganisationalRiskManagementPageToLoadFromAdvancedSearch()
                 .ValidateNoRecordMessageVisibile(false);

        }

        [TestProperty("JiraIssueID", "ACC-3222")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+' ->Click on Save Button" +
            "Click on assign button and assign the button to different team." + "Validate the export to excel button by selecting the record and clickon Export to Excel.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod009()
        {
            string riskDescription = "Org Risk Management Automation" + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");
            var nextReviewDate = DateTime.Now.AddDays(5).Date;

            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management Automation"))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();



            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .ClickRiskTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation risk")
               .TapSearchButton()
               .SelectResultElement(_riskTypeId.ToString());


            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickResponsibleUserLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing CDV6_13709_User_01")
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription(riskDescription)
                .SelectCorporateRiskRegisterValue("Yes")
                .InsertNextReviewDate(nextReviewDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertRiskConsequenceValue("2")
                .InsertRiskLikelihoodValue("2")
                .InsertResidualConsequenceValue("3")
                .InsertResidualLikelihoodValue("3")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(5000);

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad();

            var riskManagement = dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription(riskDescription);
            Assert.AreEqual(1, riskManagement.Count);

            var riskManagementFields = dbHelper.organisationalRisk.GetByOrganisationalRiskID(riskManagement[0], "consequences", "corporateriskregisterid", "nextreviewdate",
                "residualconsequences", "residuallikelihood", "RiskDescription", "ResponsibleUserId");


            Assert.AreEqual(2, riskManagementFields["consequences"]);
            Assert.AreEqual(2, riskManagementFields["corporateriskregisterid"]);
            Assert.AreEqual(nextReviewDate, riskManagementFields["nextreviewdate"]);
            Assert.AreEqual(3, riskManagementFields["residualconsequences"]);
            Assert.AreEqual(3, riskManagementFields["residuallikelihood"]);
            Assert.AreEqual(riskDescription, riskManagementFields["riskdescription"]);
            Assert.AreEqual(_systemUserId.ToString(), riskManagementFields["responsibleuserid"].ToString());


            organisationalRiskManagementRecordPage
                 .WaitForRiskRecordPageToLoad()
                 .ClickAssignButton();

            assignRecordPopup.WaitForAssignRecordForOrganisationalRiskManagementRecordPopupToLoad().ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("My Teams").TypeSearchQuery("CareProviders")
                .TapSearchButton().SelectResult("CareProviders");


            assignRecordPopup.SelectResponsibleUserDecision("Do not change").TapOkButton();

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .ClickSaveAndCloseButton();


            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SelectOrganisationalRiskRecord(riskManagement[0].ToString())
               .ClickExportToExcelButton();

            exportDataPopup
               .WaitForExportDataPopupToLoad()
               .SelectRecordsToExport("Selected Records")
               .SelectExportFormat("Csv (comma separated with quotes)")
               .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "OrganisationalRisks.csv");
            Assert.IsTrue(fileExists);

        }

        [TestProperty("JiraIssueID", "ACC-3223")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+' ->Enter all fields and click on save button." +
            "Update the records and click on save button" + "Navigate to Audit" + "Validate the Update in Audit")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod010()
        {
            string riskDescription = "Org Risk Management Automation" + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");
            var nextReviewDate = DateTime.Now.AddDays(5).Date;

            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management "))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }


            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();



            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickRiskTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation risk")
               .TapSearchButton()
               .SelectResultElement(_riskTypeId.ToString());


            organisationalRiskManagementRecordPage
              .WaitForRiskRecordPageToLoad()
              .ClickResponsibleUserLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing CDV6_13709_User_01")
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription(riskDescription)
                .SelectCorporateRiskRegisterValue("Yes")
                .InsertNextReviewDate(nextReviewDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertRiskConsequenceValue("2")
                .InsertRiskLikelihoodValue("2")
                .InsertResidualConsequenceValue("3")
                .InsertResidualLikelihoodValue("3")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(5000);

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad();

            var riskManagement = dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription(riskDescription);
            Assert.AreEqual(1, riskManagement.Count);

            var riskManagementFields = dbHelper.organisationalRisk.GetByOrganisationalRiskID(riskManagement[0], "consequences", "corporateriskregisterid", "nextreviewdate",
                "residualconsequences", "residuallikelihood", "RiskDescription", "ResponsibleUserId");


            Assert.AreEqual(2, riskManagementFields["consequences"]);
            Assert.AreEqual(2, riskManagementFields["corporateriskregisterid"]);
            Assert.AreEqual(nextReviewDate, riskManagementFields["nextreviewdate"]);
            Assert.AreEqual(3, riskManagementFields["residualconsequences"]);
            Assert.AreEqual(3, riskManagementFields["residuallikelihood"]);
            Assert.AreEqual(riskDescription, riskManagementFields["riskdescription"]);
            Assert.AreEqual(_systemUserId.ToString(), riskManagementFields["responsibleuserid"].ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .InsertRiskDescription(riskDescription + "  Updated  ")
               .ClickSaveButton();

            System.Threading.Thread.Sleep(5000);

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("organisationalrisk");


            System.Threading.Thread.Sleep(5000);


            auditListPage
               .WaitForAuditListPageToLoad("organisationalrisk")
               .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy("CW Admin_Test_User_2_" + currentTimeSuffix);


        }

        [TestProperty("JiraIssueID", "ACC-3224")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record '+' Enter all fields and save the record" +
            "Click on Delete button and validate the record is deleted.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Organisational Risks")]
        public void OrganisationalRiskManagement_UITestMethod011()
        {
            string riskDescription = "Org Risk Management Automation" + DateTime.Now.ToString("dd.MM.yyyy.hh.mm.ss");
            var nextReviewDate = DateTime.Now.AddDays(5).Date;



            foreach (var riskManagementId in dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription("Org Risk Management Automation"))
            {
                foreach (var organisationalRiskActionPlanId in dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(riskManagementId))
                    dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(organisationalRiskActionPlanId);

                dbHelper.organisationalRisk.DeleteOrganisationalRisk(riskManagementId);
            }


            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                 .WaitForOrganisationalRisksPageToLoad()
                 .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .ClickRiskTypeLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery("Automation risk")
               .TapSearchButton()
               .SelectResultElement(_riskTypeId.ToString());


            organisationalRiskManagementRecordPage
              .WaitForRiskRecordPageToLoad()
              .ClickResponsibleUserLookUpButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup View")
               .TypeSearchQuery("Testing CDV6_13709_User_01")
               .TapSearchButton()
               .SelectResultElement(_systemUserId.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription(riskDescription)
                .SelectCorporateRiskRegisterValue("Yes")
                .InsertNextReviewDate(nextReviewDate.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertRiskConsequenceValue("2")
                .InsertRiskLikelihoodValue("2")
                .InsertResidualConsequenceValue("3")
                .InsertResidualLikelihoodValue("3")
                .ClickSaveButton();

            System.Threading.Thread.Sleep(5000);

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad();

            var riskManagement = dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription(riskDescription);
            Assert.AreEqual(1, riskManagement.Count);

            var riskManagementFields = dbHelper.organisationalRisk.GetByOrganisationalRiskID(riskManagement[0], "consequences", "corporateriskregisterid", "nextreviewdate",
                "residualconsequences", "residuallikelihood", "RiskDescription", "ResponsibleUserId");

            Assert.AreEqual(2, riskManagementFields["consequences"]);
            Assert.AreEqual(2, riskManagementFields["corporateriskregisterid"]);
            Assert.AreEqual(nextReviewDate, riskManagementFields["nextreviewdate"]);
            Assert.AreEqual(3, riskManagementFields["residualconsequences"]);
            Assert.AreEqual(3, riskManagementFields["residuallikelihood"]);
            Assert.AreEqual(riskDescription, riskManagementFields["riskdescription"]);
            Assert.AreEqual(_systemUserId.ToString(), riskManagementFields["responsibleuserid"].ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(5000);

            var riskManagement_AfterDeletion = dbHelper.organisationalRisk.GetOrganisationalRiskIdByRiskDescription(riskDescription);
            Assert.AreEqual(0, riskManagement_AfterDeletion.Count);
        }

        #endregion


    }
}

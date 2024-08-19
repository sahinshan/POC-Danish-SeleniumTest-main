using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;
namespace Phoenix.UITests.Settings.FormsManagement
{
    /// <summary>
    /// This class contains Automated UI test scripts for
    /// </summary>
    [TestClass]
    public class OrganisationalRisk_ActionPlans_UITestCases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _riskTypeId;
        private Guid _organisatonalRiskRecord;
        private Guid _systemUserId;
        private Guid adminUser;
        private Guid adminUserId;
        private string _adminUser_Name;
        private string currentTimeSuffix = DateTime.Now.ToString("ddMMyyyyHHmmss_FFFF");
        private string loginUsername;


        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Authentication

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Business Unit
                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];


                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region System User

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_15459_User_01").Any();
                if (!newSystemUser)
                    dbHelper.systemUser.CreateSystemUser("Testing_CDV6_15459_User_01", "Testing", "CDV6_15459_User_01", "Testing_CDV6_15459_User_01", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_15459_User_01")[0];




                #endregion

                #region Admin user

                loginUsername = "CW_Admin_Test_User_2_" + currentTimeSuffix;
                adminUserId = dbHelper.systemUser.CreateSystemUser(loginUsername, "CW", "Admin_Test_User_2_" + currentTimeSuffix, "CW Admin_Test_User_2_" + currentTimeSuffix, "Passw0rd_!", "CW_Admin_Test_User_2_" + currentTimeSuffix + "@somemail.com", "CW_Admin_Test_User_2_" + currentTimeSuffix + "@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);

                _adminUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(adminUserId, "fullname")["fullname"];

                #endregion

                #region Organizational Risk Type

                var riskTypeExists = dbHelper.organisationalRiskType.GetRiskTypeIdByName("Automation risk").Any();
                if (!riskTypeExists)
                    dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "Automation risk", new DateTime(2020, 1, 1));
                _riskTypeId = dbHelper.organisationalRiskType.GetRiskTypeIdByName("Automation risk")[0];

                #endregion

                #region Organizational Risk Record

                var organisatonalRiskRecordExists = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459").Any();
                if (!organisatonalRiskRecordExists)
                    dbHelper.organisationalRisk.CreateOrgRecord(_careProviders_TeamId, _careProviders_BusinessUnitId, 1, 1, null, 1, 1,
                                                DateTime.Now.Date, "Org Risk Management Automation Action plan CDV6_15459", _systemUserId, 1, _riskTypeId, 1);

                _organisatonalRiskRecord = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459")[0];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-15459

        [TestProperty("JiraIssueID", "ACC-3203")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record " +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
            "validate the system displayes the error Message upon saving the record without entering any values in any of the fields ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases001()
        {

            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
               .GoToLoginPage()
               .Login(loginUsername, "Passw0rd_!", EnvironmentName)
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();

            actionPlansPage
               .WaitForActionPlansPageToLoad()
               .ClickNewRecordButton();

            actionPlansRecordPage
              .WaitForActionPlansRecordPageToLoad()
              .ClickSaveAndCloseButton()
              .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
              .ValidateTitleFieldErrorLabelText("Please fill out this field.");
        }


        [TestProperty("JiraIssueID", "ACC-3204")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record " +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
        "Enter all the mandatory and optional fields and save the record" + "Validate all the Mandatory fields, enabled and disabled fields" + "Click Save and Close button" +
            "Validate the Search functionality in Action plan page.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases002()
        {

            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();

            actionPlansPage
               .WaitForActionPlansPageToLoad()
               .ClickNewRecordButton();

            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ValidateActionPlanFields()
                .ValidateActionPlanMandatoryFields()
                .ValidateActionPlanLookUpFields()
                .InsertTitle("Testing_CDV6_15459_User_01 Action Plan")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_15459_User_01")
                .TapSearchButton()
                .SelectResult("Testing_CDV6_15459_User_01");

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .SelectStatusForAction("In Progress")
                 .InsertActionPlanDescription("Testing Action plan")
                 .InsertNextReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .ClickSaveButton();

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageElementsToLoad();


            System.Threading.Thread.Sleep(5000);


            var actionplanriskrecord = dbHelper.organisationalRiskActionPlan.GetByOrganisationalRiskId(_organisatonalRiskRecord);
            Assert.AreEqual(1, actionplanriskrecord.Count);

            var actionplanNumber = dbHelper.organisationalRiskActionPlan.GetOrganisationalRiskActionPlanByID(actionplanriskrecord[0], "actionplannumber")["actionplannumber"];


            var actionPlanRiskRecord_Fields = dbHelper.organisationalRiskActionPlan.GetOrganisationalRiskActionPlanByID(actionplanriskrecord[0], "actionplannumber", "ownerid", "responsibleuserid", "owningbusinessunitid",
                                        "title", "organisationalriskid", "statusforactionid", "descriptionofactionplan", "nextreviewdate");


            Assert.AreEqual(actionplanNumber, actionPlanRiskRecord_Fields["actionplannumber"]);
            Assert.AreEqual(_careProviders_TeamId.ToString(), actionPlanRiskRecord_Fields["ownerid"].ToString());
            Assert.AreEqual(_careProviders_BusinessUnitId.ToString(), actionPlanRiskRecord_Fields["owningbusinessunitid"].ToString());
            Assert.AreEqual(_systemUserId.ToString(), actionPlanRiskRecord_Fields["responsibleuserid"].ToString());
            Assert.AreEqual("Testing_CDV6_15459_User_01 Action Plan", actionPlanRiskRecord_Fields["title"]);
            Assert.AreEqual(_organisatonalRiskRecord.ToString(), actionPlanRiskRecord_Fields["organisationalriskid"].ToString());
            Assert.AreEqual(2, actionPlanRiskRecord_Fields["statusforactionid"]);
            Assert.AreEqual("Testing Action plan", actionPlanRiskRecord_Fields["descriptionofactionplan"]);
            Assert.AreEqual(DateTime.Now.Date, actionPlanRiskRecord_Fields["nextreviewdate"]);

            actionPlansRecordPage
               .WaitForActionPlansRecordPageElementsToLoad()
               .ValidateActionPlanNumber(actionplanNumber.ToString())
               .ClickSaveAndCloseButton();

            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .SearchFields(actionplanNumber.ToString())
                .ValidateRecordVisible(actionplanriskrecord[0].ToString())
                .WaitForActionPlansPageToLoad()
                .SearchFields("Testing_CDV6_15459_User_01")
                .ValidateRecordVisible(actionplanriskrecord[0].ToString())
                .SearchFields("In Progress")
                .ValidateRecordVisible(actionplanriskrecord[0].ToString());



        }

        [TestProperty("JiraIssueID", "ACC-3205")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record" +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
            "Enter all the mandatory and optional fields and save the record" + "Click save and close and validate the view selector option for Active and inactive records.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases003()
        {

            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();


            var record_InProgress = dbHelper.organisationalRiskActionPlan.CreateOrganisationalRiskActionPlan(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "Testing_CDV6_15459_User_01 Action Plan",
                _organisatonalRiskRecord, 2, "Testing Action plan", null);

            var record_Completed = dbHelper.organisationalRiskActionPlan.CreateOrganisationalRiskActionPlan(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "Testing_CDV6_15459_User_01 Action Plan",
              _organisatonalRiskRecord, 3, "Testing Action plan", null);

            var actionplanrecord = dbHelper.organisationalRiskActionPlan.GetOrganisationalRiskIdByTitle("Testing_CDV6_15459_User_01 Action Plan");
            Assert.AreEqual(2, actionplanrecord.Count);

            actionPlansPage
                 .WaitForActionPlansPageToLoad()
                 .SelectViewSelector("Related Active Action Plans")
                 .ValidateNoRecordMessageVisibile(false)
                 .ValidateRecordVisible(record_InProgress.ToString())
                 .SelectViewSelector("Related Inactive Action Plans")
                 .ValidateNoRecordMessageVisibile(false)
                 .ValidateRecordVisible(record_Completed.ToString());

        }


        [TestProperty("JiraIssueID", "ACC-3206")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record" +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
            "Enter all the details and save the record" + "Status for Action should be Completed or Cancelled -> Click on Save -> Verify that Action Plan Closing date is auto populated with Date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases004()
        {

            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();

            actionPlansPage
               .WaitForActionPlansPageToLoad()
               .ClickNewRecordButton();


            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ValidateActionPlanFields()
                .ValidateActionPlanMandatoryFields()
                .ValidateActionPlanLookUpFields()
                .InsertTitle("Testing_CDV6_15459_User_01 Action Plan")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_15459_User_01")
                .TapSearchButton()
                .SelectResult("Testing_CDV6_15459_User_01");

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .SelectStatusForAction("Completed")
                 .InsertActionPlanDescription("Testing Action plan")
                 .ValidateActionPlanClosureDateField(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));


        }


        [TestProperty("JiraIssueID", "ACC-3207")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record" +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
            "Enter all the details , select the status for Action as in progress and save the record" + "Status for Action should be Completed or Cancelled -> Click on Save -> Verify that Action Plan Closing date is auto populated with Date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases005()
        {

            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();

            actionPlansPage
               .WaitForActionPlansPageToLoad()
               .ClickNewRecordButton();


            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ValidateActionPlanFields()
                .ValidateActionPlanMandatoryFields()
                .ValidateActionPlanLookUpFields()
                .InsertTitle("Testing_CDV6_15459_User_01 Action Plan")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_15459_User_01")
                .TapSearchButton()
                .SelectResult("Testing_CDV6_15459_User_01");

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .SelectStatusForAction("In Progress")
                 .InsertActionPlanDescription("Testing Action plan")
                 .ClickSaveButton();
            Thread.Sleep(3000);

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .SelectStatusForAction("Cancelled")
                 .ClickSaveButton();

            Thread.Sleep(5000);

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .WaitForActionPlansRecordPageToLoad()
                 .ValidateActionPlanClosureDateField(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
        }

        [TestProperty("JiraIssueID", "ACC-3208")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record" +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
            "Enter all the details , select the status for Action as in progress and save the record" + "Click on Assign Button and validate the Assign functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases006()
        {
            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();


            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .ClickNewRecordButton();


            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ValidateActionPlanFields()
                .ValidateActionPlanMandatoryFields()
                .ValidateActionPlanLookUpFields()
                .InsertTitle("Testing_CDV6_15459_User_01 Action Plan")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_15459_User_01")
                .TapSearchButton()
                .SelectResult("Testing_CDV6_15459_User_01");

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .SelectStatusForAction("In Progress")
                 .InsertActionPlanDescription("Testing Action plan")
                 .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var actionplan = dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan");
            Assert.AreEqual(1, actionplan.Count);

            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ClickAssignButton();

            assignRecordPopup.WaitForAssignRecordForOrganisationalRiskManagementRecordPopupToLoad().ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("My Teams").TypeSearchQuery("CareProviders")
                .TapSearchButton().SelectResult("CareProviders");

            assignRecordPopup.SelectResponsibleUserDecision("Do not change").TapOkButton();

            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .WaitForActionPlansRecordPageElementsToLoad()
                .ClickSaveAndCloseButton();
        }


        [TestProperty("JiraIssueID", "ACC-3209")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record" +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
            "Enter all the details , select the status for Action as in progress and save the record" + "Click on Audit Button and validate the Audit functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases007()
        {

            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            //  _adminUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_, "fullname")["fullname"];


            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();


            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .ClickNewRecordButton();


            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ValidateActionPlanFields()
                .ValidateActionPlanMandatoryFields()
                .ValidateActionPlanLookUpFields()
                .InsertTitle("Testing_CDV6_15459_User_01 Action Plan")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_15459_User_01")
                .TapSearchButton()
                .SelectResult("Testing_CDV6_15459_User_01");

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .SelectStatusForAction("In Progress")
                 .InsertActionPlanDescription("Testing Action plan")
                 .ClickSaveButton();

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageElementsToLoad()
                 .InsertActionPlanDescription("Testing Action plan Updated")
                 .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var actionplan = dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan");
            Assert.AreEqual(1, actionplan.Count);
            System.Threading.Thread.Sleep(3000);


            actionPlansRecordPage
               .WaitForActionPlansRecordPageElementsToLoad()
               .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("organisationalriskactionplan");


            System.Threading.Thread.Sleep(10000);

            auditListPage
               .WaitForAuditListPageToLoad("organisationalriskactionplan")
               .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy(_adminUser_Name);

        }

        [TestProperty("JiraIssueID", "ACC-3210")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record" +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
            "Enter all the details , select the status for Action as in progress and save the record" + "Click on Delete Button and validate the Delete functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases008()
        {

            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();


            actionPlansPage
               .WaitForActionPlansPageToLoad()
               .ClickNewRecordButton();


            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ValidateActionPlanFields()
                .ValidateActionPlanMandatoryFields()
                .ValidateActionPlanLookUpFields()
                .InsertTitle("Testing_CDV6_15459_User_01 Action Plan")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_15459_User_01")
                .TapSearchButton()
                .SelectResult("Testing_CDV6_15459_User_01");

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .SelectStatusForAction("In Progress")
                 .InsertActionPlanDescription("Testing Action plan")
                 .InsertNextReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            actionPlansRecordPage
                .WaitForActionPlansRecordPageElementsToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            System.Threading.Thread.Sleep(5000);

            var riskManagement_AfterDeletion = dbHelper.organisationalRiskActionPlan.GetOrganisationalRiskIdByTitle("Testing_CDV6_15459_User_01 Action Plan");
            Assert.AreEqual(0, riskManagement_AfterDeletion.Count);
        }

        [TestProperty("JiraIssueID", "ACC-3211")]
        [Description("Login to CD -> Workplace -> Quality and Compliance -> Risk Management -> Click on Risk Management BO -> Click on Create new Record" +
            "Create a risk management record by entering all fields" + "Click on Save button" + "Navigate to Action plan Tab " + "Click on Add new record button" +
            "Enter all the details , select the status for Action as in progress and save the record" + "Click on Delete Button and validate the Delete functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases009()
        {

            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];

            organisationalRiskManagementPage
               .WaitForOrganisationalRisksPageToLoad()
               .SearchRiskRecord(riskNumber.ToString())
               .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();

            actionPlansPage
               .WaitForActionPlansPageToLoad()
               .ClickNewRecordButton();


            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ValidateActionPlanFields()
                .ValidateActionPlanMandatoryFields()
                .ValidateActionPlanLookUpFields()
                .InsertTitle("Testing_CDV6_15459_User_01 Action Plan")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_15459_User_01")
                .TapSearchButton()
                .SelectResult("Testing_CDV6_15459_User_01");

            actionPlansRecordPage
                 .WaitForActionPlansRecordPageToLoad()
                 .SelectStatusForAction("In Progress")
                 .InsertActionPlanDescription("Testing Action plan")
                 .InsertNextReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            var actionPlanRecord = dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan");

            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .SelectOrganisationalRiskActionPlanRecord(actionPlanRecord[0].ToString())
                .ClickExportToExcelButton();

            exportDataPopup
               .WaitForExportDataPopupToLoad()
               .SelectRecordsToExport("Selected Records")
               .SelectExportFormat("Csv (comma separated with quotes)")
               .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "OrganisationalRiskActionPlans.csv");
            Assert.IsTrue(fileExists);
        }

        [TestProperty("JiraIssueID", "ACC-3212")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [Description("Step 11 & 12 - Login to CD ->Click on Advanced Search Button" + "Select Record type as Organisation Risk Action plan and click on Advanced Search button" +
            "Click on Add new record button " + "Enter all Mandatory and optional fields and save the record" +
            "Navigate to Workplace->Quality and Compliance -> Risk Management " + "Validate the Created Action plan record under Action tabs of Organisational Risk record")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskActionPlans_UITestCases010()
        {
            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];


            mainMenu
               .WaitForMainMenuToLoad()
               .ClickAdvancedSearchButton();

            advanceSearchPage
              .WaitForAdvanceSearchPageToLoad()
              .SelectRecordType("Organisational Risk Action Plans")
              .ClickDeleteButton()
              .ClickSearchButton();

            actionPlansPage
                .WaitForOrganisationalRiskActionPlanPageToLoadFromAdvancedSearch()
                .ClickNewRecordButton();

            actionPlansRecordPage
                .WaitForOrganisationalRiskActionPlanRecordPageToLoadFromAdvancedSearch()
                .InsertTitle("Testing_CDV6_15459_User_01 Action Plan")
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_15459_User_01")
                .TapSearchButton()
                .SelectResult("Testing_CDV6_15459_User_01");

            actionPlansRecordPage
                 .WaitForOrganisationalRiskActionPlanRecordPageToLoadFromAdvancedSearch()
                 .SelectStatusForAction("In Progress")
                 .InsertActionPlanDescription("Testing Action plan")
                 .InsertNextReviewDate(DateTime.Now.Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                 .ClickParentRiskLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .TypeSearchQuery(riskNumber.ToString())
               .TapSearchButton()
               .SelectResult(riskNumber.ToString());

            actionPlansRecordPage
                .WaitForOrganisationalRiskActionPlanRecordPageToLoadFromAdvancedSearch()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            var actionPlan = dbHelper.organisationalRiskActionPlan.GetOrganisationalRiskIdByTitle("Testing_CDV6_15459_User_01 Action Plan");
            Assert.AreEqual(1, actionPlan.Count());



            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                 .WaitForOrganisationalRisksPageToLoad()
                 .SearchRiskRecord(riskNumber.ToString())
                 .OpenRiskRecord(_organisatonalRiskRecord.ToString());

            organisationalRiskManagementRecordPage
               .WaitForRiskRecordPageToLoad()
               .NavigateToActionPlansTab();

            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .ValidateRecordVisible(actionPlan[0].ToString())
                .ValidateNoRecordMessageVisibile(false);
        }

        [TestProperty("JiraIssueID", "ACC-3213")]
        [Description("Step 13 - Login to CD ->Click on Advanced Search Button" + "Select Record type as Organisation Risk Action plan and click on Advanced Search button" +
            "Select the filter with Tiltle and status for Action and validate the record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen", "Advanced Search")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRiskManagement_UITestMethod011()
        {
            foreach (var actionPlanId in dbHelper.organisationalRiskActionPlan.GetByTitle("Testing_CDV6_15459_User_01 Action Plan"))
                dbHelper.organisationalRiskActionPlan.DeleteOrganisationalRiskActionPlan(actionPlanId);

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            var record = dbHelper.organisationalRisk.GetRiskManagementRecordByRiskDescription("Org Risk Management Automation Action plan CDV6_15459");
            Assert.AreEqual(1, record.Count);
            var riskNumber = dbHelper.organisationalRisk.GetByOrganisationalRiskID(record[0], "risknumber")["risknumber"];


            var record_InProgress = dbHelper.organisationalRiskActionPlan.CreateOrganisationalRiskActionPlan(_careProviders_TeamId, _systemUserId, _careProviders_BusinessUnitId, "Testing_CDV6_15459_User_01 Action Plan",
              _organisatonalRiskRecord, 2, "Testing Action plan", null);


            mainMenu
               .WaitForMainMenuToLoad()
               .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Organisational Risk Action Plans")
                .SelectFilter("1", "Title")
                .SelectOperator("1", "Equals")
                .InsertFieldOptionValue("Testing_CDV6_15459_User_01 Action Plan");

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);


            actionPlansPage
                .WaitForOrganisationalRiskActionPlanPageToLoadFromAdvancedSearch()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateRecordVisible(record_InProgress.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
               .WaitForAdvanceSearchPageToLoad()
               .SelectRecordType("Organisational Risk Action Plans")
               .SelectFilter("1", "Status For Action")
               .SelectOperator("1", "Equals")
               .ClickRuleValueLookupButton("1");

            optionSetFormPopUp.WaitForOptionSetFormPopUpToLoad().TypeSearchQuery("In Progress").TapSearchButton().SelectResult("In Progress");


            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            System.Threading.Thread.Sleep(3000);


            actionPlansPage
                .WaitForOrganisationalRiskActionPlanPageToLoadFromAdvancedSearch()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateRecordVisible(record_InProgress.ToString());
        }

        #endregion


    }
}





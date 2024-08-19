using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;


namespace Phoenix.UITests.Finance.ServiceProvisions
{
    [TestClass]
    public class TrainingRequirementSetup_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private String _systemUsername;


        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Training Requirement Setup BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Training Requirement Setup T1", null, _businessUnitId, "907678", "TrainingRequirementSetupT1@careworkstempmail.com", "Training Requirement Setup T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Login System User

                _systemUsername = "TrainingRequirementSetupUser1";
                commonMethodsDB.CreateSystemUserRecord(_systemUsername, "TrainingRequirementSetup", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-216

        [TestProperty("JiraIssueID", "ACC-3059")]
        [Description("Step(s) 4 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Training Requirement Setup")]
        [TestMethod()]
        public void TrainingRequirementSetup_UITestMethod001()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "", "", new DateTime(2000, 1, 1), "");

            #endregion

            #region System User

            var userName = "Cook_Training_Item_" + currentDateTimeString;
            var userId = commonMethodsDB.CreateSystemUserRecord(userName, "Cook_Training_Item", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region System User Employment Contract

            int? statusId = null;
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, null, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, null, statusId);

            #endregion

            #region Staff Training Item

            var staffTrainingItem = "Cook Training Item " + currentDateTimeString;
            var staffTrainingItemId = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItem, new DateTime(2020, 1, 1));

            #endregion

            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTrainingRequirementSetupPage();

            trainingRequirementsSetupPage
                .WaitForTrainingRequirementsSetupPageToLoad()
                .ClickNewRecordButton();

            var requirementName = "Cook Requirement " + commonMethodsHelper.GetCurrentDateTimeString();

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .InsertRequirementName(requirementName)
                .ClickTrainingItemTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(staffTrainingItem).TapSearchButton().SelectResultElement(staffTrainingItemId);

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .InsertValidFromDate("01/01/2023")
                .ClickAllRolesYesOption()
                .ClickSaveAndCloseButton();

            trainingRequirementsSetupPage
                .WaitForTrainingRequirementsSetupPageToLoad()
                .ClickRefreshButton();


            //Get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Training Items")[0];

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //Validate system user training
            var systemUserTrainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(userId);
            Assert.AreEqual(1, systemUserTrainingRecords.Count);

            var fields = dbHelper.systemUserTraining.GetBySystemUserTrainingID(systemUserTrainingRecords[0], "trainingitemid");
            Assert.AreEqual(staffTrainingItemId.ToString(), fields["trainingitemid"].ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3060")]
        [Description("Step(s) 5 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Training Requirement Setup")]
        [TestMethod()]
        public void TrainingRequirementSetup_UITestMethod002()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "", "", new DateTime(2000, 1, 1), "");

            #endregion

            #region System User

            var userName = "Cook_Training_Item_" + currentDateTimeString;
            var userId = commonMethodsDB.CreateSystemUserRecord(userName, "Cook_Training_Item", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region System User Employment Contract

            var contractStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(20));
            int? statusId = null;
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, contractStartDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, null, statusId);

            #endregion

            #region Staff Training Item

            var staffTrainingItem = "Cook Training Item " + currentDateTimeString;
            var staffTrainingItemId = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItem, new DateTime(2020, 1, 1));

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTrainingRequirementSetupPage();

            trainingRequirementsSetupPage
                .WaitForTrainingRequirementsSetupPageToLoad()
                .ClickNewRecordButton();

            var requirementName = "Cook Requirement " + commonMethodsHelper.GetCurrentDateTimeString();

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .InsertRequirementName(requirementName)
                .ClickTrainingItemTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(staffTrainingItem).TapSearchButton().SelectResultElement(staffTrainingItemId);

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .InsertValidFromDate("01/01/2023")
                .InsertValidToDate(contractStartDate.AddDays(10).ToString("dd'/'MM'/'yyyy"))
                .ClickAllRolesYesOption()
                .ClickSaveAndCloseButton();

            trainingRequirementsSetupPage
                .WaitForTrainingRequirementsSetupPageToLoad()
                .ClickRefreshButton();


            //Get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Training Items")[0];

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //Validate system user training
            var systemUserTrainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(userId);
            Assert.AreEqual(0, systemUserTrainingRecords.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3061")]
        [Description("Step(s) 6 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Training Requirement Setup")]
        [TestMethod()]
        public void TrainingRequirementSetup_UITestMethod003()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "", "", new DateTime(2000, 1, 1), "");

            #endregion

            #region System User

            var userName = "Cook_Training_Item_" + currentDateTimeString;
            var userId = commonMethodsDB.CreateSystemUserRecord(userName, "Cook_Training_Item", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region System User Employment Contract

            var contractStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(20));
            int? statusId = null;
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, contractStartDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, null, statusId);

            #endregion

            #region Staff Training Item

            var staffTrainingItem = "Cook Training Item " + currentDateTimeString;
            var staffTrainingItemId = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItem, new DateTime(2020, 1, 1));

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTrainingRequirementSetupPage();

            trainingRequirementsSetupPage
                .WaitForTrainingRequirementsSetupPageToLoad()
                .ClickNewRecordButton();

            var requirementName = "Cook Requirement " + commonMethodsHelper.GetCurrentDateTimeString();

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .InsertRequirementName(requirementName)
                .ClickTrainingItemTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(staffTrainingItem).TapSearchButton().SelectResultElement(staffTrainingItemId);

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .InsertValidFromDate("01/01/2023")
                .InsertValidToDate(contractStartDate.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickAllRolesYesOption()
                .ClickSaveAndCloseButton();

            trainingRequirementsSetupPage
                .WaitForTrainingRequirementsSetupPageToLoad()
                .ClickRefreshButton();


            //Get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Training Items")[0];

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            this.dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(2000);

            //Validate system user training
            var systemUserTrainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(userId);
            Assert.AreEqual(0, systemUserTrainingRecords.Count);

            #endregion

        }

        #endregion

    }
}


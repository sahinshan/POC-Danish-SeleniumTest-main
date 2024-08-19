using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for Training Requirement Setup Record
    /// </summary>
    [TestClass]
    public class TrainingRequirementSetup_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _systemUserId;
        private string _systemUserName;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _careProviderStaffRoleType1Id;
        private string _careProviderStaffRoleType1Name;
        private Guid _careProviderStaffRoleType2Id;
        private string _careProviderStaffRoleType2Name;

        #endregion

        [TestInitialize()]
        public void TrainingRequirementSetupPage_TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region System User

                _systemUserName = "TrainingRequirementSetupUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "TrainingRequirementSetup", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _systemUserId);

                #endregion               

                #region Care Provider Staff Role Type

                _careProviderStaffRoleType1Name = "CDV6_23505_Role_1_" + currentDateTime;
                _careProviderStaffRoleType2Name = "CDV6_23505_Role_2_" + currentDateTime;

                _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType1Name, null, null, new DateTime(2020, 1, 1), null);
                _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), null);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-3825
        [TestProperty("JiraIssueID", "ACC-3814")]
        [Description("Test Case for CDV6-17051 -Notifications/error messages refer to the IDs and Requirement Name")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void TrainingRequirementSetup_UITestMethod001()
        {

            #region Step 1

            _careProviderStaffRoleType1Name = "ACC3814_Role1_" + currentDateTime;
            _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType1Name, null, null, new DateTime(2020, 1, 1), "1 ACC 3742 ...");
            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleType1Id };

            _careProviderStaffRoleType2Name = "ACC3814_Role2_" + currentDateTime;
            _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), "2 ACC 3742 ...");
            List<Guid> careProviderStaffRoleType2Ids = new List<Guid>() { _careProviderStaffRoleType2Id };

            string _staffTrainingItemName = "ACC3814_Training" + currentDateTime;
            Guid _staffTrainingItemId = commonMethodsDB.CreateStaffTrainingItem(_careProviders_TeamId, _staffTrainingItemName, new DateTime(2023, 1, 1));

            Guid _trainingRequirementSetupId1 = commonMethodsDB.CreateTrainingRequirementSetup("T3814a_Training" + currentDateTime, _staffTrainingItemId, new DateTime(2023, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            commonMethodsDB.CreateTrainingRequirement("T3814a_Training" + currentDateTime + " - Internal", _careProviders_TeamId, _staffTrainingItemId, new DateTime(2023, 1, 1), null, 6, 1);
            var _trainingRequirementSetupRecordId1 = (int)dbHelper.trainingRequirementSetup.GetTrainingRequirementByID(_trainingRequirementSetupId1, "recordid")["recordid"];

            loginPage
              .GoToLoginPage()
              .Login(_systemUserName, "Passw0rd_!", EnvironmentName);
            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTrainingRequirementSetupPage();
            #endregion

            #region Step 3
            trainingRequirementSetupPage
                .WaitForTrainingRequirementSetupPageToLoad()
                .ClickRecordIdHeaderLink()
                .ClickRecordIdHeaderLink()
                .ValidateRecordIsPresent(_trainingRequirementSetupId1.ToString());

            #endregion

            #region Step 11                        

            Guid _trainingRequirementSetupId2 = commonMethodsDB.CreateTrainingRequirementSetup("T3814b_Training" + currentDateTime, _staffTrainingItemId, new DateTime(2023, 1, 1), null, false, 4, careProviderStaffRoleType2Ids);
            commonMethodsDB.CreateTrainingRequirement("T3814b_Training" + currentDateTime + " - Internal", _careProviders_TeamId, _staffTrainingItemId, new DateTime(2023, 1, 1), null, 6, 1);
            var _trainingRequirementSetupRecordId2 = (int)dbHelper.trainingRequirementSetup.GetTrainingRequirementByID(_trainingRequirementSetupId2, "recordid")["recordid"];

            trainingRequirementSetupPage
                .WaitForTrainingRequirementSetupPageToLoad()
                .ClickRefreshButton()
                .ValidateRecordIsPresent(_trainingRequirementSetupId2.ToString())
                .ClickNewRecordButton();

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .ValidateRecordIdFieldVisibility(true)
                .InsertRequirementName(_staffTrainingItemName + "Dup")
                .ClickTrainingItemTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_staffTrainingItemName)
                .TapSearchButton()
                .SelectResultElement(_staffTrainingItemId.ToString());

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .InsertValidFromDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .ClickAllRolesNoOption()
                .ClickRolesLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_careProviderStaffRoleType1Name)
                .TapSearchButton()
                .AddElementToList(_careProviderStaffRoleType1Id.ToString())
                .TypeSearchQuery(_careProviderStaffRoleType2Name)
                .TapSearchButton()
                .AddElementToList(_careProviderStaffRoleType2Id.ToString())
                .TapOKButton();

            trainingRequirementSetupRecordPage
                .WaitForTrainingRequirementSetupRecordPageToLoad()
                .SelectRequirementStatusForFullAccepted("Current")
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText(_staffTrainingItemName + " setup record cannot be created because it overlaps with another record for the Role(s) selected. Please resolve the overlap or contact your System Administrator." +
                " Overlapping Setup records:")
                .ValidateMessageContainsText(_trainingRequirementSetupRecordId2 + " - " + "T3814b_Training" + currentDateTime)
                .ValidateMessageContainsText(_trainingRequirementSetupRecordId1 + " - " + "T3814a_Training" + currentDateTime)
                .TapCloseButton();
            #endregion
        }
        #endregion

    }
}

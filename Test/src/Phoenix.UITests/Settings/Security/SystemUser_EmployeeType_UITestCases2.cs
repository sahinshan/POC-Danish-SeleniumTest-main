using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_EmployeeType_UITestCases2 : FunctionalTest
    {
        #region Properties

        private string _environmentName;

        private Guid _authenticationproviderid;

        private Guid _languageId;

        private Guid _defaultBusinessUnitId;
        private Guid _businessUnit1Id;
        private Guid _businessUnit2Id;
        private Guid _businessUnit3Id;

        private Guid _defaultTeamId;
        private Guid _team1Id;
        private Guid _team2Id;
        private Guid _team3Id;

        private Guid _ethnicityId;

        private Guid _systemUser1Id;
        private Guid _systemUser2Id;
        private Guid _systemUser3Id;
        private string _systemUser1Name;
        private string _systemUser2Name;
        private string _systemUser3Name;

        private Guid _person1Id;
        private Guid _person2Id;
        private Guid _person3Id;
        private Guid _person4Id;
        private Guid _person5Id;

        #endregion

        #region Private Methods

        private void DataInitalization_Method1()
        {
            #region Business Units

            _businessUnit1Id = commonMethodsDB.CreateBusinessUnit("ET LSA BU1");
            _businessUnit2Id = commonMethodsDB.CreateBusinessUnit("ET LSA BU2");
            _businessUnit3Id = commonMethodsDB.CreateBusinessUnit("ET LSA BU3");

            #endregion

            #region Teams

            _team1Id = commonMethodsDB.CreateTeam("ET LSA T1", null, _businessUnit1Id, "907678", "ET_LSA_T1@careworkstempmail.com", "ET LSA T1", "020 123456");
            _team2Id = commonMethodsDB.CreateTeam("ET LSA T2", null, _businessUnit2Id, "907678", "ET_LSA_T2@careworkstempmail.com", "ET LSA T2", "020 123456");
            _team3Id = commonMethodsDB.CreateTeam("ET LSA T3", null, _businessUnit3Id, "907678", "ET_LSA_T3@careworkstempmail.com", "ET LSA T3", "020 123456");

            #endregion

            #region System Users

            var employeeType = 5; //Local System Administrator

            _systemUser1Name = "ET_LSA_User1";
            _systemUser1Id = commonMethodsDB.CreateSystemUserRecord(_systemUser1Name, "ET_LSA", "User1", "Passw0rd_!", _businessUnit1Id, _team1Id, _languageId, _authenticationproviderid);
            commonMethodsDB.CreateTeamMember(_team2Id, _systemUser1Id, DateTime.Now.Date, null);
            dbHelper.systemUser.UpdateEmployeeTypeId(_systemUser1Id, employeeType);

            _systemUser2Name = "ET_LSA_User2";
            _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUser2Name, "ET_LSA", "User2", "Passw0rd_!", _businessUnit2Id, _team2Id, _languageId, _authenticationproviderid);
            commonMethodsDB.CreateTeamMember(_team1Id, _systemUser2Id, DateTime.Now.Date, null);
            dbHelper.systemUser.UpdateEmployeeTypeId(_systemUser2Id, employeeType);

            _systemUser3Name = "ET_LSA_User3";
            _systemUser3Id = commonMethodsDB.CreateSystemUserRecord(_systemUser3Name, "ET_LSA", "User3", "Passw0rd_!", _businessUnit3Id, _team3Id, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateEmployeeTypeId(_systemUser3Id, employeeType);

            #endregion

            #region People

            _person1Id = commonMethodsDB.CreatePersonRecord("ET_LSA_ACC_768", "Person1", _ethnicityId, _team1Id);
            _person2Id = commonMethodsDB.CreatePersonRecord("ET_LSA_ACC_768", "Person2", _ethnicityId, _team1Id);
            _person3Id = commonMethodsDB.CreatePersonRecord("ET_LSA_ACC_768", "Person3", _ethnicityId, _team2Id);
            _person4Id = commonMethodsDB.CreatePersonRecord("ET_LSA_ACC_768", "Person4", _ethnicityId, _team2Id);
            _person5Id = commonMethodsDB.CreatePersonRecord("ET_LSA_ACC_768", "Person5", _ethnicityId, _team3Id);

            #endregion
        }

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                var tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

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

                #region Business Units

                _defaultBusinessUnitId = commonMethodsDB.CreateBusinessUnit("ET LSA Default BU");

                #endregion

                #region Teams

                _defaultTeamId = commonMethodsDB.CreateTeam("ET LSA Default Team", null, _defaultBusinessUnitId, "907678", "ET_LSA_Default_Team@careworkstempmail.com", "ET LSA Default Team", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_defaultTeamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-768

        [TestProperty("JiraIssueID", "ACC-736")]
        [Description("Local System Administrator employee type - Scenario 1")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_LocalSystemAdministrator_UITestCases001()
        {
            DataInitalization_Method1();

            var personNumber = (dbHelper.person.GetPersonById(_person5Id, "personnumber")["personnumber"]).ToString();

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_systemUser1Name, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SelectAvailableViewByText("My Team Records")
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordPresent(_person1Id.ToString())
                .ValidatePersonRecordPresent(_person2Id.ToString())
                .ValidatePersonRecordPresent(_person3Id.ToString())
                .ValidatePersonRecordPresent(_person4Id.ToString())
                .ValidatePersonRecordNotPresent(_person5Id.ToString());

            #endregion

            #region Step 2

            peoplePage
                .ClickDoNotUseViewFilterCheckbox()
                .InsertFirstName("ET_LSA_ACC_768")
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordPresent(_person1Id.ToString())
                .ValidatePersonRecordPresent(_person2Id.ToString())
                .ValidatePersonRecordPresent(_person3Id.ToString())
                .ValidatePersonRecordPresent(_person4Id.ToString())
                .ValidatePersonRecordNotPresent(_person5Id.ToString());

            #endregion

            #region Step 3

            peoplePage
                .ClickDoNotUseViewFilterCheckbox()
                .InsertFirstName("")
                .InsertID(personNumber)
                .ClickSearchButton()
                .WaitForPeoplePageToLoad()
                .ValidatePersonRecordNotPresent(_person1Id.ToString())
                .ValidatePersonRecordNotPresent(_person2Id.ToString())
                .ValidatePersonRecordNotPresent(_person3Id.ToString())
                .ValidatePersonRecordNotPresent(_person4Id.ToString())
                .ValidatePersonRecordNotPresent(_person5Id.ToString());

            #endregion

            #region Step 4

            peoplePage
                .SelectAvailableViewByText("My Team Records")
                .WaitForPeoplePageToLoad()
                .SelectPersonRecord(_person1Id.ToString())
                .SelectPersonRecord(_person2Id.ToString())
                .TapAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPersonRecordsToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("My Teams")
                .ValidateResultElementPresent(_team1Id)
                .ValidateResultElementPresent(_team2Id)
                .ValidateResultElementNotPresent(_team3Id);

            #endregion

            #region Step 5

            lookupPopup
                .SelectViewByText("Lookup View")
                .ValidateResultElementPresent(_team1Id)
                .ValidateResultElementPresent(_team2Id)
                .ValidateResultElementNotPresent(_team3Id);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-737")]
        [Description("Local System Administrator employee type - Scenario 2")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_LocalSystemAdministrator_UITestCases002()
        {
            DataInitalization_Method1();

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_systemUser1Name, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .SelectSystemView("Active Users")
                .ValidateRecordIsPresent(_systemUser1Id)
                .ValidateRecordIsPresent(_systemUser2Id)
                .ValidateRecordNotPresent(_systemUser3Id);

            #endregion

            #region Step 2

            systemUsersPage
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName("ET_LSA_User2")
                .ClickSearchButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ValidateRecordNotPresent(_systemUser1Id)
                .ValidateRecordIsPresent(_systemUser2Id)
                .ValidateRecordNotPresent(_systemUser3Id);

            #endregion

            #region Step 3

            systemUsersPage
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName("ET_LSA_User3")
                .ClickSearchButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ValidateRecordNotPresent(_systemUser1Id)
                .ValidateRecordNotPresent(_systemUser2Id)
                .ValidateRecordNotPresent(_systemUser3Id);

            #endregion

            #region Step 4

            systemUsersPage
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName("ET_LSA_User1")
                .ClickSearchButton()
                .OpenRecord(_systemUser1Id);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTeamsSubPage();

            systemUserTeamMemberSubPage
                .WaitForSystemUserTeamMemberSubPageToLoad()
                .ClickAddNewButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("teammember")
                .ClickOnExpandIcon();

            teamMemberPage
                .WaitForTeamMemberPagePageToLoad()
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectViewByText("My Teams")
                .ValidateResultElementPresent(_team1Id)
                .ValidateResultElementPresent(_team2Id)
                .ValidateResultElementNotPresent(_team3Id);

            #endregion

            #region step 5

            lookupPopup
                .SelectViewByText("Lookup View")
                .ValidateResultElementPresent(_team1Id)
                .ValidateResultElementPresent(_team2Id)
                .ValidateResultElementNotPresent(_team3Id);

            #endregion

            #region step 6

            lookupPopup
                .TypeSearchQuery("ET LSA T1")
                .TapSearchButton()
                .ValidateResultElementPresent(_team1Id)
                .ValidateResultElementNotPresent(_team2Id)
                .ValidateResultElementNotPresent(_team3Id);

            #endregion

            #region step 7

            lookupPopup
                .TypeSearchQuery("ET LSA T3")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_team1Id)
                .ValidateResultElementNotPresent(_team2Id)
                .ValidateResultElementNotPresent(_team3Id);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-738")]
        [Description("Local System Administrator employee type - Scenario 3")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_LocalSystemAdministrator_UITestCases003()
        {
            DataInitalization_Method1();

            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_systemUser1Name, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectEmployeeType("System Administrator")
                .InsertFirstName("ET_LSA")
                .InsertLastName("Test_" + currentDateTime)
                .InsertWorkEmail("ET_LSA_User999@somemail.com")
                .SelectBusinessUnitByText("ET LSA BU1")
                .ClickDefaultTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("ET LSA T1").TapSearchButton().SelectResultElement(_team1Id);

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertUserName("ET_LSA_Test_" + currentDateTime)
                .InsertPassword("Passw0rd_!")
                .InsertAvailableFromDateField(DateTime.Now.AddMonths(-1).ToString("dd'/'MM'/'yyyy"))
                .SelectSystemLanguage_Options("English (UK)")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Only System Administrators can create or update user with employee type of System Administrator.").TapCloseButton();

            #endregion

            #region Step 2

            systemUserRecordPage
                .SelectEmployeeType("Local System Administrator")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Only System Administrators can create or update user with employee type of Local System Administrator.").TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-739")]
        [Description("Local System Administrator employee type - Scenario 4")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_LocalSystemAdministrator_UITestCases004()
        {
            DataInitalization_Method1();

            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_systemUser1Name, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectEmployeeType("Core System User")
                .InsertFirstName("ET_LSA")
                .InsertLastName("Test_" + currentDateTime)
                .InsertWorkEmail("ET_LSA_User999@somemail.com")
                .SelectBusinessUnitByText("ET LSA BU1")
                .ClickDefaultTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("ET LSA T1").TapSearchButton().SelectResultElement(_team1Id);

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertUserName("ET_LSA_Test_" + currentDateTime)
                .InsertPassword("Passw0rd_!")
                .InsertAvailableFromDateField(DateTime.Now.AddMonths(-1).ToString("dd'/'MM'/'yyyy"))
                .SelectSystemLanguage_Options("English (UK)")
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox();

            var newSystemUserId = dbHelper.systemUser.GetSystemUserByUserName("ET_LSA_Test_" + currentDateTime)[0];

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("ET_LSA_Test_" + currentDateTime)
                .ClickSearchButton()
                .ValidateRecordIsPresent(newSystemUserId);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-740")]
        [Description("Local System Administrator employee type - Scenario 5")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_LocalSystemAdministrator_UITestCases005()
        {
            DataInitalization_Method1();

            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_systemUser1Name, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectEmployeeType("Provider System User")
                .InsertFirstName("ET_LSA")
                .InsertLastName("Test_" + currentDateTime)
                .InsertWorkEmail("ET_LSA_User999@somemail.com")
                .SelectBusinessUnitByText("ET LSA BU1")
                .ClickDefaultTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("ET LSA T1").TapSearchButton().SelectResultElement(_team1Id);

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertUserName("ET_LSA_Test_" + currentDateTime)
                .InsertPassword("Passw0rd_!")
                .InsertAvailableFromDateField(DateTime.Now.AddMonths(-1).ToString("dd'/'MM'/'yyyy"))
                .SelectSystemLanguage_Options("English (UK)")
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox();

            var newSystemUserId = dbHelper.systemUser.GetSystemUserByUserName("ET_LSA_Test_" + currentDateTime)[0];

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("ET_LSA_Test_" + currentDateTime)
                .ClickSearchButton()
                .ValidateRecordIsPresent(newSystemUserId);

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-741")]
        [Description("Local System Administrator employee type - Scenario 6")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        public void SystemUser_LocalSystemAdministrator_UITestCases006()
        {
            DataInitalization_Method1();

            var currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_systemUser1Name, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectEmployeeType("Rostered System User")
                .SelectGender_Options("Male")
                .InsertFirstName("ET_LSA")
                .InsertLastName("Test_" + currentDateTime)
                .SelectPets_Option("No Pets")
                .SelectSmoker_Option("No")
                .InsertWorkEmail("ET_LSA_User999@somemail.com")
                .SelectBusinessUnitByText("ET LSA BU1")
                .ClickDefaultTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("ET LSA T1").TapSearchButton().SelectResultElement(_team1Id);

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertUserName("ET_LSA_Test_" + currentDateTime)
                .InsertPassword("Passw0rd_!")
                .InsertAvailableFromDateField(DateTime.Now.AddMonths(-1).ToString("dd'/'MM'/'yyyy"))
                .SelectSystemLanguage_Options("English (UK)")
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox();

            var newSystemUserId = dbHelper.systemUser.GetSystemUserByUserName("ET_LSA_Test_" + currentDateTime)[0];

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName("ET_LSA_Test_" + currentDateTime)
                .ClickSearchButton()
                .ValidateRecordIsPresent(newSystemUserId);

            #endregion


        }

        #endregion


    }
}

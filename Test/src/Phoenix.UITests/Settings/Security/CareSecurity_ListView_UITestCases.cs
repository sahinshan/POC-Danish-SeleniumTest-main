using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class CareSecurity_ListView_UITestCases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _team1Id, _team2Id, _team3Id;
        private string _team1Name, _team2Name, _team3Name;
        private Guid _defaultLoginUserID;
        private string _defaultUser_Name;
        private Guid _systemUserID;
        private Guid TaskId;
        private string _loginUser_Name;
        private string _loginUser_Username;
        private Guid _ethnicityId;
        private Guid _personID1, _personID2, _personID3, _personID4, _personID5, _personID6;
        private int _personNumber1, _personNumber2, _personNumber3, _personNumber4, _personNumber5, _personNumber6;
        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Authentication Provider

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

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

                teamsExist = dbHelper.team.GetTeamIdByName("CDV6-19760 Team 1").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CDV6-19760 Team 1", null, _careProviders_BusinessUnitId, "19760", "CDV6_19760_Team_1@careworkstempmail.com", "CDV6-19760 Team 1", "020 123456");
                _team1Id = dbHelper.team.GetTeamIdByName("CDV6-19760 Team 1")[0];
                _team1Name = (string)dbHelper.team.GetTeamByID(_team1Id, "name")["name"];


                teamsExist = dbHelper.team.GetTeamIdByName("CDV6-19760 Team 2").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CDV6-19760 Team 2", null, _careProviders_BusinessUnitId, "19760", "CDV6_19760_Team_2@careworkstempmail.com", "CDV6-19760 Team 2", "020 123456");
                _team2Id = dbHelper.team.GetTeamIdByName("CDV6-19760 Team 2")[0];
                _team2Name = (string)dbHelper.team.GetTeamByID(_team2Id, "name")["name"];

                teamsExist = dbHelper.team.GetTeamIdByName("CDV6-19760 Team 3").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CDV6-19760 Team 3", null, _careProviders_BusinessUnitId, "19760", "CDV6_19760_Team_3@careworkstempmail.com", "CDV6-19760 Team 3", "020 123456");
                _team3Id = dbHelper.team.GetTeamIdByName("CDV6-19760 Team 3")[0];
                _team3Name = (string)dbHelper.team.GetTeamByID(_team3Id, "name")["name"];

                #endregion

                #region Create default system user

                var defaultLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").Any();
                if (!defaultLoginUserExists)
                    _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_1", "CW", "Admin_Test_User_1", "CW Admin Test User 1", "Passw0rd_!", "CW_Admin_Test_User_1@somemail.com", "CW_Admin_Test_User_1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4, null, DateTime.Now.Date);

                if (Guid.Empty == _defaultLoginUserID)
                    _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_1").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);
                _defaultUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];

                #endregion  Create default system user

                #region System User (Employee Type = "Rostered User")

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19760_Test_User").Any();
                if (!adminUserExists)
                {
                    _systemUserID = dbHelper.systemUser.CreateSystemUser("CDV6_19760_Test_User", "CDV6_19760", "Test User", "CDV6 19760 Test User 4", "Passw0rd_!",
                        "CDV6_19760_Test_User@somemail.com", "CDV6_19760_Test_User@othermail.com", "GMT Standard Time",
                        null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _team1Id, false, 3, 40);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserID, systemAdministratorSecurityProfileId);

                    dbHelper.teamMember.CreateTeamMember(_team2Id, _systemUserID, DateTime.Now.Date, null);
                }

                if (Guid.Empty == _systemUserID)
                    _systemUserID = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19760_Test_User").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserID, DateTime.Now.Date);

                _loginUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserID, "fullname")["fullname"];
                _loginUser_Username = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserID, "username")["username"];

                int employeeTypeId = (int)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserID, "employeetypeid")["employeetypeid"];

                if (employeeTypeId != 3)
                {
                    dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserID, 3);
                    var systemAdministratorSecurityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();

                    var UserSecureFieldsSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemUserID, systemAdministratorSecurityProfileId1).Any();
                    if (UserSecureFieldsSecurityProfileId == false)
                        dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserID, systemAdministratorSecurityProfileId1);
                }

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_team1Id, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

                #region Create First Person Record

                var personRecordExists1 = dbHelper.person.GetByFirstName("First_CDV6_19493").Any();
                if (!personRecordExists1)
                    _personID1 = dbHelper.person.CreatePersonRecord("", "First_CDV6_19493", "", "1", "", new DateTime(2000, 1, 2), _ethnicityId, _team1Id, 7, 2);

                if (_personID1 == Guid.Empty)
                    _personID1 = dbHelper.person.GetByFirstName("First_CDV6_19493").FirstOrDefault();

                _personNumber1 = (int)dbHelper.person.GetPersonById(_personID1, "personnumber")["personnumber"];

                #endregion

                #region Create Second Person Record

                var personRecordExists2 = dbHelper.person.GetByFirstName("Second_CDV6_19493").Any();
                if (!personRecordExists2)
                    _personID2 = dbHelper.person.CreatePersonRecord("", "Second_CDV6_19493", "", "2", "", new DateTime(2000, 1, 2), _ethnicityId, _team1Id, 7, 2);

                if (_personID2 == Guid.Empty)
                    _personID2 = dbHelper.person.GetByFirstName("Second_CDV6_19493").FirstOrDefault();

                _personNumber2 = (int)dbHelper.person.GetPersonById(_personID2, "personnumber")["personnumber"];

                #endregion

                #region Create Third Person Record

                var personRecordExists3 = dbHelper.person.GetByFirstName("Third_CDV6_19493").Any();
                if (!personRecordExists3)
                    _personID3 = dbHelper.person.CreatePersonRecord("", "Third_CDV6_19493", "", "3", "", new DateTime(2000, 1, 2), _ethnicityId, _team2Id, 7, 2);

                if (_personID3 == Guid.Empty)
                    _personID3 = dbHelper.person.GetByFirstName("Third_CDV6_19493").FirstOrDefault();

                _personNumber3 = (int)dbHelper.person.GetPersonById(_personID3, "personnumber")["personnumber"];

                #endregion

                #region Create Fourth Person Record

                var personRecordExists4 = dbHelper.person.GetByFirstName("Fourth_CDV6_19493").Any();
                if (!personRecordExists4)
                    _personID4 = dbHelper.person.CreatePersonRecord("", "Fourth_CDV6_19493", "", "4", "", new DateTime(2000, 1, 2), _ethnicityId, _team2Id, 7, 2);

                if (_personID4 == Guid.Empty)
                    _personID4 = dbHelper.person.GetByFirstName("Fourth_CDV6_19493").FirstOrDefault();

                _personNumber4 = (int)dbHelper.person.GetPersonById(_personID4, "personnumber")["personnumber"];

                #endregion

                #region Create Fifth Person Record

                var personRecordExists5 = dbHelper.person.GetByFirstName("Fifth_CDV6_19493").Any();
                if (!personRecordExists5)
                    _personID5 = dbHelper.person.CreatePersonRecord("", "Fifth_CDV6_19493", "", "5", "", new DateTime(2000, 1, 2), _ethnicityId, _team3Id, 7, 2);

                if (_personID5 == Guid.Empty)
                    _personID5 = dbHelper.person.GetByFirstName("Fifth_CDV6_19493").FirstOrDefault();

                _personNumber5 = (int)dbHelper.person.GetPersonById(_personID5, "personnumber")["personnumber"];

                #endregion

                #region Create Sixth Person Record

                var personRecordExists6 = dbHelper.person.GetByFirstName("Sixth_CDV6_19493").Any();
                if (!personRecordExists6)
                    _personID6 = dbHelper.person.CreatePersonRecord("", "Sixth_CDV6_19493", "", "6", "", new DateTime(2000, 1, 2), _ethnicityId, _team3Id, 7, 2);

                if (_personID6 == Guid.Empty)
                    _personID6 = dbHelper.person.GetByFirstName("Sixth_CDV6_19493").FirstOrDefault();

                _personNumber6 = (int)dbHelper.person.GetPersonById(_personID6, "personnumber")["personnumber"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-19760

        [TestProperty("JiraIssueID", "ACC-3243")]
        [Description("1) System User should have Rostered System User Employee type" +
                     "Verify Associate Teams Records and Person Records and Task should be display and Other not. Open Task and Verify Responsible User link should be prevent message." +
                     "2) Change System User Employee type to System Administrator" +
                     "Verify All Teams Records, Person Records and Task should be display. Open Task and Verify Responsible User link should be prevent message." +
                     "3) Change System User Employee type to Core / Provider System User" +
                     "Verify Associate Teams Records and Person Records and Task should be display.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Security Model")]
        [TestProperty("BusinessModule2", "Care Security Model - Filter Teams")]
        [TestProperty("BusinessModule3", "Care Security Model - Filter Users")]
        [TestProperty("Screen1", "People")]
        [TestProperty("Screen2", "Teams")]
        [TestProperty("Screen3", "Tasks")]
        public void Care_Security_List_View_UITestMethod01()
        {
            #region Create Task for Person Record

            var taskExist = dbHelper.task.GetBySubject("Task - CDV6_19493").Any();
            if (!taskExist)
                TaskId = dbHelper.task.CreatePersonTask(_defaultLoginUserID, "First_CDV6_19493 1", "Task - CDV6_19493", "notes....", _team1Id);

            if (TaskId == Guid.Empty)
                TaskId = dbHelper.task.GetBySubject("Task - CDV6_19493").FirstOrDefault();
            dbHelper.task.UpdateResponsibleTeamOwner(TaskId, _team1Id, _defaultLoginUserID, _personID1);
            #endregion

            // Login in to Application

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            // Verify My Teams People & Teams Record

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString()).ValidatePersonRecord(_personID1.ToString(), _personID1.ToString())
                .SearchPersonRecordByID(_personNumber3.ToString()).ValidatePersonRecord(_personID3.ToString(), _personID3.ToString())
                .SearchPersonRecordByID(_personNumber5.ToString()).ValidateNoRecordsMessageVisibility(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTeamsSection();

            teamsPage
                .WaitForTeamsPageToLoad()
                .SearchTeamsRecord(_team1Name.ToString()).ValidateTeamsRecord(_team1Id.ToString(), _team1Id.ToString())
                .SearchTeamsRecord(_team2Name.ToString()).ValidateTeamsRecord(_team2Id.ToString(), _team2Id.ToString())
                .SearchTeamsRecord(_team3Name.ToString()).ValidateNoRecordsMessageVisibility(true);

            // Navigate to People Record and Verify Associate task
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateRecordVisible(TaskId.ToString());


            dbHelper.task.UpdateResponsibleTeamOwner(TaskId, _team3Id, _defaultLoginUserID, _personID1); // Change Responsible Team Owner to Other Team

            // Verify Task associate with other team
            personTasksPage
                .ClickRefreshButton()
                .ValidateRecordNotVisible(TaskId.ToString());

            dbHelper.task.UpdateResponsibleTeamOwner(TaskId, _team2Id, _defaultLoginUserID, _personID1); // Change Responsible Team Owner to again My Team

            // Open Task and Verify Responsible User link should be prevent message
            personTasksPage
                .ClickRefreshButton()
                .ValidateRecordVisible(TaskId.ToString())
                .OpenPersonTaskRecord(TaskId.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task - CDV6_19493")
                .ClickResponsibleUserLinkFieldText();

            lookupViewPopup
                .WaitForAlertLookupViewPopupToLoad()
                .ValidateAlertCWNotificationMessage()
                .ClickCloseButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task - CDV6_19493");

            dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserID, 4); // Update System User Employee Type to System Administrator

            // Verify Other Teams People Records
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber5.ToString()).ValidatePersonRecord(_personID5.ToString(), _personID5.ToString())
                .SearchPersonRecordByID(_personNumber1.ToString())
                .OpenPersonRecord(_personID1.ToString());

            // Open Task and Verify Responsible User link should not be prevent message
            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ValidateRecordVisible(TaskId.ToString())
                .OpenPersonTaskRecord(TaskId.ToString());

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Task - CDV6_19493")
                .ClickResponsibleUserLinkFieldText();

            lookupViewPopup
                .WaitForLookupViewPopupToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateSystemUserRecordTitle(_defaultUser_Name);

            // Verify My and Other Teams Record
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTeamsSection();

            teamsPage
                .WaitForTeamsPageToLoad()
                .SearchTeamsRecord(_team3Name.ToString()).ValidateTeamsRecord(_team3Id.ToString(), _team3Id.ToString());

            // Update Employee Type to Provider System User
            dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserID, 2);
            var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserID, systemAdministratorSecurityProfileId);

            // Verify Only My Teams People & Teams should be display
            mainMenu
                .WaitForMainMenuToLoad().refreshPage()
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber3.ToString()).ValidatePersonRecord(_personID3.ToString(), _personID3.ToString())
                .SearchPersonRecordByID(_personNumber5.ToString()).ValidateNoRecordsMessageVisibility(true);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToTeamsSection();

            teamsPage
                .WaitForTeamsPageToLoad()
                .SearchTeamsRecord(_team1Name.ToString()).ValidateTeamsRecord(_team1Id.ToString(), _team1Id.ToString())
                .SearchTeamsRecord(_team3Name.ToString()).ValidateNoRecordsMessageVisibility(true);

        }

        #endregion


    }
}

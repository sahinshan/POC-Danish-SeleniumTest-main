using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_Tasks_CareProviders_UITestCases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _team1Id;
        private Guid _team2Id;
        private Guid _team3Id;
        private Guid _team4Id;
        private Guid adminUserId1;
        private Guid adminUserId2;
        private Guid adminUserId3;
        private Guid adminUserId4;
        private Guid _ethnicityId;
        private Guid _personID;
        private int _personNumber;
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

                var teamsExist = dbHelper.team.GetTeamIdByName("CDV6-19500 Team 1").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CDV6-19500 Team 1", null, _careProviders_BusinessUnitId, "90400", "CDV6_19500_Team_1@careworkstempmail.com", "CDV6-19500 Team 1", "020 123456");
                _team1Id = dbHelper.team.GetTeamIdByName("CDV6-19500 Team 1")[0];

                teamsExist = dbHelper.team.GetTeamIdByName("CDV6-19500 Team 2").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CDV6-19500 Team 2", null, _careProviders_BusinessUnitId, "90400", "CDV6_19500_Team_2@careworkstempmail.com", "CDV6-19500 Team 2", "020 123456");
                _team2Id = dbHelper.team.GetTeamIdByName("CDV6-19500 Team 2")[0];

                teamsExist = dbHelper.team.GetTeamIdByName("CDV6-19500 Team 3").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CDV6-19500 Team 3", null, _careProviders_BusinessUnitId, "90400", "CDV6_19500_Team_3@careworkstempmail.com", "CDV6-19500 Team 3", "020 123456");
                _team3Id = dbHelper.team.GetTeamIdByName("CDV6-19500 Team 3")[0];

                teamsExist = dbHelper.team.GetTeamIdByName("CDV6-19500 Team 4").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CDV6-19500 Team 4", null, _careProviders_BusinessUnitId, "90400", "CDV6_19500_Team_2@careworkstempmail.com", "CDV6-19500 Team 4", "020 123456");
                _team4Id = dbHelper.team.GetTeamIdByName("CDV6-19500 Team 4")[0];

                #endregion

                #region Admin User 1 (Employee Type = "System Administrator")

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19500_Test_User_1").Any();
                if (!adminUserExists)
                {
                    adminUserId1 = dbHelper.systemUser.CreateSystemUser("CDV6_19500_Test_User_1", "CDV6_19500", "Test User 1", "CDV6 19500 Test User 1", "Passw0rd_!",
                        "CDV6_19500_Test_User_1@somemail.com", "CDV6_19500_Test_User_1@othermail.com", "GMT Standard Time",
                        null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _team1Id, false, 4, 40);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId1, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId1, systemUserSecureFieldsSecurityProfileId);
                }

                adminUserId1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19500_Test_User_1").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId1, DateTime.Now.Date);

                #endregion

                #region Admin User 2 (Employee Type = "Core System User")

                adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19500_Test_User_2").Any();
                if (!adminUserExists)
                {
                    adminUserId2 = dbHelper.systemUser.CreateSystemUser("CDV6_19500_Test_User_2", "CDV6_19500", "Test User 2", "CDV6 19500 Test User 2", "Passw0rd_!",
                        "CDV6_19500_Test_User_2@somemail.com", "CDV6_19500_Test_User_2@othermail.com", "GMT Standard Time",
                        null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _team1Id, false, 2, 40);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId2, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId2, systemUserSecureFieldsSecurityProfileId);

                    //dbHelper.teamMember.CreateTeamMember(_team1Id, adminUserId2, DateTime.Now.Date, null);
                    //dbHelper.teamMember.CreateTeamMember(_team2Id, adminUserId2, DateTime.Now.Date, null);
                }
                adminUserId2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19500_Test_User_2").FirstOrDefault();

                var teamrecord = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(adminUserId2, _team2Id).Any();
                if (!teamrecord)
                    dbHelper.teamMember.CreateTeamMember(_team2Id, adminUserId2, DateTime.Now.Date, null);
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId2, DateTime.Now.Date);

                #endregion

                #region Admin User 3 (Employee Type = "Provider System User")

                adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19500_Test_User_3").Any();
                if (!adminUserExists)
                {
                    adminUserId3 = dbHelper.systemUser.CreateSystemUser("CDV6_19500_Test_User_3", "CDV6_19500", "Test User 3", "CDV6 19500 Test User 3", "Passw0rd_!",
                        "CDV6_19500_Test_User_3@somemail.com", "CDV6_19500_Test_User_3@othermail.com", "GMT Standard Time",
                        null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _team1Id, false, 1, 40);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId3, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId3, systemUserSecureFieldsSecurityProfileId);

                    //dbHelper.teamMember.CreateTeamMember(_team1Id, adminUserId2, DateTime.Now.Date, null);
                    dbHelper.teamMember.CreateTeamMember(_team3Id, adminUserId3, DateTime.Now.Date, null);
                }

                adminUserId3 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19500_Test_User_3").FirstOrDefault();
                teamrecord = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(adminUserId3, _team3Id).Any();
                if (!teamrecord)
                    dbHelper.teamMember.CreateTeamMember(_team3Id, adminUserId3, DateTime.Now.Date, null);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId3, DateTime.Now.Date);

                #endregion

                #region Admin User 4 (Employee Type = "Rostered System User")

                adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19500_Test_User_4").Any();
                if (!adminUserExists)
                {
                    adminUserId4 = dbHelper.systemUser.CreateSystemUser("CDV6_19500_Test_User_4", "CDV6_19500", "Test User 4", "CDV6 19500 Test User 4", "Passw0rd_!",
                        "CDV6_19500_Test_User_4@somemail.com", "CDV6_19500_Test_User_4@othermail.com", "GMT Standard Time",
                        null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _team1Id, false, 3, 40);

                    var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId4, systemAdministratorSecurityProfileId);
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId4, systemUserSecureFieldsSecurityProfileId);

                    //dbHelper.teamMember.CreateTeamMember(_team1Id, adminUserId2, DateTime.Now.Date, null);
                    //dbHelper.teamMember.CreateTeamMember(_team4Id, adminUserId4, DateTime.Now.Date, null);
                }

                adminUserId4 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19500_Test_User_4").FirstOrDefault();
                teamrecord = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(adminUserId4, _team4Id).Any();
                if (!teamrecord)
                    dbHelper.teamMember.CreateTeamMember(_team4Id, adminUserId4, DateTime.Now.Date, null);

                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId4, DateTime.Now.Date);

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_team1Id, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

                #region Person

                var firstName = "Testing";
                var middleName = DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "CDV6-19500";

                _personID = dbHelper.person.CreatePersonRecord("", firstName, middleName, lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _team1Id, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-19500

        [TestProperty("JiraIssueID", "ACC-3166")]
        [Description("Validate that a user with 'Employee Type' = 'System Administrator' can search by any team in the system using a Team lookup")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "Security")]
        [TestProperty("Screen1", "People")]
        public void Care_Security_Filter_Teams_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login("CDV6_19500_Test_User_1", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .SelectPersonRecord(_personID.ToString())
                .TapAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPersonRecordsToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View")
                .TypeSearchQuery("CDV6-19500 Team 1").TapSearchButton().ValidateResultElementPresent(_team1Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 2").TapSearchButton().ValidateResultElementPresent(_team2Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 3").TapSearchButton().ValidateResultElementPresent(_team3Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 4").TapSearchButton().ValidateResultElementPresent(_team4Id.ToString());
        }

        [TestProperty("JiraIssueID", "ACC-3167")]
        [Description("Validate that a user with 'Employee Type' = 'Core System User' can only search teams he is a member of")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "Security")]
        [TestProperty("Screen1", "People")]
        public void Care_Security_Filter_Teams_UITestMethod02()
        {

            loginPage
                .GoToLoginPage()
                .Login("CDV6_19500_Test_User_2", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .SelectPersonRecord(_personID.ToString())
                .TapAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPersonRecordsToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View")
                .TypeSearchQuery("CDV6-19500 Team 1").TapSearchButton().ValidateResultElementPresent(_team1Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 2").TapSearchButton().ValidateResultElementPresent(_team2Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 3").TapSearchButton().ValidateResultElementNotPresent(_team3Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 4").TapSearchButton().ValidateResultElementNotPresent(_team4Id.ToString());
        }

        [TestProperty("JiraIssueID", "ACC-3168")]
        [Description("Validate that a user with 'Employee Type' = 'Provider System User' can only search teams he is a member of")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "Security")]
        [TestProperty("Screen1", "People")]
        public void Care_Security_Filter_Teams_UITestMethod03()
        {

            loginPage
                .GoToLoginPage()
                .Login("CDV6_19500_Test_User_3", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .SelectPersonRecord(_personID.ToString())
                .TapAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPersonRecordsToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View")
                .TypeSearchQuery("CDV6-19500 Team 1").TapSearchButton().ValidateResultElementPresent(_team1Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 2").TapSearchButton().ValidateResultElementNotPresent(_team2Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 3").TapSearchButton().ValidateResultElementPresent(_team3Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 4").TapSearchButton().ValidateResultElementNotPresent(_team4Id.ToString());
        }

        [TestProperty("JiraIssueID", "ACC-3169")]
        [Description("Validate that a user with 'Employee Type' = 'Restored System User' can only search teams he is a member of")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "Security")]
        [TestProperty("Screen1", "People")]
        public void Care_Security_Filter_Teams_UITestMethod04()
        {

            loginPage
                .GoToLoginPage()
                .Login("CDV6_19500_Test_User_4", "Passw0rd_!", EnvironmentName)
                .WaitFormHomePageToLoad(false, false, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .SelectPersonRecord(_personID.ToString())
                .TapAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupForPersonRecordsToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View")
                .TypeSearchQuery("CDV6-19500 Team 1").TapSearchButton().ValidateResultElementPresent(_team1Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 2").TapSearchButton().ValidateResultElementNotPresent(_team2Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 3").TapSearchButton().ValidateResultElementNotPresent(_team3Id.ToString())
                .TypeSearchQuery("CDV6-19500 Team 4").TapSearchButton().ValidateResultElementPresent(_team4Id.ToString());
        }

        #endregion



    }
}

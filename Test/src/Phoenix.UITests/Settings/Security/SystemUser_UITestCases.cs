using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_UITestCases : FunctionalTest
    {
        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _businessUnitId2;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _teamId2;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private List<Guid> userSecProfiles;

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");
                _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("Mobile Business Unit");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _teamId2 = commonMethodsDB.CreateTeam("Mobile Team Security", null, _businessUnitId2, "907178", "MobileTeamSecurity@careworkstempmail.com", "Mobile Team Security", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User PSystemUser1

                _systemUsername = "PSystemUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PSystem", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-788

        [Description("Navigate to the system users page - perform a quick search using a user first name - Validate that only the matching results are displayed")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24581")]
        public void SystemUser_UITestMethod01()
        {
            userSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("System Administrator")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],

            };

            var userid1 = commonMethodsDB.CreateSystemUserRecord("SecurityProfileTestUser2", "Security", "Profile Test User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var userid2 = commonMethodsDB.CreateSystemUserRecord("SecurityTestUser2", "Security", "Test User2", "Passw0rd_!", _businessUnitId2, _teamId2, _languageId, _authenticationproviderid, userSecProfiles);
            var userid3_Control = commonMethodsDB.CreateSystemUserRecord("AaronTest", "Aaron", "Test", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);

            string user1id_Id = dbHelper.systemUser.GetSystemUserBySystemUserID(userid1, "usernumber")["usernumber"].ToString();
            string user2id_Id = dbHelper.systemUser.GetSystemUserBySystemUserID(userid2, "usernumber")["usernumber"].ToString();

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()

                .InsertUserName("*Security*")
                .ClickSearchButton()

                .WaitForResultsGridToLoad()

                .ValidateRecordCellText(userid1, 2, "No") //Id
                .ValidateRecordCellText(userid1, 3, "Security") //Name
                .ValidateRecordCellText(userid1, 4, "Profile Test User2") //User Name
                .ValidateRecordCellText(userid1, 5, "Security Profile Test User2") //Business Unit
                .ValidateRecordCellText(userid1, 6, "CareDirector QA") //Default Team

                .ValidateRecordCellText(userid2, 2, "No") //Id
                .ValidateRecordCellText(userid2, 3, "Security") //Name
                .ValidateRecordCellText(userid2, 4, "Test User2") //User Name
                .ValidateRecordCellText(userid2, 5, "Security Test User2") //Business Unit
                .ValidateRecordCellText(userid2, 6, "Mobile Business Unit") //Default Team

                .ValidateRecordNotPresent(userid3_Control.ToString())
                ;


        }

        [Description("Navigate to the system users page - perform a quick search using a user last name - Validate that only the matching results are displayed")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24582")]
        public void SystemUser_UITestMethod02()
        {
            userSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("System Administrator")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],

            };

            var userid1 = commonMethodsDB.CreateSystemUserRecord("Bakshish.Singh", "Bakshish", "Singh", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var userid2 = commonMethodsDB.CreateSystemUserRecord("Johnson.Singh", "Johnson", "Singh", "Passw0rd_!", _businessUnitId, _teamId2, _languageId, _authenticationproviderid, userSecProfiles);
            var userid3_Control = commonMethodsDB.CreateSystemUserRecord("AaronTest", "Aaron", "Test", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);

            string user1id_Id = dbHelper.systemUser.GetSystemUserBySystemUserID(userid1, "usernumber")["usernumber"].ToString();
            string user2id_Id = dbHelper.systemUser.GetSystemUserBySystemUserID(userid2, "usernumber")["usernumber"].ToString();

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()

                .UncheckDoNotUseViewFilterCheckbox()
                .InsertUserName("*Singh*")
                .ClickSearchButton()

                .WaitForResultsGridToLoad()

                .ValidateRecordCellText(userid1, 2, user1id_Id) //Id
                .ValidateRecordCellText(userid1, 3, "Bakshish Singh") //Name
                .ValidateRecordCellText(userid1, 4, "Bakshish.Singh") //User Name
                .ValidateRecordCellText(userid1, 5, "CareDirector QA") //Business Unit
                .ValidateRecordCellText(userid1, 6, "CareDirector QA") //Default Team

                .ValidateRecordCellText(userid2, 2, user2id_Id) //Id
                .ValidateRecordCellText(userid2, 3, "Johnson Singh") //Name
                .ValidateRecordCellText(userid2, 4, "Johnson.Singh") //User Name
                .ValidateRecordCellText(userid2, 5, "CareDirector QA") //Business Unit
                .ValidateRecordCellText(userid2, 6, "Mobile Team Security") //Default Team

                .ValidateRecordNotPresent(userid3_Control.ToString())
                ;


        }

        [Description("Navigate to the system users page - perform a quick search using a user first and last names - Validate that only the matching results are displayed")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24583")]
        public void SystemUser_UITestMethod03()
        {
            userSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("System Administrator")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],

            };

            var userid1 = commonMethodsDB.CreateSystemUserRecord("SecurityTestUserAdmin2", "Security", "Test User Admin2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var userid2 = commonMethodsDB.CreateSystemUserRecord("SecurityTestUser2", "Security", "Test User2", "Passw0rd_!", _businessUnitId2, _teamId2, _languageId, _authenticationproviderid, userSecProfiles);
            var userid3_Control = commonMethodsDB.CreateSystemUserRecord("SecurityProfileTestUser", "Security", "Profile Test User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);

            string user1id_Id = dbHelper.systemUser.GetSystemUserBySystemUserID(userid1, "usernumber")["usernumber"].ToString();
            string user2id_Id = dbHelper.systemUser.GetSystemUserBySystemUserID(userid2, "usernumber")["usernumber"].ToString();

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()

                .UncheckDoNotUseViewFilterCheckbox()
                .InsertName("*Security Test User*")
                .ClickSearchButton()

                .WaitForResultsGridToLoad()

                .ValidateRecordCellText(userid1, 2, user1id_Id) //Id
                .ValidateRecordCellText(userid1, 3, "Security Test User Admin2") //Name
                .ValidateRecordCellText(userid1, 4, "SecurityTestUserAdmin2") //User Name
                .ValidateRecordCellText(userid1, 5, "CareDirector QA") //Business Unit
                .ValidateRecordCellText(userid1, 6, "CareDirector QA") //Default Team

                .ValidateRecordCellText(userid2, 2, user2id_Id) //Id
                .ValidateRecordCellText(userid2, 3, "Security Test User2") //Name
                .ValidateRecordCellText(userid2, 4, "SecurityTestUser2") //User Name
                .ValidateRecordCellText(userid2, 5, "Mobile Business Unit") //Business Unit
                .ValidateRecordCellText(userid2, 6, "Mobile Team Security") //Default Team

                .ValidateRecordNotPresent(userid3_Control.ToString())
                ;


        }

        [Description("Navigate to the system users page - perform a quick search using a user first and last names - Open the matching system user record - " +
            "On the system user record page validate that the first and last name fields are correctly set")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24584")]
        public void SystemUser_UITestMethod04()
        {
            userSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("System Administrator")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],

            };

            var userid1 = commonMethodsDB.CreateSystemUserRecord("SecurityTestUserAdmin2", "Security", "Test User Admin2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()

                .InsertName("Security Test User Admin2")
                .ClickSearchButton()

                .WaitForResultsGridToLoad()

                .OpenRecord(userid1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateFirstNameFieldValue("Security")
                .ValidateLastNameFieldValue("Test User Admin2");
        }

        #endregion

    }
}

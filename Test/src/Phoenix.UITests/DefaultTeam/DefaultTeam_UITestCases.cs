using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Phoenix.UITests.DefaultTeam
{
    [TestClass]
    public class DefaultTeam_UITestCases : FunctionalTest
    {

        #region Properties

        private Guid _authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        private Guid _languageId;
        private Guid _MultiTeamAdminUser_UserId;
        private Guid _ChangingDefaultTeamsUser_UserId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _AdultSafeguarding_TeamId;
        private Guid _BridgendAdoption_TeamId;
        private Guid _Advanced_TeamId;
        private Guid _systemUserId;
        private string _systemUsername;
        private Guid _ethnicityId;
        private Guid _personId;
        string firstName;
        string lastName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void DefaultTeamTests_SetupTest()
        {
            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language

            #region Business Unit

            var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
            if (!businessUnitExists)
                dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
            _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

            #endregion

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
            _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

            teamsExist = dbHelper.team.GetTeamIdByName("Adult Safeguarding").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("Adult Safeguarding", null, _careDirectorQA_BusinessUnitId, "907679", "AdultSafeguarding@careworkstempmail.com", "Adult Safeguarding", "020 123457");
            _AdultSafeguarding_TeamId = dbHelper.team.GetTeamIdByName("Adult Safeguarding")[0];

            teamsExist = dbHelper.team.GetTeamIdByName("Bridgend - Adoption").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("Bridgend - Adoption", null, _careDirectorQA_BusinessUnitId, "907680", "BridgendAdoption@careworkstempmail.com", "Bridgend - Adoption", "020 123458");
            _BridgendAdoption_TeamId = dbHelper.team.GetTeamIdByName("Bridgend - Adoption")[0];

            teamsExist = dbHelper.team.GetTeamIdByName("Advanced").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("Advanced", null, _careDirectorQA_BusinessUnitId, "907681", "Advanced@careworkstempmail.com", "Advanced", "020 123459");
            _Advanced_TeamId = dbHelper.team.GetTeamIdByName("Advanced")[0];
            #endregion

            #region System User
            _systemUsername = "DefaultTeamTestUser1";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DefaultTeamTest", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

            #endregion

            #region System User - MultiTeamAdminUser

            var userExists = dbHelper.systemUser.GetSystemUserByUserName("MultiTeamAdminUser").Any();
            if (!userExists)
            {
                _MultiTeamAdminUser_UserId = dbHelper.systemUser.CreateSystemUser("MultiTeamAdminUser", "Multi Team", "Admin User", "Multi Team Admin User", "Passw0rd_!", "MultiTeamAdminUser@somemail.com", "MultiTeamAdminUser@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date, true);

                dbHelper.teamMember.CreateTeamMember(_AdultSafeguarding_TeamId, _MultiTeamAdminUser_UserId, DateTime.Now.Date.AddDays(-15), null);

                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_MultiTeamAdminUser_UserId, systemAdministratorSecurityProfileId);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_MultiTeamAdminUser_UserId, systemUserSecureFieldsSecurityProfileId);
            }
            if (_MultiTeamAdminUser_UserId == Guid.Empty)
            {
                _MultiTeamAdminUser_UserId = dbHelper.systemUser.GetSystemUserByUserName("MultiTeamAdminUser").FirstOrDefault();
            }

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_MultiTeamAdminUser_UserId, DateTime.Now.Date);

            #endregion

            #region System User - ChangingDefaultTeamsUser

            userExists = dbHelper.systemUser.GetSystemUserByUserName("ChangingDefaultTeamsUser").Any();
            if (!userExists)
            {
                _ChangingDefaultTeamsUser_UserId = dbHelper.systemUser.CreateSystemUser("ChangingDefaultTeamsUser", "Changing Default", "Admin User", "Changing Default Teams User",
                    "Passw0rd_!", "ChangingDefaultTeamsUser@somemail.com", "ChangingDefaultTeamsUser@secureemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, DateTime.Now.Date, true);

                dbHelper.teamMember.CreateTeamMember(_Advanced_TeamId, _ChangingDefaultTeamsUser_UserId, DateTime.Now.Date.AddDays(-15), null);

                var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First();
                var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Org View)").First();
                var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Person Module (BU View)").First();

                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_ChangingDefaultTeamsUser_UserId, securityProfileId1);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_ChangingDefaultTeamsUser_UserId, securityProfileId2);
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_ChangingDefaultTeamsUser_UserId, securityProfileId3);
            }
            if (_ChangingDefaultTeamsUser_UserId == Guid.Empty)
            {
                _ChangingDefaultTeamsUser_UserId = dbHelper.systemUser.GetSystemUserByUserName("ChangingDefaultTeamsUser").FirstOrDefault();
            }

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_ChangingDefaultTeamsUser_UserId, DateTime.Now.Date);

            #endregion

            #region Ethnicity

            if (!dbHelper.ethnicity.GetEthnicityIdByName("English").Any())
                dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));
            _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];

            #endregion

            #region Person

            firstName = "Automation";
            lastName = _currentDateSuffix;
            _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

            #endregion


        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-5631

        [TestProperty("JiraIssueID", "CDV6-10261")]
        [Description("Login with a user with 'Works in multiple teams' set to No - Validate that the user is redirected to the Home Page after the login")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod001()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!")
                .WaitFormHomePageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-10262")]
        [Description("Login with a user with 'Works in multiple teams' set to No - Wait for the home page to load - Validate that the Default Team is displayed in the main menu area")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod002()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ValidateDefaultTeamVisible()
                .ValidateDefaultTeamText("CareDirector QA");
        }

        [TestProperty("JiraIssueID", "CDV6-10263")]
        [Description("Login with a user with 'Works in multiple teams' set to No - Wait for the home page to load - Click on the 'Change Default Team' button - " +
            "Wait for the 'Change Default Team' popup to load - Click on the select team lookup button - wait for the team lookup popup to load - " +
            "Validate that only the user team is displayed in the popup")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod003()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickToChangeDefaultTeamButton();

            changeDefaultTeamPopup
                .WaitForChangeDefaultTeamPopupToLoad()
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateResultElementPresent(_careDirectorQA_TeamId.ToString())
                .ValidateResultElementNotPresent(_AdultSafeguarding_TeamId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-10264")]
        [Description("Login with a user with 'Works in multiple teams' set to No - Wait for the home page to load - Click on the 'Change Default Team' button - " +
            "Wait for the 'Change Default Team' popup to load - Click on the select team lookup button - wait for the team lookup popup to load - " +
            "Search for a team record the user do not belong to - Validate that no record is displayed to the user")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod004()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickToChangeDefaultTeamButton();

            changeDefaultTeamPopup
                .WaitForChangeDefaultTeamPopupToLoad()
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                //.SelectViewByText("Lookup View")
                .TypeSearchQuery("Adult Safeguarding")
                .TapSearchButton()
                .ValidateResultElementNotPresent(_careDirectorQA_TeamId.ToString())
                .ValidateResultElementNotPresent(_AdultSafeguarding_TeamId.ToString());
        }


        [TestProperty("JiraIssueID", "CDV6-10265")]
        [Description("Login with a user with 'Works in multiple teams' set to Yes - Validate that the user is redirected to the Select Default Team page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod005()
        {
            loginPage
                .GoToLoginPage()
                .Login("MultiTeamAdminUser", "Passw0rd_!");

            teamSelectingPage
                .WaitForTeamSelectingPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-10266")]
        [Description("Login with a user with 'Works in multiple teams' set to Yes - Wait for the Select Default Team page to load - " +
            "Validate that Team picklist only contains the teams that belong to the user")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod006()
        {
            loginPage
                .GoToLoginPage()
                .Login("MultiTeamAdminUser", "Passw0rd_!");

            teamSelectingPage
                .WaitForTeamSelectingPageToLoad()
                .ValidateTeamPicklistContainsElement("CareDirector QA")
                .ValidateTeamPicklistContainsElement("Adult Safeguarding")
                .ValidateTeamPicklistDoesNotContainsElement("Bridgend - Adoption");
        }

        [TestProperty("JiraIssueID", "CDV6-10267")]
        [Description("Login with a user with 'Works in multiple teams' set to Yes - Wait for the Select Default Team page to load - " +
            "Select a default team - click on the Continue button - Wait for the home page to load - " +
            "Validate that the selected team is displayed as the default team in the main menu area")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod007()
        {
            loginPage
                .GoToLoginPage()
                .Login("MultiTeamAdminUser", "Passw0rd_!");

            teamSelectingPage
                .WaitForTeamSelectingPageToLoad()
                .SelectDefaultTeam("Adult Safeguarding")
                .ClickContinueButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ValidateDefaultTeamVisible()
                .ValidateDefaultTeamText("Adult Safeguarding");
        }

        [TestProperty("JiraIssueID", "CDV6-10268")]
        [Description("Login with a user with 'Works in multiple teams' set to Yes - Wait for the Select Default Team page to load - " +
            "Select a default team - click on the Continue button - Wait for the home page to load - " +
            "Open a person record - Navigate to the Person Case Notes page - Tap on the add button - " +
            "Validate that the previous selected team is automatically displayed in the Responsible team field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod008()
        {

            loginPage
                .GoToLoginPage()
                .Login("MultiTeamAdminUser", "Passw0rd_!");

            teamSelectingPage
                .WaitForTeamSelectingPageToLoad()
                .SelectDefaultTeam("Adult Safeguarding")
                .ClickContinueButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(firstName, lastName, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ValidateResponsibleTeamLinkFieldText("Adult Safeguarding");
        }

        [TestProperty("JiraIssueID", "CDV6-10269")]
        [Description("Login with a user with 'Works in multiple teams' set to Yes - Wait for the Select Default Team page to load - " +
            "Select a default team - click on the Continue button - Wait for the home page to load - " +
            "Tap on the 'Change Default Team' button - Select a different team - Validate that the default team text in the main menu area is replaced with the new selected team")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod009()
        {
            loginPage
                .GoToLoginPage()
                .Login("MultiTeamAdminUser", "Passw0rd_!");

            teamSelectingPage
                .WaitForTeamSelectingPageToLoad()
                .SelectDefaultTeam("Adult Safeguarding")
                .ClickContinueButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickToChangeDefaultTeamButton();

            changeDefaultTeamPopup
                .WaitForChangeDefaultTeamPopupToLoad()
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_careDirectorQA_TeamId.ToString());

            changeDefaultTeamPopup
                .WaitForChangeDefaultTeamPopupToReLoad()
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            mainMenu
                .WaitForMainMenuToLoad()
                .ValidateDefaultTeamVisible()
                .ValidateDefaultTeamText("CareDirector QA");
        }

        [TestProperty("JiraIssueID", "CDV6-10270")]
        [Description("Login with a user with 'Works in multiple teams' set to Yes - Wait for the Select Default Team page to load - " +
            "Select a default team - click on the Continue button - Wait for the home page to load - " +
            "Switch the default team of the user again - Navigate to the Person Case Notes page - Tap on the add button - " +
            "Validate that the last selected team is automatically displayed in the Responsible team field")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ShowTheDefaultTeamForTheSystemUser_UITestMethod010()
        {
            loginPage
                .GoToLoginPage()
                .Login("MultiTeamAdminUser", "Passw0rd_!");

            teamSelectingPage
                .WaitForTeamSelectingPageToLoad()
                .SelectDefaultTeam("Adult Safeguarding")
                .ClickContinueButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickToChangeDefaultTeamButton();

            changeDefaultTeamPopup
                .WaitForChangeDefaultTeamPopupToLoad()
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_careDirectorQA_TeamId.ToString());

            changeDefaultTeamPopup
                .WaitForChangeDefaultTeamPopupToReLoad()
                .ClickSaveButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(firstName, lastName, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonCaseNotesPage();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ClickNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("New")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9298

        [TestProperty("JiraIssueID", "CDV6-10271")]
        [Description("Login with a user with 'Works in multiple teams' set to Yes - Wait for the Select Default Team page to load - " +
            "Select a default team - click on the Continue button - Wait for the home page to load - " +
            "Tap on the 'Change Default Team' button - Select a different team - Validate that the default team text in the main menu area is replaced with the new selected team")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void ChangingDefaultTeamsIssue_UITestMethod001()
        {
            loginPage
                .GoToLoginPage()
                .Login("ChangingDefaultTeamsUser", "Passw0rd_!");

            teamSelectingPage
                .WaitForTeamSelectingPageToLoad()
                .SelectDefaultTeam("CareDirector QA")
                .ClickContinueButton();

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .ClickToChangeDefaultTeamButton();

            changeDefaultTeamPopup
                .WaitForChangeDefaultTeamPopupToLoad()
                .ClickTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(_Advanced_TeamId.ToString());

            changeDefaultTeamPopup
                .WaitForChangeDefaultTeamPopupToReLoad()
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .ValidateDefaultTeamVisible()
                .ValidateDefaultTeamText("Advanced");
        }


        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }




    }
}

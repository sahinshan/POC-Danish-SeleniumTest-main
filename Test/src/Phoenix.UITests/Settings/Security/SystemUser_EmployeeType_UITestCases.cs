using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_EmployeeType_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _careProviders_TeamId1;
        private Guid _careProviders_TeamId2;
        private Guid _defaultLoginUserID;
        private Guid _systemUserID;
        private Guid _rosteredUserID_1;
        private Guid _rosteredUserID_2;
        private Guid _rosteredUserID_3;
        private Guid _systemAdministratorUserID_1, _systemAdministratorUserID_2, _systemAdministratorUserID_3;
        private Guid _coreSystemUserID_1, _coreSystemUserID_2, _coreSystemUserID_3;
        private Guid _providerSystemUserID_1, _providerSystemUserID_2, _providerSystemUserID_3;
        public Guid environmentid;
        public string lastname = "_" + DateTime.Now.ToString("yyyyMMddHHmmssFFFFF");
        private string _loginUser_Name;
        private string _loginUser_Username;
        private string _systemUser_Username;
        public string _rostered_SystemUsername1;
        public string _rostered_SystemUsername2;
        public string _rostered_SystemUsername3;
        public string _rostered_SystemUserFullname1;
        public string _rostered_SystemUserFullname2;
        public string _rostered_SystemUserFullname3;
        public string _core_SystemUsername1, _core_SystemUsername2, _core_SystemUsername3;
        public string _core_SystemUserFullname1, _core_SystemUserFullname2, _core_SystemUserFullname3;
        public string _provider_SystemUsername1, _provider_SystemUsername2, _provider_SystemUsername3;
        public string _admin_SystemUsername1, _admin_SystemUsername2, _admin_SystemUsername3;
        public string _provider_SystemUserFullname1, _provider_SystemUserFullname2, _provider_SystemUserFullname3;
        public string _admin_SystemUserFullname1, _admin_SystemUserFullname2, _admin_SystemUserFullname3;
        public string _careProviders_TeamName1;
        public string _careProviders_TeamName2;
        private Guid systemAdministratorSecurityProfileId;
        private Guid systemUserSecureFieldsSecurityProfileId;
        private Guid _personID;
        private Guid _taskID;
        private Guid _ethnicityId;
        private int _personNumber;
        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private void DataInitalization_Method1()
        {

            #region Authentication

            authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion Authentication

            #region Environment Name

            EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
            string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
            dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

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

            #region Security Profiles

            systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
            systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

            #endregion

            #region Create default system user

            _loginUser_Username = "CW_Admin_Test_User_1";
            _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "CW", "Admin_Test_User_1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            _loginUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];

            #endregion  Create default system user

            #region Team A

            _careProviders_TeamId1 = commonMethodsDB.CreateTeam("19763 Team A", _defaultLoginUserID, _careProviders_BusinessUnitId, "90500", "CareProvidersTeamA@careworkstempmail.com", "CareProviders Team A", "020 123465");

            #endregion

            #region Team B

            _careProviders_TeamId2 = commonMethodsDB.CreateTeam("19763 Team B", _defaultLoginUserID, _careProviders_BusinessUnitId, "90600", "CareProvidersTeamB@careworkstempmail.com", "CareProviders Team B", "020 124356");

            #endregion

            #region Ethnicity

            _ethnicityId = commonMethodsDB.CreateEthnicity(_careProviders_TeamId1, "CDV6_19763_Ethnicity", new DateTime(2020, 1, 1));

            #endregion

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19763", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Team Manager

            dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

            #endregion

        }


        /// <summary>
        /// authenticationProvider, 
        /// </summary>
        private void DataInitalization_Method2()
        {

            #region Authentication

            authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion Authentication

            #region Environment Name

            EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
            string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
            dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

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

            #region Security Profiles

            systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
            systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();

            #endregion


            #region Create default system user

            _loginUser_Username = "CW_Admin_Test_User_1";
            _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "CW", "Admin_Test_User_1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            _loginUser_Name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];

            #endregion  Create default system user

            #region Team A

            _careProviders_TeamId1 = commonMethodsDB.CreateTeam("19764 Team A", _defaultLoginUserID, _careProviders_BusinessUnitId, "90401", "CareProvidersTeamA@careworkstempmail.com", "19764 CareProviders Team A", "020 123465");

            #endregion

            #region Team B

            _careProviders_TeamId2 = commonMethodsDB.CreateTeam("19764 Team B", _defaultLoginUserID, _careProviders_BusinessUnitId, "90402", "CareProvidersTeamB@careworkstempmail.com", "19764 CareProviders Team B", "020 124356");

            #endregion

            #region Ethnicity

            _ethnicityId = commonMethodsDB.CreateEthnicity(_careProviders_TeamId1, "English", new DateTime(2020, 1, 1));

            #endregion

        }

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {


            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-19491

        [TestProperty("JiraIssueID", "ACC-3459")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]

        public void SystemUser_EmployeeType_UITestCases001()
        {
            DataInitalization_Method1();

            #region Create system user to Check Security Profile

            var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19491_User").Any();
            if (!newSystemUser)
                _systemUserID = dbHelper.systemUser.CreateSystemUser("CDV6_19491_User", "CDV6_19491", "User", "CDV6_19491 User", "Passw0rd_!", "CDV6_19491_User@somemail.com", "CDV6_19491_User@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, false, 1, null, DateTime.Now.Date);

            if (_systemUserID == Guid.Empty)
                _systemUserID = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19491_User").FirstOrDefault();

            _systemUser_Username = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserID, "username")["username"];

            var systemAdminProfile = "System Administrator";
            var systemAdminSecureProfile = "System User - Secure Fields (Edit)";
            var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName(systemAdminProfile).First();
            var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName(systemAdminSecureProfile).First();

            #endregion  Create system user to Check Security Profile

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserID.ToString());

            // Select 'Provider System User' Employee Type and Verify Security Profile
            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .SelectEmployeeTypes("Provider System User")
                .SelectGender_Options("Male")
                .ClickSaveButton(); System.Threading.Thread.Sleep(5000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .NavigateToSecurityProfilePage();

            securityProfilesPage
                .WaitForSystemUserSecurityProfilesPageToLoad()
                .InsertQuickSearchText(systemAdminSecureProfile)
                .ClickQuickSearchButton()
                .ValidateNoRecordMessageVisibile(true)

                .InsertQuickSearchText(systemAdminProfile)
                .ClickQuickSearchButton()
                .ValidateNoRecordMessageVisibile(true);

            // Select 'System Administrator' Employee Type and Verify Security Profile
            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .SelectEmployeeTypes("System Administrator")
                .InsertAvailableFromDateField(DateTime.Now.AddYears(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton(); System.Threading.Thread.Sleep(5000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToSecurityProfilePage();

            var UserAdministratorSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemUserID, systemAdministratorSecurityProfileId).First();
            var UserSecureFieldsSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemUserID, systemUserSecureFieldsSecurityProfileId).First();

            securityProfilesPage
                .WaitForSystemUserSecurityProfilesPageToLoad()
                .ValidateSecurityProfileCell(UserAdministratorSecurityProfileId.ToString(), systemAdminProfile)
                .ValidateSecurityProfileCell(UserSecureFieldsSecurityProfileId.ToString(), systemAdminSecureProfile);

            securityProfilesPage
                .InsertQuickSearchText(systemAdminSecureProfile)
                .ClickQuickSearchButton()
                .ValidateNoRecordMessageVisibile(false)

                .InsertQuickSearchText(systemAdminProfile)
                .ClickQuickSearchButton()
                .ValidateNoRecordMessageVisibile(false);

            // Select 'Core System User' Employee Type and Verify Security Profile
            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .SelectEmployeeTypes("Core System User")
                .ClickSaveButton(); System.Threading.Thread.Sleep(5000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToSecurityProfilePage();

            securityProfilesPage
                .WaitForSystemUserSecurityProfilesPageToLoad()
                .InsertQuickSearchText(systemAdminSecureProfile)
                .ClickQuickSearchButton()
                .ValidateNoRecordMessageVisibile(true)

                .InsertQuickSearchText(systemAdminProfile)
                .ClickQuickSearchButton()
                .ValidateNoRecordMessageVisibile(true);

            // Select 'Rostered System User' Employee Type and Verify Security Profile
            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .SelectEmployeeTypes("Rostered System User")
                .ClickSaveButton(); System.Threading.Thread.Sleep(5000);

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
                .NavigateToSecurityProfilePage();

            securityProfilesPage
                .WaitForSystemUserSecurityProfilesPageToLoad()
                .InsertQuickSearchText(systemAdminSecureProfile)
                .ClickQuickSearchButton()
                .ValidateNoRecordMessageVisibile(true)

                .InsertQuickSearchText(systemAdminProfile)
                .ClickQuickSearchButton()
                .ValidateNoRecordMessageVisibile(true);

            // Select 'System Administrator' Employee Type and Verify Security Profile
            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .SelectEmployeeTypes("System Administrator")
                .ClickSaveButton(); System.Threading.Thread.Sleep(5000);

            systemUserRecordPage
               .WaitForSystemUserRecordPageToLoad()
                .NavigateToSecurityProfilePage();

            UserAdministratorSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemUserID, systemAdministratorSecurityProfileId).First();
            UserSecureFieldsSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemUserID, systemUserSecureFieldsSecurityProfileId).First();

            securityProfilesPage
                .WaitForSystemUserSecurityProfilesPageToLoad()
                .ValidateSecurityProfileCell(UserAdministratorSecurityProfileId.ToString(), systemAdminProfile)
                .ValidateSecurityProfileCell(UserSecureFieldsSecurityProfileId.ToString(), systemAdminSecureProfile);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19763

        [TestProperty("JiraIssueID", "ACC-3460")]
        [Description("Care Security: Rostered User user cannot view full system record of Rostered User." +
                    "Employee of Type 'Rostered System User' - Can only view own record in Full View." +
                    "Employee of Type 'Core System User' and 'Provider System User' - can only view own record in Full View + Rostered System User if member of the same team.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]

        public void SystemUser_EmployeeType_UITestCases002()
        {
            DataInitalization_Method1();

            #region Create Rostered system user 1 for Team A

            var rosteredSystemUser1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19763_Rostered_User1" + lastname).Any();
            if (!rosteredSystemUser1)
                _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19763_Rostered_User1" + lastname, "CDV6_19763", "Rostered_User1" + lastname, "CDV6_19763 Rostered_User1" + lastname, "Passw0rd_!", "CDV6_19763_RosteredUser1@somemail.com", "CDV6_19763_RosteredUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 3);

            if (_rosteredUserID_1 == Guid.Empty)
                _rosteredUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19763_Rostered_User1" + lastname).FirstOrDefault();

            _rostered_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);
            _rostered_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Rostered system user 2 for Team A

            var rosteredSystemUser2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19763_Rostered_User2" + lastname).Any();
            if (!rosteredSystemUser2)
                _rosteredUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19763_Rostered_User2" + lastname, "CDV6_19763", "Rostered_User2" + lastname, "CDV6_19763 Rostered_User2" + lastname, "Passw0rd_!", "CDV6_19763_RosteredUser2@somemail.com", "CDV6_19763_RosteredUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 3);

            if (_rosteredUserID_2 == Guid.Empty)
                _rosteredUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19763_Rostered_User2" + lastname).FirstOrDefault();

            _rostered_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_2))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_2, DateTime.Now.Date);
            _rostered_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "fullname")["fullname"];
            #endregion

            #region Create Core system user for Team A

            var coreSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19763_Core_User1" + lastname).Any();
            if (!coreSystemUser)
                _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19763_Core_User1" + lastname, "CDV6_19763", "Core_User1" + lastname, "CDV6_19763 Core_User1" + lastname, "Passw0rd_!", "CDV6_19763_CoreUser1@somemail.com", "CDV6_19763_CoreUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 2);

            if (_coreSystemUserID_1 == Guid.Empty)
                _coreSystemUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19763_Core_User1" + lastname).FirstOrDefault();

            _core_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);
            _core_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Provider system user for Team A

            var providerSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19763_Provider_User1" + lastname).Any();
            if (!providerSystemUser)
                _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19763_Provider_User1" + lastname, "CDV6_19763", "Provider_User1" + lastname, "CDV6_19763 Provider_User1" + lastname, "Passw0rd_!", "CDV6_19763_ProviderUser1@somemail.com", "CDV6_19763_ProviderUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 1);

            if (_providerSystemUserID_1 == Guid.Empty)
                _providerSystemUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19763_Provider_User1" + lastname).FirstOrDefault();

            _provider_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_providerSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);
            _provider_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Task
            _taskID = dbHelper.task.CreatePersonTask(_personID, "Johndoe CDV6-19763" + lastname, "Test Test" + lastname, "Task Notes", _careProviders_TeamId1, _coreSystemUserID_1);


            #endregion

            #region Step 1: Employee Type "Rostered User" - Can see own record and rostered user record of same team. Can only view own record in Full View.

            loginPage
              .GoToLoginPage()
              .Login(_rostered_SystemUsername1, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString())
                .OpenRecord(_rosteredUserID_2.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordCannotBeAccessedPageToLoad()
                .ValidateRecordInaccessibleNotificationMessage("The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + _rosteredUserID_2.ToString() + " and Type SystemUser.")
                .CloseNotificationMessage();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_coreSystemUserID_1.ToString());

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_1.ToString());

            #endregion

            #region Step 2: Employee Type "Rostered User" can see own record and rostered user record of same team and can only view own record in System User Lookup field.
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("task");

            personTaskRecordPage
                .ValidateResponsibleUserLinkFieldText(_rostered_SystemUserFullname1)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_2.ToString())
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_coreSystemUserID_1.ToString())
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_1.ToString());

            lookupPopup
                .ClickCloseButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("task")
                .ClickOnCloseIcon();

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("task").ClickOnCloseIcon();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(_taskID.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("task")
                .ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ValidateResponsibleUserLinkFieldText(_core_SystemUserFullname1);

            personTaskRecordPage
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForRecordCannotBeAccessedMessageAreaToLoad()
                .ValidateRecordInaccessibleNotificationMessage("The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + _coreSystemUserID_1.ToString() + " and Type SystemUser.")
                .CloseRecordCannotBeDisplayedNotificationMessage();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_2.ToString())
                .SelectResultElement(_rosteredUserID_2.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickSaveButton()
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ValidateResponsibleUserLinkFieldText(_rostered_SystemUserFullname2)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForRecordCannotBeAccessedMessageAreaToLoad()
                .ValidateRecordInaccessibleNotificationMessage("The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + _rosteredUserID_2.ToString() + " and Type SystemUser.")
                .CloseRecordCannotBeDisplayedNotificationMessage();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            #endregion

            #region Step 3: Employee Type "Core System User" - Can see own record and rostered user record of same team. Can view own record and rostered user record of same team in Full View and System User Lookup view.
            loginPage
              .GoToLoginPage()
              .Login(_core_SystemUsername1, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_coreSystemUserID_1.ToString())
                .OpenRecord(_coreSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_core_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Core_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString())
                .OpenRecord(_rosteredUserID_2.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername2)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User2" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_1.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(_taskID.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("task")
                .ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ValidateResponsibleUserLinkFieldText(_rostered_SystemUserFullname2)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_2.ToString())
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_coreSystemUserID_1.ToString())
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_1.ToString())
                .ClickCloseButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername2)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User2" + lastname)
                .ClickBackButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .SelectResultElement(_rosteredUserID_1.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickSaveButton()
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ValidateResponsibleUserLinkFieldText(_rostered_SystemUserFullname1)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ClickBackButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_coreSystemUserID_1.ToString())
                .SelectResultElement(_coreSystemUserID_1.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickSaveButton()
                .ValidateResponsibleUserLinkFieldText(_core_SystemUserFullname1)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                 .WaitForSystemUserRecordPageToLoad()
                 .NavigateToDetailsPage()
                 .ValidateUserNameFieldValue(_core_SystemUsername1)
                 .ValidateFirstNameFieldValue("CDV6_19763")
                 .ValidateLastNameFieldValue("Core_User1" + lastname)
                 .ClickBackButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickBackButton();

            #endregion

            #region Step 4: Change team of rostered system user. Validate own user is visible, provider user and rostered user are not visible in System User page.
            dbHelper.teamMember.CreateTeamMember(_careProviders_TeamId2, _rosteredUserID_1, new DateTime(2020, 1, 1), null);//Link the user to the new team
            dbHelper.teamMember.CreateTeamMember(_careProviders_TeamId2, _rosteredUserID_2, new DateTime(2020, 1, 1), null);
            dbHelper.systemUser.UpdateDefaultTeam(_rosteredUserID_1, _careProviders_TeamId2);
            dbHelper.systemUser.UpdateDefaultTeam(_rosteredUserID_2, _careProviders_TeamId2);
            var teamMemberId = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_rosteredUserID_1, _careProviders_TeamId1).FirstOrDefault();
            dbHelper.teamMember.DeleteTeamMember(teamMemberId); //remove the old team from the user
            teamMemberId = dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_rosteredUserID_2, _careProviders_TeamId1).FirstOrDefault();
            dbHelper.teamMember.DeleteTeamMember(teamMemberId); //remove the old team from the user


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickSearchButton()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_coreSystemUserID_1.ToString())
                .OpenRecord(_coreSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_core_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Core_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickSearchButton()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_rosteredUserID_1.ToString())
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_rosteredUserID_2.ToString())
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_1.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(_taskID.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("task")
                .ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ValidateResponsibleUserLinkFieldText(_core_SystemUserFullname1)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_rosteredUserID_1.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_rosteredUserID_2.ToString())
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_coreSystemUserID_1.ToString())
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_1.ToString())
                .ClickCloseButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_core_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Core_User1" + lastname)
                .ClickBackButton();

            #endregion

            #region Step 5: Change team of Core system user. Validate own user is visible, provider user is not visible, rostered user is visible in System User page.
            dbHelper.teamMember.CreateTeamMember(_careProviders_TeamId2, _coreSystemUserID_1, new DateTime(2020, 1, 1), null);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickSearchButton()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_coreSystemUserID_1.ToString())
                .OpenRecord(_coreSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_core_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Core_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickSearchButton()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickSearchButton()
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString())
                .OpenRecord(_rosteredUserID_2.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername2)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User2" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickSearchButton()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_1.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(_taskID.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("task")
                .ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ValidateResponsibleUserLinkFieldText(_core_SystemUserFullname1)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_2.ToString())
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_coreSystemUserID_1.ToString())
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_1.ToString())
                .ClickCloseButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_core_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Core_User1" + lastname)
                .ClickBackButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .SelectResultElement(_rosteredUserID_1.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickSaveButton()
                .ValidateResponsibleUserLinkFieldText(_rostered_SystemUserFullname1)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ClickBackButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_1.ToString())
                .ClickCloseButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_2.ToString())
                .SelectResultElement(_rosteredUserID_2.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickSaveButton()
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername2)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User2" + lastname)
                .ClickBackButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            #endregion

            #region Step 6: Employee Type "Provider System User" - Can see own record and rostered user record of same team. Can view own record and rostered user record of same team in Full View and System User Lookup view.

            loginPage
              .GoToLoginPage()
              .Login(_provider_SystemUsername1, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_1.ToString())
                .OpenRecord(_providerSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_provider_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Provider_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_rosteredUserID_1.ToString());

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_rosteredUserID_2.ToString());

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_coreSystemUserID_1.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(_taskID.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("task")
                .ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ValidateResponsibleUserLinkFieldText(_rostered_SystemUserFullname2)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForRecordCannotBeAccessedMessageAreaToLoad()
                .ValidateRecordInaccessibleNotificationMessage("The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + _rosteredUserID_2.ToString() + " and Type SystemUser.")
                .CloseRecordCannotBeDisplayedNotificationMessage();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_rosteredUserID_1.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_rosteredUserID_2.ToString())
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_coreSystemUserID_1.ToString())
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_providerSystemUserID_1.ToString())
                .SelectResultElement(_providerSystemUserID_1.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_provider_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Provider_User1" + lastname)
                .ClickBackButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickSaveButton();

            #endregion

            #region Step 7: Change team of Provider system user. Validate own user is visible, core user is not visible, rostered user is visible in system user page.

            dbHelper.teamMember.CreateTeamMember(_careProviders_TeamId2, _providerSystemUserID_1, new DateTime(2020, 1, 1), null);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickSearchButton()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_1.ToString())
                .OpenRecord(_providerSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_provider_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Provider_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString())
                .OpenRecord(_rosteredUserID_2.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername2)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User2" + lastname)
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_coreSystemUserID_1.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad()
                .NavigateToTasksPage();

            personTasksPage
                .WaitForPersonTasksPageToLoad()
                .OpenPersonTaskRecord(_taskID.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("task")
                .ClickOnExpandIcon();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ValidateResponsibleUserLinkFieldText(_provider_SystemUserFullname1)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_2.ToString())
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_coreSystemUserID_1.ToString())
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_providerSystemUserID_1.ToString())
                .ClickCloseButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_provider_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Provider_User1" + lastname)
                .ClickBackButton();

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .SelectResultElement(_rosteredUserID_1.ToString());

            personTaskRecordPage
                .WaitForPersonTaskRecordPageToLoad("Test Test" + lastname)
                .ClickSaveButton()
                .ValidateResponsibleUserLinkFieldText(_rostered_SystemUserFullname1)
                .ClickResponsibleUserLinkFieldText();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19763")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ClickBackButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19761

        [TestProperty("JiraIssueID", "ACC-3461")]
        [Description("Login as Core System User and Verify other core and rostered user not ablt to see records")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "Core Users")]
        [TestProperty("Screen3", "Provider Users")]
        [TestProperty("Screen4", "Rostered Users")]
        public void SystemUser_EmployeeType_UITestCases003()
        {
            DataInitalization_Method1();

            lastname = "_19710_User" + DateTime.Now.ToString("yyyyMMddHHmmssFFFFF");

            #region Team A

            var teamAExist = dbHelper.team.GetTeamIdByName("19761 Team A").Any();
            if (!teamAExist)
                dbHelper.team.CreateTeam("19761 Team A", _defaultLoginUserID, _careProviders_BusinessUnitId, "90500", "CareProvidersTeamA@careworkstempmail.com", "CareProviders Team A", "020 123465");
            _careProviders_TeamId1 = dbHelper.team.GetTeamIdByName("19761 Team A")[0];

            #endregion

            #region Team B

            var teamBExist = dbHelper.team.GetTeamIdByName("19761 Team B").Any();
            if (!teamBExist)
                dbHelper.team.CreateTeam("19761 Team B", _defaultLoginUserID, _careProviders_BusinessUnitId, "90600", "CareProvidersTeamB@careworkstempmail.com", "CareProviders Team B", "020 124356");
            _careProviders_TeamId2 = dbHelper.team.GetTeamIdByName("19761 Team B")[0];

            #endregion

            #region Create Core system user 1 for Team A

            var coreSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User1" + lastname).Any();
            if (!coreSystemUser)
                _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Core_User1" + lastname, "CDV6_19761", "Core_User1" + lastname, "CDV6_19761 Core_User1" + lastname, "Passw0rd_!", "CDV6_19761_CoreUser1@somemail.com", "CDV6_19761_CoreUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 2, null, DateTime.Now.Date);

            if (_coreSystemUserID_1 == Guid.Empty)
                _coreSystemUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User1" + lastname).FirstOrDefault();

            _core_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "username")["username"];

            int employeeTypeId = (int)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "employeetypeid")["employeetypeid"];
            if (employeeTypeId != 2)
                dbHelper.systemUser.UpdateEmployeeTypeId(_coreSystemUserID_1, 2);

            var AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_coreSystemUserID_1, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);
            _core_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "fullname")["fullname"];

            #endregion

            #region Create Core system user 2 for Team A

            coreSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User2" + lastname).Any();
            if (!coreSystemUser)
                _coreSystemUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Core_User2" + lastname, "CDV6_19761", "Core_User2" + lastname, "CDV6_19761 Core_User2" + lastname, "Passw0rd_!", "CDV6_19761_CoreUser2@somemail.com", "CDV6_19761_CoreUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 2, null, DateTime.Now.Date);

            if (_coreSystemUserID_2 == Guid.Empty)
                _coreSystemUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User2" + lastname).FirstOrDefault();

            _core_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_2, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_coreSystemUserID_2, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_2, DateTime.Now.Date);
            _core_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_2, "fullname")["fullname"];

            #endregion

            #region Create Core system user 3 for Team B

            coreSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User3" + lastname).Any();
            if (!coreSystemUser)
                _coreSystemUserID_3 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Core_User3" + lastname, "CDV6_19761", "Core_User3" + lastname, "CDV6_19761 Core_User3" + lastname, "Passw0rd_!", "CDV6_19761_CoreUser3@somemail.com", "CDV6_19761_CoreUser3@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 2, null, DateTime.Now.Date);

            if (_coreSystemUserID_3 == Guid.Empty)
                _coreSystemUserID_3 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User3" + lastname).FirstOrDefault();

            _core_SystemUsername3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_3, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_coreSystemUserID_3, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_3, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_3, DateTime.Now.Date);
            _core_SystemUserFullname3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_3, "fullname")["fullname"];

            #endregion

            #region Create Rostered system user 1 for Team A

            var rosteredSystemUser1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Rostered_User1" + lastname).Any();
            if (!rosteredSystemUser1)
                _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Rostered_User1" + lastname, "CDV6_19761", "Rostered_User1" + lastname, "CDV6_19761 Rostered_User1" + lastname, "Passw0rd_!", "CDV6_19761_RosteredUser1@somemail.com", "CDV6_19761_RosteredUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 3, null, DateTime.Now.Date);

            if (_rosteredUserID_1 == Guid.Empty)
                _rosteredUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Rostered_User1" + lastname).FirstOrDefault();

            _rostered_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "username")["username"];

            employeeTypeId = (int)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "employeetypeid")["employeetypeid"];
            if (employeeTypeId != 3)
                dbHelper.systemUser.UpdateEmployeeTypeId(_rosteredUserID_1, 3);

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_rosteredUserID_1, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);
            _rostered_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "fullname")["fullname"];

            #endregion

            #region Create Rostered system user 2 for Team B

            var rosteredSystemUser2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Rostered_User2" + lastname).Any();
            if (!rosteredSystemUser2)
                _rosteredUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Rostered_User2" + lastname, "CDV6_19761", "Rostered_User2" + lastname, "CDV6_19761 Rostered_User2" + lastname, "Passw0rd_!", "CDV6_19761_RosteredUser2@somemail.com", "CDV6_19761_RosteredUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 3, null, DateTime.Now.Date);

            if (_rosteredUserID_2 == Guid.Empty)
                _rosteredUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Rostered_User2" + lastname).FirstOrDefault();

            _rostered_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_rosteredUserID_2, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_2, DateTime.Now.Date);
            _rostered_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "fullname")["fullname"];

            #endregion

            #region Create Provider system user 1 for Team A

            var providerSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User1" + lastname).Any();
            if (!providerSystemUser)
                _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Provider_User1" + lastname, "CDV6_19761", "Provider_User1" + lastname, "CDV6_19761 Provider_User1" + lastname, "Passw0rd_!", "CDV6_19761_ProviderUser1@somemail.com", "CDV6_19761_ProviderUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, true, 1, null, DateTime.Now.Date);

            if (_providerSystemUserID_1 == Guid.Empty)
                _providerSystemUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User1" + lastname).FirstOrDefault();

            _provider_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_providerSystemUserID_1, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);
            _provider_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "fullname")["fullname"];

            #endregion

            #region Create Provider system user 2 for Team B

            providerSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User2" + lastname).Any();
            if (!providerSystemUser)
                _providerSystemUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Provider_User2" + lastname, "CDV6_19761", "Provider_User2" + lastname, "CDV6_19761 Provider_User2" + lastname, "Passw0rd_!", "CDV6_19761_ProviderUser2@somemail.com", "CDV6_19761_ProviderUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, true, 1, null, DateTime.Now.Date);

            if (_providerSystemUserID_2 == Guid.Empty)
                _providerSystemUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User2" + lastname).FirstOrDefault();

            _provider_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_2, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_providerSystemUserID_2, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_2, DateTime.Now.Date);
            _provider_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_2, "fullname")["fullname"];

            #endregion

            #region Create System Administrator user 1 for Team A

            var systemAdminLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Admin_User1" + lastname).Any();
            if (!systemAdminLoginUserExists)
            {
                _systemAdministratorUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Admin_User1" + lastname, "CDV6_19761", "Admin_User1" + lastname, "CDV6_19761 admin_User1" + lastname, "Passw0rd_!", "CDV6_19761_AdminUser1@somemail.com", "CDV6_19761_AdminUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, true, 4, null, DateTime.Now.Date);

                var AdminSecureField1 = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_1, systemAdministratorSecurityProfileId).Any();
                if (AdminSecureField1 == false)
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, systemAdministratorSecurityProfileId);

                var AdminSecureField2 = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_1, systemUserSecureFieldsSecurityProfileId).Any();
                if (AdminSecureField2 == false)
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, systemUserSecureFieldsSecurityProfileId);
            }

            if (Guid.Empty == _systemAdministratorUserID_1)
                _systemAdministratorUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Admin_User1" + lastname).FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_1, DateTime.Now.Date);
            _admin_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_1, "username")["username"];
            _admin_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_1, "fullname")["fullname"];

            #endregion

            #region Create System Administrator user 2 for Team B

            systemAdminLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Admin_User2" + lastname).Any();
            if (!systemAdminLoginUserExists)
            {
                _systemAdministratorUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Admin_User2" + lastname, "CDV6_19761", "Admin_User2" + lastname, "CDV6_19761 admin_User2" + lastname, "Passw0rd_!", "CDV6_19761_AdminUser2@somemail.com", "CDV6_19761_AdminUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, true, 4, null, DateTime.Now.Date);

                var AdminSecureField1 = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_2, systemAdministratorSecurityProfileId).Any();
                if (AdminSecureField1 == false)
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_2, systemAdministratorSecurityProfileId);

                var AdminSecureField2 = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_2, systemUserSecureFieldsSecurityProfileId).Any();
                if (AdminSecureField2 == false)
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_2, systemUserSecureFieldsSecurityProfileId);
            }

            if (Guid.Empty == _systemAdministratorUserID_2)
                _systemAdministratorUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Admin_User2" + lastname).FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_2, DateTime.Now.Date);
            _admin_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_2, "username")["username"];
            _admin_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_2, "fullname")["fullname"];

            #endregion

            #region Create Task
            _taskID = dbHelper.task.CreatePersonTask(_personID, "Johndoe CDV6-19761" + lastname, "Test Test" + lastname, "Task Notes", _careProviders_TeamId1, _coreSystemUserID_1);


            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_core_SystemUsername1, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_coreSystemUserID_1.ToString());

            #endregion

            #region Step 3 & 4

            systemUsersPage
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_core_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_coreSystemUserID_2.ToString())

                .InsertUserName(_core_SystemUsername3)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_coreSystemUserID_3.ToString());

            #endregion

            #region Step 5 & 6

            systemUsersPage
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())

                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_rosteredUserID_2.ToString());

            #endregion

            #region Step 7

            systemUsersPage
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_1.ToString())

                .InsertUserName(_admin_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_systemAdministratorUserID_1.ToString());

            #endregion

            #region Step 8 & 9

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCoreUsersSection();

            coreUsersPage
                .WaitForCoreUsersPageToLoad()
                .ValidateRecordIsPresent(_coreSystemUserID_1.ToString());

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickRegardinguserLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_core_SystemUsername1.ToString())
                .TapSearchButton()
                .ValidateResultElementPresent(_coreSystemUserID_1.ToString())

                .TypeSearchQuery(_rostered_SystemUsername1.ToString())
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString());

            #endregion

            #region Step 11

            lookupPopup
                .TypeSearchQuery(_core_SystemUsername3.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_coreSystemUserID_3.ToString())

                .TypeSearchQuery(_rostered_SystemUsername2.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_rosteredUserID_2.ToString());

            #endregion

            #region Step 12

            lookupPopup
                .TypeSearchQuery(_provider_SystemUsername1.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_1.ToString())

                .TypeSearchQuery(_admin_SystemUsername1.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_systemAdministratorUserID_1.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 13

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            #endregion

            #region Step 14

            systemUserRecordPage
                .InsertLastName("User")
                .ClickSaveButton()
                .ValidateLastNameFieldValue("User");

            #endregion

            #region Step 15

            systemUserRecordPage
                .SelectEmployeeTypes("Core System User")
                .ClickSaveButton(); System.Threading.Thread.Sleep(5000);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Only System Administrators and Local System Administrators can create or update user with employee type of Core System User.")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3462")]
        [Description("Login as Provider System User -> ")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "Core Users")]
        [TestProperty("Screen3", "Provider Users")]
        [TestProperty("Screen4", "Rostered Users")]
        public void SystemUser_EmployeeType_UITestCases004()
        {
            DataInitalization_Method1();

            lastname = "_19714_User" + DateTime.Now.ToString("yyyyMMddHHmmssFFFFF");

            #region Team A

            var teamAExist = dbHelper.team.GetTeamIdByName("19761 Team A").Any();
            if (!teamAExist)
                dbHelper.team.CreateTeam("19761 Team A", _defaultLoginUserID, _careProviders_BusinessUnitId, "90500", "CareProvidersTeamA@careworkstempmail.com", "CareProviders Team A", "020 123465");
            _careProviders_TeamId1 = dbHelper.team.GetTeamIdByName("19761 Team A")[0];

            #endregion

            #region Team B

            var teamBExist = dbHelper.team.GetTeamIdByName("19761 Team B").Any();
            if (!teamBExist)
                dbHelper.team.CreateTeam("19761 Team B", _defaultLoginUserID, _careProviders_BusinessUnitId, "90600", "CareProvidersTeamB@careworkstempmail.com", "CareProviders Team B", "020 124356");
            _careProviders_TeamId2 = dbHelper.team.GetTeamIdByName("19761 Team B")[0];

            #endregion

            #region Create Provider system user 1 for Team A

            var providerSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User1" + lastname).Any();
            if (!providerSystemUser)
                _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Provider_User1" + lastname, "CDV6_19761", "Provider_User1" + lastname, "CDV6_19761 Provider_User1" + lastname, "Passw0rd_!", "CDV6_19761_ProviderUser1@somemail.com", "CDV6_19761_ProviderUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, true, 1, null, DateTime.Now.Date);

            if (_providerSystemUserID_1 == Guid.Empty)
                _providerSystemUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User1" + lastname).FirstOrDefault();

            _provider_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "username")["username"];

            int employeeTypeId = (int)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "employeetypeid")["employeetypeid"];
            if (employeeTypeId != 1)
                dbHelper.systemUser.UpdateEmployeeTypeId(_providerSystemUserID_1, 1);

            var AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_providerSystemUserID_1, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);
            _provider_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "fullname")["fullname"];

            #endregion

            #region Create Provider system user 2 for Team A

            providerSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User2" + lastname).Any();
            if (!providerSystemUser)
                _providerSystemUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Provider_User2" + lastname, "CDV6_19761", "Provider_User2" + lastname, "CDV6_19761 Provider_User2" + lastname, "Passw0rd_!", "CDV6_19761_ProviderUser2@somemail.com", "CDV6_19761_ProviderUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, true, 1, null, DateTime.Now.Date);

            if (_providerSystemUserID_2 == Guid.Empty)
                _providerSystemUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User2" + lastname).FirstOrDefault();

            _provider_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_2, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_providerSystemUserID_2, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_2, DateTime.Now.Date);
            _provider_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_2, "fullname")["fullname"];

            #endregion

            #region Create Provider system user 3 for Team B

            providerSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User3" + lastname).Any();
            if (!providerSystemUser)
                _providerSystemUserID_3 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Provider_User3" + lastname, "CDV6_19761", "Provider_User3" + lastname, "CDV6_19761 Provider_User3" + lastname, "Passw0rd_!", "CDV6_19761_ProviderUser3@somemail.com", "CDV6_19761_ProviderUser3@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, true, 1, null, DateTime.Now.Date);

            if (_providerSystemUserID_3 == Guid.Empty)
                _providerSystemUserID_3 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Provider_User3" + lastname).FirstOrDefault();

            _provider_SystemUsername3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_3, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_providerSystemUserID_3, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_3, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_3, DateTime.Now.Date);
            _provider_SystemUserFullname3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_3, "fullname")["fullname"];

            #endregion

            #region Create Core system user 1 for Team A

            var coreSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User1" + lastname).Any();
            if (!coreSystemUser)
                _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Core_User1" + lastname, "CDV6_19761", "Core_User1" + lastname, "CDV6_19761 Core_User1" + lastname, "Passw0rd_!", "CDV6_19761_CoreUser1@somemail.com", "CDV6_19761_CoreUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 2, null, DateTime.Now.Date);

            if (_coreSystemUserID_1 == Guid.Empty)
                _coreSystemUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User1" + lastname).FirstOrDefault();

            _core_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "username")["username"];

            employeeTypeId = (int)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "employeetypeid")["employeetypeid"];
            if (employeeTypeId != 2)
                dbHelper.systemUser.UpdateEmployeeTypeId(_coreSystemUserID_1, 2);

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_coreSystemUserID_1, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);
            _core_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "fullname")["fullname"];

            #endregion

            #region Create Core system user 2 for Team A

            coreSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User2" + lastname).Any();
            if (!coreSystemUser)
                _coreSystemUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Core_User2" + lastname, "CDV6_19761", "Core_User2" + lastname, "CDV6_19761 Core_User2" + lastname, "Passw0rd_!", "CDV6_19761_CoreUser2@somemail.com", "CDV6_19761_CoreUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 2, null, DateTime.Now.Date);

            if (_coreSystemUserID_2 == Guid.Empty)
                _coreSystemUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Core_User2" + lastname).FirstOrDefault();

            _core_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_2, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_coreSystemUserID_2, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_2, DateTime.Now.Date);
            _core_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_2, "fullname")["fullname"];

            #endregion

            #region Create Rostered system user 1 for Team A

            var rosteredSystemUser1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Rostered_User1" + lastname).Any();
            if (!rosteredSystemUser1)
                _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Rostered_User1" + lastname, "CDV6_19761", "Rostered_User1" + lastname, "CDV6_19761 Rostered_User1" + lastname, "Passw0rd_!", "CDV6_19761_RosteredUser1@somemail.com", "CDV6_19761_RosteredUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 3, null, DateTime.Now.Date);

            if (_rosteredUserID_1 == Guid.Empty)
                _rosteredUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Rostered_User1" + lastname).FirstOrDefault();

            _rostered_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "username")["username"];

            employeeTypeId = (int)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "employeetypeid")["employeetypeid"];
            if (employeeTypeId != 3)
                dbHelper.systemUser.UpdateEmployeeTypeId(_rosteredUserID_1, 3);

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_rosteredUserID_1, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);
            _rostered_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "fullname")["fullname"];

            #endregion

            #region Create Rostered system user 2 for Team B

            var rosteredSystemUser2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Rostered_User2" + lastname).Any();
            if (!rosteredSystemUser2)
                _rosteredUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Rostered_User2" + lastname, "CDV6_19761", "Rostered_User2" + lastname, "CDV6_19761 Rostered_User2" + lastname, "Passw0rd_!", "CDV6_19761_RosteredUser2@somemail.com", "CDV6_19761_RosteredUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 3, null, DateTime.Now.Date);

            if (_rosteredUserID_2 == Guid.Empty)
                _rosteredUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Rostered_User2" + lastname).FirstOrDefault();

            _rostered_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "username")["username"];

            AdminSecureField = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_rosteredUserID_2, systemAdministratorSecurityProfileId).Any();
            if (AdminSecureField == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_2, DateTime.Now.Date);
            _rostered_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "fullname")["fullname"];

            #endregion

            #region Create System Administrator user 1 for Team A

            var systemAdminLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Admin_User1" + lastname).Any();
            if (!systemAdminLoginUserExists)
            {
                _systemAdministratorUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Admin_User1" + lastname, "CDV6_19761", "Admin_User1" + lastname, "CDV6_19761 admin_User1" + lastname, "Passw0rd_!", "CDV6_19761_AdminUser1@somemail.com", "CDV6_19761_AdminUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, true, 4, null, DateTime.Now.Date);

                var AdminSecureField1 = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_1, systemAdministratorSecurityProfileId).Any();
                if (AdminSecureField1 == false)
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, systemAdministratorSecurityProfileId);

                var AdminSecureField2 = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_1, systemUserSecureFieldsSecurityProfileId).Any();
                if (AdminSecureField2 == false)
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, systemUserSecureFieldsSecurityProfileId);
            }

            if (Guid.Empty == _systemAdministratorUserID_1)
                _systemAdministratorUserID_1 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Admin_User1" + lastname).FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_1, DateTime.Now.Date);
            _admin_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_1, "username")["username"];
            _admin_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_1, "fullname")["fullname"];

            #endregion

            #region Create System Administrator user 2 for Team B

            systemAdminLoginUserExists = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Admin_User2" + lastname).Any();
            if (!systemAdminLoginUserExists)
            {
                _systemAdministratorUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19761_Admin_User2" + lastname, "CDV6_19761", "Admin_User2" + lastname, "CDV6_19761 admin_User2" + lastname, "Passw0rd_!", "CDV6_19761_AdminUser2@somemail.com", "CDV6_19761_AdminUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, true, 4, null, DateTime.Now.Date);

                var AdminSecureField1 = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_2, systemAdministratorSecurityProfileId).Any();
                if (AdminSecureField1 == false)
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_2, systemAdministratorSecurityProfileId);

                var AdminSecureField2 = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_2, systemUserSecureFieldsSecurityProfileId).Any();
                if (AdminSecureField2 == false)
                    dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_2, systemUserSecureFieldsSecurityProfileId);
            }

            if (Guid.Empty == _systemAdministratorUserID_2)
                _systemAdministratorUserID_2 = dbHelper.systemUser.GetSystemUserByUserName("CDV6_19761_Admin_User2" + lastname).FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_2, DateTime.Now.Date);
            _admin_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_2, "username")["username"];
            _admin_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_2, "fullname")["fullname"];

            #endregion

            #region Create Task
            _taskID = dbHelper.task.CreatePersonTask(_personID, "Johndoe CDV6-19761" + lastname, "Test Test" + lastname, "Task Notes", _careProviders_TeamId1, _coreSystemUserID_1);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_provider_SystemUsername1, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_1.ToString());

            #endregion

            #region Step 3

            systemUsersPage
                .InsertUserName(_provider_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_2.ToString());

            #endregion

            #region Step 4

            systemUsersPage
                .InsertUserName(_provider_SystemUsername3)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_3.ToString());

            #endregion

            #region Step 5

            systemUsersPage
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_coreSystemUserID_1.ToString());

            #endregion

            #region Step 6

            systemUsersPage
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString());

            #endregion

            #region Step 7

            systemUsersPage
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_rosteredUserID_2.ToString());

            #endregion

            #region Step 8

            systemUsersPage
                .InsertUserName(_admin_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_systemAdministratorUserID_1.ToString());

            #endregion

            #region Step 9 & 10

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSecuritySubLink()
                .ValidateCoreUsersMenuLink(true)
                .NavigateToCoreUsersSection();

            coreUsersPage
                .WaitForCoreUsersPageToLoad()
                .ValidateNoRecordsFoundText();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderUsersSection();

            providerUsersPage
                .WaitForProviderUsersPageToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_1.ToString());

            #endregion

            #region Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickRegardinguserLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_provider_SystemUsername1.ToString())
                .TapSearchButton()
                .ValidateResultElementPresent(_providerSystemUserID_1.ToString())

                .TypeSearchQuery(_rostered_SystemUsername1.ToString())
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString());

            #endregion

            #region Step 12

            lookupPopup
                .TypeSearchQuery(_provider_SystemUsername2.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_2.ToString())

                .TypeSearchQuery(_provider_SystemUsername3.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_3.ToString())

                .TypeSearchQuery(_rostered_SystemUsername2.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_rosteredUserID_2.ToString());

            #endregion

            #region Step 13

            lookupPopup
                .TypeSearchQuery(_core_SystemUsername1.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_coreSystemUserID_1.ToString())

                .TypeSearchQuery(_admin_SystemUsername1.ToString())
                .TapSearchButton()
                .ValidateResultElementNotPresent(_systemAdministratorUserID_1.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 14

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            #endregion

            #region Step 15

            systemUserRecordPage
                .InsertMiddleName("User")
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.")
                .TapOKButton();

            #endregion

            #region Step 16

            dbHelper.systemUser.UpdateEmployeeTypeId(_rosteredUserID_1, 1);

            var UserSecureFieldsSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_rosteredUserID_1, systemAdministratorSecurityProfileId).Any();
            if (UserSecureFieldsSecurityProfileId == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, systemAdministratorSecurityProfileId);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_rosteredUserID_1.ToString());

            dbHelper.systemUser.UpdateEmployeeTypeId(_rosteredUserID_1, 3);

            #endregion

            #region Step 17

            dbHelper.systemUser.UpdateEmployeeTypeId(_providerSystemUserID_1, 3);

            UserSecureFieldsSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_providerSystemUserID_1, systemAdministratorSecurityProfileId).Any();
            if (UserSecureFieldsSecurityProfileId == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, systemAdministratorSecurityProfileId);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_1.ToString())
                .OpenRecord(_providerSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSelectedEmployeeTypeValue("Rostered System User");

            #endregion

            #region Step 18

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordCannotBeAccessedPageToLoad()
                .ValidateRecordInaccessibleNotificationMessage("The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + _rosteredUserID_1.ToString() + " and Type SystemUser.")
                .CloseNotificationMessage();

            #endregion

            #region Step 19

            dbHelper.systemUser.UpdateEmployeeTypeId(_providerSystemUserID_1, 1);

            UserSecureFieldsSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_providerSystemUserID_1, systemAdministratorSecurityProfileId).Any();
            if (UserSecureFieldsSecurityProfileId == false)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, systemAdministratorSecurityProfileId);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_1.ToString())
                .OpenRecord(_providerSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSelectedEmployeeTypeValue("Provider System User");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3463")]
        [Description("Care Security: If logged in user employee type is “Rostered system user” then “Provider System User “ ,”Admin” and  ”Core System User” are never shown but other “Rostered system user” are shown")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "Core Users")]
        [TestProperty("Screen3", "Provider Users")]
        [TestProperty("Screen4", "Rostered Users")]
        public void SystemUser_EmployeeType_UITestCases005()
        {
            DataInitalization_Method1();

            #region Create Rostered system user 1 for Team A

            _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Rostered_User1" + lastname, "CDV6_19715", "Rostered_User1" + lastname, "CDV6_19715 Rostered_User1" + lastname, "Passw0rd_!", "CDV6_19715_RosteredUser1@somemail.com", "CDV6_19715_RosteredUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 3);
            _rostered_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "username")["username"];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);
            _rostered_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Rostered system user 2 for Team A

            _rosteredUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Rostered_User2" + lastname, "CDV6_19715", "Rostered_User2" + lastname, "CDV6_19715Rostered_User2" + lastname, "Passw0rd_!", "CDV6_19715_RosteredUser2@somemail.com", "CDV6_19715_RosteredUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 3);
            _rostered_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "username")["username"];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_2, DateTime.Now.Date);
            _rostered_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "fullname")["fullname"];
            #endregion

            #region Create Rostered system user 3 for Team B

            _rosteredUserID_3 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Rostered_User3" + lastname, "CDV6_19715", "Rostered_User3" + lastname, "CDV6_19715Rostered_User3" + lastname, "Passw0rd_!", "CDV6_19715_RosteredUser3@somemail.com", "CDV6_19715_RosteredUser3@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 3);
            _rostered_SystemUsername3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_3, "username")["username"];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_3, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_3, DateTime.Now.Date);
            _rostered_SystemUserFullname3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "fullname")["fullname"];
            #endregion

            #region Create Core system user 1 for Team A

            _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Core_User1" + lastname, "CDV6_19715", "Core_User1" + lastname, "CDV6_19715 Core_User1" + lastname, "Passw0rd_!", "CDV6_19715_CoreUser1@somemail.com", "CDV6_19715_CoreUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 2);
            _core_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);
            _core_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Core system user 2 for Team B

            _coreSystemUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Core_User2" + lastname, "CDV6_19715", "Core_User2" + lastname, "CDV6_19715 Core_User2" + lastname, "Passw0rd_!", "CDV6_19715_CoreUser2@somemail.com", "CDV6_19715_CoreUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 2);
            _core_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_2, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_2))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_2, DateTime.Now.Date);
            _core_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_2, "fullname")["fullname"];
            #endregion

            #region Create Provider system user 1 for Team A

            _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Provider_User1" + lastname, "CDV6_19715", "Provider_User1" + lastname, "CDV6_19715 Provider_User1" + lastname, "Passw0rd_!", "CDV6_19715_ProviderUser1@somemail.com", "CDV6_19715_ProviderUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1);
            _provider_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_providerSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);
            _provider_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Provider system user 2 for Team B

            _providerSystemUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Provider_User2" + lastname, "CDV6_19715", "Provider_User2" + lastname, "CDV6_19715 Provider_User2" + lastname, "Passw0rd_!", "CDV6_19715_ProviderUser2@somemail.com", "CDV6_19715_ProviderUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2);
            _provider_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_2, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_providerSystemUserID_2))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_2, DateTime.Now.Date);
            _provider_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_2, "fullname")["fullname"];
            #endregion

            #region Create Admin system user 1 for Team A

            _systemAdministratorUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Admin1" + lastname, "CDV6_19715", "Admin1" + lastname, "CDV6_19715 Admin1" + lastname, "Passw0rd_!", "CDV6_19715_Admin1@somemail.com", "CDV6_19715_Admin1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 4);
            _admin_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_1, "username")["username"];

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_1, DateTime.Now.Date);
            _admin_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Admin system user 2 for Team B

            _systemAdministratorUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19715_Admin2" + lastname, "CDV6_19715", "Admin2" + lastname, "CDV6_19715 Admin2" + lastname, "Passw0rd_!", "CDV6_19715_Admin2@somemail.com", "CDV6_19715_Admin2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 4);
            _admin_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_2, "username")["username"];

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_2, DateTime.Now.Date);
            _admin_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_2, "fullname")["fullname"];
            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_rostered_SystemUsername1, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19715")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ValidateSystemUserRecordTitle(_rostered_SystemUserFullname1)
                .ClickBackButton();

            #endregion

            #region Step 3
            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString())
                .OpenRecord(_rosteredUserID_2.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordCannotBeAccessedPageToLoad()
                .ValidateRecordInaccessibleNotificationMessage("The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + _rosteredUserID_2.ToString() + " and Type SystemUser.")
                .CloseNotificationMessage();

            #endregion

            #region Step 4

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_rostered_SystemUsername3)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_rosteredUserID_3.ToString());

            #endregion

            #region Step 5

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_coreSystemUserID_1.ToString());

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_1.ToString());

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_admin_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_systemAdministratorUserID_1.ToString());

            #endregion

            #region Step 6

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_core_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_coreSystemUserID_2.ToString());

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_provider_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_providerSystemUserID_2.ToString());

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_admin_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordNotPresent(_systemAdministratorUserID_2.ToString());

            #endregion

            #region Step 7
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSecuritySubLink()
                .ValidateCoreUsersMenuLink(true)
                .ValidateProviderUsersMenuLink(true)
                .ValidateRosteredUsersMenuLink(true);

            #endregion

            #region Step 8

            mainMenu
                .NavigateToRosteredUsersSection();

            rosteredUsersPage
                .WaitForRosteredUsersPageToLoad()
                .ClickSortByCreatedOnDescendingOrder()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString())
                .ValidateRecordNotPresent(_rosteredUserID_3.ToString());

            #endregion

            #region Step 9 to Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickRegardinguserLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_2.ToString())
                .TypeSearchQuery(_rostered_SystemUsername3)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_rosteredUserID_3.ToString())
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_coreSystemUserID_1.ToString())
                .TypeSearchQuery(_core_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_coreSystemUserID_2.ToString())
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_1.ToString())
                .TypeSearchQuery(_provider_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_providerSystemUserID_2.ToString())
                .TypeSearchQuery(_admin_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_systemAdministratorUserID_1.ToString())
                .TypeSearchQuery(_admin_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementNotPresent(_systemAdministratorUserID_2.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .SelectResultElement(_rosteredUserID_2.ToString());

            #endregion

            #region Step 12

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ValidateRegardingUserLinkFieldText(_rostered_SystemUserFullname2)
                .ClickRegardingFieldLink();

            lookupFormPage
                .WaitForRecordCannotBeAccessedMessageAreaToLoad()
                .ValidateRecordInaccessibleNotificationMessage("The record you are attempting to access cannot be displayed because it is either deleted or your security privileges prevent access. Id " + _rosteredUserID_2.ToString() + " and Type SystemUser.")
                .CloseRecordCannotBeDisplayedNotificationMessage();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickRegardinguserLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .SelectResultElement(_rosteredUserID_1.ToString());

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ValidateRegardingUserLinkFieldText(_rostered_SystemUserFullname1)
                .ClickRegardingFieldLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_rostered_SystemUsername1)
                .ValidateFirstNameFieldValue("CDV6_19715")
                .ValidateLastNameFieldValue("Rostered_User1" + lastname)
                .ValidateSystemUserRecordTitle(_rostered_SystemUserFullname1);

            #endregion

            #region Step 13

            systemUserRecordPage
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertAvailableFromDateField(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .SelectGender_Options("Male")
                .SelectSmoker_Option("No")
                .SelectPets_Option("No Pets")
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .validateStartDate_Field(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateAddressType_FieldText("Home")
                .ClickBackButton();

            #endregion

            #region Step 14

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            dbHelper.systemUser.UpdateEmployeeTypeId(_rosteredUserID_1, 1);

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_rostered_SystemUserFullname1)
                .ValidateEmployeeTypeFieldValue("1");

            #endregion

            #region Step 15
            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_rosteredUserID_2.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_rostered_SystemUserFullname2);

            #endregion

            #region Step 16

            dbHelper.systemUser.UpdateEmployeeTypeId(_rosteredUserID_1, 1);

            systemUserRecordPage
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString());

            #endregion

            #region Step 17

            dbHelper.systemUser.UpdateEmployeeTypeId(_rosteredUserID_1, 3);
            dbHelper.systemUser.UpdateEmployeeTypeId(_rosteredUserID_2, 3);

            systemUsersPage
                .WaitForResultsGridToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3464")]
        [Description("Care Security: If logged in user employee type is “System Administrator”, then all records of type “Provider System User “ ,”System Administrator” and  ”Core System User” are shown.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "Core Users")]
        [TestProperty("Screen3", "Provider Users")]
        [TestProperty("Screen4", "Rostered Users")]
        public void SystemUser_EmployeeType_UITestCases006()
        {
            DataInitalization_Method1();

            #region Create Admin system user 1 for Team A

            _systemAdministratorUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Admin1" + lastname, "CDV6_19716", "Admin1" + lastname, "CDV6_19716 Admin1" + lastname, "Passw0rd_!", "CDV6_19716_Admin1@somemail.com", "CDV6_19716_Admin1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 4, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _admin_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_1, "username")["username"];

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_1, DateTime.Now.Date);
            _admin_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Admin system user 2 for Team B

            _systemAdministratorUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Admin2" + lastname, "CDV6_19716", "Admin2" + lastname, "CDV6_19716 Admin2" + lastname, "Passw0rd_!", "CDV6_19716_Admin2@somemail.com", "CDV6_19716_Admin2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 4, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _admin_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_2, "username")["username"];

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_2, DateTime.Now.Date);
            _admin_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_2, "fullname")["fullname"];
            #endregion

            #region Create Admin system user 3 for Team A

            _systemAdministratorUserID_3 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Admin3" + lastname, "CDV6_19716", "Admin3" + lastname, "CDV6_19716 Admin3" + lastname, "Passw0rd_!", "CDV6_19716_Admin3@somemail.com", "CDV6_19716_Admin3@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 4, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _admin_SystemUsername3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_3, "username")["username"];

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_3, DateTime.Now.Date);
            _admin_SystemUserFullname3 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID_3, "fullname")["fullname"];
            #endregion

            #region Create Rostered system user 1 for Team A

            _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Rostered_User1" + lastname, "CDV6_19716", "Rostered_User1" + lastname, "CDV6_19716 Rostered_User1" + lastname, "Passw0rd_!", "CDV6_19716_RosteredUser1@somemail.com", "CDV6_19716_RosteredUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 3, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _rostered_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);
            _rostered_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Rostered system user 2 for Team B

            _rosteredUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Rostered_User2" + lastname, "CDV6_19716", "Rostered_User2" + lastname, "CDV6_19716Rostered_User2" + lastname, "Passw0rd_!", "CDV6_19716_RosteredUser2@somemail.com", "CDV6_19716_RosteredUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 3, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _rostered_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_2))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_2, DateTime.Now.Date);
            _rostered_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_rosteredUserID_2, "fullname")["fullname"];
            #endregion            

            #region Create Core system user 1 for Team A

            _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Core_User1" + lastname, "CDV6_19716", "Core_User1" + lastname, "CDV6_19716 Core_User1" + lastname, "Passw0rd_!", "CDV6_19716_CoreUser1@somemail.com", "CDV6_19716_CoreUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 2, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _core_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);
            _core_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Core system user 2 for Team B

            _coreSystemUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Core_User2" + lastname, "CDV6_19716", "Core_User2" + lastname, "CDV6_19716 Core_User2" + lastname, "Passw0rd_!", "CDV6_19716_CoreUser2@somemail.com", "CDV6_19716_CoreUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 2, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _core_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_2, "username")["username"];

            //remove any existing profile from the user
            foreach (var secProfId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_2))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(secProfId);

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_2, DateTime.Now.Date);
            _core_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_coreSystemUserID_2, "fullname")["fullname"];
            #endregion

            #region Create Provider system user 1 for Team A

            _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Provider_User1" + lastname, "CDV6_19716", "Provider_User1" + lastname, "CDV6_19716 Provider_User1" + lastname, "Passw0rd_!", "CDV6_19716_ProviderUser1@somemail.com", "CDV6_19716_ProviderUser1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, 1, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _provider_SystemUsername1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "username")["username"];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);
            _provider_SystemUserFullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_1, "fullname")["fullname"];
            #endregion

            #region Create Provider system user 2 for Team B

            _providerSystemUserID_2 = dbHelper.systemUser.CreateSystemUser("CDV6_19716_Provider_User2" + lastname, "CDV6_19716", "Provider_User2" + lastname, "CDV6_19716 Provider_User2" + lastname, "Passw0rd_!", "CDV6_19716_ProviderUser2@somemail.com", "CDV6_19716_ProviderUser2@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId2, false, 1, null, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
            _provider_SystemUsername2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_2, "username")["username"];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_2, systemAdministratorSecurityProfileId);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_2, DateTime.Now.Date);
            _provider_SystemUserFullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_providerSystemUserID_2, "fullname")["fullname"];
            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(_admin_SystemUsername1, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_admin_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_systemAdministratorUserID_1.ToString())
                .OpenRecord(_systemAdministratorUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateUserNameFieldValue(_admin_SystemUsername1)
                .ValidateSystemUserRecordTitle(_admin_SystemUserFullname1)
                .ClickBackButton();

            #endregion

            #region Step 3 to Step 6
            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_admin_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_systemAdministratorUserID_2.ToString())
                .InsertUserName(_admin_SystemUsername3)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_systemAdministratorUserID_3.ToString())
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_coreSystemUserID_1.ToString())
                .InsertUserName(_core_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_coreSystemUserID_2.ToString())
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_1.ToString())
                .InsertUserName(_provider_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_2.ToString())
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .InsertUserName(_rostered_SystemUsername2)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString());

            #endregion

            #region Step 7

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSecuritySubLink()
                .ValidateCoreUsersMenuLink(true)
                .ValidateProviderUsersMenuLink(true)
                .ValidateRosteredUsersMenuLink(true);

            #endregion

            #region Step 8

            mainMenu
                .NavigateToRosteredUsersSection();

            rosteredUsersPage
                .WaitForRosteredUsersPageToLoad()
                .ClickSortByCreatedOnDescendingOrder()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .ValidateRecordIsPresent(_rosteredUserID_2.ToString());

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSecuritySubLink()
                .NavigateToCoreUsersSection();

            coreUsersPage
                .WaitForCoreUsersPageToLoad()
                .ClickSortByCreatedOnDescendingOrder()
                .ValidateRecordIsPresent(_coreSystemUserID_1.ToString())
                .ValidateRecordIsPresent(_coreSystemUserID_2.ToString());

            #endregion

            #region Step 9 to Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffReviewSection();

            staffReviewPage
                .WaitForStaffReviewPageToLoad()
                .ClickCreateRecordButton();

            staffReviewRecordPage
                .WaitForStaffReviewNewRecordCreatePageToLoad()
                .ClickRegardinguserLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery(_rostered_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_1.ToString())
                .TypeSearchQuery(_rostered_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_rosteredUserID_2.ToString())
                .TypeSearchQuery(_admin_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_systemAdministratorUserID_1.ToString())
                .TypeSearchQuery(_admin_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_systemAdministratorUserID_2.ToString())
                .TypeSearchQuery(_admin_SystemUsername3)
                .TapSearchButton()
                .ValidateResultElementPresent(_systemAdministratorUserID_3.ToString())
                .TypeSearchQuery(_core_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_coreSystemUserID_1.ToString())
                .TypeSearchQuery(_core_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_coreSystemUserID_2.ToString())
                .TypeSearchQuery(_provider_SystemUsername1)
                .TapSearchButton()
                .ValidateResultElementPresent(_providerSystemUserID_1.ToString())
                .TypeSearchQuery(_provider_SystemUsername2)
                .TapSearchButton()
                .ValidateResultElementPresent(_providerSystemUserID_2.ToString())
                .ClickCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_admin_SystemUsername3)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_systemAdministratorUserID_3.ToString())
                .OpenRecord(_systemAdministratorUserID_3.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_admin_SystemUserFullname3)
                .InsertBirthDate(DateTime.Now.AddYears(-15).ToString("dd'/'MM'/'yyyy"))
                .SelectGender_Options("Male")
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .ValidateDateOfBirthFieldValue(DateTime.Now.AddYears(-15).ToString("dd'/'MM'/'yyyy"))
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_core_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_coreSystemUserID_1.ToString())
                .OpenRecord(_coreSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_core_SystemUserFullname1)
                .InsertPersonalEmail("test@testmail.com")
                .SelectGender_Options("Male")
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .ValidatePesonalEmailFieldValue("test@testmail.com")
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_provider_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_providerSystemUserID_1.ToString())
                .OpenRecord(_providerSystemUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_provider_SystemUserFullname1)
                .InsertPersonalEmail("test@testmail.com")
                .SelectGender_Options("Male")
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .ValidatePesonalEmailFieldValue("test@testmail.com")
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_rostered_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_rosteredUserID_1.ToString())
                .OpenRecord(_rosteredUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_rostered_SystemUserFullname1)
                .InsertPersonalEmail("test@testmail.com")
                .SelectGender_Options("Male")
                .SelectSmoker_Option("No")
                .SelectPets_Option("No Pets")
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .ValidatePesonalEmailFieldValue("test@testmail.com")
                .ClickBackButton();

            System.Threading.Thread.Sleep(6000);

            #endregion

            #region Step 12 to Step 16

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_admin_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_systemAdministratorUserID_1.ToString())
                .OpenRecord(_systemAdministratorUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_admin_SystemUserFullname1)
                .SelectEmployeeType("Core System User")
                .SelectGender_Options("Male")
                .ClickSaveButton()
                .ValidateUnauthorizedAccessNotificationMessage("Unauthorized Access")
                .CloseNotificationMessage();

            dynamicDialogPopup
                .WaitForUnauthorizedAccessDynamicDialogPopUpToLoad()
                .ValidateMessage("Unauthorized access to View Record Type SystemUser. If you have any questions, please contact the system administrator.")
                .TapCloseButton();

            dbHelper.systemUser.UpdateEmployeeTypeId(_systemAdministratorUserID_1, 4);

            mainMenu
                .WaitForMainMenuToLoad()
                .refreshPage();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_admin_SystemUsername1)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .ValidateRecordIsPresent(_systemAdministratorUserID_1.ToString())
                .OpenRecord(_systemAdministratorUserID_1.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(_admin_SystemUserFullname1)
                .ValidateEmployeeTypeFieldValue("4")
                .NavigateToSecurityProfilePage();

            string systemAdminProfile = "System Administrator";
            string systemAdminSecureProfile = "System User - Secure Fields (Edit)";
            var UserAdministratorSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_1, systemAdministratorSecurityProfileId).First();
            var UserSecureFieldsSecurityProfileId = dbHelper.userSecurityProfile.GetByUserIDAndProfileId(_systemAdministratorUserID_1, systemUserSecureFieldsSecurityProfileId).First();

            securityProfilesPage
                .WaitForSystemUserSecurityProfilesPageToLoad()
                .ValidateSecurityProfileCell(UserAdministratorSecurityProfileId.ToString(), systemAdminProfile)
                .ValidateSecurityProfileCell(UserSecureFieldsSecurityProfileId.ToString(), systemAdminSecureProfile);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-19764

        [TestProperty("JiraIssueID", "ACC-3465")]
        [Description("User employee type = Rostered System User - User has 'Team View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases001()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 3; //Rostered System User 
            var username = "CDV6_19764_User1_" + lastname;
            _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3466")]
        [Description("User employee type = Rostered System User - User has 'BU View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases002()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 3; //Rostered System User 
            var username = "CDV6_19764_User1_" + lastname;
            _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3467")]
        [Description("User employee type = Rostered System User - User has 'PCBU View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases003()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 3; //Rostered System User 
            var username = "CDV6_19764_User1_" + lastname;
            _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3468")]
        [Description("User employee type = Rostered System User - User has 'PCBU View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases004()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 3; //Rostered System User 
            var username = "CDV6_19764_User1_" + lastname;
            _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3469")]
        [Description("User employee type = Rostered System User - User has 'PCBU View' security profile to view person records - User belongs to Team A - Person record belongs to Team B - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases005()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId2, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 3; //Rostered System User 
            var username = "CDV6_19764_User1_" + lastname;
            _rosteredUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_rosteredUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_rosteredUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_rosteredUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .ValidatePersonRecordNotPresent(_personID.ToString())
                .ValidateNoRecordsMessageVisibility(true);

            #endregion

        }

        //--

        [TestProperty("JiraIssueID", "ACC-3470")]
        [Description("User employee type = Core System User - User has 'Team View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases006()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 2; //Core System User 
            var username = "CDV6_19764_User1_" + lastname;
            _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3471")]
        [Description("User employee type = Core System User - User has 'BU View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases007()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 2; //Core System User 
            var username = "CDV6_19764_User1_" + lastname;
            _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3472")]
        [Description("User employee type = Core System User - User has 'PCBU View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases008()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 2; //Core System User 
            var username = "CDV6_19764_User1_" + lastname;
            _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3473")]
        [Description("User employee type = Core System User - User has 'Org View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases009()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 2; //Core System User 
            var username = "CDV6_19764_User1_" + lastname;
            _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3474")]
        [Description("User employee type = Core System User - User has 'Org View' security profile to view person records - User belongs to Team A - Person record belongs to Team B - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases010()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId2, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 2; //Core System User 
            var username = "CDV6_19764_User1_" + lastname;
            _coreSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_coreSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_coreSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_coreSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .ValidatePersonRecordNotPresent(_personID.ToString())
                .ValidateNoRecordsMessageVisibility(true);

            #endregion

        }

        //--

        [TestProperty("JiraIssueID", "ACC-3475")]
        [Description("User employee type = Provider System User - User has 'Team View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases011()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 1; //Provider System User 
            var username = "CDV6_19764_User1_" + lastname;
            _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_providerSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3476")]
        [Description("User employee type = Provider System User - User has 'BU View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases012()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 1; //Provider System User 
            var username = "CDV6_19764_User1_" + lastname;
            _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_providerSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3477")]
        [Description("User employee type = Provider System User - User has 'PCBU View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases013()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 1; //Provider System User 
            var username = "CDV6_19764_User1_" + lastname;
            _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_providerSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3478")]
        [Description("User employee type = Provider System User - User has 'Org View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases014()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 1; //Provider System User 
            var username = "CDV6_19764_User1_" + lastname;
            _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_providerSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3479")]
        [Description("User employee type = Provider System User - User has 'Org View' security profile to view person records - User belongs to Team A - Person record belongs to Team B - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases015()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId2, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 1; //Provider System User 
            var username = "CDV6_19764_User1_" + lastname;
            _providerSystemUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_providerSystemUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_providerSystemUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_providerSystemUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .ValidatePersonRecordNotPresent(_personID.ToString())
                .ValidateNoRecordsMessageVisibility(true);

            #endregion

        }

        //--

        [TestProperty("JiraIssueID", "ACC-3480")]
        [Description("User employee type = System Administrator - User has 'Org View' security profile to view person records - User belongs to Team A - Person record belongs to Team A - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases016()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId1, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 4; //System Administrator 
            var username = "CDV6_19764_User1_" + lastname;
            _systemAdministratorUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_systemAdministratorUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3481")]
        [Description("User employee type = System Administrator - User has 'Org View' security profile to view person records - User belongs to Team A - Person record belongs to Team B - User should be able to access the person record")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Security")]
        [TestProperty("Screen1", "People")]
        public void SystemUser_EmployeeType_19619_UITestCases017()
        {
            DataInitalization_Method2();

            #region Create a Person record

            _personID = dbHelper.person.CreatePersonRecord("Mr", "Johndoe", "CDV6-19764", lastname, "", new DateTime(2000, 1, 2), _ethnicityId, _careProviders_TeamId2, 7, 1);
            System.Threading.Thread.Sleep(1000);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            #endregion

            #region Login User

            //create the system user
            var employeeTypeId = 4; //System Administrator 
            var username = "CDV6_19764_User1_" + lastname;
            _systemAdministratorUserID_1 = dbHelper.systemUser.CreateSystemUser(username, "CDV6_19764", "User1_" + lastname, "CDV6_19764 _User1_" + lastname, "Passw0rd_!", "CDV6_19764_User1@somemail.com", "CDV6_19764_User1@somemail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId1, false, employeeTypeId);

            //set the last password change date
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID_1, DateTime.Now.Date);

            //if any sec profile was added to the user, remove it
            foreach (var userSecurityProfileId in dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(_systemAdministratorUserID_1))
                dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileId);

            //Link the necessary security profiles to the user
            var secProfile1 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0];
            var secProfile2 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0];
            var secProfile3 = dbHelper.securityProfile.GetSecurityProfileByName("Person (View)")[0];

            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, secProfile1);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, secProfile2);
            dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemAdministratorUserID_1, secProfile3);

            #endregion

            #region Step 1

            loginPage
              .GoToLoginPage()
              .Login(username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region step 2

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForSystemUserPersonRecordPageToLoad();

            #endregion

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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_CareProviderEnvironment_UITestCases : FunctionalTest
    {
        private Guid _environmentId;
        private Guid _authenticationproviderid;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _productLanguageId;
        private Guid _languageId;
        private Guid _systemUserAliasType;
        private Guid _demographicsTitleId;
        private Guid _Testing_CDV6_13418_SystemUserId;
        private string _Testing_CDV6_13418_SystemUserId_Name = "Testing_CDV6_13418_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
        private Guid _Testing_CDV6_11927_SystemUserId;
        private Guid _Testing_CDV6_11927_SystemUserLanguageId;
        private Guid _Testing_CDV6_11927_SystemUserAddressId;
        private Guid _Testing_CDV6_11927_SystemUserAliasId;
        private Guid _Testing_CDV6_11927_SystemUserEmergencyContactId;
        private string _Testing_CDV6_11927_SystemUsername;
        private Guid _AddressBoroughId;
        private Guid _AddressWardId;
        private Guid adminUserId;
        private string loginUsername;
        private Guid user1Id;
        private Guid user2Id;
        private string userLastName = DateTime.Now.ToString("yyyyMMdd.HHmmss");
        private string user1name;
        private string user2name;
        private string fullname1;
        private string fullname2;

        [TestInitialize()]
        public void SystemUser_CareProviderEnvironment_SetupTest()
        {
            try
            {
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

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

                #region Product Language

                var productLanguageExists = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!productLanguageExists)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _productLanguageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Language

                var languageExists = dbHelper.language.GetLanguageIdByName("English (UK)").Any();
                if (!languageExists)
                {
                    _languageId = dbHelper.language.CreateLanguage("English (UK)", _careProviders_TeamId, "2345", "001", new DateTime(2015, 1, 1), null);
                }
                if (_languageId == Guid.Empty)
                {
                    _languageId = dbHelper.language.GetLanguageIdByName("English (UK)")[0];
                }

                #endregion Language

                #region Security Profiles
                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                #endregion

                #region Create SystemUser Record 2

                loginUsername = "CW_Admin_Test_User_3";
                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName(loginUsername).Any();
                if (!adminUserExists)
                {
                    adminUserId = dbHelper.systemUser.CreateSystemUser(loginUsername, "CW", "Admin Test User 3", "CW Admin Test User 3", "Passw0rd_!", "CW_Admin_Test_User_3@somemail.com", "CW_Admin_Test_User_3@othermail.com", "GMT Standard Time", null, null, _productLanguageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                }

                adminUserId = dbHelper.systemUser.GetSystemUserByUserName(loginUsername).FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);

                #endregion

                dbHelper = new Phoenix.DBHelper.DatabaseHelper(loginUsername, "Passw0rd_!", tenantName);

                #region SystemUser Alias Type

                var systemUserAliasTypeExists = dbHelper.systemUserAliasType.GetSystemUserAliasesTypeByName("Birth Name").Any();
                if (!systemUserAliasTypeExists)
                    dbHelper.systemUserAliasType.CreateSystemUserAliasType(_careProviders_TeamId, _careProviders_BusinessUnitId, "Birth Name", DateTime.Now.Date.AddYears(-3));
                _systemUserAliasType = dbHelper.systemUserAliasType.GetSystemUserAliasesTypeByName("Birth Name")[0];

                #endregion SystemUser Alias Type

                #region Demographics Title

                var demographicsTitleExists = dbHelper.demographicsTitle.GetByName("Dr").Any();
                if (!demographicsTitleExists)
                    dbHelper.demographicsTitle.CreateDemographicsTitle("Dr", DateTime.Now, _careProviders_TeamId);
                _demographicsTitleId = dbHelper.demographicsTitle.GetByName("Dr")[0];

                #endregion Title

                #region Create new record if necessary "Testing_CDV6_13418_...."

                _Testing_CDV6_13418_SystemUserId = dbHelper.systemUser.CreateSystemUser(_Testing_CDV6_13418_SystemUserId_Name, "Testing", "CDV6-13418", "Testing CDV6-13418", "Passw0rd_!", "Testing_CDV6_13418@somerandommail.com", null, "GMT Standard Time", null, null, _productLanguageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                #endregion

                #region Testing_CDV6_11927

                _Testing_CDV6_11927_SystemUsername = "Testing_CDV6_11927";
                if (!dbHelper.systemUser.GetSystemUserByUserName(_Testing_CDV6_11927_SystemUsername).Any())
                {
                    _Testing_CDV6_11927_SystemUserId = dbHelper.systemUser.CreateSystemUser(_Testing_CDV6_11927_SystemUsername, "Testing", "CDV6-11927", "Testing CDV6-11927", "Passw0rd_!", "Testing_CDV6_11927@somerandommail.com", null, "GMT Standard Time", null, null, _productLanguageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                    _Testing_CDV6_11927_SystemUserLanguageId = dbHelper.systemUserLanguage.CreateSystemUserLanguage(_careProviders_TeamId, _Testing_CDV6_11927_SystemUserId, _languageId, null, new DateTime(2020, 1, 2));
                    _Testing_CDV6_11927_SystemUserAddressId = dbHelper.systemUserAddress.CreateSystemUserAddress(_careProviders_TeamId, _Testing_CDV6_11927_SystemUserId, "pna", "pno", "", "", "", "", "", "pc", 1, new DateTime(2020, 1, 2), null);
                    _Testing_CDV6_11927_SystemUserAliasId = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _Testing_CDV6_11927_SystemUserId, _systemUserAliasType, "Testing", "mid name", "CDV6 13418");
                    _Testing_CDV6_11927_SystemUserEmergencyContactId = dbHelper.systemUserEmergencyContacts.CreateSystemUserEmergencyContacts(_careProviders_TeamId, _Testing_CDV6_11927_SystemUserId, _demographicsTitleId, "fn", "ln", new DateTime(2020, 1, 2), false, "123321123");
                }

                if (_Testing_CDV6_11927_SystemUserId == Guid.Empty)
                {
                    _Testing_CDV6_11927_SystemUserId = dbHelper.systemUser.GetSystemUserByUserName(_Testing_CDV6_11927_SystemUsername).FirstOrDefault();
                    _Testing_CDV6_11927_SystemUserLanguageId = dbHelper.systemUserLanguage.GetBySystemUserId(_Testing_CDV6_11927_SystemUserId).FirstOrDefault();
                    _Testing_CDV6_11927_SystemUserAddressId = dbHelper.systemUserAddress.GetBySystemUser(_Testing_CDV6_11927_SystemUserId.ToString()).FirstOrDefault();
                    _Testing_CDV6_11927_SystemUserAliasId = dbHelper.systemUserAlias.GetBySystemUserAliasId(_Testing_CDV6_11927_SystemUserId).FirstOrDefault();
                    _Testing_CDV6_11927_SystemUserEmergencyContactId = dbHelper.systemUserEmergencyContacts.GetBySystemUserId(_Testing_CDV6_11927_SystemUserId).FirstOrDefault();
                }

                #endregion

                #region Test User 1

                user1Id = dbHelper.systemUser.CreateSystemUser("CW_Test_User_1_" + userLastName, "CW", "Test User 1 " + userLastName, "CW Test User 1 " + userLastName, "Passw0rd_!", "CW_Test_User_1@somemail.com", "CW_Test_User_1@othermail.com", "GMT Standard Time", null, null, _productLanguageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(user1Id, systemAdministratorSecurityProfileId);
                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(user1Id, systemUserSecureFieldsSecurityProfileId);
                dbHelper.systemUser.UpdateLastPasswordChangedDate(user1Id, DateTime.Now.Date);
                user1name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(user1Id, "username")["username"];

                #endregion

                #region Test User 2
                user2Id = dbHelper.systemUser.CreateSystemUser("CW_Test_User_2_" + userLastName, "CW", "Test User 2 " + userLastName, "CW Test User 2 " + userLastName, "Passw0rd_!", "CW_Test_User_2@somemail.com", "CW_Test_User_2@othermail.com", "GMT Standard Time", null, null, _productLanguageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(user2Id, systemAdministratorSecurityProfileId);
                //dbHelper.userSecurityProfile.CreateUserSecurityProfile(user2Id, systemUserSecureFieldsSecurityProfileId);
                dbHelper.systemUser.UpdateLastPasswordChangedDate(user2Id, DateTime.Now.Date);
                user2name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(user2Id, "username")["username"];
                #endregion

                #region Address Borough

                var boroughExists = dbHelper.addressBorough.GetAddressBoroughByName("Camden").Any();
                if (!boroughExists)
                {
                    _AddressBoroughId = dbHelper.addressBorough.CreateAddressBorough("Camden", new DateTime(2020, 1, 2), _careProviders_TeamId);
                }
                if (_AddressBoroughId == Guid.Empty)
                    _AddressBoroughId = dbHelper.addressBorough.GetAddressBoroughByName("Camden").FirstOrDefault();

                #endregion

                #region Address Ward

                var wardExists = dbHelper.addressWard.GetAddressWardByName("Gorse Hill").Any();
                if (!wardExists)
                {
                    _AddressWardId = dbHelper.addressWard.CreateAddressWard("Gorse Hill", new DateTime(2020, 1, 2), _careProviders_TeamId);
                }
                if (_AddressWardId == Guid.Empty)
                    _AddressWardId = dbHelper.addressWard.GetAddressWardByName("Gorse Hill").FirstOrDefault();

                #endregion

                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13419

        [TestProperty("JiraIssueID", "ACC-871")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-11074")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Languages")]
        public void SystemUserLanguage_UITestMethod01()
        {
            dbHelper = new Phoenix.DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var languageId = commonMethodsDB.CreateLanguage(new Guid("8b1ae67d-fc00-ec11-a327-f90a4322a942"), "French",
                _careProviders_TeamId, "", "", new DateTime(2021, 8, 19), null);
            var language2Id = commonMethodsDB.CreateLanguage(new Guid("18657974-fc00-ec11-a327-f90a4322a942"), "German",
                _careProviders_TeamId, "", "", new DateTime(2021, 8, 19), null);
            var fluencyId = commonMethodsDB.CreateLanguageFluency(new Guid("31515b47-41a1-e811-80dc-0050560502cc"), "Fluently",
                _careProviders_TeamId, new DateTime(2018, 8, 16));

            user1name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(user1Id, "username")["username"];
            user2name = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(user2Id, "username")["username"];

            fullname1 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(user1Id, "fullname")["fullname"];
            fullname2 = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(user2Id, "fullname")["fullname"];

            foreach (var systemUserLanguageId in dbHelper.systemUserLanguage.GetBySystemUserId(user1Id))
                dbHelper.systemUserLanguage.DeleteSystemUserLanguage(systemUserLanguageId);

            foreach (var systemUserLanguageId in dbHelper.systemUserLanguage.GetBySystemUserId(user2Id))
                dbHelper.systemUserLanguage.DeleteSystemUserLanguage(systemUserLanguageId);


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()

                .InsertUserName(user1name)
                .ClickSearchButton()

                .WaitForResultsGridToLoad()
                .OpenRecord(user1Id.ToString());

            #endregion

            #region Step 2

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToLanguagesSubPage();

            #endregion

            #region Step 3 


            systemUserLanguagesPage
                .WaitForSystemUserLanguagesPageToLoad()
                .ClickNewRecordButton();

            #endregion

            #region Step 4 and Step 5

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New");

            #endregion

            #region Step 6

            systemUserLanguageRecordPage
                .ClickSystemUserRemoveButton()
                .ClickSaveButton()

                .ValidateMessageAreaVisible(true)
                .ValidateSystemUserFieldErrorLabelVisibility(true)
                .ValidateLanguageFieldErrorLabelVisibility(true)
                .ValidateStartDateFieldErrorLabelVisibility(true)

                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateSystemUserFieldErrorLabelText("Please fill out this field.")
                .ValidateLanguageFieldErrorLabelText("Please fill out this field.")
                .ValidateStartDateFieldErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 7

            systemUserLanguageRecordPage
                .ClickSystemUserLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(user1name).TapSearchButton().SelectResultElement(user1Id.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickLanguageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("French").TapSearchButton().SelectResultElement(languageId.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .InsertStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickFluencyLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Fluently").TapSearchButton().SelectResultElement(fluencyId.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickSaveButton()

                .WaitForSystemUserLanguageRecordPageToLoad(fullname1 + " \\ French \\ Fluently")
                .ValidateSystemUserLinkFieldText(fullname1)
                .ValidateLanguageLinkFieldText("French")
                .ValidateStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateFluencyLinkFieldText("Fluently");

            var userLanguages = dbHelper.systemUserLanguage.GetBySystemUserId(user1Id);
            Assert.AreEqual(1, userLanguages.Count);
            var newUserLanguageId = userLanguages[0];

            #endregion

            #region Step 8 and Step 9

            systemUserLanguageRecordPage
                .ClickSaveAndCloseButton();

            systemUserLanguagesPage
                .WaitForSystemUserLanguagesPageToLoad()
                .ValidateRecordPresent(newUserLanguageId.ToString());

            #endregion

            #region Step 10

            systemUserLanguagesPage
                .ValidateRecordCellText(newUserLanguageId.ToString(), 2, "French")
                .ValidateRecordCellText(newUserLanguageId.ToString(), 3, "Fluently")
                .ValidateRecordCellText(newUserLanguageId.ToString(), 4, DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 11

            systemUserLanguagesPage
                .ClickNewRecordButton();

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ValidateSystemUserLinkFieldText(fullname1);

            #endregion

            #region Step 12

            systemUserLanguageRecordPage
                .ClickSystemUserLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(user2name).TapSearchButton().SelectResultElement(user2Id.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickLanguageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("French").TapSearchButton().SelectResultElement(languageId.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .InsertStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ClickFluencyLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Fluently").TapSearchButton().SelectResultElement(fluencyId.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickSaveButton()

                .WaitForSystemUserLanguageRecordPageToLoad(fullname2 + " \\ French \\ Fluently")
                .ValidateSystemUserLinkFieldText(fullname2)
                .ValidateLanguageLinkFieldText("French")
                .ValidateStartDate(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateFluencyLinkFieldText("Fluently");

            userLanguages = dbHelper.systemUserLanguage.GetBySystemUserId(user1Id);
            Assert.AreEqual(1, userLanguages.Count);

            var user2Languages = dbHelper.systemUserLanguage.GetBySystemUserId(user2Id);
            Assert.AreEqual(1, user2Languages.Count);

            #endregion

            #region Step 13

            systemUserLanguageRecordPage
                .ClickBackButton();

            foreach (var systemUserLanguageId in dbHelper.systemUserLanguage.GetBySystemUserId(user1Id))
                dbHelper.systemUserLanguage.DeleteSystemUserLanguage(systemUserLanguageId);

            systemUserLanguagesPage
                .WaitForSystemUserLanguagesPageToLoad()
                .ClickNewRecordButton();

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickLanguageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("French").TapSearchButton().SelectResultElement(languageId.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .InsertStartDate("01/01/2003")
                .ClickFluencyLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Fluently").TapSearchButton().SelectResultElement(fluencyId.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickSaveButton()

                .WaitForSystemUserLanguageRecordPageToLoad(fullname1 + " \\ French \\ Fluently")
                .ValidateSystemUserLinkFieldText(fullname1)
                .ValidateLanguageLinkFieldText("French")
                .ValidateStartDate("01/01/2003")
                .ValidateFluencyLinkFieldText("Fluently");

            userLanguages = dbHelper.systemUserLanguage.GetBySystemUserId(user1Id);
            Assert.AreEqual(1, userLanguages.Count);
            var frenchUserLanguageId = userLanguages[0];

            #endregion

            #region Step 14

            systemUserLanguageRecordPage
                .ValidateSystemUserLookUpButtonDisabled(true)
                .ValidateLanguageLookUpButtonDisabled(false)
                .ValidateStartDateFieldDisabled(false)
                .ValidateFluencyLookUpButtonDisabled(false);

            #endregion

            #region Step 15

            systemUserLanguageRecordPage
                .ClickBackButton();

            systemUserLanguagesPage
                .WaitForSystemUserLanguagesPageToLoad()
                .ClickNewRecordButton();

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickLanguageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("French").TapSearchButton().SelectResultElement(languageId.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .InsertStartDate("20/12/2002")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Another Language record of the same type already exists.").TapCloseButton();


            #endregion

            #region Step 16

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickLanguageLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("German").TapSearchButton().SelectResultElement(language2Id.ToString());

            systemUserLanguageRecordPage
                .WaitForSystemUserLanguageRecordPageToLoad("New")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);
            systemUserLanguagesPage
                .WaitForSystemUserLanguagesPageToLoad();

            userLanguages = dbHelper.systemUserLanguage.GetBySystemUserId(user1Id);
            Assert.AreEqual(2, userLanguages.Count);
            var germanUserLanguageId = userLanguages.Where(c => c != frenchUserLanguageId).FirstOrDefault();

            systemUserLanguagesPage
                .ValidateRecordPositionInList(frenchUserLanguageId.ToString(), 1)
                .ValidateRecordPositionInList(germanUserLanguageId.ToString(), 2);

            #endregion

            #region Step 17

            systemUserLanguagesPage
                .ClickTableHeaderCell(2) //Click on the Language Header
                .WaitForSystemUserLanguagesPageToLoad()
                .ValidateRecordPositionInList(frenchUserLanguageId.ToString(), 1)
                .ValidateRecordPositionInList(germanUserLanguageId.ToString(), 2)

                .ClickTableHeaderCell(3) //Click on the Fluency Header
                .WaitForSystemUserLanguagesPageToLoad()
                .ValidateRecordPositionInList(germanUserLanguageId.ToString(), 1)
                .ValidateRecordPositionInList(frenchUserLanguageId.ToString(), 2)

                .ClickTableHeaderCell(4) //Click on the Start Date Header
                .WaitForSystemUserLanguagesPageToLoad()
                .ValidateRecordPositionInList(germanUserLanguageId.ToString(), 1)
                .ValidateRecordPositionInList(frenchUserLanguageId.ToString(), 2);

            #endregion

            #region Step 19

            systemUserLanguagesPage
                .InsertQuickSearchText("French")
                .ClickQuickSearchButton()
                .ValidateRecordPresent(frenchUserLanguageId.ToString())
                .ValidateRecordNotPresent(germanUserLanguageId.ToString())

                .ValidateRecordCellText(frenchUserLanguageId.ToString(), 2, fullname1)
                .ValidateRecordCellText(frenchUserLanguageId.ToString(), 3, "French")
                .ValidateRecordCellText(frenchUserLanguageId.ToString(), 4, "Fluently")
                .ValidateRecordCellText(frenchUserLanguageId.ToString(), 5, "01/01/2003")

                .InsertQuickSearchText("German")
                .ClickQuickSearchButton()
                .ValidateRecordNotPresent(frenchUserLanguageId.ToString())
                .ValidateRecordPresent(germanUserLanguageId.ToString());

            #endregion

            #region Step 18

            systemUserLanguagesPage
                .SelectRecord(germanUserLanguageId.ToString())
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            userLanguages = dbHelper.systemUserLanguage.GetBySystemUserId(user1Id);
            Assert.AreEqual(1, userLanguages.Count);

            #endregion

            #region Step 21

            //var auditRecords = dbHelper.audit.GetAuditByRecordID(frenchUserLanguageId, 1);
            //Assert.AreEqual(auditRecords.Count, 1);

            #endregion

            #region Step 22

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Languages")
                .SelectFilter("1", "System User")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").SelectViewByText("Lookup View").TypeSearchQuery(user1name).TapSearchButton().SelectResultElement(user1Id.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(frenchUserLanguageId.ToString());

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-13418

        [TestProperty("JiraIssueID", "ACC-908")]
        [Description("https://advancedcsg.atlassian.net/browse/CDV6-11496")]
        [TestMethod, TestCategory("UITest")]
        public void SystemUserGazetteerAddress_UITestMethod01()
        {

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_Testing_CDV6_13418_SystemUserId_Name)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_Testing_CDV6_13418_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            #endregion

            #region Step 2

            systemUserRecordPage
                .InsertPropertyName("CDV6-11496 PNA A")
                .ClickAddressSearchButton()

                .ValidatePropertyNameFieldValue("CDV6-11496 PNA A")
                .ValidatePropertyNoFieldValue("CDV6-11496 PNO A")
                .ValidateStreetFieldValue("CDV6-11496 ST A")
                .ValidateVillageDistrictFieldValue("CDV6-11496 VLG A")
                .ValidateTownCityFieldValue("CDV6-11496 TW A")
                .ValidatePostcodeFieldValue("CR0 3RA")
                .ValidateCountyFieldValue("CDV6-11496 CON A")
                .ValidateCountryFieldValue("CDV6-11496 COUNTRY A");

            #endregion

            #region Step 3

            systemUserRecordPage
                .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPropertyNo("CDV6-11496 PNO C")
            .ClickAddressSearchButton()
            .ValidatePropertyNameFieldValue("")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO C")
            .ValidateStreetFieldValue("CDV6-11496 ST C")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG C")
            .ValidateTownCityFieldValue("CDV6-11496 TW C")
            .ValidatePostcodeFieldValue("CR0 3RC")
            .ValidateCountyFieldValue("CDV6-11496 CON C")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY C");

            #endregion

            #region Step 4

            systemUserRecordPage
                .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertStreetName("CDV6-11496 ST A")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA A")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO A")
            .ValidateStreetFieldValue("CDV6-11496 ST A")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG A")
            .ValidateTownCityFieldValue("CDV6-11496 TW A")
            .ValidatePostcodeFieldValue("CR0 3RA")
            .ValidateCountyFieldValue("CDV6-11496 CON A")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY A")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertVillageDistrict("CDV6-11496 VLG B")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA B")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO B")
            .ValidateStreetFieldValue("CDV6-11496 ST B")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG B")
            .ValidateTownCityFieldValue("CDV6-11496 TW B")
            .ValidatePostcodeFieldValue("CR0 3RB")
            .ValidateCountyFieldValue("CDV6-11496 CON B")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY B")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertTownCity("CDV6-11496 TW A")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA A")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO A")
            .ValidateStreetFieldValue("CDV6-11496 ST A")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG A")
            .ValidateTownCityFieldValue("CDV6-11496 TW A")
            .ValidatePostcodeFieldValue("CR0 3RA")
            .ValidateCountyFieldValue("CDV6-11496 CON A")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY A")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPostCode("CR0 3RB")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA B")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO B")
            .ValidateStreetFieldValue("CDV6-11496 ST B")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG B")
            .ValidateTownCityFieldValue("CDV6-11496 TW B")
            .ValidatePostcodeFieldValue("CR0 3RB")
            .ValidateCountyFieldValue("CDV6-11496 CON B")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY B")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertCounty("CDV6-11496 CON A")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA A")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO A")
            .ValidateStreetFieldValue("CDV6-11496 ST A")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG A")
            .ValidateTownCityFieldValue("CDV6-11496 TW A")
            .ValidatePostcodeFieldValue("CR0 3RA")
            .ValidateCountyFieldValue("CDV6-11496 CON A")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY A")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertCountry("CDV6-11496 COUNTRY B")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA B")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO B")
            .ValidateStreetFieldValue("CDV6-11496 ST B")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG B")
            .ValidateTownCityFieldValue("CDV6-11496 TW B")
            .ValidatePostcodeFieldValue("CR0 3RB")
            .ValidateCountyFieldValue("CDV6-11496 CON B")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY B");

            #endregion

            #region Step 5

            systemUserRecordPage
                .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPropertyName("CDV6-11496 PNA D")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA D")
            .ValidatePropertyNoFieldValue("")
            .ValidateStreetFieldValue("CDV6-11496 ST D")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG D")
            .ValidateTownCityFieldValue("CDV6-11496 TW D")
            .ValidatePostcodeFieldValue("CR0 3RD")
            .ValidateCountyFieldValue("CDV6-11496 CON D")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY D")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPropertyName("CDV6-11496 PNA F")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA F")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO F")
            .ValidateStreetFieldValue("")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG F")
            .ValidateTownCityFieldValue("CDV6-11496 TW F")
            .ValidatePostcodeFieldValue("CR0 3RF")
            .ValidateCountyFieldValue("CDV6-11496 CON F")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY F")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPropertyName("CDV6-11496 PNA E")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA E")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO E")
            .ValidateStreetFieldValue("CDV6-11496 ST E")
            .ValidateVillageDistrictFieldValue("")
            .ValidateTownCityFieldValue("CDV6-11496 TW E")
            .ValidatePostcodeFieldValue("CR0 3RE")
            .ValidateCountyFieldValue("CDV6-11496 CON E")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY E")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPropertyName("CDV6-11496 PNA G")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA G")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO G")
            .ValidateStreetFieldValue("CDV6-11496 ST G")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG G")
            .ValidateTownCityFieldValue("")
            .ValidatePostcodeFieldValue("CR0 3RG")
            .ValidateCountyFieldValue("CDV6-11496 CON G")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY G")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPropertyName("CDV6-11496 PNA H")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA H")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO H")
            .ValidateStreetFieldValue("CDV6-11496 ST H")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG H")
            .ValidateTownCityFieldValue("CDV6-11496 TW H")
            .ValidatePostcodeFieldValue("CR0 3RH")
            .ValidateCountyFieldValue("")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY H")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPropertyName("CDV6-11496 PNA K")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA K")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO K")
            .ValidateStreetFieldValue("CDV6-11496 ST K")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG K")
            .ValidateTownCityFieldValue("CDV6-11496 TW K")
            .ValidatePostcodeFieldValue("")
            .ValidateCountyFieldValue("CDV6-11496 CON K")
            .ValidateCountryFieldValue("CDV6-11496 COUNTRY K")
            .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .WaitForSystemUserRecordPageToLoad()
            .InsertPropertyName("CDV6-11496 PNA I")
            .ClickAddressSearchButton()

            .ValidatePropertyNameFieldValue("CDV6-11496 PNA I")
            .ValidatePropertyNoFieldValue("CDV6-11496 PNO I")
            .ValidateStreetFieldValue("CDV6-11496 ST I")
            .ValidateVillageDistrictFieldValue("CDV6-11496 VLG I")
            .ValidateTownCityFieldValue("CDV6-11496 TW I")
            .ValidatePostcodeFieldValue("CR0 3RI")
            .ValidateCountyFieldValue("CDV6-11496 CON I")
            .ValidateCountryFieldValue("");

            #endregion

            #region Step 6

            systemUserRecordPage
                .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .InsertPropertyName("invalid search patter used here 122321")
            .ClickAddressSearchButton(false);

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("No matches found. Please refine your search address and try again.").TapOKButton();

            #endregion

            #region Step 7

            systemUserRecordPage
                .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
            .ClickAddressSearchButton(false);

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("You must enter some Address Search criteria.").TapOKButton();

            #endregion

            #region Step 8

            systemUserRecordPage
                .ClickBackButton();

            //alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(_Testing_CDV6_13418_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .InsertPropertyName("CDV6-11496 PNA A")
                .ClickAddressSearchButton()
                .SelectAddressType_Options("Home")
                .InsertStartDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertAvailableFromDateField(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .SelectGender_Options("Male")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(1000);

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .WaitForResultsGridToLoad()
                .OpenRecord(_Testing_CDV6_13418_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidatePropertyNameFieldValue("CDV6-11496 PNA A")
                .ValidatePropertyNoFieldValue("CDV6-11496 PNO A")
                .ValidateStreetFieldValue("CDV6-11496 ST A")
                .ValidateVillageDistrictFieldValue("CDV6-11496 VLG A")
                .ValidateTownCityFieldValue("CDV6-11496 TW A")
                .ValidatePostcodeFieldValue("CR0 3RA")
                .ValidateCountyFieldValue("CDV6-11496 CON A")
                .ValidateCountryFieldValue("CDV6-11496 COUNTRY A");

            #endregion

            #region Step 9

            systemUserRecordPage
                .InsertPropertyName("CDV6-11496 PNA A ...")
                .InsertPropertyNo("CDV6-11496 PNO A ...")
                .InsertStreetName("CDV6-11496 ST A ...")
                .InsertVillageDistrict("CDV6-11496 VLG A ...")
                .InsertTownCity("CDV6-11496 TW A ...")
                .InsertPostCode("CR0 3RA ...")
                .InsertCounty("CDV6-11496 CON A ...")
                .InsertCountry("CDV6-11496 COUNTRY A ...")
                .ClickSaveButton();

            addressActionPopUp.WaitForAddressActionPopUpToLoad().SelectViewByText("Update existing address record").TapOkButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(_Testing_CDV6_13418_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidatePropertyNameFieldValue("CDV6-11496 PNA A ...")
                .ValidatePropertyNoFieldValue("CDV6-11496 PNO A ...")
                .ValidateStreetFieldValue("CDV6-11496 ST A ...")
                .ValidateVillageDistrictFieldValue("CDV6-11496 VLG A ...")
                .ValidateTownCityFieldValue("CDV6-11496 TW A ...")
                .ValidatePostcodeFieldValue("CR0 3RA ...")
                .ValidateCountyFieldValue("CDV6-11496 CON A ...")
                .ValidateCountryFieldValue("CDV6-11496 COUNTRY A ...");

            #endregion

            #region Step 10

            systemUserRecordPage
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickNewRecordButton();

            string newSystemUsername = "CDV6_13418_Automation_User_2_" + DateTime.Now.ToString("ddMMyyyyHHmmss_FFFFFFF");
            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .SelectEmployeeType("Provider System User")
                .InsertFirstName("Testing")
                .InsertLastName("CDV6_13418_Automation_User_2")
                .SelectBusinessUnitByText("CareProviders")
                .SelectAuthenticationProviderid_Options("Internal")
                .InsertUserName(newSystemUsername)
                .InsertPassword("Passw0rd_!")
                .SelectSystemLanguage_Options("English (UK)")
                .ClickDefaultTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("CareProviders").TapSearchButton().SelectResultElement(_careProviders_TeamId.ToString());

            systemUserRecordPage
                .WaitForNewSystemUserRecordPageToLoad()
                .InsertPropertyName("CDV6-11496 PNA A")
                .ClickAddressSearchButton()

                .ValidatePropertyNameFieldValue("CDV6-11496 PNA A")
                .ValidatePropertyNoFieldValue("CDV6-11496 PNO A")
                .ValidateStreetFieldValue("CDV6-11496 ST A")
                .ValidateVillageDistrictFieldValue("CDV6-11496 VLG A")
                .ValidateTownCityFieldValue("CDV6-11496 TW A")
                .ValidatePostcodeFieldValue("CR0 3RA")
                .ValidateCountyFieldValue("CDV6-11496 CON A")
                .ValidateCountryFieldValue("CDV6-11496 COUNTRY A")
                .ClickClearAddressButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to clear address fields?").TapOKButton();

            systemUserRecordPage
                .InsertPropertyNo("CDV6-11496 PNO C")
                .ClickAddressSearchButton()

                .ValidatePropertyNameFieldValue("")
                .ValidatePropertyNoFieldValue("CDV6-11496 PNO C")
                .ValidateStreetFieldValue("CDV6-11496 ST C")
                .ValidateVillageDistrictFieldValue("CDV6-11496 VLG C")
                .ValidateTownCityFieldValue("CDV6-11496 TW C")
                .ValidatePostcodeFieldValue("CR0 3RC")
                .ValidateCountyFieldValue("CDV6-11496 CON C")
                .ValidateCountryFieldValue("CDV6-11496 COUNTRY C")

                .InsertPropertyName("NEW VALUE PNA C")
                .InsertCountry("CDV6-11496 COUNTRY C ...")
                .SelectAddressType_Options("Home")
                .SelectGender_Options("Male")
                .InsertStartDate("01/11/2000")
                .InsertAvailableFromDateField(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .InsertWorkEmail(newSystemUsername + "@test.com")
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var matchingUsers = dbHelper.systemUser.GetSystemUserByUserName(newSystemUsername);
            Assert.AreEqual(1, matchingUsers.Count());
            var newSystemUserId = matchingUsers[0];

            systemUsersPage
                .InsertUserName(newSystemUsername)
                .ClickSearchButton()
                .OpenRecord(newSystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidatePropertyNameFieldValue("NEW VALUE PNA C")
                .ValidatePropertyNoFieldValue("CDV6-11496 PNO C")
                .ValidateStreetFieldValue("CDV6-11496 ST C")
                .ValidateVillageDistrictFieldValue("CDV6-11496 VLG C")
                .ValidateTownCityFieldValue("CDV6-11496 TW C")
                .ValidatePostcodeFieldValue("CR0 3RC")
                .ValidateCountyFieldValue("CDV6-11496 CON C")
                .ValidateCountryFieldValue("CDV6-11496 COUNTRY C ...");

            #endregion

            #region Step 11

            systemUserRecordPage
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickAddNewButton();

            systemUserAddressRecordPage
                .WaitForSystemUserAddressRecordPageTitleToLoad("New")
                .InsertPropertyName("CDV6-11496 PNA A")
                .ClickAddressSearchButton();

            System.Threading.Thread.Sleep(1000);
            systemUserAddressRecordPage
                .ValidatePropertyNameText("CDV6-11496 PNA A")
                .ValidatePropertyNoText("CDV6-11496 PNO A")
                .ValidateStreetText("CDV6-11496 ST A")
                .ValidateVillageDistrictText("CDV6-11496 VLG A")
                .ValidateTownCityText("CDV6-11496 TW A")
                .ValidatePostCodeText("CR0 3RA")
                .ValidateCountyText("CDV6-11496 CON A")
                .ValidateCountryText("CDV6-11496 COUNTRY A");

            systemUserAddressRecordPage
                .InsertPropertyName("CDV6-11496 PNA D")
                .InsertPropertyNo("")
                .InsertStreet("")
                .InsertVillageDistrict("")
                .InsertTownCity("")
                .InsertPostCode("")
                .InsertCounty("")
                .InsertCountry("")
                .ClickAddressSearchButton();

            System.Threading.Thread.Sleep(1000);

            systemUserAddressRecordPage
                .ValidatePropertyNoText("")
                .ValidateStreetText("CDV6-11496 ST D")
                .ValidateVillageDistrictText("CDV6-11496 VLG D")
                .ValidateTownCityText("CDV6-11496 TW D")
                .ValidatePostCodeText("CR0 3RD")
                .ValidateCountyText("CDV6-11496 CON D")
                .ValidateCountryText("CDV6-11496 COUNTRY D");

            systemUserAddressRecordPage
                .InsertPropertyNo("UPDATE PNO D")
                .SelectAddressType_Options("Work")
                .InsertStartDate("15/11/2000")
                .ClickSaveAndCloseButton();

            systemUserAddressPage
                .WaitForSystemUserAddressPageToLoad()
                .ClickRefreshButton();

            //System.Threading.Thread.Sleep(2000);

            matchingUsers = dbHelper.systemUserAddress.GetBySystemUser(newSystemUserId.ToString());
            Assert.AreEqual(2, matchingUsers.Count());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-13424

        [TestProperty("JiraIssueID", "ACC-904")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "Security")]
        [TestProperty("BusinessModule3", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "Advanced Search")]
        [TestProperty("Screen2", "System Users")]
        [TestProperty("Screen3", "System User Languages")]
        [TestProperty("Screen4", "System User Aliases")]
        [TestProperty("Screen5", "System User Addresses")]
        [TestProperty("Screen6", "System User Emergency Contacts")]
        public void SystemUser_AdvancedSearch_UITestMethod01()
        {

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(loginUsername, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Languages")
                .SelectFilter("1", "System User")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_Testing_CDV6_11927_SystemUsername).TapSearchButton().SelectResultElement(_Testing_CDV6_11927_SystemUserId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_Testing_CDV6_11927_SystemUserLanguageId.ToString())
                .ValidateSearchResultRecordCellContent(_Testing_CDV6_11927_SystemUserLanguageId.ToString(), 2, "Testing CDV6-11927");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Addresses")
                .SelectFilter("1", "System User")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_Testing_CDV6_11927_SystemUsername).TapSearchButton().SelectResultElement(_Testing_CDV6_11927_SystemUserId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_Testing_CDV6_11927_SystemUserAddressId.ToString())
                .ValidateSearchResultRecordCellContent(_Testing_CDV6_11927_SystemUserAddressId.ToString(), 2, "Testing CDV6-11927");

            #endregion

            #region Step 3

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Aliases")
                .SelectFilter("1", "System User")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_Testing_CDV6_11927_SystemUsername).TapSearchButton().SelectResultElement(_Testing_CDV6_11927_SystemUserId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_Testing_CDV6_11927_SystemUserAliasId.ToString())
                .ValidateSearchResultRecordCellContent(_Testing_CDV6_11927_SystemUserAliasId.ToString(), 2, "Testing CDV6-11927");

            #endregion

            #region Step 4

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Emergency Contacts")
                .SelectFilter("1", "System User")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(_Testing_CDV6_11927_SystemUsername).TapSearchButton().SelectResultElement(_Testing_CDV6_11927_SystemUserId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_Testing_CDV6_11927_SystemUserEmergencyContactId.ToString())
                .ValidateSearchResultRecordCellContent(_Testing_CDV6_11927_SystemUserEmergencyContactId.ToString(), 2, "Testing CDV6-11927");

            #endregion

            #region Step 5

            mainMenu.WaitForMainMenuToLoad().ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Languages")
                .SelectFilter("1", "System User Language")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().ValidateGridHeaderCellText(2, "System User").ClickCloseButton();

            mainMenu.WaitForMainMenuToLoad().ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Addresses")
                .SelectFilter("1", "System User Address")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().ValidateGridHeaderCellText(2, "System User").ClickCloseButton();

            mainMenu.WaitForMainMenuToLoad().ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Aliases")
                .SelectFilter("1", "System User Alias")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().ValidateGridHeaderCellText(2, "System User").ClickCloseButton();

            mainMenu.WaitForMainMenuToLoad().ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Emergency Contacts")
                .SelectFilter("1", "System User Emergency Contact")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().ValidateGridHeaderCellText(2, "System User").ClickCloseButton();



            #endregion

            #region Step 6

            mainMenu.WaitForMainMenuToLoad().NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_Testing_CDV6_11927_SystemUsername)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_Testing_CDV6_11927_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            systemUserAliasesPage
               .WaitForSystemUserAliasesPageToLoad()
               .InsertSearchFieldText("Testing")
               .ClickSearchButton()
               .ValidateRecordCellText(_Testing_CDV6_11927_SystemUserAliasId.ToString(), 2, "Testing CDV6-11927");

            #endregion

            #region Step 7

            mainMenu.WaitForMainMenuToLoad().NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_Testing_CDV6_11927_SystemUsername)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_Testing_CDV6_11927_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToLanguagesSubPage();

            systemUserLanguagesPage
               .WaitForSystemUserLanguagesPageToLoad()
               .InsertQuickSearchText("English")
               .ClickQuickSearchButton()
               .ValidateRecordCellText(_Testing_CDV6_11927_SystemUserLanguageId.ToString(), 2, "Testing CDV6-11927");

            #endregion

            #region Step 8

            mainMenu.WaitForMainMenuToLoad().NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_Testing_CDV6_11927_SystemUsername)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_Testing_CDV6_11927_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Address();

            systemUserAddressPage
               .WaitForSystemUserAddressPageToLoad()
               .InsertQuickSearchText("pna")
               .ClickQuickSearchButton()
               .ValidateRecordCellText(_Testing_CDV6_11927_SystemUserAddressId.ToString(), 2, "Testing CDV6-11927");

            #endregion

            #region Step 9

            mainMenu.WaitForMainMenuToLoad().NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_Testing_CDV6_11927_SystemUsername)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_Testing_CDV6_11927_SystemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmergencyContactsSubPage();

            systemUserEmergencyContactsPage
               .WaitForSystemUserEmergencyContactsPageToLoad()
               .InsertQuickSearchText("fn")
               .ClickQuickSearchButton()
               .ValidateRecordCellText(_Testing_CDV6_11927_SystemUserEmergencyContactId.ToString(), 2, "Testing CDV6-11927");

            #endregion

            #region Step 10

            //this step is not necessay. this is already tested in Step 4 above

            #endregion

            #region Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System Users")
                .SelectRelatedRecords("Always Available Transport | Created By");
            //.SelectRelatedRecords("Always Available Transport Type | Created By");
            #endregion

            #region Step 12

            advanceSearchPage
                .ClickRelatedRecordsAddButton()
                .ValidateConditionBuilderCardTitle("Always Available Transport | Created By");

            #endregion
        }

        #endregion

    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-11077
    ///
    /// </summary>
    [TestClass]

    public class SystemUser_RelatedItems_Aliases_UITestCases : FunctionalTest
    {

        private string _environmentName;
        private Guid _environmentId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _languageId;
        private string loginUserName;
        private string _systemUserName01;
        private string _systemUserName02;
        private Guid _systemUserId01;
        private Guid _systemUserId02;
        private Guid _systemUserAliasType1;
        private Guid _systemUserAliasType2;
        public Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal
        public Guid adminUserId;
        public string adminUserName;

        [TestInitialize()]
        public void SystemUser_CareProviderEnvironment_SetupTest()
        {
            try
            {
                #region Environment

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];

                #endregion Environment

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                #region Create Admin user

                loginUserName = "CW_Admin_Test_User_2";
                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName(loginUserName).Any();
                if (!adminUserExists)
                    adminUserId = dbHelper.systemUser.CreateSystemUser(loginUserName, "CW", "Admin Test User 2", "CW Admin Test User 2", "Passw0rd_!", loginUserName + "@somemail.com", loginUserName + "@othermail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                adminUserId = dbHelper.systemUser.GetSystemUserByUserName(loginUserName).FirstOrDefault();
                adminUserName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(adminUserId, "fullname")["fullname"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);

                #endregion Create Admin User

                dbHelper = new DBHelper.DatabaseHelper(loginUserName, "Passw0rd_!", tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #region Create SystemUser01 Record

                _systemUserName01 = "Automation_Testing_SystemUser_Aliases01_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserId01 = dbHelper.systemUser.CreateSystemUser(_systemUserName01, "Automation_Testing", "SystemUser", "Aliases01", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                #endregion  Create SystemUser01 Record

                #region Create SystemUser02 Record

                _systemUserName02 = "CP_SystemUser1_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserId02 = dbHelper.systemUser.CreateSystemUser(_systemUserName02, "CP_Automation1_Testing", "SystemUser", "Aliases", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                #endregion  Create SystemUser02 Record

                #region SystemUserAliasType1

                var systemUserAliasType1 = dbHelper.systemUserAliasType.GetSystemUserAliasesTypeByName("Birth Name").Any();
                if (!systemUserAliasType1)
                    dbHelper.systemUserAliasType.CreateSystemUserAliasType(_careProviders_TeamId, _careProviders_BusinessUnitId, "Birth Name", DateTime.Now.Date.AddDays(-30));
                _systemUserAliasType1 = dbHelper.systemUserAliasType.GetSystemUserAliasesTypeByName("Birth Name")[0];

                #endregion SystemUserAliasType1

                #region SystemUserAliasType2

                var systemUserAliasType2 = dbHelper.systemUserAliasType.GetSystemUserAliasesTypeByName("Adopted Name").Any();
                if (!systemUserAliasType2)
                    dbHelper.systemUserAliasType.CreateSystemUserAliasType(_careProviders_TeamId, _careProviders_BusinessUnitId, "Adopted Name", DateTime.Now.Date.AddDays(-30));
                _systemUserAliasType2 = dbHelper.systemUserAliasType.GetSystemUserAliasesTypeByName("Adopted Name")[0];

                #endregion SystemUserAliasType2

                dbHelper = new DBHelper.DatabaseHelper();
                commonMethodsDB = new CommonMethodsDB(dbHelper);
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13422

        [TestProperty("JiraIssueID", "ACC-3238")]
        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "Verify the icon is present for Aliases and open Aliases" +
             "Click on Add new record button and Validate the title as System User Alias:New " +
             "Validate the fields 1. System user (Mandatory),2.Alias Type 3.First Name(by default, Null),4.Middle Name(by default, Null, 5.Last Name(Mandatory),6.Preferred Name(Mandatory - Yes / No radio button, by default - No)" +
             "Click on Save and Validate the Error messages" + "Enter all the fields and save the record" + "Validate the record is saved successfully" + "Validate the Page tiltle" +
             "Click Save and Close Button It should navigateto the previous page" + "Validate the Related records view and the created record is available" +
             "Validate the column header")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Aliases")]
        public void SystemUser_RelatedItems_Aliases_UITestMethod001()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(loginUserName, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertUserName(_systemUserName01)
               .ClickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());

            #endregion Step 1

            #region Step 2 & Step 3

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            #endregion Step 2 & Step 3

            #region Step 2 & 4

            systemUserAliasesPage
                  .WaitForSystemUserAliasesPageToLoad()
                  .ValidatePageTitle("System User Aliases")
                  .ClickAddNewButton();

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ValidateSystemUserAliasesRecordPageTitle("New");

            #endregion Step 2 & 4

            #region Step 5

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ValidateAvailableFields()
                .ValidateFirstNameFieldValue("")
                .ValidateMiddleNameFieldValue("")
                .ValidateSystemUserMandatoryField()
                .ValidateLastNameMandatoryField()
                .ValidatePreferredNameMandatoryField();

            #endregion Step 5

            #region Step 6

            systemUserAliasesRecordPage
               .WaitForSystemUserAliasesRecordPageToLoad()
               .ClickSaveButton()
               .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
               .ValidateLastNameFieldErrorLabelText("Please fill out this field.");

            #endregion Step 6

            #region Step 7

            systemUserAliasesRecordPage
               .WaitForSystemUserAliasesRecordPageToLoad()
               .InsertFirstName("Test")
               .InsertMiddleName("QA")
               .InsertLastName("Automation")
               .ClickAliasTypeLookUpButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Birth Name")
                .TapSearchButton()
                .ClickResultElementByText("Birth Name");

            systemUserAliasesRecordPage
                 .WaitForSystemUserAliasesRecordPageToLoad()
                 .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var userAlias = dbHelper.systemUserAlias.GetBySystemUserAliasId(_systemUserId01);
            Assert.AreEqual(1, userAlias.Count);
            var newUser = userAlias[0];

            systemUserAliasesRecordPage
              .WaitForSystemUserAliasesRecordPageToLoad()
              .ValidateSystemUserAliasesRecordPageTitle("Test \\ QA \\ Automation");

            var systemUserAliasesRecordFields = dbHelper.systemUserAlias.GetAliasBySystemUserAliasID(userAlias[0], "systemuserid", "firstname", "middlename", "lastname", "systemuseraliastypeid", "preferredname");

            Assert.AreEqual(_systemUserId01.ToString(), systemUserAliasesRecordFields["systemuserid"].ToString());
            Assert.AreEqual("Test", systemUserAliasesRecordFields["firstname"]);
            Assert.AreEqual("QA", systemUserAliasesRecordFields["middlename"]);
            Assert.AreEqual("Automation", systemUserAliasesRecordFields["lastname"]);
            Assert.AreEqual(_systemUserAliasType1.ToString(), systemUserAliasesRecordFields["systemuseraliastypeid"].ToString());
            Assert.AreEqual(false, systemUserAliasesRecordFields["preferredname"]);


            #endregion Step 7

            #region Step 8

            systemUserAliasesRecordPage
              .WaitForSystemUserAliasesRecordPageToLoad()
              .ClickSaveAndCloseButton();

            #endregion Step 8

            #region Step 9

            systemUserAliasesPage
                 .WaitForSystemUserAliasesPageToLoad()
                 .ValidateRelatedRecords_Option("Related Records");

            #endregion Step 9 

            #region Step 10 

            systemUserAliasesPage
                 .WaitForSystemUserAliasesPageToLoad()
                 .ValidateNoRecordMessageVisibile(false)
                 .ValidateColumnHeader(2, "Alias Type", "Alias Type")
                 .ValidateColumnHeader(3, "First Name", "First Name")
                 .ValidateColumnHeader(4, "Last Name", "Last Name")
                 .ValidatePreferredNameColumnHeader(5, "Preferred Name", "Preferred Name");

            #endregion Step 10

        }

        [TestProperty("JiraIssueID", "ACC-3239")]
        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "Click Add new record button" +
            "Validate the system user is autopopulated and Editable" + "Select different user with the System user look up button ,enter all the fields and save the record" +
            "Open the another user record and validate the created record is available under the respective system user." +
            "Validate the System user field is disabled and all the remaining fields are editable , select the preferred name radio button to yes and save the record" +
            "Open the other record and change the preferred Name option to Yes and click save button " + "validate the error message.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Aliases")]
        public void SystemUser_RelatedItems_Aliases_UITestMethod002()
        {
            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(loginUserName, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName01)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId01.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .ClickAddNewButton();

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ValidateSystemUser_Editable(true)
                .ValidateSystemUser_LinkField("Automation_Testing SystemUser");

            #endregion Step 11

            #region Step 12

            systemUserAliasesRecordPage
                .ClickSystemUserLoopUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CP_Automation1_Testing SystemUser")
                .TapSearchButton()
                .SelectResultElement(_systemUserId02.ToString());

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .InsertFirstName("TestUser02")
                .InsertMiddleName("QA")
                .InsertLastName("Automation")
                .ClickAliasTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Birth Name")
                .TapSearchButton()
                .ClickResultElementByText("Birth Name");

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ClickSaveButton();

            System.Threading.Thread.Sleep(3000);

            var userAliasRecord = dbHelper.systemUserAlias.GetBySystemUserAliasId(_systemUserId02);
            Assert.AreEqual(1, userAliasRecord.Count);
            var newUserAliasesRecordFields = userAliasRecord[0];

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ValidateSystemUserAliasesRecordPageTitle("TestUser02 \\ QA \\ Automation");

            var systemUserAliasesRecordFields = dbHelper.systemUserAlias.GetAliasBySystemUserAliasID(userAliasRecord[0], "systemuserid", "firstname", "middlename", "lastname", "systemuseraliastypeid", "preferredname");

            Assert.AreEqual(_systemUserId02.ToString(), systemUserAliasesRecordFields["systemuserid"].ToString());
            Assert.AreEqual("TestUser02", systemUserAliasesRecordFields["firstname"]);
            Assert.AreEqual("QA", systemUserAliasesRecordFields["middlename"]);
            Assert.AreEqual("Automation", systemUserAliasesRecordFields["lastname"]);
            Assert.AreEqual(_systemUserAliasType1.ToString(), systemUserAliasesRecordFields["systemuseraliastypeid"].ToString());
            Assert.AreEqual(false, systemUserAliasesRecordFields["preferredname"]);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName02)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId02.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .ValidateNoRecordMessageVisibile(false);

            #endregion Step 12

            #region Step 13

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .OpenRecord(newUserAliasesRecordFields.ToString());

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ValidateSystemUserAliasesRecordPageTitle("TestUser02 \\ QA \\ Automation")
                .ValidateSystemUser_Editable(false);

            #endregion Step 13

            #region Step 14

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ValidateFirstName_Editable(true)
                .ValidateMiddleName_Editable(true)
                .ValidateLastName_Editable(true)
                .ValidateAliasType_Editable(true)
                .ValidatePreferredYesOption_Editable(true)
                .ValidatePreferredNoOption_Editable(true);

            #endregion step 14

            #region step 15

            Guid UserAliases02 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType1, "QA", "CareProvider", "Testing");

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ClickPreferredName_YesRadioButton()
                .ClickSaveAndCloseButton();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName02)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId02.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .OpenRecord(UserAliases02.ToString());

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ClickPreferredName_YesRadioButton()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is another alias record with the preferred name set to true.");

            #endregion Step 15

        }

        [TestProperty("JiraIssueID", "ACC-3240")]
        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "User should have 2 Aliases records" +
            "Validate the search for First name and Last name" + "Validate the sort functionality for Aliases type, First name and last name " +
            "Validate the 2 records and its position" + "Validate Export to excel option")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Aliases")]
        public void SystemUser_RelatedItems_Aliases_UITestMethod003()
        {

            Guid UserAliases01 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType1, "QA", "CareProvider", "Testing");

            Guid UserAliases02 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType2, "Caredirector", "Automation", "TestingQA");

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(loginUserName, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName02)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId02.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            var userAliasRecord = dbHelper.systemUserAlias.GetBySystemUserAliasId(_systemUserId02);
            Assert.AreEqual(2, userAliasRecord.Count);

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .ValidateNoRecordMessageVisibile(false)
                .InsertSearchFieldText("QA")
                .ClickSearchButton()
                .ValidateRecordCellText(userAliasRecord[0].ToString(), 4, "QA");

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .InsertSearchFieldText("TestingQA")
                .ClickSearchButton()
                .WaitForSystemUserAliasesPageToLoad()
                .ValidateRecordCellText(userAliasRecord[1].ToString(), 5, "TestingQA");

            #endregion Step 16

            #region Step 17

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .InsertSearchFieldText("")
                .ClickSearchButton()
                .WaitForSystemUserAliasesPageToLoad()
                .WaitForSystemUserAliasesPageToLoad()
                .ValidateRecordPosition(1, userAliasRecord[1].ToString())
                .ValidateRecordPosition(2, userAliasRecord[0].ToString());

            #endregion Step 17

            #region Step 18

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .ClickAliasTypeSort();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .ValidateRecordPosition(1, userAliasRecord[0].ToString())
                .ValidateRecordPosition(2, userAliasRecord[1].ToString())
                .ClickFirstNameSort();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .ValidateRecordPosition(1, userAliasRecord[1].ToString())
                .ValidateRecordPosition(2, userAliasRecord[0].ToString())
                .ClickLastNameSort();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .ValidateRecordPosition(1, userAliasRecord[0].ToString())
                .ValidateRecordPosition(2, userAliasRecord[1].ToString());

            #endregion Step 18

            #region Step 20
            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .SelectRecord(userAliasRecord[1].ToString())
                .ClickExportToExcel();

            exportDataPopup
                .WaitForExportDataPopupToLoad()
                .SelectRecordsToExport("Selected Records")
                .SelectExportFormat("Csv (comma separated with quotes)")
                .ClickExportButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "SystemUserAliases.csv");
            Assert.IsTrue(fileExists);

            #endregion Step 20

        }

        [TestProperty("JiraIssueID", "ACC-3241")]
        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "User should have 2 Aliases records - Validate the delete functionality")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Aliases")]
        public void SystemUser_RelatedItems_Aliases_UITestMethod004()
        {
            Guid UserAliases01 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType1, "QA", "CareProvider", "Testing");

            Guid UserAliases02 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType2, "Caredirector", "Automation", "TestingQA");

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(loginUserName, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName02)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId02.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .SelectRecord(UserAliases01.ToString())
                .SelectRecord(UserAliases02.ToString())
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("2 item(s) deleted.").TapOKButton();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var userAliasesRecords = dbHelper.systemUserAddress.GetBySystemUserAddressId(_systemUserId02);
            Assert.AreEqual(0, userAliasesRecords.Count);

            #endregion Step 19

        }

        [TestProperty("JiraIssueID", "ACC-3242")]
        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "User should update the existing record - Validate the changes updated in Audit page")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Demographics")]
        [TestProperty("Screen1", "System User Aliases")]
        public void SystemUser_RelatedItems_Aliases_UITestMethod005()
        {
            Guid UserAliases = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType1, "QA", "CareProvider", "Testing");

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(loginUserName, "Passw0rd_!", _environmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName02)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId02.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            var userAliasRecord = dbHelper.systemUserAlias.GetBySystemUserAliasId(_systemUserId02);
            Assert.AreEqual(1, userAliasRecord.Count);

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .OpenRecord(userAliasRecord[0].ToString());

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .InsertFirstName("UpdatedQA")
                .ClickSaveButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUserName02)
                .ClickSearchButton()
                .WaitForResultsGridToLoad()
                .OpenRecord(_systemUserId02.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_Aliases();

            systemUserAliasesPage
                .WaitForSystemUserAliasesPageToLoad()
                .OpenRecord(userAliasRecord[0].ToString());

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .ValidateFirstNameText("UpdatedQA");

            System.Threading.Thread.Sleep(3000);

            var userAliasRecordFields = dbHelper.systemUserAlias.GetAliasBySystemUserAliasID(userAliasRecord[0], "firstname");

            Assert.AreEqual("UpdatedQA", userAliasRecordFields["firstname"]);

            systemUserAliasesRecordPage
                .WaitForSystemUserAliasesRecordPageToLoad()
                .NavigateToAuditSubPage();

            auditListPage
                .WaitForAuditListPageToLoad("systemuseralias")
                .ClickOnAuditRecordText("Updated");

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy(adminUserName);

            #endregion Step 21

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


using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using Phoenix.UITests.Framework;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Configuration;
using Phoenix.UITests.Framework.PageObjects;

namespace Phoenix.UITests.Settings.Security
{

    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-11077
    ///
    /// </summary>
    [TestClass]

    public class SystemUser_RelatedItems_Aliases_UITestCases : FunctionalTest
    {


        private Guid _environmentId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _languageId;
        private Guid _systemUserId01;
        private Guid _systemUserId02;
        private Guid _systemUserAliasType1;
        private Guid _systemUserAliasType2;
        public Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc");//Internal


        [TestInitialize()]
        public void SystemUser_CareProviderEnvironment_SetupTest()
        {

            #region Connecting Database : CareProvider

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["CareDirectorQA_CDEntities"].ConnectionString = connectionStringsSection.ConnectionStrings["CareDirectorQA_CDEntities"].ConnectionString.Replace("&quot;", "\"").Replace("CareDirectorQA_CD", "CareProviders_CD");
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");

            #endregion Connecting Database : CareProvider

            #region Environment

            _environmentId = Guid.Parse(ConfigurationManager.AppSettings["CareProvidersEnvironmentID"]);

                dbHelper = new Phoenix.DBHelper.DatabaseHelper("CW_Admin_Test_User_1", "Passw0rd_!", _environmentId);

            
            #endregion Environment


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

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Lanuage



            #region To delete System user :Automation_Testing_SystemUser_Aliases01, Address & Aliases Record related to the Person


            int systemUser = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_Aliases01").Count();
            if (systemUser == 1)
            { //To delete System user Address
                foreach (var systemUserId in dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_Aliases01"))
                {
                    foreach (var systemUserLanguageId in dbHelper.systemUserLanguage.GetBySystemUserId(systemUserId))
                    {
                        dbHelper.systemUserLanguage.DeleteSystemUserLanguage(systemUserLanguageId);
                    }

                    foreach (var systemUserAddressId in dbHelper.systemUserAddress.GetBySystemUserAddressId(systemUserId))
                    {
                        dbHelper.systemUser.UpdateLinkedAddress(systemUserId, null);

                        dbHelper.systemUserAddress.DeleteSystemUserAddress_AdoNetDirectConnection(systemUserAddressId);
                    }

                    foreach (var userApplicationId in dbHelper.userApplication.GetUserApplicationBySystemUserId(systemUserId))
                    {
                        dbHelper.userApplication.DeleteUserApplication(userApplicationId);
                    }

                    foreach (var TeamMemberId in dbHelper.teamMember.GetTeamMemberByUserAndTeamID(systemUserId, _careProviders_TeamId))
                    {
                        dbHelper.teamMember.DeleteTeamMember_AdoNetDirectConnection(TeamMemberId);
                    }

                    foreach (var systemUserAliasId in dbHelper.systemUserAlias.GetBySystemUserAliasId(systemUserId))
                    {
                        dbHelper.systemUserAlias.DeleteSystemUserAlias(systemUserAliasId);
                    }


                    dbHelper.systemUser.DeleteSystemUser_AdoNetDirectConnection(systemUserId);
                }

            }



            #endregion To delete System user :Automation_Testing_SystemUser_Aliases01, Address & Aliases Record related to the Person


            #region To delete System user :CP_SystemUser1, Address & Aliases Record related to the Person


            int systemUser02 = dbHelper.systemUser.GetSystemUserByUserName("CP_SystemUser1").Count();
            if (systemUser02 == 1)
            { //To delete System user Address
                foreach (var systemUserId in dbHelper.systemUser.GetSystemUserByUserName("CP_SystemUser1"))
                {
                    foreach (var systemUserLanguageId in dbHelper.systemUserLanguage.GetBySystemUserId(systemUserId))
                    {
                        dbHelper.systemUserLanguage.DeleteSystemUserLanguage(systemUserLanguageId);
                    }

                    foreach (var systemUserAddressId in dbHelper.systemUserAddress.GetBySystemUserAddressId(systemUserId))
                    {
                        dbHelper.systemUser.UpdateLinkedAddress(systemUserId, null);

                        dbHelper.systemUserAddress.DeleteSystemUserAddress_AdoNetDirectConnection(systemUserAddressId);
                    }

                    foreach (var userApplicationId in dbHelper.userApplication.GetUserApplicationBySystemUserId(systemUserId))
                    {
                        dbHelper.userApplication.DeleteUserApplication(userApplicationId);
                    }

                    foreach (var TeamMemberId in dbHelper.teamMember.GetTeamMemberByUserAndTeamID(systemUserId, _careProviders_TeamId))
                    {
                        dbHelper.teamMember.DeleteTeamMember_AdoNetDirectConnection(TeamMemberId);
                    }

                    foreach (var systemUserAliasId in dbHelper.systemUserAlias.GetBySystemUserAliasId(systemUserId))
                    {
                        dbHelper.systemUserAlias.DeleteSystemUserAlias(systemUserAliasId);
                    }


                    dbHelper.systemUser.DeleteSystemUser_AdoNetDirectConnection(systemUserId);
                }

            }

            #endregion To delete System user :CP_SystemUser1, Address & Aliases Record related to the Person



            #region Create SystemUser01 Record

            var newSystemUser01 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_Aliases01").Any();
            if (!newSystemUser01)
                dbHelper.systemUser.CreateSystemUser("Automation_Testing_SystemUser_Aliases01", "Automation_Testing", "SystemUser", "Aliases01", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
            _systemUserId01 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_Aliases01")[0];


            #endregion  Create SystemUser01 Record


            #region Create SystemUser02 Record

            var newSystemUser02 = dbHelper.systemUser.GetSystemUserByUserName("CP_SystemUser1").Any();
            if (!newSystemUser02)
                dbHelper.systemUser.CreateSystemUser("CP_SystemUser1", "CP_Automation1_Testing", "SystemUser", "Aliases", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
            _systemUserId02 = dbHelper.systemUser.GetSystemUserByUserName("CP_SystemUser1")[0];


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

        }



        #region https://advancedcsg.atlassian.net/browse/CDV6-13422



        [TestProperty("JiraIssueID", "CDV6-13986")]

        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "Verify the icon is present for Aliases and open Aliases" +
             "Click on Add new record button and Validate the title as System User Alias:New " +
             "Validate the fields 1. System user (Mandatory),2.Alias Type 3.First Name(by default, Null),4.Middle Name(by default, Null, 5.Last Name(Mandatory),6.Preferred Name(Mandatory - Yes / No radio button, by default - No)" +
             "Click on Save and Validate the Error messages" + "Enter all the fields and save the record" + "Validate the record is saved successfully" + "Validate the Page tiltle" +
             "Click Save and Close Button It should navigateto the previous page" + "Validate the Related records view and the created record is available" +
             "Validate the column header")]


        [TestMethod]
        public void SystemUser_RelatedItems_Aliases_UITestCases_UITestMethod001()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", "Care Providers")
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertQuickSearchText("Automation_Testing_SystemUser_Aliases01")
               .ClickQuickSearchButton()
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

            Assert.AreEqual(_systemUserId01, systemUserAliasesRecordFields["systemuserid"]);
            Assert.AreEqual("Test", systemUserAliasesRecordFields["firstname"]);
            Assert.AreEqual("QA", systemUserAliasesRecordFields["middlename"]);
            Assert.AreEqual("Automation", systemUserAliasesRecordFields["lastname"]);
            Assert.AreEqual(_systemUserAliasType1, systemUserAliasesRecordFields["systemuseraliastypeid"]);
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
                 .ValidateColumnHeader(2, "Alias Type", "ALIAS TYPE")
                 .ValidateColumnHeader(3, "First Name", "FIRST NAME")
                 .ValidateColumnHeader(4, "Last Name", "LAST NAME")
                 .ValidatePreferredNameColumnHeader(5, "Preferred Name", "PREFERRED NAME");

            #endregion Step 10




        }


        [TestProperty("JiraIssueID", "CDV6-14009")]

        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "Click Add new record button" +
          "Validate the system user is autopopulated and Editable" + "Select different user with the System user look up button ,enter all the fields and save the record" +
          "Open the another user record and validate the created record is available under the respective system user." +
          "Validate the System user field is disabled and all the remaining fields are editable , select the preferred name radio button to yes and save the record" +
          "Open the other record and change the preferred Name option to Yes and click save button " + "validate the error message.")]


        [TestMethod]
        public void SystemUser_RelatedItems_Aliases_UITestCases_UITestMethod002()
        {

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login("CW_Admin_Test_User_1", "Passw0rd_!", "Care Providers")
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertQuickSearchText("Automation_Testing_SystemUser_Aliases01")
               .ClickQuickSearchButton()
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
                .ValidateSystemUser_LinkField("Automation_Testing SystemUser")

            #endregion Step 11

            #region Step 12


                 .ClickSystemUserLoopUpButton();

            lookupPopup
                 .WaitForLookupPopupToLoad()
                 .TypeSearchQuery("CP_Automation1_Testing SystemUser")
                 .TapSearchButton()
                 .ClickResultElementByText("CP_Automation1_Testing SystemUser");

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

            Assert.AreEqual(_systemUserId02, systemUserAliasesRecordFields["systemuserid"]);
            Assert.AreEqual("TestUser02", systemUserAliasesRecordFields["firstname"]);
            Assert.AreEqual("QA", systemUserAliasesRecordFields["middlename"]);
            Assert.AreEqual("Automation", systemUserAliasesRecordFields["lastname"]);
            Assert.AreEqual(_systemUserAliasType1, systemUserAliasesRecordFields["systemuseraliastypeid"]);
            Assert.AreEqual(false, systemUserAliasesRecordFields["preferredname"]);


            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertQuickSearchText("CP_Automation1_Testing SystemUser")
               .ClickQuickSearchButton()
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
                .InsertQuickSearchText("CP_Automation1_Testing SystemUser")
                .ClickQuickSearchButton()
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


        [TestProperty("JiraIssueID", "CDV6-14028")]

        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "User should have 2 Aliases records" +
          "Validate the search for First name and Last name" + "Validate the sort functionality for Aliases type, First name and last name " +
            "Validate the 2 records and its position" + "Validate Export to excel option")]


        [TestMethod]
        public void SystemUser_RelatedItems_Aliases_UITestCases_UITestMethod003()
        {

            Guid UserAliases01 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType1, "QA", "CareProvider", "Testing");

            Guid UserAliases02 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType2, "Caredirector", "Automation", "TestingQA");

            #region Step 16

            loginPage
                 .GoToLoginPage()
                 .Login("CW_Admin_Test_User_1", "Passw0rd_!", "Care Providers")
                 .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertQuickSearchText("CP_SystemUser1")
               .ClickQuickSearchButton()
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
                .ClickSearchFieldLookUpButton()
                .ValidateRecordCellText(userAliasRecord[0].ToString(), 3, "QA");


            systemUserAliasesPage
                 .WaitForSystemUserAliasesPageToLoad()
                 .InsertSearchFieldText("TestingQA")
                 .ClickSearchFieldLookUpButton()
                 .WaitForSystemUserAliasesPageToLoad()
                 .WaitForSystemUserAliasesPageToLoad()
                 .ValidateRecordCellText(userAliasRecord[1].ToString(), 5, "TestingQA");

            #endregion Step 16

            #region Step 17

            systemUserAliasesPage
               .WaitForSystemUserAliasesPageToLoad()
               .InsertSearchFieldText("")
               .ClickSearchFieldLookUpButton()
               .WaitForSystemUserAliasesPageToLoad()
               .WaitForSystemUserAliasesPageToLoad()
               .ValidateRecordPosition(1, userAliasRecord[1].ToString())
               .ValidateRecordPosition(2, userAliasRecord[0].ToString());
            #endregion Step 17


            #region Step 18

            systemUserAliasesPage
               .WaitForSystemUserAliasesPageToLoad()
               .ClickAliasTypeSort()
               .ValidateRecordPosition(1, userAliasRecord[0].ToString())
               .ValidateRecordPosition(2, userAliasRecord[1].ToString())
               .WaitForSystemUserAliasesPageToLoad()
               .ClickFirstNameSort()
               .ValidateRecordPosition(1, userAliasRecord[1].ToString())
               .ValidateRecordPosition(2, userAliasRecord[0].ToString())
               .WaitForSystemUserAliasesPageToLoad()
               .ClickLastNameSort()
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

        [TestProperty("JiraIssueID", "CDV6-14029")]

        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "User should have 2 Aliases records" +
       "Validate the delete functionality")]


        [TestMethod]
        public void SystemUser_RelatedItems_Aliases_UITestCases_UITestMethod004()
        {

            Guid UserAliases01 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType1, "QA", "CareProvider", "Testing");

            Guid UserAliases02 = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType2, "Caredirector", "Automation", "TestingQA");

            #region Step 19

            loginPage
                  .GoToLoginPage()
                  .Login("CW_Admin_Test_User_1", "Passw0rd_!", "Care Providers")
                  .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertQuickSearchText("CP_SystemUser1")
               .ClickQuickSearchButton()
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

        [TestProperty("JiraIssueID", "CDV6-14029")]

        [Description("Open the System user record, Navigate to Menu  -> Related items ->Aliases" + "User should update the existing record" +
      "Validate the changes updated in Audit page")]


        [TestMethod]
        public void SystemUser_RelatedItems_Aliases_UITestCases_UITestMethod005()
        {

            Guid UserAliases = dbHelper.systemUserAlias.CreateSystemUserAlias(_careProviders_TeamId, _systemUserId02, _systemUserAliasType1, "QA", "CareProvider", "Testing");


            #region Step 21

            loginPage
                  .GoToLoginPage()
                  .Login("CW_Admin_Test_User_1", "Passw0rd_!", "Care Providers")
                  .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertQuickSearchText("CP_SystemUser1")
               .ClickQuickSearchButton()
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
               .InsertQuickSearchText("CP_SystemUser1")
               .ClickQuickSearchButton()
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
               .WaitForAuditListPageToLoad("systemuseralias");

            System.Threading.Thread.Sleep(3000);

            var updateAudits = dbHelper.audit.GetAuditByRecordID(userAliasRecord[0], 2);//get all update operations
            Assert.AreEqual(1, updateAudits.Count);


            auditListPage
               .ValidateRecordPresent(updateAudits.First().ToString())
               .ClickOnAuditRecord(updateAudits.First().ToString());

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Update")
                .ValidateChangedBy("CW Admin User 1");



            #endregion Step 21

        }
        #endregion

    }

}


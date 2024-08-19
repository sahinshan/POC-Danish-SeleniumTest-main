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
    /// https://advancedcsg.atlassian.net/browse/CDV6-11132
    ///
    /// </summary>
    [TestClass]
    public class SystemUser_RelatedItem_EmergencyContacts_UITestCases : FunctionalTest
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

            #endregion  Business Unit

            #region Team

            var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
            if (!teamsExist)
                dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
            _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

            #endregion Team

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Lanuage

            #region To delete System user :Automation_Testing_SystemUser_EmergencyContacts01, Address & Aliases Record related to the Person


            int systemUser = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts01").Count();
            if (systemUser == 1)
            { //To delete System user Address
                foreach (var systemUserId in dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts01"))
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



            #endregion To delete System user :Automation_Testing_SystemUser_EmergencyContacts01, Address & Aliases Record related to the Person






            #region Create Automation_Testing_SystemUser_EmergencyContacts01 Record

            var newSystemUser01 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts01").Any();
            if (!newSystemUser01)
                dbHelper.systemUser.CreateSystemUser("Automation_Testing_SystemUser_EmergencyContacts01", "Automation_Testing", "SystemUser", "EmergencyContacts01", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
            _systemUserId01 = dbHelper.systemUser.GetSystemUserByUserName("Automation_Testing_SystemUser_EmergencyContacts01")[0];


            #endregion  Create SystemUser01 Record


           





        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-13422

        [TestProperty("JiraIssueID", "")]

        [Description("")]


        [TestMethod]
        public void SystemUser_RelatedItems_EmergencyContacts_UITestCases_UITestMethod001()
        {
            loginPage
               .GoToLoginPage()
               .Login("CW_Admin_Test_User_1", "Passw0rd_!", "Care Providers")
               .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
               .WaitForSystemUsersPageToLoad()
               .InsertQuickSearchText("Automation_Testing_SystemUser_EmergencyContacts01")
               .ClickQuickSearchButton()
               .WaitForResultsGridToLoad()
               .OpenRecord(_systemUserId01.ToString());


            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToRelatedItemsSubPage_EmergencyContacts();

            systemUserEmergencyContactsPage
                .WaitForSystemUserEmergencyContactsPageToLoad()
                .ValidateNoRecordMessageVisibile(true)
                .ClickAddNewButton();

            systemUserEmergencyContactsRecordPage
                .WaitForSystemUserEmergencyContactsRecordPageToLoad()
                .ValidateSystemUserEmergencyContactsRecordPageTitle("New")
                .ValidateSystemUser_Editable(true)
                .ValidateSystemUser_LinkField("Automation_Testing SystemUser")
                .ValidateSystemUser_MandatoryField()
                .ValidateTitle_Field()
                .ValidateTitle_Field_Text("")
                .ValidateFirstName_Field()
                .ValidateFirstName_Field_Text("")
                .ValidateLastName_Field()
                .ValidateLastName_Field_Text("")
                .ValidateStartDate_Field()
              //  .ValidateStartDate_Field_Text(DateTime.Now.ToString("dd / MM / yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .ValidateNextOfKin_MandatoryField()
                .ValidateNextOfKin_NoOption()
                .ValidateContactTelehonePrimary_Field()
                .ValidateContactTelehonePrimary_Field_Text("")
                .ValidateContactTelehonePrimary_MandatoryField()
                .ValidateContactTelehoneOther1_Field()
                .ValidateContactTelehoneOther1_Field_Text("")
                .ValidateContactTelehoneOther2_Field()
                .ValidateContactTelehoneOther2_Field_Text("")
                .ValidateContactTelehoneOther3_Field()
                .ValidateContactTelehoneOther3_Field_Text("")
                .ValidateEndDate_Field()
                .ValidateEndDate_Field_Text("");



        }
        #endregion
    }

}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
namespace Phoenix.UITests.Settings.Configuration
{
    /// <summary>
    /// This class contains Automated UI test scripts for
    /// </summary>
    [TestClass]
    public class Option_Sets_RecruitmentDocumentStatus_UITestCases : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid optionSetID;
        private Guid Outstanding_OptionsetValueId;
        private Guid Requested_OptionsetValueId;
        private Guid Completed_OptionsetValueId;
        private Guid NotProgressed_OptionsetValueId;
        private Guid Override_OptionsetValueId;
        private Guid Expired_OptionsetValueId;
        private string _loginUsername;
        private string _loginUserFullname;
        private string _userName = "Login_User_" + DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(tenantName);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Create default system user

                _defaultLoginUserID = dbHelper.systemUser.CreateSystemUser(_userName, "Login_", "Automation_User", "Login Automation User", "Passw0rd_!", "Login_Automation_User@somemail.com", "Login_Automation_User@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId, true, 4);

                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);
                _loginUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];

                #endregion

                #region Option Set (RecruitmentDocumentStatus)

                optionSetID = dbHelper.optionSet.GetOptionSetIdByName("RecruitmentDocumentStatus")[0];

                #endregion

                #region Optionset Values

                Outstanding_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Outstanding").FirstOrDefault();
                Requested_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Requested").FirstOrDefault();
                Completed_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Completed").FirstOrDefault();
                NotProgressed_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Not Progressed").FirstOrDefault();
                Override_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Override").FirstOrDefault();
                Expired_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Expired").FirstOrDefault();

                #endregion


            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-21081

        [TestProperty("JiraIssueID", "ACC-3370")]
        [Description("Login CD as a Care Provider  -> Settings -> Configuration -> Customizations -> Option Sets " +
            "Check and Validate the pick list option set for Recruitment Requirement status.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "System Setupdata")]
        [TestProperty("Screen", "Option Sets")]
        public void Option_Sets_RecruitmentDocumentStatus_UITestCases01()
        {

            loginPage
              .GoToLoginPage()
              .Login(_loginUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickOptionSetsButton();

            optionSetsPage
                .WaitForOptionSetsPageToLoad()
                .InsertQuickSearchText("RecruitmentDocumentStatus")
                .ClickQuickSearchButton()
                .OpenRecord(optionSetID.ToString());

            optionSetsRecordPage
                .WaitForOptionSetsRecordPageToLoad()
                .ValidateOptionSetTextValue("RecruitmentDocumentStatus")
                .NavigateToOptionSetValuesPage();

            optionsetValuesPage
                .WaitForOptionsetValuesPageToLoad()
                .ValidateDisplayOrder_AvailableOption_CustomizableOptionOfOutstandingOptionsetValue(Outstanding_OptionsetValueId.ToString())
                .ValidateDisplayOrder_AvailableOption_CustomizableOptionOfRequestedOptionsetValue(Requested_OptionsetValueId.ToString())
                .ValidateDisplayOrder_AvailableOption_CustomizableOptionOfCompletedOptionsetValue(Completed_OptionsetValueId.ToString())
                .ValidateDisplayOrder_AvailableOption_CustomizableOptionOfNotProgressedOptionsetValue(NotProgressed_OptionsetValueId.ToString())
                .ValidateDisplayOrder_AvailableOption_CustomizableOptionOfOverrideOptionsetValue(Override_OptionsetValueId.ToString())
                .ValidateDisplayOrder_AvailableOption_CustomizableOptionOfExpiredOptionsetValue(Expired_OptionsetValueId.ToString());

            optionsetValuesPage
                .OpenRecord(Outstanding_OptionsetValueId.ToString());

            optionsetValueRecordPage
                .WaitForOptionsetValueRecordPageToLoad()
                .ValidateAll_Fields_Disabled(true)
                .ValidateOptionSetFieldValue(optionSetID.ToString())
                .ValidateTextFieldValue("Outstanding")
                .ValidateDisplayOrderFieldValue("1");

        }

        #endregion
    }
}
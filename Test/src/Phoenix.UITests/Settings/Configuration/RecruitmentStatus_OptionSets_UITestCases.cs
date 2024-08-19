using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration
{
    [TestClass]
    public class RecruitmentStatus_OptionSets_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid optionSetID;
        private Guid Pending_OptionSetValueId;
        private Guid Induction_OptionSetValueId;
        private Guid FullyAccepted_OptionSetValueId;
        private Guid Rejected_OptionSetValueId;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                string user = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(user)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "907678", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion


                #region Create SystemUser Record

                _systemUserName = "RecruitmentStatus_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "RecruitmentStatus", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Option Set (RecruitmentDocumentStatus)

                optionSetID = dbHelper.optionSet.GetOptionSetIdByName("RecruitmentStatus")[0];

                #endregion

                #region Optionset Values

                Pending_OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Pending").FirstOrDefault();
                Induction_OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Induction").FirstOrDefault();
                FullyAccepted_OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Fully Accepted").FirstOrDefault();
                Rejected_OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Rejected").FirstOrDefault();

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

        #region https://advancedcsg.atlassian.net/browse/ACC-3050

        [TestProperty("JiraIssueID", "ACC-3629")]
        [Description("Login CD as a Care Provider  -> Settings -> Configuration -> Customizations -> Option Sets " +
                     "Check and Validate the pick list option set for Recruitment Status.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "System Setupdata")]
        [TestProperty("Screen1", "Option Sets")]
        public void RecruitmentStatus_OptionSets_UITestCases01()
        {

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCustomizationsSection();

            customizationsPage
                .WaitForCustomizationsPageToLoad()
                .ClickOptionSetsButton();

            optionSetsPage
                .WaitForOptionSetsPageToLoad()
                .InsertQuickSearchText("RecruitmentStatus")
                .ClickQuickSearchButton()
                .OpenRecord(optionSetID.ToString());

            optionSetsRecordPage
                .WaitForOptionSetsRecordPageToLoad()
                .ValidateOptionSetTextValue("RecruitmentStatus")
                .NavigateToOptionSetValuesPage();

            optionsetValuesPage
                .WaitForOptionsetValuesPageToLoad()

                .ValidateOptionSet_DisplayName(Pending_OptionSetValueId.ToString(), "Pending")
                .ValidateOptionSet_DisplayName(Induction_OptionSetValueId.ToString(), "Induction")
                .ValidateOptionSet_DisplayName(FullyAccepted_OptionSetValueId.ToString(), "Fully Accepted")
                .ValidateOptionSet_DisplayName(Rejected_OptionSetValueId.ToString(), "Rejected")

                .ValidateOptionSet_DisplayOrder(Pending_OptionSetValueId.ToString(), 1)
                .ValidateOptionSet_DisplayOrder(Induction_OptionSetValueId.ToString(), 2)
                .ValidateOptionSet_DisplayOrder(FullyAccepted_OptionSetValueId.ToString(), 3)
                .ValidateOptionSet_DisplayOrder(Rejected_OptionSetValueId.ToString(), 4)

                .ValidateOptionSet_CustomizableOption(Pending_OptionSetValueId.ToString(), "No")
                .ValidateOptionSet_CustomizableOption(Induction_OptionSetValueId.ToString(), "Yes")
                .ValidateOptionSet_CustomizableOption(FullyAccepted_OptionSetValueId.ToString(), "No")
                .ValidateOptionSet_CustomizableOption(Rejected_OptionSetValueId.ToString(), "No")

                .ValidateOptionSet_AvailableOption(Pending_OptionSetValueId.ToString(), "Yes")
                .ValidateOptionSet_AvailableOption(Induction_OptionSetValueId.ToString(), "Yes")
                .ValidateOptionSet_AvailableOption(FullyAccepted_OptionSetValueId.ToString(), "Yes")
                .ValidateOptionSet_AvailableOption(Rejected_OptionSetValueId.ToString(), "Yes");


            optionsetValuesPage
                .OpenRecord(FullyAccepted_OptionSetValueId.ToString());

            optionsetValueRecordPage
                .WaitForOptionsetValueRecordPageToLoad()
                .ValidateAll_Fields_Disabled(true)
                .ValidateOptionSetFieldValue(optionSetID.ToString())
                .ValidateTextFieldValue("Fully Accepted")
                .ValidateDisplayOrderFieldValue("3");

        }

        #endregion
    }
}
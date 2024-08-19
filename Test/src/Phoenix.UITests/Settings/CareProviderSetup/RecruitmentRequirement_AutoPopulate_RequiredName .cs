using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.CareProviderSetup
{
    /// <summary>
    /// This class contains Automated UI test scripts for Auto populate Required Name
    /// </summary>
    [TestClass]
    public class RecruitmentRequirement_AutoPopulate_RequiredName : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _systemUserId;
        private string _systemUserName;
        private Guid _standardAvailabilityTypeId;
        private Guid _regularAvailabilityTypeId;
        private Guid _overtimeAvailabilityTypeId;
        private string _staffRecruitmentItem1Name;
        private Guid _staffRecruitmentItem1Id;
        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string RequirementName = "RequirementName";
        private string RequirementNameEdit = "RequirementNameEdit";
        private string _careProviderStaffRoleTypeName;
        private Guid _careProviderStaffRoleTypeId;

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
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

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

                #region System User

                _systemUserName = "APRN_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "AvailabilityTypes", "User1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _systemUserId);

                #endregion

                #region Staff Recruitment Item

                _staffRecruitmentItem1Name = "ACC-1178" + currentDateTime;

                _staffRecruitmentItem1Id = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careProviders_TeamId, _staffRecruitmentItem1Name, new DateTime(2020, 1, 1));

                #endregion

                _careProviderStaffRoleTypeName = "ACC-1178" + currentDateTime;
                _careProviderStaffRoleTypeId = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleTypeName, null, null, new DateTime(2020, 1, 1), null);

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region https: //advancedcsg.atlassian.net/browse/ACC-1178

        [TestProperty("JiraIssueID", "ACC-1178")]
        [Description("when the User selects Required Item, the Required Name field is auto-filled with a text from the Required Item fielddon’t auto - populate when the Required Name field is not emptythe Requirement Name field is still editable")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void AvailabilityTypes_UITestMethod001()
        {

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2 - Step 3

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickRequiredItemTypeLookUpButton();

            #endregion

            #region Step 4

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItem1Name).TapSearchButton().SelectResultElement(_staffRecruitmentItem1Id.ToString());

            recruitmentRequirementRecordPage
                .ValidateRequirementNameValue(_staffRecruitmentItem1Name);

            #endregion

            #region Step 5

            recruitmentRequirementRecordPage
                .InsertRequirementName(RequirementName)
                .ValidateRequirementNameValue(RequirementName)
                .ClickBackButton();

            alertPopup
              .WaitForAlertPopupToLoad()
              .TapOKButton();

            #endregion

            #region Step 6 repeat step2 - 3

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
              .WaitForRecruitmentRequirementsPageToLoad()
              .ClickNewRecordButton();

            #endregion

            #region Step 7

            recruitmentRequirementRecordPage
              .WaitForRecruitmentRequirementRecordPageToLoad()
              .InsertRequirementName(RequirementName)
              .ClickRequiredItemTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItem1Name).TapSearchButton().SelectResultElement(_staffRecruitmentItem1Id.ToString());

            recruitmentRequirementRecordPage
              .ValidateRequirementNameValue(RequirementName)
              .ClickAllRolesNoRadioButton()
              .ClickRolesLookUpButton();

            lookupMultiSelectPopup
              .WaitForLookupMultiSelectPopupToLoad()
              .TypeSearchQuery(_careProviderStaffRoleTypeName)
              .TapSearchButton()
              .SelectResultElement(_careProviderStaffRoleTypeId.ToString());

            #endregion

            #region Step 8

            recruitmentRequirementRecordPage
              .WaitForRecruitmentRequirementRecordPageToLoad()
              .SelectStatusForInduction("Completed")
              .SelectStatusForAcceptance("Completed")
              .ClickSaveButton()
              .WaitForRecordToBeSaved();

            recruitmentRequirementRecordPage
              .WaitForRecruitmentRequirementRecordPageToLoad()
              .InsertRequirementName(RequirementNameEdit)
              .ValidateRequirementNameValue(RequirementNameEdit)
              .ClickSaveButton()
              .WaitForRecordToBeSaved()
              .ValidateRequirementNameValue(RequirementNameEdit);

            #endregion
        }

        #endregion
    }
}
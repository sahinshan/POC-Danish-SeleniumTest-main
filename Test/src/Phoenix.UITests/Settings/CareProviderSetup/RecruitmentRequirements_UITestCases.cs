using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class RecruitmentRequirements_UITestCases : FunctionalTest
    {

        #region Private Properties

        private string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _systemUserId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private string _username = "CW_Admin_Test_User_CDV6_20506";

        private Guid _staffRecruitmentItem1Id;
        private string _staffRecruitmentItem1Name;
        private Guid _careProviderStaffRoleType1Id;
        private string _careProviderStaffRoleType1Name;

        private Guid _staffRecruitmentItem2Id;
        private string _staffRecruitmentItem2Name;
        private Guid _careProviderStaffRoleType2Id;
        private string _careProviderStaffRoleType2Name;

        #endregion


        #region Private Methods

        private void DataInitialization1()
        {

            #region Staff Recruitment Item

            _staffRecruitmentItem1Name = "CDV6_20506_Item_1_" + currentDateTime;
            _staffRecruitmentItem2Name = "CDV6_20506_Item_2_" + currentDateTime;

            _staffRecruitmentItem1Id = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careDirectorQA_TeamId, _staffRecruitmentItem1Name, new DateTime(2020, 1, 1));
            _staffRecruitmentItem2Id = dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(_careDirectorQA_TeamId, _staffRecruitmentItem2Name, new DateTime(2020, 1, 1));

            #endregion

            #region Care Provider Staff Role Type

            _careProviderStaffRoleType1Name = "CDV6_20506_Role_1_" + currentDateTime;
            _careProviderStaffRoleType2Name = "CDV6_20506_Role_2_" + currentDateTime;

            _careProviderStaffRoleType1Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, _careProviderStaffRoleType1Name, null, null, new DateTime(2020, 1, 1), null);
            _careProviderStaffRoleType2Id = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, _careProviderStaffRoleType2Name, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create SystemUser Record

            if (!dbHelper.systemUser.GetSystemUserByUserName(_username).Any())
                _systemUserId = dbHelper.systemUser.CreateSystemUser(_username, "CW_Admin", "Test_User_CDV6_20506", "CW_Admin Test_User_CDV6_20506", "Passw0rd_!", "CW_Admin_Test_User_CDV6_20506@somemail.com", "CW_Admin_Test_User_CDV6_20506@somemail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, true, 4);

            _systemUserId = dbHelper.systemUser.GetSystemUserByUserName(_username).First();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);

            #endregion

        }

        #endregion


        [TestInitialize()]
        public void RecruitmentRequirements_SetupMethod()
        {
            try
            {
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new DBHelper.DatabaseHelper(tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careDirectorQA_BusinessUnitId, null, "CareDirectorQA@careworkstempmail.com", "Default team for business unit", null);
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-20506

        [TestProperty("JiraIssueID", "ACC-3244")]
        [Description("Steps 1 to 3 from the original test CDV6-12315")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirements_UITestMethod01()
        {
            DataInitialization1();

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad();

            var recruitmentRequirements = dbHelper.recruitmentRequirement.GetAll();
            Assert.IsTrue(recruitmentRequirements.Count >= 0);

        }

        [TestProperty("JiraIssueID", "ACC-3245")]
        [Description("Steps 4 and 5 from the original test CDV6-12315")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirements_UITestMethod02()
        {
            DataInitialization1();

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickSaveButton();

            recruitmentRequirementRecordPage
                .ValidateRequirementNameFieldErrorLabelVisibility(true)
                .ValidateRequiredItemTypeFieldErrorLabelVisibility(true)
                .ValidateStatusForInductionFieldErrorLabelVisibility(true)
                .ValidateStatusForAcceptanceFieldErrorLabelVisibility(true)
                .ValidateRequirementNameFieldErrorLabelText("Please fill out this field.")
                .ValidatRequiredItemTypeFieldErrorLabelText("Please fill out this field.")
                .ValidatStatusForInductionFieldErrorLabelText("Please fill out this field.")
                .ValidatStatusForAcceptanceFieldErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "ACC-3247")]
        [Description("Steps 6 to 14 from the original test CDV6-12315")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirements_UITestMethod03()
        {
            DataInitialization1();

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            #region Step 6

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItem1Name).TapSearchButton().SelectResultElement(_staffRecruitmentItem1Id.ToString());

            #endregion

            #region Step 8

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateAllRolesYesRadionButtonChecked(true)
                .ValidateAllRolesNoRadionButtonChecked(false)
                .ValidateRolesLookupButtonVisibility(false)

                .ClickAllRolesNoRadioButton()
                .ValidateRolesLookupButtonVisibility(true)
                .ClickRolesLookUpButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(_careProviderStaffRoleType1Name).TapSearchButton().SelectResultElement(_careProviderStaffRoleType1Id.ToString());

            #endregion

            #region Step 9

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .SelectStatusForInduction("Completed") //any of the 2 status can be selected
                .SelectStatusForAcceptance("Completed")
                .SelectStatusForInduction("Requested")
                .SelectStatusForAcceptance("Requested");

            #endregion

            #region Step 10

            recruitmentRequirementRecordPage
                .ValidateNoRequiredForInductionFieldErrorLabelVisibility(false)
                .ValidateNoRequiredForAcceptanceFieldErrorLabelVisibility(false)
                .InsertNoRequiredForInduction("4")
                .InsertNoRequiredForAcceptance("4")
                .ValidateNoRequiredForInductionFieldErrorLabelVisibility(true)
                .ValidateNoRequiredForAcceptanceFieldErrorLabelVisibility(true)
                .ValidateNoRequiredForInductionFieldErrorLabelText("Please enter a value between 0 and 3.")
                .ValidateNoRequiredForAcceptanceFieldErrorLabelText("Please enter a value between 0 and 3.")
                .InsertNoRequiredForInduction("3")
                .InsertNoRequiredForAcceptance("3")
                .ValidateNoRequiredForInductionFieldErrorLabelVisibility(false)
                .ValidateNoRequiredForAcceptanceFieldErrorLabelVisibility(false);

            #endregion

            #region Step 12

            recruitmentRequirementRecordPage
                .InsertStartDate("19/09/2022")
                .InsertEndDate("18/09/2022");

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("End Date should be greater than or equal to Start Date").TapOKButton();

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertEndDate("20/09/2022");

            #endregion

            #region Step 13

            recruitmentRequirementRecordPage
                .ClickSaveButton();

            #endregion

            #region Step 14

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickSaveAndCloseButton();

            recruitmentRequirementsPage
                    .WaitForRecruitmentRequirementsPageToLoad()
                    .ClickRecordIdHeaderLink() //order records in ascending way
                    .WaitForRecruitmentRequirementsPageToLoad()
                    .ClickRecordIdHeaderLink(); //order records in a descending way (newer first)

            var records = dbHelper.recruitmentRequirement.GetByName(_staffRecruitmentItem1Name);
            Assert.AreEqual(1, records.Count);

            recruitmentRequirementsPage
                    .WaitForRecruitmentRequirementsPageToLoad()
                    .OpenRecord(records[0].ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateRequirementNameValue(_staffRecruitmentItem1Name)
                .ValidateRequiredItemTypeLinkText(_staffRecruitmentItem1Name)
                .ValidateStartDateValue("19/09/2022")
                .ValidateEndDateValue("20/09/2022")
                .ValidateAllRolesYesRadionButtonChecked(false)
                .ValidateAllRolesNoRadionButtonChecked(true)
                .ValidateRolesMultiSelectElementText(_careProviderStaffRoleType1Id, _careProviderStaffRoleType1Name)
                .ValidateNoRequiredForInductionValue("3")
                .ValidateNoRequiredForAcceptanceValue("3")
                .ValidateStatusForInductionSelectedText("Requested")
                .ValidateStatusForAcceptanceSelectedText("Requested");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3248")]
        [Description("Step 15 from the original test CDV6-12315")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirements_UITestMethod04()
        {
            DataInitialization1();

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleType1Id };
            var recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careDirectorQA_TeamId, _staffRecruitmentItem1Name, _staffRecruitmentItem1Id, "staffrecruitmentitem", _staffRecruitmentItem1Name, false, 1, 3, 1, 3, new DateTime(2022, 1, 1), null, careProviderStaffRoleTypeIds);

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            #region Step 15

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItem2Name).TapSearchButton().SelectResultElement(_staffRecruitmentItem2Id.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertStartDate("20/09/2022")
                .SelectStatusForInduction("Requested")
                .SelectStatusForAcceptance("Requested")
                .ClickSaveAndCloseButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink() //order records in ascending way
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink(); //order records in a descending way (newer first)

            var records = dbHelper.recruitmentRequirement.GetByName(_staffRecruitmentItem2Name);
            Assert.AreEqual(1, records.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3250")]
        [Description("Step 16 and 17 from the original test CDV6-12315")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirements_UITestMethod05()
        {
            DataInitialization1();

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleType1Id };
            var recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careDirectorQA_TeamId, _staffRecruitmentItem1Name, _staffRecruitmentItem1Id, "staffrecruitmentitem", _staffRecruitmentItem1Name, false, 1, 3, 1, 3, new DateTime(2022, 1, 1), null, careProviderStaffRoleTypeIds);

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink() //order ascending
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink() //order descending
                .OpenRecord(recruitmentRequirementId.ToString());

            #region Step 16 - 17

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickRolesLookUpButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(_careProviderStaffRoleType2Name).TapSearchButton().SelectResultElement(_careProviderStaffRoleType2Id.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertEndDate("21/09/2022")
                .ClickSaveAndCloseButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .OpenRecord(recruitmentRequirementId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateEndDateValue("21/09/2022")
                .ValidateRolesMultiSelectElementText(_careProviderStaffRoleType1Id, _careProviderStaffRoleType1Name)
                .ValidateRolesMultiSelectElementText(_careProviderStaffRoleType2Id, _careProviderStaffRoleType2Name)
                .ClickSaveAndCloseButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .SelectView("Inactive Records")
                .ClickRecordIdHeaderLink() //order ascending
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink() //order descending
                .WaitForRecruitmentRequirementsPageToLoad()
                .ValidateRecordIsPresent(recruitmentRequirementId.ToString());

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .SelectView("Active Records")
                .ClickRecordIdHeaderLink(); //order ascending

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink() //order descending
                .WaitForRecruitmentRequirementsPageToLoad()
                .ValidateRecordIsNotPresent(recruitmentRequirementId.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3252")]
        [Description("Step 18 from the original test CDV6-12315")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirements_UITestMethod06()
        {
            DataInitialization1();

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid>() { _careProviderStaffRoleType1Id };
            var recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(_careDirectorQA_TeamId, _staffRecruitmentItem1Name, _staffRecruitmentItem1Id, "staffrecruitmentitem", _staffRecruitmentItem1Name, false, 1, 3, 1, 3, new DateTime(2022, 1, 1), null, careProviderStaffRoleTypeIds);

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            #region Step 18

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink() //order ascending
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickRecordIdHeaderLink() //order descending
                .OpenRecord(recruitmentRequirementId.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();
            System.Threading.Thread.Sleep(1000);

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ValidateRecordIsNotPresent(recruitmentRequirementId.ToString());

            #endregion

        }

        #endregion


        #region https://advancedcsg.atlassian.net/browse/ACC-3684

        [TestProperty("JiraIssueID", "ACC-1178")]
        [Description("")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Recruitment")]
        [TestProperty("Screen1", "Recruitment Requirements")]
        public void RecruitmentRequirements_UITestMethod07()
        {
            #region Create SystemUser Record

            _username = "User_ACC_1178";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_username, "User", "ACC_1178", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);

            #endregion

            #region Staff Recruitment Item

            _staffRecruitmentItem1Name = "SRI_ACC_1178";
            _staffRecruitmentItem1Id = commonMethodsDB.CreateStaffRecruitmentItem(_careDirectorQA_TeamId, _staffRecruitmentItem1Name, new DateTime(2020, 1, 1));

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_username, "Passw0rd_!", "Care Providers");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToRecruitmentRequirementSetup();

            #endregion

            #region Step 3

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            #endregion

            #region Step 4

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItem1Name).TapSearchButton().SelectResultElement(_staffRecruitmentItem1Id.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateRequirementNameValueViaJavascript(_staffRecruitmentItem1Name);

            #endregion

            #region Step 5

            recruitmentRequirementRecordPage
                .InsertRequirementName("ACC-1178 changed name");

            #endregion

            #region Step 6

            recruitmentRequirementRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            recruitmentRequirementsPage
                .WaitForRecruitmentRequirementsPageToLoad()
                .ClickNewRecordButton();

            #endregion

            #region Step 7

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .InsertRequirementName("Pre Populated Value")
                .ClickRequiredItemTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_staffRecruitmentItem1Name).TapSearchButton().SelectResultElement(_staffRecruitmentItem1Id.ToString());

            recruitmentRequirementRecordPage
                .WaitForRecruitmentRequirementRecordPageToLoad()
                .ValidateRequirementNameValueViaJavascript("Pre Populated Value");

            #endregion

            #region Step 8

            //already validated in steps 3 to 7

            #endregion

        }

        #endregion
    }
}

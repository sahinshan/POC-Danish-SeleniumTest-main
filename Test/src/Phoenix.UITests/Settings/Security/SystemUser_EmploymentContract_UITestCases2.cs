using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_EmploymentContract_UITestCases2 : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private TimeZone _localZone;


        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("EmploymentContract BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("EmploymentContract T1", null, _businessUnitId, "907678", "EmploymentContractT1@careworkstempmail.com", "EmploymentContract T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User EmploymentContractUser1

                _systemUsername = "EmploymentContractUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-1798

        [TestProperty("JiraIssueID", "ACC-1846")]
        [Description("Step(s) 1 to 3 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod001()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldEnabled(false)
                .ValidateStatusFieldSelectedText("Not Started")
                .ClickRoleLoopUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Helper").TapSearchButton().SelectResultElement(_careProviderStaffRoleTypeId);

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ClickTypeLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Contracted").TapSearchButton().SelectResultElement(_employmentContractTypeid.ToString());

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .InsertStartDate("", "")
               .InsertDescription("testing ACC-835")
               .ClickResponsibleTeamLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("EmploymentContract T1").TapSearchButton().SelectResultElement(_teamId.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertContractHoursPerWeek("40")
                .InsertFixedWorkingPatternCycle("1")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            var employmentContracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(newUserId);
            Assert.AreEqual(1, employmentContracts.Count);
            var employmentContractId = employmentContracts[0];

            systemUserEmploymentContractsPage
                .OpenRecord(employmentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Not Started");

            #endregion

            #region Step 2

            var matchingStaffReviewSetupFound = dbHelper.staffReviewSetup.GetRecordsForAllRoles().Any();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);

            systemUserEmploymentContractsRecordPage
                .InsertStartDate(startDate.ToString("dd'/'MM'/'yyyy"), "00:00")
                .ClickSaveAndCloseButton();



            if (matchingStaffReviewSetupFound)
            {
                staffReviewRequirementsPopup
                    .WaitForStaffReviewRequirementsPopupToLoad()
                    .ClickSaveButton();

                confirmDynamicDialogPopup
                    .WaitForConfirmDynamicDialogPopupToLoad()
                    .ValidateMessage("No reviews have been selected, do you wish to proceed?")
                    .TapOKButton();
            }

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(employmentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active");

            #endregion

            #region Step 3

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate("", "")
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(employmentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Not Started");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1847")]
        [Description("Step(s) 4 to 8 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod002()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active")
                .InsertStartDate(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"), "")
                .InsertDescription("test ...")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Not Started");

            #endregion

            #region Step 5

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate(DateTime.Now.AddDays(-10).ToString("dd'/'MM'/'yyyy"), "")
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            //Create suspension with a past date
            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended");

            #endregion

            #region Step 6

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-6)).Date;
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active");

            #endregion

            #region Step 7

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, null, contracts);

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended");

            #endregion

            #region Step 8

            dbHelper.systemUserSuspension.DeleteSystemUserSuspension(suspensionId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1848")]
        [Description("Step(s) 9 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod003()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            //Create suspension with a past date
            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var systemUserSuspensionEndDate = systemUserSuspensionStartDate.AddDays(2);
            var contracts = new List<Guid> { systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active")
                .InsertEndDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertDescription("test ...")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended");

            #endregion



        }

        [TestProperty("JiraIssueID", "ACC-1849")]
        [Description("Step(s) 10 to 12 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod004()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            //Create suspension with a past date
            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var systemUserSuspensionEndDate = systemUserSuspensionStartDate.AddDays(2);
            var contracts = new List<Guid> { systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended")
                .ValidateStartDateFieldEnabled(false);

            #endregion

            #region Step 11

            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active")
                .ValidateStartDateFieldEnabled(false);

            #endregion

            #region Step 12

            systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(6));
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended");

            dbHelper.systemUserSuspension.DeleteSystemUserSuspension(suspensionId);

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1850")]
        [Description("Step(s) 13 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod005()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            //Create suspension with a past date
            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var systemUserSuspensionEndDate = systemUserSuspensionStartDate.AddDays(4);
            var contracts = new List<Guid> { systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active");

            dbHelper.systemUserSuspension.DeleteSystemUserSuspension(suspensionId);

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1851")]
        [Description("Step(s) 14 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod006()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension 1

            //Create suspension with a past date
            var systemUserSuspension1StartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var systemUserSuspension1EndDate = systemUserSuspension1StartDate.AddDays(4);
            var contracts1 = new List<Guid> { systemUserEmploymentContractId };
            var suspension1Id = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspension1StartDate, _teamId, systemUserSuspensionReasonId, contracts1);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspension1Id, systemUserSuspension1EndDate, contracts1);

            #endregion

            #region System User Suspension 2

            //Create suspension with a past date
            var systemUserSuspension2StartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-3)).Date;
            var systemUserSuspension2EndDate = systemUserSuspension2StartDate.AddDays(6);
            var contracts2 = new List<Guid> { systemUserEmploymentContractId };
            var suspension2Id = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspension2StartDate, _teamId, systemUserSuspensionReasonId, contracts2);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspension2Id, systemUserSuspension2EndDate, contracts2);

            #endregion

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended");

            dbHelper.systemUserSuspension.DeleteSystemUserSuspension(suspension1Id);
            dbHelper.systemUserSuspension.DeleteSystemUserSuspension(suspension2Id);

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1852")]
        [Description("Step(s) 15 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod007()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            //Create suspension with a past date
            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended")
                .InsertDescription("test ...")
                .InsertEndDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad();

            endContractWithSuspensionDatePopup
                .WaitForEndContractWithSuspensionDatePopupToLoad()
                .InsertEndDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "00:00")
                .ClickOkButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1853")]
        [Description("Step(s) 16 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod008()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion


            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active")
                .InsertDescription("test ...")
                .InsertEndDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickContractEndReasonLoopUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Unknown reason").TapSearchButton().SelectResultElement(contractEndReasonId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended")
                .InsertStartDate("", "")
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Not Started")
                .ValidateStartDate_FieldText("")
                .ValidateEndDate_FieldText("")
                .ValidateContractEndReason_LinkField("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1854")]
        [Description("Step(s) 17 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod009()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion


            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active")
                .InsertDescription("test ...")
                .InsertEndDate(DateTime.Now.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickContractEndReasonLoopUpButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Unknown reason").TapSearchButton().SelectResultElement(contractEndReasonId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended")
                .InsertStartDate(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"), "")
                .InsertEndDate("")
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Not Started")
                .ValidateStartDate_FieldText(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate_FieldText("")
                .ValidateContractEndReason_LinkField("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1855")]
        [Description("Step(s) 18 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod010()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContractId, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Step 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended")
                .InsertDescription("test ...")
                .InsertEndDate("")
                .InsertEndTime("")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active")
                .ValidateStartDate_FieldText(systemUserEmploymentContractStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate_FieldText("")
                .ValidateContractEndReason_LinkField("");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1856")]
        [Description("Step(s) 19 to 20 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod011()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            //Create suspension with a past date
            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var systemUserSuspensionEndDate = systemUserSuspensionStartDate.AddDays(4);
            var contracts = new List<Guid> { systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContractId, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended")
                .InsertDescription("test ...")
                .InsertEndDate("")
                .InsertEndTime("")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .OpenRecord(systemUserEmploymentContractId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active")
                .ValidateStartDate_FieldText(systemUserEmploymentContractStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateEndDate_FieldText("")
                .ValidateContractEndReason_LinkField("");

            #endregion

            #region Step 20

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentDetailsSectionTitleVisible(true, "Employment Details")
                .ValidateEmploymentStatusVisible(true)
                .ValidateJobTitleVisible(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1857")]
        [Description("Step(s) 21 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System Users")]
        public void SU_EC_UITestMethod012()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentStatusSelectedText("Not Started");


            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(3);
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContractStartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            systemUserRecordPage
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentStatusSelectedText("Not Started");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1858")]
        [Description("Step(s) 22 to 24 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System Users")]
        public void SU_EC_UITestMethod013()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract 1

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-20);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract1EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-15);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract1Id, systemUserEmploymentContract1EndDate, contractEndReasonId);

            #endregion

            #region System User Employment Contract 2

            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Step 22

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentStatusSelectedText("Active");

            #endregion

            #region Step 23

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContract2Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            systemUserRecordPage
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentStatusSelectedText("Suspended");

            #endregion

            #region Step 24

            #region System User Suspension

            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-6)).Date;
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region System User Employment Contract 2

            var systemUserEmploymentContract2EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-4);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract2Id, systemUserEmploymentContract2EndDate, contractEndReasonId);

            #endregion

            systemUserRecordPage
                .ClickBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentStatusSelectedText("Ended");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1859")]
        [Description("Step(s) 25 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System Users")]
        public void SU_EC_UITestMethod014()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract 1 (Ended)

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-20);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract1EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-15);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract1Id, systemUserEmploymentContract1EndDate, contractEndReasonId);

            #endregion

            #region System User Employment Contract 2 (Not Started)

            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(10);
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region System User Employment Contract 3 (Suspended)

            var systemUserEmploymentContract3StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract3Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract3StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContract3Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Step 25

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentStatusSelectedText("Suspended");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1860")]
        [Description("Step(s) 26 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System Users")]
        public void SU_EC_UITestMethod015()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract 1 (Ended)

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-20);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract1EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-15);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract1Id, systemUserEmploymentContract1EndDate, contractEndReasonId);

            #endregion

            #region System User Employment Contract 2 (Not Started)

            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(10);
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Step 26

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentStatusSelectedText("Not Started");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1861")]
        [Description("Step(s) 27 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System Users")]
        public void SU_EC_UITestMethod016()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeName = "EC_R_" + currentDateTimeString;
            var _careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, _careProviderStaffRoleTypeName, "90", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract 1 (Ended)

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-20);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract1EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-15);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract1Id, systemUserEmploymentContract1EndDate, contractEndReasonId);

            #endregion

            #region System User Employment Contract 3 (Suspended)

            var systemUserEmploymentContract3StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract3Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract3StartDate, _careProviderStaffRoleTypeId, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-8)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContract3Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Step 27

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateEmploymentStatusSelectedText("Suspended");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2793

        [TestProperty("JiraIssueID", "ACC-2875")]
        [Description("End to End test case 1 - Step(s) 1 to 6 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod001()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType3Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cleaner", "51", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType4Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Transporter", "52", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(3);
            var systemUserEmploymentContract3StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract3EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-5);
            var systemUserEmploymentContract4StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-5);

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40, "", null); //Active
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleType2Id, _teamId, _employmentContractTypeid, 40, "", null); //Not Started
            var systemUserEmploymentContract3Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract3StartDate, _careProviderStaffRoleType3Id, _teamId, _employmentContractTypeid, 40, "", null); //Ended
            var systemUserEmploymentContract4Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract4StartDate, _careProviderStaffRoleType4Id, _teamId, _employmentContractTypeid, 40, "", null); //Suspended

            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract3Id, systemUserEmploymentContract3EndDate, contractEndReasonId);

            #endregion

            #region Suspension

            dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserEmploymentContract4StartDate.AddDays(2), _teamId, systemUserSuspensionReasonId, new List<Guid> { systemUserEmploymentContract4Id });

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #endregion

            #region Step 2

            systemUserEmploymentContractsPage
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .WaitForGeneralSectionToLoad()
                .ValidateStartDate_FieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .WaitForSchedulingSectionToLoad()
                .WaitForAnnualLeaveSectionToLoad();

            #endregion

            #region Step 3

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()

                .WaitForGeneralSectionToLoad(true)
                .WaitForSchedulingSectionToLoad()
                .WaitForAnnualLeaveSectionToLoad(false)

                .ValidateContractEndReasonLoopUpButtonEnabled(false);

            #endregion

            #region Step 4

            systemUserEmploymentContractsRecordPage
                .ValidateSystemUserEmploymentContractsRecordPageTitle("System User Employment Contract:\r\nEC_User " + currentDateTimeString + " - Helper");

            #endregion

            #region Step 5

            systemUserEmploymentContractsRecordPage
                .ValidateStatusFieldSelectedText("Active")
                .ValidateRoleLoopUpButtonEnabled(false)
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract2Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Not Started")
                .ValidateRoleLoopUpButtonEnabled(true)
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract3Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended")
                .ValidateRoleLoopUpButtonEnabled(false)
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract4Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended")
                .ValidateRoleLoopUpButtonEnabled(false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2877")]
        [Description("End to End test case 1 - Step(s) 6 to 7 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        [TestMethod()]
        public void SU_EC_ETE1_UITestMethod002()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var newUserNumber = (dbHelper.systemUser.GetSystemUserBySystemUserID(newUserId, "usernumber")["usernumber"]).ToString();

            #endregion

            #region System User 2

            var newUserName2 = "EC_User2_" + currentDateTimeString;
            var newUser2Id = commonMethodsDB.CreateSystemUserRecord(newUserName2, "EC_User2", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var newUser2Number = (dbHelper.systemUser.GetSystemUserBySystemUserID(newUser2Id, "usernumber")["usernumber"]).ToString();

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType3Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cleaner", "51", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract3StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);


            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleType2Id, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract3Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUser2Id, systemUserEmploymentContract3StartDate, _careProviderStaffRoleType3Id, _teamId, _employmentContractTypeid, 40);

            dbHelper.systemUserEmploymentContract.UpdatePayrollId(systemUserEmploymentContract1Id, currentDateTimeString + "_A");
            dbHelper.systemUserEmploymentContract.UpdatePayrollId(systemUserEmploymentContract3Id, currentDateTimeString + "_B");

            #endregion


            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract2Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertPayrollId(currentDateTimeString + "_A")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please note that the following Employment Contract(s) have already used this payroll ID:\r\nEC_User " + currentDateTimeString + " (Id: " + newUserNumber + ") - EC_User " + currentDateTimeString + " - Helper\r\nWould you like to proceed?")
                .TapCancelButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertPayrollId(currentDateTimeString + "_B")
                .ClickSaveAndCloseButton(); ;

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Please note that the following Employment Contract(s) have already used this payroll ID:\r\nEC_User2 " + currentDateTimeString + " (Id: " + newUser2Number + ") - EC_User2 " + currentDateTimeString + " - Cleaner\r\nWould you like to proceed?")
                .TapCancelButton();

            #endregion

            #region Step 7

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate("", "")
                .ValidateEndDateFieldEnabled(false)
                .ValidateContractEndReasonLoopUpButtonEnabled(false)

                .InsertStartDate("03/07/2023", "00:00")
                .ValidateEndDateFieldEnabled(true)
                .ValidateContractEndReasonLoopUpButtonEnabled(false)

                .InsertEndDate("05/07/2023")
                .ValidateEndDateFieldEnabled(true)
                .ValidateContractEndReasonLoopUpButtonEnabled(true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2880")]
        [Description("End to End test case 1 - Step(s) 8 to 10 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod003()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "ECU3_" + currentDateTimeString;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ECU3", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var newUserNumber = (dbHelper.systemUser.GetSystemUserBySystemUserID(newUserId, "usernumber")["usernumber"]).ToString();

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40, "Test", null);

            #endregion


            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateCanAlsoWorkAtLoopUpButtonVisible(false)
                .ValidateCanAlsoWorkAtAsterisksFieldVisible(true)
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            #endregion

            #region Step 9

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field View)")[0];
            var userSecurityProfileID = commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            systemUserEmploymentContractsPage
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateCanAlsoWorkAtLoopUpButtonVisible(true)
                .ValidateCanAlsoWorkAtAsterisksFieldVisible(false)
                .ClickBackButton();

            #endregion

            #region Step 10

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            dbHelper.userSecurityProfile.DeleteUserSecurityProfile(userSecurityProfileID);
            _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            userSecurityProfileID = commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            systemUserEmploymentContractsPage
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateCanAlsoWorkAtLoopUpButtonVisible(true)
                .ValidateCanAlsoWorkAtAsterisksFieldVisible(false)
                .ClickBackButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2881")]
        [Description("End to End test case 1 - Step(s) 11 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod004()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Team

            var teamAId = commonMethodsDB.CreateTeam("EC TA", null, _businessUnitId, "907678", "ECTA@careworkstempmail.com", "EC TA", "020 123456");
            var teamBId = commonMethodsDB.CreateTeam("EC TB", null, _businessUnitId, "907678", "ECTB@careworkstempmail.com", "EC TB", "020 123457");
            var teamCId = commonMethodsDB.CreateTeam("EC TC", null, _businessUnitId, "907678", "ECTC@careworkstempmail.com", "EC TC", "020 123458");
            dbHelper.team.UpdateInactive(teamCId, true); //Deactivate Team C

            #endregion

            #region System User (Login)

            _systemUsername = "ECU3_" + currentDateTimeString;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ECU3", currentDateTimeString, "Passw0rd_!", _businessUnitId, teamAId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Team Member

            commonMethodsDB.CreateTeamMember(teamBId, _systemUserId, new DateTime(2020, 1, 1), null);
            commonMethodsDB.CreateTeamMember(teamCId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, teamAId, _languageId, _authenticationproviderid);
            commonMethodsDB.CreateTeamMember(teamBId, newUserId, new DateTime(2020, 1, 1), null);
            commonMethodsDB.CreateTeamMember(teamCId, newUserId, new DateTime(2020, 1, 1), null);

            #endregion


            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickResponsibleTeamLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("EC TA", teamAId);

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ClickCanAlsoWorkAtLoopUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("EC TA").TapSearchButton().ValidateResultElementNotPresent(teamAId)
                .TypeSearchQuery("EC TB").TapSearchButton().ValidateResultElementPresent(teamBId)
                .TypeSearchQuery("EC TC").TapSearchButton().ValidateResultElementNotPresent(teamCId)
                .TypeSearchQuery("EC TB").TapSearchButton().SelectResultElement(teamBId);

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ValidateCanAlsoWorkAtRecordVisible(teamBId, true)
               .ClickResponsibleTeamLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("EC TB", teamBId);

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ValidateCanAlsoWorkAtRecordVisible(teamBId, false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2883")]
        [Description("End to End test case 1 - Step(s) 12 to 13 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod005()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Team

            var teamAId = commonMethodsDB.CreateTeam("EC TA", null, _businessUnitId, "907678", "ECTA@careworkstempmail.com", "EC TA", "020 123456");
            var teamDId = commonMethodsDB.CreateTeam("EC TD", null, _businessUnitId, "907678", "ECTD@careworkstempmail.com", "EC TD", "020 123459");
            dbHelper.team.UpdateIncludeInSchedulingScreens(teamDId, false);

            #endregion

            #region System User (Login)

            _systemUsername = "ECU3_" + currentDateTimeString;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ECU3", currentDateTimeString, "Passw0rd_!", _businessUnitId, teamAId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Team Member

            commonMethodsDB.CreateTeamMember(teamDId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, teamAId, _languageId, _authenticationproviderid);
            commonMethodsDB.CreateTeamMember(teamDId, newUserId, new DateTime(2020, 1, 1), null);

            #endregion


            #region Step 12 & 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickResponsibleTeamLookUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("EC TA", teamAId);

            systemUserEmploymentContractsRecordPage
               .WaitForSystemUserEmploymentContractsRecordPageToLoad()
               .ClickCanAlsoWorkAtLoopUpButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("EC TD").TapSearchButton().ValidateResultElementNotPresent(teamDId);


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2885")]
        [Description("End to End test case 1 - Step(s) 14 to 15 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod006()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Booking Types

            int BookingTypeClass1 = 1; //Booking (to location)
            int WorkingContractedTime1 = 1; //Count full booking length

            var CPBookingType1Id = commonMethodsDB.CreateCPBookingType("EC_ETE_1", BookingTypeClass1, 30, new TimeSpan(9, 0, 0), new TimeSpan(9, 30, 0), WorkingContractedTime1, false, null, new DateTime(2022, 3, 1), null);
            var CPBookingType2Id = commonMethodsDB.CreateCPBookingType("EC_ETE_2", BookingTypeClass1, 30, new TimeSpan(9, 0, 0), new TimeSpan(9, 30, 0), WorkingContractedTime1, false, null, new DateTime(2022, 3, 1), new DateTime(2022, 3, 21));
            var CPBookingType3Id = commonMethodsDB.CreateCPBookingType("EC_ETE_3", BookingTypeClass1, 30, new TimeSpan(9, 0, 0), new TimeSpan(9, 30, 0), WorkingContractedTime1, false, null, new DateTime(2022, 3, 1), new DateTime(2022, 12, 31));
            var CPBookingType4Id = commonMethodsDB.CreateCPBookingType("EC_ETE_4", BookingTypeClass1, 30, new TimeSpan(9, 0, 0), new TimeSpan(9, 30, 0), WorkingContractedTime1, false, null, null, null);
            var CPBookingType5Id = commonMethodsDB.CreateCPBookingType("EC_ETE_5", BookingTypeClass1, 30, new TimeSpan(9, 0, 0), new TimeSpan(9, 30, 0), WorkingContractedTime1, false, null, new DateTime(2022, 12, 1), null);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(
                newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40, new List<Guid> { CPBookingType1Id, CPBookingType3Id, CPBookingType4Id });

            #endregion


            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickBookingTypesLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("EC_ETE_1").TapSearchButton().ValidateResultElementPresent(CPBookingType1Id)
                .TypeSearchQuery("EC_ETE_2").TapSearchButton().ValidateResultElementNotPresent(CPBookingType2Id)
                .TypeSearchQuery("EC_ETE_3").TapSearchButton().ValidateResultElementNotPresent(CPBookingType3Id)
                .TypeSearchQuery("EC_ETE_4").TapSearchButton().ValidateResultElementPresent(CPBookingType4Id)
                .TypeSearchQuery("EC_ETE_5").TapSearchButton().ValidateResultElementPresent(CPBookingType5Id)
                .ClickCloseButton();

            #endregion

            #region Step 15

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessageContainsText("Please consider updating the booking type(s) that are no longer valid.")
                .ValidateMessageContainsText("Booking Type(s) is not current: EC_ETE_3")
                .TapCloseButton();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2886")]
        [Description("End to End test case 1 - Step(s) 17 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod007()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Team

            var teamAId = commonMethodsDB.CreateTeam("EC TA", null, _businessUnitId, "907678", "ECTA@careworkstempmail.com", "EC TA", "020 123456");
            var teamBId = commonMethodsDB.CreateTeam("EC TB", null, _businessUnitId, "907678", "ECTB@careworkstempmail.com", "EC TB", "020 123457");
            var teamEId = commonMethodsDB.CreateTeam("EC TE", null, _businessUnitId, "907678", "ECTB@careworkstempmail.com", "EC TE", "020 123459");

            #endregion

            #region System User (Login)

            _systemUsername = "ECU3_" + currentDateTimeString;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ECU3", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Team Member

            commonMethodsDB.CreateTeamMember(teamAId, _systemUserId, new DateTime(2020, 1, 1), null);
            commonMethodsDB.CreateTeamMember(teamBId, _systemUserId, new DateTime(2020, 1, 1), null);
            commonMethodsDB.CreateTeamMember(teamEId, _systemUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Team Member

            commonMethodsDB.CreateTeamMember(teamAId, newUserId, new DateTime(2020, 1, 1), null);
            commonMethodsDB.CreateTeamMember(teamBId, newUserId, new DateTime(2020, 1, 1), null);
            commonMethodsDB.CreateTeamMember(teamEId, newUserId, new DateTime(2020, 1, 1), null);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40, new List<Guid>(), new List<Guid> { teamAId, teamBId }, null);

            #endregion


            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickAssignButton();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad(false)
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("EC TE", teamEId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad(false)
                .TapOkButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateResponsibleTeamLinkFieldText("EC TE")
                .ValidateCanAlsoWorkAtRecordVisible(teamAId, true)
                .ValidateCanAlsoWorkAtRecordVisible(teamBId, true);

            systemUserEmploymentContractsRecordPage
                .ClickCanAlsoWorkAtRecordRemoveButton(teamAId);

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Because you are making changes to the Can Also Work At teams, please review the selected Booking Types for this Staff Contract.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2871")]
        [Description("End to End test case 1 - Step(s) 19 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod008()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);

            #endregion


            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate("01/07/2023")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Contract end date/time must be after the start date/time")
                .TapCloseButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(systemUserEmploymentContract1StartDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2872")]
        [Description("End to End test case 1 - Step(s) 20 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod009()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract1Id, systemUserEmploymentContract1StartDate.AddDays(1), contractEndReasonId);

            #endregion


            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateContractEndReasonLoopUpButtonEnabled(true)
                .ValidateContractEndReason_LinkField("Unknown reason")
                .InsertEndDate("")
                .ValidateContractEndReasonLoopUpButtonEnabled(false)
                .ValidateContractEndReason_LinkField("");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2873")]
        [Description("End to End test case 1 - Step(s) 21 to 22 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod010()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract1EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-1);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract1Id, systemUserEmploymentContract1EndDate, contractEndReasonId);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-7));
            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-3));
            var contracts = new List<Guid> { systemUserEmploymentContract1Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(systemUserSuspensionStartDate.AddDays(2).ToString("dd'/'MM'/'yyyy"))
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Contract end date is earlier than the last suspension end date - please choose a value after or equal to " + systemUserSuspensionEndDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .TapCloseButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad();

            #endregion

            #region Step 22

            systemUserEmploymentContractsRecordPage
                .InsertEndDate(systemUserSuspensionEndDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2874")]
        [Description("End to End test case 1 - Step(s) 23 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod011()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(2));
            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(7));
            var contracts = new List<Guid> { systemUserEmploymentContract1Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region Step 23

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate(systemUserSuspensionStartDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "00:00")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Invalid start date/time value due to active suspensions - please choose a value before or equal to " + systemUserSuspensionStartDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .TapCloseButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2876")]
        [Description("End to End test case 1 - Step(s) 24 and 25 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_ETE1_UITestMethod012()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).Date;
            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(7)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContract1Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region Step 24

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(systemUserSuspensionStartDate.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This Contract have some Contract Suspensions that start after this End Date, please check if they need to be adjusted.")
                .TapCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            #endregion

            #region Step 25

            endContractWithSuspensionDatePopup
                .WaitForEndContractWithSuspensionDatePopupToLoad()
                .ValidateTopMessage("Ending this contract will cause the linked suspension to be ended also. Please choose a suspension end date/time below:")
                .ValidateBottomMessage("This value must be earlier than or equal to " + systemUserSuspensionStartDate.AddDays(-2).ToString("dd'/'MM'/'yyyy 00:00:00") + " and after " + systemUserSuspensionStartDate.ToString("dd'/'MM'/'yyyy 00:00:00") + "")
                .ClickOkButton();

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-2878")]
        [Description("End to End test case 1 - Step(s) 26 to 31 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        [TestMethod()]
        public void SU_EC_ETE1_UITestMethod013()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).Date;
            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(7)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContract1Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region Step 26

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            var newContractEndDate = systemUserSuspensionEndDate.AddDays(2);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(newContractEndDate.ToString("dd'/'MM'/'yyyy"))
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractWithSuspensionDatePopup
                .WaitForEndContractWithSuspensionDatePopupToLoad()
                .ValidateTopMessage("Ending this contract will cause the linked suspension to be ended also. Please choose a suspension end date/time below:")
                .ValidateBottomMessage("This value must be earlier than or equal to " + newContractEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00:00 and after " + systemUserSuspensionStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00:00")
                ;

            #endregion

            #region Step 27

            endContractWithSuspensionDatePopup
                .ValidateEndDate(newContractEndDate.ToString("dd'/'MM'/'yyyy"), "00:00")
                .InsertEndDate("", "")
                .ValidateEndDate(newContractEndDate.ToString("dd'/'MM'/'yyyy"), "00:00");

            #endregion

            #region Step 28

            endContractWithSuspensionDatePopup
                .InsertEndDate(newContractEndDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "00:00")
                .ClickOkButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You have chosen an invalid value for suspension end date/time. Please choose a value which is before or equal to " + newContractEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00:00 and after " + systemUserSuspensionStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00:00")
                .TapCloseButton();

            #endregion

            #region Step 29

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractWithSuspensionDatePopup
                .WaitForEndContractWithSuspensionDatePopupToLoad()
                .InsertEndDate(systemUserSuspensionStartDate.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "00:00")
                .ClickOkButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You have chosen an invalid value for suspension end date/time. Please choose a value which is before or equal to " + newContractEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00:00 and after " + systemUserSuspensionStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00:00")
                .TapCloseButton();

            #endregion

            #region Step 30 & 31

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractWithSuspensionDatePopup
                .WaitForEndContractWithSuspensionDatePopupToLoad()
                .ClickCancelButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractWithSuspensionDatePopup
                .WaitForEndContractWithSuspensionDatePopupToLoad()
                .ClickOkButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            var suspensionNewEndDate = (DateTime)(dbHelper.systemUserSuspension.GetSystemUserSuspensionByID(suspensionId, "suspensionenddate")["suspensionenddate"]);
            Assert.AreEqual(newContractEndDate, suspensionNewEndDate.ToLocalTime());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2879")]
        [Description("End to End test case 1 - Step(s) 32 and 33 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        [TestMethod()]
        public void SU_EC_ETE1_UITestMethod014()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-8);

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleType2Id, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).Date;
            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(7)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContract1Id, systemUserEmploymentContract2Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion

            #region Step 32

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            var newContractEndDate = systemUserSuspensionEndDate.AddDays(-2);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(newContractEndDate.ToString("dd'/'MM'/'yyyy"))
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractActionPopup
                .WaitForEndContractActionPopupToLoad()
                .ValidateTopMessage("Please choose how to handle the ending of contracts and linked suspensions.")
                .SelectEndContractOption("End all system users suspensions and linked contracts")
                .SelectEndContractOption("End suspension and all contracts linked to it");

            #endregion

            #region Step 33

            endContractActionPopup
                .ClickOkButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending this contract will cause the linked suspension to be ended also.")
                .TapCancelButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractActionPopup
                .WaitForEndContractActionPopupToLoad()
                .ClickOkButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending this contract will cause the linked suspension to be ended also.")
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            var suspensionNewEndDate = (DateTime)(dbHelper.systemUserSuspension.GetSystemUserSuspensionByID(suspensionId, "suspensionenddate")["suspensionenddate"]);
            Assert.AreEqual(newContractEndDate, suspensionNewEndDate.ToLocalTime());

            var contract1NewEndDate = (DateTime)(dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(systemUserEmploymentContract1Id, "enddate")["enddate"]);
            Assert.AreEqual(newContractEndDate, contract1NewEndDate.ToLocalTime());

            var contract2NewEndDate = (DateTime)(dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(systemUserEmploymentContract2Id, "enddate")["enddate"]);
            Assert.AreEqual(newContractEndDate, contract2NewEndDate.ToLocalTime());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2882")]
        [Description("End to End test case 1 - Step(s) 34 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        [TestMethod()]
        public void SU_EC_ETE1_UITestMethod015()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-8);

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleType2Id, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).Date;
            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(7)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContract1Id, systemUserEmploymentContract2Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion


            #region Step 34

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            var newContractEndDate = systemUserSuspensionEndDate.AddDays(-2);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(newContractEndDate.ToString("dd'/'MM'/'yyyy"))
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractActionPopup
                .WaitForEndContractActionPopupToLoad()
                .ValidateTopMessage("Please choose how to handle the ending of contracts and linked suspensions.")
                .SelectEndContractOption("End all system users suspensions and linked contracts")
                .ValidateBottomMessage("As a result of this action all contracts and suspensions end date/time values will be updated or overridden with " + newContractEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00:00");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2884")]
        [Description("End to End test case 1 - Step(s) 35 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        [TestMethod()]
        public void SU_EC_ETE1_UITestMethod016()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-8);

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleType2Id, _teamId, _employmentContractTypeid, 40);

            #endregion

            #region System User Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).Date;
            var systemUserSuspensionEndDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(7)).Date;
            var contracts = new List<Guid> { systemUserEmploymentContract1Id, systemUserEmploymentContract2Id };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);
            dbHelper.systemUserSuspension.UpdateSuspensionEndDate(suspensionId, systemUserSuspensionEndDate, contracts);

            #endregion


            #region Step 35

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            var newContractEndDate = systemUserSuspensionStartDate.AddDays(-2);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(newContractEndDate.ToString("dd'/'MM'/'yyyy"))
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This Contract have some Contract Suspensions that start after this End Date, please check if they need to be adjusted.")
                .TapCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractActionPopup
                .WaitForEndContractActionPopupToLoad()
                .ClickOkButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending this contract will cause the linked suspension to be ended also.")
                .TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Contract end date is earlier than the suspension start date")
                .TapCloseButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad();

            #endregion

            #region Step 36

            newContractEndDate = systemUserSuspensionStartDate.AddDays(2);

            systemUserEmploymentContractsRecordPage
                .InsertEndDate(newContractEndDate.ToString("dd'/'MM'/'yyyy"))
                .ClickContractEndReasonLoopUpButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Unknown reason", contractEndReasonId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?")
                .TapOKButton();

            endContractActionPopup
                .WaitForEndContractActionPopupToLoad()
                .SelectEndContractOption("End all system users suspensions and linked contracts")
                .ClickOkButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Ending this contract will cause the linked suspension to be ended also.")
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton();

            var suspensionNewEndDate = (DateTime)(dbHelper.systemUserSuspension.GetSystemUserSuspensionByID(suspensionId, "suspensionenddate")["suspensionenddate"]);
            Assert.AreEqual(newContractEndDate, suspensionNewEndDate.ToLocalTime());

            var contract1EndReasonId = (dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(systemUserEmploymentContract1Id, "contractendreasonid")["contractendreasonid"]).ToString();
            Assert.AreEqual(contractEndReasonId.ToString(), contract1EndReasonId);

            var contract2EndReasonId = (dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(systemUserEmploymentContract2Id, "contractendreasonid")["contractendreasonid"]).ToString();
            Assert.AreEqual(contractEndReasonId.ToString(), contract2EndReasonId);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5026

        [TestProperty("JiraIssueID", "ACC-5045")]
        [Description("Step(s) 1, 4 to 6 from the original jira test ACC-4912")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod017()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser3";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var newUserFullName = "EC_User " + currentDateTimeString;

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType3Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cleaner", "51", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType4Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Transporter", "52", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);
            var _employmentContractTypeId2 = commonMethodsDB.CreateEmploymentContractType(_teamId, "ACC-5045_Contracted", "5045", null, new DateTime(2020, 1, 1), 2);
            var _employmentContractTypeId3 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Hourly", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Booking Types

            var CPBookingType1Id = commonMethodsDB.CreateCPBookingType("EC_BookingType_1", 1, 30, new TimeSpan(9, 0, 0), new TimeSpan(9, 30, 0), 1, false, null, new DateTime(2022, 3, 1), null);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(3);
            var systemUserEmploymentContract3StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract3EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-5);
            var systemUserEmploymentContract4StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-5);

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeId1, 40); //Active
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleType2Id, _teamId, _employmentContractTypeId1, 40); //Not Started
            var systemUserEmploymentContract3Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract3StartDate, _careProviderStaffRoleType3Id, _teamId, _employmentContractTypeId1, 40); //Ended
            var systemUserEmploymentContract4Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract4StartDate, _careProviderStaffRoleType4Id, _teamId, _employmentContractTypeId1, 40); //Suspended

            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract3Id, systemUserEmploymentContract3EndDate, contractEndReasonId);

            #endregion

            #region Suspension

            dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserEmploymentContract4StartDate.AddDays(2), _teamId, systemUserSuspensionReasonId, new List<Guid> { systemUserEmploymentContract4Id });

            #endregion


            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentMenuAndValidateSubMenus()
                .NavigateToEmploymentContractsSubPage();

            #endregion

            #region Step 4

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateSystemUser_LinkField(newUserFullName)
                .ValidateStartDate_FieldText(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                //.ValidateStartTime_FieldText(DateTime.Now.ToString("HH:mm"))
                .ValidateResponsibleTeamLinkFieldText("")
                .ValidateStatusFieldSelectedText("Not Started")
                //.ValidateYearlyEntitlement_FieldText("5.60") //field logic was changed due to this story https://advancedcsg.atlassian.net/browse/ACC-6847
                .ValidateEntitlementUnitFieldSelectedText("Day(s)");

            #endregion

            #region Step 5

            systemUserEmploymentContractsRecordPage
                .ClickRoleLoopUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("Helper", _careProviderStaffRoleType1Id, true)
                .SearchAndValidateRecordPresentOrNot("Cook", _careProviderStaffRoleType2Id, true)
                .SearchAndValidateRecordPresentOrNot("Cleaner", _careProviderStaffRoleType3Id, true)
                .SearchAndValidateRecordPresentOrNot("Transporter", _careProviderStaffRoleType4Id, true)
                .ClickCloseButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("Contracted", _employmentContractTypeId1, true)
                .SearchAndValidateRecordPresentOrNot("ACC-5045_Contracted", _employmentContractTypeId2, true)
                .ClickCloseButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickBookingTypesLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndValidateRecordPresentOrNot("EC_BookingType_1", CPBookingType1Id, true)
                .ClickCloseButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateEntitlementUnitPicklist_FieldOptionIsPresent("Hour(s)")
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract2Id.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Not Started")
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract3Id.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Ended")
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract4Id.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended")
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Active");

            #endregion

            #region Step 6

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateMaximumLimitOfDescriptionField("2000")
                .ValidateMaximumLimitOfPayrollIDField("250");
            //.InsertYearlyEntitlementWeek("53") //field logic was changed due to this story https://advancedcsg.atlassian.net/browse/ACC-6847
            //.ValidateYearlyEntitlementWeeksFieldErrorLabelText("Please enter a value between 0 and 52.")
            //.InsertYearlyEntitlementWeek("50");

            systemUserEmploymentContractsRecordPage
                .InsertContractHoursPerWeek("49")
                .InsertFixedWorkingPatternCycle("1")
                .ClickSaveButton();

            systemUserEmploymentContractsRecordPage
                .ValidateContractHoursPerWeekErrorLabelVisible("Please enter a value between 0.5 and 48."); // https://advancedcsg.atlassian.net/browse/ACC-7080 - there is a new min value of 0.5

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Hourly", _employmentContractTypeId3);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                //.InsertContractHoursPerWeek("49") // For 'Hourly' contract type, is Contract Hours Per Week field now changed to readonly - https://advancedcsg.atlassian.net/browse/ACC-5702
                //.ClickSaveButton()

                .ValidateContractHoursPerWeekErrorLabelVisible("Please enter a value between 0.5 and 48.");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5027

        [TestProperty("JiraIssueID", "ACC-5046")]
        [Description("Test case for Step 7 - Points 4 and 5 from the original Jira test ACC-4912.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod018()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            var _houry_EmploymentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Hourly", null, null, new DateTime(2021, 8, 16), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract1EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1);

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeid, 40);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract1Id, systemUserEmploymentContract1EndDate, contractEndReasonId);

            #endregion

            #region Step 7 - Point 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateEndDate_FieldText(systemUserEmploymentContract1EndDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateContractEndReason_LinkField("Unknown reason");

            //Insert blank string End Date Field Text and validate contract end reason link field is empty string
            systemUserEmploymentContractsRecordPage
                .InsertEndDate("")
                .ValidateContractEndReason_LinkField("");

            #endregion

            #region Step 7 - Point 5

            systemUserEmploymentContractsRecordPage
                .ValidateType_LinkField("Contracted")
                .ValidateContactHoursPerWeek_IsMandatoryField(false)
                .ValidateMandatoryFieldIsDisplayed("Contract Hours Per Week", false)
                .InsertContractHoursPerWeek("") // For 'Hourly' contract type, is Contract Hours Per Week field now changed to readonly - https://advancedcsg.atlassian.net/browse/ACC-5702
                .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Hourly", _houry_EmploymentContractTypeId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateContactHoursPerWeek_IsMandatoryField(true)
                .ValidateMandatoryFieldIsDisplayed("Contract Hours Per Week", true);

            systemUserEmploymentContractsRecordPage
                .InsertContractHoursPerWeek("")
                .ClickSaveButton()
                .ValidateContractHoursPerWeekErrorLabelVisible("Please fill out this field.")
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertContractHoursPerWeek("40")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveButton()
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateType_LinkField("Hourly")
                .ValidateContractHoursPerWeek_FieldText("40.00");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5028

        [TestProperty("JiraIssueID", "ACC-5055")]
        [Description("Test case for Step 9 from the original Jira test ACC-4912.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod019()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser2";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            var _houry_EmploymentContractTypeId = commonMethodsDB.CreateEmploymentContractType(_teamId, "Hourly", null, null, new DateTime(2021, 8, 16), 2);

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            var systemUserEmploymentContract1StartDateUpdated = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-1);
            var systemUserEmploymentContract1EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(1);

            #endregion

            #region Step 9

            #region Create an Employment Contract.

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickAddNewButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateSystemUserEmploymentContractsRecordPageTitle("System User Employment Contract:\r\nNew")
                .ClickResponsibleTeamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("EmploymentContract T1", _teamId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickRoleLoopUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Helper", _careProviderStaffRoleType1Id);

            //click type lookup 
            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Contracted", _employmentContractTypeid);

            //insert start date as 2 days in past
            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate(systemUserEmploymentContract1StartDate.ToString("dd'/'MM'/'yyyy"), "00:00")
                .InsertDescription(newUserName + " Employment Contract")
                .InsertFixedWorkingPatternCycle("1")
                .InsertContractHoursPerWeek("40")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveAndCloseButton();

            var matchingStaffReviewSetupFound = dbHelper.staffReviewSetup.GetRecordsForAllRoles().Any();

            if (matchingStaffReviewSetupFound)
            {
                staffReviewRequirementsPopup
                    .WaitForStaffReviewRequirementsPopupToLoad()
                    .ClickSaveButton();

                confirmDynamicDialogPopup
                    .WaitForConfirmDynamicDialogPopupToLoad()
                    .ValidateMessage("No reviews have been selected, do you wish to proceed?")
                    .TapOKButton();
            }

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad();

            var _employmentContractId1 = dbHelper.systemUserEmploymentContract.GetBySystemUserId(newUserId)[0];

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(_employmentContractId1.ToString(), true);

            #endregion

            #region Edit an Employment Contract.

            //wait for page to load and open employment contract record
            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(_employmentContractId1);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateSystemUserEmploymentContractsRecordPageTitle("System User Employment Contract:\r\n" + "EC_User" + " " + currentDateTimeString + " - Helper")
                .ValidateType_LinkField("Contracted")
                .ValidateStartDate_FieldText(systemUserEmploymentContract1StartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime_FieldText("00:00")
                .ValidateDescriptionText(newUserName + " Employment Contract")
                .InsertStartDate(systemUserEmploymentContract1StartDateUpdated.ToString("dd'/'MM'/'yyyy"), "01:00")
                .InsertDescription(newUserName + " Employment Contract Update")
                .ClickTypeLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Hourly", _houry_EmploymentContractTypeId);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickSaveAndCloseButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(_employmentContractId1.ToString(), true)
                .OpenRecord(_employmentContractId1);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateSystemUserEmploymentContractsRecordPageTitle("System User Employment Contract:\r\n" + "EC_User" + " " + currentDateTimeString + " - Helper")
                .ValidateStartDate_FieldText(systemUserEmploymentContract1StartDateUpdated.ToString("dd'/'MM'/'yyyy"))
                .ValidateStartTime_FieldText("01:00")
                .ValidateDescriptionText(newUserName + " Employment Contract Update")
                .ValidateType_LinkField("Hourly")
                .ValidateContractHoursPerWeek_FieldText("40.00")
                .ValidateResponsibleTeamLinkFieldText("EmploymentContract T1");

            #endregion

            #region Assign Employment Contract to another team

            #region Team to assign

            var _newTeamId = commonMethodsDB.CreateTeam("EmploymentContract T2", null, _businessUnitId, "907887", "EmploymentContractT2@careworkstempmail.com", "EmploymentContract T2", "020 123456");

            #endregion

            systemUserEmploymentContractsRecordPage
                .ClickBackButton();

            //wait for page to load, select employment and and click assign
            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .SelectRecord(_employmentContractId1.ToString())
                .ClickAssignButton();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad(false)
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("EmploymentContract T2", _newTeamId);

            assignRecordPopup
                .TapOkButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(_employmentContractId1);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateResponsibleTeamLinkFieldText("EmploymentContract T2");

            #endregion

            #region Delete an Employment Contract

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(_employmentContractId1.ToString(), false)
                .ValidateNoRecordMessageVisibile(true);

            #endregion

            #endregion
        }
        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5029

        [TestProperty("JiraIssueID", "ACC-5071")]
        [Description("Step(s) 10 to 11 from the original jira test ACC-4912")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod020()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser3";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User3", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var newUserFullName = "EC_User " + currentDateTimeString;

            #endregion

            #region Team

            var _teamName2 = "EmploymentContract T2";
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId, "907632", "EmploymentContractT2@careworkstempmail.com", "EmploymentContract T2", "020 123456");

            #endregion

            #region Booking Types

            var CPBookingType1Name = "EC_BookingType_001";
            var CPBookingType1Id = commonMethodsDB.CreateCPBookingType(CPBookingType1Name, 1, 30, new TimeSpan(9, 0, 0), new TimeSpan(9, 30, 0), 1, false, null, new DateTime(2022, 3, 1), null);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cook", "3", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType3Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Cleaner", "51", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType4Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Transporter", "52", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var systemUserEmploymentContract2StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(3);
            var systemUserEmploymentContract3StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var systemUserEmploymentContract3EndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-5);
            var systemUserEmploymentContract4StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-5);

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeId1, 40); //Active

            System.Threading.Thread.Sleep(1000);
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract2StartDate, _careProviderStaffRoleType2Id, _teamId, _employmentContractTypeId1, 40); //Not Started

            System.Threading.Thread.Sleep(1000);
            var systemUserEmploymentContract3Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract3StartDate, _careProviderStaffRoleType3Id, _teamId, _employmentContractTypeId1, 40); //Ended
            dbHelper.systemUserEmploymentContract.UpdateEndDate(systemUserEmploymentContract3Id, systemUserEmploymentContract3EndDate, contractEndReasonId);

            System.Threading.Thread.Sleep(1000);
            var systemUserEmploymentContract4Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract4StartDate, _careProviderStaffRoleType4Id, _teamId, _employmentContractTypeId1, 40); //Suspended

            #endregion

            #region Suspension

            dbHelper.systemUserSuspension.CreateSystemUserSuspension(newUserId, systemUserEmploymentContract4StartDate.AddDays(2), _teamId, systemUserSuspensionReasonId, new List<Guid> { systemUserEmploymentContract4Id });

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentMenuAndValidateSubMenus()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateSelectedSystemView("Related Records")
                .ValidateRecordPosition(systemUserEmploymentContract4Id.ToString(), 1)
                .ValidateRecordPosition(systemUserEmploymentContract3Id.ToString(), 2)
                .ValidateRecordPosition(systemUserEmploymentContract2Id.ToString(), 3)
                .ValidateRecordPosition(systemUserEmploymentContract1Id.ToString(), 4);

            systemUserEmploymentContractsPage
                .ValidateHeaderCellTextAndSortByIsVisible(2, "Responsible Team")
                .ValidateHeaderCellTextAndSortByIsVisible(3, "Start Date")
                .ValidateHeaderCellTextAndSortByIsVisible(4, "Type")
                .ValidateHeaderCellTextAndSortByIsVisible(5, "Name")
                .ValidateHeaderCellTextAndSortByIsVisible(6, "Status")
                .ValidateHeaderCellTextAndSortByIsVisible(7, "Description", false)

                .ValidateHeaderCellTextAndSortByIsVisible(8, "Role")
                .ValidateHeaderCellTextAndSortByIsVisible(9, "End Date")
                .ValidateHeaderCellTextAndSortByIsVisible(10, "Employee Payroll ID")
                .ValidateHeaderCellTextAndSortByIsVisible(11, "Modified On")
                .ValidateHeaderCellSortOrdedByDescending(11, "Modified On");

            systemUserEmploymentContractsPage
                .SelectSystemView("Not Started Contracts")
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateSelectedSystemView("Not Started Contracts")
                .ValidateRecordPosition(systemUserEmploymentContract2Id.ToString(), 1);

            systemUserEmploymentContractsPage
                .ValidateHeaderCellTextAndSortByIsVisible(2, "Responsible Team")
                .ValidateHeaderCellTextAndSortByIsVisible(3, "Start Date")
                .ValidateHeaderCellTextAndSortByIsVisible(4, "Type")
                .ValidateHeaderCellTextAndSortByIsVisible(5, "Name")
                .ValidateHeaderCellTextAndSortByIsVisible(6, "Status")
                .ValidateHeaderCellTextAndSortByIsVisible(7, "Description", false)

                .ValidateHeaderCellTextAndSortByIsVisible(8, "Role")
                .ValidateHeaderCellTextAndSortByIsVisible(9, "End Date")
                .ValidateHeaderCellTextAndSortByIsVisible(10, "Employee Payroll ID")
                .ValidateHeaderCellTextAndSortByIsVisible(11, "Modified On")
                .ValidateHeaderCellSortOrdedByDescending(11, "Modified On");

            systemUserEmploymentContractsPage
                .SelectSystemView("Active Contracts")
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateSelectedSystemView("Active Contracts")
                .ValidateRecordPosition(systemUserEmploymentContract1Id.ToString(), 1);

            systemUserEmploymentContractsPage
                .ValidateHeaderCellTextAndSortByIsVisible(2, "Responsible Team")
                .ValidateHeaderCellTextAndSortByIsVisible(3, "Start Date")
                .ValidateHeaderCellTextAndSortByIsVisible(4, "Type")
                .ValidateHeaderCellTextAndSortByIsVisible(5, "Name")
                .ValidateHeaderCellTextAndSortByIsVisible(6, "Status")
                .ValidateHeaderCellTextAndSortByIsVisible(7, "Description", false)

                .ValidateHeaderCellTextAndSortByIsVisible(8, "Role")
                .ValidateHeaderCellTextAndSortByIsVisible(9, "End Date")
                .ValidateHeaderCellTextAndSortByIsVisible(10, "Employee Payroll ID")
                .ValidateHeaderCellTextAndSortByIsVisible(11, "Modified On")
                .ValidateHeaderCellSortOrdedByDescending(11, "Modified On");

            systemUserEmploymentContractsPage
                .SelectSystemView("Ended Contracts")
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateSelectedSystemView("Ended Contracts")
                .ValidateRecordPosition(systemUserEmploymentContract3Id.ToString(), 1);

            systemUserEmploymentContractsPage
                .ValidateHeaderCellTextAndSortByIsVisible(2, "Responsible Team")
                .ValidateHeaderCellTextAndSortByIsVisible(3, "Start Date")
                .ValidateHeaderCellTextAndSortByIsVisible(4, "Type")
                .ValidateHeaderCellTextAndSortByIsVisible(5, "Name")
                .ValidateHeaderCellTextAndSortByIsVisible(6, "Status")
                .ValidateHeaderCellTextAndSortByIsVisible(7, "Description", false)

                .ValidateHeaderCellTextAndSortByIsVisible(8, "Role")
                .ValidateHeaderCellTextAndSortByIsVisible(9, "End Date")
                .ValidateHeaderCellTextAndSortByIsVisible(10, "Employee Payroll ID")
                .ValidateHeaderCellTextAndSortByIsVisible(11, "Modified On")
                .ValidateHeaderCellSortOrdedByDescending(11, "Modified On");

            systemUserEmploymentContractsPage
                .SelectSystemView("Suspended Contracts")
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateSelectedSystemView("Suspended Contracts")
                .ValidateRecordPosition(systemUserEmploymentContract4Id.ToString(), 1);

            systemUserEmploymentContractsPage
                .ValidateHeaderCellTextAndSortByIsVisible(2, "Responsible Team")
                .ValidateHeaderCellTextAndSortByIsVisible(3, "Start Date")
                .ValidateHeaderCellTextAndSortByIsVisible(4, "Type")
                .ValidateHeaderCellTextAndSortByIsVisible(5, "Name")
                .ValidateHeaderCellTextAndSortByIsVisible(6, "Status")
                .ValidateHeaderCellTextAndSortByIsVisible(7, "Description", false)

                .ValidateHeaderCellTextAndSortByIsVisible(8, "Role")
                .ValidateHeaderCellTextAndSortByIsVisible(9, "End Date")
                .ValidateHeaderCellTextAndSortByIsVisible(10, "Employee Payroll ID")
                .ValidateHeaderCellTextAndSortByIsVisible(11, "Modified On")
                .ValidateHeaderCellSortOrdedByDescending(11, "Modified On");

            #endregion

            #region Step 11

            systemUserEmploymentContractsPage
                .SelectSystemView("Related Records")
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateSelectedSystemView("Related Records")
                .SelectRecord(systemUserEmploymentContract2Id.ToString())
                .ClickDeletedButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(systemUserEmploymentContract2Id.ToString(), false);

            systemUserEmploymentContractsPage
                .SelectRecord(systemUserEmploymentContract1Id.ToString())
                .ClickAssignRecordButton();

            assignRecordPopup
                .WaitForAssignRecordPopupToLoad(false)
                .ClickResponsibleTeamLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord(_teamName2, _teamId2);

            assignRecordPopup
                .TapOkButton();

            System.Threading.Thread.Sleep(2000);
            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordCellText(systemUserEmploymentContract1Id.ToString(), 2, _teamName2);

            systemUserEmploymentContractsPage
                .SelectRecord(systemUserEmploymentContract1Id.ToString())
                .ClickAddBookingTypesButton();

            lookupMultiSelectPopup
               .WaitForLookupMultiSelectPopupToLoad()
               .TypeSearchQuery(CPBookingType1Name).TapSearchButton().AddElementToList(CPBookingType1Id.ToString())
               .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(systemUserEmploymentContract1Id.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateAvailableBookingTypesLinkFieldText(CPBookingType1Id.ToString(), CPBookingType1Name);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5030

        [TestProperty("JiraIssueID", "ACC-5072")]
        [Description("Step(s) 12 to 13 from the original jira test ACC-4912")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Employment Contracts")]
        public void SU_EC_UITestMethod021()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User (Login)

            _systemUsername = "EmploymentContractUser4";
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "EmploymentContract", "User4", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);

            #endregion

            #region Security Profile

            var _securityProfileid = dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0];
            commonMethodsDB.CreateUserSecurityProfile(_systemUserId, _securityProfileid);

            #endregion

            #region System User 1

            var newUserName = "EC_User_" + currentDateTimeString;
            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, "EC_User", currentDateTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var newUserFullName = "EC_User " + currentDateTimeString;

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Carer Assistant", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1), 2);

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(newUserId, systemUserEmploymentContract1StartDate, _careProviderStaffRoleType1Id, _teamId, _employmentContractTypeId1, 40); //Active

            #endregion           

            #region Step 12, 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            #region Advanced Search Page

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Employment Contracts")
                .ValidateSelectFilterFieldOptionIsPresent("1", "End Date")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Modified On")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Name")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Employee Payroll ID")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Responsible Team")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Role")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Start Date")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Status")
                .ValidateSelectFilterFieldOptionIsPresent("1", "System User")
                .ValidateSelectFilterFieldOptionIsPresent("1", "System User Employment Contract")

                .SelectFilter("1", "End Date")
                .SelectFilter("1", "Modified On")
                .SelectFilter("1", "Name")
                .SelectFilter("1", "Employee Payroll ID")
                .SelectFilter("1", "Responsible Team")
                .SelectFilter("1", "Role")
                .SelectFilter("1", "Start Date")
                .SelectFilter("1", "Status")
                .SelectFilter("1", "System User")
                .SelectFilter("1", "System User Employment Contract");

            advanceSearchPage
                .SelectFilter("1", "Name")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", newUserFullName + " - Carer Assistant")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(systemUserEmploymentContract1Id.ToString());

            advanceSearchPage
                .ResultsPageValidateHeaderCellText(2, "System User")
                .ResultsPageValidateHeaderCellText(3, "Responsible Team")
                .ResultsPageValidateHeaderCellText(4, "Start Date")
                .ResultsPageValidateHeaderCellText(5, "Name")
                .ResultsPageValidateHeaderCellText(6, "Status")
                .ResultsPageValidateHeaderCellText(7, "Role")
                .ResultsPageValidateHeaderCellText(8, "Employee Payroll ID")
                .ResultsPageValidateHeaderCellText(9, "End Date")
                .ResultsPageValidateHeaderCellText(10, "Modified On");

            #endregion

            #region System User Employment Contracts Page

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(newUserName)
                .ClickSearchButton()
                .OpenRecord(newUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                //.InsertQuickSearchText(newUserName + " - Carer Assistant")
                //.ClickQuickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(systemUserEmploymentContract1Id.ToString(), true);

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                //.InsertQuickSearchText("Carer Assistant")
                //.ClickQuickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(systemUserEmploymentContract1Id.ToString(), true);

            systemUserEmploymentContractsPage
                .ValidateHeaderCellText(2, "Responsible Team")
                .ValidateHeaderCellText(3, "Start Date")
                .ValidateHeaderCellText(5, "Name")
                .ValidateHeaderCellText(6, "Status")
                .ValidateHeaderCellText(8, "Role")
                .ValidateHeaderCellText(9, "End Date")
                .ValidateHeaderCellText(10, "Employee Payroll ID")
                .ValidateHeaderCellText(11, "Modified On");

            #endregion

            #endregion

        }

        #endregion
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-14430
    ///
    /// </summary>
    [TestClass]

    public class SystemUser_Training_TestCases : FunctionalTest
    {

        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private string _environmentName;

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Environment

                _environmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders Security");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareProviders Security", null, _businessUnitId, "907678", "CareProvidersSecurity@careworkstempmail.com", "CareProviders Security", "020 123456");

                #endregion

                #region System User SystemUserTrainingUser1

                commonMethodsDB.CreateSystemUserRecord("SystemUserTrainingUser1", "SystemUserTraining", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22351

        [TestProperty("JiraIssueID", "ACC-3138")]
        [Description("Step 1 to 3 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod001()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Outstanding");

            #endregion

            #region Step 2

            systemUserTrainingRecordPage
                .ValidateTrainingItemErrorLableVisible(false)
                .ValidateResponsibleTeamErrorLableVisible(false)

                .ClickResponsibleTeamRemoveButton()
                .ClickSaveButton()

                .ValidateTrainingItemErrorLableVisible(true)
                .ValidateResponsibleTeamErrorLableVisible(true)
                .ValidateTrainingItemErrorLableText("Please fill out this field.")
                .ValidateResponsibleTeamErrorLableText("Please fill out this field.");

            #endregion

            #region Step 3

            systemUserTrainingRecordPage
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("CareProviders Security").TapSearchButton().SelectResultElement(_teamId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                //.InsertQuickSearchText("Default")
                .ClickSearchButton();

            var trainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, trainingRecords.Count);

            systemUserTrainingPage
                .OpenRecord(trainingRecords[0].ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default")
                .ValidateStatusFieldSelectedText("Outstanding")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3139")]
        [Description("Step 4 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod002()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("Default - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default - Internal").TapSearchButton().SelectResultElement(trainingCourseID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate("01/01/2022")
                .InsertTrainingCourseFinishDate("20/01/2022")
                .SelectOutcome("Pass")
                .InsertExpiryDate("01/01/2023")
                .InsertReferenceNumber("1A")
                .InsertNotes("Notes ...")
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                //.InsertQuickSearchText("Default")
                .ClickSearchButton();

            var trainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, trainingRecords.Count);

            systemUserTrainingPage
                .OpenRecord(trainingRecords[0].ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default")
                .ValidateTrainingCourseLinkFieldText("Default - Internal")

                .ValidateTrainingCourseStartDateFieldValue("01/01/2022")
                .ValidateTrainingCourseFinishDateFieldValue("20/01/2022")
                .ValidateOutcomeFieldSelectedText("Pass")
                .ValidateExpiryDateFieldValue("01/01/2023")
                .ValidateStatusFieldSelectedText("Expired")
                .ValidateReferenceNumberFieldValue("1A")
                .ValidateNotesFieldValue("Notes ...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3140")]
        [Description("Step 5 and 6 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod003()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("Default - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default - Internal").TapSearchButton().SelectResultElement(trainingCourseID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate("20/01/2022")
                .InsertTrainingCourseFinishDate("01/01/2022")
                .SelectOutcome("Pass")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Start Date cannot be greater than the Finish Date.").TapCloseButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad();

            var trainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(0, trainingRecords.Count);

            #endregion

            #region Step 6

            systemUserTrainingRecordPage
                .InsertTrainingCourseFinishDate("")
                .InsertTrainingCourseStartDate("")
                .ClickTrainingCourseRemoveButton()
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                //.InsertQuickSearchText("Default")
                .ClickSearchButton();

            trainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, trainingRecords.Count);

            systemUserTrainingPage
                .OpenRecord(trainingRecords[0].ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default")
                .ValidateTrainingCourseLinkFieldText("")

                .ValidateTrainingCourseStartDateFieldValue("")
                .ValidateTrainingCourseFinishDateFieldValue("")
                .ValidateOutcomeFieldSelectedText("")
                .ValidateExpiryDateFieldValue("")
                .ValidateStatusFieldSelectedText("Outstanding")
                .ValidateReferenceNumberFieldValue("")
                .ValidateNotesFieldValue("")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3141")]
        [Description("Step 7 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod004()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("Default - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(3)).ToString("dd'/'MM'/'yyyy");

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default - Internal").TapSearchButton().SelectResultElement(trainingCourseID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingCourseStartDate)
                .InsertReferenceNumber("1A")
                .InsertNotes("Notes ...")
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                //.InsertQuickSearchText("Default")
                .ClickSearchButton();

            var trainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, trainingRecords.Count);

            systemUserTrainingPage
                .OpenRecord(trainingRecords[0].ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default")
                .ValidateTrainingCourseLinkFieldText("Default - Internal")

                .ValidateTrainingCourseStartDateFieldValue(trainingCourseStartDate)
                .ValidateTrainingCourseFinishDateFieldValue("")
                .ValidateOutcomeFieldSelectedText("")
                .ValidateExpiryDateFieldValue("")
                .ValidateStatusFieldSelectedText("Planned")
                .ValidateReferenceNumberFieldValue("1A")
                .ValidateNotesFieldValue("Notes ...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3142")]
        [Description("Step 8 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod005()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("Default - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-3)).ToString("dd'/'MM'/'yyyy");
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(3)).ToString("dd'/'MM'/'yyyy");

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default - Internal").TapSearchButton().SelectResultElement(trainingCourseID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingCourseStartDate)
                .InsertTrainingCourseFinishDate(trainingCourseFinishDate);

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("You cannot finish a Training Course in the future.").TapCloseButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad();

            var trainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(0, trainingRecords.Count);


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3143")]
        [Description("Step 9 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod006()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("Default - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-3)).ToString("dd'/'MM'/'yyyy");

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default - Internal").TapSearchButton().SelectResultElement(trainingCourseID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingCourseStartDate)
                .InsertReferenceNumber("1A")
                .InsertNotes("Notes ...")
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                //.InsertQuickSearchText("Default")
                .ClickSearchButton();

            var trainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, trainingRecords.Count);

            systemUserTrainingPage
                .OpenRecord(trainingRecords[0].ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default")
                .ValidateTrainingCourseLinkFieldText("Default - Internal")

                .ValidateTrainingCourseStartDateFieldValue(trainingCourseStartDate)
                .ValidateTrainingCourseFinishDateFieldValue("")
                .ValidateOutcomeFieldSelectedText("")
                .ValidateExpiryDateFieldValue("")
                .ValidateStatusFieldSelectedText("In Progress")
                .ValidateReferenceNumberFieldValue("1A")
                .ValidateNotesFieldValue("Notes ...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3144")]
        [Description("Step 10 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod007()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("Default - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-12)).ToString("dd'/'MM'/'yyyy");
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).ToString("dd'/'MM'/'yyyy");
            var expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(60)).ToString("dd'/'MM'/'yyyy");

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default - Internal").TapSearchButton().SelectResultElement(trainingCourseID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingCourseStartDate)
                .InsertTrainingCourseFinishDate(trainingCourseFinishDate)
                .SelectOutcome("Pass")
                .InsertExpiryDate(expiryDate)
                .InsertReferenceNumber("1A")
                .InsertNotes("Notes ...")
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                //.InsertQuickSearchText("Default")
                .ClickSearchButton();

            var trainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, trainingRecords.Count);

            systemUserTrainingPage
                .OpenRecord(trainingRecords[0].ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default")
                .ValidateTrainingCourseLinkFieldText("Default - Internal")

                .ValidateTrainingCourseStartDateFieldValue(trainingCourseStartDate)
                .ValidateTrainingCourseFinishDateFieldValue(trainingCourseFinishDate)
                .ValidateOutcomeFieldSelectedText("Pass")
                .ValidateExpiryDateFieldValue(expiryDate)
                .ValidateStatusFieldSelectedText("Current")
                .ValidateReferenceNumberFieldValue("1A")
                .ValidateNotesFieldValue("Notes ...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3145")]
        [Description("Step 11 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod008()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("Default - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            #region System User Training

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-12));
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2));
            var outcomeId = 1; //Pass
            var expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(60));
            var systemUserTrainingId = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourseID, trainingCourseStartDate, trainingCourseFinishDate, outcomeId, expiryDate, "1A", "Notes...", _teamId);

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()

                .ValidateSelectedView("All Training")

                .ValidateRecordVisible(systemUserTrainingId.ToString())
                .ValidateTableHeaderCellText(6, "Status")
                .ValidateRecordCellText(systemUserTrainingId.ToString(), 6, "Current")

                .SelectView("Current Training")

                .ValidateRecordVisible(systemUserTrainingId.ToString())
                .ValidateTableHeaderCellText(4, "Status")
                .ValidateRecordCellText(systemUserTrainingId.ToString(), 4, "Current")
                ;

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3146")]
        [Description("Step 12 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_UITestMethod009()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("Default - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            #region System User Training

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-12));
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2));
            var outcomeId = 1; //Pass
            var expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(60));
            dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourseID, trainingCourseStartDate, trainingCourseFinishDate, outcomeId, expiryDate, "1A", "Notes...", _teamId);

            #endregion


            #region Option Set Value

            var optionSetId = dbHelper.optionSet.GetOptionSetIdByName("StaffTrainingStatus")[0];
            var optionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Current")[0];

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Training")
                .SelectFilter("1", "Status")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForOptionSetLookupPopupToLoad().SelectResultElement(optionSetValueId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Training Item")
                .ResultsPageValidateHeaderCellText(3, "Course Title");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23219

        [TestProperty("JiraIssueID", "ACC-3147")]
        [Description("Steps 1 to 12 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_CDV6_13062_UITestMethod001()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default CDV6_23219", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourse1ID = commonMethodsDB.CreateTrainingRequirement("Default CDV6_23219 - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(2)).ToString("dd'/'MM'/'yyyy");

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            #endregion

            #region Step 3

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #endregion

            #region Step 4 & 5

            systemUserTrainingPage
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingItemErrorLableVisible(false)

                .ClickSaveAndCloseButton()

                .ValidateTrainingItemErrorLableVisible(true)
                .ValidateTrainingItemErrorLableText("Please fill out this field.");

            #endregion

            #region Steps 6 to 9

            systemUserTrainingRecordPage
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default CDV6_23219").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(trainingCourse1ID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingCourseStartDate)
                .InsertNotes("Save with future date...")
                .ClickSaveButton()

                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default CDV6_23219")
                .ValidateTrainingCourseLinkFieldText("Default CDV6_23219 - Internal")

                .ValidateTrainingCourseStartDateFieldValue(trainingCourseStartDate)
                .ValidateTrainingCourseFinishDateFieldValue("")
                .ValidateOutcomeFieldSelectedText("")
                .ValidateExpiryDateFieldValue("")
                .ValidateStatusFieldSelectedText("Planned")
                .ValidateReferenceNumberFieldValue("")
                .ValidateNotesFieldValue("Save with future date...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security")

                .ValidateTrainingAttachmentsAreaVisible(true);

            #endregion

            #region Steps 10

            systemUserTrainingRecordPage
                .ClickBackButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            var records = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, records.Count);
            var newTrainingRecordId = records[0];

            systemUserTrainingPage
                .ValidateRecordVisible(newTrainingRecordId.ToString());

            #endregion

            #region Step 11

            systemUserTrainingPage
                .OpenRecord(newTrainingRecordId.ToString());

            trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).ToString("dd'/'MM'/'yyyy");

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingCourseStartDate)
                .InsertReferenceNumber("1A")
                .InsertNotes("Updated Notes...")
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(newTrainingRecordId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default CDV6_23219")
                .ValidateTrainingCourseLinkFieldText("Default CDV6_23219 - Internal")

                .ValidateTrainingCourseStartDateFieldValue(trainingCourseStartDate)
                .ValidateTrainingCourseFinishDateFieldValue("")
                .ValidateOutcomeFieldSelectedText("")
                .ValidateExpiryDateFieldValue("")
                .ValidateStatusFieldSelectedText("In Progress")
                .ValidateReferenceNumberFieldValue("1A")
                .ValidateNotesFieldValue("Updated Notes...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security")

                .ValidateTrainingAttachmentsAreaVisible(true);

            #endregion

            #region Step 12

            systemUserTrainingRecordPage
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("systemusertraining")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(2, 2, "Created");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3148")]
        [Description("Steps 13 to 16 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [DeploymentItem("Files\\DocToUpload.txt"), DeploymentItem("chromedriver.exe")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_CDV6_13062_UITestMethod002()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default CDV6_23219", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourse1ID = commonMethodsDB.CreateTrainingRequirement("Default CDV6_23219 - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            #region System User Training

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-10));
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2));
            var outcomeId = 1; //Pass
            var expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(60));
            var systemUserTrainingId = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourse1ID, trainingCourseStartDate, trainingCourseFinishDate, outcomeId, expiryDate, "1A", "Notes...", _teamId);

            #endregion


            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad();

            systemUserTrainingRecordPage
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default CDV6_23219").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(trainingCourse1ID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingCourseStartDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTrainingCourseFinishDate(trainingCourseFinishDate.ToString("dd'/'MM'/'yyyy"))
                .SelectOutcome("Pass")
                .InsertExpiryDate(expiryDate.ToString("dd'/'MM'/'yyyy"))
                .InsertReferenceNumber("1A")
                .InsertNotes("Notes...")
                .ClickSaveAndCloseButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            var records = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(2, records.Count);

            systemUserTrainingPage
                .OpenRecord(records[0].ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("Default CDV6_23219")
                .ValidateTrainingCourseLinkFieldText("Default CDV6_23219 - Internal")

                .ValidateTrainingCourseStartDateFieldValue(trainingCourseStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateTrainingCourseFinishDateFieldValue(trainingCourseFinishDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateOutcomeFieldSelectedText("Pass")
                .ValidateExpiryDateFieldValue(expiryDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateStatusFieldSelectedText("Current")
                .ValidateReferenceNumberFieldValue("1A")
                .ValidateNotesFieldValue("Notes...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security")

                .ValidateTrainingAttachmentsAreaVisible(true);

            #endregion

            #region Step 14

            systemUserTrainingRecordPage
                .ClickBackButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .SelectRecord(records[0].ToString())
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("1 item(s) deleted.").TapOKButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            records = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, records.Count);

            #endregion

            #region Step 15

            systemUserTrainingPage
                .OpenRecord(systemUserTrainingId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad();

            systemUserTrainingAttachmentsPage
                .WaitForSystemUserTrainingAttachmentsSubgridPageToLoad()
                .ClickAddNewButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("systemusertrainingattachment")
                .ClickOnExpandIcon();

            systemUserTrainingAttachmentRecordPage
                .WaitForSystemUserTrainingAttachmentRecordPageToLoad()
                .InsertName("Att 01")
                .UploadFile(TestContext.DeploymentDirectory + "\\DocToUpload.txt")
                .ClickSaveAndCloseButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad();

            #endregion

            #region Step 16

            systemUserTrainingRecordPage
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Related record exists in Training Attachment. Please delete related records before deleting record in System User Training.")
                .TapCloseButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad();

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-3149")]
        [Description("Step 17 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_CDV6_13062_UITestMethod003()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default CDV6_23219", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourse1ID = commonMethodsDB.CreateTrainingRequirement("Default CDV6_23219 - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-10)).ToString("dd'/'MM'/'yyyy");
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).ToString("dd'/'MM'/'yyyy");

            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad();

            systemUserTrainingRecordPage
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default CDV6_23219").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(trainingCourse1ID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingCourseFinishDateFieldDisabled(true)
                .InsertTrainingCourseStartDate(trainingCourseStartDate)
                .ValidateTrainingCourseFinishDateFieldDisabled(false)
                .InsertTrainingCourseFinishDate(trainingCourseFinishDate)

                .InsertTrainingCourseStartDate("")
                .ValidateTrainingCourseFinishDateFieldDisabled(true)
                .ValidateTrainingCourseFinishDateFieldValue("")
                ;

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3150")]
        [Description("Step 18 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void SystemUser_CDV6_13062_UITestMethod004()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default CDV6_23219", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourse1ID = commonMethodsDB.CreateTrainingRequirement("Default CDV6_23219 - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            #region System User Training

            DateTime trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-10));
            DateTime? trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2));
            int outcomeId = 1; //Pass
            DateTime? expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(60));

            var systemUserTraining1Id = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourse1ID, trainingCourseStartDate, trainingCourseFinishDate, outcomeId, expiryDate, "1A", "Notes...", _teamId);

            trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5));
            expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2));
            var systemUserTraining2Id = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourse1ID, trainingCourseStartDate, trainingCourseFinishDate, outcomeId, expiryDate, "1A", "Notes...", _teamId);

            #endregion

            #region Step 18

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateSelectedView("All Training")
                .ValidateRecordVisible(systemUserTraining1Id.ToString())
                .ValidateRecordVisible(systemUserTraining2Id.ToString())

                .SelectView("Current Training")
                .ValidateRecordVisible(systemUserTraining1Id.ToString())
                .ValidateRecordNotVisible(systemUserTraining2Id.ToString())

                .SelectView("Expired Training")
                .ValidateRecordNotVisible(systemUserTraining1Id.ToString())
                .ValidateRecordVisible(systemUserTraining2Id.ToString());



            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3151")]
        [Description("Steps 19 to 20 from the original test method")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        public void SystemUser_CDV6_13062_UITestMethod005()
        {
            #region System User AllActivitiesUser1

            var userFirstName = "User_CDV6_22351";
            var userLastName = commonMethodsHelper.GetCurrentDateTimeString();
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default CDV6_23219", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourse1ID = commonMethodsDB.CreateTrainingRequirement("Default CDV6_23219 - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Training")

                .SelectFilter("1", "Start Date")
                .SelectFilter("1", "Finish Date")
                .SelectFilter("1", "Expiry Date");

            #endregion

            #region Step 20

            advanceSearchPage
                .SelectSavedViewWithoutWaitingForRefreshPanel("Active Records");

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Do you want to discard pending changes and load the query?").TapOKButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()

                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoadFromAdvancedSearch()
                .ClickRegardingLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery(username).TapSearchButton().SelectResultElement(systemUserId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoadFromAdvancedSearch()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default CDV6_23219").TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoadFromAdvancedSearch()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            advanceSearchPage
                .WaitForResultsPageToLoad();

            var records = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(1, records.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23548

        [TestProperty("JiraIssueID", "ACC-3152")]
        [Description("Step 1 to 3 from the original test method: Test case to verify Training Record the Expiry Date should be calculated as per current recurrence value (For )")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_CDV6_23548_UITestMethod001()
        {
            #region System User Test User

            var userFirstName = "User_CDV6_23548";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item
            String partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "23548_Default_" + partialDateTimeString, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var monthly_RecurrenceId = 3;
            var quarterly_RecurrenceId = 4;
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("23548 - Internal" + partialDateTimeString, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, monthly_RecurrenceId, categoryid);
            var trainingStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("dd'/'MM'/'yyyy");
            var trainingExpiryDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(1).Date.ToString("dd'/'MM'/'yyyy");

            #endregion

            #region Step 1 to 3

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("23548_Default_" + partialDateTimeString).TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("23548 - Internal" + partialDateTimeString).TapSearchButton().SelectResultElement(trainingCourseID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingStartDate)
                .InsertTrainingCourseFinishDate(trainingStartDate)
                .SelectOutcome("Pass")
                .ValidateExpiryDateFieldValue(trainingExpiryDate)
                .InsertReferenceNumber("1A")
                .InsertNotes("Notes ...")
                .ClickSaveButton()
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickBackButton();

            var systemUserTrainingID = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId)[0];

            dbHelper.trainingRequirement.UpdateRecurrence(trainingCourseID, quarterly_RecurrenceId);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(systemUserTrainingID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateExpiryDateFieldValue(trainingExpiryDate);

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("23548_Default_" + partialDateTimeString)
                .ValidateTrainingCourseLinkFieldText("23548 - Internal" + partialDateTimeString)
                .ValidateTrainingCourseStartDateFieldValue(trainingStartDate)
                .ValidateTrainingCourseFinishDateFieldValue(trainingStartDate)
                .ValidateOutcomeFieldSelectedText("Pass")
                .ValidateStatusFieldSelectedText("Current")
                .ValidateReferenceNumberFieldValue("1A")
                .ValidateNotesFieldValue("Notes ...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security");

            trainingExpiryDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(3).Date.ToString("dd'/'MM'/'yyyy");
            systemUserTrainingRecordPage
                .InsertTrainingCourseFinishDate("")
                .InsertTrainingCourseFinishDate(trainingStartDate)
                .SelectOutcome("Pass")
                .ValidateExpiryDateFieldValue(trainingExpiryDate)
                .ClickSaveButton()
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateExpiryDateFieldValue(trainingExpiryDate);
            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23659

        [TestProperty("JiraIssueID", "ACC-3153")]
        [Description("Step 1 to 6 from the original test method: Test case is to verify the Status validation including Outcome field")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_CDV6_23659_UITestMethod001()
        {
            #region System User Test User

            var userFirstName = "User_CDV6_23659";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item
            String partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "23659_Default_" + partialDateTimeString, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var monthly_RecurrenceId = 3;
            var outcomeId = 1; //Pass
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("23659 - Internal" + partialDateTimeString, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, monthly_RecurrenceId, categoryid);
            var trainingStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            var trainingFinishDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-1);
            var trainingExpiryDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(1).Date;
            var systemUserTrainingId = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourseID, trainingStartDate, trainingStartDate, outcomeId, trainingExpiryDate, "1A", "Notes...", _teamId, userFirstName + " " + userLastName);
            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            #endregion

            #region Step 2

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(systemUserTrainingId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateOutcomeFieldDisplayed();

            #endregion

            #region Step 3

            systemUserTrainingRecordPage
                .ClickBackButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickAddNewButton();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("23659_Default_" + partialDateTimeString).TapSearchButton().SelectResultElement(staffTrainingItemID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("23659 - Internal" + partialDateTimeString).TapSearchButton().SelectResultElement(trainingCourseID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingStartDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTrainingCourseFinishDate(trainingFinishDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateOutcomeFieldDisabled(false)
                .SelectOutcome("Fail")
                .InsertReferenceNumber("1A")
                .InsertNotes("Notes ...")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Failed")

                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText("23659_Default_" + partialDateTimeString)
                .ValidateTrainingCourseLinkFieldText("23659 - Internal" + partialDateTimeString)
                .ValidateTrainingCourseStartDateFieldValue(trainingStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateTrainingCourseFinishDateFieldValue(trainingFinishDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateOutcomeFieldSelectedText("Fail")
                .ValidateReferenceNumberFieldValue("1A")
                .ValidateNotesFieldValue("Notes ...")
                .ValidateResponsibleTeamLinkFieldText("CareProviders Security");

            #endregion

            #region Step 4

            systemUserTrainingRecordPage
                .ClickBackButton();

            var failed_systemUserTrainingID = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingNameAndRegardingUserId("23659 - Internal" + partialDateTimeString, systemUserId)[0];
            var current_systemUserTrainingID = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingNameAndRegardingUserId("23659 - Internal" + partialDateTimeString, systemUserId)[1];

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateRecordCellText(failed_systemUserTrainingID.ToString(), 6, "Failed");

            #endregion

            #region Step 5

            systemUserTrainingPage
                .SelectView("Failed Training")
                .ValidateSelectedView("Failed Training")
                .ValidateRecordVisible(failed_systemUserTrainingID.ToString())
                .ValidateRecordNotVisible(current_systemUserTrainingID.ToString());

            #endregion

            #region Step 6

            systemUserTrainingPage
                .OpenRecord(failed_systemUserTrainingID.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .SelectOutcome("Complete")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Current");

            systemUserTrainingRecordPage
                .ClickBackButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateRecordNotVisible(failed_systemUserTrainingID.ToString());

            systemUserTrainingPage
                .SelectView("All Training")
                .ValidateSelectedView("All Training")
                .ValidateRecordVisible(failed_systemUserTrainingID.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3154")]
        [Description("Step 6 to 612 from the original test method: Test case is to verify the Status validation including Outcome field")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        [TestProperty("Screen2", "Advanced Search")]
        public void SystemUser_CDV6_23659_UITestMethod002()
        {
            #region System User Test User

            var userFirstName = "User_CDV6_23659";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item
            String partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "23659_Default_" + partialDateTimeString, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var monthly_RecurrenceId = 3;
            var pass_outcomeId = 1; //Pass
            var fail_outcomeId = 2; //Fail
            var trainingCourseID = commonMethodsDB.CreateTrainingRequirement("23659 - Internal" + partialDateTimeString, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, monthly_RecurrenceId, categoryid);
            var trainingStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var trainingFinishDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            var trainingExpiryDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddMonths(1).Date;
            var systemUserTrainingId = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourseID, trainingStartDate, trainingStartDate, pass_outcomeId, trainingExpiryDate, "1A", "Notes...", _teamId, userFirstName + " " + userLastName);
            var failed_systemUserTrainingId = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourseID, trainingStartDate, trainingStartDate, fail_outcomeId, null, "1A", "Notes...", _teamId, userFirstName + " " + userLastName);
            var systemUserTraining2Id = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourseID, trainingStartDate, trainingFinishDate, pass_outcomeId, trainingExpiryDate, "1A", "Notes...", _teamId, userFirstName + " " + userLastName);

            var Outcome_BusinessObjectFieldId = new Guid("8939427c-dc30-ec11-a343-0050569231cf"); //Outcome
            #endregion

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .SelectView("All Training")
                .OpenRecord(systemUserTrainingId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .SelectOutcome("Fail")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Failed");

            #endregion

            #region Step 8

            systemUserTrainingRecordPage
                .ClickBackButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .SelectView("Failed Training")
                .ValidateSelectedView("Failed Training")
                .ValidateRecordVisible(systemUserTrainingId.ToString());

            #endregion

            #region Step 9

            systemUserTrainingPage
                .SelectView("Current Training")
                .OpenRecord(systemUserTraining2Id.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertExpiryDate(commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateStatusFieldSelectedText("Expired");

            systemUserTrainingRecordPage
                .ClickBackButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateRecordNotVisible(systemUserTraining2Id.ToString());

            systemUserTrainingPage
                .SelectView("Expired Training")
                .ValidateRecordVisible(systemUserTraining2Id.ToString());

            #endregion

            #region Step 10

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("System User Training")
                .SelectFilter("1", "Outcome");

            #endregion

            #region Step 11

            advanceSearchPage
                .ClickChooseColumnsButton()
                .WaitForChooseColumnsPopupToLoad()
                .ChooseColumnsPopup_ValidateColumn(Outcome_BusinessObjectFieldId.ToString(), "Outcome")
                .ChooseColumnsPopup_ClickOKButton();

            #endregion

            #region Step 12

            advanceSearchPage
                .SelectFilter("1", "Outcome")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            optionSetFormPopUp.WaitForOptionSetFormPopUpToLoad().TypeSearchQuery("Fail").TapSearchButton().SelectResult("Fail");

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(systemUserTrainingId.ToString());

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23668

        [TestProperty("JiraIssueID", "ACC-3155")]
        [Description("Step 1 to 4 from the original test method: Test Case is to Verify the change request made for Training Course and Outcome field in System User Training Record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_CDV6_23668_UITestMethod001()
        {
            string partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-12)).ToString("dd'/'MM'/'yyyy");
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).ToString("dd'/'MM'/'yyyy");

            #region System User Test User

            var userFirstName = "User_CDV6_23713";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "23713_Default_" + partialDateTimeString, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Care provider staff role type

            string roleName = "CDV6_23713" + partialDateTimeString;
            var _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName, "", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("Training_23713_" + partialDateTimeString, staffTrainingItemID, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "Item_23713_" + partialDateTimeString + " - Internal";
            var StaffTrainingItemId = dbHelper.trainingRequirement.CreateTrainingRequirement(trainingTitle, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Employment Contract Type

            if (!dbHelper.employmentContractType.GetByName("CDV6_23713").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_teamId, "CDV6_23713", "23713", null, new DateTime(2020, 1, 1));

            var _employmentContractTypeid = dbHelper.employmentContractType.GetByName("CDV6_23713").FirstOrDefault();

            #endregion

            #region system User Employment Contract

            if (!dbHelper.systemUserEmploymentContract.GetBySystemUserId(systemUserId).Any())
                dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(3000);

            #endregion

            #region System User Training 

            var trainingRecordsId1 = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId).FirstOrDefault();

            #endregion

            #region Step 1 to 2

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(trainingRecordsId1.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingCourseMandatoryField(false);

            #endregion

            #region Step 3

            systemUserTrainingRecordPage
                .InsertTrainingCourseStartDate(trainingCourseStartDate.ToString())
                .InsertTrainingCourseFinishDate(trainingCourseFinishDate.ToString())
                .ValidateTrainingCourseMandatoryField(true)
                .ClickSaveButton();

            systemUserTrainingRecordPage
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateTrainingCourseFieldErrorNotificationMessageText("Please fill out this field.");

            #endregion

            #region Step 4

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(trainingTitle).TapSearchButton().SelectResultElement(StaffTrainingItemId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateOutcomeMandatoryField(true)
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateOutcomeFieldErrorNotificationMessageText("Please fill out this field.");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3156")]
        [Description("Step 5 to 8 from the original test method: Test Case is to Verify the change request made for Training Course and Outcome field in System User Training Record.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_CDV6_23668_UITestMethod002()
        {
            String partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-12)).ToString("dd'/'MM'/'yyyy");
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).ToString("dd'/'MM'/'yyyy");

            #region System User Test User

            var userFirstName = "User_CDV6_23714";
            var userLastName = partialDateTimeString;
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "23714_Default_" + partialDateTimeString, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Care provider staff role type

            string roleName = "CDV6_23714" + partialDateTimeString;
            var _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName, "", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Training Requirement 

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("Training_23714_" + partialDateTimeString, staffTrainingItemID, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "Item_23714_" + partialDateTimeString + " - Internal";
            var StaffTrainingItemId = dbHelper.trainingRequirement.CreateTrainingRequirement(trainingTitle, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Employment Contract Type

            if (!dbHelper.employmentContractType.GetByName("CDV6_23714").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_teamId, "CDV6_23714", "23714", null, new DateTime(2020, 1, 1));
            var _employmentContractTypeid = dbHelper.employmentContractType.GetByName("CDV6_23714").FirstOrDefault();

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateNoRecordMessageVisibile(true);

            #region System User Employment Contract

            dbHelper = new DBHelper.DatabaseHelper();
            if (!dbHelper.systemUserEmploymentContract.GetBySystemUserId(systemUserId).Any())
                dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);
            var EmploymentContractId = dbHelper.systemUserEmploymentContract.GetBySystemUserId(systemUserId).FirstOrDefault();
            var trainingRecordsId = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId).FirstOrDefault();

            #endregion

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateRecordVisibility(EmploymentContractId.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(trainingRecordsId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingCourseMandatoryField(false);

            #endregion

            #region Step 6

            systemUserTrainingRecordPage
                .InsertTrainingCourseStartDate(trainingCourseStartDate.ToString())
                .InsertTrainingCourseFinishDate(trainingCourseFinishDate.ToString())
                .ValidateTrainingCourseMandatoryField(true)
                .ClickSaveButton();

            systemUserTrainingRecordPage
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateTrainingCourseFieldErrorNotificationMessageText("Please fill out this field.");

            #endregion

            #region Step 7 & 8

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(trainingTitle).TapSearchButton().SelectResultElement(StaffTrainingItemId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateOutcomeMandatoryField(true)
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateOutcomeFieldErrorNotificationMessageText("Please fill out this field.");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-23665

        [TestProperty("JiraIssueID", "ACC-3157")]
        [Description("Step 3, 5 to 7 from the original test method: Test case is to verify the creation of Training Items for a new Contract based on the Role.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("BusinessModule2", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Training")]
        [TestProperty("Screen2", "System User Employment Contracts")]
        public void SystemUser_Training_CDV6_23665_UITestMethod001()
        {
            string partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-12)).ToString("dd'/'MM'/'yyyy");
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).ToString("dd'/'MM'/'yyyy");

            #region System User Test User

            var userFirstName = "User_CDV6_24014";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemName = "24014_Default_" + partialDateTimeString;
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName2 = "24014_Default_2_" + partialDateTimeString;
            var staffTrainingItemID2 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName2, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Care provider staff role type

            string roleName = "CDV6_24014" + partialDateTimeString;
            var _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName, "", null, new DateTime(2020, 1, 1), null);

            string roleName2 = "CDV6_24014_2" + partialDateTimeString;
            var _careProviderStaffRoleTypeid2 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName2, "", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("Training_24014_" + partialDateTimeString, staffTrainingItemID, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "Item_24014_" + partialDateTimeString + " - Internal";
            dbHelper.trainingRequirement.CreateTrainingRequirement(trainingTitle, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, 6, 1);

            List<Guid> careProviderStaffRoleTypeIds2 = new List<Guid> { _careProviderStaffRoleTypeid2 };
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("Training_24014_2_" + partialDateTimeString, staffTrainingItemID2, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds2);
            string trainingTitle2 = "Item_24014_2_" + partialDateTimeString + " - Internal";
            dbHelper.trainingRequirement.CreateTrainingRequirement(trainingTitle2, _teamId, staffTrainingItemID2, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Employment Contract Type

            if (!dbHelper.employmentContractType.GetByName("CDV6_24014").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_teamId, "CDV6_24014", "24014", null, new DateTime(2020, 1, 1));
            var _employmentContractTypeid = dbHelper.employmentContractType.GetByName("CDV6_24014").FirstOrDefault();

            if (!dbHelper.employmentContractType.GetByName("CDV6_24014_2").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_teamId, "CDV6_24014_2", "240142", null, new DateTime(2020, 1, 1));
            var _employmentContractTypeid2 = dbHelper.employmentContractType.GetByName("CDV6_24014_2").FirstOrDefault();

            #endregion

            #region system User Employment Contract

            if (!dbHelper.systemUserEmploymentContract.GetBySystemUserId(systemUserId).Any())
                dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region System User Training 

            var trainingRecordsId1 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId).FirstOrDefault();

            #endregion

            #region Step 3, 5 & 6

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(trainingRecordsId1.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateRegardingLinkFieldText(userFirstName + " " + userLastName)
                .ValidateTrainingItemLinkFieldText(staffTrainingItemName)
                .ValidateStatusFieldDisabled(true)
                .ValidateStatusFieldSelectedText("Outstanding")
                .ClickBackButton();

            #endregion

            #region Step 7

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #region System User Employment Contract

            dbHelper = new DBHelper.DatabaseHelper();
            var EmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid2, _teamId, _employmentContractTypeid2, null);

            System.Threading.Thread.Sleep(2000);
            var trainingRecordsId2 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID2, systemUserId).FirstOrDefault();

            #endregion

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .ValidateNoRecordMessageVisibile(false)
                .ValidateRecordVisibility(EmploymentContractId.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(trainingRecordsId2.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingCourseMandatoryField(false)
                .ValidateTrainingItemLinkFieldText(staffTrainingItemName2)
                .ValidateStatusFieldSelectedText("Outstanding")
                .InsertTrainingCourseStartDate(trainingCourseStartDate.ToString())
                .InsertTrainingCourseFinishDate(trainingCourseFinishDate.ToString())
                .ValidateTrainingCourseMandatoryField(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3158")]
        [Description("Step 8 to 9 from the original test method: Test case is to verify the creation of Training Items for a new Contract based on the Role.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("BusinessModule2", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Training")]
        [TestProperty("Screen2", "System User Employment Contracts")]
        public void SystemUser_Training_CDV6_23665_UITestMethod002()
        {
            string partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-12)).ToString("dd'/'MM'/'yyyy");
            var trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-4)).ToString("dd'/'MM'/'yyyy");
            var expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2)).ToString("dd'/'MM'/'yyyy");

            #region System User Test User

            var userFirstName = "User_CDV6_24018";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemName = "24018_Default_" + partialDateTimeString;
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Care provider staff role type

            string roleName = "CDV6_24018" + partialDateTimeString;
            var _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName, "", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("Training_24018_" + partialDateTimeString, staffTrainingItemID, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "Item_24018_" + partialDateTimeString + " - Internal";
            var StaffTrainingItemId = dbHelper.trainingRequirement.CreateTrainingRequirement(trainingTitle, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Employment Contract Type

            if (!dbHelper.employmentContractType.GetByName("CDV6_24018").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_teamId, "CDV6_24018", "24018", null, new DateTime(2020, 1, 1));
            var _employmentContractTypeid = dbHelper.employmentContractType.GetByName("CDV6_24018").FirstOrDefault();

            #endregion

            #region system User Employment Contract

            if (!dbHelper.systemUserEmploymentContract.GetBySystemUserId(systemUserId).Any())
                dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);

            #endregion

            #region System User Training 

            var trainingRecordsId1 = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId).FirstOrDefault();

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateTableHeaderCellText(6, "Status")
                .ValidateRecordCellText(trainingRecordsId1.ToString(), 6, "Outstanding");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #region System User Employment Contract

            dbHelper = new DBHelper.DatabaseHelper();
            var EmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);
            List<Guid> trainingRecordIdsCount = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId);

            #endregion

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .ValidateRecordVisibility(EmploymentContractId2.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            Assert.AreEqual(1, trainingRecordIdsCount.Count);

            #endregion

            #region Step 9

            systemUserTrainingPage
            .OpenRecord(trainingRecordsId1.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingItemLinkFieldText(staffTrainingItemName)
                .ClickTrainingCourseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(trainingTitle).TapSearchButton().SelectResultElement(StaffTrainingItemId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .InsertTrainingCourseStartDate(trainingCourseStartDate)
                .InsertTrainingCourseFinishDate(trainingCourseFinishDate)
                .SelectOutcome("Complete")
                .InsertExpiryDate(expiryDate)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Expired")
                .ClickBackButton();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #region System User Employment Contract

            dbHelper = new DBHelper.DatabaseHelper();
            var EmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);
            trainingRecordIdsCount = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId);

            #region System User Training 

            var trainingRecordsId2 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId).FirstOrDefault();

            #endregion

            #endregion

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .ValidateRecordVisibility(EmploymentContractId3.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            Assert.AreEqual(2, trainingRecordIdsCount.Count);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateTableHeaderCellText(6, "Status")
                .ValidateRecordCellText(trainingRecordsId1.ToString(), 6, "Expired")
                .ValidateRecordCellText(trainingRecordsId2.ToString(), 6, "Outstanding");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3159")]
        [Description("Step 10 to 16 from the original test method: Test case is to verify the creation of Training Items for a new Contract based on the Role.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("Screen1", "System User Training")]
        public void SystemUser_Training_CDV6_23665_UITestMethod003()
        {
            #region System User Test User

            var userFirstName = "User_CDV6_24023";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default CDV6_24023", commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Training Course

            var categoryid = 1; // internal
            var trainingCourse1ID = commonMethodsDB.CreateTrainingRequirement("Default CDV6_24023 - Internal", _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, null, categoryid);

            #endregion

            #region System User Training

            DateTime trainingCourseStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-10));
            DateTime? trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-4));
            int outcomeId = 1; //Pass
            int outcomeId2 = 2; //Fail
            DateTime? expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(60));

            var systemUserTrainingId_Current = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourse1ID, trainingCourseStartDate, trainingCourseFinishDate, outcomeId, expiryDate, "1A", "Notes...", _teamId, userFirstName + " " + userLastName);

            trainingCourseFinishDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-5));
            expiryDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.AddDays(-2));
            var systemUserTrainingId_Expired = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourse1ID, trainingCourseStartDate, trainingCourseFinishDate, outcomeId, expiryDate, "2A", "Notes...", _teamId, userFirstName + " " + userLastName);

            var systemUserTrainingId_Failed = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourse1ID, trainingCourseStartDate, trainingCourseFinishDate, outcomeId2, null, "3A", "Notes...", _teamId, userFirstName + " " + userLastName); ;
            var systemUserTrainingId_InProgress = dbHelper.systemUserTraining.CreateSystemUserTraining(systemUserId, staffTrainingItemID, trainingCourse1ID, trainingCourseStartDate, null, null, null, "4A", "Notes...", _teamId, userFirstName + " " + userLastName);

            #endregion

            #region Steps 10

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateSelectedView("All Training")
                .ValidateRecordVisible(systemUserTrainingId_Current.ToString())
                .ValidateRecordVisible(systemUserTrainingId_Expired.ToString())
                .ValidateRecordVisible(systemUserTrainingId_Failed.ToString())
                .ValidateRecordVisible(systemUserTrainingId_InProgress.ToString());

            systemUserTrainingPage
                .ValidateTableHeaderCellText(2, "Training Item")
                .ValidateTableHeaderCellText(3, "Course Title")
                .ValidateTableHeaderCellText(4, "Regarding")
                .ValidateTableHeaderCellText(5, "Responsible Team")
                .ValidateTableHeaderCellText(6, "Status")
                .ValidateTableHeaderCellText(7, "Start Date")
                .ValidateTableHeaderCellText(8, "Finish Date")
                .ValidateTableHeaderCellText(9, "Expiry Date");

            systemUserTrainingPage
                .SelectView("Current Training")
                .ValidateRecordVisible(systemUserTrainingId_Current.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Expired.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Failed.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_InProgress.ToString());

            systemUserTrainingPage
                .ValidateTableHeaderCellText(2, "Training Item")
                .ValidateTableHeaderCellText(3, "Course Title")
                .ValidateTableHeaderCellText(4, "Status")
                .ValidateTableHeaderCellText(5, "Start Date")
                .ValidateTableHeaderCellText(6, "Finish Date")
                .ValidateTableHeaderCellText(7, "Expiry Date")
                .ValidateTableHeaderCellText(8, "Created By")
                .ValidateTableHeaderCellText(9, "Created On")
                .ValidateTableHeaderCellText(10, "Modified By")
                .ValidateTableHeaderCellText(11, "Modified On");

            systemUserTrainingPage
                .SelectView("Expired Training")
                .ValidateRecordVisible(systemUserTrainingId_Expired.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Current.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Failed.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_InProgress.ToString());

            systemUserTrainingPage
                .ValidateTableHeaderCellText(2, "Training Item")
                .ValidateTableHeaderCellText(3, "Course Title")
                .ValidateTableHeaderCellText(4, "Status")
                .ValidateTableHeaderCellText(5, "Start Date")
                .ValidateTableHeaderCellText(6, "Finish Date")
                .ValidateTableHeaderCellText(7, "Expiry Date")
                .ValidateTableHeaderCellText(8, "Created By")
                .ValidateTableHeaderCellText(9, "Created On")
                .ValidateTableHeaderCellText(10, "Modified By")
                .ValidateTableHeaderCellText(11, "Modified On");

            systemUserTrainingPage
                .SelectView("Failed Training")
                .ValidateRecordVisible(systemUserTrainingId_Failed.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Current.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Expired.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_InProgress.ToString());

            systemUserTrainingPage
                .ValidateTableHeaderCellText(2, "Training Item")
                .ValidateTableHeaderCellText(3, "Course Title")
                .ValidateTableHeaderCellText(4, "Status")
                .ValidateTableHeaderCellText(5, "Start Date")
                .ValidateTableHeaderCellText(6, "Finish Date")
                .ValidateTableHeaderCellText(7, "Expiry Date")
                .ValidateTableHeaderCellText(8, "Created By")
                .ValidateTableHeaderCellText(9, "Created On")
                .ValidateTableHeaderCellText(10, "Modified By")
                .ValidateTableHeaderCellText(11, "Modified On");

            systemUserTrainingPage
                .SelectView("In Progress Training")
                .ValidateRecordVisible(systemUserTrainingId_InProgress.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Current.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Failed.ToString())
                .ValidateRecordNotVisible(systemUserTrainingId_Expired.ToString());

            systemUserTrainingPage
                .ValidateTableHeaderCellText(2, "Training Item")
                .ValidateTableHeaderCellText(3, "Course Title")
                .ValidateTableHeaderCellText(4, "Status")
                .ValidateTableHeaderCellText(5, "Start Date")
                .ValidateTableHeaderCellText(6, "Finish Date")
                .ValidateTableHeaderCellText(7, "Expiry Date")
                .ValidateTableHeaderCellText(8, "Created By")
                .ValidateTableHeaderCellText(9, "Created On")
                .ValidateTableHeaderCellText(10, "Modified By")
                .ValidateTableHeaderCellText(11, "Modified On");

            systemUserTrainingPage
                .OpenRecord(systemUserTrainingId_InProgress.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-3160")]
        [Description("Step 17 to 19 from the original test method: Test case is to verify the creation of Training Items for a new Contract based on the Role.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("BusinessModule2", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Training")]
        [TestProperty("Screen2", "System User Employment Contracts")]
        public void SystemUser_Training_CDV6_23665_UITestMethod004()
        {
            string partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");

            #region System User Test User

            var userFirstName = "User_CDV6_24046";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemName = "24046_Default_" + partialDateTimeString;
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName_First = "First_Training_" + partialDateTimeString;
            var staffTrainingItemName_Second = "Second_Training_" + partialDateTimeString;
            var staffTrainingItemName_All = "All_Default_Training_" + partialDateTimeString;

            var staffTrainingItemID_First = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName_First, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);
            var staffTrainingItemID_Second = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName_Second, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);
            var staffTrainingItemID_All = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName_All, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Care provider staff role type

            string roleName = "CDV6_24046" + partialDateTimeString;
            var _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName, "", null, new DateTime(2020, 1, 1), null);

            string roleName2 = "Roles_Default_" + partialDateTimeString;
            var _careProviderStaffRoleTypeId_Default = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName2, "", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Training Requirement Setup

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid };
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("Training_24046_" + partialDateTimeString, staffTrainingItemID, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "Item_24046_" + partialDateTimeString + " - Internal";
            dbHelper.trainingRequirement.CreateTrainingRequirement(trainingTitle, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, 6, 1);

            List<Guid> careProviderStaffRoleTypeIds_Default = new List<Guid> { _careProviderStaffRoleTypeId_Default };
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("First_Requirement_Setup_" + partialDateTimeString, staffTrainingItemID_First, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds_Default);
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("Second_Requirement_Setup_" + partialDateTimeString, staffTrainingItemID_Second, new DateTime(2022, 1, 1), new DateTime(2023, 1, 1), false, 4, careProviderStaffRoleTypeIds_Default);
            dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup("All_Requirement_Setup_" + partialDateTimeString, staffTrainingItemID_All, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds_Default);

            #endregion

            #region Employment Contract Type

            if (!dbHelper.employmentContractType.GetByName("CDV6_24046").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_teamId, "CDV6_24046", "24046", null, new DateTime(2020, 1, 1));
            var _employmentContractTypeid = dbHelper.employmentContractType.GetByName("CDV6_24046").FirstOrDefault();

            if (!dbHelper.employmentContractType.GetByName("CDV6_24046_2").Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(_teamId, "CDV6_24046_2", "240462", null, new DateTime(2020, 1, 1));
            var _employmentContractTypeid2 = dbHelper.employmentContractType.GetByName("CDV6_24046_2").FirstOrDefault();

            #endregion

            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #region System User Employment Contract

            dbHelper = new DBHelper.DatabaseHelper();
            var EmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);
            List<Guid> trainingRecordIdsCount = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId);

            #endregion

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .ValidateRecordVisibility(EmploymentContractId1.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            Assert.AreEqual(1, trainingRecordIdsCount.Count);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(EmploymentContractId1.ToString(), true)
                .OpenRecord(EmploymentContractId1.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate(DateTime.Now.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"), "01:00")
                .InsertDescription("Employment Contract")
                .ClickEntitledToAnnualLeaveAccrual_NoRadioButton()
                .ClickSaveButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .ValidateRecordVisibility(EmploymentContractId1.ToString(), true);

            trainingRecordIdsCount = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            Assert.AreEqual(1, trainingRecordIdsCount.Count);

            #endregion

            #region Step 18 & 19

            dbHelper = new DBHelper.DatabaseHelper();
            var EmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeId_Default, _teamId, _employmentContractTypeid2, null);

            System.Threading.Thread.Sleep(2000);
            List<Guid> trainingRecordIdsCount_First = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID_First, systemUserId);
            List<Guid> trainingRecordIdsCount_Second = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID_Second, systemUserId);
            List<Guid> trainingRecordIdsCount_All = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID_All, systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(EmploymentContractId2.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            Assert.AreEqual(1, trainingRecordIdsCount_First.Count);
            Assert.AreEqual(0, trainingRecordIdsCount_Second.Count);
            Assert.AreEqual(1, trainingRecordIdsCount_All.Count);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4065

        [TestProperty("JiraIssueID", "ACC-4076")]
        [Description("Cascade Create New Training Items to Current Staff")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("BusinessModule2", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Training")]
        [TestProperty("Screen2", "System User Employment Contracts")]
        public void SystemUserTraining_ACC4063_UITestMethod001()
        {
            #region Step 1 to Step 3

            //Steps 1, 2, and 3 will be ignored for automation as its not possible to activate or deactivate business modules programmatically.

            #endregion

            #region Step 4

            #region System User Test User

            string partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");

            var userFirstName = "User4076";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemName = "STI4063_" + partialDateTimeString;
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName2 = "TrainingB_" + partialDateTimeString;
            var staffTrainingItemId2 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName2, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName3 = "TrainingC_" + partialDateTimeString;
            var staffTrainingItemId3 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName3, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName4 = "TrainingD_" + partialDateTimeString;
            var staffTrainingItemId4 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName4, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName5 = "TrainingE_" + partialDateTimeString;
            var staffTrainingItemId5 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName5, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Care provider staff role type

            Guid _defaultCareProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Default CPSTRT", null, null, new DateTime(2021, 1, 1), "Default CPSTRT ...");

            string roleName = "Role4063A_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, roleName, "", null, new DateTime(2020, 1, 1), null);

            string roleName2 = "Role4063B_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid2 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName2, "", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Training Requirement Setup

            foreach (var trId in dbHelper.trainingRequirementSetup.GetByAllRoles(true))
                dbHelper.trainingRequirementSetup.UpdateAllRoles(trId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { };
            commonMethodsDB.CreateTrainingRequirementSetup("TR4063_" + partialDateTimeString, staffTrainingItemID, new DateTime(2022, 1, 1), null, true, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "TC4063_" + partialDateTimeString + " - Internal";
            commonMethodsDB.CreateTrainingRequirement(trainingTitle, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #region System User Employment Contract

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var EmploymentContractId1 = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, null, _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);
            List<Guid> trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId);

            #endregion

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(EmploymentContractId1.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            Assert.AreEqual(1, trainingRecordIds.Count);

            commonMethodsDB.CreateTrainingRequirementSetup("TR4063_B_" + partialDateTimeString, staffTrainingItemId2, new DateTime(2022, 1, 1), null, true, 4, careProviderStaffRoleTypeIds);
            string trainingTitle2 = "TC4063_B_" + partialDateTimeString + " - Internal";
            var _trainingCourseSetupId2 = commonMethodsDB.CreateTrainingRequirement(trainingTitle2, _teamId, staffTrainingItemId2, new DateTime(2022, 1, 1), null, 6, 1);

            #region Execute Job 'Create Missing Training Items'

            Guid CreateMissingRecruitmentDocumentsId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Training Items")[0];
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(CreateMissingRecruitmentDocumentsId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(CreateMissingRecruitmentDocumentsId);
            System.Threading.Thread.Sleep(2000);
            #endregion

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            System.Threading.Thread.Sleep(2000);

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(2, trainingRecordIds.Count);

            #endregion

            #region Step 5

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            commonMethodsDB.CreateTrainingRequirementSetup("TR4063_C_" + partialDateTimeString, staffTrainingItemId3, new DateTime(2022, 1, 1), DateTime.Now.AddDays(2), true, 4, careProviderStaffRoleTypeIds);
            string trainingTitle3 = "TC4063_C_" + partialDateTimeString + " - Internal";
            var _trainingCourseSetupId3 = commonMethodsDB.CreateTrainingRequirement(trainingTitle3, _teamId, staffTrainingItemId3, new DateTime(2022, 1, 1), null, 6, 1);

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now.AddDays(1), _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(3, trainingRecordIds.Count);

            #endregion

            #region Step 6

            commonMethodsDB.CreateTrainingRequirementSetup("TR4063_D_" + partialDateTimeString, staffTrainingItemId4, new DateTime(2022, 1, 1), DateTime.Now.AddDays(-1), true, 4, careProviderStaffRoleTypeIds);
            string trainingTitle4 = "TC4063_D_" + partialDateTimeString + " - Internal";
            commonMethodsDB.CreateTrainingRequirement(trainingTitle4, _teamId, staffTrainingItemId4, new DateTime(2022, 1, 1), null, 6, 1);

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(3, trainingRecordIds.Count);

            commonMethodsDB.CreateTrainingRequirementSetup("TR4063_E_" + partialDateTimeString, staffTrainingItemId5, new DateTime(2022, 1, 1), DateTime.Now.AddDays(1), true, 4, careProviderStaffRoleTypeIds);
            string trainingTitle5 = "TC4063_E_" + partialDateTimeString + " - Internal";
            var _trainingCourseSetupId5 = commonMethodsDB.CreateTrainingRequirement(trainingTitle5, _teamId, staffTrainingItemId5, new DateTime(2022, 1, 1), null, 6, 1);

            var EmploymentContractId4 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now.AddDays(2), _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(4, trainingRecordIds.Count);

            #endregion

            #region Step 7

            //Step 7 does not seem to be valid anymore as no prompt is displayed to the user when saving a Training Requirement Setup record

            #endregion

            #region Step 8

            var _systemUserTrainingId2 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId2, systemUserId)[0];
            dbHelper.systemUserTraining.UpdateSystemUserTraining(_systemUserTrainingId2, _trainingCourseSetupId2, DateTime.Now, DateTime.Now, 2);
            var _systemUserTrainingId3 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId3, systemUserId)[0];
            dbHelper.systemUserTraining.UpdateSystemUserTraining(_systemUserTrainingId3, _trainingCourseSetupId3, DateTime.Now, DateTime.Now, 2);

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now.AddDays(1), _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);
            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(6, trainingRecordIds.Count);

            var _systemUserTrainingId5 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId2, systemUserId)[0];
            var _systemUserTrainingId6 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId3, systemUserId)[0];

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateRecordCellText(_systemUserTrainingId5.ToString(), 6, "Outstanding")
                .ValidateRecordCellText(_systemUserTrainingId6.ToString(), 6, "Outstanding");

            #endregion

            #region Step 9

            var _systemUserTrainingId4 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId5, systemUserId)[0];
            dbHelper.systemUserTraining.UpdateSystemUserTraining(_systemUserTrainingId4, _trainingCourseSetupId5, DateTime.Now, DateTime.Now, 1);
            var _systemUserTrainingId1 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId)[0];

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);
            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(6, trainingRecordIds.Count);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateRecordCellText(_systemUserTrainingId1.ToString(), 6, "Outstanding")
                .ValidateRecordCellText(_systemUserTrainingId4.ToString(), 6, "Current");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4077")]
        [Description("Cascade Create New Training Items to Current Staff")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("BusinessModule2", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Training")]
        [TestProperty("Screen2", "System User Employment Contracts")]
        public void SystemUserTraining_ACC4063_UITestMethod002()
        {
            #region Step 1 to Step 3

            //Steps 1, 2, and 3 will be ignored for automation as its not possible to activate or deactivate business modules programmatically.

            #endregion

            #region Step 4

            #region System User Test User
            string partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");

            var userFirstName = "User4077";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemName = "STI4077_" + partialDateTimeString;
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName2 = "T4077B_" + partialDateTimeString;
            var staffTrainingItemId2 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName2, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName3 = "T4077C_" + partialDateTimeString;
            var staffTrainingItemId3 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName3, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName4 = "T4077D_" + partialDateTimeString;
            var staffTrainingItemId4 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName4, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName5 = "T4077E_" + partialDateTimeString;
            var staffTrainingItemId5 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName5, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Care provider staff role type
            Guid _defaultCareProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Default CPSTRT", null, null, new DateTime(2021, 1, 1), "Default CPSTRT ...");

            string roleName = "Role4077A_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, roleName, "", null, new DateTime(2020, 1, 1), null);

            string roleName2 = "Role4077B_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid2 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName2, "", null, new DateTime(2020, 1, 1), null);

            string roleName3 = "Role4077C_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid3 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName3, "", null, new DateTime(2020, 1, 1), null);

            string roleName4 = "Role4077D_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid4 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName4, "", null, new DateTime(2020, 1, 1), null);

            string roleName5 = "Role4077E_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid5 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName5, "", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Training Requirement Setup
            foreach (var trId in dbHelper.trainingRequirementSetup.GetByAllRoles(true))
                dbHelper.trainingRequirementSetup.UpdateAllRoles(trId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid1 };
            commonMethodsDB.CreateTrainingRequirementSetup("TR4077_" + partialDateTimeString, staffTrainingItemID, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "TC4077_" + partialDateTimeString + " - Internal";
            commonMethodsDB.CreateTrainingRequirement(trainingTitle, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Employment Contract Type
            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #region System User Employment Contract

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var EmploymentContractId1 = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, null, _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);
            List<Guid> trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId);

            #endregion

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(EmploymentContractId1.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            Assert.AreEqual(1, trainingRecordIds.Count);

            //careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid2 };
            commonMethodsDB.CreateTrainingRequirementSetup("TR4077_B_" + partialDateTimeString, staffTrainingItemId2, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle2 = "TC4077_B_" + partialDateTimeString + " - Internal";
            var _trainingCourseSetupId2 = commonMethodsDB.CreateTrainingRequirement(trainingTitle2, _teamId, staffTrainingItemId2, new DateTime(2022, 1, 1), null, 6, 1);

            #region Execute Job 'Create Missing Training Items'

            Guid CreateMissingRecruitmentDocumentsId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Create Missing Training Items")[0];
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(CreateMissingRecruitmentDocumentsId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(CreateMissingRecruitmentDocumentsId);
            System.Threading.Thread.Sleep(2000);
            #endregion

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            System.Threading.Thread.Sleep(2000);

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(2, trainingRecordIds.Count);
            #endregion

            #region Step 5

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid2 };
            commonMethodsDB.CreateTrainingRequirementSetup("TR4077_C_" + partialDateTimeString, staffTrainingItemId3, new DateTime(2022, 1, 1), DateTime.Now.AddDays(2), false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle3 = "TC4077_C_" + partialDateTimeString + " - Internal";
            var _trainingCourseSetupId3 = commonMethodsDB.CreateTrainingRequirement(trainingTitle3, _teamId, staffTrainingItemId3, new DateTime(2022, 1, 1), null, 6, 1);

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now.AddDays(1), _careProviderStaffRoleTypeid2, _teamId, _employmentContractTypeid, null);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(3, trainingRecordIds.Count);

            #endregion

            #region Step 6

            careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid3 };
            commonMethodsDB.CreateTrainingRequirementSetup("TR4077_D_" + partialDateTimeString, staffTrainingItemId4, new DateTime(2022, 1, 1), DateTime.Now.AddDays(-1), false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle4 = "TC4077_D_" + partialDateTimeString + " - Internal";
            commonMethodsDB.CreateTrainingRequirement(trainingTitle4, _teamId, staffTrainingItemId4, new DateTime(2022, 1, 1), null, 6, 1);

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid3, _teamId, _employmentContractTypeid, null);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(3, trainingRecordIds.Count);

            careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid4 };
            commonMethodsDB.CreateTrainingRequirementSetup("TR4077_E_" + partialDateTimeString, staffTrainingItemId5, new DateTime(2022, 1, 1), DateTime.Now.AddDays(1), false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle5 = "TC4077_E_" + partialDateTimeString + " - Internal";
            var _trainingCourseSetupId5 = commonMethodsDB.CreateTrainingRequirement(trainingTitle5, _teamId, staffTrainingItemId5, new DateTime(2022, 1, 1), null, 6, 1);

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now.AddDays(2), _careProviderStaffRoleTypeid4, _teamId, _employmentContractTypeid, null);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(4, trainingRecordIds.Count);

            #endregion

            #region Step 7

            //Step 7 does not seem to be valid anymore as no prompt is displayed to the user when saving a Training Requirement Setup record

            #endregion

            #region Step 8

            var _systemUserTrainingId2 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId2, systemUserId)[0];
            dbHelper.systemUserTraining.UpdateSystemUserTraining(_systemUserTrainingId2, _trainingCourseSetupId2, DateTime.Now, DateTime.Now, 2);
            var _systemUserTrainingId3 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId3, systemUserId)[0];
            dbHelper.systemUserTraining.UpdateSystemUserTraining(_systemUserTrainingId3, _trainingCourseSetupId3, DateTime.Now, DateTime.Now, 2);

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now.AddDays(1), _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now.AddDays(1), _careProviderStaffRoleTypeid2, _teamId, _employmentContractTypeid, null);
            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(6, trainingRecordIds.Count);

            var _systemUserTrainingId5 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId2, systemUserId)[0];
            var _systemUserTrainingId6 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId3, systemUserId)[0];

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateRecordCellText(_systemUserTrainingId5.ToString(), 6, "Outstanding")
                .ValidateRecordCellText(_systemUserTrainingId6.ToString(), 6, "Outstanding");

            #endregion

            #region Step 9

            var _systemUserTrainingId4 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId5, systemUserId)[0];
            dbHelper.systemUserTraining.UpdateSystemUserTraining(_systemUserTrainingId4, _trainingCourseSetupId5, DateTime.Now, DateTime.Now, 1);
            var _systemUserTrainingId1 = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId)[0];

            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid5, _teamId, _employmentContractTypeid, null);
            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(6, trainingRecordIds.Count);

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad()
                .ValidateRecordCellText(_systemUserTrainingId1.ToString(), 6, "Outstanding")
                .ValidateRecordCellText(_systemUserTrainingId4.ToString(), 6, "Current");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4078")]
        [Description("Cascade Create New Training Items to Current Staff")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Staff Training")]
        [TestProperty("BusinessModule2", "Care Worker Contracts")]
        [TestProperty("Screen1", "System User Training")]
        [TestProperty("Screen2", "System User Employment Contracts")]
        public void SystemUserTraining_ACC4063_UITestMethod003()
        {
            #region Step 31 - Step 32

            #region System User Test User

            string partialDateTimeString = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");

            var userFirstName = "User4078";
            var userLastName = commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("yyyyMMddHHmmss");
            var username = userFirstName + userLastName;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Staff Training Item

            var staffTrainingItemName = "T4078A_" + partialDateTimeString;
            var staffTrainingItemID = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            var staffTrainingItemName2 = "T4078B_" + partialDateTimeString;
            var staffTrainingItemId2 = commonMethodsDB.CreateStaffTrainingItem(_teamId, staffTrainingItemName2, commonMethodsHelper.GetCurrentDateWithoutCulture().Date);

            #endregion

            #region Care provider staff role type

            Guid _defaultCareProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Default CPSTRT", null, null, new DateTime(2021, 1, 1), "Default CPSTRT ...");

            string roleName = "Role4078A_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid1 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, roleName, "", null, new DateTime(2020, 1, 1), null);

            string roleName2 = "Role4078B_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid2 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName2, "", null, new DateTime(2020, 1, 1), null);

            string roleName3 = "Role4078C_" + partialDateTimeString;
            var _careProviderStaffRoleTypeid3 = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId, roleName3, "", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Training Requirement Setup

            foreach (var trId in dbHelper.trainingRequirementSetup.GetByAllRoles(true))
                dbHelper.trainingRequirementSetup.UpdateAllRoles(trId, false, new List<Guid> { _defaultCareProviderStaffRoleTypeid });

            List<Guid> careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid1, _careProviderStaffRoleTypeid2 };
            commonMethodsDB.CreateTrainingRequirementSetup("TR4078_" + partialDateTimeString, staffTrainingItemID, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle = "TC4078_" + partialDateTimeString + " - Internal";
            commonMethodsDB.CreateTrainingRequirement(trainingTitle, _teamId, staffTrainingItemID, new DateTime(2022, 1, 1), null, 6, 1);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("SystemUserTrainingUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(systemUserId.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad();

            #region System User Employment Contract

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var EmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);
            var EmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);
            var EmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid1, _teamId, _employmentContractTypeid, null);

            System.Threading.Thread.Sleep(2000);
            List<Guid> trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemID, systemUserId);

            #endregion

            systemUserEmploymentContractsPage
                .ClickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(EmploymentContractId1.ToString(), true)
                .ValidateRecordVisibility(EmploymentContractId2.ToString(), true)
                .ValidateRecordVisibility(EmploymentContractId3.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad();

            Assert.AreEqual(1, trainingRecordIds.Count);

            careProviderStaffRoleTypeIds = new List<Guid> { _careProviderStaffRoleTypeid2 };
            commonMethodsDB.CreateTrainingRequirementSetup("TR4077_B_" + partialDateTimeString, staffTrainingItemId2, new DateTime(2022, 1, 1), null, false, 4, careProviderStaffRoleTypeIds);
            string trainingTitle2 = "TC4077_B_" + partialDateTimeString + " - Internal";
            commonMethodsDB.CreateTrainingRequirement(trainingTitle2, _teamId, staffTrainingItemId2, new DateTime(2022, 1, 1), null, 6, 1);
            var EmploymentContractId4 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid2, _teamId, _employmentContractTypeid, null);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(EmploymentContractId4.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            System.Threading.Thread.Sleep(2000);

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(2, trainingRecordIds.Count);

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);
            var EmploymentContractId5 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUserId, DateTime.Now, _careProviderStaffRoleTypeid3, _teamId, _employmentContractTypeid, null);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ValidateRecordVisibility(EmploymentContractId5.ToString(), true);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .ClickSearchButton()
                .WaitForSystemUserTrainingPageToLoad();

            trainingRecordIds = dbHelper.systemUserTraining.GetSystemUserTrainingByRegardingUserId(systemUserId);
            Assert.AreEqual(2, trainingRecordIds.Count);

            #endregion
        }

        #endregion

    }

}
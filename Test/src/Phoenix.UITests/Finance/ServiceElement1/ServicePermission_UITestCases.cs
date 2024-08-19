using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceElement1
{
    [TestClass]
    public class ServicePermission_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private string _systemUsername;
        private Guid _ethnicityId;
        private string partialStringSuffix;

        #endregion

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

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region System User

                _systemUsername = "ServicePermissionUser1";
                commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServicePermission", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22774

        [TestProperty("JiraIssueID", "CDV6-22917")]
        [Description("Steps 1 to 8 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServicePermission_UITestMethod001()
        {
            #region Step 1

            var serviceElement1Name = "CDV6_22774_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            var validRateUnits = new List<Guid>();
            var rateUnit1Id = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault();
            var rateUnit2Id = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            var rateUnit3Id = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            validRateUnits.Add(rateUnit1Id);
            validRateUnits.Add(rateUnit2Id);
            validRateUnits.Add(rateUnit3Id);

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, _careDirectorQA_TeamId, startDate, code, 1, 1, validRateUnits, rateUnit3Id);

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            #endregion

            #region Step 2

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");
            #endregion

            #region Step 3

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .ValidateSelectedSystemView("Active Records")
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServicePermissionsPage();

            #endregion

            #region Step 4 and Step 5

            servicePermissionsPage
                .WaitForServicePermissionPageToLoad()
                .ClickNewRecordButton();

            servicePermissionRecordPage
                .WaitForServicePermissionRecordPageToLoad()
                .ValidateServiceElement1FieldVisible()
                .ValidateTeamFieldVisible()
                .ValidateResponsibleTeamFieldVisible();

            #endregion

            #region Step 6
            servicePermissionRecordPage
                .ClickSaveButton()
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateTeamFieldErrorNotificationMessageText("Please fill out this field.");

            servicePermissionRecordPage
                .ClickTeamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(_careDirectorQA_TeamId.ToString());

            servicePermissionRecordPage
                .WaitForServicePermissionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateServiceElement1FieldLinkText(serviceElement1Name)
                .ValidateResponsibleTeamFieldLinkText("CareDirector QA")
                .ValidateTeamFieldLinkText("CareDirector QA");
            #endregion

            #region Step 7
            servicePermissionRecordPage
                .ValidateRecordTitle("CareDirector QA \\ " + serviceElement1Name);
            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-22934")]
        [Description("Steps 9 to 12 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServicePermission_UITestMethod002()
        {

            #region Step 9

            #region Service Element 1 - Service Permission

            var serviceElement1Name = "CDV6_22934_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            var validRateUnits = new List<Guid>();
            var rateUnit1Id = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault();
            var rateUnit2Id = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            var rateUnit3Id = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            validRateUnits.Add(rateUnit1Id);
            validRateUnits.Add(rateUnit2Id);
            validRateUnits.Add(rateUnit3Id);

            partialStringSuffix = commonMethodsHelper.GetCurrentDateTimeString();

            var serviceElement1 = commonMethodsDB.CreateServiceElement1(serviceElement1Name, _careDirectorQA_TeamId, startDate, code, 1, 1, validRateUnits, rateUnit3Id);

            var TeamAId = commonMethodsDB.CreateTeam("Team A" + partialStringSuffix, null, _careDirectorQA_BusinessUnitId, "97076", "TeamA2@careworkstempmail.com", "Team A2", "020 123456");

            var servicePermission1 = dbHelper.servicePermission.CreateServicePermission(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, serviceElement1, TeamAId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServicePermissionsTab();

            servicePermissionsPage
                .WaitForServicePermissionPageToLoad()
                .ClickNewRecordButton();

            servicePermissionRecordPage
                .WaitForServicePermissionRecordPageToLoad()
                .ClickTeamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Team A" + partialStringSuffix)
                .TapSearchButton()
                .SelectResultElement(TeamAId.ToString());

            servicePermissionRecordPage
                .WaitForServicePermissionRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There is already an Active Service Permission record open using these settings. Please correct as necessary.")
                .TapCloseButton();

            #endregion

            #region Step 10
            servicePermissionRecordPage
                .WaitForServicePermissionRecordPageToLoad()
                .ClickTeamLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CareDirector QA")
                .TapSearchButton()
                .SelectResultElement(_careDirectorQA_TeamId.ToString());

            servicePermissionRecordPage
                .WaitForServicePermissionRecordPageToLoad()
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateRecordTitle("CareDirector QA \\ " + serviceElement1Name)
                .ValidateServiceElement1FieldDisabled(true)
                .ValidateTeamFieldDisabled(true)
                .ValidateResponsibleTeamFieldDisabled(true);

            #endregion

            #region Step 11
            servicePermissionRecordPage
                .WaitForServicePermissionRecordPageToLoad()
                .ClickBackButton();

            servicePermissionsPage
                .WaitForServicePermissionPageToLoad()
                .SelectServicePermissionRecord(servicePermission1.ToString())
                .ClickDeleteButton();

            alertPopup
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
               .TapOKButton()
               .WaitForAlertPopupToLoad()
               .ValidateAlertText("1 item(s) deleted.")
               .TapOKButton();

            System.Threading.Thread.Sleep(1500);

            servicePermissionsPage
                .WaitForServicePermissionPageToLoad()
                .ClickRefreshButton()
                .InsertSearchQuery("Team A" + partialStringSuffix)
                .TapSearchButton()
                .ValidateRecordIsVisible(servicePermission1.ToString(), false)
                .ValidateNoRecordsMessageVisible();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22775

        [TestProperty("JiraIssueID", "CDV6-22912")]
        [Description("Steps 13 to 14 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServicePermission_UITestMethod003()
        {
            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Permissions")
                .SelectSavedView("Active Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Team")
                .ResultsPageValidateHeaderCellText(3, "Service Element 1");

            #endregion

            #region Step 14

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Service Permissions")
                .SelectSavedView("Inactive Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Team")
                .ResultsPageValidateHeaderCellText(3, "Service Element 1");

            #endregion

        }

        [TestProperty("JiraIssueID", "CDV6-22935")]
        [Description("Steps 15 to 18 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void ServicePermission_UITestMethod004()
        {
            #region Service Element 1

            var _serviceElement1Name = "CDV6_22935_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);

            var validRateUnits = new List<Guid>();
            var rateUnit1Id = dbHelper.rateUnit.GetByName("Per 1 Hour \\ Hours (Whole)").FirstOrDefault();
            var rateUnit2Id = dbHelper.rateUnit.GetByName("Per 1 Hour Unit \\ Units (Parts)").FirstOrDefault();
            var rateUnit3Id = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();
            validRateUnits.Add(rateUnit1Id);
            validRateUnits.Add(rateUnit2Id);
            validRateUnits.Add(rateUnit3Id);

            var _serviceElement1Id = commonMethodsDB.CreateServiceElement1(_serviceElement1Name, _careDirectorQA_TeamId, startDate, code, 1, 1, validRateUnits, rateUnit3Id);

            #endregion

            #region Team

            var B_TeamId = commonMethodsDB.CreateTeam("Team B", null, _careDirectorQA_BusinessUnitId, "229352", "TeamB@careworkstempmail.com", "Team B", "020 123456");
            var A_TeamId = commonMethodsDB.CreateTeam("Team A", null, _careDirectorQA_BusinessUnitId, "229351", "TeamA@careworkstempmail.com", "Team A", "020 123456");

            #endregion

            #region Service Permission

            dbHelper.servicePermission.CreateServicePermission(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _serviceElement1Id, B_TeamId);
            dbHelper.servicePermission.CreateServicePermission(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _serviceElement1Id, A_TeamId);

            #endregion

            #region Step 15

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .InsertSearchQuery(_serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(_serviceElement1Id.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToServicePermissionsTab();

            servicePermissionsPage
                .WaitForServicePermissionPageToLoad()
                .ValidateSelectedSystemView("Related Active Records")
                .ValidateTeamHeaderCellSortOrdedAscending()
                .ValidateServiceElement1HeaderCellSortOrdedAscending()
                .ValidateRecordCellContent(1, "Team A")
                .ValidateRecordCellContent(2, "Team B");

            #endregion

            #region Step 17

            servicePermissionsPage
                .InsertSearchQuery("Team")
                .TapSearchButton()
                .ValidateSelectedSystemView("Search Results")
                .ValidateTeamHeaderCellSortOrdedAscending()
                .ValidateServiceElement1HeaderCellSortOrdedAscending()
                .ValidateRecordCellContent(1, "Team A")
                .ValidateRecordCellContent(2, "Team B");

            #endregion

        }

        #endregion

    }
}

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
    public class BandRateTypes_UITestCases : FunctionalTest
    {
        #region properties

        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private string _systemUserName;
        private string _currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestMethod_Setup()
        {
            #region Internal

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
            var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
            TimeZone localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

            var fields = dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "FirstName", "LastName");

            #endregion

            #region Language

            _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Business Unit

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("Band Rate Schedule BU");

            #endregion

            #region Team

            _teamId = commonMethodsDB.CreateTeam("Band Rate Schedule T1", null, _businessUnitId, "907678", "BandRateScheduleT1@careworkstempmail.com", "Band Rate Schedule Testing", "020 123456");

            #endregion

            #region System User

            _systemUserName = "BRS_User1";
            commonMethodsDB.CreateSystemUserRecord(_systemUserName, "BandRateSchedule", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-7735

        [TestProperty("JiraIssueID", "ACC-7819")]
        [Description("Step(s) 1 to 2 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Band Rate Schedules")]
        public void BandRateSchedules_UITestMethod001()
        {
            #region Band Rate Type

            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_teamId, "BRT ACC-887 A", "991000", new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            var bandRateSchedule1Id = commonMethodsDB.CreateCareProviderBandRateSchedule(_teamId, "Level 1", bandRateTypeId, new TimeSpan(00, 01, 00), new TimeSpan(12, 00, 00));
            var bandRateSchedule2Id = commonMethodsDB.CreateCareProviderBandRateSchedule(_teamId, "Level 2", bandRateTypeId, new TimeSpan(12, 01, 00), new TimeSpan(23, 59, 00));

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Band Rate Types")
                .ClickReferenceDataMainHeader("Person Contracts")
                .ClickReferenceDataElement("Band Rate Types");

            bandRateTypesPage
                .WaitForPageToLoad()
                .InsertSearchQuery("BRT ACC-887 A")
                .TapSearchButton()
                .OpenRecord(bandRateTypeId);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("careproviderbandratetype").ClickOnExpandIcon();

            bandRateTypeRecordPage
                .WaitForPageToLoad()
                .ClickBandRateSchedulesTab();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .OpenRecord(bandRateSchedule1Id);

            bandRateScheduleRecordPage
                .WaitForPageToLoad()
                .ValidateNameText("Level 1")
                .ValidateVisitLengthFromText("00:01")
                .ValidateResponsibleTeamLinkText("Band Rate Schedule T1")
                .ValidateBandRateTypeLinkText("BRT ACC-887 A")
                .ValidateVisitLengthToText("12:00");

            #endregion

            #region Step 2

            bandRateScheduleRecordPage
                .ClickBackButton();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .InsertSearchQuery("Level 1")
                .TapSearchButton()
                .WaitForPageToLoad()
                .ValidateRecordPresent(bandRateSchedule1Id, true)
                .ValidateRecordPresent(bandRateSchedule2Id, false);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Band Rate Schedules")
                .SelectFilter("1", "Band Rate Type")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BRT ACC-887 A", bandRateTypeId);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()

                .ClickAddRuleButton(1)
                .SelectFilter("2", "Name")
                .SelectOperator("2", "Equals")
                .InsertRuleValueText("2", "Level 2")

                .ClickSearchButton()

                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordNotPresent(bandRateSchedule1Id.ToString())
                .ValidateSearchResultRecordPresent(bandRateSchedule2Id.ToString());



            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7820")]
        [Description("Step(s) 3 & 4 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Band Rate Schedules")]
        public void BandRateSchedules_UITestMethod002()
        {
            #region Band Rate Type

            var code = dbHelper.careProviderBandRateType.GetHighestCode();
            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_teamId, "BRT " + _currentTimeString, code.ToString(), new DateTime(2020, 1, 1));

            #endregion

            #region Step 3 & 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Band Rate Types")
                .ClickReferenceDataMainHeader("Person Contracts")
                .ClickReferenceDataElement("Band Rate Types");

            bandRateTypesPage
                .WaitForPageToLoad()
                .InsertSearchQuery("BRT " + _currentTimeString)
                .TapSearchButton()
                .OpenRecord(bandRateTypeId);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("careproviderbandratetype").ClickOnExpandIcon();

            bandRateTypeRecordPage
                .WaitForPageToLoad()
                .ClickBandRateSchedulesTab();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            //Create new record
            bandRateScheduleRecordPage
                .WaitForPageToLoad()
                .InsertTextOnVisitLengthFrom("00:00")
                .InsertTextOnVisitLengthTo("12:00")
                .ClickSaveAndCloseButton();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .ClickRefreshButton();

            var bandRateSchedules = dbHelper.careProviderBandRateSchedule.GetByBandRateTypeId(bandRateTypeId);
            Assert.AreEqual(1, bandRateSchedules.Count);
            var bandRateScheduleId = bandRateSchedules[0];

            bandRateSchedulesPage
                .WaitForPageToLoad();

            //Validate grid view page
            bandRateSchedulesPage
                .ValidateHeaderCellSortIcon(3, true)
                .ValidateHeaderCellSortIcon(4, true)
                .ValidateRecordCellContent(bandRateScheduleId, 2, "Level 1")
                .ValidateRecordCellContent(bandRateScheduleId, 3, "BRT " + _currentTimeString)
                .ValidateRecordCellContent(bandRateScheduleId, 4, "00:00")
                .ValidateRecordCellContent(bandRateScheduleId, 5, "12:00")
                .ValidateRecordCellContent(bandRateScheduleId, 6, "BandRateSchedule User1");


            bandRateSchedulesPage
            .OpenRecord(bandRateScheduleId);

            //Update Record
            bandRateScheduleRecordPage
                .WaitForPageToLoad()
                .ValidateNameText("Level 1")
                .ValidateVisitLengthFromText("00:00")
                .ValidateResponsibleTeamLinkText("Band Rate Schedule T1")
                .ValidateBandRateTypeLinkText("BRT " + _currentTimeString)
                .ValidateVisitLengthToText("12:00")

                .InsertTextOnName(" Level 1 Updated")
                .InsertTextOnVisitLengthFrom("01:30")
                .InsertTextOnVisitLengthTo("13:30")
                .ClickSaveAndCloseButton();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .OpenRecord(bandRateScheduleId);

            bandRateScheduleRecordPage
                .WaitForPageToLoad()
                .ValidateNameText("Level 1 Updated")
                .ValidateVisitLengthFromText("01:30")
                .ValidateResponsibleTeamLinkText("Band Rate Schedule T1")
                .ValidateBandRateTypeLinkText("BRT " + _currentTimeString)
                .ValidateVisitLengthToText("13:30");

            //Delete Record
            bandRateScheduleRecordPage
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .WaitForPageToLoad()
                .ValidateRecordPresent(bandRateScheduleId, false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7821")]
        [Description("Step(s) 5 to 10 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Person Contracts")]
        [TestProperty("Screen1", "Band Rate Schedules")]
        public void BandRateSchedules_UITestMethod003()
        {
            #region Band Rate Type

            var code = dbHelper.careProviderBandRateType.GetHighestCode();
            var bandRateTypeId = commonMethodsDB.CreateCareProviderBandRateType(_teamId, "BRT " + _currentTimeString, code.ToString(), new DateTime(2020, 1, 1));

            #endregion

            #region Band Rate Schedule

            var bandRateSchedule1Id = commonMethodsDB.CreateCareProviderBandRateSchedule(_teamId, "Level 1", bandRateTypeId, new TimeSpan(00, 01, 00), new TimeSpan(08, 00, 00));
            var bandRateSchedule2Id = commonMethodsDB.CreateCareProviderBandRateSchedule(_teamId, "Level 2", bandRateTypeId, new TimeSpan(08, 01, 00), new TimeSpan(12, 00, 00));

            #endregion

            #region Step 5 and 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Band Rate Types")
                .ClickReferenceDataMainHeader("Person Contracts")
                .ClickReferenceDataElement("Band Rate Types");

            bandRateTypesPage
                .WaitForPageToLoad()
                .InsertSearchQuery("BRT " + _currentTimeString)
                .TapSearchButton()
                .OpenRecord(bandRateTypeId);

            drawerDialogPopup.WaitForDrawerDialogPopupToLoad("careproviderbandratetype").ClickOnExpandIcon();

            bandRateTypeRecordPage
                .WaitForPageToLoad()
                .ClickBandRateSchedulesTab();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            bandRateScheduleRecordPage
                .WaitForPageToLoad()
                .ValidateNameText("Level 3")
                .ValidateVisitLengthFromText("12:01")

                .InsertTextOnName("Level 2")
                .InsertTextOnVisitLengthTo("12:01")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("It is NOT permitted to save a record that has the same name. Please correct as necessary.").TapCloseButton();

            bandRateScheduleRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("Level 3")
                .ClickSaveAndCloseButton();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 7

            var newCareProviderBandRateSchedules = dbHelper.careProviderBandRateSchedule.GetByBandRateTypeId(bandRateTypeId).Where(c => c != bandRateSchedule1Id && c != bandRateSchedule2Id).ToList();
            Assert.AreEqual(1, newCareProviderBandRateSchedules.Count);
            var bandRateSchedule3Id = newCareProviderBandRateSchedules[0];

            bandRateSchedulesPage
                .OpenRecord(bandRateSchedule3Id);

            bandRateScheduleRecordPage
                .WaitForPageToLoad()
                .InsertTextOnVisitLengthFrom("11:00")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("It is NOT permitted to save a record where it would mean there is a duplicate time duration. Please correct as necessary.").TapCloseButton();

            bandRateScheduleRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 8

            bandRateScheduleRecordPage
                .InsertTextOnVisitLengthFrom("12:02")
                .InsertTextOnVisitLengthTo("13:00")
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("It is NOT permitted to edit a record where it would mean there is a time duration that is not covered. If a change is required, please delete the records with higher Visit Length From values first.").TapCloseButton();

            bandRateScheduleRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 9

            bandRateScheduleRecordPage
                .InsertTextOnVisitLengthFrom("12:01")
                .InsertTextOnVisitLengthTo("13:00")
                .ClickSaveAndCloseButton();

            bandRateSchedulesPage
                .WaitForPageToLoad()
                .ClickRefreshButton()
                .OpenRecord(bandRateSchedule2Id);

            bandRateScheduleRecordPage
                .WaitForPageToLoad()
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("It is NOT permitted to delete a record where it would mean there is a time duration that is not covered. If a change is required, please delete the records with higher Visit Length From values first.").TapCloseButton();

            bandRateScheduleRecordPage
                .WaitForPageToLoad();

            #endregion

            #region Step 10

            bandRateScheduleRecordPage
                .ValidateVisitLengthFromFieldTooltip("Enter the start minute (relates to the visit length, not the start time of the visit) that the rate will apply from")
                .ValidateVisitLengthToFieldTooltip("Enter the end minute that the rate will apply to");

            #endregion

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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration
{
    [TestClass]
    public class CareProviderStaffRoleTypeGroups_UITestCases : FunctionalTest
    {
        #region properties

        private Guid _businessUnitId;
        private Guid _teamId;
        private string _teamName;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private string _systemUserName;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

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

            commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

            #endregion

            #region Language

            _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Business Unit

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

            #endregion

            #region Team

            _teamName = "CareDirector QA";
            _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

            #endregion

            #region System User

            _systemUserName = "CareWorkerContractsUser1";
            commonMethodsDB.CreateSystemUserRecord(_systemUserName, "CareWorkerContracts", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2787

        [TestProperty("JiraIssueID", "ACC-2807")]
        [Description("Step(s) 1 to 6 from the original test method ACC-870")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Employment Contracts")]
        [TestProperty("Screen1", "Care Provider Staff Role Type Groups")]
        [TestMethod]
        public void CareProviderStaffRoleTypeGroups_UITestMethod01()
        {
            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Care Provider Staff Role Type Groups")
                .ClickReferenceDataMainHeader("Care Worker Contracts")
                .ClickReferenceDataElement("Care Provider Staff Role Type Groups");

            careProviderStaffRoleTypeGroupsPage
                .WaitForCareProviderStaffRoleTypeGroupsPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderstaffroletypegroup")
                .ClickOnExpandIcon();

            careProviderStaffRoleTypeGroupRecordPage
                .WaitForCareProviderStaffRoleTypeGroupRecordPageToLoad()
                .ValidateAllFieldsVisible()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateStartDateText(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 3 & 4

            var careProviderStaffRoleTypeGroupName = "Group_" + _currentDateString;

            careProviderStaffRoleTypeGroupRecordPage
                .InsertName(careProviderStaffRoleTypeGroupName)
                .InsertCode("123")
                .InsertGovCode("123")
                .InsertDescription(_currentDateString)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            careProviderStaffRoleTypeGroupName = "Record_" + _currentDateString;

            careProviderStaffRoleTypeGroupRecordPage
                .ValidateResponsibleTeamLookupButtondDisabled(true)
                .InsertName(careProviderStaffRoleTypeGroupName)
                .InsertCode("111")
                .InsertGovCode("111")
                .InsertDescription("Automation Script")
                .ClickValidForExport_Option(true)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            #endregion

            #region Step 6

            careProviderStaffRoleTypeGroupRecordPage
                .ValidateCareProviderStaffRoleTypeGroupRecordTitle(careProviderStaffRoleTypeGroupName)
                .ClickInactive_Option(true)
                .ClickSaveButton()
                .ValidateActivateButtonVisible();

            careProviderStaffRoleTypeGroupRecordPage
                .ValidateNameFieldIsDisabledOrNot(true)
                .ValidateCodeFieldIsDisabledOrNot(true)
                .ValidateGovCodeFieldIsDisabledOrNot(true)
                .ValidateDescriptionFieldIsDisabledOrNot(true)
                .ValidateStartDateFieldIsDisabledOrNot(true)
                .ValidateEndDateFieldIsDisabledOrNot(true)
                .ValidateInactiveOptionsIsDisabledOrNot(true)
                .ValidateValidForExportOptionsIsDisabledOrNot(true)
                .ValidateResponsibleTeamLookupButtondDisabled(true)
                .ClickActivateButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
                .TapOKButton();

            careProviderStaffRoleTypeGroupRecordPage
                .WaitForCareProviderStaffRoleTypeGroupRecordPageToLoad()
                .ValidateNameFieldIsDisabledOrNot(false)
                .ValidateCodeFieldIsDisabledOrNot(false)
                .ValidateGovCodeFieldIsDisabledOrNot(false)
                .ValidateDescriptionFieldIsDisabledOrNot(false)
                .ValidateStartDateFieldIsDisabledOrNot(false)
                .ValidateEndDateFieldIsDisabledOrNot(false)
                .ValidateInactiveOptionsIsDisabledOrNot(false)
                .ValidateValidForExportOptionsIsDisabledOrNot(false)
                .ValidateResponsibleTeamLookupButtondDisabled(true)
                .ClickBackButton();

            #endregion

            #region Step 5

            Guid careProviderStaffRoleTypeGroupId = dbHelper.careProviderStaffRoleTypeGroup.GetByName(careProviderStaffRoleTypeGroupName).FirstOrDefault();

            careProviderStaffRoleTypeGroupsPage
                .WaitForCareProviderStaffRoleTypeGroupsPageToLoad()
                .InsertSearchQuery(careProviderStaffRoleTypeGroupName)
                .TapSearchButton()
                .SelectCareProviderStaffRoleTypeGroupRecord(careProviderStaffRoleTypeGroupId.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            var careProviderStaffRoleTypeGroupCount = dbHelper.careProviderStaffRoleTypeGroup.GetByName(careProviderStaffRoleTypeGroupName);
            Assert.AreEqual(0, careProviderStaffRoleTypeGroupCount.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2808")]
        [Description("Step(s) 7 to 10 from the original test method ACC-870")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Employment Contracts")]
        [TestProperty("Screen1", "Care Provider Staff Role Type Groups")]
        public void CareProviderStaffRoleTypeGroups_UITestMethod02()
        {
            var careProviderStaffRoleTypeGroupName = "Group_" + _currentDateString;
            var careProviderStaffRoleTypeGroupName2 = "Group_2_" + _currentDateString;
            Guid careProviderStaffRoleTypeGroupId = dbHelper.careProviderStaffRoleTypeGroup.CreateCareProviderStaffRoleTypeGroup(_teamId, careProviderStaffRoleTypeGroupName, "2808", "ACC_2808", DateTime.Now.Date, "");

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Care Provider Staff Role Type Groups")
                .ClickReferenceDataMainHeader("Care Worker Contracts")
                .ClickReferenceDataElement("Care Provider Staff Role Type Groups");

            careProviderStaffRoleTypeGroupsPage
                .WaitForCareProviderStaffRoleTypeGroupsPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderstaffroletypegroup")
                .ClickOnExpandIcon();

            careProviderStaffRoleTypeGroupRecordPage
                .WaitForCareProviderStaffRoleTypeGroupRecordPageToLoad()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ClickSaveButton()
                .ValidateNotificationAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ClickBackButton();

            #endregion

            #region step 9

            careProviderStaffRoleTypeGroupsPage
                .WaitForCareProviderStaffRoleTypeGroupsPageToLoad()
                .ValidateHeaderCellText(2, "Name")
                .ValidateGridHeaderCellSortAscendingOrder(2, "Name")
                .ValidateHeaderCellText(3, "Code")
                .ValidateHeaderCellText(4, "Gov Code")
                .ValidateHeaderCellText(5, "Start Date")
                .ValidateHeaderCellText(6, "End Date")
                .ValidateHeaderCellText(7, "Valid For Export")
                .ValidateHeaderCellText(8, "Created By")
                .ValidateHeaderCellText(9, "Created On")
                .ValidateHeaderCellText(10, "Modified By")
                .ValidateHeaderCellText(11, "Modified On");

            #endregion

            #region Step 10

            careProviderStaffRoleTypeGroupsPage
                .InsertSearchQuery(careProviderStaffRoleTypeGroupName)
                .TapSearchButton()
                .ValidateRecordPresentOrNot(careProviderStaffRoleTypeGroupId.ToString(), true)

                .InsertSearchQuery("2808")
                .TapSearchButton()
                .ValidateRecordPresentOrNot(careProviderStaffRoleTypeGroupId.ToString(), true)

                .InsertSearchQuery("ACC_2808")
                .TapSearchButton()
                .ValidateRecordPresentOrNot(careProviderStaffRoleTypeGroupId.ToString(), true)
                .OpenRecord(careProviderStaffRoleTypeGroupId.ToString());

            #endregion

            #region Step 8

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderstaffroletypegroup")
                .ClickOnExpandIcon();

            careProviderStaffRoleTypeGroupRecordPage
                .WaitForCareProviderStaffRoleTypeGroupRecordPageToLoad()
                .InsertName(careProviderStaffRoleTypeGroupName2)
                .InsertDescription("Automation")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .NavigateToAuditSubPage();

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("careproviderstaffroletypegroup", "iframe_careproviderstaffroletypegroup")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(2, 2, "Created");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2809")]
        [Description("Step(s) 12 to 17 & 23 from the original test method ACC-870")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule", "Employment Contracts")]
        [TestProperty("Screen1", "Care Provider Staff Role Types")]
        public void CareProviderStaffRoleTypeGroups_UITestMethod03()
        {
            #region Step 12 & 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Care Provider Staff Role Types")
                .ClickReferenceDataMainHeader("Care Worker Contracts")
                .ClickReferenceDataElement("Care Provider Staff Role Types");

            careProviderStaffRoleTypesPage
                .WaitForCareProviderStaffRoleTypesPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderstaffroletype")
                .ClickOnExpandIcon();

            careProviderStaffRoleTypeRecordPage
                .WaitForCareProviderStaffRoleTypeRecordPageToLoad()
                .ValidateAllFieldsVisible()
                .ValidateResponsibleTeamLinkText(_teamName)
                .ValidateStartDateText(DateTime.Now.Date.ToString("dd'/'MM'/'yyyy"))
                .ValidateInactive_OptionIsCheckedOrNot(false)
                .ValidateValidForExport_OptionIsCheckedOrNot(false)
                .ValidateDeliversCare_OptionIsCheckedOrNot(true);

            #endregion

            #region Step 14 & 23

            Guid careProviderStaffRoleTypeGroupId1 = commonMethodsDB.CreateCareProviderStaffRoleTypeGroup(_teamId, "Group Record First", "28091", "ACC_2809_1", new DateTime(2020, 1, 1), "");
            Guid careProviderStaffRoleTypeGroupId2 = commonMethodsDB.CreateCareProviderStaffRoleTypeGroup(_teamId, "Group Record Second", "28092", "ACC_2809_2", new DateTime(2020, 1, 1), "");
            Guid careProviderStaffRoleTypeGroupId3 = commonMethodsDB.CreateCareProviderStaffRoleTypeGroup(_teamId, "Group Record Third", "28093", "ACC_2809_3", new DateTime(2020, 1, 1), "", true);

            careProviderStaffRoleTypeRecordPage
                .ClickRoleTypeGroupsLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .ValidateMultiSelectLookupSection()
                .SelectElementAndClickAddRecordsButton("Group Record First", careProviderStaffRoleTypeGroupId1.ToString())
                .SelectElementAndClickAddRecordsButton("Group Record Second", careProviderStaffRoleTypeGroupId2.ToString())
                .SearchAndValidateRecordPresentOrNot("Group Record Third", careProviderStaffRoleTypeGroupId3, false)
                .ClickOkButton();

            careProviderStaffRoleTypeRecordPage
                .WaitForCareProviderStaffRoleTypeRecordPageToLoad()
                .ValidateRoleTypeGroupsMultiSelectElementText(careProviderStaffRoleTypeGroupId1, "Group Record First")
                .ValidateRoleTypeGroupsMultiSelectElementText(careProviderStaffRoleTypeGroupId2, "Group Record Second");

            #endregion

            #region Step 15

            var careProviderStaffRoleTypeName1 = "Record_1_" + _currentDateString;
            var careProviderStaffRoleTypeName2 = "Record_2_" + _currentDateString;

            careProviderStaffRoleTypeRecordPage
                .InsertName(careProviderStaffRoleTypeName1)
                .InsertCode("123")
                .InsertGovCode("123")
                .InsertDescription("Automation")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            careProviderStaffRoleTypeRecordPage
                .ValidateResponsibleTeamLookupButtonIsDisabled(true)
                .ValidateNameText(careProviderStaffRoleTypeName1)
                .ValidateCodeText("123")
                .ValidateGovCodeText("123")
                .ValidateDescriptionText("Automation")

                .InsertName(careProviderStaffRoleTypeName2)
                .InsertCode("456")
                .InsertGovCode("456")
                .InsertDescription("Automation Record")
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            careProviderStaffRoleTypeRecordPage
                .ValidateNameText(careProviderStaffRoleTypeName2)
                .ValidateCodeText("456")
                .ValidateGovCodeText("456")
                .ValidateDescriptionText("Automation Record");

            #endregion

            #region Step 17

            careProviderStaffRoleTypeRecordPage
                .ValidateCareProviderStaffRoleTypeRecordTitle(careProviderStaffRoleTypeName2)
                .ClickInactive_Option(true)
                .ClickSaveButton()
                .ValidateActivateButtonVisible();

            careProviderStaffRoleTypeRecordPage
                .ValidateNameFieldIsDisabledOrNot(true)
                .ValidateCodeFieldIsDisabledOrNot(true)
                .ValidateGovCodeFieldIsDisabledOrNot(true)
                .ValidateStartDateFieldIsDisabledOrNot(true)
                .ValidateEndDateFieldIsDisabledOrNot(true)
                .ValidateInactiveOptionsIsDisabledOrNot(true)
                .ValidateValidForExportOptionsIsDisabledOrNot(true)
                .ValidateDeliversCareOptionsIsDisabledOrNot(true)
                .ValidateDescriptionFieldIsDisabledOrNot(true)
                .ValidateRoleTypeGroupsLookupButtonIsDisabledOrNot(true)
                .ValidateDefaultBookingTypeLookupButtonIsDisabledOrNot(true)
                .ClickActivateButton();


            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.")
                .TapOKButton();

            careProviderStaffRoleTypeRecordPage
                .WaitForCareProviderStaffRoleTypeRecordPageToLoad()
                .ValidateNameFieldIsDisabledOrNot(false)
                .ValidateCodeFieldIsDisabledOrNot(false)
                .ValidateGovCodeFieldIsDisabledOrNot(false)
                .ValidateStartDateFieldIsDisabledOrNot(false)
                .ValidateEndDateFieldIsDisabledOrNot(false)
                .ValidateInactiveOptionsIsDisabledOrNot(false)
                .ValidateValidForExportOptionsIsDisabledOrNot(false)
                .ValidateDeliversCareOptionsIsDisabledOrNot(false)
                .ValidateDescriptionFieldIsDisabledOrNot(false)
                .ValidateRoleTypeGroupsLookupButtonIsDisabledOrNot(false)
                .ValidateDefaultBookingTypeLookupButtonIsDisabledOrNot(false)
                .ClickBackButton();

            #endregion

            #region Step 16

            Guid careProviderStaffRoleTypeId = dbHelper.careProviderStaffRoleType.GetByName(careProviderStaffRoleTypeName2).FirstOrDefault();

            careProviderStaffRoleTypesPage
                .WaitForCareProviderStaffRoleTypesPageToLoad()
                .InsertSearchQuery(careProviderStaffRoleTypeName2)
                .TapSearchButton()
                .SelectCareProviderStaffRoleTypesRecord(careProviderStaffRoleTypeId.ToString())
                .ClickDeleteRecordButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.")
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            var careProviderStaffRoleTypeCount = dbHelper.careProviderStaffRoleType.GetByName(careProviderStaffRoleTypeName2);
            Assert.AreEqual(0, careProviderStaffRoleTypeCount.Count);


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2810")]
        [Description("Step(s) 18 to 22 from the original test method ACC-870")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Employment Contracts")]
        [TestProperty("Screen1", "Care Provider Staff Role Types")]
        public void CareProviderStaffRoleTypeGroups_UITestMethod04()
        {
            #region Care Provider Staff Role Types

            var careProviderStaffRoleTypeName1 = "ACC_2810_1_Record";
            var careProviderStaffRoleTypeName2 = "ACC_2810_2_Record" + _currentDateString;
            Guid careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, careProviderStaffRoleTypeName1, "2810", "ACC_2810", new DateTime(2020, 1, 1), "Automation");
            Guid careProviderStaffRoleTypeId_Inactive = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, careProviderStaffRoleTypeName1 + "_Inactive", "", "", new DateTime(2020, 1, 1), "Automation");
            dbHelper.careProviderStaffRoleType.UpdateInactive(careProviderStaffRoleTypeId_Inactive, true);
            Guid careProviderStaffRoleTypeId_Delivers_No = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, careProviderStaffRoleTypeName1 + "_Deliver_No", "", "", new DateTime(2020, 1, 1), "Automation");
            dbHelper.careProviderStaffRoleType.UpdateDeliversCare(careProviderStaffRoleTypeId_Delivers_No, false);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Care Provider Staff Role Types")
                .ClickReferenceDataMainHeader("Care Worker Contracts")
                .ClickReferenceDataElement("Care Provider Staff Role Types");

            careProviderStaffRoleTypesPage
                .WaitForCareProviderStaffRoleTypesPageToLoad()
                .ValidateSelectedSystemView("Active Records")
                .ValidateHeaderCellText(2, "Name")
                .ValidateGridHeaderCellSortAscendingOrder(2, "Name")
                .ValidateHeaderCellText(3, "Delivers Care?")
                .ValidateHeaderCellText(4, "Code")
                .ValidateHeaderCellText(5, "Gov Code")
                .ValidateHeaderCellText(6, "Start Date")
                .ValidateHeaderCellText(7, "End Date")
                .ValidateHeaderCellText(8, "Valid For Export")
                .ValidateHeaderCellText(9, "Created By")
                .ValidateHeaderCellText(10, "Created On")
                .ValidateHeaderCellText(11, "Modified By")
                .ValidateHeaderCellText(12, "Modified On")

                .SelectSystemView("Inactive Records")
                .ValidateSelectedSystemView("Inactive Records")
                .ValidateHeaderCellText(2, "Name")
                .ValidateGridHeaderCellSortAscendingOrder(2, "Name")
                .ValidateHeaderCellText(3, "Delivers Care?")
                .ValidateHeaderCellText(4, "Code")
                .ValidateHeaderCellText(5, "Gov Code")
                .ValidateHeaderCellText(6, "Start Date")
                .ValidateHeaderCellText(7, "End Date")
                .ValidateHeaderCellText(8, "Valid For Export")
                .ValidateHeaderCellText(9, "Created By")
                .ValidateHeaderCellText(10, "Created On")
                .ValidateHeaderCellText(11, "Modified By")
                .ValidateHeaderCellText(12, "Modified On");

            #endregion

            #region Step 20

            careProviderStaffRoleTypesPage
                .SelectSystemView("Delivers Care (Active)")
                .ValidateSelectedSystemView("Delivers Care (Active)")
                .ValidateHeaderCellText(2, "Name")
                .ValidateHeaderCellText(3, "Code")
                .ValidateHeaderCellText(4, "Gov Code")
                .ValidateHeaderCellText(5, "Start Date")
                .ValidateGridHeaderCellSortDescendingOrder(5, "Start Date")
                .ValidateHeaderCellText(6, "End Date")
                .ValidateHeaderCellText(7, "Valid For Export")
                .ValidateHeaderCellText(8, "Created By")
                .ValidateHeaderCellText(9, "Created On")
                .ValidateHeaderCellText(10, "Modified By")
                .ValidateHeaderCellText(11, "Modified On")

                .SelectSystemView("Does not Deliver Care (Active)")
                .ValidateSelectedSystemView("Does not Deliver Care (Active)")
                .ValidateRecordPresentOrNot(careProviderStaffRoleTypeId_Delivers_No.ToString(), true)
                .ValidateHeaderCellText(2, "Name")
                .ValidateHeaderCellText(3, "Code")
                .ValidateHeaderCellText(4, "Gov Code")
                .ValidateHeaderCellText(5, "Start Date")
                .ValidateGridHeaderCellSortDescendingOrder(5, "Start Date")
                .ValidateHeaderCellText(6, "End Date")
                .ValidateHeaderCellText(7, "Valid For Export")
                .ValidateHeaderCellText(8, "Created By")
                .ValidateHeaderCellText(9, "Created On")
                .ValidateHeaderCellText(10, "Modified By")
                .ValidateHeaderCellText(11, "Modified On");

            #endregion

            #region Step 21

            careProviderStaffRoleTypesPage
                .InsertSearchQuery(careProviderStaffRoleTypeName1)
                .TapSearchButton()
                .ValidateRecordPresentOrNot(careProviderStaffRoleTypeId.ToString(), true)

                .InsertSearchQuery("2810")
                .TapSearchButton()
                .ValidateRecordPresentOrNot(careProviderStaffRoleTypeId.ToString(), true)

                .InsertSearchQuery("ACC_2810")
                .TapSearchButton()
                .ValidateRecordPresentOrNot(careProviderStaffRoleTypeId.ToString(), true)
                .OpenRecord(careProviderStaffRoleTypeId.ToString());

            #endregion

            #region Step 18

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("careproviderstaffroletype")
                .ClickOnExpandIcon();

            careProviderStaffRoleTypeRecordPage
                .WaitForCareProviderStaffRoleTypeRecordPageToLoad()
                .InsertName(careProviderStaffRoleTypeName2)
                .InsertCode("28101")
                .InsertGovCode("ACC_28101")
                .InsertDescription("Automation Record")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .NavigateToAuditSubPage();

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("careproviderstaffroletype", "iframe_careproviderstaffroletype")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(2, 2, "Created");

            dbHelper.careProviderStaffRoleType.DeleteCareProviderStaffRoleType(careProviderStaffRoleTypeId);

            #endregion

            #region Step 22

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Staff Role Types")
                .ClickSelectFilterFieldOption("1")
                .ValidateSelectFilterFieldOptionIsPresent("1", "Delivers Care?")
                .SelectFilter("1", "Delivers Care?")
                .SelectOperator("1", "Equals")
                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ClickNewRecordButton_ResultsPage();

            careProviderStaffRoleTypeRecordPage
                .WaitForCareProviderStaffRoleTypeRecordPageToLoadFromAdvancedSearch()
                .InsertName("Advanced_Record" + _currentDateString)
                .InsertCode("9898")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateCareProviderStaffRoleTypeRecordTitle("Advanced_Record" + _currentDateString);

            Guid careProviderTypeId = dbHelper.careProviderStaffRoleType.GetByName("Advanced_Record" + _currentDateString).FirstOrDefault();
            var careProviderStaffRoleTypeCount = dbHelper.careProviderStaffRoleType.GetByName("Advanced_Record" + _currentDateString);
            Assert.AreEqual(1, careProviderStaffRoleTypeCount.Count);

            dbHelper.careProviderStaffRoleType.DeleteCareProviderStaffRoleType(careProviderTypeId);

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

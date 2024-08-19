using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    /// <summary>
    /// This class contains Automated UI test scripts for Provider Type
    /// </summary>
    [TestClass]
    public class MasterPayArrangements_JB_UITestCases : FunctionalTest
    {
        #region Properties

        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private string _teamName;
        private Guid _teamId;
        private Guid _defaultLoginUserID;
        private string _defaultLoginUserName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Authentication

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

                #endregion

                #region SDK API User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Master Pay Arrangements BU 1");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "Master Pay Arrangements Team 1";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90200", "MasterPayArrangementsTeam1@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _defaultLoginUserName = "MasterPayArrangement_User01";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_defaultLoginUserName, "Master Pay Arrangement", "User_01", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-7581

        [TestProperty("JiraIssueID", "ACC-7967")]
        [Description("Step(s) 1 to 3 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases001()
        {

            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateDisplayedFieldsInCreateMode();

            #endregion

            #region Step 2

            cpMasterPayArrangementRecordPage
                .ValidateApplyToAllEmployeeContracts_NoRadioButtonChecked()
                .ValidateApplyToAllProviders_NoRadioButtonChecked()
                .ValidateApplyToAllContractSchemes_YesRadioButtonChecked()
                .ValidateApplyToAllRoles_YesRadioButtonChecked()
                .ValidateApplyToAllContractTypes_YesRadioButtonChecked()
                .ValidateApplyToAllBookingTypes_NoRadioButtonChecked()
                .ValidateApplyToAllPersonContracts_YesRadioButtonChecked()
                .ValidateApplyToAllTimebandSets_NoRadioButtonChecked()

                .ValidateSaveAsDraft_NoRadioButtonChecked()
                .ValidateAllowForHybridRates_NoRadioButtonChecked()
                .ValidateApplyDurationFrom_NoRadioButtonChecked()
                .ValidateApplyDurationTo_NoRadioButtonChecked()

                .ValidateRateText("0.00")
                .ValidateUnitTypeSelectedText("Hour(s)")
                ;

            #endregion

            #region Step 3

            cpMasterPayArrangementRecordPage
                .SelectUnitType("Booking")
                .SelectUnitType("Hour(s)");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7968")]
        [Description("Step(s) 4 to 5 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases002()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 B", "91315", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "mpa_b_" + currentTimeString;
            var user2FirstName = "Master Pay Arrangement B";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2024, 2, 10), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2024, 2, 10), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion


            #region Step 4 & 5

            #region Create

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickAdditionalItemsButton() //Step 5 validation
                .ValidateToolbarOptionsDisplayed() //Step 5 validation
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickApplyToAllBookingTypes_NoRadioButton()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAsDraft_YesRadioButton()
                .ClickAllowForHybridRates_YesRadioButton()
                .ClickPayScheduledCareOnActuals_YesRadioButton()
                .ClickSaveButton()
                .WaitForPageToLoad()
                .ClickBackButton();

            #endregion

            #region Validate creation

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangementId = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString)[0];

            cpMasterPayArrangementsPage
                .ValidateHeaderCellSortAscendingOrder(2) //Step 5 validation
                .ValidateHeaderCellText(2, "Name") //Step 5 validation
                .ValidateHeaderCellText(3, "Contract Types") //Step 5 validation
                .ValidateHeaderCellText(4, "Roles") //Step 5 validation
                .ValidateHeaderCellText(5, "Booking Types") //Step 5 validation
                .ValidateHeaderCellText(6, "Providers") //Step 5 validation
                .ValidateHeaderCellText(7, "Employee Contracts") //Step 5 validation
                .ValidateHeaderCellText(8, "Timeband Sets") //Step 5 validation
                .ValidateHeaderCellText(9, "Start Date") //Step 5 validation
                .ValidateHeaderCellText(10, "End Date") //Step 5 validation
                .ValidateHeaderCellText(11, "Unit Type") //Step 5 validation
                .ValidateHeaderCellText(12, "Rate") //Step 5 validation
                .OpenRecord(masterPayArrangementId);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateNameText("MPA " + currentTimeString)
                .ValidateApplyToAllEmployeeContracts_NoRadioButtonChecked()
                .ValidateEmployeeContracts_SelectedOptionLinkText(systemUserEmploymentContract1Id.ToString(), user1FirstName + " " + user1LastName + " - Helper")
                .ValidateApplyToAllProviders_NoRadioButtonChecked()
                .ValidateProviders_SelectedOptionLinkText(provider1Id.ToString(), provider1Name)
                .ValidateApplyToAllContractSchemes_YesRadioButtonChecked()
                .ValidateApplyToAllRoles_YesRadioButtonChecked()
                .ValidateStartDateText("01/01/2024")
                .ValidateSaveAsDraft_YesRadioButtonChecked()
                .ValidateAllowForHybridRates_YesRadioButtonChecked()
                .ValidatePayScheduledCareOnActuals_YesRadioButtonChecked()

                .ValidateRateText("14.75")
                .ValidateApplyToAllContractTypes_YesRadioButtonChecked()
                .ValidateApplyToAllBookingTypes_NoRadioButtonChecked()
                .ValidateBookingTypes_SelectedOptionLinkText(_bookingType1Id.ToString(), "BTC ACC-868 A")
                .ValidateApplyToAllPersonContracts_YesRadioButtonChecked()
                .ValidateApplyToAllTimebandSets_NoRadioButtonChecked()
                .ValidateEndDateText("")
                .ValidateUnitTypeSelectedText("Hour(s)")
                .ValidateApplyDurationFrom_NoRadioButtonChecked()
                .ValidateApplyDurationTo_NoRadioButtonChecked();

            #endregion

            #region Update

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString + " Update")
                .InsertTextOnRate("26.31")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract2Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider2Name).TapSearchButton().AddElementToList(provider2Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 B").TapSearchButton().AddElementToList(_bookingType2Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 B").TapSearchButton().AddElementToList(timebandSet2Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("10/02/2024")
                .InsertTextOnEndDate("20/03/2024")
                .ClickSaveAsDraft_NoRadioButton()
                .ClickAllowForHybridRates_NoRadioButton()
                .ClickPayScheduledCareOnActuals_NoRadioButton()
                .SelectUnitType("Booking")
                .ClickSaveButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBackButton();

            #endregion

            #region Validate Update

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA " + currentTimeString + " Update")
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad()
                .OpenRecord(masterPayArrangementId);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateNameText("MPA " + currentTimeString + " Update")
                .ValidateApplyToAllEmployeeContracts_NoRadioButtonChecked()
                .ValidateEmployeeContracts_SelectedOptionLinkText(systemUserEmploymentContract1Id.ToString(), user1FirstName + " " + user1LastName + " - Helper")
                .ValidateEmployeeContracts_SelectedOptionLinkText(systemUserEmploymentContract2Id.ToString(), user2FirstName + " " + user2LastName + " - Helper")
                .ValidateApplyToAllProviders_NoRadioButtonChecked()
                .ValidateProviders_SelectedOptionLinkText(provider1Id.ToString(), provider1Name)
                .ValidateProviders_SelectedOptionLinkText(provider2Id.ToString(), provider2Name)
                .ValidateApplyToAllContractSchemes_YesRadioButtonChecked()
                .ValidateApplyToAllRoles_YesRadioButtonChecked()
                .ValidateStartDateText("10/02/2024")
                .ValidateEndDateText("20/03/2024")
                .ValidateSaveAsDraft_NoRadioButtonChecked()
                .ValidateAllowForHybridRates_NoRadioButtonChecked()
                .ValidatePayScheduledCareOnActuals_NoRadioButtonChecked()

                .ValidateRateText("26.31")
                .ValidateApplyToAllContractTypes_YesRadioButtonChecked()
                .ValidateApplyToAllBookingTypes_NoRadioButtonChecked()
                .ValidateBookingTypes_SelectedOptionLinkText(_bookingType1Id.ToString(), "BTC ACC-868 A")
                .ValidateBookingTypes_SelectedOptionLinkText(_bookingType2Id.ToString(), "BTC ACC-868 B")
                .ValidateApplyToAllPersonContracts_YesRadioButtonChecked()
                .ValidateApplyToAllTimebandSets_YesRadioButtonChecked()
                .ValidateEndDateText("20/03/2024")
                .ValidateUnitTypeSelectedText("Booking")
                .ValidateApplyDurationFrom_NoRadioButtonChecked()
                .ValidateApplyDurationTo_NoRadioButtonChecked();

            #endregion

            #region Delete

            cpMasterPayArrangementRecordPage
                .ClickDeleteRecordButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickClearFiltersButton_SearchArea();

            #endregion

            #region Validate Delete

            cpMasterPayArrangementsPage
                .ValidateRecordNotVisible(masterPayArrangementId);

            #endregion

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7969")]
        [Description("Step(s) 6 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases003()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 B", "91315", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var user1FullName = user1FirstName + " " + user1LastName;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "mpa_b_" + currentTimeString;
            var user2FirstName = "Master Pay Arrangement B";
            var user2LastName = currentTimeString;
            var user2FullName = user2FirstName + " " + user2LastName;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, new DateTime(2024, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, new DateTime(2024, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Step 6

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ValidateSystemViewSelectedText("Active Records")
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id)
                .TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract2Id)
                .TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id)
                .TypeSearchQuery(provider2Name).TapSearchButton().AddElementToList(provider2Id)
                .TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868").TapSearchButton().AddElementToList(_bookingType1Id).AddElementToList(_bookingType2Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868").TapSearchButton().AddElementToList(timebandSet1Id).AddElementToList(timebandSet2Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .InsertTextOnEndDate("31/01/2024")
                .ClickSaveAsDraft_YesRadioButton()
                .ClickAllowForHybridRates_YesRadioButton()
                .ClickPayScheduledCareOnActuals_YesRadioButton()
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangementId = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString)[0];

            cpMasterPayArrangementsPage
                .ValidateRecordCellText(masterPayArrangementId, 2, "MPA " + currentTimeString)
                .ValidateRecordCellText(masterPayArrangementId, 3, "Any")
                .ValidateRecordCellText(masterPayArrangementId, 4, "Any")
                .ValidateRecordCellText(masterPayArrangementId, 5, "BTC ACC-868 A, BTC ACC-868 B")
                .ValidateRecordCellText(masterPayArrangementId, 6, provider1Name + ", " + provider2Name)
                .ValidateRecordCellText(masterPayArrangementId, 7, user1FullName + " - Helper, " + user2FullName + " - Helper")
                .ValidateRecordCellText(masterPayArrangementId, 8, "TBS ACC-868 A, TBS ACC-868 B")
                .ValidateRecordCellText(masterPayArrangementId, 9, "01/01/2024")
                .ValidateRecordCellText(masterPayArrangementId, 10, "31/01/2024")
                .ValidateRecordCellText(masterPayArrangementId, 11, "Hour(s)")
                .ValidateRecordCellText(masterPayArrangementId, 12, "£14.75");

            cpMasterPayArrangementsPage
                .ClickColumnHeader(2)
                .WaitForPageToLoad()
                .ClickColumnHeader(9)
                .WaitForPageToLoad()
                .ClickColumnHeader(10)
                .WaitForPageToLoad()
                .ClickColumnHeader(11)
                .WaitForPageToLoad()
                .ClickColumnHeader(12)
                .WaitForPageToLoad();

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7970")]
        [Description("Step(s) 8 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases004()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 B", "91315", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var user1FullName = user1FirstName + " " + user1LastName;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "mpa_b_" + currentTimeString;
            var user2FirstName = "Master Pay Arrangement B";
            var user2LastName = currentTimeString;
            var user2FullName = user2FirstName + " " + user2LastName;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Step 8

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ValidateSystemViewSelectedText("Active Records")
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateNameFieldMaxLenght("100")

                .InsertTextOnRate("10000000.01")
                .ValidateRateErrorLabelVisible(true)
                .ValidateRateErrorLabelText("Please enter a value between -10000000 and 10000000.")
                .InsertTextOnRate("-10000000.01")
                .ValidateRateErrorLabelVisible(true)
                .ValidateRateErrorLabelText("Please enter a value between -10000000 and 10000000.")
                .InsertTextOnRate("-10000000.00")
                .ValidateRateErrorLabelVisible(false)

                .ClickApplyDurationFrom_YesRadioButton()

                .InsertTextOnDurationFromMinutes("0")
                .ValidateDurationFromMinutesErrorLabelVisible(true)
                .ValidateDurationFromMinutesErrorLabelText("Please enter a value between 1 and 4320.")
                .InsertTextOnDurationFromMinutes("4321")
                .ValidateDurationFromMinutesErrorLabelVisible(true)
                .ValidateDurationFromMinutesErrorLabelText("Please enter a value between 1 and 4320.")
                .InsertTextOnDurationFromMinutes("4320")
                .ValidateDurationFromMinutesErrorLabelVisible(false)

                .ClickApplyDurationTo_YesRadioButton()

                .InsertTextOnDurationToMinutes("0")
                .ValidateDurationToMinutesErrorLabelVisible(true)
                .ValidateDurationToMinutesErrorLabelText("Please enter a value between 1 and 4320.")
                .InsertTextOnDurationToMinutes("4321")
                .ValidateDurationToMinutesErrorLabelVisible(true)
                .ValidateDurationToMinutesErrorLabelText("Please enter a value between 1 and 4320.")
                .InsertTextOnDurationToMinutes("4320")
                .ValidateDurationToMinutesErrorLabelVisible(false);


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7971")]
        [Description("Step(s) 9 to 13 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases005()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType3Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 E", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1); // BTC 5

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 B", "91315", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var user1FullName = user1FirstName + " " + user1LastName;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "mpa_b_" + currentTimeString;
            var user2FirstName = "Master Pay Arrangement B";
            var user2LastName = currentTimeString;
            var user2FullName = user2FirstName + " " + user2LastName;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Step 9

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString)
                .InsertTextOnRate("-0.01")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("Rate for this Pay Arrangement is lower than 0. Do you want to save it as is?")
                .TapOKButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangementId = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString)[0];

            cpMasterPayArrangementsPage
                .OpenRecord(masterPayArrangementId);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("14/02/2024")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickSearchButton_SearchArea()
                .OpenRecord(masterPayArrangementId);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateStartDateText("14/02/2024");

            #endregion

            #region Step 10

            cpMasterPayArrangementRecordPage
                .InsertTextOnEndDate("11/02/2024")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("End date must be after start date on all pay arrangement records.").TapCloseButton();

            #endregion

            #region Step 11

            //already tested as part of test MasterPayArrangements_ACC_868_UITestCases002. In the update process we link multiple records to the fields mentioned in this step
            //Duration From (Minutes) and Duration To (Minutes) are tested as part of MasterPayArrangements_ACC_868_UITestCases004

            #endregion

            #region Step 12

            cpMasterPayArrangementRecordPage
                .InsertTextOnEndDate("")
                .ClickSaveAsDraft_YesRadioButton()

                .ClickApplyToAllEmployeeContracts_YesRadioButton()
                .ClickApplyToAllProviders_YesRadioButton()
                .ClickApplyToAllBookingTypes_YesRadioButton()
                .ClickApplyToAllTimebandSets_YesRadioButton()

                .ValidateEmployeeContractsLookupButtonVisible(false)
                .ValidateProvidersLookupButtonVisible(false)
                .ValidateRolesLookupButtonVisible(false)
                .ValidateContractTypesLookupButtonVisible(false)
                .ValidateBookingTypesLookupButtonVisible(false)
                .ValidateTimebandSetsLookupButtonVisible(false);

            #endregion

            #region Step 13

            cpMasterPayArrangementRecordPage
                .ValidateApplyToAllContractSchemes_YesRadioButtonChecked()
                .ValidateApplyToAllContractSchemes_YesRadioButtonEnabled(false)
                .ValidateApplyToAllContractSchemes_NoRadioButtonEnabled(false)
                .ValidateContractSchemesLookupButtonVisible(false)

                .ValidateApplyToAllPersonContracts_YesRadioButtonChecked()
                .ValidateApplyToAllPersonContracts_YesRadioButtonEnabled(false)
                .ValidateApplyToAllPersonContracts_NoRadioButtonEnabled(false)
                .ValidatePersonContractsLookupButtonVisible(false)

                .ClickApplyToAllBookingTypes_NoRadioButton()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 E").TapSearchButton().AddElementToList(_bookingType3Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()

                .ValidateApplyToAllContractSchemes_YesRadioButtonChecked()
                .ValidateApplyToAllContractSchemes_YesRadioButtonEnabled(true)
                .ValidateApplyToAllContractSchemes_NoRadioButtonEnabled(true)
                .ValidateContractSchemesLookupButtonVisible(false)

                .ValidateApplyToAllPersonContracts_YesRadioButtonChecked()
                .ValidateApplyToAllPersonContracts_YesRadioButtonEnabled(true)
                .ValidateApplyToAllPersonContracts_NoRadioButtonEnabled(true)
                .ValidatePersonContractsLookupButtonVisible(false);

            cpMasterPayArrangementRecordPage
                .ClickApplyToAllContractSchemes_NoRadioButton()
                .ClickApplyToAllPersonContracts_NoRadioButton()

                .ValidateContractSchemesLookupButtonEnabled(true)
                .ValidatePersonContractsLookupButtonEnabled(true)

                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()

                .ValidateApplyToAllContractSchemes_YesRadioButtonChecked()
                .ValidateApplyToAllContractSchemes_YesRadioButtonEnabled(false)
                .ValidateApplyToAllContractSchemes_NoRadioButtonEnabled(false)
                .ValidateContractSchemesLookupButtonVisible(false)

                .ValidateApplyToAllPersonContracts_YesRadioButtonChecked()
                .ValidateApplyToAllPersonContracts_YesRadioButtonEnabled(false)
                .ValidateApplyToAllPersonContracts_NoRadioButtonEnabled(false)
                .ValidatePersonContractsLookupButtonVisible(false);


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7972")]
        [Description("Step(s) 14 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases006()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var funderProviderName = "Funder Provider A " + currentTimeString;
            var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            var bookingTypeClassId = 5; //Booking (To Service User)
            var workingContractedTime = 1; //Count full booking length
            var bookingChargeType = 3; //Per Staff Number
            var _bookingType5Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 E1", bookingTypeClassId, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), workingContractedTime, false, null, null, null, bookingChargeType);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType5Id, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractScheme1Name = "CPCS_A_" + currentTimeString;
            var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2024, 1, 1), contractScheme1Code, provider1Id, funderProviderID);

            var contractScheme2Name = "CPCS_B_" + currentTimeString;
            var contractScheme2Code = contractScheme1Code + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme2Name, new DateTime(2024, 1, 1), contractScheme2Code, provider2Id, funderProviderID);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var firstName1 = "Christopher";
            var lastName1 = currentTimeString;
            var _person1ID = dbHelper.person.CreatePersonRecord("", firstName1, "", lastName1, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            var firstName2 = "Jhon";
            var lastName2 = currentTimeString;
            var _person2ID = dbHelper.person.CreatePersonRecord("", firstName2, "", lastName2, "", new DateTime(2000, 1, 1), _ethnicityId, _teamId, 1, 1);

            #endregion

            #region Person Contract

            var _personcontract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person1ID, _defaultLoginUserID, provider1Id, careProviderContractScheme1Id, funderProviderID, thisWeekMonday, null, true);

            var _personcontract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _person2ID, _defaultLoginUserID, provider2Id, careProviderContractScheme2Id, funderProviderID, thisWeekMonday, null, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 B", "91315", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var user1FullName = user1FirstName + " " + user1LastName;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "mpa_b_" + currentTimeString;
            var user2FirstName = "Master Pay Arrangement B";
            var user2LastName = currentTimeString;
            var user2FullName = user2FirstName + " " + user2LastName;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Step 14

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString)
                .InsertTextOnRate("12.34")
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 E1").TapSearchButton().AddElementToList(_bookingType5Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickApplyToAllContractSchemes_NoRadioButton()
                .ClickApplyToAllPersonContracts_NoRadioButton()
                .ClickContractSchemesLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .ValidateResultElementPresent(careProviderContractScheme1Id)
                .ValidateResultElementNotPresent(careProviderContractScheme2Id)
                .AddElementToList(careProviderContractScheme1Id)
                .TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickPersonContractsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .ValidateResultElementPresent(_personcontract1Id)
                .ValidateResultElementNotPresent(_personcontract2Id)
                .AddElementToList(_personcontract1Id)
                .TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProviders_SelectedOptionRemoveButton(provider1Id) //remove the selected provider
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .ValidateResultElementPresent(provider1Id) //only providers linked to the selected Contract Schemes and Person Contracts should be available for selection
                .ValidateResultElementNotPresent(provider2Id)
                .AddElementToList(provider1Id)
                .TapOKButton();
            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7973")]
        [Description("Step(s) 15 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases007()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 B", "91315", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "mpa_b_" + currentTimeString;
            var user2FirstName = "Master Pay Arrangement B";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion


            #region Step 15

            #region Create 1st record

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA A " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA A " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement1Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA A " + currentTimeString)[0];

            #endregion

            #region Create 2nd record

            cpMasterPayArrangementsPage
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA B " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This Pay Arrangement is in conflict with another one. Please make sure that two Pay Arrangements would not be valid for the same booking. If you want to save it anyway change status to Draft (in which case it will not be taken into account when calculating payroll records).")
                .TapCloseButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickSaveAsDraft_YesRadioButton()
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA B " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement2Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA B " + currentTimeString)[0];

            cpMasterPayArrangementsPage
                .ValidateRecordVisible(masterPayArrangement2Id);

            #endregion

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-7974")]
        [Description("Step(s) 16 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases008()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 B", "91315", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "mpa_b_" + currentTimeString;
            var user2FirstName = "Master Pay Arrangement B";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 C", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 D", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 1; //Monday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(9, 0, 0), endDay, new TimeSpan(17, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(17, 0, 0), endDay, new TimeSpan(23, 0, 0));

            #endregion


            #region Step 16

            #region Create 1st record

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA A " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 C").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA A " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement1Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA A " + currentTimeString)[0];

            #endregion

            #region Create 2nd record

            cpMasterPayArrangementsPage
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA B " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 D").TapSearchButton().AddElementToList(timebandSet2Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA B " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement2Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA B " + currentTimeString)[0];

            cpMasterPayArrangementsPage
                .ValidateRecordVisible(masterPayArrangement2Id);

            #endregion

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-7651

        [TestProperty("JiraIssueID", "ACC-7975")]
        [Description("Step(s) 18 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases009()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            var provider2Name = "Provider B " + currentTimeString;
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType1Id, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType2Id, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 B", "91315", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var user1FullName = user1FirstName + " " + user1LastName;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "mpa_b_" + currentTimeString;
            var user2FirstName = "Master Pay Arrangement B";
            var user2LastName = currentTimeString;
            var user2FullName = user2FirstName + " " + user2LastName;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Step 18

            #region Create record 1

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement1Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString)[0];

            #endregion

            #region Create record 2

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString + " - 2")
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This Pay Arrangement is in conflict with another one. Please make sure that two Pay Arrangements would not be valid for the same booking. If you want to save it anyway change status to Draft (in which case it will not be taken into account when calculating payroll records).")
                .TapCloseButton();

            #endregion


            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7976")]
        [Description("Step(s) 21 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases010()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType1Id, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var user1FullName = user1FirstName + " " + user1LastName;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);


            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id });

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Steps 20 and 21

            #region Create record 1

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickAllowForHybridRates_YesRadioButton()
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement1Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString)[0];

            #endregion

            #region Create record 2

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString + " - 2")
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .SelectUnitType("Booking")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("You are trying to create Hybrid Rates Pay Arrangement that means Pay Arrangement that would be used at the same time as another one. If you want to save the current one record as the Hybrid Rates Scenario Candidate please set 'Allow Hybrid Rates' flag in this record and try again. If you want both to be used at the same time for specific bookings, please, set 'Allow Hybrid Rates' flag in both records. Note that two Pay Arrangements need to have different unit types - one has to be 'per shift'.")
                .TapCloseButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickAllowForHybridRates_YesRadioButton()
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement2Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString + " - 2")[0];

            #endregion



            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-7977")]
        [Description("Step(s) 22 from the original test")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MasterPayArrangements_ACC_868_UITestCases011()
        {
            string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider

            var provider1Name = "Provider A " + currentTimeString;
            var addressType = 10; //Home
            var provider1Id = commonMethodsDB.CreateProvider(provider1Name, _teamId, 13, true, new DateTime(2024, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

            #endregion

            #region Booking Type

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-868 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, provider1Id, _bookingType1Id, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-868 A", "91310", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "mpa_a_" + currentTimeString;
            var user1FirstName = "Master Pay Arrangement A";
            var user1LastName = currentTimeString;
            var user1FullName = user1FirstName + " " + user1LastName;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);


            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Helper", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id });

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-868 A", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Step 22

            #region Create record 1

            loginPage
                   .GoToLoginPage()
                   .Login(_defaultLoginUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToFinanceAdminSection();

            financeAdminPage
                .WaitForFinanceAdminPageToLoad()
                .ClickCareProviderPayrollExpandButton()
                .ClickMasterPayArrangementsButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString)
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickApplyDurationFrom_YesRadioButton()
                .ClickApplyDurationTo_YesRadioButton()
                .InsertTextOnDurationFromMinutes("30")
                .InsertTextOnDurationToMinutes("60")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .InsertTextOnNameField_SearchArea("MPA " + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement1Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString)[0];

            #endregion

            #region Create record 2

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString + " - 2")
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickApplyDurationFrom_YesRadioButton()
                .InsertTextOnDurationFromMinutes("61")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement2Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString + " - 2")[0];

            #endregion

            #region Create record 3

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString + " - 3")
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickApplyDurationFrom_YesRadioButton()
                .ClickApplyDurationTo_YesRadioButton()
                .InsertTextOnDurationFromMinutes("1")
                .InsertTextOnDurationToMinutes("29")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement3Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString + " - 3")[0];

            #endregion

            #region Create record 4

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnName("MPA " + currentTimeString + " - 4")
                .InsertTextOnRate("14.75")
                .ClickEmployeeContractsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("*" + currentTimeString + "*").TapSearchButton().AddElementToList(systemUserEmploymentContract1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickProvidersLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery(provider1Name).TapSearchButton().AddElementToList(provider1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickBookingTypesLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("BTC ACC-868 A").TapSearchButton().AddElementToList(_bookingType1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ClickTimebandSetsLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("TBS ACC-868 A").TapSearchButton().AddElementToList(timebandSet1Id).TapOKButton();

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .InsertTextOnStartDate("01/01/2024")
                .ClickSaveAndCloseButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad();

            var masterPayArrangement4Id = dbHelper.careProviderMasterPayArrangement.GetByName("MPA " + currentTimeString + " - 4")[0];

            #endregion

            #endregion

        }

        #endregion

    }
}

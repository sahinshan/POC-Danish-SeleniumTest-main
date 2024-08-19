using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Configuration.CPFinanceAdmin
{
    /// <summary>
    /// This class contains Automated UI test scripts for  MPA Rates Uplift
    /// </summary>
    [TestClass]
    public class CPFinance_MpaRatesUplift_UITestCases : FunctionalTest
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("MPA Uplift BU1");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamName = "MPA Uplift Team1";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "90200", "MPATeam1@careworkstempmail.com", _teamName, "020 123456");

                #endregion

                #region Create default system user

                _defaultLoginUserName = "MPAUpliftUser01";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_defaultLoginUserName, "MPAUplift", "User01", "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8064

        [TestProperty("JiraIssueID", "ACC-8103")]
        [Description("Step(s) 1 to 7 from the original test ACC-901")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MpaRatesUplift_ACC_901_UITestCases001()
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

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-901 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-901 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-901 A", "91316", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-901 B", "91317", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "upliftmpa_a_" + currentTimeString;
            var user1FirstName = "UpliftMPA A";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "upliftmpa_b_" + currentTimeString;
            var user2FirstName = "UpliftMPA B";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Lifter", "4", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });
            string employmentContract1Name = (string)dbHelper.systemUserEmploymentContract.GetByID(systemUserEmploymentContract1Id, "name")["name"];
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });
            string employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetByID(systemUserEmploymentContract2Id, "name")["name"];

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-901 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-901 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Master Pay Arrangement

            Dictionary<Guid, string> Providers = new Dictionary<Guid, string>();
            Providers.Add(provider1Id, provider1Name);
            Dictionary<Guid, string> BookingTypes = new Dictionary<Guid, string>();
            BookingTypes.Add(_bookingType1Id, "BTC ACC-901 A");
            Dictionary<Guid, string> SystemUserEmploymentContracts = new Dictionary<Guid, string>();
            SystemUserEmploymentContracts.Add(systemUserEmploymentContract1Id, employmentContract1Name);
            Dictionary<Guid, string> TimebandSets = new Dictionary<Guid, string>();
            TimebandSets.Add(timebandSet1Id, "TBS ACC-901 A");


            var masterPayArrangementId1 = dbHelper.careProviderMasterPayArrangement.CreateRecord(_teamId, "UpliftMPA A " + currentTimeString,
                2, 10m, false, true, new DateTime(2024, 1, 1), null, true, false, Providers, true, null, false, BookingTypes, true, null,
                true, null, true, false, SystemUserEmploymentContracts, false, TimebandSets);

            var masterPayArrangementId2 = dbHelper.careProviderMasterPayArrangement.CreateRecord(_teamId, "UpliftMPA B " + currentTimeString,
                2, 20m, false, true, new DateTime(2024, 2, 1), null, true, false, Providers, true, null, false, BookingTypes, true, null,
                true, null, true, false, SystemUserEmploymentContracts, false, TimebandSets);

            #endregion


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
                .InsertTextOnNameField_SearchArea("*" + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad()
                .ValidateUpliftRatesIconIsDisplayed()
                .ValidateToolTipForUpliftRatesButton("Uplift Rates - change rates of existing ones.");

            #endregion

            #region Step 2

            cpMasterPayArrangementsPage
                .ValidateRecordVisible(masterPayArrangementId1)
                .ValidateRecordVisible(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Please select at least one record")
                .TapCloseButton();

            #endregion

            #region Step 3

            cpMasterPayArrangementsPage
                .SelectRecord(masterPayArrangementId1)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ValidateUpliftOptionsAreDisplayed();

            #endregion

            #region Step 4

            upliftRatesforMasterPayArrangementsPopup
                .ClickSetToSpecificValueOption()
                .ValidateSetToSpecificValueChecked(true)
                .ValidateChangeBySpecificValueChecked(false)
                .ValidateIncreaseByPercentageChecked(false)
                .ValidateUpliftValueFieldLabel("New Rate");

            upliftRatesforMasterPayArrangementsPopup
                .ClickChangeBySpecificValueOption()
                .ValidateChangeBySpecificValueChecked(true)
                .ValidateSetToSpecificValueChecked(false)
                .ValidateIncreaseByPercentageChecked(false)
                .ValidateUpliftValueFieldLabel("Change by");

            upliftRatesforMasterPayArrangementsPopup
                .ClickIncreaseByPercentageOption()
                .ValidateIncreaseByPercentageChecked(true)
                .ValidateSetToSpecificValueChecked(false)
                .ValidateChangeBySpecificValueChecked(false)
                .ValidateUpliftValueFieldLabel("Change by %");

            #endregion

            #region Step 5, 6, 7

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickSetToSpecificValueOption()
                .InsertUpliftValue("5")
                .ClickUplift();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .OpenRecord(masterPayArrangementId1);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateRateText("5.00")
                .ClickBackButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickChangeBySpecificValueOption()
                .InsertUpliftValue("25")
                .ClickUplift();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .OpenRecord(masterPayArrangementId1);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateRateText("30.00")
                .ClickBackButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .OpenRecord(masterPayArrangementId2);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateRateText("45.00")
                .ClickBackButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickIncreaseByPercentageOption()
                .InsertUpliftValue("50")
                .ClickUplift();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ValidateRecordCellText(masterPayArrangementId1, 12, "£45.00")
                .ValidateRecordCellText(masterPayArrangementId2, 12, "£67.50");

            #endregion

        }



        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8087

        [TestProperty("JiraIssueID", "ACC-8104")]
        [Description("Step(s) 7 from the original test ACC-901")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MpaRatesUplift_ACC_901_UITestCases002()
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

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-901 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-901 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-901 A", "91316", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-901 B", "91317", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "upliftmpa_a_" + currentTimeString;
            var user1FirstName = "UpliftMPA A";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "upliftmpa_b_" + currentTimeString;
            var user2FirstName = "UpliftMPA B";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Driver", "4", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });
            string employmentContract1Name = (string)dbHelper.systemUserEmploymentContract.GetByID(systemUserEmploymentContract1Id, "name")["name"];
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });
            string employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetByID(systemUserEmploymentContract2Id, "name")["name"];

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-901 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-901 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Master Pay Arrangement

            Dictionary<Guid, string> Providers = new Dictionary<Guid, string>();
            Providers.Add(provider1Id, provider1Name);
            Dictionary<Guid, string> BookingTypes = new Dictionary<Guid, string>();
            BookingTypes.Add(_bookingType1Id, "BTC ACC-901 A");
            Dictionary<Guid, string> SystemUserEmploymentContracts = new Dictionary<Guid, string>();
            SystemUserEmploymentContracts.Add(systemUserEmploymentContract1Id, employmentContract1Name);
            Dictionary<Guid, string> TimebandSets = new Dictionary<Guid, string>();
            TimebandSets.Add(timebandSet1Id, "TBS ACC-901 A");


            var masterPayArrangementId1 = dbHelper.careProviderMasterPayArrangement.CreateRecord(_teamId, "UpliftMPA A " + currentTimeString,
                2, 50m, false, true, new DateTime(2024, 1, 1), null, true, false, Providers, true, null, false, BookingTypes, true, null,
                true, null, true, false, SystemUserEmploymentContracts, false, TimebandSets);

            var masterPayArrangementId2 = dbHelper.careProviderMasterPayArrangement.CreateRecord(_teamId, "UpliftMPA B " + currentTimeString,
                2, 40m, false, true, new DateTime(2024, 2, 1), null, true, false, Providers, true, null, false, BookingTypes, true, null,
                true, null, true, false, SystemUserEmploymentContracts, false, TimebandSets);

            #endregion


            #region Step 7, 8

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
                .InsertTextOnNameField_SearchArea("*" + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickChangeBySpecificValueOption()
                .InsertUpliftValue("-10")
                .ClickUplift();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .OpenRecord(masterPayArrangementId1);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateRateText("40.00")
                .ClickBackButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .OpenRecord(masterPayArrangementId2);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateRateText("30.00")
                .ClickBackButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickIncreaseByPercentageOption()
                .InsertUpliftValue("-5")
                .ClickUplift();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ValidateRecordCellText(masterPayArrangementId1, 12, "£38.00")
                .ValidateRecordCellText(masterPayArrangementId2, 12, "£28.50");

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickIncreaseByPercentageOption()
                .InsertUpliftValue("-100")
                .ClickUplift();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .OpenRecord(masterPayArrangementId1);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateRateText("0.00")
                .ClickBackButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .OpenRecord(masterPayArrangementId2);

            cpMasterPayArrangementRecordPage
                .WaitForPageToLoad()
                .ValidateRateText("0.00")
                .ClickBackButton();



            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8105")]
        [Description("Step(s) 8 to 11 from the original test ACC-901")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Payroll")]
        [TestProperty("Screen1", "Master Pay Arrangements")]
        public void MpaRatesUplift_ACC_901_UITestCases003()
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

            var _bookingType1Id = commonMethodsDB.CreateCPBookingType("BTC ACC-901 A", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            var _bookingType2Id = commonMethodsDB.CreateCPBookingType("BTC ACC-901 B", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleType1Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-901 A", "91316", null, new DateTime(2020, 1, 1), null);
            var _careProviderStaffRoleType2Id = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT ACC-901 B", "91317", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Salaried

            var _employmentContractType1Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Salaried", "", null, new DateTime(2020, 1, 1));
            var _employmentContractType2Id = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User

            var user1name = "upliftmpa_a_" + currentTimeString;
            var user1FirstName = "UpliftMPA A";
            var user1LastName = currentTimeString;
            var systemUser1Id = commonMethodsDB.CreateSystemUserRecord(user1name, user1FirstName, user1LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            var user2name = "upliftmpa_b_" + currentTimeString;
            var user2FirstName = "UpliftMPA B";
            var user2LastName = currentTimeString;
            var systemUser2Id = commonMethodsDB.CreateSystemUserRecord(user2name, user2FirstName, user2LastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, authenticationproviderid);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Driver", "4", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Full Time Employee Contract", "2", null, new DateTime(2020, 1, 1));

            #endregion

            #region system User Employment Contract

            var systemUserEmploymentContract1Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser1Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });
            string employmentContract1Name = (string)dbHelper.systemUserEmploymentContract.GetByID(systemUserEmploymentContract1Id, "name")["name"];
            var systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(systemUser2Id, DateTime.Now.Date, _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 40, new List<Guid> { _bookingType1Id, _bookingType2Id });
            string employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetByID(systemUserEmploymentContract2Id, "name")["name"];

            #endregion

            #region Timeband Set

            var timebandSet1Id = commonMethodsDB.CreateTimebandSet("TBS ACC-901 A", _teamId);
            var timebandSet2Id = commonMethodsDB.CreateTimebandSet("TBS ACC-901 B", _teamId);

            #endregion

            #region Timeband

            var startDay = 1; //Monday
            var endDay = 7; //Sunday
            var timeband1Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet1Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));
            var timeband2Id = commonMethodsDB.CreateTimeband(_teamId, timebandSet2Id, startDay, new TimeSpan(0, 0, 0), endDay, new TimeSpan(0, 0, 0));

            #endregion

            #region Master Pay Arrangement

            Dictionary<Guid, string> Providers = new Dictionary<Guid, string>();
            Providers.Add(provider1Id, provider1Name);
            Providers.Add(provider2Id, provider2Name);
            Dictionary<Guid, string> BookingTypes = new Dictionary<Guid, string>();
            BookingTypes.Add(_bookingType1Id, "BTC ACC-901 A");
            Dictionary<Guid, string> SystemUserEmploymentContracts = new Dictionary<Guid, string>();
            SystemUserEmploymentContracts.Add(systemUserEmploymentContract1Id, employmentContract1Name);
            SystemUserEmploymentContracts.Add(systemUserEmploymentContract2Id, employmentContract2Name);
            Dictionary<Guid, string> TimebandSets = new Dictionary<Guid, string>();
            TimebandSets.Add(timebandSet1Id, "TBS ACC-901 A");


            var masterPayArrangementId1 = dbHelper.careProviderMasterPayArrangement.CreateRecord(_teamId, "UpliftMPA A " + currentTimeString,
                2, 50m, false, true, new DateTime(2024, 1, 1), null, true, false, Providers, true, null, false, BookingTypes, true, null,
                true, null, true, false, SystemUserEmploymentContracts, false, TimebandSets);

            var masterPayArrangementId2 = dbHelper.careProviderMasterPayArrangement.CreateRecord(_teamId, "UpliftMPA B " + currentTimeString,
                2, 40m, false, true, new DateTime(2024, 2, 1), null, true, false, Providers, true, null, false, BookingTypes, true, null,
                true, null, true, false, SystemUserEmploymentContracts, false, TimebandSets);

            #endregion


            #region Step 9, 10, 11

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
                .InsertTextOnNameField_SearchArea("*" + currentTimeString)
                .ClickSearchButton_SearchArea()
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickSetToSpecificValueOption()
                .ValidateUpliftValueFieldRange("-10000000,10000000")
                .InsertUpliftValue("20000000")
                .ValidateUpliftValueFieldError("Please enter a value between -10000000 and 10000000.")
                .ClickChangeBySpecificValueOption()
                .InsertUpliftValue("1000000")
                .ValidateUpliftValueFieldError("Please enter a value between -100000 and 100000.")
                .ValidateUpliftValueFieldRange("-100000,100000")
                .ClickIncreaseByPercentageOption()
                .ValidateUpliftValueFieldRange("-100,1000")
                .InsertUpliftValue("-200")
                .ValidateUpliftValueFieldError("Please enter a value between -100 and 1000.")
                .ClickClose();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickSetToSpecificValueOption()
                .InsertUpliftValue("10000000")
                .ClickClose();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ValidateRecordCellText(masterPayArrangementId1, 12, "£50.00")
                .ValidateRecordCellText(masterPayArrangementId2, 12, "£40.00");

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickSetToSpecificValueOption()
                .InsertUpliftValue("10000000")
                .ClickUplift();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ValidateRecordCellText(masterPayArrangementId1, 12, "£10,000,000.00")
                .ValidateRecordCellText(masterPayArrangementId2, 12, "£10,000,000.00")
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickIncreaseByPercentageOption()
                .InsertUpliftValue("1")
                .ClickUplift();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("For 2 Master Pay Arrangement records we were unable to uplift rates as new values did not meet validation requirements. Final “Rate” value needs to be between -10000000 and 10000000.\r\n")
                .TapOKButton();

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .ValidateRecordCellText(masterPayArrangementId1, 12, "£10,000,000.00")
                .ValidateRecordCellText(masterPayArrangementId2, 12, "£10,000,000.00");

            cpMasterPayArrangementsPage
                .WaitForPageToLoad()
                .SelectRecord(masterPayArrangementId1)
                .SelectRecord(masterPayArrangementId2)
                .ClickUpliftRatesButton();

            upliftRatesforMasterPayArrangementsPopup
                .WaitForPageToLoad()
                .ClickSetToSpecificValueOption()
                .InsertUpliftValue("-5")
                .ClickUplift();

            alertPopup
                 .WaitForAlertPopupToLoad()
                 .ValidateAlertText("There are some rates that weren't changed because of changing value from positive to negative. Can't change that by update multiple, only on single edit view.\r\n")
                 .TapOKButton();

            #endregion

        }


        #endregion 

    }
}

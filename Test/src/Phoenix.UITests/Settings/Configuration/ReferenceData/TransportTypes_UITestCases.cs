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
    public class TransportTypes_UITestCases : FunctionalTest
    {
        #region properties

        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private string _systemUserName;

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

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("Transport Type BU");

            #endregion

            #region Team

            _teamId = commonMethodsDB.CreateTeam("Transport Type T1", null, _businessUnitId, "907678", "TransportTypeT1@careworkstempmail.com", "Transport Type", "020 123456");

            #endregion

            #region System User

            _systemUserName = "TransportTypeUser1";
            commonMethodsDB.CreateSystemUserRecord(_systemUserName, "TransportType", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-247

        [TestProperty("JiraIssueID", "ACC-326")]
        [Description("Step(s) 4 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Reference Data")]
        [TestProperty("Screen1", "Transport Types")]
        public void TransportTypes_UITestMethod001()
        {
            var transportTypeID1 = dbHelper.transportType.GetTransportTypeByName("Bicycle")[0];
            var transportTypeID2 = dbHelper.transportType.GetTransportTypeByName("Car")[0];
            var transportTypeID3 = dbHelper.transportType.GetTransportTypeByName("Motorbike")[0];
            var transportTypeID4 = dbHelper.transportType.GetTransportTypeByName("Walking")[0];

            #region Steps 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Transport Types")
                .ClickReferenceDataMainHeader("Care Provider Transport Availability")
                .ClickReferenceDataElement("Transport Types");

            transportTypesPage
                .WaitForTransportTypesPageToLoad()
                .ValidateRecordCellContent(transportTypeID1, 2, "Bicycle")
                .ValidateRecordCellContent(transportTypeID2, 2, "Car")
                .ValidateRecordCellContent(transportTypeID3, 2, "Motorbike")
                .ValidateRecordCellContent(transportTypeID4, 2, "Walking");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-327")]
        [Description("Step(s) 5 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod002()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");

            #region Transport Type

            var transporttypeiconid = 12; //Other
            var transportTypeId = commonMethodsDB.CreateTransportType(_teamId, "Longboard", new DateTime(2000, 1, 1), 1, "15", transporttypeiconid);

            #endregion

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region Steps 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Longboard");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-328")]
        [Description("Step(s) 6 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Reference Data")]
        [TestProperty("Screen1", "Transport Types")]
        public void TransportTypes_UITestMethod003()
        {
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Transport Type

            var transporttypeiconid = 12; //Other
            var transportTypeId = commonMethodsDB.CreateTransportType(_teamId, "Longboard", new DateTime(2000, 1, 1), 1, "15", transporttypeiconid);

            #endregion

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region User Transportation Schedule

            var todayDate = DateTime.Now.Date;
            var recurrencePatternTitle = "Occurs every 1 week(s) on " + DateTime.Now.DayOfWeek.ToString().ToLower();
            var recurrencePatternId = dbHelper.recurrencePattern.GetByTitle(recurrencePatternTitle).FirstOrDefault();
            dbHelper.userTransportationSchedule.CreateSystemUserTransportationSchedule(_teamId, userId, "AutoGenerated", todayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), recurrencePatternId, transportTypeId);

            #endregion

            #region Steps 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Transport Types")
                .ClickReferenceDataMainHeader("Care Provider Transport Availability")
                .ClickReferenceDataElement("Transport Types");

            transportTypesPage
                .WaitForTransportTypesPageToLoad()
                .InsertSearchQuery("Longboard")
                .TapSearchButton()
                .OpenRecord(transportTypeId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("transporttype")
                .ClickOnExpandIcon();

            transportTypeRecordPage
                .WaitForTransportTypeRecordPageToLoad()
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("Related record exists in User Transportation Schedule. Please delete related records before deleting record in Transport Type.").TapCloseButton();

            transportTypeRecordPage
                .WaitForTransportTypeRecordPageToLoad()
                .ClickBackButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-329")]
        [Description("Step(s) 7 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod004()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region Steps 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Bicycle")
                .ValidateTransportationModeIsVisible("Car")
                .ValidateTransportationModeIsVisible("Motorbike")
                .ValidateTransportationModeIsVisible("Walking");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-330")]
        [Description("Step(s) 9 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod005()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region Steps 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-331")]
        [Description("Step(s) 11 to 14 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod006()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Transport Type

            var transportTypeID = dbHelper.transportType.GetTransportTypeByName("Car")[0];

            #endregion

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region User Transportation Schedule

            var todayDate = DateTime.Now.Date;
            var recurrencePatternTitle = "Occurs every 1 week(s) on " + DateTime.Now.DayOfWeek.ToString().ToLower();
            var recurrencePatternId = dbHelper.recurrencePattern.GetByTitle(recurrencePatternTitle).FirstOrDefault();
            dbHelper.userTransportationSchedule.CreateSystemUserTransportationSchedule(_teamId, userId, "AutoGenerated", todayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), recurrencePatternId, transportTypeID);

            #endregion

            #region Availability Type

            var availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted")[0];

            #endregion

            #region User Work Schedule

            dbHelper.userWorkSchedule.CreateUserWorkSchedule(userId, _teamId, recurrencePatternId, systemUserEmploymentContractId, availabilityTypeId, todayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);

            #endregion

            #region Steps 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car");

            #endregion

            #region Step 12

            systemUserAvailabilitySubPage
                .ValidateWeek1CycleDate(currentWeekMonday.ToString("dd'/'MM'/'yyyy"));

            #endregion

            #region Step 13

            systemUserAvailabilitySubPage
                .ValidateWeek1CycleDateReadonly(true);

            #endregion

            #region Step 14

            systemUserAvailabilitySubPage
                .ClickAddWeekButton()
                .ClickAddWeekButton()

                .ClickWeekButton(1)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.ToString("dd'/'MM'/'yyyy"), true)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), false)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), false)

                .ClickWeekButton(2)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.ToString("dd'/'MM'/'yyyy"), false)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), true)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), false)

                .ClickWeekButton(3)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.ToString("dd'/'MM'/'yyyy"), false)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), false)
                .ValidateScheduleTransportDayOfWeekAreaVisible(todayDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-332")]
        [Description("Step(s) 16 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod007()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Transport Type

            var transportTypeID = dbHelper.transportType.GetTransportTypeByName("Car")[0];

            #endregion

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            var systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region User Transportation Schedule

            var todayDate = DateTime.Now.Date;
            var recurrencePatternTitle = "Occurs every 1 week(s) on " + DateTime.Now.DayOfWeek.ToString().ToLower();
            var recurrencePatternId = dbHelper.recurrencePattern.GetByTitle(recurrencePatternTitle).FirstOrDefault();
            dbHelper.userTransportationSchedule.CreateSystemUserTransportationSchedule(_teamId, userId, "AutoGenerated", todayDate, null, new TimeSpan(9, 0, 0), new TimeSpan(17, 0, 0), recurrencePatternId, transportTypeID);

            #endregion

            #region Steps 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickSpecificSlotToCreateOrEditScheduleTransport(currentDate, "car");

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(false).ClickRemoveTimeSlotButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-333")]
        [Description("Step(s) 17 to 19 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod008()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region Steps 17

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragScheduleTransportSlotRightSlider(currentDate, "car", 500)
                .DragScheduleTransportSlotLeftSlider(currentDate, "car", -700)
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true);

            #endregion

            #region Step 18

            systemUserAvailabilitySubPage
                .ClickSpecificSlotToCreateOrEditScheduleTransport(currentDate, "car");

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(false);

            #endregion

            #region Step 19

            systemUserAvailabilityScheduleTransportPage.ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "walking", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-334")]
        [Description("Step(s) 20 to 23 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod009()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region Steps 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "walking", true);

            #endregion

            #region Step 22

            systemUserAvailabilitySubPage
                .ClickSpecificSlotToCreateOrEditScheduleTransport(currentDate, "walking");

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(false).ClickBicycleButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "bicycle", true);

            #endregion

            #region Step 23

            systemUserAvailabilitySubPage
                .DragScheduleTransportSlotRightSlider(currentDate, "car", 45)
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "bicycle", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-335")]
        [Description("Step(s) 24 to 25 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod010()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            string tomorrowDate = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion


            #region Steps 24 & 25

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ClickCreateScheduleTransportButtonByDate(tomorrowDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(tomorrowDate, "car", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-336")]
        [Description("Step(s) 26 to 27 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod011()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            string tomorrowDate = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region Steps 26 & 27

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "walking", true)
                .ClickCreateScheduleTransportButtonByDate(tomorrowDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(tomorrowDate, "car", true)
                .ClickCreateScheduleTransportButtonByDate(tomorrowDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(tomorrowDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(tomorrowDate, "walking", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-337")]
        [Description("Step(s) 28 to 29 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod012()
        {
            string week3Date = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion


            #region Steps 28 & 29

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickWeekButton(3)
                .ClickCreateScheduleTransportButtonByDate(week3Date, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ClickWeekButton(3)
                .ValidateAvailableRecordUnderScheduleTransport(week3Date, "car", true)
                .ClickCreateScheduleTransportButtonByDate(week3Date, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ClickWeekButton(3)
                .ValidateAvailableRecordUnderScheduleTransport(week3Date, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(week3Date, "walking", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-338")]
        [Description("Step(s) 30 to 31 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod013()
        {
            string currentDate;
            string week1OtherDate;
            string week3Date;

            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                week1OtherDate = DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy");
            else
                week1OtherDate = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");

            currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            week3Date = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion


            #region Steps 30 & 31

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickWeekButton(3)
                .ClickCreateScheduleTransportButtonByDate(week3Date, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ClickWeekButton(3)
                .ValidateAvailableRecordUnderScheduleTransport(week3Date, "car", true)
                .ClickCreateScheduleTransportButtonByDate(week3Date, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ClickWeekButton(3)
                .ValidateAvailableRecordUnderScheduleTransport(week3Date, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(week3Date, "walking", true)
                .ClickWeekButton(1)
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ClickWeekButton(1)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ClickCreateScheduleTransportButtonByDate(week1OtherDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragScheduleTransportSlotRightSlider(week1OtherDate, "walking", -50)
                .DragScheduleTransportSlotLeftSlider(week1OtherDate, "walking", -50)
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ClickWeekButton(1)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(week1OtherDate, "walking", true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-339")]
        [Description("Step(s) 34 to 35 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod014()
        {
            var currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion


            #region Steps 34 to 35

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true);

            var scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(userId);
            Assert.AreEqual(2, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .DragScheduleTransportSlotRightSlider(currentDate, "car", 50)
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true);

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(userId);
            Assert.AreEqual(1, scheduleTransports.Count());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-340")]
        [Description("Step(s) 36 to 37 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod015()
        {
            var currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var currentDatePlus1Day = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
            var currentDatePlus2Day = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
            var currentDatePlus3Day = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
            var currentDatePlus4Day = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
            var currentDatePlus5Day = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
            var currentDatePlus6Day = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region Steps 36 to 37

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus1Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus1Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus2Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus2Day, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus3Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus3Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus4Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus4Day, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus5Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus5Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus6Day, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(currentDatePlus6Day, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus1Day, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus1Day, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus2Day, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus2Day, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus3Day, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus3Day, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus4Day, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus4Day, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus5Day, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus5Day, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus6Day, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(currentDatePlus6Day, "walking", true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-341")]
        [Description("Step(s) 38 to 39 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void TransportTypes_UITestMethod016()
        {
            string Week1Day1 = "";
            string Week1Day2 = "";
            string Week1Day3 = "";
            string Week1Day4 = "";
            string Week1Day5 = "";
            string Week1Day6 = "";
            string Week1Day7 = "";

            string Week2Day1 = "";
            string Week2Day2 = "";
            string Week2Day3 = "";
            string Week2Day4 = "";
            string Week2Day5 = "";
            string Week2Day6 = "";
            string Week2Day7 = "";
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Calculate week 1 and week 2 dates

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    #region If current date is a Sunday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Monday:
                    #region If current date is a Monday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Tuesday:
                    #region If current date is a Tuesday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Wednesday:
                    #region If current date is a Wednesday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Thursday:
                    #region If current date is a Thursday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Friday:
                    #region If current date is a Friday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Saturday:
                    #region If current date is a Saturday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                default:
                    break;
            }

            #endregion

            #region System User

            var currentDateTimeSting = commonMethodsHelper.GetCurrentDateTimeString();
            var username = "ACC_247_" + currentDateTimeSting;
            var userId = commonMethodsDB.CreateSystemUserRecord(username, "ACC_247", currentDateTimeSting, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userId, currentWeekMonday);
            dbHelper.systemUser.UpdateSATransportWeek1CycleStartDate(userId, currentWeekMonday);

            #endregion

            #region Care Provider Staff Role Type

            var careProviderStaffRoleTypeId = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Computer Specialist Level 1", "998", "998", new DateTime(2000, 1, 1), "Computer Specialist (base level)");

            #endregion

            #region Employment Contract Type

            var employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Employment Contract

            var startDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date.AddDays(-10));
            dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userId, startDate, careProviderStaffRoleTypeId, _teamId, employmentContractTypeId, 40);

            #endregion

            #region Steps 38 to 39

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(username)
                .ClickSearchButton()
                .OpenRecord(userId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            //WEEK 1 Schedules

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickScheduleTransportCard()
                .ClickAddWeekButton()
                .ClickWeekButton(1)
                .ClickCreateScheduleTransportButtonByDate(Week1Day1, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day1, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day2, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day2, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day3, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day3, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day4, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day4, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day5, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day5, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day6, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day6, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day7, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week1Day7, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();


            //WEEK 2 Schedules

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickWeekButton(2)
                .ClickCreateScheduleTransportButtonByDate(Week2Day1, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day1, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day2, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day2, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day3, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day3, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day4, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day4, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day5, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day5, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day6, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day6, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day7, 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(Week2Day7, 3);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickWalkingButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveAndCloseButton()
                .WaitForSavedOperationToComplete()
                .ClickScheduleTransportCard()
                .ClickWeekButton(1)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day1, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day1, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day2, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day2, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day3, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day3, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day4, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day4, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day5, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day5, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day6, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day6, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day7, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day7, "walking", true)
                .ClickWeekButton(2)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day1, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day1, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day2, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day2, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day3, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day3, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day4, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day4, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day5, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day5, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day6, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day6, "walking", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day7, "car", true)
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day7, "walking", true);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-1799

        [TestProperty("JiraIssueID", "ACC-1958")]
        [Description("Step(s) 1 to 7 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Reference Data")]
        [TestProperty("Screen1", "Transport Types")]
        public void TransportTypes_RD_UITestMethod001()
        {
            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Transport Types")
                .ClickReferenceDataMainHeader("Care Provider Transport Availability")
                .ClickReferenceDataElement("Transport Types");

            transportTypesPage
                .WaitForTransportTypesPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("transporttype")
                .ClickOnExpandIcon();

            transportTypeRecordPage
                .WaitForTransportTypeRecordPageToLoad()
                .ValidateNewRecordFieldsVisible()
                .ValidateMileageExpenseCostsApplyToThisTransportTypeYesOptionChecked(true);

            #endregion

            #region Step 2

            transportTypeRecordPage
                .ValidateAutoAllocationJourneyBetweenBookingsSectionVisible()
                .ValidateJourneyBetweenBookings_AverageJourneyTime_FieldValue("15");

            #endregion

            #region Step 3

            transportTypeRecordPage
                .ValidateAutoAllocationJourneyToFromHomeVisible()
                .ValidateJourneyToFromHome_AverageJourneyTime_FieldValue("15");

            #endregion

            #region Step 4

            transportTypeRecordPage
                .SelectTravelTimeCalculation("Average Speed")
                .SelectTravelTimeCalculation("Third Party");

            #endregion

            #region Step 5

            transportTypeRecordPage
                .SelectTravelTimeCalculation("")
                .InsertStartDate("")
                .ClickResponsibleTeamRemoveButton()
                .InsertJourneyBetweenBookings_AverageJourneyTime("")
                .InsertJourneyToFromHome_AverageJourneyTime("")
                .ClickSaveAndCloseButton();

            transportTypeRecordPage
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateTravelTimeCalculationFieldErrorLabelText("Please fill out this field.")
                .ValidateSpeedFieldErrorLabelText("Please fill out this field.")
                .ValidateStartDateFieldErrorLabelText("Please fill out this field.")
                .ValidateIconFieldErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamFieldErrorLabelText("Please fill out this field.")
                .ValidateJourneyBetweenBookings_AverageJourneyTime_FieldErrorLabelText("Please fill out this field.")
                .ValidateJourneyToFromHome_AverageJourneyTime_FieldErrorLabelText("Please fill out this field.");


            #endregion

            #region Step 6

            transportTypeRecordPage
                .InsertSpeed("-1")
                .ValidateSpeedFieldErrorLabelText("Please enter a value between 0 and 5000.")
                .InsertSpeed("5001")
                .ValidateSpeedFieldErrorLabelText("Please enter a value between 0 and 5000.")
                .InsertSpeed("5001")
                .ValidateSpeedFieldErrorLabelText("Please enter a value between 0 and 5000.")
                .InsertSpeed("5000")
                .ValidateSpeedFieldErrorLabelVisible(false);

            transportTypeRecordPage
                .InsertJourneyBetweenBookings_TotalJourneyTimeExceeds("-1")
                .ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabelText("Please enter a value between 0 and 10080.")
                .InsertJourneyBetweenBookings_TotalJourneyTimeExceeds("10081")
                .ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabelText("Please enter a value between 0 and 10080.")
                .InsertJourneyBetweenBookings_TotalJourneyTimeExceeds("10080")
                .ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabelVisible(false)
                .InsertJourneyBetweenBookings_TotalJourneyTimeExceeds("");

            transportTypeRecordPage
                .InsertJourneyBetweenBookings_TotalJourneyDistanceExceeds("-1")
                .ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabelText("Please enter a value between 0 and 42000.")
                .InsertJourneyBetweenBookings_TotalJourneyDistanceExceeds("42001")
                .ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabelText("Please enter a value between 0 and 42000.")
                .InsertJourneyBetweenBookings_TotalJourneyDistanceExceeds("42000")
                .ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabelVisible(false)
                .InsertJourneyBetweenBookings_TotalJourneyDistanceExceeds("");

            transportTypeRecordPage
                .InsertJourneyBetweenBookings_AverageJourneyTime("-1")
                .ValidateJourneyBetweenBookings_AverageJourneyTime_FieldErrorLabelText("Please enter a value between 0 and 10080.")
                .InsertJourneyBetweenBookings_AverageJourneyTime("10081")
                .ValidateJourneyBetweenBookings_AverageJourneyTime_FieldErrorLabelText("Please enter a value between 0 and 10080.")
                .InsertJourneyBetweenBookings_AverageJourneyTime("10080")
                .ValidateJourneyBetweenBookings_AverageJourneyTime_FieldErrorLabelVisible(false)
                .InsertJourneyBetweenBookings_AverageJourneyTime("");

            transportTypeRecordPage
                .InsertJourneyToFromHome_TotalJourneyTimeExceeds("-1")
                .ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabelText("Please enter a value between 0 and 10080.")
                .InsertJourneyToFromHome_TotalJourneyTimeExceeds("10081")
                .ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabelText("Please enter a value between 0 and 10080.")
                .InsertJourneyToFromHome_TotalJourneyTimeExceeds("10080")
                .ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabelVisible(false)
                .InsertJourneyToFromHome_TotalJourneyTimeExceeds("");

            transportTypeRecordPage
                .InsertJourneyToFromHome_TotalJourneyDistanceExceeds("-1")
                .ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabelText("Please enter a value between 0 and 42000.")
                .InsertJourneyToFromHome_TotalJourneyDistanceExceeds("42001")
                .ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabelText("Please enter a value between 0 and 42000.")
                .InsertJourneyToFromHome_TotalJourneyDistanceExceeds("42000")
                .ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabelVisible(false)
                .InsertJourneyToFromHome_TotalJourneyDistanceExceeds("");

            transportTypeRecordPage
                .InsertJourneyToFromHome_AverageJourneyTime("-1")
                .ValidateJourneyToFromHome_AverageJourneyTime_FieldErrorLabelText("Please enter a value between 0 and 10080.")
                .InsertJourneyToFromHome_AverageJourneyTime("10081")
                .ValidateJourneyToFromHome_AverageJourneyTime_FieldErrorLabelText("Please enter a value between 0 and 10080.")
                .InsertJourneyToFromHome_AverageJourneyTime("10080")
                .ValidateJourneyToFromHome_AverageJourneyTimeFieldErrorLabelVisible(false)
                .InsertJourneyToFromHome_AverageJourneyTime("");

            #endregion

            #region Step 7

            transportTypeRecordPage
                .InsertJourneyBetweenBookings_TotalJourneyTimeExceeds("12")
                .ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldDisabled(true)

                .InsertJourneyBetweenBookings_TotalJourneyTimeExceeds("")
                .InsertJourneyBetweenBookings_TotalJourneyDistanceExceeds("7")
                .ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldDisabled(true);

            transportTypeRecordPage
                .InsertJourneyToFromHome_TotalJourneyTimeExceeds("12")
                .ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldDisabled(true)

                .InsertJourneyToFromHome_TotalJourneyTimeExceeds("")
                .InsertJourneyToFromHome_TotalJourneyDistanceExceeds("7")
                .ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldDisabled(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-1959")]
        [Description("Step(s) 8 and 9 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Reference Data")]
        [TestProperty("Screen1", "Transport Types")]
        public void TransportTypes_RD_UITestMethod002()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Transport Types")
                .ClickReferenceDataMainHeader("Care Provider Transport Availability")
                .ClickReferenceDataElement("Transport Types");

            transportTypesPage
                .WaitForTransportTypesPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("transporttype")
                .ClickOnExpandIcon();

            transportTypeRecordPage
                .WaitForTransportTypeRecordPageToLoad()
                .ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldLabelAttribute("title", "Only one “A cost is incurred..” field can be used at a time. Clear the value in one field to enable the other.")
                .ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldLabelAttribute("title", "Only one “A cost is incurred..” field can be used at a time. Clear the value in one field to enable the other.")
                .ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldLabelAttribute("title", "Only one “A cost is incurred..” field can be used at a time. Clear the value in one field to enable the other.")
                .ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldLabelAttribute("title", "Only one “A cost is incurred..” field can be used at a time. Clear the value in one field to enable the other.");

            #endregion

            #region Step 9

            transportTypeRecordPage
                .InsertName("ACC-1799 " + currentDateTimeString)
                .SelectTravelTimeCalculation("Average Speed")
                .InsertSpeed("15")
                .SelectIcon("Other")
                .InsertStartDate("01/01/2020")
                .ClickSaveAndCloseButton();

            transportTypesPage
                .WaitForTransportTypesPageToLoad()
                .ClickRefreshButton();

            var newRecordID = dbHelper.transportType.GetTransportTypeByName("ACC-1799 " + currentDateTimeString).First();

            transportTypesPage
                .InsertSearchQuery("ACC-1799 " + currentDateTimeString)
                .TapSearchButton()
                .OpenRecord(newRecordID.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("transporttype")
                .ClickOnExpandIcon();

            transportTypeRecordPage
                .WaitForTransportTypeRecordPageToLoad()
                .ValidateNameFieldValue("ACC-1799 " + currentDateTimeString)
                .ValidateCodeFieldValue("")
                .ValidateGovCodeFieldValue("")
                .ValidateInactiveYesOptionChecked(false)
                .ValidateTravelTimeCalculationSelectedText("Average Speed")
                .ValidateSpeedFieldValue("15")
                .ValidateIconSelectedText("Other")
                .ValidateStartDateFieldValue("01/01/2020")
                .ValidateEndDateFieldValue("")
                .ValidateValidForExportYesOptionChecked(false)
                .ValidateResponsibleTeamLinkFieldText("Transport Type T1")

                .ValidateMileageExpenseCostsApplyToThisTransportTypeYesOptionChecked(true)

                .ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldValue("")
                .ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldValue("")
                .ValidateJourneyBetweenBookings_AverageJourneyTime_FieldValue("15")

                .ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldValue("")
                .ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldValue("")
                .ValidateJourneyToFromHome_AverageJourneyTime_FieldValue("15");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1960")]
        [Description("Step(s) 10 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Reference Data")]
        [TestProperty("Screen1", "Transport Types")]
        public void TransportTypes_RD_UITestMethod003()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Transport Types")
                .ClickReferenceDataMainHeader("Care Provider Transport Availability")
                .ClickReferenceDataElement("Transport Types");

            transportTypesPage
                .WaitForTransportTypesPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("transporttype")
                .ClickOnExpandIcon();

            transportTypeRecordPage
                .WaitForTransportTypeRecordPageToLoad()
                .InsertName("ACC-1799 " + currentDateTimeString)
                .InsertCode("9000")
                .InsertGovCode("9001")
                .ClickInactiveYesOption()
                .SelectTravelTimeCalculation("Average Speed")
                .InsertSpeed("15")
                .SelectIcon("Other")
                .InsertStartDate("01/01/2020")
                .InsertEndDate("02/01/2020")
                .InsertJourneyBetweenBookings_TotalJourneyTimeExceeds("30")
                .InsertJourneyBetweenBookings_AverageJourneyTime("20")
                .InsertJourneyToFromHome_TotalJourneyDistanceExceeds("25")
                .InsertJourneyToFromHome_AverageJourneyTime("10")
                .ClickSaveAndCloseButton();

            transportTypesPage
                .WaitForTransportTypesPageToLoad()
                .ClickRefreshButton();

            var newRecordID = dbHelper.transportType.GetTransportTypeByName("ACC-1799 " + currentDateTimeString).First();

            transportTypesPage
                .InsertSearchQuery("ACC-1799 " + currentDateTimeString)
                .TapSearchButton()
                .OpenRecord(newRecordID.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("transporttype")
                .ClickOnExpandIcon();

            transportTypeRecordPage
                .WaitForTransportTypeRecordPageToLoad()
                .ValidateNameFieldValue("ACC-1799 " + currentDateTimeString)
                .ValidateCodeFieldValue("9000")
                .ValidateGovCodeFieldValue("9001")
                .ValidateInactiveYesOptionChecked(true)
                .ValidateTravelTimeCalculationSelectedText("Average Speed")
                .ValidateSpeedFieldValue("15")
                .ValidateIconSelectedText("Other")
                .ValidateStartDateFieldValue("01/01/2020")
                .ValidateEndDateFieldValue("02/01/2020")
                .ValidateValidForExportYesOptionChecked(false)
                .ValidateResponsibleTeamLinkFieldText("Transport Type T1")

                .ValidateMileageExpenseCostsApplyToThisTransportTypeYesOptionChecked(true)

                .ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldValue("30")
                .ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldValue("")
                .ValidateJourneyBetweenBookings_AverageJourneyTime_FieldValue("20")

                .ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldValue("")
                .ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldValue("25")
                .ValidateJourneyToFromHome_AverageJourneyTime_FieldValue("10");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-1961")]
        [Description("Step(s) 12 to 19 from the original test method")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Reference Data")]
        [TestProperty("BusinessModule2", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Transport Types")]
        [TestProperty("Screen2", "System Users")]
        public void TransportTypes_RD_UITestMethod004()
        {
            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();

            #region System User

            var newUserName = "TTU_" + currentDateTimeString;
            var newUserFirstName = "TransportType";
            var newUserLastName = "User" + currentDateTimeString;

            var newUserId = commonMethodsDB.CreateSystemUserRecord(newUserName, newUserFirstName, newUserLastName, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Transport Type

            var transportTypeId = dbHelper.transportType.GetTransportTypeByName("Car").First();

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Transport Types")
                .SelectFilterInsideOptGroup("1", "Speed (kmph)")
                .SelectFilterInsideOptGroup("1", "Average Journey Time (minutes), between bookings")
                .SelectFilterInsideOptGroup("1", "Average Journey Time (minutes), to / from home");

            #endregion

            #region Step 13

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Transport Types")
                .ClickReferenceDataMainHeader("Care Provider Transport Availability")
                .ClickReferenceDataElement("Transport Types");

            transportTypesPage
                .WaitForTransportTypesPageToLoad()
                .ClickNewRecordButton();

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("transporttype")
                .ClickOnExpandIcon();

            transportTypeRecordPage
                .WaitForTransportTypeRecordPageToLoad()
                .SelectIcon("Bicycle")
                .SelectIcon("Boat")
                .SelectIcon("Bus")
                .SelectIcon("Car")
                .SelectIcon("Flight")
                .SelectIcon("Motorcycle")
                .SelectIcon("Other")
                .SelectIcon("Subway")
                .SelectIcon("Taxi")
                .SelectIcon("Train")
                .SelectIcon("Tram")
                .SelectIcon("Walking")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            transportTypesPage
                .WaitForTransportTypesPageToLoad();

            #endregion

            #region Step 14

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(newUserName)
                .ClickDoNotUseViewFilterCheckbox()
                .ClickSearchButton()
                .OpenRecord(newUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage();

            #endregion

            #region Step 15 and 16

            systemUserRecordPage
                .ValidateAdditionalDemographicsSectionFields()
                .ValidateAlwaysAvailableTransportFieldIsEmpty();

            #endregion

            #region Step 17

            systemUserRecordPage
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Car").TapSearchButton().SelectResultElement(transportTypeId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateAlwaysAvailableTranspotLinkFieldText("Car")
                .InsertAvailableFromDateField("01/01/2000")
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(newUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateAlwaysAvailableTranspotLinkFieldText("Car");

            #endregion

            #region Step 18

            systemUserRecordPage
                .ClickAlwaysAvailableTransportRemoveButton()
                .ClickSaveAndCloseButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .OpenRecord(newUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateAlwaysAvailableTransportFieldIsEmpty();

            #endregion

            #region Step 19

            //already included in step 17 and 18

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

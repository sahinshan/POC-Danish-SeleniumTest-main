using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// Staff Availability data is updated and displayed in all screens instantly without clicking on refresh button.
    /// </summary>
    [TestClass]
    public class StaffAvailability_DataUpdateWithoutRefresh_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid = new Guid("64d2d456-11dc-e611-80d4-0050560502cc"); //Internal
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid;
        private Guid _employmentContractId;
        public Guid environmentid;
        private string _loginUser_Username;
        private string _careProviderStaffRoleType_Name;
        private string _employmentContractName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _loginUser_Username2;
        private Guid _defaultLoginUserID2;
        private Guid _careProviderStaffRoleTypeid2;
        private Guid _employmentContractTypeid2;
        private Guid _employmentContractId2;
        private string _careProviderStaffRoleType_Name2;

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

                #region Business Unit

                _careProviders_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "CW_Admin_WO_Refresh_" + currentTimeString;
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "CWRefresh_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                #endregion Create default system user

                #region Create default system user2

                _loginUser_Username2 = "CW_Admin_Refresh_" + currentTimeString;
                _defaultLoginUserID2 = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username2, "CWRefresh_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                #endregion Create default system user2

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);
                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID2);

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Carer_ACC-4055", "2", null, new DateTime(2020, 1, 1), null);

                _careProviderStaffRoleType_Name = (string)dbHelper.careProviderStaffRoleType.GetCareProviderStaffRoleTypeByID(_careProviderStaffRoleTypeid, "name")["name"];

                #endregion

                #region Care provider staff role type2

                _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Carer_ACC-4136", "2", null, new DateTime(2020, 1, 1), null);

                _careProviderStaffRoleType_Name2 = (string)dbHelper.careProviderStaffRoleType.GetCareProviderStaffRoleTypeByID(_careProviderStaffRoleTypeid2, "name")["name"];

                #endregion

                #region Employment Contract Type

                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 4003 " + currentTimeString, "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region Employment Contract Type2

                _employmentContractTypeid2 = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 4136 " + currentTimeString, "2", null, new DateTime(2020, 1, 1));

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https: //advancedcsg.atlassian.net/browse/ACC-4055

        [TestProperty("JiraIssueID", "ACC-4055")]
        [Description("Login CD as a Care Provider." +
          "Staff Availability data is updated and displayed in all screens instantly without clicking on refresh button.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void StaffAvailability_DataUpdateWithoutRefresh_UITestCases001()
        {
            string currentDate;

            currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");

            #region Step 1 login

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2 navigate to availability and verify schedule availability screen is there after creating record

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToSystemUserSection();

            systemUsersPage
              .WaitForSystemUsersPageToLoad()
              .InsertUserName(_loginUser_Username)
              .ClickSearchButton()
              .OpenRecord(_defaultLoginUserID.ToString());

            systemUserRecordPage
              .WaitForSystemUserRecordPageToLoad()
              .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.");

            #endregion

            #region Step 3 navigate to Staff Availability screens

            _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_defaultLoginUserID,
              new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            #endregion

            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            #region Step 3 verify calendar icon is disabled

            systemUserAvailabilitySubPage
              .ClickRefreshButton();

            DateTime currentDayOfTheWeek = DateTime.Now;

            string firstDayofTheCurrentWeek = datetimeFirstDay1.GetFirstDayOfWeek1(currentDayOfTheWeek).ToString("dd'/'MM'/'yyyy");

            #endregion

            #region Step 4 Updated data should be saved and displayed in the current schedule availability.

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateScheduleAvailabilityAreaDislayed()
              .ClickScheduleAvailabilityCard()
              .ValidateWeek1CycleDateReadonly(true)
              .ValidateWeek1CycleDate(firstDayofTheCurrentWeek)
              .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
              .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
              .WaitForAvailabilityDinamicDialogPopupPageToLoad()
              .ClickAvailabilityButton("Hourly/Overtime");

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickOnSaveButton()
              .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
              .ClickScheduleTransportCard()
              .ClickScheduleAvailabilityCard()
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true);

            #endregion

            #region Step 5 verify in view dairy screen without clicking on refresh button.

            systemUserAvailabilitySubPage
              .ClickViewDiary_ManageAdHocCard()
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true);

            #endregion

            #region Step 6 create Schedule Transport

            systemUserAvailabilitySubPage
              .ClickScheduleTransportCard()
              .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
              .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickOnSaveButton()
              .WaitForSavedOperationToComplete()
              .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true);

            #endregion

            #region Step 7 verify in view dairy screen without clicking on refresh button.

            systemUserAvailabilitySubPage
              .ClickViewDiary_ManageAdHocCard()
              .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true);

            systemUserAvailabilitySubPage
              .ClickScheduleTransportCard()
              .ValidateScheduleTransportAreaDislayed()
              .ClickSpecificSlotToCreateOrEditScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car");

            availabilityDinamicDialogPopup
              .WaitForAvailabilityDinamicDialogPopupPageToLoad()
              .ClickRemoveTimeSlotButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickOnSaveButton();

            #endregion
        }

        #endregion

        #region https: //advancedcsg.atlassian.net/browse/ACC-ACC-4136

        [TestProperty("JiraIssueID", "ACC-4136")]
        [Description("To Test the Refresh behaviour on Staff Availability." +
          "Refresh should be maintained correctly in each screen of staff Availability.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void StaffAvailability_DataUpdateWithRefresh_UITestCases002()
        {
            string currentDate;
            String nextDate = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
            currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");

            #region Step 1 login

            loginPage
              .GoToLoginPage()
              .Login(_loginUser_Username2, "Passw0rd_!", EnvironmentName);

            #endregion

            #region Step 2 verify System user window should be displayed

            mainMenu
              .WaitForMainMenuToLoad()
              .NavigateToSystemUserSection();

            systemUsersPage
              .WaitForSystemUsersPageToLoad()
              .InsertUserName(_loginUser_Username2)
              .ClickSearchButton()
              .OpenRecord(_defaultLoginUserID2.ToString());

            #endregion

            #region Step 3 verify System displays the Staff Availability icons

            systemUserRecordPage
              .WaitForSystemUserRecordPageToLoad()
              .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.");

            #endregion

            #region Step 4 By clicking on refresh button, the data should get refreshed in schedule Availability screen.

            _employmentContractId2 = commonMethodsDB.CreateSystemUserEmploymentContract(_defaultLoginUserID2,
              new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2);

            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId2, "name")["name"];

            systemUserAvailabilitySubPage
              .ClickRefreshButton();

            DateTime currentDayOfTheWeek = DateTime.Now;

            string firstDayofTheCurrentWeek = datetimeFirstDay1.GetFirstDayOfWeek1(currentDayOfTheWeek).ToString("dd'/'MM'/'yyyy");

            #endregion

            #region Step 5 Refresh the schedule availability screen to view the created / edited employment contracts.

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateScheduleAvailabilityAreaDislayed()
              .ClickScheduleAvailabilityCard()
              .ValidateWeek1CycleDateReadonly(true)
              .ValidateWeek1CycleDate(firstDayofTheCurrentWeek)
              .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
              .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
              .WaitForAvailabilityDinamicDialogPopupPageToLoad()
              .ClickAvailabilityButton("Hourly/Overtime");

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickOnSaveButton()
              .WaitForRecordToBeSaved()
              .ClickRefreshButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
              .ClickScheduleTransportCard()
              .ClickScheduleAvailabilityCard()
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true);

            #endregion

            #region Step 5 verify in view dairy screen without clicking on refresh button.

            systemUserAvailabilitySubPage
              .ClickViewDiary_ManageAdHocCard()
              .ClickRefreshButton()
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true);

            #endregion

            #region Step 6 When user clicks on reload the newly changed slot will not be displayed in the current screen..

            systemUserAvailabilitySubPage
              .ClickScheduleAvailabilityCard()
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateScheduleAvailabilityAreaDislayed()
              .ClickScheduleAvailabilityCard()
              .ValidateWeek1CycleDateReadonly(true)
              .ValidateWeek1CycleDate(firstDayofTheCurrentWeek)
              .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
              .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
              .WaitForAvailabilityDinamicDialogPopupPageToLoad()
              .ClickAvailabilityButton("Salaried/Contracted");

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
              .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
              .ValidateDialogText("You have changes made to Schedule Availability, do you want to save these changes?")
              .ValidateReloadButton()
              .ClickOnReloadButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickRefreshButton()
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true);

            #endregion

            #region Step 7 When user clicks on save and reload the newly created slot will be displayed in the current screen

            systemUserAvailabilitySubPage
              .ClickScheduleAvailabilityCard()
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateScheduleAvailabilityAreaDislayed()
              .ClickScheduleAvailabilityCard()
              .ValidateWeek1CycleDateReadonly(true)
              .ValidateWeek1CycleDate(firstDayofTheCurrentWeek)
              .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
              .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
              .WaitForAvailabilityDinamicDialogPopupPageToLoad()
              .ClickAvailabilityButton("Salaried/Contracted");

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
              .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
              .ValidateDialogText("You have changes made to Schedule Availability, do you want to save these changes?")
              .ValidateReloadButton()
              .ClickOnSaveAndReloadButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickRefreshButton()
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
              .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true);

            systemUserAvailabilitySubPage
              .ClickViewDiary_ManageAdHocCard()
              .ClickRefreshButton();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateCreatedAdHocSlot(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName)
                .ValidateCreatedAdHocSlot(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), _employmentContractName);

            #endregion

            #region Step 8 create refresh Schedule Transport

            systemUserAvailabilitySubPage
              .ClickScheduleTransportCard()
              .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
              .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickCarButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickOnSaveButton();

            systemUserAvailabilitySubPage
              .WaitForSavedOperationToComplete()
              .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true);

            systemUserAvailabilitySubPage
              .ClickRefreshButton()
              .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true);

            #endregion

            #region Step 9 update and refresh Schedule Transport without save

            systemUserAvailabilitySubPage
              .ClickScheduleTransportCard()
              .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
              .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(2).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickBicycleButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
              .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
              .ValidateDialogText("You have changes made to Schedule Transport, do you want to save these changes?")
              .ValidateReloadButton()
              .ClickOnReloadButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
              .ValidateAvailableRecordUnderScheduleTransport(nextDate, "bicycle", false);

            #endregion

            #region Step 10 update and refresh Schedule Transport with save

            systemUserAvailabilitySubPage
              .ClickScheduleTransportCard()
              .ValidateScheduleTransportAreaDislayed();

            systemUserAvailabilitySubPage
              .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(2).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage.WaitForApplicantSelectScheduledTransportPopupToLoad(true).ClickBicycleButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
              .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
              .ValidateDialogText("You have changes made to Schedule Transport, do you want to save these changes?")
              .ValidateReloadButton()
              .ClickOnSaveAndReloadButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ValidateAvailableRecordUnderScheduleTransport(currentDate, "car", true)
              .ValidateAvailableRecordUnderScheduleTransport(nextDate, "bicycle", true);

            #endregion

            #region Step 11 Updated data of (schedule availability and transport) should be displayed correctly in the View Dairy screen without clicking on refresh button.

            systemUserAvailabilitySubPage
              .ClickViewDiary_ManageAdHocCard();

            systemUserAvailabilitySubPage
              .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate, 2, "car")
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(nextDate, 2, "bicycle");

            #endregion

            #region Step 12 By clicking on refresh button, the data should get refreshed in View dairy screen..

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard()
                .ClickRefreshButton();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate, 2, "car")
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(nextDate, 2, "bicycle");

            systemUserAvailabilitySubPage
              .ClickScheduleTransportCard()
              .ValidateScheduleTransportAreaDislayed()
              .ClickSpecificSlotToCreateOrEditScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car");

            availabilityDinamicDialogPopup
              .WaitForAvailabilityDinamicDialogPopupPageToLoad()
              .ClickRemoveTimeSlotButton();

            systemUserAvailabilitySubPage
              .WaitForSystemUserAvailabilitySubPageToLoad()
              .ClickOnSaveButton();

            #endregion
        }

        #endregion

    }

    static class datetimeFirstDay1
    {
        //To Get The First Day of the Week in C#
        public static DateTime GetFirstDayOfWeek1(this DateTime date)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
            var diff = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return date.AddDays(-diff).Date;
        }
    }
}
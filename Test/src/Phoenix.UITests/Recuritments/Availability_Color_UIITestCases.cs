using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// Staff Availability data is updated and displayed in all screens instantly without clicking on refresh button.
    /// </summary>
    [TestClass]
    public class Availability_Color_UIITestCases : FunctionalTest
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
        private string _employmentContractName;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private DateTime firstDayofTheCurrentWeek;
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

                _loginUser_Username = "CW_Admin_Colors_" + currentTimeString;
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "CWColors_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                #endregion Create default system user

                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Carer_ACC-4294", "2", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type

                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 4003 " + currentTimeString, "2", null, new DateTime(2020, 1, 1));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https: //advancedcsg.atlassian.net/browse/ACC-4294

        [TestProperty("JiraIssueID", "ACC-4294")]
        [Description("To Verify Colours assigned with Each Availability Type and Slot should appeared In the Row.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Care Provider Availability")]
        [TestProperty("Screen1", "System Users")]
        [TestProperty("Screen2", "Schedule Availability")]
        public void Verify_Availibility_Colors_UITestCases001()
        {
            #region Step 1 login 

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            #endregion

            #region Step 2 validate availability option names.

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

            #region EmploymentContractId Type

            _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_defaultLoginUserID,
              new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Availability Type

            commonMethodsDB.CreateAvailabilityType("Work-SCHEDULE", 7, false, _careProviders_TeamId, 1, 1, true);
            commonMethodsDB.CreateAvailabilityType("Holiday Hours", 8, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            systemUserAvailabilitySubPage
                .ClickRefreshButton();

            firstDayofTheCurrentWeek = commonMethodsHelper.GetThisWeekFirstMonday();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleAvailabilityAreaDislayed()
                .ClickScheduleAvailabilityCard()
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(firstDayofTheCurrentWeek.ToString("dd'/'MM'/'yyyy"))
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ValidateAvailabilityOptionText("Salaried/Contracted")
                .ValidateAvailabilityOptionText("Hourly/Overtime")
                .ValidateAvailabilityOptionText("Holiday Hours")
                .ValidateAvailabilityOptionText("Work-SCHEDULE");

            #endregion

            #region Step 3 validate Colour is assigned When User Select Availability Type.

            availabilityDinamicDialogPopup
                .ClickAvailabilityButton("Hourly/Overtime");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Salaried/Contracted");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Work-SCHEDULE");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("Holiday Hours");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            #endregion

            #region Step 4 Slots Should be appeared with different colour In the Contract Row When Selected Availability Type.

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
                .ValidateSelectedScheduleAvailabilityColorIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, "02", true)
                .ValidateSelectedScheduleAvailabilityColorIsVisible(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), _employmentContractName, "01", true)
                .ValidateSelectedScheduleAvailabilityColorIsVisible(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"), _employmentContractName, "03", true)
                .ValidateSelectedScheduleAvailabilityColorIsVisible(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"), _employmentContractName, "04", true);

            #endregion

            #region Step 5  when more availability types are created, same colors will be reused

            commonMethodsDB.CreateAvailabilityType("RepeatColor", 9, false, _careProviders_TeamId, 1, 1, true);

            #endregion

            systemUserAvailabilitySubPage
                .ClickRefreshButton()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityButton("RepeatColor");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
                .ValidateSelectedScheduleAvailabilityColorIsVisible(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"), _employmentContractName, "01", true);

        }

        #endregion

    }
}
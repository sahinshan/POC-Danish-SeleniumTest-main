using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Recuritments
{
    /// <summary>
    /// This class contains Automated UI test scripts for Schedule Availability
    /// </summary>
    [TestClass]
    public class ScheduleAvailability_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _defaultLoginUserID;
        private Guid _systemUserID;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _careProviderStaffRoleTypeid2;
        private Guid _employmentContractTypeid;
        private Guid _employmentContractTypeid2;
        private Guid _employmentContractId;
        private Guid _employmentContractId2;
        public Guid environmentid;
        private string _loginUser_Username;
        private string _systemUser_Username;
        private string _careProviderStaffRoleType_Name;
        private string _careProviderStaffRoleType2_Name;
        private string _employmentContractName;
        private string _employmentContract2Name;
        private Guid _availabilityTypeId;

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

                #region Authentication Provider

                authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

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

                _loginUser_Username = "CW_Admin_Test_User_1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "CW", "Admin_Test_User_1", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                #endregion  Create default system user


                #region Team Manager

                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _defaultLoginUserID);

                #endregion

                #region Create SystemUser Record

                _systemUser_Username = "CDV6_13622_User";
                _systemUserID = commonMethodsDB.CreateSystemUserRecord(_systemUser_Username, "CDV6_13622", "User", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

                #endregion  Create SystemUser Record

                #region Care provider staff role type

                _careProviderStaffRoleType_Name = "Carer_CDV6_13622";
                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType_Name, "2", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Care provider staff role type 2

                _careProviderStaffRoleType2_Name = "Cook_CDV6_13622";
                _careProviderStaffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, _careProviderStaffRoleType2_Name, "2", null, new DateTime(2021, 1, 1), null);

                #endregion

                #region Employment Contract Type

                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type 13622", "2", null, new DateTime(2020, 1, 1));

                #endregion

                #region Employment Contract Type 2

                _employmentContractTypeid2 = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type2 13622", "2", null, new DateTime(2020, 1, 1));

                #endregion

                _availabilityTypeId = commonMethodsDB.CreateAvailabilityType("Standard_17618", 25, false, _careProviders_TeamId, 1, 1, true);
                commonMethodsDB.CreateAvailabilityType("Regular_17618", 24, false, _careProviders_TeamId, 1, 1, true);
                commonMethodsDB.CreateAvailabilityType("Overtime_17618", 23, false, _careProviders_TeamId, 1, 1, true);


                #region Delete Employment Contracts for System User

                foreach (var workSheduleId in dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(_systemUserID))
                    dbHelper.userWorkSchedule.DeleteUserWorkSchedule(workSheduleId);

                var employmentContracts = dbHelper.systemUserEmploymentContract.GetBySystemUserId(_systemUserID);

                foreach (var employmentContract in employmentContracts)
                    dbHelper.systemUserEmploymentContract.DeleteSystemUserEmploymentContract(employmentContract);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-17618

        [TestProperty("JiraIssueID", "ACC-3522")]
        [Description("Login CD as a Care Provider." +
            "Verify That  active system user each Employment Contract Name is  represented by a unique row in the grid." +
            "Verify that In The Availabiity Tab The Error Message Should Display As \"Current system user does not have any employment\"")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void ScheduleAvailability_UITestCases001()
        {
            #region Step 3

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.");

            #endregion

            #region Step 2

            #region Employment Contract 

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                    new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            if (_employmentContractId == Guid.Empty)
            {
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            }

            _employmentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                    new DateTime(2021, 1, 1), _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2);

            if (_employmentContractId2 == Guid.Empty)
            {
                _employmentContractId2 = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType2_Name + " - " + new DateTime(2021, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            }
            #endregion

            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            _employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId2, "name")["name"];

            systemUserAvailabilitySubPage
                .ClickRefreshButton();

            DateTime currentDayOfTheWeek = DateTime.Now;
            string firstDayofTheCurrentWeek = datetimeext.GetFirstDayOfWeek(currentDayOfTheWeek).ToString("dd'/'MM'/'yyyy");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleAvailabilityAreaDislayed()
                .ClickScheduleAvailabilityCard()
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(firstDayofTheCurrentWeek)
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContract2Name.ToUpper());


            #endregion

            #region Step 4, 5

            // deprecated

            #endregion

            #region Step 6

            // Same as step 2

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-3523")]
        [Description("Login CD as a Care Provider." +
            "Verify That  All contracts that are active based on current date should be displayed Under Schedule Availability." +
            "Verify That Inactive(Ended) Contracts Is displayed Under Schedule Availability When End date Is Entered In Past." +
            "Verify That Active Contracts Is displayed Under Schedule Availability When End date Is Entered In Future." +
            "Verify That  Contracts Which are Not Started Is displayed Under Schedule Availability When Start date and End date are Entered In Future." +
            "Verify That Ended(Inactive) Contracts are displayed From Current Schedule Screen When Clicked on Refresh Button." +
            "Verify That New(Active) Contracts are displayed In Current Schedule Screen When Clicked on Refresh Button.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("BusinessModule2", "Care Worker Contracts")]
        [TestProperty("Screen1", "Schedule Availability")]
        [TestProperty("Screen2", "System User Employment Contracts")]
        public void ScheduleAvailability_UITestCases002()
        {
            #region Step 7

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.Date, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, null);

            if (_employmentContractId == Guid.Empty)
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            _employmentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID, DateTime.Now.AddDays(-6), _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2, null);

            if (_employmentContractId2 == Guid.Empty)
                _employmentContractId2 = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType2_Name + " - " + new DateTime(2021, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            _employmentContract2Name = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId2, "name")["name"];

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .ValidateRecordCellText(_employmentContractId2.ToString(), 6, "Active")
                .ValidateRecordCellText(_employmentContractId.ToString(), 6, "Active");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ValidateScheduleAvailabilityAreaDislayed()
                .ClickScheduleAvailabilityCard()
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContractName.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContract2Name.ToUpper());

            #endregion

            #region Step 8

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .OpenRecord(_employmentContractId2.ToString());

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertEndDate(DateTime.Now.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .InsertEndTime("01:00")
                .ClickSaveButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Ending the Contract will deallocate Diary and Schedule Bookings that are after the supplied End Date. This action cannot be undone, do you wish to continue?").TapOKButton();

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ClickBackButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .ValidateRecordCellText(_employmentContractId2.ToString(), 6, "Ended");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateCreateScheduleAvailabilityButton_Monday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(_employmentContract2Name.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(_employmentContract2Name.ToUpper());
            #endregion

            #region Step 9

            Guid contract_Step9 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                    DateTime.Now.AddDays(-2), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, DateTime.Now.AddDays(10));
            string contractName_Step9 = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(contract_Step9, "name")["name"];

            systemUserAvailabilitySubPage
                .ClickOnBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .ValidateRecordCellText(contract_Step9.ToString(), 6, "Active");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateCreateScheduleAvailabilityButton_Monday(contractName_Step9.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(contractName_Step9.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(contractName_Step9.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(contractName_Step9.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(contractName_Step9.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(contractName_Step9.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(contractName_Step9.ToUpper());

            #endregion

            #region Step 10

            Guid contract_Step10 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                    DateTime.Now.AddDays(5), _careProviderStaffRoleTypeid2, _careProviders_TeamId, _employmentContractTypeid2, DateTime.Now.AddDays(15));
            string contractName_Step10 = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(contract_Step10, "name")["name"];

            systemUserAvailabilitySubPage
                .ClickOnBackButton();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToEmploymentContractsSubPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserEmploymentContractsPageToLoad()
                .ClickSearchButton()
                .ValidateRecordCellText(contract_Step10.ToString(), 6, "Not Started");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateCreateScheduleAvailabilityButton_Monday(contractName_Step10.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Tuesday(contractName_Step10.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Wednesday(contractName_Step10.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Thursday(contractName_Step10.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Friday(contractName_Step10.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Saturday(contractName_Step10.ToUpper())
                .ValidateCreateScheduleAvailabilityButton_Sunday(contractName_Step10.ToUpper());

            #endregion

            #region Step 11
            // Same as Step 8
            #endregion

            #region Step 12
            // Same as Step 6
            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-3524")]
        [Description("Login CD as a Care Provider. Create a new active employee contract." +
            "Navigate To System User > Availability > Update Schedule Availability Data > Click Refresh Button" +
            "Verify That The Prompt should display When user Updated data and Hit the Refresh Button.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void ScheduleAvailability_UITestCases003()
        {

            #region Step 13 

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, null);

            if (_employmentContractId == Guid.Empty)
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(3000);

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButton(currentDayOfTheWeek, 1);

            System.Threading.Thread.Sleep(5000);

            createScheduledAvailabilityPopup
                .WaitForCreateScheduledAvailabilityPopupToLoad(true)
                .ValidateAvailabilityTypeDisplayed("Standard_17618")
                .ValidateAvailabilityTypeDisplayed("Regular_17618")
                .ValidateAvailabilityTypeDisplayed("Overtime_17618")
                .ClickAvailabilityButton("Standard_17618");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("You have changes made to Schedule Availability, do you want to save these changes?")
                .ValidateReloadButton()
                .ValidateSaveAndReloadButton()
                .ClickOnReloadButton();

            Guid availabilityTypeID = commonMethodsDB.CreateAvailabilityType("Night Shift Test13622", 22, false, _careProviders_TeamId, 1, 1, true);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ClickCreateScheduleAvailabilityButton(currentDayOfTheWeek, 1);

            createScheduledAvailabilityPopup
                .WaitForCreateScheduledAvailabilityPopupToLoad(true)
                .ValidateAvailabilityTypeDisplayed("Standard_17618")
                .ValidateAvailabilityTypeDisplayed("Regular_17618")
                .ValidateAvailabilityTypeDisplayed("Night Shift Test13622")
                .ValidateAvailabilityTypeDisplayed("Overtime_17618");

            #endregion

        }


        [TestProperty("JiraIssueID", "ACC-3525")]
        [Description("Login CD as a Care Provider. Create a new active employee contract." +
           "Navigate To System User > Availability >Validate the Create Future schedule tab is not visible" + "Update the avialability and click on Save button " +
            "Validate the Create Future schedule tab is visible")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void ScheduleAvailability_UITestCases004()
        {
            #region Step 16

            _employmentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserID,
                    DateTime.Now, _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid, null);

            if (_employmentContractId == Guid.Empty)
                _employmentContractId = dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByName(_careProviderStaffRoleType_Name + " - " + new DateTime(2020, 1, 1).ToString("dd'/'MM'/'yyyy")).FirstOrDefault();

            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            string currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickRefreshButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCreateFutureScheduleTab(false);

            System.Threading.Thread.Sleep(3000);

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButton(currentDayOfTheWeek, 1);

            System.Threading.Thread.Sleep(5000);

            createScheduledAvailabilityPopup
                .WaitForCreateScheduledAvailabilityPopupToLoad(true)
                .ValidateAvailabilityTypeDisplayed("Standard_17618")
                .ValidateAvailabilityTypeDisplayed("Regular_17618")
                .ValidateAvailabilityTypeDisplayed("Overtime_17618")
                .ClickAvailabilityButton("Standard_17618");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton();

            System.Threading.Thread.Sleep(5000);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCreateFutureScheduleTab(true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-3982

        [TestProperty("JiraIssueID", "ACC-3983")]
        [Description("test steps for original test ACC-3983")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void ScheduleAvailability_UITestCases005()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.")
                .ClickDeleteSignIcon();

            systemUserAvailabilitySubPage
                .ClickRefreshButton()
                .ValidateSystemUserWarningMessage("Current system user does not have any employment contracts.");

            #region Employment Contract 

            _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            #endregion

            systemUserAvailabilitySubPage
                .ClickRefreshButton();

            DateTime currentDayOfTheWeek = DateTime.Now;
            string firstDayofTheCurrentWeek = datetimeext.GetFirstDayOfWeek(currentDayOfTheWeek).ToString("dd'/'MM'/'yyyy");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateWarningMessageNotDisplay()
                .ValidateScheduleAvailabilityAreaDislayed()
                .ClickScheduleAvailabilityCard()
                .ValidateCurrentScheduleIsVisible(true)
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(firstDayofTheCurrentWeek);

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("You have changes made to Schedule Availability, do you want to save these changes?")
                .ValidateCloseButton()
                .ValidateReloadButton()
                .ValidateSaveAndReloadButton()
                .ClickOnCloseButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("You have changes made to Schedule Availability, do you want to save these changes?")
                .ValidateCloseButton()
                .ValidateReloadButton()
                .ValidateSaveAndReloadButton()
                .ClickOnReloadButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreDisabled()
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, false);

            systemUserAvailabilitySubPage
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreDisabled()
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("You have changes made to Schedule Availability, do you want to save these changes?")
                .ValidateCloseButton()
                .ValidateReloadButton()
                .ValidateSaveAndReloadButton()
                .ClickOnSaveAndReloadButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreDisabled()
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true)
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), _employmentContractName, true);

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4048

        [TestProperty("JiraIssueID", "ACC-4047")]
        [Description("test steps for original test ACC-4047")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Appointment Scheduler")]
        [TestProperty("Screen1", "Schedule Availability")]
        public void ScheduleAvailability_UITestCases006()
        {
            #region Employment Contract 

            _employmentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserID, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            _employmentContractName = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_employmentContractId, "name")["name"];

            #endregion

            #region System Setting (FutureScheduleAvailabilityEnabled)

            var FutureScheduleAvailabilityEnabledId = dbHelper.systemSetting.GetSystemSettingIdByName("FutureScheduleAvailabilityEnabled")[0];
            dbHelper.systemSetting.UpdateSystemSettingValue(FutureScheduleAvailabilityEnabledId, "true");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickSystemSettingLink();

            systemSettingsPage
                .WaitForSystemSettingsPageToLoad()
                .InsertQuickSearchText("FutureScheduleAvailabilityEnabled")
                .ClickQuickSearchButton()
                .WaitForSystemSettingsPageToLoad()
                .OpenRecord(FutureScheduleAvailabilityEnabledId.ToString());

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("systemsetting")
                .ClickOnExpandIcon();

            systemSettingRecordPage
                .WaitForSystemSettingRecordPageToLoad()
                .ValidateNameFieldValue("FutureScheduleAvailabilityEnabled")
                .ValidateSettingValueFieldText("true");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .InsertUserName(_systemUser_Username)
                .ClickSearchButton()
                .OpenRecord(_systemUserID.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentScheduleIsVisible(true)
                .ValidateCreateFutureScheduleTab(false)
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentScheduleIsVisible(true)
                .ValidateCreateFutureScheduleTab(true);

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 2);

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickRemoveTimeSlotButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateSelectedScheduleAvailabilitySlotIsVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), _employmentContractName, false);

            systemUserAvailabilitySubPage
                .ValidateCurrentScheduleIsVisible(true)
                .ValidateCreateFutureScheduleTab(false);

            dbHelper.systemSetting.UpdateSystemSettingValue(FutureScheduleAvailabilityEnabledId, "false");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ValidateCurrentScheduleIsVisible(false)
                .ValidateCreateFutureScheduleTab(false)
                .ClickCreateScheduleAvailabilityButtonByDate(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, _employmentContractName.ToUpper());

            availabilityDinamicDialogPopup
                .WaitForAvailabilityDinamicDialogPopupPageToLoad()
                .ClickAvailabilityRegularButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateCurrentScheduleIsVisible(false)
                .ValidateCreateFutureScheduleTab(false);

            dbHelper.systemSetting.UpdateSystemSettingValue(FutureScheduleAvailabilityEnabledId, "true");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickRefreshButton()
                .ValidateCurrentScheduleIsVisible(true)
                .ValidateCreateFutureScheduleTab(true);

        }

        #endregion

    }

    static class datetimeext
    {
        //To Get The First Day of the Week in C#
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            var culture = System.Globalization.CultureInfo.GetCultureInfo("en-GB");
            var diff = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;

            return date.AddDays(-diff).Date;
        }
    }

}

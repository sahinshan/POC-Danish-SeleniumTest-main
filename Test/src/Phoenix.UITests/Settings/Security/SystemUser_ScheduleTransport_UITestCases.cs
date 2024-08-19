using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.Security
{
    [TestClass]
    public class SystemUser_ScheduleTransport_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid authenticationproviderid;
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _employmentContractTypeid;
        public Guid environmentid;
        private string tenantName;
        private string _loginUser_Username;
        private Guid _loginUserID;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
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

                #endregion

                #region Team

                _careProviders_TeamId = commonMethodsDB.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region Create default system user

                _loginUser_Username = "Schedule_Transport_User_1";
                _loginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUser_Username, "Schedule Transport", "User", "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
                dbHelper.team.UpdateTeamManager(_careProviders_TeamId, _loginUserID);

                #endregion

                #region Employment Contract Type

                _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_careProviders_TeamId, "Employment Contract Type Schedule", "2", null, new DateTime(2020, 1, 1));

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-4085

        [TestProperty("JiraIssueID", "ACC-4084")]
        [Description("test steps for original test ACC-4084")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases001()
        {
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "ACC-4084_1_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "ACC-4084_1", "LN_" + currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var systemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(systemUserId, "fullname")["fullname"];

            #endregion 

            #region Create System User

            var systemUserName2 = "ACC-4084_2_" + currentTimeString;
            var systemUserId2 = commonMethodsDB.CreateSystemUserRecord(systemUserName2, "ACC-4084_2", "LN_" + currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion 

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);
            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId2, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            var transportTypeCar = commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);
            var transportTypeTrain = commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Train", new DateTime(2000, 1, 1), 1, "6", 6);
            var transportTypeBus = commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Bus", new DateTime(2000, 1, 1), 1, "5", 5);
            var transportTypeBoat = commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Boat", new DateTime(2000, 1, 1), 1, "11", 11);
            var transportTypeWalking = commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Walking", new DateTime(2000, 1, 1), 1, "4", 4);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(systemUserFullName)
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Train")
                .TapSearchButton()
                .ValidateResultElementPresent(transportTypeTrain.ToString())

                .TypeSearchQuery("Bus")
                .TapSearchButton()
                .ValidateResultElementPresent(transportTypeBus.ToString())

                .TypeSearchQuery("Boat")
                .TapSearchButton()
                .ValidateResultElementPresent(transportTypeBoat.ToString())

                .TypeSearchQuery("Walking")
                .TapSearchButton()
                .ValidateResultElementPresent(transportTypeWalking.ToString())

                .TypeSearchQuery("Car")
                .TapSearchButton()
                .ValidateResultElementPresent(transportTypeCar.ToString())
                .SelectResultElement(transportTypeCar.ToString());

            #endregion

            #region Step 3

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveButton();
            System.Threading.Thread.Sleep(1000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard()
                .ValidateInfoAreaMessage("You have already selected that you always have access to the transportation type: CAR");

            #endregion

            #region Step 4 & 5

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName2)
                .ClickSearchButton()
                .OpenRecord(systemUserId2);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
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

            #region Step 6

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Train")
                .TapSearchButton()
                .ValidateResultElementPresent(transportTypeTrain.ToString())
                .SelectResultElement(transportTypeTrain.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveButton();

            #endregion

            #region Step 7 & 8

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage(systemUserName2 + " already has a transport schedule saved. It is not possible to have an always available transport and a transport schedule. Do you wish to continue the save and end the schedule transport?")
                .TapCancelButton();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage(systemUserName2 + " already has a transport schedule saved. It is not possible to have an always available transport and a transport schedule. Do you wish to continue the save and end the schedule transport?")
                .TapOKButton();

            #endregion

            #region Step 9

            System.Threading.Thread.Sleep(3000);
            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard()
                .ValidateInfoAreaMessage("You have already selected that you always have access to the transportation type: TRAIN");

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateSystemUserWarningMessage("You have already selected that you always have access to the transportation type: TRAIN")
                .ValidateWaringMessageSignIcons();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4162

        [TestProperty("JiraIssueID", "ACC-533")]
        [Description("test steps for original test ACC-533")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases002()
        {

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "ACC-533_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "ACC-533a_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);
            var systemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(systemUserId, "fullname")["fullname"];

            #endregion 

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            var transportTypeCar = commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(systemUserFullName)
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Car")
                .TapSearchButton()
                .SelectResultElement(transportTypeCar.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard()
                .ValidateInfoAreaMessage("You have already selected that you always have access to the transportation type: CAR");

            systemUserAvailabilitySubPage
                .ValidateScheduleTransportDayOfWeekAreaVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), false)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 1, false);

            #endregion

            #region Step 3

            /*
             * Step 2 and Step 3 are same as they verify whether appropriate message with selected Transport type is rendered and the existing Scheduled Transport grid should be empty.
             */

            #endregion

            #region Step 4

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateSystemUserWarningMessage("You have already selected that you always have access to the transportation type: CAR")
                .ValidateWaringMessageSignIcons();

            #endregion

            #region Step 5

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ClickAlwaysAvailableTransportRemoveButton()
                .ClickSaveButton()
                .WaitForSystemUserRecordPageToLoad()
                .ValidateAlwaysAvailableTransportFieldIsEmpty();

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard()
                .ValidateWeek1CycleDateIsDisplayed(true)
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(commonMethodsHelper.GetDayOfWeek(DateTime.Now.Date, DayOfWeek.Monday).ToString("dd'/'MM'/'yyyy"))
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 1, true);

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButtonByDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTrainButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "train");

            #endregion

            #region Step 6

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToDetailsPage()
                .ValidateSystemUserRecordTitle(systemUserFullName)
                .ClickAlwaysAvailableTransportLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Car")
                .TapSearchButton()
                .SelectResultElement(transportTypeCar.ToString());

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickSaveButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage(systemUserName + " already has a transport schedule saved. It is not possible to have an always available transport and a transport schedule. Do you wish to continue the save and end the schedule transport?")
                .TapOKButton();

            System.Threading.Thread.Sleep(2000);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ValidateAlwaysAvailableTranspotLinkFieldText("Car");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard()
                .ValidateInfoAreaMessage("You have already selected that you always have access to the transportation type: CAR");

            systemUserAvailabilitySubPage
                .ValidateScheduleTransportDayOfWeekAreaVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), false)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 1, false)
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "train", false); ;

            #endregion

            #region Step 7

            systemUserAvailabilitySubPage
                .ValidateWeek1CycleDateIsDisplayed(false);

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateSystemUserWarningMessage("You have already selected that you always have access to the transportation type: CAR")
                .ValidateWaringMessageSignIcons();

            #endregion

            #region Step 8

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ClickRefreshButton()
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateSystemUserWarningMessage("You have already selected that you always have access to the transportation type: CAR");

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4163

        [TestProperty("JiraIssueID", "ACC-4164")]
        [Description("test steps for original test ACC-4164")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases003()
        {
            var currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            var futureDate = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");

            #region Create System User

            var systemUserName = "ACC-4164_1_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "ACC-4164_1", "LN_" + currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Option Set & Value

            var optionSetID = dbHelper.optionSet.GetOptionSetIdByName("TransportTypeIcon")[0];

            var Other_OptionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetID, "Other").FirstOrDefault();
            var Other_OptionSetValueCode = (string)dbHelper.optionsetValue.GetOptionsetValueByID(Other_OptionSetValueId, "code")["code"];

            #endregion

            #region Transport Type

            var transportTypeOther_Name = "ABCDEFGHIJKLMNOPQRSTUVWXYZ_1234567890_0987654321_ZYXWVUTSRQPONMLKJIHGFEDCBA_1234567890_0987654321_TESTDATA"; // more than 100 character
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, transportTypeOther_Name, new DateTime(2000, 1, 1), 1, "1", 12);

            #endregion

            #region Steps

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateScheduleTransportAreaDislayed()
                .ClickCreateScheduleTransportButtonByDate(currentDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible(transportTypeOther_Name)
                .ValidateTransportationTypeIconIsVisible(transportTypeOther_Name)
                .ClickTransportTypeButton(transportTypeOther_Name);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateTooltipOnMouseHover(currentDate, Other_OptionSetValueCode, "" + transportTypeOther_Name + ": 09:00 – 17:00");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButtonByDate(futureDate, 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateTooltipOnMouseHover(futureDate, "car", "Car: 09:00 – 17:00")

                .ClickOnSaveButton()
                .WaitForRecordToBeSaved();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateViewDiary_ManageAdHocAreaDislayed()
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate, Other_OptionSetValueCode, true)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(futureDate, "car", true)

                .ValidateTooltipOnMouseHover(currentDate, Other_OptionSetValueCode, "" + transportTypeOther_Name + ": 09:00 – 17:00")
                .ValidateTooltipOnMouseHover(futureDate, "car", "Car: 09:00 – 17:00");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-4165

        [TestProperty("JiraIssueID", "ACC-4223")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases004()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter562A", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "Aaustin_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "Aaustin_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Flight", new DateTime(2000, 1, 1), 1, "10", 10);

            #endregion           

            #region Step 6, 7 & 9

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateWeek1CycleDateIsDisplayed(true)
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(commonMethodsHelper.GetDayOfWeek(DateTime.Now.Date, DayOfWeek.Monday).ToString("dd'/'MM'/'yyyy"))
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy"), 1, true);

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickFlightButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "airplane_mode_on");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateWeek1CycleDateIsDisplayed(true)
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(commonMethodsHelper.GetDayOfWeek(DateTime.Now.Date, DayOfWeek.Monday).ToString("dd'/'MM'/'yyyy"))
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.ToString("dd'/'MM'/'yyyy"), 3, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy"), 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy"), 1, true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4224")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases005()
        {
            #region Create System User

            var systemUserName = "AaustinB_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinB_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Step 10, 11, 12 & 13

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateWeek1CycleDateIsDisplayed(true)
                .ValidateWeek1CycleDateReadonly(true)
                .ValidateWeek1CycleDate(commonMethodsHelper.GetDayOfWeek(DateTime.Now.Date, DayOfWeek.Monday).ToString("dd'/'MM'/'yyyy"))
                .ValidateAddWeekButtonVisibility(true)
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ValidateWeekButtonVisibility(1, true)
                .ValidateWeekButtonVisibility(2, true)
                .ValidateWeekButtonVisibility(3, true)
                .ValidateWeekButtonVisibility(4, true)
                .ValidateWeekButtonVisibility(5, true)
                .ValidateWeekButtonVisibility(6, false)
                .ValidateWeekButtonVisibility(12, false)
                .ValidateAddWeekButtonVisibility(true);

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(1)
                .ValidateRemoveWeekButtonVisibility(1, true)
                .ValidateDuplicateWeekButtonVisibility(1, true)
                .ValidateMoveWeekRightButtonVisibility(1, true);

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ValidateWeekButtonVisibility(6, true)
                .ValidateWeekButtonVisibility(7, true)
                .ValidateWeekButtonVisibility(8, true)
                .ValidateWeekButtonVisibility(9, true)
                .ValidateWeekButtonVisibility(10, true)
                .ValidateWeekButtonVisibility(11, true)
                .ValidateWeekButtonVisibility(12, true)
                .ValidateAddWeekButtonVisibility(false);

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(12)
                .ClickRemoveWeekButton(12)
                .ClickWeekExpandButton(2)
                .ValidateRemoveWeekButtonVisibility(2, true)
                .ValidateDuplicateWeekButtonVisibility(2, true)
                .ValidateMoveWeekLeftButtonVisibility(2, true)
                .ValidateMoveWeekRightButtonVisibility(2, true);

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(3)
                .ValidateRemoveWeekButtonVisibility(3, true)
                .ValidateDuplicateWeekButtonVisibility(3, true)
                .ValidateMoveWeekLeftButtonVisibility(3, true)
                .ValidateMoveWeekRightButtonVisibility(3, true);

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(10)
                .ValidateRemoveWeekButtonVisibility(10, true)
                .ValidateDuplicateWeekButtonVisibility(10, true)
                .ValidateMoveWeekLeftButtonVisibility(10, true)
                .ValidateMoveWeekRightButtonVisibility(10, true);

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(11)
                .ValidateRemoveWeekButtonVisibility(11, true)
                .ValidateDuplicateWeekButtonVisibility(11, true)
                .ValidateMoveWeekLeftButtonVisibility(11, true);

            systemUserAvailabilitySubPage
                .ClickAddWeekButton()
                .ClickWeekExpandButton(12)
                .ValidateRemoveWeekButtonVisibility(12, true)
                .ValidateMoveWeekLeftButtonVisibility(12, true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-4226")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases006()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter562C", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "AaustinC_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinC_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Train", new DateTime(2000, 1, 1), 1, "6", 6);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Bus", new DateTime(2000, 1, 1), 1, "5", 5);

            #endregion           

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Train")
                .ValidateTransportationTypeIconIsVisible("Train")
                .ValidateTransportationModeIsVisible("Flight")
                .ValidateTransportationTypeIconIsVisible("Flight")
                .ValidateTransportationModeIsVisible("Car")
                .ValidateTransportationTypeIconIsVisible("Car")
                .ValidateTransportationModeIsVisible("Bus")
                .ValidateTransportationTypeIconIsVisible("Bus")
                .ValidateTransportationModeIsVisible("Walking")
                .ValidateTransportationTypeIconIsVisible("Walking")
                .ValidateTransportationModeIsVisible("Bicycle")
                .ValidateTransportationTypeIconIsVisible("Bicycle")
                .ValidateTransportationModeIsVisible("Motorbike")
                .ValidateTransportationTypeIconIsVisible("Motorbike");

            #endregion

            #region Step 15

            systemUserAvailabilityScheduleTransportPage
                .ClickTransportTypeButton("Bus");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bus")
                .ValidateTooltipOnMouseHover(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 09:00 – 17:00");

            var scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(1, scheduleTransports.Count());

            #endregion

            #region Step 16

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car")
                .ValidateTooltipOnMouseHover(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", "Car: 00:30 – 08:30");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(2, scheduleTransports.Count());

            #endregion

            #region Step 17

            systemUserAvailabilitySubPage
                .DragScheduleTransportSlotRightSlider(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bus", 65)
                .ClickOnSaveButton()
                .WaitForSavedOperationToComplete()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bus", true)
                .ValidateTooltipOnMouseHover(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 09:00 – 17:45");

            systemUserAvailabilitySubPage
                .DragScheduleTransportSlotRightSlider(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bus", -80)
                .ClickOnSaveButton()
                .WaitForSavedOperationToComplete()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bus", true)
                .ValidateTooltipOnMouseHover(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 09:00 – 16:30");

            systemUserAvailabilitySubPage
                .DragScheduleTransportSlotLeftSlider(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", 150)
                .ClickOnSaveButton()
                .WaitForSavedOperationToComplete()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateTooltipOnMouseHover(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", "Car: 02:45 – 08:30");

            systemUserAvailabilitySubPage
                .DragScheduleTransportSlotLeftSlider(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", -80)
                .ClickOnSaveButton()
                .WaitForSavedOperationToComplete()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", true)
                .ValidateTooltipOnMouseHover(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car", "Car: 01:45 – 08:30");

            #endregion

            #region Step 18

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(1).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickCarButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), "car")
                .ValidateTooltipOnMouseHover(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(3, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .DragAndDropTransportAvailabilitySlotFromLeftToRight(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), 2, 2)
                .ClickOnSaveButton()
                .WaitForSavedOperationToComplete()
                .ValidateTooltipOnMouseHover(DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy"), "car", "Car: 16:45 – 17:00");

            #endregion

            #region Step 19

            systemUserAvailabilitySubPage
                .ClickSpecificSlotToCreateOrEditScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "car");

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateTooltipOnMouseHover(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "train", "Train: 01:45 – 08:30");

            #endregion

            #region Step 20

            systemUserAvailabilitySubPage
                .ClickSpecificSlotToCreateOrEditScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "train");

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickRemoveTimeSlotButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateAvailableRecordUnderScheduleTransport(DateTime.Now.ToString("dd'/'MM'/'yyyy"), "train", false)
                .WaitForSystemUserAvailabilitySubPageToLoad();

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(2, scheduleTransports.Count());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4236")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases007()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter562D", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "AaustinD_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinD_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Train", new DateTime(2000, 1, 1), 1, "6", 6);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);

            #endregion

            #region Step 21 to Step 23
            string currentDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            string futureDate = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Train")
                .ValidateTransportationTypeIconIsVisible("Train")
                .ValidateTransportationModeIsVisible("Car")
                .ValidateTransportationTypeIconIsVisible("Car");

            systemUserAvailabilityScheduleTransportPage
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "train", 2)
                .ValidateTooltipOnMouseHover(currentDate, "train", 2, "Train: 09:00 – 17:00");

            var scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(1, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.DayOfWeek.ToString(), 3);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "train", 4)
                .ValidateTooltipOnMouseHover(currentDate, "train", 4, "Train: 17:30 – 23:30");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(2, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .DragAndDropTransportAvailabilitySlotFromLeftToRight(currentDate, 4, 2)
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate, "train", 2)
                .ValidateTooltipOnMouseHover(currentDate, "train", 2, "Train: 09:00 – 23:30");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(1, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(1).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(futureDate, "train", 2)
                .ValidateTooltipOnMouseHover(futureDate, "train", 2, "Train: 09:00 – 17:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(2, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Now.AddDays(1).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(futureDate, "car", 2)
                .ValidateTooltipOnMouseHover(futureDate, "car", 2, "Car: 00:30 – 08:30");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(3, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .DragAndDropTransportAvailabilitySlotFromLeftToRight(futureDate, 4, 2)
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(futureDate, "car", 2)
                .ValidateTooltipOnMouseHover(futureDate, "car", 2, "Car: 00:30 – 08:30")
                .ValidateAvailableRecordUnderScheduleTransport(futureDate, "train", 4)
                .ValidateTooltipOnMouseHover(futureDate, "train", 4, "Train: 08:30 – 17:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(3, scheduleTransports.Count());


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4243")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases008()
        {
            #region Create System User

            var systemUserName = "AaustinE_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinE_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Step 25 to Step 29

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

            string Week3Day1 = "";
            string Week3Day2 = "";
            string Week3Day3 = "";
            string Week3Day4 = "";
            string Week3Day5 = "";
            string Week3Day6 = "";
            string Week3Day7 = "";
            var currentWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Calculate week 1, week 2 and week 3 dates

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    #region If current date is a Sunday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(15).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(17).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(18).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(19).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(20).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");

                    Week3Day1 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week3Day2 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week3Day3 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week3Day4 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week3Day5 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week3Day6 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");
                    Week3Day7 = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");

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

                    Week3Day1 = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");
                    Week3Day2 = DateTime.Now.AddDays(15).ToString("dd'/'MM'/'yyyy");
                    Week3Day3 = DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy");
                    Week3Day4 = DateTime.Now.AddDays(17).ToString("dd'/'MM'/'yyyy");
                    Week3Day5 = DateTime.Now.AddDays(18).ToString("dd'/'MM'/'yyyy");
                    Week3Day6 = DateTime.Now.AddDays(19).ToString("dd'/'MM'/'yyyy");
                    Week3Day7 = DateTime.Now.AddDays(20).ToString("dd'/'MM'/'yyyy");

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
                    Week1Day7 = DateTime.Now.AddDays(20).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");

                    Week3Day1 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");
                    Week3Day2 = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");
                    Week3Day3 = DateTime.Now.AddDays(15).ToString("dd'/'MM'/'yyyy");
                    Week3Day4 = DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy");
                    Week3Day5 = DateTime.Now.AddDays(17).ToString("dd'/'MM'/'yyyy");
                    Week3Day6 = DateTime.Now.AddDays(18).ToString("dd'/'MM'/'yyyy");
                    Week3Day7 = DateTime.Now.AddDays(19).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Wednesday:
                    #region If current date is a Wednesday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(19).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(20).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");

                    Week3Day1 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week3Day2 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");
                    Week3Day3 = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");
                    Week3Day4 = DateTime.Now.AddDays(15).ToString("dd'/'MM'/'yyyy");
                    Week3Day5 = DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy");
                    Week3Day6 = DateTime.Now.AddDays(17).ToString("dd'/'MM'/'yyyy");
                    Week3Day7 = DateTime.Now.AddDays(18).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Thursday:
                    #region If current date is a Thursday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(18).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(19).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(20).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");

                    Week3Day1 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week3Day2 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week3Day3 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");
                    Week3Day4 = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");
                    Week3Day5 = DateTime.Now.AddDays(15).ToString("dd'/'MM'/'yyyy");
                    Week3Day6 = DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy");
                    Week3Day7 = DateTime.Now.AddDays(17).ToString("dd'/'MM'/'yyyy");

                    #endregion
                    break;
                case DayOfWeek.Friday:
                    #region If current date is a Friday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(17).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(18).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(19).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(20).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");

                    Week3Day1 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week3Day2 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week3Day3 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week3Day4 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");
                    Week3Day5 = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");
                    Week3Day6 = DateTime.Now.AddDays(15).ToString("dd'/'MM'/'yyyy");
                    Week3Day7 = DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Saturday:
                    #region If current date is a Saturday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(16).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(17).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(18).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(19).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(20).ToString("dd'/'MM'/'yyyy");

                    Week2Day1 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week2Day2 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week2Day3 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week2Day4 = DateTime.Now.AddDays(5).ToString("dd'/'MM'/'yyyy");
                    Week2Day5 = DateTime.Now.AddDays(6).ToString("dd'/'MM'/'yyyy");
                    Week2Day6 = DateTime.Now.AddDays(7).ToString("dd'/'MM'/'yyyy");
                    Week2Day7 = DateTime.Now.AddDays(8).ToString("dd'/'MM'/'yyyy");

                    Week3Day1 = DateTime.Now.AddDays(9).ToString("dd'/'MM'/'yyyy");
                    Week3Day2 = DateTime.Now.AddDays(10).ToString("dd'/'MM'/'yyyy");
                    Week3Day3 = DateTime.Now.AddDays(11).ToString("dd'/'MM'/'yyyy");
                    Week3Day4 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    Week3Day5 = DateTime.Now.AddDays(13).ToString("dd'/'MM'/'yyyy");
                    Week3Day6 = DateTime.Now.AddDays(14).ToString("dd'/'MM'/'yyyy");
                    Week3Day7 = DateTime.Now.AddDays(15).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                default:
                    break;
            }
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAddWeekButtonVisibility(true)
                .ClickAddWeekButton()
                .ValidateWeekButtonVisibility(1, true)
                .ValidateWeekButtonVisibility(2, true)
                .ClickWeekExpandButton(1)
                .ClickDuplicateWeekButton(1)
                .ValidateWeekButtonVisibility(3, true)
                .ValidateAddWeekButtonVisibility(true);

            systemUserAvailabilitySubPage
                .ClickWeekButton(1)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day1, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day2, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day3, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day4, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day5, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day6, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day7, 1, true);

            systemUserAvailabilitySubPage
                .ClickWeekButton(2)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day1, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day2, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day3, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day4, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day5, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day6, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day7, 1, true);

            systemUserAvailabilitySubPage
                .ClickWeekButton(3)
                .ValidateSlotToCreateScheduleTransportVisible(Week3Day1, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week3Day2, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week3Day3, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week3Day4, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week3Day5, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week3Day6, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week3Day7, 1, true);

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

            systemUserAvailabilitySubPage
                .ClickWeekExpandButton(2)
                .ClickRemoveWeekButton(2)
                .ValidateWeekButtonVisibility(1, true)
                .ValidateWeekButtonVisibility(2, true)
                .ValidateWeekButtonVisibility(3, false)
                .ClickWeekButton(1);

            systemUserAvailabilitySubPage
                .ClickWeekButton(1)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day1, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day2, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day3, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day4, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day5, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day6, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day7, 1, true);

            systemUserAvailabilitySubPage
                .ClickWeekButton(2)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day1, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day2, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day3, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day4, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day5, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day6, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week2Day7, 1, true);

            #region Calculate week 1 and week 12 dates

            string Week12Day1 = "";
            string Week12Day2 = "";
            string Week12Day3 = "";
            string Week12Day4 = "";
            string Week12Day5 = "";
            string Week12Day6 = "";
            string Week12Day7 = "";

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    #region If current date is a Sunday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(78).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(79).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(80).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(81).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(82).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(83).ToString("dd'/'MM'/'yyyy");

                    Week12Day1 = DateTime.Now.AddDays(71).ToString("dd'/'MM'/'yyyy");
                    Week12Day2 = DateTime.Now.AddDays(72).ToString("dd'/'MM'/'yyyy");
                    Week12Day3 = DateTime.Now.AddDays(73).ToString("dd'/'MM'/'yyyy");
                    Week12Day4 = DateTime.Now.AddDays(74).ToString("dd'/'MM'/'yyyy");
                    Week12Day5 = DateTime.Now.AddDays(75).ToString("dd'/'MM'/'yyyy");
                    Week12Day6 = DateTime.Now.AddDays(76).ToString("dd'/'MM'/'yyyy");
                    Week12Day7 = DateTime.Now.AddDays(77).ToString("dd'/'MM'/'yyyy");
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

                    Week12Day1 = DateTime.Now.AddDays(78).ToString("dd'/'MM'/'yyyy");
                    Week12Day2 = DateTime.Now.AddDays(79).ToString("dd'/'MM'/'yyyy");
                    Week12Day3 = DateTime.Now.AddDays(80).ToString("dd'/'MM'/'yyyy");
                    Week12Day4 = DateTime.Now.AddDays(81).ToString("dd'/'MM'/'yyyy");
                    Week12Day5 = DateTime.Now.AddDays(82).ToString("dd'/'MM'/'yyyy");
                    Week12Day6 = DateTime.Now.AddDays(83).ToString("dd'/'MM'/'yyyy");
                    Week12Day7 = DateTime.Now.AddDays(84).ToString("dd'/'MM'/'yyyy");
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
                    Week1Day7 = DateTime.Now.AddDays(83).ToString("dd'/'MM'/'yyyy");

                    Week12Day1 = DateTime.Now.AddDays(77).ToString("dd'/'MM'/'yyyy");
                    Week12Day2 = DateTime.Now.AddDays(78).ToString("dd'/'MM'/'yyyy");
                    Week12Day3 = DateTime.Now.AddDays(79).ToString("dd'/'MM'/'yyyy");
                    Week12Day4 = DateTime.Now.AddDays(80).ToString("dd'/'MM'/'yyyy");
                    Week12Day5 = DateTime.Now.AddDays(81).ToString("dd'/'MM'/'yyyy");
                    Week12Day6 = DateTime.Now.AddDays(82).ToString("dd'/'MM'/'yyyy");
                    Week12Day7 = DateTime.Now.AddDays(12).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Wednesday:
                    #region If current date is a Wednesday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(4).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(82).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(83).ToString("dd'/'MM'/'yyyy");

                    Week12Day1 = DateTime.Now.AddDays(75).ToString("dd'/'MM'/'yyyy");
                    Week12Day2 = DateTime.Now.AddDays(76).ToString("dd'/'MM'/'yyyy");
                    Week12Day3 = DateTime.Now.AddDays(77).ToString("dd'/'MM'/'yyyy");
                    Week12Day4 = DateTime.Now.AddDays(78).ToString("dd'/'MM'/'yyyy");
                    Week12Day5 = DateTime.Now.AddDays(79).ToString("dd'/'MM'/'yyyy");
                    Week12Day6 = DateTime.Now.AddDays(80).ToString("dd'/'MM'/'yyyy");
                    Week12Day7 = DateTime.Now.AddDays(81).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Thursday:
                    #region If current date is a Thursday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(3).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(81).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(82).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(83).ToString("dd'/'MM'/'yyyy");

                    Week12Day1 = DateTime.Now.AddDays(74).ToString("dd'/'MM'/'yyyy");
                    Week12Day2 = DateTime.Now.AddDays(75).ToString("dd'/'MM'/'yyyy");
                    Week12Day3 = DateTime.Now.AddDays(76).ToString("dd'/'MM'/'yyyy");
                    Week12Day4 = DateTime.Now.AddDays(77).ToString("dd'/'MM'/'yyyy");
                    Week12Day5 = DateTime.Now.AddDays(78).ToString("dd'/'MM'/'yyyy");
                    Week12Day6 = DateTime.Now.AddDays(79).ToString("dd'/'MM'/'yyyy");
                    Week12Day7 = DateTime.Now.AddDays(80).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Friday:
                    #region If current date is a Friday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(2).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(80).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(81).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(82).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(83).ToString("dd'/'MM'/'yyyy");

                    Week12Day1 = DateTime.Now.AddDays(73).ToString("dd'/'MM'/'yyyy");
                    Week12Day2 = DateTime.Now.AddDays(74).ToString("dd'/'MM'/'yyyy");
                    Week12Day3 = DateTime.Now.AddDays(75).ToString("dd'/'MM'/'yyyy");
                    Week12Day4 = DateTime.Now.AddDays(76).ToString("dd'/'MM'/'yyyy");
                    Week12Day5 = DateTime.Now.AddDays(77).ToString("dd'/'MM'/'yyyy");
                    Week12Day6 = DateTime.Now.AddDays(78).ToString("dd'/'MM'/'yyyy");
                    Week12Day7 = DateTime.Now.AddDays(79).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                case DayOfWeek.Saturday:
                    #region If current date is a Saturday
                    Week1Day1 = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    Week1Day2 = DateTime.Now.AddDays(1).ToString("dd'/'MM'/'yyyy");
                    Week1Day3 = DateTime.Now.AddDays(79).ToString("dd'/'MM'/'yyyy");
                    Week1Day4 = DateTime.Now.AddDays(80).ToString("dd'/'MM'/'yyyy");
                    Week1Day5 = DateTime.Now.AddDays(81).ToString("dd'/'MM'/'yyyy");
                    Week1Day6 = DateTime.Now.AddDays(82).ToString("dd'/'MM'/'yyyy");
                    Week1Day7 = DateTime.Now.AddDays(83).ToString("dd'/'MM'/'yyyy");

                    Week12Day1 = DateTime.Now.AddDays(72).ToString("dd'/'MM'/'yyyy");
                    Week12Day2 = DateTime.Now.AddDays(73).ToString("dd'/'MM'/'yyyy");
                    Week12Day3 = DateTime.Now.AddDays(74).ToString("dd'/'MM'/'yyyy");
                    Week12Day4 = DateTime.Now.AddDays(75).ToString("dd'/'MM'/'yyyy");
                    Week12Day5 = DateTime.Now.AddDays(76).ToString("dd'/'MM'/'yyyy");
                    Week12Day6 = DateTime.Now.AddDays(77).ToString("dd'/'MM'/'yyyy");
                    Week12Day7 = DateTime.Now.AddDays(78).ToString("dd'/'MM'/'yyyy");
                    #endregion
                    break;
                default:
                    break;
            }

            #endregion

            systemUserAvailabilitySubPage
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ClickAddWeekButton()
                .ValidateWeekButtonVisibility(1, true)
                .ValidateWeekButtonVisibility(2, true)
                .ValidateWeekButtonVisibility(3, true)
                .ValidateWeekButtonVisibility(4, true)
                .ValidateWeekButtonVisibility(5, true)
                .ValidateWeekButtonVisibility(6, true)
                .ValidateWeekButtonVisibility(7, true)
                .ValidateWeekButtonVisibility(8, true)
                .ValidateWeekButtonVisibility(9, true)
                .ValidateWeekButtonVisibility(10, true)
                .ValidateWeekButtonVisibility(11, true)
                .ValidateWeekButtonVisibility(12, true)
                .ValidateAddWeekButtonVisibility(false);

            systemUserAvailabilitySubPage
                .ClickWeekButton(1)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day1, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day2, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day3, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day4, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day5, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day6, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week1Day7, 1, true);

            systemUserAvailabilitySubPage
                .ClickWeekButton(12)
                .ValidateSlotToCreateScheduleTransportVisible(Week12Day1, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week12Day2, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week12Day3, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week12Day4, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week12Day5, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week12Day6, 1, true)
                .ValidateSlotToCreateScheduleTransportVisible(Week12Day7, 1, true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4248")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases009()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter562F", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "AaustinF_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinF_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);

            #endregion

            #region Step 37

            DateTime currentDate = DateTime.Now;

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Car")
                .ValidateTransportationTypeIconIsVisible("Car");

            systemUserAvailabilityScheduleTransportPage
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2, "Car: 09:00 – 17:00");

            var scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(1, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(35).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(35).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00");

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateWeekButtonVisibility(2, true)
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate.AddDays(35).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(42).ToString("dd'/'MM'/'yyyy"), 2, "car");


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4253")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases010()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter562G", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "AaustinG_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinG_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Bus", new DateTime(2000, 1, 1), 1, "5", 5);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Train", new DateTime(2000, 1, 1), 1, "6", 6);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Walking", new DateTime(2000, 1, 1), 1, "4", 4);

            #endregion

            #region Step 38

            DateTime currentDate = DateTime.Now;

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Car")
                .ValidateTransportationTypeIconIsVisible("Car");

            systemUserAvailabilityScheduleTransportPage
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2, "Car: 09:00 – 17:00");

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Bus")
                .ValidateTransportationTypeIconIsVisible("Bus");

            systemUserAvailabilityScheduleTransportPage
                .ClickTransportTypeButton("Bus");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "bus", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "bus", 2, "Bus: 00:30 – 08:30");

            var scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(2, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(currentDate.AddDays(1).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Train")
                .ValidateTransportationTypeIconIsVisible("Train");

            systemUserAvailabilityScheduleTransportPage
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "train", 2)
                .ValidateTooltipOnMouseHover(currentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "train", 2, "Train: 09:00 – 17:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(3, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButton(currentDate.AddDays(2).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Car")
                .ValidateTransportationTypeIconIsVisible("Car");

            systemUserAvailabilityScheduleTransportPage
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "car", 2)
                .ValidateTooltipOnMouseHover(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "car", 2, "Car: 09:00 – 17:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(4, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButton(currentDate.AddDays(3).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ValidateTransportationModeIsVisible("Walking")
                .ValidateTransportationTypeIconIsVisible("Walking");

            systemUserAvailabilityScheduleTransportPage
                .ClickTransportTypeButton("Walking");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "walking", 2)
                .ValidateTooltipOnMouseHover(currentDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "walking", 2, "Walking: 09:00 – 17:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(5, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 4, "car")
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 2, "bus")
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 00:30 – 08:30")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), 2, "walking")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "walking", "Walking: 09:00 – 17:00");

            systemUserViewDiaryManageAdHocPage
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), 4, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), 2, "bus")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 00:30 – 08:30")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(1).AddDays(7).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(8).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(2).AddDays(7).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(9).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(3).AddDays(7).ToString("dd'/'MM'/'yyyy"), 2, "walking")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(10).ToString("dd'/'MM'/'yyyy"), "walking", "Walking: 09:00 – 17:00");

            systemUserViewDiaryManageAdHocPage
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), 4, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "bus")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 00:30 – 08:30")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(1).AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(15).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(2).AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(16).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(3).AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "walking")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(17).ToString("dd'/'MM'/'yyyy"), "walking", "Walking: 09:00 – 17:00");

            systemUserViewDiaryManageAdHocPage
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), 4, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), 2, "bus")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 00:30 – 08:30")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(1).AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(22).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(2).AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(23).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(3).AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "walking")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(24).ToString("dd'/'MM'/'yyyy"), "walking", "Walking: 09:00 – 17:00");

            systemUserViewDiaryManageAdHocPage
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), 4, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "bus")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 00:30 – 08:30")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(29).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(29).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(30).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(30).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(31).ToString("dd'/'MM'/'yyyy"), 2, "walking")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(31).ToString("dd'/'MM'/'yyyy"), "walking", "Walking: 09:00 – 17:00");


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4260")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases011()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter562H", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "AaustinH_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinH_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Bus", new DateTime(2000, 1, 1), 1, "5", 5);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Train", new DateTime(2000, 1, 1), 1, "6", 6);

            #endregion

            #region Step 39

            DateTime currentDate = DateTime.Now;
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

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()
                .ClickWeekButton(1)
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day1, "car", 2)
                .ValidateTooltipOnMouseHover(Week1Day1, "car", 2, "Car: 09:00 – 17:00");

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Parse(Week1Day4).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Bus");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day4, "bus", 2)
                .ValidateTooltipOnMouseHover(Week1Day4, "bus", 2, "Bus: 09:00 – 17:00");

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(DateTime.Parse(Week1Day4).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(Week1Day4, "car", 2)
                .ValidateTooltipOnMouseHover(Week1Day4, "car", 2, "Car: 00:30 – 08:30");

            var scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(3, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickWeekButton(2)
                .ClickCreateScheduleTransportButton(DateTime.Parse(Week2Day1).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickWeekButton(2)
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day1, "train", 2)
                .ValidateTooltipOnMouseHover(Week2Day1, "train", 2, "Train: 09:00 – 17:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(4, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickScheduleTransportCard()
                .ClickWeekButton(2)
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButton(DateTime.Parse(Week2Day2).DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickWeekButton(2)
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(Week2Day2, "car", 2)
                .ValidateTooltipOnMouseHover(Week2Day2, "car", 2, "Car: 09:00 – 17:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(5, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(Week1Day1, 2, "car")
                .ValidateTooltipOnMouseHover(Week1Day1, "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(Week1Day4, 2, "car")
                .ValidateTooltipOnMouseHover(Week1Day4, "car", "Car: 00:30 – 08:30")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(Week1Day4, 4, "bus")
                .ValidateTooltipOnMouseHover(Week1Day4, "bus", "Bus: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(Week2Day1, 2, "train")
                .ValidateTooltipOnMouseHover(Week2Day1, "train", "Train: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(Week2Day2, 2, "car")
                .ValidateTooltipOnMouseHover(Week2Day2, "car", "Car: 09:00 – 17:00");

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week1Day1).Date.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week1Day4).Date.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week1Day4).Date.AddDays(7).ToString("dd'/'MM'/'yyyy"), "bus", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week2Day1).Date.AddDays(7).ToString("dd'/'MM'/'yyyy"), "train", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week2Day2).Date.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", false);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week1Day1).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week1Day1).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week1Day4).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week1Day4).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), "car", "Car: 00:30 – 08:30")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week1Day4).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), 4, "bus")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week1Day4).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week2Day1).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week2Day1).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week2Day2).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week2Day2).Date.AddDays(14).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00");

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week1Day1).Date.AddDays(21).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week1Day4).Date.AddDays(21).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week1Day4).Date.AddDays(21).ToString("dd'/'MM'/'yyyy"), "bus", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week2Day1).Date.AddDays(21).ToString("dd'/'MM'/'yyyy"), "train", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(DateTime.Parse(Week2Day2).Date.AddDays(21).ToString("dd'/'MM'/'yyyy"), "car", false);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week1Day1).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week1Day1).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week1Day4).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week1Day4).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), "car", "Car: 00:30 – 08:30")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week1Day4).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), 4, "bus")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week1Day4).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), "bus", "Bus: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week2Day1).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week2Day1).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(DateTime.Parse(Week2Day2).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(DateTime.Parse(Week2Day2).Date.AddDays(28).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4282")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases012()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter562I", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "AaustinI_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinI_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Train", new DateTime(2000, 1, 1), 1, "6", 6);

            #endregion

            #region Step 40

            DateTime currentDate = DateTime.Now;
            DateTime futureDate = DateTime.Now.AddDays(7);


            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickAddWeekButton()
                .ClickWeekButton(1)
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2, "Car: 09:00 – 17:00");

            systemUserAvailabilitySubPage
                .ClickWeekButton(2)
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButton(futureDate.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickWeekButton(2)
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(futureDate.ToString("dd'/'MM'/'yyyy"), "train", 2)
                .ValidateTooltipOnMouseHover(futureDate.ToString("dd'/'MM'/'yyyy"), "train", 2, "Train: 09:00 – 17:00");

            var scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(2, scheduleTransports.Count());

            systemUserAvailabilitySubPage
                .ClickViewDiary_ManageAdHocCard();

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(futureDate.ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(futureDate.ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00");

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(futureDate.AddDays(7).ToString("dd'/'MM'/'yyyy"), "train", false);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(futureDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(futureDate.AddDays(14).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00");

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(currentDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), "car", false)
                .ValidateAvailableRecordUnderViewDiary_ManageAdHoc(futureDate.AddDays(21).ToString("dd'/'MM'/'yyyy"), "train", false);

            systemUserViewDiaryManageAdHocPage
                .WaitForSystemUserViewDiaryManageAdHocPageToLoad()
                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "car")
                .ValidateTooltipOnMouseHover(currentDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), "car", "Car: 09:00 – 17:00")

                .ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(futureDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), 2, "train")
                .ValidateTooltipOnMouseHover(futureDate.AddDays(28).ToString("dd'/'MM'/'yyyy"), "train", "Train: 09:00 – 17:00");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-4285")]
        [Description("test steps for original test ACC-562")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Transport Availability")]
        [TestProperty("Screen1", "Schedule Transport")]
        public void SystemUser_ScheduleTransport_UITestCases013()
        {
            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careProviders_TeamId, "Transporter562J", "2", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Create System User

            var systemUserName = "AaustinJ_" + currentTimeString;
            var systemUserId = commonMethodsDB.CreateSystemUserRecord(systemUserName, "AaustinJ_", currentTimeString, "Passw0rd_!", _careProviders_BusinessUnitId, _careProviders_TeamId, _languageId, authenticationproviderid);

            #endregion

            #region Employment Contract 

            commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2020, 1, 1), _careProviderStaffRoleTypeid, _careProviders_TeamId, _employmentContractTypeid);

            #endregion

            #region Transport Type

            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Car", new DateTime(2000, 1, 1), 1, "1", 1);
            commonMethodsDB.CreateTransportType(_careProviders_TeamId, "Train", new DateTime(2000, 1, 1), 1, "6", 6);

            #endregion

            #region Step 41 to 43

            DateTime currentDate = DateTime.Now;

            loginPage
                .GoToLoginPage()
                .Login(_loginUser_Username, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(systemUserName)
                .ClickSearchButton()
                .OpenRecord(systemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToAvailabilityPage();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickScheduleTransportCard();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 1);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(true)
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2, "Car: 09:00 – 17:00");

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 2);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("You have changes made to Schedule Transport, do you want to save these changes?")
                .ValidateCloseButton()
                .ValidateReloadButton()
                .ValidateSaveAndReloadButton()
                .ClickOnCloseButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "train", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "train", 2, "Train: 09:00 – 17:00");

            #endregion

            #region Step 44

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 2);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("You have changes made to Schedule Transport, do you want to save these changes?")
                .ValidateCloseButton()
                .ValidateReloadButton()
                .ValidateSaveAndReloadButton()
                .ClickOnReloadButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreDisabled()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "car", 2, "Car: 09:00 – 17:00");

            #endregion

            #region Step 45

            systemUserAvailabilitySubPage
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 2);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickTransportTypeButton("Train");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickRefreshButton();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("You have changes made to Schedule Transport, do you want to save these changes?")
                .ValidateCloseButton()
                .ValidateReloadButton()
                .ValidateSaveAndReloadButton()
                .ClickOnSaveAndReloadButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "train", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "train", 2, "Train: 09:00 – 17:00");

            var scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(1, scheduleTransports.Count());

            #endregion

            #region Step 46

            systemUserAvailabilitySubPage
                .DragScheduleTransportSlotRightSlider(currentDate.ToString("dd'/'MM'/'yyyy"), "train", 550)
                .DragScheduleTransportSlotLeftSlider(currentDate.ToString("dd'/'MM'/'yyyy"), "train", -650)
                .ClickOnSaveButton()
                .WaitForRecordToBeSaved()
                .WaitForSystemUserAvailabilitySubPageToLoad();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateAvailableRecordUnderScheduleTransport(currentDate.ToString("dd'/'MM'/'yyyy"), "train", 2)
                .ValidateTooltipOnMouseHover(currentDate.ToString("dd'/'MM'/'yyyy"), "train", 2, "Train: 00:00 – 00:00");

            scheduleTransports = dbHelper.userTransportationSchedule.GetBySystemUsertID(systemUserId);
            Assert.AreEqual(1, scheduleTransports.Count());

            #endregion

            #region Step 47

            systemUserAvailabilitySubPage
                .ValidateSaveButtonsAreDisabled()
                .ClickCreateScheduleTransportButton(currentDate.DayOfWeek.ToString(), 2);

            systemUserAvailabilityScheduleTransportPage
                .WaitForApplicantSelectScheduledTransportPopupToLoad(false)
                .ClickTransportTypeButton("Car");

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled()
                .ClickScheduleAvailabilityCard();

            availabilitySaveChangesDialogPopup
                .WaitForAvailabilitySaveChangesDialogPopupPageToLoad()
                .ValidateDialogText("Your changes made to your transport schedule have not been saved.\r\nTo stay on the page so you can save your changes, click Close.")
                .ClickOnCloseButton();

            systemUserAvailabilitySubPage
                .WaitForSystemUserAvailabilitySubPageToLoad()
                .ValidateSaveButtonsAreEnabled();

            #endregion

            #region Step 48

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.");

            #endregion
        }
        #endregion

    }

}

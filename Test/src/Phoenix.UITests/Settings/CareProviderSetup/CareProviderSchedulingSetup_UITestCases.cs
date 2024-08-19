using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Settings.CareProviderSetup
{
    [TestClass]
    public class CareProviderSchedulingSetup_UITestCases : FunctionalTest
    {
        #region Properties

        private string EnvironmentName;
        private Guid _authenticationProviderId;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _systemUserId;
        private string _systemUserName;

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

                _authenticationProviderId = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareProviders");

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareProviders", null, _businessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");

                #endregion

                #region System User

                _systemUserName = "SchedulingSetup_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "SchedulingSetup", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationProviderId);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-5953

        [TestProperty("JiraIssueID", "ACC-xxxx")]
        [Description("Step(s) 1 to 12 from the original jira test ACC-5952")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Care Provider Scheduling Setup")]
        public void CareProviderSchedulingSetup_ACC_5952_UITestMethod001()
        {
            #region Step 1 to 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSchedulingSetupPage();

            careProviderSchedulingSetupPage
                .WaitForCareProviderSchedulingSetupPageToLoad()
                .ClickNewRecordButton();

            careProviderSchedulingSetupRecordPage
                .WaitForCareProviderSchedulingSetupRecordPopupToLoad()
                .ValidateAllFieldsOfDiaryBookingsValidationSection();

            #endregion

            #region Step 6 to 8

            careProviderSchedulingSetupRecordPage
                .ValidateUseBookingTypeClashActionsSettingForClashesWithScheduleBookings_OptionIsCheckedOrNot(true)

                .ClickUseBookingTypeClashActionsSettingForClashesWithScheduleBookings_Option(false)
                .ValidateUseBookingTypeClashActionsSettingForClashesWithScheduleBookings_OptionIsCheckedOrNot(false)

                .ValidateDoubleBookingActionFieldVisibility(true)
                .ValidateDoubleBookingActionMandatoryFieldVisibility(true);

            #endregion

            #region Step 9 & 10

            careProviderSchedulingSetupRecordPage
                .ClickDoubleBookingAction()
                .ValidateDoubleBookingActionFieldOptionIsPresent("Allow")
                .ValidateDoubleBookingActionFieldOptionIsPresent("Warn Only")
                .ValidateDoubleBookingActionFieldOptionIsPresent("Prevent")

                .SelectDoubleBookingAction("Warn Only")
                .ClickDoubleBookingAction();

            careProviderSchedulingSetupRecordPage
                .ValidateSelectedDoubleBookingActionPickListText("Warn Only");

            #endregion

            #region Step 11

            #region Booking Type 6 -> "Booking (Service User non-care booking)" 

            var _bookingType6Name = "BookingType-006";
            var _bookingType6Id = commonMethodsDB.CreateCPBookingType(_bookingType6Name, 6, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);
            dbHelper.cpBookingType.UpdateIsAbsence(_bookingType6Id, true);

            #endregion

            careProviderSchedulingSetupRecordPage
                .ClickUseBookingTypeClashActionsSettingForClashesWithScheduleBookings_Option(true)
                .ClickUseBookingTypeClashActionsSettingForClashesWithScheduleBookings_Option(false)
                .ClickDefaultBookingTypeForPersonAbsenceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_bookingType6Name)
                .TapSearchButton()
                .SelectResultElement(_bookingType6Id.ToString());

            careProviderSchedulingSetupRecordPage
                .WaitForCareProviderSchedulingSetupRecordPopupToLoad()
                .ClickSaveButton()
                .ValidateDoubleBookingActionErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 12

            careProviderSchedulingSetupRecordPage
                .MouseHoverOnYesOptionOfUseBookingTypeClashActionsSettingForClashesWithScheduleBookings()
                .ValidateYesOptionToolTipText("Do you want to use the 'Booking Type: Clash Actions' setting for clashes with schedule bookings? If No, please specify the double-booking action for diary bookings clashing with schedule bookings?")

                .MouseHoverOnNoOptionOfUseBookingTypeClashActionsSettingForClashesWithScheduleBookings()
                .ValidateNoOptionToolTipText("Do you want to use the 'Booking Type: Clash Actions' setting for clashes with schedule bookings? If No, please specify the double-booking action for diary bookings clashing with schedule bookings?");

            #endregion

        }

        #endregion
    }
}
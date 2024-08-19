using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Providers
{
    [TestClass]
    public class ProviderSchedule_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private string _teamName;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private string _defaultSystemUserFullName;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _systemUserFullname;
        private string _providerName;
        private Guid _providerId;
        private TimeZone _localZone;
        private string _tenantName;
        private string EnvironmentName;
        private Guid cPSchedulingSetupId;
        private string _dateSuffix = DateTime.Now.ToString("yyyyMMdd");
        private string _currentDateTimeSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                _defaultSystemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];
                _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PS BU 1");

                #endregion

                #region Team

                _teamName = "PS T " + _dateSuffix;
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907643", "PST" + _dateSuffix + "@careworkstempmail.com", "ProviderSchedule Team " + _dateSuffix, "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Provider Schedule User1

                _systemUsername = "ProviderScheduleUser" + _dateSuffix;
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ProviderScheduleUser", "User" + _dateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);
                _systemUserFullname = "ProviderScheduleUser User" + _dateSuffix;

                #endregion

                #region Provider 1

                _providerName = "Schedule Provider_" + _currentDateTimeSuffix;
                _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId, 12, true); // Training Provider

                #endregion

                #region Care Provider Scheduling Setup

                cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 4); //Check and Offer Create

                // Update Delete Reason Required Schedule 
                dbHelper.cPSchedulingSetup.UpdateDeleteReasonRequiredSchedule(cPSchedulingSetupId, true);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid availabilityTypeId)
        {
            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            for (int i = 0; i < 7; i++)
            {
                var workScheduleDate = DateTime.Now.AddDays(i).Date;

                switch (workScheduleDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Monday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Tuesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Wednesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Thursday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Friday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Saturday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, availabilityTypeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    default:
                        break;
                }
            }
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-5084

        [TestProperty("JiraIssueID", "ACC-4765")]
        [Description("Step(s) 1 to 4 from the original jira test ACC-4765")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestMethod001()
        {
            #region Providers

            commonMethodsDB.CreateProvider("Provider_2_" + _currentDateTimeSuffix, _teamId, 12, true);
            commonMethodsDB.CreateProvider("Provider_3_" + _currentDateTimeSuffix, _teamId, 12, true);
            commonMethodsDB.CreateProvider("Provider_4_" + _currentDateTimeSuffix, _teamId, 12, true);
            commonMethodsDB.CreateProvider("Provider_5_" + _currentDateTimeSuffix, _teamId, 12, true);
            commonMethodsDB.CreateProvider("Provider_6_" + _currentDateTimeSuffix, _teamId, 12, true);
            commonMethodsDB.CreateProvider("Provider_7_" + _currentDateTimeSuffix, _teamId, 12, true);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            var ProviderCollections = dbHelper.provider.GetAllProviderSortByNameAscendingOrder();

            var provider1Name = (string)(dbHelper.provider.GetProviderByID(ProviderCollections[0], "name"))["name"];
            var provider2Name = (string)(dbHelper.provider.GetProviderByID(ProviderCollections[1], "name"))["name"];
            var provider3Name = (string)(dbHelper.provider.GetProviderByID(ProviderCollections[2], "name"))["name"];
            var provider4Name = (string)(dbHelper.provider.GetProviderByID(ProviderCollections[3], "name"))["name"];
            var provider5Name = (string)(dbHelper.provider.GetProviderByID(ProviderCollections[4], "name"))["name"];

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickProviderDropdownList()
                .ValidateSortingListOfProvider(1, provider1Name)
                .ValidateSortingListOfProvider(2, provider2Name)
                .ValidateSortingListOfProvider(3, provider3Name)
                .ValidateSortingListOfProvider(4, provider4Name)
                .ValidateSortingListOfProvider(5, provider5Name)
                .ClickOnProviderWithoutSearching(ProviderCollections[0]);//because of a screen overlay we need to select 1 provider to remove it. otherwise the screen interation will not be possible

            #endregion

            #region Step 3

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address");

            #endregion

            #region Step 4

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapDetailsTab();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickEnableSchedulingRadioButton(false)
                .ClickSaveAndCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickProviderDropdownList()
                .ValidateProviderIsPresent(_providerName + " - No Address", false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5101

        [TestProperty("JiraIssueID", "ACC-4770")]
        [Description("Step(s) 1 to 2 & 5 to 8 from the original jira test ACC-4770")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestMethod002()
        {
            #region Provider

            // Config for Step 5
            var ParentProviderName_1 = "Parent_Provider_1_" + _currentDateTimeSuffix;
            var ParentProviderId_1 = commonMethodsDB.CreateProvider(ParentProviderName_1, _teamId, 3, false); // Hospital - Enable Scheduling = false

            var ChildProviderName_1 = "Child_Provider_1_" + _currentDateTimeSuffix;
            var ChildProviderId_1 = commonMethodsDB.CreateProvider(ChildProviderName_1, _teamId, 3, false); // Hospital - Enable Scheduling = false
            dbHelper.provider.UpdateParentProvider(ChildProviderId_1, ParentProviderId_1);

            // Config for Step 6 & Step 7
            var ParentProviderName_2 = "Parent_Provider_2_" + _currentDateTimeSuffix;
            var ParentProviderId_2 = commonMethodsDB.CreateProvider(ParentProviderName_2, _teamId, 3, false); // Hospital - Enable Scheduling = false

            var ChildProviderName_2 = "Child_Provider_2_" + _currentDateTimeSuffix;

            // Config for Step 8
            var ChildProviderName_3 = "Child_Provider_3_" + _currentDateTimeSuffix;

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ValidateEnableSchedulingFieldIsVisible()
                .ValidateEnableScheduling_OptionIsCheckedOrNot(false);

            #endregion

            #region Step 3 & 4

            // Not valid for automation. Through automation we can not Active or Inactive BM

            #endregion

            #region Step 5

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(ParentProviderName_1, ParentProviderId_1.ToString())
                .OpenProviderRecord(ParentProviderId_1.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapDetailsTab();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickEnableSchedulingRadioButton(true)
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("This provider is the parent of at least one other provider. Scheduling cannot be enabled for a parent provider.")
                .TapCloseButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            var ParentProviderSchedulingId_1 = (bool)dbHelper.provider.GetProviderByID(ParentProviderId_1, "enablescheduling")["enablescheduling"];
            Assert.AreEqual(false, ParentProviderSchedulingId_1);

            #endregion

            #region Step 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(ParentProviderName_2, ParentProviderId_2.ToString())
                .OpenProviderRecord(ParentProviderId_2.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapDetailsTab();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickEnableSchedulingRadioButton(true)
                .ClickSaveButton();

            System.Threading.Thread.Sleep(1000);
            var ParentProviderSchedulingId_2 = (bool)dbHelper.provider.GetProviderByID(ParentProviderId_2, "enablescheduling")["enablescheduling"];
            Assert.AreEqual(true, ParentProviderSchedulingId_2);

            #endregion

            #region Step 7

            providersRecordPage
                .NavigateToChildProvider();

            providersPage
                .WaitForChildProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForChildProvidersRecordPageToLoad(ParentProviderId_2.ToString())
                .InsertName(ChildProviderName_2)
                .SelectProviderType("Residential Establishment")
                .ClickParentProviderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ParentProviderName_2.ToString())
                .TapSearchButton()
                .SelectResultElement(ParentProviderId_2);

            providersRecordPage
                .WaitForChildProvidersRecordPageToLoad(ParentProviderId_2.ToString())
                .ClickCalculateAnnualLeaveForEmployeesNoRadioButton()
                .ClickSaveButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Scheduling is enabled for the selected parent provider. Providers that have scheduling enabled cannot be the parent of another provider.")
                .TapCloseButton();

            providersRecordPage
                .WaitForChildProvidersRecordPageToLoad(ParentProviderId_2.ToString())
                .ClickBackButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 8

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(ParentProviderName_1, ParentProviderId_1.ToString())
                .OpenProviderRecord(ParentProviderId_1.ToString());

            providerRecordPage
                .WaitForProviderRecordPageToLoad()
                .TapDetailsTab();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .NavigateToChildProvider();

            providersPage
                .WaitForChildProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForChildProvidersRecordPageToLoad(ParentProviderId_1.ToString())
                .InsertName(ChildProviderName_3)
                .SelectProviderType("Residential Establishment")
                .ClickParentProviderLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(ParentProviderName_1.ToString())
                .TapSearchButton()
                .SelectResultElement(ParentProviderId_1);

            providersRecordPage
                .WaitForChildProvidersRecordPageToLoad(ParentProviderId_1.ToString())
                .ClickCalculateAnnualLeaveForEmployeesNoRadioButton()
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);
            var ChildProviderId_3 = dbHelper.provider.GetProviderByName(ChildProviderName_3.ToString())[0];

            providersPage
                .WaitForChildProvidersPageToLoad()
                .ValidateProviderRecordIsPresent(ChildProviderId_3.ToString(), true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5133

        [TestProperty("JiraIssueID", "ACC-4794")]
        [Description("Step(s) 1 to 5 from the original jira test ACC-4794")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestMethod003()
        {
            #region Option Set (RecruitmentDocumentStatus)

            var optionSetName = "CP Express Book on Public Holiday";
            var optionSetId = dbHelper.optionSet.GetOptionSetIdByName(optionSetName)[0];

            #endregion

            #region Optionset Values

            var DoesOccur_OptionsetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Does Occur")[0];

            #endregion

            #region Step 1 to 3

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            //NOTE: Option Set entry is no longer available in the Customizations page.
            //mainMenu
            //    .WaitForMainMenuToLoad()
            //    .NavigateToCustomizationsSection();

            //customizationsPage
            //    .WaitForCustomizationsPageToLoad()
            //    .ClickOptionSetsButton();

            //optionSetsPage
            //    .WaitForOptionSetsPageToLoad()
            //    .InsertQuickSearchText(optionSetName)
            //    .ClickQuickSearchButton()
            //    .ValidateRecordIsPresent(optionSetId.ToString(), true)
            //    .OpenRecord(optionSetId.ToString());

            //optionSetsRecordPage
            //    .WaitForOptionSetsRecordPageToLoad()
            //    .ValidateOptionSetTextValue(optionSetName)
            //    .NavigateToOptionSetValuesPage();

            //optionsetValuesPage
            //    .WaitForOptionsetValuesPageToLoad()
            //    .ValidateOptionSet_DisplayName(DoesOccur_OptionsetValueId.ToString(), "Does Occur")
            //    .ValidateOptionSet_AvailableOption(DoesOccur_OptionsetValueId.ToString(), "Yes");

            #endregion

            #region Step 4

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSchedulingSetupPage();

            careProviderSchedulingSetupPage
                .WaitForCareProviderSchedulingSetupPageToLoad()
                .ClickNewRecordButton();

            careProviderSchedulingSetupRecordPage
                .WaitForCareProviderSchedulingSetupRecordPopupToLoad()
                .ValidateAllFieldsOfBookingsSection()
                .ValidateAllFieldsOfValidationSection()
                .ValidateAllFieldsOfDiaryBookingsValidationSection()
                .ValidateAllFieldsOfContractHoursValidationSection()
                .ValidateAllFieldsOfAutoRefreshSection()
                .ValidateDefaultPublicHolidayForRegularBookingsMandatoryFieldVisibility(true)
                .ValidateSelectedDefaultPublicHolidayForRegularBookingsPickListValue("Does Occur")
                .ClickDrawerCloseButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            #endregion

            #region Step 5

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateOnAPublicHolidayMandatoryFieldVisibility(true)
                .ValidateOnAPublicHolidayDropDownText("Does Occur");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5185

        [TestProperty("JiraIssueID", "ACC-5212")]
        [Description("Step(s) 1 to 6 from the original jira test ACC-4790")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestMethod004()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityType_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _teamId, 1, 1, true);

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5212_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-5212 1";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA2" + _currentDateTimeSuffix;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA2", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityType_StandardId);

            #endregion

            #region Step 1 to 3

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .ValidateNextDueToTakePlaceFieldVisibility()
                .ValidateNextDueToTakePlaceFieldDisabled(true);

            #endregion

            #region Step 4

            createScheduleBookingPopup
                .ValidateBookingTakesPlaceEveryDropDownText("1 week")
                .ValidateNextDueToTakePlaceFieldDisabled(true)

                .SelectBookingTakesPlaceEvery("2 weeks")
                .ValidateBookingTakesPlaceEveryDropDownText("2 weeks")
                .ValidateNextDueToTakePlaceFieldDisabled(false)
                .ValidateNextDueToTakePlaceMandatoryFieldVisibility(true);

            #endregion

            #region Step 5

            var targetYear = todayDate.Year.ToString();
            var targetMonth = todayDate.ToString("MMMM");
            var targetDay = todayDate.Day.ToString();

            var NextDueTargetYear = DateTime.Now.AddDays(14).Year.ToString();
            var NextDueTargetMonth = DateTime.Now.AddDays(14).ToString("MMMM");
            var NextDueTargetDay = DateTime.Now.AddDays(14).Day.ToString();

            var LastOccurrenceTargetYear = DateTime.Now.AddDays(21).Year.ToString();
            var LastOccurrenceTargetMonth = DateTime.Now.AddDays(21).ToString("MMMM");
            var LastOccurrenceTargetDay = DateTime.Now.AddDays(21).Day.ToString();

            createScheduleBookingPopup
                .SelectBookingTakesPlaceEvery("1 week")
                .ClickRosteringTab()
                .SelectBookingType(_bookingTypeName)
                .SetStartDay("Monday")
                .SetEndDay("Monday")
                .SetStartTime("10", "00")
                .SetEndTime("18", "00");

            createScheduleBookingPopup
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectFirstOccurrenceDate(targetYear, targetMonth, targetDay)
                .SelectLastOccurrenceDate(LastOccurrenceTargetYear, LastOccurrenceTargetMonth, LastOccurrenceTargetDay)

                .ValidateNextDueToTakePlaceFieldDisabled(true)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(2000);

            var careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #endregion

            #region Step 6

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(careProviderBookingSchedules[0].ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")
                .ValidateNextDueToTakePlaceFieldDisabled(false)
                .SelectNextDueToTakePlaceDate(NextDueTargetYear, NextDueTargetMonth, NextDueTargetDay)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(2000);

            careProviderBookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderBookingSchedules.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5213")]
        [Description("Step(s) 7 to 9 from the original jira test ACC-4790")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderSchedule_UITestMethod005()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Availability Type

            var _availabilityType_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _teamId, 1, 1, true);

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5213_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-5213 1";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffAName = "StaffA2" + _currentDateTimeSuffix;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffAName, "StaffA2", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityType_StandardId);

            #endregion

            #region Step 7 & 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .SelectBookingType(_bookingTypeName)
                .SetStartDay("Monday")
                .SetEndDay("Monday")
                .SetStartTime("10", "00")
                .SetEndTime("18", "00");

            var targetYear = DateTime.Now.AddDays(14).Year.ToString();
            var targetMonth = DateTime.Now.AddDays(14).ToString("MMMM");
            var targetDay = DateTime.Now.AddDays(14).Day.ToString();

            var Year = DateTime.Now.AddDays(7).Year.ToString();
            var Month = DateTime.Now.AddDays(7).ToString("MMMM");
            var Day = DateTime.Now.AddDays(7).Day.ToString();

            var FutureNextDueTargetYear = DateTime.Now.AddDays(21).Year.ToString();
            var FutureNextDueTargetMonth = DateTime.Now.AddDays(21).ToString("MMMM");
            var FutureNextDueTargetDay = DateTime.Now.AddDays(21).Day.ToString();

            var LastOccurrenceTargetYear = DateTime.Now.AddDays(14).Year.ToString();
            var LastOccurrenceTargetMonth = DateTime.Now.AddDays(14).ToString("MMMM");
            var LastOccurrenceTargetDay = DateTime.Now.AddDays(14).Day.ToString();

            createScheduleBookingPopup
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectBookingTakesPlaceEvery("2 weeks")

                .SelectNextDueToTakePlaceDate(Year, Month, Day);

            System.Threading.Thread.Sleep(1000);
            createScheduleBookingPopup
                .ValidateNextDueToTakePlaceDate(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7).ToString("dd'/'MM'/'yyyy"))
                .SelectFirstOccurrenceDate(targetYear, targetMonth, targetDay);

            System.Threading.Thread.Sleep(1000);
            createScheduleBookingPopup
                .ValidateNextDueToTakePlaceDate("");

            #endregion

            #region Step 8 & 9

            createScheduleBookingPopup
                .SelectNextDueToTakePlaceDate(FutureNextDueTargetYear, FutureNextDueTargetMonth, FutureNextDueTargetDay);

            System.Threading.Thread.Sleep(1000);
            createScheduleBookingPopup
                .ValidateNextDueToTakePlaceDate(commonMethodsHelper.GetThisWeekFirstMonday().AddDays(21).ToString("dd'/'MM'/'yyyy"))
                .SelectFirstOccurrenceDate(Year, Month, Day);

            System.Threading.Thread.Sleep(1000);
            createScheduleBookingPopup
                .SelectLastOccurrenceDate(LastOccurrenceTargetYear, LastOccurrenceTargetMonth, LastOccurrenceTargetDay);

            System.Threading.Thread.Sleep(1000);
            createScheduleBookingPopup
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(2000);

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Last occurrence date cannot be < Next Due to Take Place date")
                .ClickDismissButton_DynamicDialogue();

            createScheduleBookingPopup
                .WaitForOccurrenceTabToLoad();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5310

        [TestProperty("JiraIssueID", "ACC-5393")]
        [Description("Step(s) 1 to 12 from the original jira test ACC-4819")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_4819_UITestMethod001()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Booking Type

            var _bookingTypeName1 = "BTC ACC-5393 1";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "BTC ACC-5393 2";
            var _bookingType2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType2, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5393", "5393", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingType1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing");
            DateTime currentDateTime = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var CreatedOnDate = currentDateTime.ToString("d MMM yyyy, HH:mm");
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingScheduleId, 1);

            #endregion

            #region Step 1 to 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            #endregion

            #region Step 5 & 6

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateHistoryTabIsVisible(true)
                .ClickHistoryTab()
                .WaitForHistoryTabToLoad();

            #endregion

            #region Step 7 & 8

            createScheduleBookingPopup
                .ValidateDetailsOnHistoryTab(1, "Created: " + CreatedOnDate + " " + _defaultSystemUserFullName + "");

            #endregion

            #region Step 9

            createScheduleBookingPopup
                .ClickRosteringTab()
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SelectBookingType(_bookingTypeName2)
                .SetStartTime("08", "00")
                .SetEndTime("09", "00")
                .InsertTextInCommentsTextArea("Changing Booking type and other Value");

            var targetYear = todayDate.Year.ToString();
            var targetMonth = todayDate.ToString("MMMM");
            var targetDay = todayDate.Day.ToString();

            var LastOccurrenceTargetYear = DateTime.Now.AddDays(7).Year.ToString();
            var LastOccurrenceTargetMonth = DateTime.Now.AddDays(7).ToString("MMMM");
            var LastOccurrenceTargetDay = DateTime.Now.AddDays(7).Day.ToString();

            createScheduleBookingPopup
                .ClickOccurrenceTab()
                .WaitForOccurrenceTabToLoad()
                .SelectFirstOccurrenceDate(targetYear, targetMonth, targetDay)
                .SelectLastOccurrenceDate(LastOccurrenceTargetYear, LastOccurrenceTargetMonth, LastOccurrenceTargetDay)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickConfirmButton_WarningDialogue();

            System.Threading.Thread.Sleep(2000);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            #endregion

            #region Step 10 & 11

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickHistoryTab()
                .WaitForHistoryTabToLoad()
                .ExpandHistoryDetailSection(1)
                .ExpandSpecificField("Booking Type")
                .ValidateExpandedFieldIsVisible("Booking Type", true)
                .ValidateCurrentAndPreviousValue("Booking Type", _bookingTypeName2, _bookingTypeName1);

            createScheduleBookingPopup
                .ExpandSpecificField("Comments")
                .ValidateExpandedFieldIsVisible("Comments", true)
                .ValidateCurrentAndPreviousValue("Comments", "Changing Booking type and other Value", "Express Book Testing");

            createScheduleBookingPopup
                .ExpandSpecificField("End Time")
                .ValidateExpandedFieldIsVisible("End Time", true)
                .ValidateCurrentAndPreviousValue("End Time", "09:00", "08:00");

            createScheduleBookingPopup
                .ExpandSpecificField("First Occurrence")
                .ValidateExpandedFieldIsVisible("First Occurrence", true)
                .ValidateCurrentAndPreviousValue("First Occurrence", commonMethodsHelper.GetThisWeekFirstMonday().ToString("dd'/'MM'/'yyyy"), "");

            createScheduleBookingPopup
                .ExpandSpecificField("Last Occurrence")
                .ValidateExpandedFieldIsVisible("Last Occurrence", true)
                .ValidateCurrentAndPreviousValue("Last Occurrence", commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7).ToString("dd'/'MM'/'yyyy"), "");

            createScheduleBookingPopup
                .ExpandSpecificField("Staff")
                .ValidateExpandedFieldIsVisible("Staff", true)
                .ValidateCurrentAndPreviousValue("Staff", "Unassigned", "");

            createScheduleBookingPopup
                .ExpandSpecificField("Start Time")
                .ValidateExpandedFieldIsVisible("Start Time", true)
                .ValidateCurrentAndPreviousValue("Start Time", "08:00", "07:00");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5394")]
        [Description("Step(s) 12 to 16 from the original jira test ACC-4819")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        // Resolved Bug https://advancedcsg.atlassian.net/browse/ACC-5973
        public void ProviderScheduleBooking_ACC_4819_UITestMethod002()
        {
            #region Booking Type

            var _bookingTypeName1 = "BTC ACC-5394 1";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            var _bookingTypeName2 = "BTC ACC-5394 2";
            var _bookingType2 = commonMethodsDB.CreateCPBookingType(_bookingTypeName2, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);
            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType2, false);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5394", "5394", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingType1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing");

            #endregion

            #region Step 12 & 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SelectBookingType(_bookingTypeName2)
                .SetStartTime("08", "00")
                .SetEndTime("09", "00")
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(2000);
            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking edited")
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .InsertTextInCommentsTextArea("Changing Comments")
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(2000);
            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking edited")
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickHistoryTab()
                .WaitForHistoryTabToLoad()
                .ExpandHistoryDetailSection(1)
                .ExpandSpecificField("Comments")
                .ValidateExpandedFieldIsVisible("Comments", true)
                .ValidateCurrentAndPreviousValue("Comments", "Changing Comments", "Express Book Testing");

            #endregion

            #region Step 13

            createScheduleBookingPopup
                .ClickRosteringTab()
                .WaitForEditScheduleBookingPopupPageToLoad()
                .SelectBookingType(_bookingTypeName1)

                .ClickHistoryTab()
                .WaitForHistoryTabToLoad()
                .ClickResetChangesButton()

                .ClickRosteringTab()
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName2);

            #endregion

            #region Step 14

            createScheduleBookingPopup
                .ClickHistoryTab()
                .WaitForHistoryTabToLoad()
                .ClickOnCloseButton()
                .WaitForCreateScheduleBookingPopupClosed();

            #endregion

            #region Step 15

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickHistoryTab()
                .WaitForHistoryTabToLoad()
                .ClickOnDeleteButton();

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .SelectReasonForDeletePicklistOption("Added in error")
                .InsertTextInComments_DeleteBookingDynamicDialogue("Need to delete this.")
                .ClickDeleteButton_DeleteBookingDynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking deleted");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5402

        [TestProperty("JiraIssueID", "ACC-5490")]
        [Description("Step(s) 1 to 12 from the original jira test ACC-4822")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        // Resolved Bug https://advancedcsg.atlassian.net/browse/ACC-5973
        public void ProviderScheduleBooking_ACC_4822_UITestMethod001()
        {
            #region Booking Type

            var _bookingTypeName1 = "BTC ACC-5402 1";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5402", "5402", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingType1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing");

            #endregion


            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSchedulingSetupPage();

            careProviderSchedulingSetupPage
                .WaitForCareProviderSchedulingSetupPageToLoad()
                .OpenRecord(cPSchedulingSetupId);

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpschedulingsetup")
                .ClickOnExpandIcon();

            careProviderSchedulingSetupRecordPage
                .WaitForCareProviderSchedulingSetupRecordPageToLoad()
                .ValidateDeleteReasonRequiredSchedule_OptionIsCheckedOrNot(true);

            #endregion

            #region Step 3 to 6

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateDeleteButtonIsPresent();

            #endregion

            #region Step 7

            createScheduleBookingPopup
                .ClickOnDeleteButton();

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .ValidateReasonForDeleteMandatoryIconVisible(true)
                .ValidateCommentsMandatoryIconVisible_DeleteBookingDialouge(false)
                .ValidateSelectedReasonForDeletePickListOption("Select");

            #endregion

            #region Step 8

            createScheduleBookingPopup
                .SelectReasonForDeletePicklistOption("Added in error")
                .ValidateSelectedReasonForDeletePickListOption("Added in error");

            #endregion

            #region Step 9

            createScheduleBookingPopup
                .ClickCancelButton_DeleteBookingDynamicDialogue()
                .WaitForDeleteBookingDynamicDialoguePopupClosed();

            #endregion

            #region Step 10 & 11

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOnDeleteButton();

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .SelectReasonForDeletePicklistOption("Added in error")
                .InsertTextInComments_DeleteBookingDynamicDialogue("Need to delete this.")
                .ClickDeleteButton_DeleteBookingDynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking deleted");

            #endregion

            #region Step 12

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingScheduleId.ToString(), false);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5491")]
        [Description("Step(s) 13 to 17 from the original jira test ACC-4822")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        // Resolved Bug https://advancedcsg.atlassian.net/browse/ACC-5499
        public void ProviderScheduleBooking_ACC_4822_UITestMethod002()
        {
            #region Booking Type

            var _bookingTypeName1 = "BTC ACC-5402 1";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5402", "5402", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingType1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing");

            #endregion


            #region Step 13 & 14

            #region Update Delete Reason Required Schedule to False

            dbHelper.cPSchedulingSetup.UpdateDeleteReasonRequiredSchedule(cPSchedulingSetupId, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateDeleteButtonIsPresent()
                .ClickOnDeleteButton();

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad(false)
                .ValidateDeleteAlertMessage("Are you sure you want to delete?")
                .ClickCancelButton_DeleteBookingDynamicDialogue()
                .WaitForDeleteBookingDynamicDialoguePopupClosed(false);

            #endregion

            #region Step 15

            createScheduleBookingPopup
                .ClickOnDeleteButton()
                .WaitForDeleteBookingDynamicDialogueToLoad(false)
                .ValidateDeleteAlertMessage("Are you sure you want to delete?")
                .ClickDeleteButton_DeleteBookingDynamicDialogue()
                .WaitForDeleteBookingDynamicDialoguePopupClosed(false);

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking deleted");

            #endregion

            #region Step 16

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingScheduleId.ToString(), false);

            #endregion

            #region Step 17

            dbHelper.cPSchedulingSetup.UpdateDeleteReasonRequiredSchedule(cPSchedulingSetupId, true);

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToSchedulingSetupPage();

            careProviderSchedulingSetupPage
                .WaitForCareProviderSchedulingSetupPageToLoad()
                .OpenRecord(cPSchedulingSetupId);

            drawerDialogPopup
                .WaitForDrawerDialogPopupToLoad("cpschedulingsetup")
                .ClickOnExpandIcon();

            careProviderSchedulingSetupRecordPage
                .WaitForCareProviderSchedulingSetupRecordPageToLoad()
                .ValidateDeleteReasonRequiredSchedule_OptionIsCheckedOrNot(true);

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5492")]
        [Description("Step(s) 18 to 19 from the original jira test ACC-4822")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        // Resolved Bug https://advancedcsg.atlassian.net/browse/ACC-5973
        public void ProviderScheduleBooking_ACC_4822_UITestMethod003()
        {
            #region Booking Type

            var _bookingTypeName1 = "BTC ACC-5402 1";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5402", "5402", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingType1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing");

            #endregion

            #region Booking Schedule Deletion Reasons

            var _cpBookingScheduleDeletionReasonName = "Added in error";
            var _cpBookingScheduleDeletionReasonId = dbHelper.cpBookingScheduleDeletionReason.GetCPBookingScheduleDeletionReasonByName(_cpBookingScheduleDeletionReasonName).First();

            #endregion

            #region Step 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateDeleteButtonIsPresent()
                .ClickOnDeleteButton();

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .SelectReasonForDeletePicklistOption("Added in error")
                .InsertTextInComments_DeleteBookingDynamicDialogue("Delete Booking ACC-5211.")
                .ClickDeleteButton_DeleteBookingDynamicDialogue();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking deleted");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Care Provider Booking Schedules")
                .SelectFilter("1", "BookingScheduleDeletionReason")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_cpBookingScheduleDeletionReasonName).TapSearchButton().SelectResultElement(_cpBookingScheduleDeletionReasonId.ToString());

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton()
                .WaitForResultsPageToLoad()

                .ClickColumnHeader(2)
                .ClickColumnHeader(2)
                .ValidateSearchResultRecordPresent(cpBookingScheduleId.ToString());

            System.Threading.Thread.Sleep(2000);
            advanceSearchPage
                .ResultsPageValidateHeaderCellText(17, "Deleted")
                .ValidateSearchResultRecordCellContent(cpBookingScheduleId.ToString(), 17, "Yes");

            advanceSearchPage
                .OpenRecord(cpBookingScheduleId.ToString());

            careProviderBookingScheduleRecordPage
                .WaitForCareProviderBookingScheduleRecordPageToLoadFromAdvancedSearch()
                .ValidateDeletionCommentsText("Delete Booking ACC-5211.");

            #endregion

            #region Step 19

            careProviderBookingScheduleRecordPage
                .NavigateToRelatedItemsSubPage_Audit();

            var auditSearch1 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 2, //update operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = cpBookingScheduleId.ToString(),
                ParentTypeName = "cpbookingschedule",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData = WebAPIHelper.Audit.RetrieveAudits(auditSearch1, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Updated", auditResponseData.GridData[0].cols[0].Text);
            Assert.AreEqual(_systemUserFullname, auditResponseData.GridData[0].cols[1].Text);

            var auditRecordId1 = auditResponseData.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("cpbookingschedule", "iframe_CWDataFormDialog")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(2, 2, "Created");

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("cpbookingschedule", "iframe_CWDataFormDialog")
                .ValidateRecordPresent(auditRecordId1)
                .ClickOnAuditRecord(auditRecordId1);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Updated")
                .ValidateChangedBy(_systemUserFullname)

                .ValidateFieldNameCellText(1, "Deletion Comments")
                .ValidateOldValueCellText(1, "")
                .ValidateNewValueCellText(1, "Delete Booking ACC-5211.");

            auditChangeSetDialogPopup
                .ValidateFieldNameCellText(2, "Deleted")
                .ValidateOldValueCellText(2, "No")
                .ValidateNewValueCellText(2, "Yes");

            auditChangeSetDialogPopup
                .ValidateFieldNameCellText(3, "Inactive")
                .ValidateOldValueCellText(3, "No")
                .ValidateNewValueCellText(3, "Yes");

            auditChangeSetDialogPopup
                .ValidateFieldNameCellText(4, "BookingScheduleDeletionReason")
                .ValidateOldValueCellText(4, "")
                .ValidateNewValueCellText(4, "Added in error")

                .TapCloseButton();

            var auditSearch2 = new Framework.WebAppAPI.Entities.CareDirector.AuditSearch
            {
                Operation = 1, //create operation
                CurrentPage = "1",
                TypeName = "audit",
                ParentId = cpBookingScheduleId.ToString(),
                ParentTypeName = "cpbookingschedule",
                RecordsPerPage = "50",
                ViewType = "0",
                AllowMultiSelect = "false",
                ViewGroup = "1",
                Year = "Last 90 Days",
                IsGeneralAuditSearch = false,
                UsePaging = true,
                PageNumber = 1
            };

            WebAPIHelper.Security.Authenticate();
            var auditResponseData2 = WebAPIHelper.Audit.RetrieveAudits(auditSearch2, WebAPIHelper.Security.AuthenticationCookie);
            Assert.AreEqual("Created", auditResponseData2.GridData[0].cols[0].Text);
            Assert.AreEqual(_defaultSystemUserFullName, auditResponseData2.GridData[0].cols[1].Text);
            var createdOnDate = auditResponseData2.GridData[0].cols[2].Text;

            var auditRecordId2 = auditResponseData2.GridData[0].Id;

            auditListPage
                .WaitForAuditListPageToLoadFromReferenceDataPage("cpbookingschedule", "iframe_CWDataFormDialog")
                .ValidateRecordPresent(auditRecordId2)
                .ClickOnAuditRecord(auditRecordId2);

            auditChangeSetDialogPopup
                .WaitForAuditChangeSetDialogPopupToLoad()
                .ValidateOperation("Created")
                .ValidateChangedBy(_defaultSystemUserFullName)
                .ValidateChangedOn(createdOnDate)
                .TapCloseButton();


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-5493")]
        [Description("Step(s) 20 to 22 from the original jira test ACC-4822")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        // Resolved Bug https://advancedcsg.atlassian.net/browse/ACC-5973
        public void ProviderScheduleBooking_ACC_4822_UITestMethod004()
        {
            #region Booking Type

            var _bookingTypeName1 = "BTC ACC-5402 1";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName1, 2, 960, new TimeSpan(7, 0, 0), new TimeSpan(21, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Care Provider Staff Role Type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "CPSRT 5402", "5402", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type - Contracted

            var _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid1);

            #endregion

            #region System User Employment Contract CP Booking Type

            if (!dbHelper.systemUserEmploymentContractCPBookingType.GetBySystemUserEmploymentContractIdAndCPBookingTypeId(_systemUserEmploymentContractId, _bookingType1).Any())
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region User Work Schedule

            //Create the user work schedule for all days of the week
            CreateUserWorkSchedule(_systemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region Booking Schedule Deletion Reasons

            var _cpBookingScheduleDeletionReasonName = "Added in error";
            var _cpBookingScheduleDeletionReasonId = dbHelper.cpBookingScheduleDeletionReason.GetCPBookingScheduleDeletionReasonByName(_cpBookingScheduleDeletionReasonName).First();

            #endregion

            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .ClickNewRecordButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickRegardingLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad().SelectLookFor("Providers").SearchAndSelectRecord(_providerName, _providerId);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickSaveButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateNoRecordMessageVisible(true)
                .ClickCloseButton();

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Pending");

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();

            #region Booking Schedule

            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing");

            #endregion

            expressBookingCriteriaRecordPage
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad()
                .ValidateNoRecordMessageVisible(false)
                .ValidateRecordPresent(cpBookingScheduleId.ToString())
                .ClickCloseButton();

            #region Execute 'Process Invoice Batches' scheduled job

            //Get the schedule job id
            Guid processExporessBookingId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Express Booking job for the Provider " + _providerName)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Express Booking job for the Provider" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(processExporessBookingId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(processExporessBookingId);

            #endregion

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickBackButton();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .OpenRecord(expressBookingCriteriaId.ToString());

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ValidateStatusSelectedText("Succeeded");

            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            #endregion

            #region Step 21

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingScheduleId.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickOnDeleteButton();

            createScheduleBookingPopup
                .WaitForDeleteBookingDynamicDialogueToLoad()
                .SelectReasonForDeletePicklistOption(_cpBookingScheduleDeletionReasonName)
                .InsertTextInComments_DeleteBookingDynamicDialogue("Delete Booking ACC-5211.")
                .ClickDeleteButton_DeleteBookingDynamicDialogue()
                .WaitForCreateScheduleBookingPopupClosed();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking deleted");

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .OpenRecord(expressBookingCriteriaId.ToString());

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickViewScheduledBookings();

            processScheduledBookingsForWeekCommencingPopup
                .WaitForProcessScheduledBookingsForWeekCommencingPopupToLoad(false)
                .ValidateNoRecordMessageVisible(true)
                .ValidateRecordNotPresent(cpBookingScheduleId.ToString())
                .ClickCloseButton();

            #endregion

            #region Step 22

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Booking Schedule Deletion Reasons")
                .ClickReferenceDataMainHeader("Care Provider Scheduling")
                .ClickReferenceDataElement("Booking Schedule Deletion Reasons");

            bookingScheduleDeletionReasonsPage
                .WaitForBookingScheduleDeletionReasonsPageToLoad()
                .InsertSearchQuery(_cpBookingScheduleDeletionReasonName)
                .TapSearchButton()
                .OpenRecord(_cpBookingScheduleDeletionReasonId.ToString());

            bookingScheduleDeletionReasonsRecordPage
                .WaitForBookingScheduleDeletionReasonsRecordPageToLoadInDrawerMode()
                .ValidateNameFieldValue(_cpBookingScheduleDeletionReasonName);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5513

        [TestProperty("JiraIssueID", "ACC-5729")]
        [Description("Step(s) 1 to 4 from the original jira test ACC-4832" +
                    "-> Create a schedule booking -> start date and time should be less than Staff Contract Start date." +
                    "-> Staff Contract status as Not Started" +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Validate Exception message as ' {system use full name} contract is not valid before {contractStartDate} .This staff member has been deallocated.'" +
                    "-> Dairy Booking should be created but the invalid staff should be unallocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod001()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            string _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            DateTime contractStartDate = DateTime.Parse(DateTime.Now.AddDays(28).ToString("dd'/'MM'/'yyyy"));
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, contractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var actualStartDate = commonMethodsHelper.GetThisWeekFirstMonday();

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 1 to 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, commonMethodsHelper.GetThisWeekFirstMonday().AddDays(7), commonMethodsHelper.GetThisWeekFirstMonday().AddDays(13), commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(actualStartDate.ToString("yyyy"), actualStartDate.ToString("MMMM"), actualStartDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role 5513_" + _currentDateTimeSuffix + " contract is not valid before " + contractStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00:00. This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5521

        [TestProperty("JiraIssueID", "ACC-5730")]
        [Description("Step(s) 5 from the original jira test ACC-4832" +
                    "-> Create a schedule booking -> start date and time should be greater than Staff Contract Start date." +
                    "-> Staff Contract status as Not Started" +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Warning message will be displayed while saving the schedule booking as '{staff system user full name} - {system User Employment Contract Title} contract has not started yet, and will not be allocated to this diary booking until this contract has started.'" +
                    "-> Dairy Booking should be created and the staff should be allocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod002()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            var _staffBFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_staff_SystemUserId, "fullname")["fullname"];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(2);
            var _currentDayOfTheWeek = (int)DateTime.Now.AddDays(1).DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractTitle = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId2, "name")["name"];

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForWarningDialogueToLoad()
                .ValidateWarningAlertMessage(_systemUserEmploymentContractTitle + " contract has not started yet, and will not be allocated to this diary booking until this contract has started.")
                .ClickDismissButton_WarningDialogue();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5731")]
        [Description("Step(s) 6 from the original jira test ACC-4832" +
                    "-> Create a schedule booking -> start date and time should be less than Staff Contract Start date." +
                    "-> Staff Contract status as Active" +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Validate Exception message as '{staff System User Full Name} contract is not valid before {Contract Start Date}. This staff member has been deallocated.'" +
                    "-> Dairy Booking should be created and the invalid staff should be unallocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod003()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            string _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;

            #endregion

            #region System User Employment Contract

            var contractStartDate = commonMethodsHelper.GetDatePartWithoutCulture().AddHours(23); //start date will be the current date at 23:00
            var _DayOfTheWeek = commonMethodsHelper.GetWallchartDayOfWeekIntegerValue(contractStartDate);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, contractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var actualStartDate = commonMethodsHelper.GetThisWeekFirstMonday();

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _DayOfTheWeek, _DayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 6

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(actualStartDate.ToString("yyyy"), actualStartDate.ToString("MMMM"), actualStartDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role 5513_" + _currentDateTimeSuffix + " contract is not valid before " + contractStartDate.ToString("dd'/'MM'/'yyyy") + " 23:00:00. This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5732")]
        [Description("Step(s) 7 from the original jira test ACC-4832" +
                    "-> Create a schedule booking -> start date and time should be greater than Staff Contract Start date." +
                    "-> Staff Contract status as Active" +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Exception Message should not be displayed in Express Booking Criteria Results tab." +
                    "-> Dairy Booking should be created but the staff should be allocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod004()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-5);
            var _currentDayOfTheWeek = (int)DateTime.Now.AddDays(1).DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad();

            var searchDate = DateTime.Now.AddDays(1);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .ValidateWarningDialogueNotVisibile();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking edited")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5733")]
        [Description("Step(s) 8 from the original jira test ACC-4832" +
                    "-> Create a schedule booking -> End date and time should be greater than Staff Contract End date." +
                    "-> Staff Contract status as Not Started" +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Validate Exception message as '{staff System User Full Name} contract is not valid after { system User Employment Contract End Date}. This staff member has been deallocated.'" +
                    "-> Dairy Booking should be created but the invalid staff should be unallocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod005()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            string _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(2);
            var _bookingDayOfTheWeek = (int)DateTime.Now.AddDays(1).DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(5);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _bookingDayOfTheWeek, _bookingDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            var cpBookingScheduleStaffId = dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 8

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Update System User Employment Contract to Ended

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-28);
            systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateStartDate(_systemUserEmploymentContractId2, systemUserEmploymentContractStartDate);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            dbHelper.cpBookingScheduleStaff.UpdateSystemUserEmploymentContract(cpBookingScheduleStaffId, _systemUserEmploymentContractId2);

            #endregion

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.AddDays(-1);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is not valid after " + systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyyy HH:mm:ss") + ". This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickSaveButton_DynamicDialogue();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking edited")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5613

        [TestProperty("JiraIssueID", "ACC-5734")]
        [Description("Step(s) 9 from the original jira test ACC-4832" +
                    "-> Create a schedule booking -> End date and time should be less than Staff Contract End date." +
                    "-> Select an active staff contract after creating Schedule booking change the active contract status to ended." +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Exception Message should not be displayed in Express Booking Criteria Results tab." +
                    "-> Dairy Booking should be created but the staff should be allocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod006()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-28);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Update Sysytem User Employment Contract to Ended

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .WaitForProviderSchedulePageToLoad();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5735")]
        [Description("Step(s) 10 from the original jira test ACC-4832" +
                    "-> Create a schedule booking -> End date and time should be less than Staff Contract End date." +
                    "-> Select an active staff contract after creating Schedule booking change the active contract status to ended." +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Validate Exception message as '{staff System User Full Name} contract is not valid after { system User Employment Contract End Date}. This staff member has been deallocated.'" +
                    "-> Dairy Booking should be created but the invalid staff should be unallocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod007()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            string _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-28);
            var _bookingDayOfTheWeek = (int)DateTime.Now.AddDays(1).DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddHours(2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _bookingDayOfTheWeek, _bookingDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            var cpBookingScheduleStaffId = dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            #endregion

            #region Step 10

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Update System User Employment Contract to Ended

            systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-6);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            dbHelper.cpBookingScheduleStaff.UpdateSystemUserEmploymentContract(cpBookingScheduleStaffId, _systemUserEmploymentContractId2);

            #endregion
            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(2000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is not valid after " + systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyyy HH:mm:ss") + ". This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5736")]
        [Description("Step(s) 11 from the original jira test ACC-4832" +
                    "-> Create a schedule booking -> End date and time should be less than Staff Contract End date." +
                    "-> Select an active staff contract." +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Exception Message should not be displayed in Express Booking Criteria Results tab." +
                    "-> Dairy Booking should be created but the staff should be allocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod008()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-7);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5738")]
        [Description("Step(s) 12 from the original jira test ACC-4832" +
                    "-> Create a Schedule booking -> with suspended staff contract." +
                    "-> Create an Express Booking with the same selected Scheduled booking provider and save the record." +
                    "-> Validate Exception message as '{staff System User Full Name} contract is currently suspended. This staff member has been deallocated.'" +
                    "-> Dairy Booking should be created but the invalid staff should be deallocated.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod009()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-7);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { _systemUserEmploymentContractId2 };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_staff_SystemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 12

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad();

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is currently suspended. This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5739")]
        [Description("Step(s) 13 from the original jira test ACC-4832" +
                    "-> Create a Schedule Booking and select active staff member while creating scheduled booking then end the contract of staff before creating/running express book and verify exception and diary booking" +
                    "-> Validate Exception message as '{staff System User Full Name} contract is not valid after { system User Employment Contract End Date}. This staff member has been deallocated.'" +
                    "-> Dairy Booking should be created but invalid staff should be unassigned.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod010()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            var cpBookingScheduleStaffId = dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Update System User Employment Contract to Ended

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            dbHelper.cpBookingScheduleStaff.UpdateSystemUserEmploymentContract(cpBookingScheduleStaffId, _systemUserEmploymentContractId2);

            #endregion

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is not valid after " + systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyyy HH:mm:ss") + ". This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5740")]
        [Description("Step(s) 14 from the original jira test ACC-4832" +
                    "-> Create a Schedule Booking and select active staff member while creating scheduled booking then suspend the contract of staff before creating/running express book and verify exception and diary booking" +
                    "-> Validate Exception message as '{staff System User Full Name} contract is currently suspended. This staff member has been deallocated.'" +
                    "-> Diary Booking should be created but the invalid staff should be unassigned.")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod011()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-7);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            var cpBookingScheduleStaffId = dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { _systemUserEmploymentContractId2 };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_staff_SystemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            dbHelper.cpBookingScheduleStaff.UpdateSystemUserEmploymentContract(cpBookingScheduleStaffId, _systemUserEmploymentContractId2);

            #endregion


            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " contract is currently suspended. This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5741")]
        [Description("Step(s) 15 from the original jira test ACC-4832" +
                    "-> Create a Schedule Booking -> select active staff member while creating scheduled booking " +
                    " then update staff contract start date or end date in such a way that staff becomes invalid for the scheduled booking and " +
                    " then create and run express book and verify exceptions and diary booking")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod012()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            var cpBookingScheduleStaffId = dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 15

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Update System User Employment Contract to Ended

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(3);
            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7);
            dbHelper.systemUserEmploymentContract.UpdateStartDate(_systemUserEmploymentContractId2, systemUserEmploymentContractStartDate);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            dbHelper.cpBookingScheduleStaff.UpdateSystemUserEmploymentContract(cpBookingScheduleStaffId, _systemUserEmploymentContractId2);

            #endregion

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is not valid before " + systemUserEmploymentContractStartDate.ToString("dd'/'MM'/'yyyy HH:mm:ss") + ". This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5667

        [TestProperty("JiraIssueID", "ACC-5742")]
        [Description("Step(s) 16 from the original jira test ACC-4832" +
                    "-> Edit a Schedule Booking if they are not processed by Express Booking* " +
                    "-> select active staff member while editing scheduled booking then end the contract of staff before creating/running express book and verify exception and diary booking")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod013()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            string _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            #endregion

            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .VerifyResponsibleTeamIsDisplayed(_teamId, _teamName)
                .EnterTextIntoFilterStaffByNameSearchBox(_staff_SystemUserFullName)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId2)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2.ToString())
                .ClickStaffConfirmSelection();

            //terminate the contract before the save opperation
            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId2, systemUserEmploymentContractEndDate, contractEndReasonId);

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickSaveButton_DynamicDialogue();

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is not valid after " + systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyyy HH:mm:ss") + ". This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            #endregion

        }


        [TestProperty("JiraIssueID", "ACC-5743")]
        [Description("Step(s) 17 from the original jira test ACC-4832" +
                    "-> Edit a Schedule Booking if they are not processed by Express Booking* " +
                    "-> select active staff member while editing scheduled booking then end the contract of staff before creating/running express book and verify exception and diary booking")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod014()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            string _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, null);
            var _systemUserEmploymentContractTitle = (string)dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId2, "name")["name"];

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion


            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            var cpBookingScheduleStaffId = dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 17

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .VerifyResponsibleTeamIsDisplayed(_teamId, _teamName)
                .EnterTextIntoFilterStaffByNameSearchBox(_staff_SystemUserFullName)
                .VerifySelectedStaffRecordInstaffForBookingIsDisplayed(_systemUserEmploymentContractId2, _staff_SystemUserFullName, true)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickSaveChangesBookingButton();

            System.Threading.Thread.Sleep(2000);

            #region Staff Contract Suspension Reason

            systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { _systemUserEmploymentContractId2 };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_staff_SystemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            dbHelper.cpBookingScheduleStaff.UpdateSystemUserEmploymentContract(cpBookingScheduleStaffId, _systemUserEmploymentContractId2);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffContractsPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserStaffContractsPageToLoad()
                .ClickColumnCellToSortRecord(2, "Id")
                .ClickColumnCellToSortRecord(2, "Id")
                .OpenRecord(_systemUserEmploymentContractId2);

            System.Threading.Thread.Sleep(2000);
            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .ValidateStatusFieldSelectedText("Suspended");

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is currently suspended. This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");






            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5754

        [TestProperty("JiraIssueID", "ACC-5744")]
        [Description("Step(s) 18 from the original jira test ACC-4832" +
                    "-> *Edit a Schedule Booking if they are not processed by Express Booking* " +
                    "-> select active staff member while editing scheduled booking then " +
                    "update staff contract start date or end date in such a way that staff becomes invalid for the scheduled booking and then " +
                    "create and run express book and verify exceptions and diary booking")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod015()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            string _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 18

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(2);
            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(14);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToStaffContractsPage();

            systemUserEmploymentContractsPage
                .WaitForSystemUserStaffContractsPageToLoad()
                .ClickColumnCellToSortRecord(2, "Id")
                .ClickColumnCellToSortRecord(2, "Id")
                .OpenRecord(_systemUserEmploymentContractId2);

            systemUserEmploymentContractsRecordPage
                .WaitForSystemUserEmploymentContractsRecordPageToLoad()
                .InsertStartDate(systemUserEmploymentContractStartDate.ToString("dd'/'MM'/'yyyy"), "01:00")
                .InsertEndDate(systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyyy"))
                .InsertEndTime("01:00")
                .ClickSaveAndCloseButton();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .TapOKButton();

            systemUserEmploymentContractsPage
                .WaitForSystemUserStaffContractsPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .VerifyResponsibleTeamIsDisplayed(_teamId, _teamName)
                .EnterTextIntoFilterStaffByNameSearchBox(_staff_SystemUserFullName);

            selectStaffPopup
                .ClickStaffRecordCellText(_systemUserEmploymentContractId2)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ClickSaveChangesBookingButton();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ClickConfirmButton_WarningDialogue();

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(1, expressBookingResultId.Count);

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(expressBookingResultId[0], true)
                .OpenRecord(expressBookingResultId[0]);

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is not valid before " + systemUserEmploymentContractStartDate.ToString("dd'/'MM'/'yyyy") + " 01:00:00. This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5745")]
        [Description("Step(s) 19_1 from the original jira test ACC-4832" +
                    "-> *Create a scheduled booking* -> Having BTC - 4 and " +
                    "Select Not Started staff and then verify exception and diary booking after running express book")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod016()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5631_4", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract = Not Started

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(3);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            dbHelper.systemUserEmploymentContract.UpdateFixedWorkingPatternCycle(_systemUserEmploymentContractId2, 1);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            DateTime bookingStartDate = commonMethodsHelper.GetWeekFirstMonday(currentDate.AddDays(7)); //Monday of next week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5746")]
        [Description("Step(s) 19_2 from the original jira test ACC-4832" +
                    "-> *Create a scheduled booking* -> Having BTC - 4 and " +
                    "Select Suspended staff and then verify exception and diary booking after running express book")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod017()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5631_4", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract

            var systemUserEmploymentContract1StartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContract1StartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            dbHelper.systemUserEmploymentContract.UpdateFixedWorkingPatternCycle(_systemUserEmploymentContractId2, 1);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { _systemUserEmploymentContractId2 };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_staff_SystemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 19

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();
            DateTime bookingStartDate = commonMethodsHelper.GetWeekFirstMonday(currentDate.AddDays(7)); //Monday of next week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            Assert.AreEqual(1, cpDiaryBookingId.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(0, expressBookingResultId.Count);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5798")]
        [Description("Step(s) 20 from the original jira test ACC-4832" +
                   "-> create scheduled booking having multiple staff members " +
                   " First contract is suspended, Second contract is Not Started, Third contract is Ended " +
                   " Then run express book and verify exception for each staff member. we need to check in diary booking that all invalid staffs are deallocated")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        [TestMethod()]
        public void ProviderScheduleBooking_ACC_4832_UITestMethod018()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role_5513_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-5513", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserFullName = "Staff_SystemUser_ " + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region System User Employment Contract = Suspended

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-7);
            var _currentDayOfTheWeek = (int)DateTime.Now.DayOfWeek;
            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { _systemUserEmploymentContractId1 };
            dbHelper.systemUserSuspension.CreateSystemUserSuspension(_staff_SystemUserId, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region System User Employment Contract = Not Started

            var systemUserEmploymentContractNotStartedStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(7);
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContractNotStartedStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion


            #region System User Employment Contract = Ended

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            var _systemUserEmploymentContractId3 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, systemUserEmploymentContractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            dbHelper.systemUserEmploymentContract.UpdateEndDate(_systemUserEmploymentContractId3, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId3, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);
            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId3, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing 1_" + _currentDateTimeSuffix);
            var cpBookingSchedule2Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(9, 0, 0), new TimeSpan(10, 0, 0), _providerId, "Express Book Testing 2_" + _currentDateTimeSuffix);
            var cpBookingSchedule3Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, _currentDayOfTheWeek, _currentDayOfTheWeek, new TimeSpan(11, 0, 0), new TimeSpan(12, 0, 0), _providerId, "Express Book Testing 3_" + _currentDateTimeSuffix);

            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule2Id, 1);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule3Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId1, _staff_SystemUserId);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule2Id, _systemUserEmploymentContractId2, _staff_SystemUserId);
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule3Id, _systemUserEmploymentContractId3, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true);

            #region Express Book for Provider

            DateTime bookingStartDate = commonMethodsHelper.GetThisWeekFirstMonday(); //Monday of This week
            DateTime bookingEndDate = bookingStartDate.AddDays(6);
            var _expressBookingCriteriaId1 = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_teamId, _businessUnitId, "", 1, _providerId, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture(), _providerId, "provider", "P4821 " + _currentDateTimeSuffix);
            System.Threading.Thread.Sleep(5000);

            #endregion

            #region Schdeduled job for Express Book

            //get the schedule job id
            var scheduleJobId = dbHelper.scheduledJob.GetByPartialName(_currentDateTimeSuffix).FirstOrDefault();

            //execute the schedule job and wait for the Idle status
            this.WebAPIHelper.Security.Authenticate();
            this.WebAPIHelper.ScheduleJob.Execute(scheduleJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            System.Threading.Thread.Sleep(2000);

            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(scheduleJobId);

            System.Threading.Thread.Sleep(5000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            var searchDate = DateTime.Now.Date;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickChangeDate(searchDate.ToString("yyyy"), searchDate.ToString("MMMM"), searchDate.Day.ToString());

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();

            var cpDiaryBookingId1 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule1Id);
            var cpDiaryBookingId2 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule2Id);
            var cpDiaryBookingId3 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingSchedule3Id);
            Assert.AreEqual(1, cpDiaryBookingId1.Count);
            Assert.AreEqual(1, cpDiaryBookingId2.Count);
            Assert.AreEqual(1, cpDiaryBookingId3.Count);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToExpressBookSection();

            expressBookingCriteriaPage
                .WaitForExpressBookingCriteriaPageToLoad()
                .SearchRecord("*" + _currentDateTimeSuffix)
                .ValidateRecordPresent(_expressBookingCriteriaId1, true)
                .OpenRecord(_expressBookingCriteriaId1);

            expressBookingCriteriaRecordPage
                .WaitForExpressBookingCriteriaRecordPageToLoad()
                .ClickResultsTab();

            var expressBookingCriteriaId = dbHelper.cpExpressBookingCriteria.GetByRegardingID(_providerId).First();
            var expressBookingResultId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaID(expressBookingCriteriaId);
            Assert.AreEqual(3, expressBookingResultId.Count);

            var suspendededId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaIDAndCPBookingScheduleID(expressBookingCriteriaId, cpBookingSchedule1Id).First();
            var notStartedId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaIDAndCPBookingScheduleID(expressBookingCriteriaId, cpBookingSchedule2Id).First();
            var endedId = dbHelper.cpExpressBookingResult.GetByExpressBookingCriteriaIDAndCPBookingScheduleID(expressBookingCriteriaId, cpBookingSchedule3Id).First();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(suspendededId.ToString(), true)
                .OpenRecord(suspendededId.ToString());

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is currently suspended. This staff member has been deallocated.");

            var cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId1[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            var _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId1[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .ClickBackButton();

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ClickBackButton();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(notStartedId.ToString(), true)
                .OpenRecord(notStartedId.ToString());

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is not valid before " + systemUserEmploymentContractNotStartedStartDate.ToString("dd'/'MM'/'yyyy HH:mm:ss") + ". This staff member has been deallocated.");

            cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId2[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId2[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .ClickBackButton();

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ClickBackButton();

            expressBookingResultsPage
                .WaitForExpressBookingResultsPageToLoad()
                .ValidateRecordPresent(endedId.ToString(), true)
                .OpenRecord(endedId.ToString());

            expressBookingResultRecordPage
                .WaitForExpressBookingResultRecordPageToLoad()
                .ValidateExceptionMessageText(_staff_SystemUserFullName + " - Role_5513_" + _currentDateTimeSuffix + " contract is not valid after " + systemUserEmploymentContractEndDate.ToString("dd'/'MM'/'yyyy HH:mm:ss") + ". This staff member has been deallocated.");

            cpDiaryBookingTitle = dbHelper.cPBookingDiary.GetCPBookingDiaryByID(cpDiaryBookingId3[0], "title")["title"].ToString();

            expressBookingResultRecordPage
                .ValidateBookingDiaryLinkText(cpDiaryBookingTitle)
                .ClickBookingDiaryLink();

            lookupFormPage
                .WaitForLookupFormPageToLoad()
                .ClickViewButton();

            _cpBookingDiaryStaffID = dbHelper.cPBookingDiaryStaff.GetByCPBookingDiaryId(cpDiaryBookingId3[0]);

            diaryBookingsRecordPage
                .WaitForDiaryBookingRecordPageToLoad()
                .WaitForBookingDiaryStaffSubRecordPageToLoad()
                .ValidateRecordIsPresent(_cpBookingDiaryStaffID[0].ToString(), true)
                .ValidateRecordCellText(_cpBookingDiaryStaffID[0].ToString(), 2, "Unallocated");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-5828

        [TestProperty("JiraIssueID", "ACC-5866")]
        [Description("Step(s) 1 & 2 from the original jira test ACC-5827" +
                    "--> Navigate to workplace -> Provider Schedule -> select a provider -> create a schedule booking with a valid booking type available for a provider. " +
                    "--> Navigate to Workplace -> Providers -> open a provider -> Details -> Scheduling booking types remove a booking type selected in step 1 and save " +
                    "--> Refresh and open the same schedule booking created in step 1 and click on save " +
                    "--> Verify the Warning Message")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5827_UITestMethod001()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 5827_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-5866";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            var ProviderAllowableBookingTypesId = commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            DateTime contractStartDate = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("dd'/'MM'/'yyyy"));
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, contractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
               .WaitForEditScheduleBookingPopupPageToLoad()
               .ValidateBookingTypeDropDownText(_bookingTypeName)
               .ClickOnCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickDetailsTab()
                .WaitForProvidersSchedulingBookingTypesSectionToLoad()
                .SelectRecord(ProviderAllowableBookingTypesId.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            System.Threading.Thread.Sleep(1000);
            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName + " - Invalid")
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type " + _bookingTypeName + ". It is no longer valid for this provider.")
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-5867")]
        [Description("Step(s) 3 from the original jira test ACC-5827" +
                   "--> Navigate to workplace -> Provider Schedule -> select a provider ->  click on add booking and select a booking type in the create booking form " +
                   "--> Open another tab and navigate to workplace -> providers -> select a provider same as step 1 -> go to details tab -> remove booking type selected in step 1 from scheduling booking types section " +
                   "--> Come back to previous tab create booking form and fill in other required details->click on create booking" +
                   "--> Verify error message")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5827_UITestMethod002()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 5827_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-5867";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            var ProviderAllowableBookingTypesId = commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            DateTime contractStartDate = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("dd'/'MM'/'yyyy"));
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, contractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Step 3

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
               .WaitForCreateScheduleBookingPopupPageToLoad()
               .ValidateBookingTypeDropDownText(_bookingTypeName);

            createScheduleBookingPopup
                .OpenDuplicateTab()
                .SwitchToNewTab();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .SearchProviderRecord(_providerName, _providerId.ToString())
                .OpenProviderRecord(_providerId.ToString());

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickDetailsTab()
                .WaitForProvidersSchedulingBookingTypesSectionToLoad()
                .SelectRecord(ProviderAllowableBookingTypesId.ToString())
                .ClickDeleteButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("1 item(s) deleted.")
                .TapOKButton();

            createScheduleBookingPopup
                .SwitchToPreviousTab();

            System.Threading.Thread.Sleep(1000);
            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("The booking type " + _bookingTypeName + " selected is not allowed for the selected Provider " + _providerName + ".")
                .ClickDismissButton_DynamicDialogue();

            #endregion

            // Step 4 already covered in Step 2.

        }

        [TestProperty("JiraIssueID", "ACC-5868")]
        [Description("Step(s) 5 from the original jira test ACC-5827 " +
                   "--> Verify that the warning message is different from the above when the valid to date is in the past for a booking type and the provider does not have that booking as allowable booking type.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_5827_UITestMethod003()
        {
            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 5827_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var validFromDate = DateTime.Now.AddDays(-14);
            var validToDate = DateTime.Now.AddDays(14);
            var _bookingTypeName = "BTC ACC-5868_5";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);
            dbHelper.cpBookingType.UpdateValidFromAndValidTo(_bookingType1, validFromDate, validToDate);
            //dbHelper.cpBookingType.UpdateValidFrom(_bookingType1, validFromDate);
            //dbHelper.cpBookingType.UpdateValidTo(_bookingType1, validToDate);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            DateTime contractStartDate = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("dd'/'MM'/'yyyy"));
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, contractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId2, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId2, _availabilityTypeId);

            #endregion

            #region Booking Schedule

            var cpBookingSchedule1Id = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), _providerId, "Express Book Testing " + _currentDateTimeSuffix);
            dbHelper.cpBookingSchedule.UpdateGenderPreference(cpBookingSchedule1Id, 1);

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, cpBookingSchedule1Id, _systemUserEmploymentContractId2, _staff_SystemUserId);

            var _cpBookingScheduleStaffIds = dbHelper.cpBookingScheduleStaff.GetByCPBookingScheduleIdAndEmploymentContractId(cpBookingSchedule1Id, null);

            foreach (var _cpBookingScheduleStaffId in _cpBookingScheduleStaffIds)
                dbHelper.cpBookingScheduleStaff.DeleteCPBookingScheduleStaff(_cpBookingScheduleStaffId);

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName)
                .ClickOnCloseButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToBookingTypesPage();

            bookingTypesPage
                .WaitForBookingTypesPageToLoad()
                .InsertQuickSearchText(_bookingTypeName)
                .ClickQuickSearchButton()
                .OpenRecord(_bookingType1.ToString());

            var validToDateNew = DateTime.Now.AddDays(-7);
            bookingTypeRecordPage
                .WaitForBookingTypeRecordPageToLoad()
                .InsertTextOnValidToDate(validToDateNew.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .ValidateScheduleBookingIsPresent(cpBookingSchedule1Id.ToString(), true)
                .ClickScheduleBooking(cpBookingSchedule1Id.ToString());

            System.Threading.Thread.Sleep(1000);
            createScheduleBookingPopup
                .WaitForEditScheduleBookingPopupPageToLoad()
                .ValidateBookingTypeDropDownText(_bookingTypeName + " - Invalid")
                .ClickCreateBooking();

            createScheduleBookingPopup
                .WaitForDynamicDialogueToLoad()
                .ValidateMessage_DynamicDialogue("Please consider updating the booking type " + _bookingTypeName + ". It is now invalid, valid between " + validFromDate.ToString("dd'/'MM'/'yyyy") + " - " + validToDateNew.ToString("dd'/'MM'/'yyyy") + ".")
                .ClickDismissButton_DynamicDialogue();

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6066

        [TestProperty("JiraIssueID", "ACC-6190")]
        [Description("Step(s) 1 to 4 from the original jira test ACC-4616")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod()]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_4616_UITestMethod001()
        {
            #region Business Unit

            var _businessUnit2Id = commonMethodsDB.CreateBusinessUnit("PS BU 2");

            #endregion

            #region Team 2

            var _teamId2 = commonMethodsDB.CreateTeam("PS T2 " + _dateSuffix, null, _businessUnit2Id, "907633", "PST2" + _dateSuffix + "@careworkstempmail.com", "ProviderSchedule T2", "020 123456");

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 4616_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);
            var _staffRoleTypeid2 = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 4616_2_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-4616";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            var ProviderAllowableBookingTypesId = commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Provider_Staff_SystemUser_" + _currentDateTimeSuffix;
            var _staff_SystemUserId = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Provider_Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_staff_SystemUserId, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_staff_SystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            DateTime contractStartDate = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("dd'/'MM'/'yyyy"));
            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, contractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId });
            var _systemUserEmploymentContractId2 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_staff_SystemUserId, contractStartDate, _staffRoleTypeid2, _teamId2, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId2 });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_staff_SystemUserId, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Step 1 to 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")

                .WaitForProviderSchedulePageToLoad()
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickManageStaffButton();

            selectStaffPopup
               .WaitForSelectStaffPopupToLoad()
               .ClickOnlyShowAvailableStaff()
               .EnterTextIntoFilterStaffByNameSearchBox("Provider_Staff_SystemUser_ " + _currentDateTimeSuffix)
               .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId1)
               .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId2, false);

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6169

        [TestProperty("JiraIssueID", "ACC-6191")]
        [Description("Step(s) 5 to 6 from the original jira test ACC-4616")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_4616_UITestMethod002()
        {
            #region Security Profiles

            var securityProfileId1 = dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User").First();
            var securityProfileId2 = dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data").First();
            var securityProfileId3 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup").First();
            var securityProfileId4 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)").First();
            var securityProfileId5 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)").First();
            var securityProfileId6 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Recruitment (Edit)").First();
            var securityProfileId7 = dbHelper.securityProfile.GetSecurityProfileByName("Rostering (Edit)").First();
            var securityProfileId8 = dbHelper.securityProfile.GetSecurityProfileByName("Staff Training (Edit)").First();
            var securityProfileId9 = dbHelper.securityProfile.GetSecurityProfileByName("Training Setup (Edit)").First();
            var securityProfileId10 = dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)").First();
            var securityProfileId11 = dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)").First();
            var securityProfileId12 = dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access").First();
            var securityProfileId13 = dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access").First();
            var securityProfileId14 = dbHelper.securityProfile.GetSecurityProfileByName("Team Membership (Edit)").First();
            var securityProfileId15 = dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)").First();
            var securityProfileId16 = dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Availability Type View Only").First();
            var securityProfileId17 = dbHelper.securityProfile.GetSecurityProfileByName("Person Contracts (Edit)").First();
            var securityProfileId18 = dbHelper.securityProfile.GetSecurityProfileByName("Scheduling Setup (Edit)").First();
            
            var securityProfilesList = new List<Guid> 
            {
                securityProfileId1, securityProfileId2, securityProfileId3, securityProfileId4, securityProfileId5,
                securityProfileId6, securityProfileId7, securityProfileId8, securityProfileId9, securityProfileId10,
                securityProfileId11, securityProfileId12, securityProfileId13, securityProfileId14, securityProfileId15, 
                securityProfileId16, securityProfileId17, securityProfileId18 
            };

            #endregion

            #region System User (Login User)

            _systemUsername = "PSU" + _dateSuffix;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PS", "U" + _dateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, securityProfilesList, 2); //core system user
            dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, _localZone.StandardName);
            _systemUserFullname = "ProviderScheduleUser User" + _dateSuffix;

            #endregion

            #region Business Unit

            var _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("PS BU 2");

            #endregion

            #region Team

            var _teamName2 = "PS T2";
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId2, "107624", "PST2@careworkstempmail.com", "Provider Schedule Booking T2", "020 123456");

            var _teamName2_B = "PS T2B";
            var _teamId2_B = commonMethodsDB.CreateTeam(_teamName2_B, null, _businessUnitId2, "107625", "PST2B@careworkstempmail.com", "Provider Schedule Booking T2B", "020 123456");

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 4616_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            _providerName = "P4616_" + _currentDateTimeSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId2, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-4616";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Provider_Staff_SystemUser_" + _currentDateTimeSuffix;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Provider_Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, new List<Guid>(), 3); //Rostered System User
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Team member for Team 2 - all System Users

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId, DateTime.Now, null);

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId1, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId1, DateTime.Now, null);

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Step 5

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateRosteringTabIsVisible()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Provider_Staff_SystemUser_ " + _currentDateTimeSuffix)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId1)
                .ClickBackToBookingButton();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickOnCloseButton();

            #endregion

            #region Step 6

            // Update Can Work At Team
            dbHelper.systemUserEmploymentContract.DeleteCanWorksAt(_systemUserEmploymentContractId1, new List<Guid> { _teamId2 });
            dbHelper.systemUserEmploymentContract.UpdateCanWorksAt(_systemUserEmploymentContractId1, new List<Guid> { _teamId2_B });

            mainMenu
                .WaitForMainMenuToLoad(true, false, true, false, true, true)
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateRosteringTabIsVisible()
                .ValidateLocationProviderText(_providerName + " - No Address")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Provider_Staff_SystemUser_ " + _currentDateTimeSuffix)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId1, true); //due to recent changes this validation needs to be set to true now as both the login user and the staff member belong to the PS T2 team

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6192")]
        [Description("Step(s) 8 from the original jira test ACC-4616")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_4616_UITestMethod003()
        {
            #region Business Unit

            var _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("Provider Schedule Booking BU2");

            #endregion

            #region Team

            var _teamName2 = "ProviderScheduleBookingT2";
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId2, "107624", "ProviderScheduleBookingT2@careworkstempmail.com", "ProviderScheduleBookingT2", "020 123456");

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 4616_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            _providerName = "P4616_" + _currentDateTimeSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId2, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC ACC-4616", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, true);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staffUserName1 = "Staff_First" + _currentDateTimeSuffix;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staffUserName1, "Staff_First", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            string _staffUserName2 = "Staff_Second" + _currentDateTimeSuffix;
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_staffUserName2, "Staff_Second", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId2, 1); //1 = Male

            string _staffUserName3 = "Staff_Third" + _currentDateTimeSuffix;
            var _systemUserId3 = commonMethodsDB.CreateSystemUserRecord(_staffUserName3, "Staff_Third", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId3, 1); //1 = Male

            string _staffUserName4 = "Staff_Fourth" + _currentDateTimeSuffix;
            var _systemUserId4 = commonMethodsDB.CreateSystemUserRecord(_staffUserName4, "Staff_Fourth", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId4, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId2, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId3, commonMethodsHelper.GetThisWeekFirstMonday());
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId4, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Team member for Team 2 - all System Users

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId, DateTime.Now, null);

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId1, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId1, DateTime.Now, null);

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId2, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId2, DateTime.Now, null);

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId3, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId3, DateTime.Now, null);

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId4, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId4, DateTime.Now, null);

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });
            var _notStarted_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId4, null, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });

            #endregion

            #region System User Employment Contract for Suspended status

            var systemUserEmploymentContractStartDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-3);
            var suspension_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId2, systemUserEmploymentContractStartDate, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });


            #endregion

            #region System User Employment Contract for Ended status

            var systemUserEmploymentContractStartDate2 = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-10);
            var ended_systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId3, systemUserEmploymentContractStartDate2, _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });

            #endregion

            #region Contract End Reasons

            var contractEndReasonId = dbHelper.contractEndReason.GetByName("Unknown reason")[0];

            #endregion

            #region Staff Contract Suspension Reason

            var systemUserSuspensionReasonId = commonMethodsDB.CreateSystemUserSuspensionReason(_teamId, "Default Suspension Reason", new DateTime(2020, 1, 1));

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(suspension_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(ended_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_notStarted_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId2, _teamId, suspension_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId3, _teamId, ended_systemUserEmploymentContractId, _availabilityTypeId);
            CreateUserWorkSchedule(_systemUserId4, _teamId, _notStarted_systemUserEmploymentContractId, _availabilityTypeId);

            #endregion

            #region End System User Employment Contract

            var systemUserEmploymentContractEndDate = commonMethodsHelper.GetCurrentDateWithoutCulture().AddDays(-2);
            dbHelper.systemUserEmploymentContract.UpdateEndDate(ended_systemUserEmploymentContractId, systemUserEmploymentContractEndDate, contractEndReasonId);

            #endregion

            #region Create System User Contract Suspension

            var systemUserSuspensionStartDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now).Date;
            var contracts = new List<Guid> { suspension_systemUserEmploymentContractId };
            var suspensionId = dbHelper.systemUserSuspension.CreateSystemUserSuspension(_systemUserId2, systemUserSuspensionStartDate, _teamId, systemUserSuspensionReasonId, contracts);

            #endregion

            #region Step 8

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateRosteringTabIsVisible()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Staff_First " + _currentDateTimeSuffix)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId1)
                .EnterTextIntoFilterStaffByNameSearchBox("Staff_Second " + _currentDateTimeSuffix)
                .VerifyStaffRecordIsDisplayed(suspension_systemUserEmploymentContractId)
                .EnterTextIntoFilterStaffByNameSearchBox("Staff_Third " + _currentDateTimeSuffix)
                .VerifyStaffRecordIsDisplayed(ended_systemUserEmploymentContractId, false)
                .EnterTextIntoFilterStaffByNameSearchBox("Staff_Fourth " + _currentDateTimeSuffix)
                .VerifyStaffRecordIsDisplayed(_notStarted_systemUserEmploymentContractId);

            #endregion


        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-6233

        [TestProperty("JiraIssueID", "ACC-6259")]
        [Description("Step(s) 9 to 10 from the original jira test ACC-4616")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Provider Schedule")]
        public void ProviderScheduleBooking_ACC_4616_UITestMethod004()
        {
            var currentDayOfTheWeek = DateTime.Now.DayOfWeek.ToString();

            #region Business Unit

            var _businessUnitId2 = commonMethodsDB.CreateBusinessUnit("Provider Schedule Booking BU2");

            #endregion

            #region Team

            var _teamName2 = "ProviderScheduleBookingT2";
            var _teamId2 = commonMethodsDB.CreateTeam(_teamName2, null, _businessUnitId2, "107624", "ProviderScheduleBookingT2@careworkstempmail.com", "ProviderScheduleBookingT2", "020 123456");

            #endregion

            #region Availability Type

            var _availabilityTypeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Role 4616_" + _currentDateTimeSuffix, null, null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Contracted")[0];

            #endregion

            #region Provider

            _providerName = "P4616_" + _currentDateTimeSuffix;
            _providerId = commonMethodsDB.CreateProvider(_providerName, _teamId2, 12, true); // Training Provider

            #endregion

            #region Booking Type

            var _bookingTypeName = "BTC ACC-4616";
            var _bookingType1 = commonMethodsDB.CreateCPBookingType(_bookingTypeName, 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Provider Allowable Booking Types

            commonMethodsDB.CreateProviderAllowableBookingTypes(_teamId, _providerId, _bookingType1, false);

            #endregion

            #region Staff - System Users

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            string _staff_SystemUserName = "Provider_Staff_SystemUser_" + _currentDateTimeSuffix;
            var _systemUserId1 = commonMethodsDB.CreateSystemUserRecord(_staff_SystemUserName, "Provider_Staff_SystemUser_", _currentDateTimeSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateStatedGender(_systemUserId1, 1); //1 = Male

            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId1, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region Team member for Team 2 - all System Users

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId, DateTime.Now, null);

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(_systemUserId1, _teamId2).Any())
                dbHelper.teamMember.CreateTeamMember(_teamId2, _systemUserId1, DateTime.Now, null);

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId1 = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId1, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId, 40, new List<Guid>() { }, new List<Guid> { _teamId, _teamId2 });

            #endregion

            #region Link Booking Type to Employment Contract

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId1, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_systemUserId1, _teamId, _systemUserEmploymentContractId1, _availabilityTypeId);

            #endregion

            #region Step 9

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!", EnvironmentName);

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderScheduleSection();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .selectProvider(_providerName + " - No Address")
                .clickAddBooking();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ValidateRosteringTabIsVisible()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .SelectBookingType(_bookingTypeName)
                .SetStartDay(currentDayOfTheWeek)
                .SetStartTime("10", "00")
                .SetEndDay(currentDayOfTheWeek)
                .SetEndTime("12", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Provider_Staff_SystemUser_ " + _currentDateTimeSuffix)
                .VerifyStaffRecordIsDisplayed(_systemUserEmploymentContractId1)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createScheduleBookingPopup
                .WaitForCreateScheduleBookingPopupPageToLoad()
                .ClickCreateBooking();

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateBookingStatus("Booking created");

            System.Threading.Thread.Sleep(1000);
            var bookingSchedules = dbHelper.cpBookingSchedule.GetByLocationId(_providerId);
            Assert.AreEqual(1, bookingSchedules.Count);
            var cpBookingScheduleId = bookingSchedules[0];

            providerSchedulePage
                .WaitForProviderSchedulePageToLoad()
                .ValidateScheduleBookingIsPresent(cpBookingScheduleId.ToString(), true);

            #endregion

            #region Step 10

            var startYear = DateTime.Now.Date.Year.ToString();
            var startMonth = DateTime.Now.Date.ToString("MMMM");
            var startDay = DateTime.Now.Date.Day.ToString();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProviderDiarySection();

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address")
                .WaitForProviderDiaryPageToLoad()
                .clickAddBooking();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .ValidateLocationProviderText(_providerName + " - No Address")

                .SelectBookingType(_bookingTypeName)
                .SelectStartDate(startYear, startMonth, startDay)
                .SelectEndDate(startYear, startMonth, startDay)
                .InsertStartTime("12", "00")
                .InsertEndTime("14", "00")
                .ClickEditSelectedStaff();

            selectStaffPopup
                .WaitForSelectStaffPopupToLoad()
                .EnterTextIntoFilterStaffByNameSearchBox("Provider_Staff_SystemUser_ " + _currentDateTimeSuffix)
                .ClickStaffRecordCellText(_systemUserEmploymentContractId1)
                .ClickStaffConfirmSelection();

            createDiaryBookingPopup
                .WaitForCreateDiaryBookingPopupToLoad()
                .InsertTextInCommentsTextArea("Automation Test")
                .ClickCreateBooking();

            System.Threading.Thread.Sleep(7000);
            var careProviderDiaryBooking = dbHelper.cPBookingDiary.GetByLocationId(_providerId);
            Assert.AreEqual(1, careProviderDiaryBooking.Count);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ValidateDiaryBookingIsPresent(careProviderDiaryBooking[0].ToString());

            #endregion

        }

        #endregion

    }
}

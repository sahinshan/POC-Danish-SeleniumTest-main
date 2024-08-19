using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class PersonContracts_UITestCases : FunctionalTest
    {
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                string user = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(user)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PersonContracts BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PersonContracts T1", null, _businessUnitId, "907678", "PersonContractsT1@careworkstempmail.com", "PersonContracts T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "PersonContractsUser1";
                _systemUserFullName = "PersonContracts User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "PersonContracts", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

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

        internal void CreateCPBookingSchedule(Guid TeamID, Guid BookingTypeId, Guid ProviderID, Guid SystemUserEmploymentContractId, Guid SystemUserId)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
        }

        #region https://advancedcsg.atlassian.net/browse/ACC-922

        [TestProperty("JiraIssueID", "ACC-2414")]
        [Description("Step(s) 1 to 3 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod01()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad();

            #endregion

            #region Step 2

            personContractRecordPage
                .ValidateIdText("")
                .ValidatePersonLinkText(_person_fullName)
                .ValidateResponsibleUserLinkText(_systemUserFullName)
                .ValidateResponsibleTeamLinkText("PersonContracts T1");

            #endregion

            #region Step 3

            personContractRecordPage
                .ValidateEstablishmentLinkTextVisible(false)
                .ValidateContractSchemeLinkTextVisible(true)
                .ValidateFunderLinkTextVisible(true)
                .ValidatePersonContractIsEnabledForScheduledBookings_NoRadioButtonChecked()
                .ValidatePersonContractIsEnabledForScheduledBookings_FieldLabelTooltip("This flag enables Scheduled Bookings to be linked to this Person Contract record.");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2416")]
        [Description("Step(s) 4 to 10 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod02()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType);

            var provider2Name = "Master Organisation " + _currentDateSuffix;
            var provider2Type = 15; //Master Organisation
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(
                _teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractSchemeId,
                careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion


            #region Step 4

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(providerName, providerId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractSchemeName, careProviderContractSchemeId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidatePersonContractIsEnabledForScheduledBookings_NoRadioButtonChecked();

            dbHelper.careProviderContractScheme.UpdateDefaultAllPersonContractsEnabledForScheduledBookings(careProviderContractSchemeId, true);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeClearButton()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractSchemeName, careProviderContractSchemeId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidatePersonContractIsEnabledForScheduledBookings_YesRadioButtonChecked();

            #endregion

            #region Step 5

            personContractRecordPage
                .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(provider2Name).TapSearchButton().ValidateResultElementNotPresent(provider2Id).ClickCloseButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad();

            #endregion

            #region Step 6

            personContractRecordPage
                .ClickEstablishmentClearButton()
                .ValidateContractSchemeLookupButtonDisabled(true);

            #endregion

            #region Step 7

            personContractRecordPage
                .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(providerName, providerId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractSchemeName, careProviderContractSchemeId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateFunderLinkText(providerName);

            #endregion

            #region Step 8

            personContractRecordPage
                .ValidateEndDateFieldVisible(false)
                .ValidateEndReasonFieldVisible(false);

            #endregion

            #region Step 9

            personContractRecordPage
                .InsertTextOnNoteText("Final notes line 1\r\nFinal notes line 2\r\nFinal notes line 3\r\nFinal notes line 4");

            #endregion

            #region Step 10

            personContractRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateNotificationAreaVisible(false)
                .ClickSaveAndCloseButton()
                .ValidateNotificationAreaVisible(true)
                .ValidateNotificationAreaText("Some data is not correct. Please review the data in the Form.");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2421")]
        [Description("Step(s) 11 to 12 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod03()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, true);

            var provider2Name = "Master Organisation " + _currentDateSuffix;
            var provider2Type = 15; //Master Organisation
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(
                _teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractSchemeId,
                careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion


            #region Step 11

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(providerName, providerId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractSchemeName, careProviderContractSchemeId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnStartDate("01/06/2023")
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("PersonContracts T1", _teamId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickRefreshButton();

            var personContractRecords = dbHelper.careProviderPersonContract.GetBypersonId(_personId);
            Assert.AreEqual(1, personContractRecords.Count);
            var personContractRecordId = personContractRecords.First();
            var personContractRecordNumber = (int)(dbHelper.careProviderPersonContract.GetByID(personContractRecordId, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]);

            personContractsPage
                .OpenRecord(personContractRecordId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateIdText(personContractRecordNumber.ToString())
                .ValidateResponsibleTeamLinkText("PersonContracts T1")
                .ValidateResponsibleUserLinkText("PersonContracts User1")
                .ValidatePersonLinkText(_person_fullName);

            personContractRecordPage
                .ValidateEstablishmentLinkText(providerName)
                .ValidateContractSchemeLinkText(contractSchemeName)
                .ValidateFunderLinkText(providerName)
                .ValidatePersonContractIsEnabledForScheduledBookings_NoRadioButtonChecked();

            personContractRecordPage
                .ValidateStartDateText("01/06/2023")
                .ValidateEndDateTimeText("")
                .ValidateEndDateTime_TimeText("")
                .ValidateEndReasonLinkText("")
                .ValidateNotifiedOfEndingDateTimeText("")
                .ValidateNotifiedOfEndingDateTime_TimeText("");

            personContractRecordPage
                .ValidateNoteTextText("");

            #endregion

            #region Step 12

            personContractRecordPage
                .ClickBackButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(providerName, providerId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractSchemeName, careProviderContractSchemeId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnStartDate("01/06/2023")
                .ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("PersonContracts T1", _teamId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("There are already existing records for this Person with the same Contract Scheme which overlap. Change the Contract Scheme or Start / End date values to avoid overlap.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2422")]
        [Description("Step(s) 13 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod04()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var provider2Name = "Master Organisation " + _currentDateSuffix;
            var provider2Type = 15; //Master Organisation
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(
                _teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractSchemeId,
                careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion


            #region Step 13

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(providerName, providerId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractSchemeName, careProviderContractSchemeId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractIsEnabledForScheduledBookings_YesRadioButton()
                .InsertTextOnStartDate("01/06/2023").ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("PersonContracts T1", _teamId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("It is not permitted to create a Person Contract record that enables Scheduled Bookings to be linked, but the associated Provider is not enabled for Scheduling. Please correct as necessary.")
                .TapCloseButton();

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2419")]
        [Description("Step(s) 14 to 15 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod05()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, true);

            var provider2Name = "Master Organisation " + _currentDateSuffix;
            var provider2Type = 15; //Master Organisation
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(
                _teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractSchemeId,
                careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId, providerId, new DateTime(2023, 6, 12));

            #endregion

            #region Booking Type

            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BookingType-1", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false);

            #endregion

            #region providerallowablebookingtypes

            dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId, providerId, _bookingType1, true);

            #endregion

            #region Care provider staff role type

            var _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_teamId, "Express Book - Role 1", "9999", null, new DateTime(2020, 1, 1), null);

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeid = commonMethodsDB.CreateEmploymentContractType(_teamId, "Contracted", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Recurrence Patterns

            _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
            _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
            _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
            _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
            _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
            _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
            _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();

            #endregion

            #region Availability Type

            var _availabilityTypes_StandardId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();

            #endregion

            #region System User Record

            var _newSystemUserName = "PCU_" + _currentDateSuffix;
            var _newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_newSystemUserName, "Person Contracts", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_newSystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_newSystemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_newSystemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypes_StandardId);

            #endregion

            #region Booking Schedule

            var _cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_teamId, _bookingType1, 1, 1, 1, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0), providerId, "Express Book Testing");

            #endregion

            #region Booking Schedule Staff

            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_teamId, _cpBookingScheduleId, _systemUserEmploymentContractId, _newSystemUserId);

            #endregion

            #region Schedule Booking To People

            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(_teamId, _cpBookingScheduleId, _personId, careProviderPersonContractId, careProviderContractServiceId);

            #endregion


            #region Step 14

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidatePersonContractIsEnabledForScheduledBookings_YesRadioButtonChecked()
                .ValidatePersonContractIsEnabledForScheduledBookingsDisabled(true);

            #endregion

            #region Step 15

            personContractRecordPage
                .ValidatePageHeaderText("Person Contract:\r\n" + _person_fullName + " \\ " + providerName + " \\ " + providerName + " \\ " + contractSchemeName + " \\ 12/06/2023");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2420")]
        [Description("Step(s) 16 to 19 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod06()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var provider2Name = "Master Organisation " + _currentDateSuffix;
            var provider2Type = 15; //Master Organisation
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(
                _teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractSchemeId,
                careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId, providerId, new DateTime(2023, 6, 12));

            #endregion

            #region Care Provider Person Contract End Reason

            var careProviderPersonContractEndReasonId = dbHelper.careProviderPersonContractEndReason.GetByName("Change of contract")[0];
            var inactiveCareProviderPersonContractEndReasonId = commonMethodsDB.CreateCareProviderPersonContractEndReason("Inactive End Reason", new DateTime(2020, 1, 1), 999999, _teamId, true);

            #endregion


            #region Step 16

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContractId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateEndReasonLookupButtonDisabled(true)
                .InsertTextOnEndDateTime("12/06/2023")
                .ValidateEndReasonLookupButtonDisabled(false)
                .InsertTextOnEndDateTime("")
                .ValidateEndReasonLookupButtonDisabled(true);

            personContractRecordPage
                .InsertTextOnStartDate("")
                .ValidateEndDateTimeFieldDisabled(true)
                .ValidateEndReasonLookupButtonDisabled(true)
                .InsertTextOnStartDate("12/06/2023")
                .ValidateEndDateTimeFieldDisabled(false)
                .ValidateEndReasonLookupButtonDisabled(true);

            #endregion

            #region Step 17

            personContractRecordPage
                .InsertTextOnStartDate("12/06/2023")
                .InsertTextOnEndDateTime("12/06/2023")
                .InsertTextOnEndDateTime_Time("19:45")
                .ClickEndReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Change of contract", careProviderPersonContractEndReasonId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateEndReasonLinkText("Change of contract")
                .InsertTextOnStartDate("")
                .ValidateEndDateTimeFieldDisabled(true)
                .ValidateEndReasonLookupButtonDisabled(true)
                .ValidateEndReasonLinkText("");

            personContractRecordPage
                .InsertTextOnStartDate("12/06/2023")
                .InsertTextOnEndDateTime("12/06/2023")
                .InsertTextOnEndDateTime_Time("19:45")
                .ClickEndReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Change of contract", careProviderPersonContractEndReasonId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateEndReasonLinkText("Change of contract")
                .InsertTextOnEndDateTime("")
                .ValidateEndReasonLookupButtonDisabled(true)
                .ValidateEndReasonLinkText("");

            #endregion

            #region Step 18

            personContractRecordPage
                .InsertTextOnEndDateTime("12/06/2023")
                .ClickEndReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad()
                .TypeSearchQuery("Change of contract").TapSearchButton().ValidateResultElementPresent(careProviderPersonContractEndReasonId)
                .TypeSearchQuery("Inactive End Reason").TapSearchButton().ValidateResultElementNotPresent(inactiveCareProviderPersonContractEndReasonId)
                .ClickCloseButton();

            #endregion

            #region Step 19

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnStartDate("13/06/2023")
                .InsertTextOnEndDateTime("12/06/2023");

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("End Date must be after the Start Date.").TapCloseButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickSaveButton();

            dynamicDialogPopup.WaitForDynamicDialogPopupToLoad().ValidateMessage("End Date must be after the Start Date.").TapCloseButton();

            #endregion


        }

        [TestProperty("JiraIssueID", "ACC-2425")]
        [Description("Step(s) 20 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod07()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var provider2Name = "Master Organisation " + _currentDateSuffix;
            var provider2Type = 15; //Master Organisation
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            contractSchemeName = "Default2_" + _currentDateSuffix;
            contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            contractSchemeName = "Default3_" + _currentDateSuffix;
            contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme3Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            contractSchemeName = "Default4_" + _currentDateSuffix;
            contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme4Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractScheme1Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractScheme2Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractScheme3Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractScheme4Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, providerId, new DateTime(2023, 6, 8), new DateTime(2023, 6, 9));
            var careProviderPersonContract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme2Id, providerId, new DateTime(2023, 6, 8), new DateTime(2023, 6, 10));
            var careProviderPersonContract3Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme3Id, providerId, new DateTime(2023, 6, 14));
            var careProviderPersonContract4Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme4Id, providerId, new DateTime(2023, 6, 10));

            #endregion

            #region Step 20

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ValidateRecordPosition(1, careProviderPersonContract3Id.ToString())//  14/06/2023 
                .ValidateRecordPosition(2, careProviderPersonContract4Id.ToString())//  10/06/2023 
                .ValidateRecordPosition(3, careProviderPersonContract2Id.ToString())//  08/06/2023 - 10/06/2023
                .ValidateRecordPosition(4, careProviderPersonContract1Id.ToString())//  08/06/2023 - 09/06/2023
                ;

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2423")]
        [Description("Step(s) 21 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod08()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment A " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var provider2Name = "Residential Establishment B " + _currentDateSuffix;
            var provider2Type = 13; //Residential Establishment
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

            var provider3Name = "Residential Establishment C " + _currentDateSuffix;
            var provider3Type = 13; //Residential Establishment
            var provider3Id = commonMethodsDB.CreateProvider(provider3Name, _teamId, provider3Type, true);

            var provider4Name = "Residential Establishment D " + _currentDateSuffix;
            var provider4Type = 13; //Residential Establishment
            var provider4Id = commonMethodsDB.CreateProvider(provider4Name, _teamId, provider4Type, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            contractSchemeName = "Default2_" + _currentDateSuffix;
            contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, provider3Id, provider4Id);

            contractSchemeName = "Default3_" + _currentDateSuffix;
            contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme3Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            contractSchemeName = "Default4_" + _currentDateSuffix;
            contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme4Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractScheme1Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", provider3Id, provider4Id, careProviderContractScheme2Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractScheme3Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractScheme4Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, provider2Id, new DateTime(2023, 6, 8), new DateTime(2023, 6, 9));
            var careProviderPersonContract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, provider3Id, careProviderContractScheme2Id, provider4Id, new DateTime(2023, 6, 8), new DateTime(2023, 6, 10));
            var careProviderPersonContract3Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme3Id, providerId, new DateTime(2023, 6, 14));
            var careProviderPersonContract4Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme4Id, providerId, new DateTime(2023, 6, 10));

            #endregion

            #region Step 21

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()

                .SearchRecord(providerName) //Search by an establishment
                .ValidateRecordPresent(careProviderPersonContract1Id, true)
                .ValidateRecordPresent(careProviderPersonContract2Id, false)
                .ValidateRecordPresent(careProviderPersonContract3Id, true)
                .ValidateRecordPresent(careProviderPersonContract4Id, true);

            personContractsPage
                .SearchRecord("Default1_" + _currentDateSuffix) //Search by a Contract Scheme name
                .ValidateRecordPresent(careProviderPersonContract1Id, true)
                .ValidateRecordPresent(careProviderPersonContract2Id, false)
                .ValidateRecordPresent(careProviderPersonContract3Id, false)
                .ValidateRecordPresent(careProviderPersonContract4Id, false);

            personContractsPage
                .SearchRecord(provider4Name) //Search by a Funder name
                .ValidateRecordPresent(careProviderPersonContract1Id, false)
                .ValidateRecordPresent(careProviderPersonContract2Id, true)
                .ValidateRecordPresent(careProviderPersonContract3Id, false)
                .ValidateRecordPresent(careProviderPersonContract4Id, false);


            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2424")]
        [Description("Step(s) 22 to 23 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod09()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var provider2Name = "Residential Establishment " + _currentDateSuffix;
            var provider2Type = 13; //Residential Establishment
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractScheme1Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, provider2Id, new DateTime(2023, 6, 8), new DateTime(2023, 6, 9));
            var careProviderPersonContractNumber = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContract1Id, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]).ToString();

            #endregion

            #region Step 22

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("Person Contracts")

                .SelectFilter("1", "Person")
                .SelectFilter("1", "Id")
                .SelectFilter("1", "Responsible Team")
                .SelectFilter("1", "Responsible User")
                .SelectFilter("1", "Establishment")
                .SelectFilter("1", "Contract Scheme")
                .SelectFilter("1", "Funder")
                .SelectFilter("1", "Start Date")
                .SelectFilter("1", "End Date/Time")
                .SelectFilter("1", "End Reason")
                .SelectFilter("1", "Person Contract Is Enabled For Scheduled Bookings?");

            #endregion

            #region Step 23

            advanceSearchPage
                .SelectFilter("1", "Person")
                .SelectOperator("1", "Equals")
                .ClickRuleValueLookupButton("1");

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("All Active People").SearchAndSelectRecord(_personNumber.ToString(), _personId);

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Id")
                .ResultsPageValidateHeaderCellText(3, "Person")
                .ResultsPageValidateHeaderCellText(4, "Establishment")
                .ResultsPageValidateHeaderCellText(5, "Contract Scheme")
                .ResultsPageValidateHeaderCellText(6, "Funder")
                .ResultsPageValidateHeaderCellText(7, "Start Date")
                .ResultsPageValidateHeaderCellText(8, "End Date/Time")
                .ResultsPageValidateHeaderCellText(10, "Person Contract Is Enabled For Scheduled Bookings?");

            advanceSearchPage
                .ValidateSearchResultRecordCellContent(careProviderPersonContract1Id.ToString(), 2, careProviderPersonContractNumber)
                .ValidateSearchResultRecordCellContent(careProviderPersonContract1Id.ToString(), 3, _person_fullName)
                .ValidateSearchResultRecordCellContent(careProviderPersonContract1Id.ToString(), 4, providerName)
                .ValidateSearchResultRecordCellContent(careProviderPersonContract1Id.ToString(), 5, contractSchemeName)
                .ValidateSearchResultRecordCellContent(careProviderPersonContract1Id.ToString(), 6, provider2Name)
                .ValidateSearchResultRecordCellContent(careProviderPersonContract1Id.ToString(), 7, "08/06/2023")
                .ValidateSearchResultRecordCellContent(careProviderPersonContract1Id.ToString(), 8, "09/06/2023 00:00:00")
                .ValidateSearchResultRecordCellContent(careProviderPersonContract1Id.ToString(), 10, "No");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ValidateRecordCellText(careProviderPersonContract1Id.ToString(), 2, careProviderPersonContractNumber)
                .ValidateRecordCellText(careProviderPersonContract1Id.ToString(), 3, providerName)
                .ValidateRecordCellText(careProviderPersonContract1Id.ToString(), 4, contractSchemeName)
                .ValidateRecordCellText(careProviderPersonContract1Id.ToString(), 5, "No")
                .ValidateRecordCellText(careProviderPersonContract1Id.ToString(), 6, provider2Name)
                .ValidateRecordCellText(careProviderPersonContract1Id.ToString(), 7, "08/06/2023")
                .ValidateRecordCellText(careProviderPersonContract1Id.ToString(), 8, "09/06/2023 00:00:00")
                .ValidateRecordCellText(careProviderPersonContract1Id.ToString(), 9, "No");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2417")]
        [Description("Step(s) 24 to 26 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod10()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, true);

            var provider2Name = "Master Organisation " + _currentDateSuffix;
            var provider2Type = 10; //Local Authority
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, providerId);

            var contractScheme2Name = "Default2_" + _currentDateSuffix;
            var contractScheme2Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme2Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractScheme2Name, new DateTime(2020, 1, 1), contractScheme2Code, providerId, provider2Id);


            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractSchemeId, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);
            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, providerId, careProviderContractScheme2Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion


            #region Step 24

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(providerName, providerId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(contractSchemeName, careProviderContractSchemeId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnStartDate("01/06/2023").ClickResponsibleTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("PersonContracts T1", _teamId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickRefreshButton();

            var personContractRecords = dbHelper.careProviderPersonContract.GetBypersonId(_personId);
            Assert.AreEqual(1, personContractRecords.Count);
            var personContractRecordId = personContractRecords.First();
            var personContractRecordNumber = (int)(dbHelper.careProviderPersonContract.GetByID(personContractRecordId, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]);


            personContractsPage
                .OpenRecord(personContractRecordId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnNoteText("Updated Value")
                .ClickSaveAndCloseButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickRefreshButton()
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(personContractRecordId);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .NavigateToAuditPage();

            auditListPage
                .WaitForAuditListPageToLoad("careproviderpersoncontract")
                .ValidateCellText(1, 2, "Updated")
                .ValidateCellText(2, 2, "Created");

            #endregion

            #region Step 25

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPersonContractsSection();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace()

                .GridHeaderIdFieldLink()//Sort by Id ascending
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .GridHeaderIdFieldLink()//Sort by Id descending

                .ValidateRecordCellText(personContractRecordId.ToString(), 2, personContractRecordNumber.ToString())
                .ValidateRecordCellText(personContractRecordId.ToString(), 3, _person_fullName)
                .ValidateRecordCellText(personContractRecordId.ToString(), 4, providerName)
                .ValidateRecordCellText(personContractRecordId.ToString(), 5, contractSchemeName)
                .ValidateRecordCellText(personContractRecordId.ToString(), 6, "No")
                .ValidateRecordCellText(personContractRecordId.ToString(), 7, providerName)
                .ValidateRecordCellText(personContractRecordId.ToString(), 8, "01/06/2023")
                .ValidateRecordCellText(personContractRecordId.ToString(), 9, "")
                .ValidateRecordCellText(personContractRecordId.ToString(), 10, "No");

            personContractsPage
                .ClickNewRecordButton();

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidatePersonLinkFieldVisible(false);

            #endregion

            #region Step 26

            personContractRecordPage
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().TapOKButton();

            personContractsPage
                .WaitForPersonContractsPageToLoadFromWorkplace();

            personContractsPage
                .SelectViewSelector("Private Care")
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .GridHeaderIdFieldLink()//Sort by Id ascending
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .GridHeaderIdFieldLink()//Sort by Id descending
                .WaitForPersonContractsPageToLoadFromWorkplace();

            personContractsPage
                .ValidateRecordCellText(personContractRecordId.ToString(), 2, personContractRecordNumber.ToString())
                .ValidateRecordCellText(personContractRecordId.ToString(), 3, _person_fullName)
                .ValidateRecordCellText(personContractRecordId.ToString(), 4, providerName)
                .ValidateRecordCellText(personContractRecordId.ToString(), 5, contractSchemeName)
                .ValidateRecordCellText(personContractRecordId.ToString(), 6, "No")
                .ValidateRecordCellText(personContractRecordId.ToString(), 7, providerName)
                .ValidateRecordCellText(personContractRecordId.ToString(), 8, "01/06/2023")
                .ValidateRecordCellText(personContractRecordId.ToString(), 9, "")
                .ValidateRecordCellText(personContractRecordId.ToString(), 10, "No");

            #region Care Provider Person Contract

            var careProviderPersonContract2Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme2Id, provider2Id, new DateTime(2023, 6, 8), new DateTime(2023, 6, 9));
            var careProviderPersonContract2Number = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContract2Id, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]).ToString();

            #endregion

            personContractsPage
                .SelectViewSelector("Funded Care")
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .GridHeaderIdFieldLink()//Sort by Id ascending
                .WaitForPersonContractsPageToLoadFromWorkplace()
                .GridHeaderIdFieldLink()//Sort by Id descending
                .WaitForPersonContractsPageToLoadFromWorkplace();

            personContractsPage
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 2, careProviderPersonContract2Number.ToString())
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 3, _person_fullName)
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 4, providerName)
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 5, contractScheme2Name)
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 6, "No")
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 7, provider2Name)
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 8, "08/06/2023")
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 9, "09/06/2023 00:00:00")
                .ValidateRecordCellText(careProviderPersonContract2Id.ToString(), 10, "No");

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-2418")]
        [Description("Step(s) 27 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod11()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var provider2Name = "Residential Establishment " + _currentDateSuffix;
            var provider2Type = 13; //Residential Establishment
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractScheme1Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, provider2Id, new DateTime(2023, 6, 8), new DateTime(2023, 6, 9));
            var careProviderPersonContractNumber = (dbHelper.careProviderPersonContract.GetByID(careProviderPersonContract1Id, "careproviderpersoncontractnumber")["careproviderpersoncontractnumber"]).ToString();

            #endregion


            #region Step 27

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .SelectRecord(careProviderPersonContract1Id.ToString())
                .ClickPinButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContract1Id.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickUnpinButton();

            personContractsPage
                .WaitForPersonContractsPageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(careProviderPersonContract1Id.ToString(), false);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2415")]
        [Description("Step(s) 28 to 31 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Person Contracts")]
        [TestProperty("Screen", "Person Contracts")]
        public void PersonContracts_UITestMethod12()
        {
            #region Person

            var firstName = "Atley";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, false);

            var provider2Name = "Residential Establishment " + _currentDateSuffix;
            var provider2Type = 13; //Residential Establishment
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, false);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default1_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

            #endregion

            #region Care Provider Service

            var careProviderServiceId = commonMethodsDB.CreateCareProviderService(_teamId, "Default Care Provider Service", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Rate Unit

            var careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_teamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Care Provider Batch Grouping

            var careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_teamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            var careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Contract Service

            var careProviderContractServiceID = dbHelper.careProviderContractService.CreateCareProviderContractService(_teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractScheme1Id, careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContract1Id = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractScheme1Id, provider2Id, new DateTime(2023, 6, 8), null);

            #endregion

            #region Person Contract Service End Reason

            var careProviderPersonContractServiceEndReasonId = commonMethodsDB.CreateCareProviderPersonContractServiceEndReason(_teamId, "Default Person Contract Service End Reason", new DateTime(2020, 1, 1), 999999);

            #endregion

            #region Care Provider Person Contract Service

            var careProviderPersonContractServiceId = dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(careProviderPersonContract1Id, _teamId,
                careProviderContractScheme1Id, careProviderServiceId, careProviderContractServiceID,
                new DateTime(2023, 6, 11), 1,
                1, careProviderRateUnitId);

            dbHelper.careProviderPersonContractService.UpdateEndDateTime(careProviderPersonContractServiceId, new DateTime(2023, 6, 12, 9, 30, 0), careProviderPersonContractServiceEndReasonId);

            #endregion

            #region Step 28

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .OpenRecord(careProviderPersonContract1Id.ToString());

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidateEstablishmentLookupButtonDisabled(true)
                .ValidateContractSchemeLookupButtonDisabled(true)
                .ValidateFunderLookupButtonDisabled(true);

            #endregion

            #region Person Absence Reason

            var cpPersonAbsenceReasonId = commonMethodsDB.CreateCPPersonAbsenceReason(_teamId, "Hospital", new DateTime(2020, 1, 1), 123);

            #endregion

            #region Person Absence

            var personContractIds = new System.Collections.Generic.List<Guid> { careProviderPersonContract1Id };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_teamId, _personId, new DateTime(2023, 6, 10), personContractIds, cpPersonAbsenceReasonId, new DateTime(2023, 6, 10));

            #endregion



            #region Step 29

            personContractRecordPage
                .ClickPersonAbsenceTab();

            personAbsencesPage
                .WaitForPersonAbsencesPageToLoadInsidePersonContractPage()
                .ValidateRecordPresent(cpPersonAbsenceId, true);

            #endregion

            #region Step 30

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickFinanceTransactionTab();

            financeTransactionsPage
                .WaitForFinanceTransactionsPageToLoadInsidePersonContractPage("careproviderpersoncontract")
                .ValidateHeaderCellText(3, "Payer")
                .ValidateHeaderCellText(4, "Funder")
                .ValidateHeaderCellText(5, "Establishment")
                .ValidateHeaderCellText(2, "Person")
                .ValidateHeaderCellText(6, "Contract Scheme")
                .ValidateHeaderCellText(7, "Service")
                .ValidateHeaderCellText(8, "Booking Type")
                .ValidateHeaderCellText(9, "Transaction No")
                .ValidateHeaderCellText(10, "Start Date")
                .ValidateHeaderCellText(11, "Start Time")
                .ValidateHeaderCellText(12, "End Date")
                .ValidateHeaderCellText(13, "End Time")
                .ValidateHeaderCellText(14, "Net Amount")
                .ValidateHeaderCellText(15, "VAT Amount")
                .ValidateHeaderCellText(16, "Gross Amount")
                .ValidateHeaderCellText(17, "Finance Code")
                .ValidateHeaderCellText(18, "Rate Unit")
                .ValidateHeaderCellText(19, "Total Units")
                .ValidateHeaderCellText(20, "Transaction Class")
                .ValidateHeaderCellText(21, "Invoice Number")
                .ValidateHeaderCellText(22, "Finance Invoice Status [Finance Invoice]")
                .ValidateHeaderCellText(23, "Extract No")
                .ValidateHeaderCellText(24, "Apportioned?")
                .ValidateHeaderCellText(25, "Confirmed?")
                .ValidateHeaderCellText(26, "Full Net Amount")
                .ValidateHeaderCellText(27, "For Information Only?")
                .ValidateHeaderCellText(28, "Has Contra?")
                .ValidateHeaderCellText(29, "Modified On")
                .ValidateHeaderCellText(30, "Created On");

            financeTransactionsPage
                .SelectAvailableViewByText("All")

                .WaitForFinanceTransactionsPageToLoadInsidePersonContractPage("careproviderpersoncontract")
                .SelectAvailableViewByText("Actuals")

                .WaitForFinanceTransactionsPageToLoadInsidePersonContractPage("careproviderpersoncontract")
                .SelectAvailableViewByText("Commitments");

            #endregion

            #region Step 31

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickDetailsTab()
                .InsertTextOnEndDateTime("09/06/2023")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("End date must be later than or equal to 12/06/2023 09:30:00")
                .TapCloseButton();

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














using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.PersonAbsence
{
    /// <summary>
    /// This class contains Automated UI test scripts for Person Contract Flag Booking
    /// </summary>
    [TestClass]
    public class PersonAbsence_PersonContractFlag_Booking_UITestCases : FunctionalTest
    {
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _adminSystemUserId;
        private string _systemUsername;
        private string _adminSystemUsername;
        private Guid _bookingType5;
        private Guid _bookingType6;
        private Guid _providereId1;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _schedulingsetupid = new Guid("8B61B44F-B485-EC11-A350-0050569231CF");//Scheduling setup id
        private Guid _cpPersonabsencereasonid;
        private string _cpPersonabsencereasonName;
        private Guid _contractschemeid;
        private Guid _personcontractId;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid1;
        private Guid _availabilityTypes_StandardId;
        private Guid _recurrencePattern_Every1WeekMondayId;
        private Guid _recurrencePattern_Every1WeekTuesdayId;
        private Guid _recurrencePattern_Every1WeekWednesdayId;
        private Guid _recurrencePattern_Every1WeekThursdayId;
        private Guid _recurrencePattern_Every1WeekFridayId;
        private Guid _recurrencePattern_Every1WeekSaturdayId;
        private Guid _recurrencePattern_Every1WeekSundayId;
        private Guid _recurrencePatternId_EveryDay;



        [TestInitialize()]
        public void PersonAbsence_SetupTest()
        {

            try
            {
                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Marital Status

                _maritalStatusId = commonMethodsDB.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "PersonCarePlan_Ethnicity", new DateTime(2020, 1, 1));

                #endregion

                #region SecurityProfiles

                var userSecProfiles = new List<Guid>();

                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Bed Management (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Bed Management Setup (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Create Person Absences")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Create Person Contract and Person Contract Services")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Alert/Hazard Module (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("CAMT Integration")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can Access Customizations")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View People We Support")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Planning (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Availability Type View Only")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Plan Forms (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Scheduling Setup (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Demographics (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Rostering (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Delete Booking Diary")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Delete Booking Schedules")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Provider (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Qualified to Authorise Care Plans")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Open Ended Absence (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System User Open Ended Absence (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Tracking")[0]);

                #endregion

                #region Create SystemUser 

                _systemUsername = "PersonContractFlag" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceProvisions", "User4", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);

                #endregion


                #region Booking Type 5 -> "Booking (to service user)" 

                _bookingType5 = commonMethodsDB.CreateCPBookingType("BookingType-555", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, 1440);

                _bookingType6 = commonMethodsDB.CreateCPBookingType("BookingType-666", 6, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, 1440);
                dbHelper.cpBookingType.UpdateIsAbsence(_bookingType6, true);

                #endregion

                #region update Care Provider scheduling set up

                dbHelper.cPSchedulingSetup.UpdateCPScheduleSetup(_schedulingsetupid, _bookingType6);

                #endregion

                #region Provider 1

                _providereId1 = commonMethodsDB.CreateProvider("ProviderContract-002" + DateTime.Now.ToString("yyyyMMdd"), _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType6, false);

                #endregion

                #region Person

                var firstName = "Person_Absence" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "LN_CDV6_24104";
                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();

                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                _personFullName = firstName + lastName;

                #endregion

                #region create contract scheme
                var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
                _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-002" + DateTime.Now.ToString("yyyyMMdd"), new DateTime(2000, 1, 2), contractSchemeCode, _providereId1, _providereId1);

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-001", "1234", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type - Salaried

                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));

                #endregion

                #region Create Care Provider Person Absence Reason

                _cpPersonabsencereasonName = "Person Absence Reason Hospital";
                _cpPersonabsencereasonid = commonMethodsDB.CreateCPPersonAbsenceReason(_cpPersonabsencereasonName, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, 123, DateTime.Today);

                #endregion

                #region Availability Type
                _availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careDirectorQA_TeamId, 1, 1, true);


                #endregion

                #region Recurrence Patterns

                _recurrencePattern_Every1WeekMondayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on monday").First();
                _recurrencePattern_Every1WeekTuesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on tuesday").First();
                _recurrencePattern_Every1WeekWednesdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on wednesday").First();
                _recurrencePattern_Every1WeekThursdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on thursday").First();
                _recurrencePattern_Every1WeekFridayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on friday").First();
                _recurrencePattern_Every1WeekSaturdayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on saturday").First();
                _recurrencePattern_Every1WeekSundayId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 week(s) on sunday").First();
                var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
                if (!recurrencePatternExists)
                    _recurrencePatternId_EveryDay = dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);


                _recurrencePatternId_EveryDay = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").First();

                #endregion

                #region Care Provider Scheduling Setup

                var cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCheckStaffAvailability(cPSchedulingSetupId, 4); //Check and Offer Create

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        internal DateTime GetThisWeekFirstWednesday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-4810

        [TestProperty("JiraIssueID", "ACC-387")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-387 - " +
            "Verify system behaviour while creating booking from Person Absence when Person Contract flag is set to No .Pre-Condition-> Person record should exist-> Person record should not have any existing person contracts")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Person Contracts")]
        [TestProperty("BusinessModule3", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "Person Contracts")]
        [TestProperty("Screen3", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBookingPersonContractFlag_01()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            #endregion

            #region Step 2

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            #endregion

            #region step 3

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                 .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(provider, _providereId1);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Contract-Scheme-002" + DateTime.Now.ToString("yyyyMMdd"), _contractschemeid);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidatePersonContractIsEnabledForScheduledBookings_NoRadioButtonChecked()
                .InsertTextOnStartDate("01/06/2023")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            #endregion

            var personContractRecords = dbHelper.careProviderPersonContract.GetBypersonId(_personID);
            var personContractRecordId = personContractRecords.First();
            var personcontractname = (string)(dbHelper.careProviderPersonContract.GetByID(personContractRecordId, "title")["title"]);

            #region step 4 and 5

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickRefreshButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery(_cpPersonabsencereasonName)
                .TapSearchButton()
                .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .InsertHealthAbsenceNotifiedTime("10:00")
                .ClickHealthAbsenceSave();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .ValidateMessage("The following contracts are not enabled for Scheduled Bookings. An absence booking will not be created for these contracts:" + "\r\n" + personcontractname.ToString())
                .TapCancelButton();

            #endregion

            #region step 6

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceSave();

            confirmDynamicDialogPopup
                .WaitForConfirmDynamicDialogPopupToLoad()
                .TapOKButton();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceCloseButton();

            #endregion

            #region step 7

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleDiarySection();

            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");

            peopleDiaryPage
                 .WaitForPeopleDiaryPageToLoad()
                 .selectProvider(provider + " - No Address")
                 .WaitForPeopleDiaryPageToLoad()
                 .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));
            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ValidateDiaryBooking(_personcontractId.ToString());

            #endregion
        }

        [TestProperty("JiraIssueID", "ACC-386")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-386 - " +
            "Verify system behaviour while creating booking from Person Absence when Person Contract flag is set to Yes .Pre-Condition-> Person record should exist-> Person record should not have any existing person contracts")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("BusinessModule2", "Person Contracts")]
        [TestProperty("BusinessModule3", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Person Absences")]
        [TestProperty("Screen2", "Person Contracts")]
        [TestProperty("Screen3", "People Diary")]
        public void PersonAbsence_VerifyAbsenceBookingPersonContractFlag_02()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            #endregion

            #region Step 2

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToPersonContractsPage();

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickNewRecordButton();

            #endregion

            #region step 3

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                 .ClickEstablishmentLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(provider, _providereId1);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickContractSchemeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Contract-Scheme-002" + DateTime.Now.ToString("yyyyMMdd"), _contractschemeid);

            personContractRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ValidatePersonContractIsEnabledForScheduledBookings_NoRadioButtonChecked()
                .ClickPersonContractIsEnabledForScheduledBookings_YesRadioButton()
                .InsertTextOnStartDate("01/06/2023")
                .ClickSaveAndCloseButton();

            System.Threading.Thread.Sleep(2000);

            #endregion

            var personContractRecords = dbHelper.careProviderPersonContract.GetBypersonId(_personID);
            var personContractRecordId = personContractRecords.First();
            var personcontractname = (string)(dbHelper.careProviderPersonContract.GetByID(personContractRecordId, "title")["title"]);

            #region step 4 

            personContractsPage
                .WaitForPersonContractsPageToLoad()
                .ClickRefreshButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .NavigateToHealthPersonAbsencesPage();

            personHealthAbsencesPage
                .WaitForPersonHealthAbsencesPageToLoad()
                .ClickNewRecordButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceReasonLookupButton();

            lookupPopup
               .WaitForLookupPopupToLoad()
               .SelectLookIn("Lookup Records")
               .TypeSearchQuery(_cpPersonabsencereasonName)
               .TapSearchButton()
               .SelectResultElement(_cpPersonabsencereasonid.ToString());

            personHealthAbsencesRecordPage
               .WaitForPageToLoadInDrawerMode()
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSaveNClose();

            dynamicDialogPopup
                 .WaitForCareCloudDynamicDialoguePopUpToLoad()
                 .ValidateMessage("No bookings exist during the absence period")
                 .TapCloseButton();

            personHealthAbsencesPage
              .WaitForPersonHealthAbsencesPageToLoad()
              .ClickRefreshPage();

            //Get the Booking Diary Id
            var _cpbookingdiaryid = dbHelper.cPBookingDiary.GetCPBookingIdByCreator(_systemUserId).First();

            #endregion

            #region step 5

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToPeopleDiarySection();
            System.Threading.Thread.Sleep(1000);
            String Date = DateTime.Today.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy");
            String NextDate = DateTime.Today.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy");
            System.Threading.Thread.Sleep(1000);

            peopleDiaryPage
                 .WaitForPeopleDiaryPageToLoad()
                 .selectProvider(provider + " - No Address")
                 .WaitForPeopleDiaryPageToLoad()
                 .ClickDatePicker();

            calendarPickerPopup
                .WaitForCalendarPickerPopupToLoad()
                .ClickOnCalendarDate(Convert.ToDateTime(Date));
            
            peopleDiaryPage
                 .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(_cpbookingdiaryid.ToString())
                .ValidateTimeLabelText("Planned Time: " + Date + " 00:00 - " + NextDate + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BookingType-666");

            #endregion

        }



        #endregion
    }
}


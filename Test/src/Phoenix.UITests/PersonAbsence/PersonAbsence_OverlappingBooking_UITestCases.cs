using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.PersonAbsence
{
    /// <summary>
    /// This class contains Automated UI test scripts for Regular Care Tasks
    /// </summary>
    [TestClass]
    public class PersonAbsence_OverlappingBooking_UITestCases : FunctionalTest
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
        private Guid _cpdiarybookingid;
        private string _systemUserEmployentcontract;



        [TestInitialize()]
        public void PersonAbsence_SetupTest()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

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

                _systemUsername = "TestUser" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "ServiceProvisions", "User4", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);

                #endregion

                #region Create Admin SystemUser 


                _adminSystemUsername = "AdminUser";
                _adminSystemUserId = commonMethodsDB.CreateSystemUserRecord(_adminSystemUsername, "AdvancedSearch", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
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

                _providereId1 = commonMethodsDB.CreateProvider("Provider-002", _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider
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

                _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), 734, _providereId1, _providereId1);

                #endregion

                #region create person contract

                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, new DateTime(2023, 6, 1));
                dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId, true);

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-001", "1234", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type - Salaried

                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));

                #endregion

                #region Create Care Provider Person Absence Reason

                _cpPersonabsencereasonName = "Person Absence Reason Hospital";
                _cpPersonabsencereasonid = commonMethodsDB.CreateCPPersonAbsenceReason(_cpPersonabsencereasonName, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, 123, commonMethodsHelper.GetDatePartWithoutCulture());

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

        #region https://advancedcsg.atlassian.net/browse/ACC-5529

        [TestProperty("JiraIssueID", "ACC-1559")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-1559 - " +
            "As a Care Coordinator verify “Review existing bookings popup “and columns on the data grid of popup when saving a person absence record with planned / actual dates and time.Pre-Condition-> Person record should exist-> Should have active person contract to create a person absence-> Create some booking of type btc5 with same person and provider\r\n\r\nBooking should be created for selecting person absence period")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_01()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);

            #endregion

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
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .ReviewExistingBookingValidateHeaderCellText(2, "Booking")
                .ReviewExistingBookingValidateHeaderCellText(3, "Booking Type")
                .ReviewExistingBookingValidateHeaderCellText(4, "Start Date")
                .ReviewExistingBookingValidateHeaderCellText(5, "Start Time")
                .ReviewExistingBookingValidateHeaderCellText(6, "End Date")
                .ReviewExistingBookingValidateHeaderCellText(7, "End Time");
        }

        [TestProperty("JiraIssueID", "ACC-5959")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5959 - " +
            "As a Care Coordinator verify bookings displayed on popup when bookings exist during selected absence period when saving a person absence record with planned / actual dates and time")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_02()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);

            #endregion

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
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();


            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .SelectCancellationOptionsByValue("some")
                .ReviewExistingBookingsValidateCellText(1, _cpdiarybookingid.ToString(), 4, DateTime.Today.ToString("yyyy'-'MM'-'dd"))
                .ReviewExistingBookingsValidateCellText(1, _cpdiarybookingid.ToString(), 5, "00:00:00")
                .ReviewExistingBookingsValidateCellText(1, _cpdiarybookingid.ToString(), 6, DateTime.Today.ToString("yyyy'-'MM'-'dd"))
                .ReviewExistingBookingsValidateCellText(1, _cpdiarybookingid.ToString(), 7, "23:00:00");


        }

        [TestProperty("JiraIssueID", "ACC-5964")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5964 - " +
           "As a Care Coordinator verify message displayed on popup when no bookings exist during selected absence period when saving a person absence record with planned / actual dates and time")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_03()
        {
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
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            dynamicDialogPopup
                .WaitForCareCloudDynamicDialoguePopUpToLoad()
                .ValidateMessage("No bookings exist during the absence period")
                .TapCloseButton();

            personHealthAbsencesRecordPage
              .WaitForPageToLoadInDrawerMode()
              .VerifyOpenEndedAbsenceNotVisible();

        }

        [TestProperty("JiraIssueID", "ACC-5968")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5968 - " +
           "As a Care Coordinator verify picklist and display options at bottom of booking cancellation pop-up when saving a person absence record with planned / actual dates and time.Automation Step 1-6")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_04()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);

            #endregion

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
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();


            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .ValidateCancellationOptionsByValue("all")
                .ValidateCancellationOptionsByValue("some")
                .ValidateCancellationOptionsByValue("none")
                .ValidateOkButtonDisabled(true)
                .SelectCancellationOptionsByValue("all")
                .ReviewExistingBookingsValidateCheckboxChecked(1, _cpdiarybookingid.ToString(), 1)
                .SelectCancellationOptionsByValue("some")
                .ClickOkButton()
                .ValidateAlertWarningText("Please select at least one booking for cancellation, or choose the option to not cancel any bookings.")
                .SelectCancellationOptionsByValue("none")
                .ValidateReviewExistingBookingsCheckboxDisabled(true, 1, _cpdiarybookingid.ToString(), 1);


        }

        [TestProperty("JiraIssueID", "ACC-5969")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5969 - " +
           "As a Care Coordinator verify picklist and display options at bottom of booking cancellation pop-up when saving a person absence record with planned / actual dates and time.Automation Step 7")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_05()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);

            #endregion

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
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();


            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .SelectCancellationOptionsByValue("some")
                .ReviewExistingBookingsSelectCheckbox(1, _cpdiarybookingid.ToString(), 1)
                .ClickOkButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceSaveNClose();

            personHealthAbsencesPage
               .WaitForPersonHealthAbsencesPageToLoad()
               .ClickRefreshPage();

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider("Provider-002 - No Address")
                .clickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                //.ClickAdminTab()
                .ValidateDateofCancellation(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .ValidateTimeofCancellation("10:00")
                .ValidateReasonofCancellation("Service User Absent")
                .ClickOnCloseButton();

        }

        [TestProperty("JiraIssueID", "ACC-5969")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-5969 - " +
           "As a Care Coordinator verify picklist and display options at bottom of booking cancellation pop-up when saving a person absence record with planned / actual dates and time.Automation Step 8")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_06()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);

            #endregion

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
               .InsertHealthAbsencePlannedStartDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(DateTime.Today.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();


            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .SelectCancellationOptionsByValue("some")
                .ReviewExistingBookingsSelectCheckbox(1, _cpdiarybookingid.ToString(), 1)
                .ClickCancelButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceSaveNClose();

        }

        [TestProperty("JiraIssueID", "ACC-6007")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6007 - " +
           "As a Care Coordinator verify the functionality of cancelling overlapping bookings when a person absence is created with planned / Actual start date & time and End date & time")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_07()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(-2);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);

            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);

            #endregion

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
               .InsertHealthAbsenceActualStartDate(todayDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(todayDate.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();


            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .SelectCancellationOptionsByValue("some")
                .ReviewExistingBookingsSelectCheckbox(1, _cpdiarybookingid.ToString(), 1)
                .ClickOkButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClickHealthAbsenceSaveNClose();

            personHealthAbsencesPage
               .WaitForPersonHealthAbsencesPageToLoad()
               .ClickRefreshPage();

            mainMenu
                .WaitForMainMenuToLoad(true, true, true, false, false, false)
                .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider("Provider-002 - No Address")
                .WaitForPeopleDiaryPageToLoad()
                .ClickPreviousDateButton()
                .WaitForPeopleDiaryPageToLoad()
                .ClickPreviousDateButton()
                .WaitForPeopleDiaryPageToLoad()
                .clickDiaryBooking(_cpdiarybookingid.ToString());

            createDiaryBookingPopup
                .WaitForEditDiaryBookingPopupPageToLoad()
                // .ClickAdminTab()
                .ValidateDateofCancellation(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .ValidateTimeofCancellation("10:00")
                .ValidateReasonofCancellation("Service User Absent")
                .ClickOnCloseButton();

        }

        [TestProperty("JiraIssueID", "ACC-6014")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6014 - " +
           "As a Care Coordinator verify system behaviour while displaying booking cancellation popup when updating an existing person absence record.Step 1-4")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_08()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = DateTime.Now.Date.AddDays(-2);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);
            #endregion

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
               .InsertHealthAbsenceActualStartDate(todayDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndDate(todayDate.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceActualEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            System.Threading.Thread.Sleep(1000);
            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .SelectCancellationOptionsByValue("some")
                .ValidateResultElementPresent(_cpdiarybookingid)
                .ClickCancelButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .InsertHealthAbsenceActualStartDate(todayDate.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
                .InsertHealthAbsenceActualEndDate(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickHealthAbsenceSave();
            System.Threading.Thread.Sleep(1000);
            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .SelectCancellationOptionsByValue("some")
                .ValidateResultElementPresent(_cpdiarybookingid)
                .ClickCancelButton();



        }


        [TestProperty("JiraIssueID", "ACC-6015")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6015 - " +
          "As a Care Coordinator verify system behaviour while displaying booking cancellation popup when updating an existing person absence record.Step 6")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_09()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = DateTime.Now.Date.AddDays(-2);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);
            #endregion

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
               .InsertHealthAbsencePlannedStartDateTime(todayDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(todayDate.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();


            reviewExistingBookingsPopUp
                .WaitForPopUpToLoadInDrawerMode()
                .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                .SelectCancellationOptionsByValue("some")
                .ValidateResultElementPresent(_cpdiarybookingid)
                .ClickCancelButton();

            personHealthAbsencesRecordPage
                .WaitForPageToLoadInDrawerMode()
                .ClearHealthAbsencePlannedEndDateTime()
                .ClickHealthAbsenceSave();

            personHealthAbsencesRecordPage
                 .WaitForPageToLoadInDrawerMode();



        }

        [TestProperty("JiraIssueID", "ACC-6016")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6016 - " +
          "As a Care Coordinator verify system behaviour while displaying booking cancellation popup when updating an existing person absence record.Step 7")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Person Absence")]
        [TestProperty("Screen1", "Person Absences")]
        public void PersonAbsence_VerifyOverlappingBooking_010()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            _systemUserEmployentcontract = (string)dbHelper.systemUserEmploymentContract.GetByID(_systemUserEmploymentContractId, "name")["name"];

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = DateTime.Now.Date.AddDays(-2);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            #region User Work Schedule

            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekMondayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekTuesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(1), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(2), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekThursdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(3), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekFridayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(4), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSaturdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(5), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);
            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekSundayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, thisWeekMonday.AddDays(6), null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), 1);

            #endregion

            // var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(0, 0, 0), todayDate, new TimeSpan(23, 0, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, _systemUserEmployentcontract, _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);

            var peopleid = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);
            #endregion

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
               .InsertHealthAbsencePlannedStartDateTime(todayDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndDateTime(todayDate.ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsencePlannedEndTime("23:00")
               .InsertHealthAbsenceNotifiedDateTime(DateTime.Today.Date.AddDays(-3).ToString("dd'/'MM'/'yyyy"))
               .InsertHealthAbsenceNotifiedTime("10:00")
               .ClickHealthAbsenceSave();

            reviewExistingBookingsPopUp
                 .WaitForPopUpToLoadInDrawerMode()
                 .ValidateAlertText("Adding an end date to an absence will create an Absence Booking in the Diary. Please review existing bookings during this period for cancellation if required.")
                 .SelectCancellationOptionsByValue("all")
                 .ClickOkButton();

            personHealthAbsencesRecordPage
                 .WaitForPageToLoadInDrawerMode()
                 .ClickHealthAbsenceInactiveYesRadio()
                 .ClickHealthAbsenceSave();



        }
        #endregion

    }
}



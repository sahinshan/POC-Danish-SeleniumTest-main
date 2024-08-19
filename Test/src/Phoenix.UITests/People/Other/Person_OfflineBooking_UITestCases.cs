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
    public class Person_OfflineBooking_UITestCases : FunctionalTest
    {
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private string _systemUsername;
        private Guid _bookingType5;
        private Guid _bookingType6;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid1;
        private Guid _providereId1;
        private Guid _recurrencePattern_Every1WeekMondayId;
        private Guid _recurrencePattern_Every1WeekTuesdayId;
        private Guid _recurrencePattern_Every1WeekWednesdayId;
        private Guid _recurrencePattern_Every1WeekThursdayId;
        private Guid _recurrencePattern_Every1WeekFridayId;
        private Guid _recurrencePattern_Every1WeekSaturdayId;
        private Guid _recurrencePattern_Every1WeekSundayId;
        private Guid _availabilityTypes_StandardId;
        private Guid _availabilityTypes_OverTimeId;
        private Guid _cpdiarybookingid;
        private Guid _personcontractId;
        private Guid _contractschemeid;
        private Guid _recurrencePatternId_EveryDay;
        private Guid _schedulingsetupid = new Guid("8B61B44F-B485-EC11-A350-0050569231CF");//Scheduling setup id
        public string firstName;
        public string lastName;
        private Guid _providereId2;



        [TestInitialize()]
        public void PersonOfflineBooking_SetupTest()
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

                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("System Administrator")[0]);

                #endregion

                #region Create SystemUser 

                _systemUsername = "TestUser9";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Test", "User9", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(_systemUserId, localZone.StandardName);

                #endregion

                #region Booking Type 5 -> "Booking (to service user)" 


                _bookingType5 = commonMethodsDB.CreateCPBookingType("BookingType-555", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, 1440);

                _bookingType6 = commonMethodsDB.CreateCPBookingType("BookingType-666", 6, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, 1440);


                #endregion

                #region update Care Provider scheduling set up

                dbHelper.cPSchedulingSetup.UpdateCPScheduleSetup(_schedulingsetupid, _bookingType6);
                dbHelper.cPSchedulingSetup.UpdateCPScheduleSetupModeOfCareDelivery(_schedulingsetupid, 1);
                #endregion

                #region Provider 1

                _providereId1 = commonMethodsDB.CreateProvider("Provider-002", _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, false);

                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType6, false);

                #endregion

                #region Person
                firstName = "Person_CarePlan1" + DateTime.Now.ToString("yyyyMMddHHmmss");
                lastName = "LN_CDV6_23953";
                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();

                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];


                _personFullName = "Person_CarePlan1 LN_CDV6_17302" + DateTime.Now.ToString("yyyyMMddHHmmss");

                #endregion

                #region create contract scheme
                _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), 734, _providereId1, _providereId1);
                #endregion

                #region create person contract

                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, DateTime.Now.Date.AddDays(-5));
                dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId, true);
                #endregion

                #region Care provider staff role type
                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-001", "1234", null, new DateTime(2020, 1, 1), null);
                #endregion

                #region Employment Contract Type - Salaried
                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));
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

                #region Availability Type
                _availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careDirectorQA_TeamId, 1, 1, true);

                _availabilityTypes_OverTimeId = commonMethodsDB.CreateAvailabilityType("OverTime", 4, false, _careDirectorQA_TeamId, 1, 1, true);

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
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Wednesday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2575

        //Test is no longer needed
        //[TestProperty("JiraIssueID", "ACC-3554")]
        //[Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23953 - " +
        //    "Verify automatic pin functionality for a booking with one Person and one staff when person is not pinned manually, and the booking is in offline boundaries" +
        //    "PreCondition:Booking should be of type BTC5. Person record should not be pinned to User. Offline boundaries from current date 3 days before and 7 days after" +
        //    "Automatic pin functionality will work only for the booking with BTC5 & person record should be only")]
        //[TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestMethod]
        //[TestProperty("BusinessModule1", "System Features")]
        //[TestProperty("BusinessModule2", "System Metadata")]
        //[TestProperty("Screen1", "Pinned Records")]
        //public void PersonOffline_VerifyAutomaticPin_01()
        //{
        //    var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

        //    //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
        //    dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

        //    //Link Booking Types with the Employment Contract created previously
        //    dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

        //    #region create diary booking

        //    // var date=commonMethodsHelper.GetCurrentDate();
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture().AddDays(-3);
        //    var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
        //    var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, commonMethodsHelper.GetCurrentDateWithoutCulture(), _careDirectorQA_TeamId, 1);

        //    var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
        //    var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


        //    _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

        //    var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
        //    dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID, _personcontractId);

        //    #endregion

        //    #region execute the scheduled job to pin the records

        //    Guid pinPeopleForIncomingBookingsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

        //    //authenticate against the v6 Web API
        //    this.WebAPIHelper.Security.Authenticate();

        //    //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
        //    this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

        //    //reset the dbHelper because of the athentication using the web api class
        //    dbHelper = new DBHelper.DatabaseHelper();
        //    this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId);

        //    System.Threading.Thread.Sleep(2000);

        //    #endregion

        //    loginPage
        //        .GoToLoginPage()
        //        .Login(_systemUsername, "Passw0rd_!");

        //    mainMenu
        //         .WaitForMainMenuToLoad()
        //         .ClickPinnedRecordsButton();

        //    mainMenuPinnedRecordsArea
        //        .WaitForPinnedRecordsAreaToLoad()
        //        .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

        //    mainMenu
        //       .WaitForMainMenuToLoad()
        //       .ClickPinnedRecordsButton();

        //}

        [TestProperty("JiraIssueID", "ACC-3555")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23953 - " +
            "Verify automatic pin functionality for a booking with one Person and one staff when person is not pinned manually, and the booking is otside offline boundaries" +
            "PreCondition:Booking should be of type BTC5.Person record should not be pinned to User.Offline boundaries from current date 3 days before and 7 days after" +
            "Automatic pin functionality will work only for the booking with BTC5 & person record should be only")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "System Metadata")]
        [TestProperty("Screen1", "Pinned Records")]
        public void PersonOffline_VerifyAutomaticPin_02()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking
            // var date=commonMethodsHelper.GetCurrentDate();
            var todayDate = DateTime.UtcNow.Date.AddDays(-5);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID, _personcontractId);

            #endregion

            #region execute the scheduled job to pin the records
            Guid pinPeopleForIncomingBookingsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId);

            System.Threading.Thread.Sleep(2000);
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), false);

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickPinnedRecordsButton();

        }

        [TestProperty("JiraIssueID", "ACC-3556")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23953 - " +
            "Verify automatic pin functionality for a booking which is deleted when person is not pinned manually & booking is with in offline boundaries " +
            "PreCondition:Booking should be of type BTC5.Person record should not be pinned to User.Offline boundaries from current date 3 days before and 7 days after" +
            "Automatic pin functionality will work only for the booking with BTC5 & person record should be only")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "System Metadata")]
        [TestProperty("Screen1", "Pinned Records")]
        public void PersonOffline_VerifyAutomaticPin_03()
        {
            dbHelper.userFavouriteRecord.CreateUserFavouriteRecord(_systemUserId, _personID, "person");

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking
            // var date=commonMethodsHelper.GetCurrentDate();
            var todayDate = DateTime.UtcNow.AddDays(0);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
            var diarybookingToPeopleId = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID, _personcontractId);

            #endregion

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

            //Delete the Diary Booking created
            dbHelper.diaryBookingToPeople.DeleteDiaryBookingToPeople(diarybookingToPeopleId);
            dbHelper.cPBookingDiaryStaff.DeleteCPBookingDiaryStaff(staffid);
            dbHelper.cPBookingDiary.DeleteCPBookingDiary(_cpdiarybookingid);

            #region execute the scheduled job to pin the records

            Guid pinPeopleForIncomingBookingsJobId1 = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId1.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId1);

            System.Threading.Thread.Sleep(2000);

            #endregion

            mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), false);

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickPinnedRecordsButton();
        }

        [TestProperty("JiraIssueID", "ACC-3557")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23956 - " +
            "Verify automatic pin functionality for a booking which is deleted when person is pinned manually & booking is with in offline boundaries " +
            "PreCondition:Booking should be of type BTC5.Person record should not be pinned to User.Offline boundaries from current date 3 days before and 7 days after" +
            "Automatic pin functionality will work only for the booking with BTC5 & person record should be only")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "System Metadata")]
        [TestProperty("Screen1", "Pinned Records")]
        [TestProperty("Screen2", "People")]
        public void PersonOffline_VerifyAutomaticPin_04()
        {
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            var todayDate = DateTime.UtcNow.AddDays(0);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
            var diarybookingToPeopleId = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID, _personcontractId);

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
                .WaitForPersonRecordPageToLoad()
                .ClickPinToMeLinkButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            //Delete the Care Provider diary booking created .
            dbHelper.diaryBookingToPeople.DeleteDiaryBookingToPeople(diarybookingToPeopleId);
            dbHelper.cPBookingDiaryStaff.DeleteCPBookingDiaryStaff(staffid);
            dbHelper.cPBookingDiary.DeleteCPBookingDiary(_cpdiarybookingid);

            #region execute the scheduled job to pin the records

            Guid pinPeopleForIncomingBookingsJobId1 = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId1.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId1);

            System.Threading.Thread.Sleep(2000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();
        }

        /* [TestProperty("JiraIssueID", "ACC-3558")]
         [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23957 - " +
             "Verify pin & unpin functionality for a booking with respective to offline boundaries when person is not pinned manually " +
             "PreCondition:Booking should be of type BTC5.Person record should not be pinned to User.Offline boundaries from current date 3 days before and 7 days after" +
             "Automatic pin functionality will work only for the booking with BTC5 & person record should be only")]
         [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
         [TestMethod]
         [TestProperty("BusinessModule1", "System Features")]
         [TestProperty("BusinessModule2", "System Metadata")]
         [TestProperty("Screen1", "Pinned Records")]
         public void PersonOffline_VerifyAutomaticPin_05()
         {
             var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

             //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
             dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

             //Link Booking Types with the Employment Contract created previously
             dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

             #region create diary booking
             // var date=commonMethodsHelper.GetCurrentDate();
             var todayDate = DateTime.UtcNow.AddDays(-5);
             var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
             var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

             var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
             var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


             _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

             var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
             var diarybookingToPeopleId = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID, _personcontractId);

             #endregion

             loginPage
                 .GoToLoginPage()
                 .Login(_systemUsername, "Passw0rd_!");


             mainMenu
                  .WaitForMainMenuToLoad()
                  .ClickPinnedRecordsButton();

             mainMenuPinnedRecordsArea
                 .WaitForPinnedRecordsAreaToLoad()
                 .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), false);

             mainMenu
               .WaitForMainMenuToLoad()
               .ClickPinnedRecordsButton();


             #region execute the scheduled job to pin the records

             Guid pinPeopleForIncomingBookingsJobId1 = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

             //authenticate against the v6 Web API
             this.WebAPIHelper.Security.Authenticate();

             //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
             this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId1.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

             //reset the dbHelper because of the athentication using the web api class
             dbHelper = new DBHelper.DatabaseHelper();
             this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId1);

             System.Threading.Thread.Sleep(2000);

             #endregion

             mainMenu
                  .WaitForMainMenuToLoad()
                  .ClickPinnedRecordsButton();

             mainMenuPinnedRecordsArea
                 .WaitForPinnedRecordsAreaToLoad()
                 .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), false);

             mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

             //Update booking diary to current date
             dbHelper.cPBookingDiary.UpdateCPBookingDiary(_cpdiarybookingid, DateTime.UtcNow.AddDays(0), DateTime.UtcNow.AddDays(0));
             System.Threading.Thread.Sleep(2000);

             #region execute the scheduled job to pin the records

             Guid pinPeopleForIncomingBookingsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

             //authenticate against the v6 Web API
             this.WebAPIHelper.Security.Authenticate();

             //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
             this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

             //reset the dbHelper because of the athentication using the web api class
             dbHelper = new DBHelper.DatabaseHelper();
             this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId);

             System.Threading.Thread.Sleep(2000);

             #endregion

             //Verify the records are in the pinned section

             mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickPinnedRecordsButton();

             mainMenuPinnedRecordsArea
                 .WaitForPinnedRecordsAreaToLoad()
                 .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

             mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

             //Update booking diary to current date+8 days
             dbHelper.cPBookingDiary.UpdateCPBookingDiary(_cpdiarybookingid, DateTime.UtcNow.AddDays(+9), DateTime.UtcNow.AddDays(+9));
             System.Threading.Thread.Sleep(2000);

             #region execute the scheduled job to pin the records

             Guid pinPeopleForIncomingBookingsJobId2 = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

             //authenticate against the v6 Web API
             this.WebAPIHelper.Security.Authenticate();

             //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
             this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId2.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

             //reset the dbHelper because of the athentication using the web api class
             dbHelper = new DBHelper.DatabaseHelper();
             this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId2);

             System.Threading.Thread.Sleep(2000);

             #endregion

             //Verify the records are in the pinned section

             mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickPinnedRecordsButton();

             mainMenuPinnedRecordsArea
                 .WaitForPinnedRecordsAreaToLoad()
                 .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), false);

             mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();
         }
 */
        [TestProperty("JiraIssueID", "ACC-3559")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23958 - " +
            "Verify automatic pin functionality with respective to offline boundaries for a booking when a person is pinned manually " +
            "PreCondition:Booking should be of type BTC5.Person record should not be pinned to User.Offline boundaries from current date 3 days before and 7 days after" +
            "Automatic pin functionality will work only for the booking with BTC5 & person record should be only")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "System Metadata")]
        [TestProperty("Screen1", "Pinned Records")]
        [TestProperty("Screen2", "People")]
        public void PersonOffline_VerifyAutomaticPin_06()
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
                .WaitForPersonRecordPageToLoad()
                .ClickPinToMeLinkButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            // var date=commonMethodsHelper.GetCurrentDate();
            var todayDate = DateTime.UtcNow.AddDays(-5);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
            var diarybookingToPeopleId = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID, _personcontractId);

            #endregion

            //Verify the person record is in the pinned section.

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            //Update booking diary to -4 days outside offline boundry
            dbHelper.cPBookingDiary.UpdateCPBookingDiary(_cpdiarybookingid, DateTime.UtcNow.AddDays(-4), DateTime.UtcNow.AddDays(-4));
            System.Threading.Thread.Sleep(2000);

            #region execute the scheduled job to pin the records

            Guid pinPeopleForIncomingBookingsJobId = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId);

            System.Threading.Thread.Sleep(2000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            //Update booking diary to +8 days outside offline boundry
            dbHelper.cPBookingDiary.UpdateCPBookingDiary(_cpdiarybookingid, DateTime.UtcNow.AddDays(+12), DateTime.UtcNow.AddDays(+12));
            System.Threading.Thread.Sleep(2000);

            #region execute the scheduled job to pin the records

            Guid pinPeopleForIncomingBookingsJobId1 = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId1.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId1);

            System.Threading.Thread.Sleep(2000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), true);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();
        }

        [TestProperty("JiraIssueID", "ACC-3560")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23959 - " +
            "Verify automatic pin functionality for a booking with more than one person when no person records pinned manually " +
            "PreCondition:Booking should be of type BTC5.Person record should not be pinned to User.Offline boundaries from current date 3 days before and 7 days after" +
            "Automatic pin functionality will work only for the booking with BTC5 & person record should be only")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "System Metadata")]
        [TestProperty("Screen1", "Pinned Records")]
        public void PersonOffline_VerifyAutomaticPin_07()
        {

            #region Create Person 2
            var firstName2 = "Person_CarePlan2" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var lastName2 = "LN_CDV6_23953_1";
            var personRecordExists = dbHelper.person.GetByFirstName(firstName2).Any();

            var _personID2 = dbHelper.person.CreatePersonRecord("", firstName2, "", lastName2, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            var _personNumber2 = (int)dbHelper.person.GetPersonById(_personID2, "personnumber")["personnumber"];


            var _personFullName2 = "Person_CarePlan2 LN_CDV6_23953_1" + DateTime.Now.ToString("yyyyMMddHHmmss");

            #endregion

            #region create person contract

            var _personcontractId1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID2, _systemUserId, _providereId1, _contractschemeid, _providereId1, DateTime.Now.Date.AddDays(-5));
            dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId1, true);
            #endregion

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            // var date=commonMethodsHelper.GetCurrentDate();
            var todayDate = DateTime.UtcNow.AddDays(0);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

            var staffid = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
            var diarybookingToPeopleId = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID, _personcontractId);

            //Create Another booking with 2nd person created

            var _cpdiarybookingid1 = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

            var staffid1 = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid1, _systemUserEmploymentContractId, _systemUserId);
            var diarybookingToPeopleId1 = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid1, _personID2, _personcontractId1);

            #endregion

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), false)
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID2.ToString(), false);
            mainMenu
               .WaitForMainMenuToLoad()
               .ClickPinnedRecordsButton();

            #region execute the scheduled job to pin the records

            Guid pinPeopleForIncomingBookingsJobId1 = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId1.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId1);

            System.Threading.Thread.Sleep(2000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID.ToString(), false)
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID2.ToString(), false);

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickPinnedRecordsButton();
        }

        [TestProperty("JiraIssueID", "ACC-3561")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-23960 - " +
            "Verify automatic pin functionality for a booking with booking type other than BTC5 when person is not pinned manually " +
            "PreCondition:Booking should be of type BTC5.Person record should not be pinned to User.Offline boundaries from current date 3 days before and 7 days after" +
            "Automatic pin functionality will work only for the booking with BTC5 & person record should be only")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "System Features")]
        [TestProperty("BusinessModule2", "System Metadata")]
        [TestProperty("Screen1", "Pinned Records")]
        public void PersonOffline_VerifyAutomaticPin_08()
        {
            #region Provider for booking type other than btc-5

            if (!dbHelper.provider.GetProviderByName("Provider-003").Any())
            {
                _providereId2 = dbHelper.provider.CreateProvider("Provider-003", _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider

                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId2, _bookingType6, true);

            }

            if (_providereId2 == Guid.Empty)
                _providereId2 = dbHelper.provider.GetProviderByName("Provider-003").First();

            #endregion

            #region Person

            firstName = "Person_CarePlan1" + DateTime.Now.ToString("yyyyMMddHHmmss");
            lastName = "LN_CDV6_23953";
            var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();

            var _personID1 = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
            _personNumber = (int)dbHelper.person.GetPersonById(_personID1, "personnumber")["personnumber"];


            _personFullName = "Person_CarePlan1 LN_CDV6_23953" + DateTime.Now.ToString("yyyyMMddHHmmss");

            #endregion

            #region create contract scheme for provider 2

            var _contractschemeid1 = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), 734, _providereId2, _providereId2);

            #endregion

            #region create person contract

            var _personcontractId1 = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID1, _systemUserId, _providereId2, _contractschemeid1, _providereId2, DateTime.Now.Date.AddDays(-5));
            dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId1, true);

            #endregion

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #region create diary booking

            // var date=commonMethodsHelper.GetCurrentDate();
            var todayDate = DateTime.UtcNow.AddDays(0);
            var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
            var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

            var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
            var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);


            _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _bookingType6, _providereId2, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 30, 0), "staff", 30, 1, "people", 890);

            var diarybookingToPeopleId = dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _cpdiarybookingid, _personID1, _personcontractId1);

            #endregion


            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID1.ToString(), false);

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickPinnedRecordsButton();

            #region execute the scheduled job to pin the records

            Guid pinPeopleForIncomingBookingsJobId1 = dbHelper.scheduledJob.GetScheduledJobByScheduledJobName("Pin People for Incoming Bookings")[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(pinPeopleForIncomingBookingsJobId1.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(pinPeopleForIncomingBookingsJobId1);

            System.Threading.Thread.Sleep(2000);

            #endregion

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickPinnedRecordsButton();

            mainMenuPinnedRecordsArea
                .WaitForPinnedRecordsAreaToLoad()
                .ValidatePinnedRecordsAreaLinkElementVisible(_personID1.ToString(), false);

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickPinnedRecordsButton();
        }

        #endregion



    }
}


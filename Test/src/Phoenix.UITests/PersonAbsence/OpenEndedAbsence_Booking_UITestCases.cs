using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.DBHelper.Models;
using Phoenix.UITests.Framework.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Numerics;


namespace Phoenix.UITests.PersonAbsence
{
    /// <summary>
    /// This class contains Automated UI test scripts for Regular Care Tasks
    /// </summary>
    [TestClass]
    public class OpenEndedAbsence_Booking_UITestCases : FunctionalTest
    {
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid userid;
        private string _systemUsername;
        private Guid _bookingType1;
        private Guid _bookingType2;
        private Guid _bookingType3;
        private Guid _bookingType4;
        private Guid _providereId1;
        private Guid _contractschemeid;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid1;
        private Guid _employmentContractTypeid2;
        private Guid _employmentContractTypeid3;
        private Guid _employmentContractTypeid4;
        private Guid _recurrencePattern_Every1WeekMondayId;
        private Guid _recurrencePattern_Every1WeekTuesdayId;
        private Guid _recurrencePattern_Every1WeekWednesdayId;
        private Guid _recurrencePattern_Every1WeekThursdayId;
        private Guid _recurrencePattern_Every1WeekFridayId;
        private Guid _recurrencePattern_Every1WeekSaturdayId;
        private Guid _recurrencePattern_Every1WeekSundayId;
        private Guid _recurrencePatternId_EveryDay;
        private Guid _availabilityTypes_StandardId;
        private Guid _availabilityTypes_OverTimeId;
        private Guid _applicantId;
        private Guid _recruitmentRoleApplicantId;
        private Guid _expressbookingcriteriaid;
        private Guid _expressBookingProcessed1;
        private Guid _expressBookingProcessed2;
        private Guid _expressBookingProcessed3;
        private Guid _expressBookingProcessed4;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _personcontractId;
        private string _providerName;
        private Guid _bookingType5;
        private Guid _bookingType6;
        private string _TeamName;
        private Guid careProviderContractServiceId;


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

                _TeamName = "OEAT " + DateTime.Now.ToString("yyyyMMdd");
                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam(_TeamName, null, _careDirectorQA_BusinessUnitId, "907678", "OpenEndedAbsenceTeam@careworkstempmail.com", "Open Ended Absence Team" + DateTime.Now.ToString("yyyyMMdd"), "020 123456");

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

                #region Default User

                userid = dbHelper.systemUser.GetSystemUserByUserName("administrator").FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

                #endregion

                #region Create SystemUser 

                _systemUsername = "expressbook_testuser" + _currentDateSuffix;
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "expressbook", "testuser", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, new List<Guid>());
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);



                #endregion

                #region USer Security Profile


                dbHelper.userSecurityProfile.CreateMultipleUserSecurityProfile(_systemUserId, userSecProfiles);

                #endregion

                #region Booking Type 1 -> "Booking (to location)" & "Count full booking length"

                _bookingType1 = commonMethodsDB.CreateCPBookingType("BTC-1", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false);

                #endregion

                #region Booking Type 2 ->  "Booking (to internal care activity)" & "Count full booking length"

                _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC-2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false);

                #endregion

                #region Booking Type 3 ->  "Booking (to external care activity)" & "Count full booking length"

                _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC-3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false);

                #endregion

                #region Booking Type 4 ->  "Booking (to internal non-care booking e.g. annual leave, training)" & "Count full booking length"

                _bookingType4 = commonMethodsDB.CreateCPBookingType("BTC-04", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, true, false, true, true, false, false);

                #endregion

                #region Booking Type 5 ->  "Booking (to Service User)" & "Count full booking length"

                _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC-05", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 3);

                #endregion

                #region Booking Type 6->"Booking (Service User non-care booking)"

                _bookingType6 = commonMethodsDB.CreateCPBookingType("BTC-6", 6, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, 1440);

                #endregion

                #region Provider 1
                _providereId1 = commonMethodsDB.CreateProvider("OEProvider-001" + DateTime.Now.ToString("yyyyMMdd"), _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider

                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType1, true);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType2, false);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType3, false);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType4, false);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, false);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType6, false);

                #endregion

                #region create contract scheme
                _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), 939, _providereId1, _providereId1);
                #endregion

                #region Care provider staff role type
                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-001", "1234", null, new DateTime(2020, 1, 1), null);
                #endregion

                #region recruitmentrole applicant
                var firstName = "RoleApplicant_";
                var lastName = "LN_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                _applicantId = dbHelper.applicant.CreateApplicant(firstName, lastName, _careDirectorQA_TeamId);
                _recruitmentRoleApplicantId = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

                #endregion

                #region Employment Contract Type - Salaried

                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));

                #endregion

                #region Employment Contract Type - Hourly

                _employmentContractTypeid2 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Hourly", "", null, new DateTime(2020, 1, 1));

                #endregion

                #region Employment Contract Type - Volunteer
                _employmentContractTypeid3 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Volunteer", "", null, new DateTime(2020, 1, 1));
                #endregion

                #region Employment Contract Type - Contracted

                _employmentContractTypeid4 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Contracted", "", null, new DateTime(2020, 1, 1));

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

                //TODO: verify if the "For Diary Bookings" and "For Schedule Bookings" are set to valid
                #endregion

                #region Person

                var PersonfirstName = "Person_Absence" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();

                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                _personFullName = firstName + lastName;

                #endregion

                #region create person contract

                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, DateTime.Now.Date.AddDays(-5));
                dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId, true);

                #endregion

                #region Care Provider Service

                var isScheduledService = true;
                var careProviderServiceName = "CPS A " + _currentDateSuffix;
                var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
                var careProviderService1Id = commonMethodsDB.CreateCareProviderService(_careDirectorQA_TeamId, careProviderServiceName, new DateTime(2024, 1, 1), careProviderServiceCode, null, isScheduledService);

                #endregion



                #region Care Provider Batch Grouping

                string careProviderBatchGroupingName = "Standard";
                Guid careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(_careDirectorQA_TeamId, careProviderBatchGroupingName, new DateTime(2020, 1, 1), 8888888);

                #endregion

                #region Care Provider Extract Name

                var careProviderExtract1Name = "CPEN " + _currentDateSuffix;
                var careProviderExtractName1Code = dbHelper.careProviderExtractName.GetHighestCode() + 1;
                var careProviderExtractName1Id = commonMethodsDB.CreateCareProviderExtractName(_careDirectorQA_TeamId, careProviderExtract1Name, careProviderExtractName1Code, null, new DateTime(2024, 1, 1), null, false, false);

                #endregion

                #region VAT Code

                var _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

                #endregion

                #region Care Provider Rate Unit

                var _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(_careDirectorQA_TeamId, "CPRU ACC-7556 A", new DateTime(2020, 1, 1), 750000, true);

                #endregion

                #region Care Provider Service Mapping

                var careProviderServiceMappingId = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, _careDirectorQA_TeamId, null, _bookingType5, null, "");

                #endregion

                #region Care Provider Contract Service

                var contractserviceadjusteddaysid = 2; //Charge End Date Less 1 Day
                var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "", _providereId1, _providereId1, _contractschemeid, careProviderService1Id, null, _bookingType5, _careProviderVATCodeId, careProviderBatchGroupingId, _careProviderRateUnitId, 1, contractserviceadjusteddaysid, false, false);
                var careProviderContractServiceTitle = (string)(dbHelper.careProviderContractService.GetCareProviderContractServiceByID(careProviderContractServiceId, "title")["title"]);

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

        internal DateTime GetNextWeekFirstMonday()
        {

            DateTime today = DateTime.Today;
            int daysUntilMonday = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7;

            if (daysUntilMonday == 0)
            {
                daysUntilMonday = 7;
            }
            return today.AddDays(daysUntilMonday);


        }

        internal DateTime GetThisWeekMonday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2688

        [TestProperty("JiraIssueID", "ACC-3593")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-21774 - " +
            "As a Care Coordinator verify the list of bookings displayed when a staff member’s open-ended absence is ended.Pre-Condition-> Open ended absence record should be created for required provider, staff, date & time" +
            "To create absence required Booking type should be of Absence booking type (Class -4, Absence -Yes, Open ended absence -Yes)" +
            "Selected Provider should be configured with Absence booking type)" +
            "Booking should be created through schedule using express book functionality.Only schedule bookings are available to reallocate & not able to reallocate standalone bookings created  through dairy")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Scheduling")]
        [TestProperty("Screen", "Open ended Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_01()
        {
            #region Create a System User Employment Contract for Employment Contract Type - Salaried
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            #endregion

            #region Link Booking Types with the Employment Contract created previously
            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);
            #endregion 

            #region Create CPBooking Schedule
            //Create the provider schedule records for the user
            //BTC-1 for monday

            var cpBookingScheduleId1 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType1, 1, 1, 1, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId1, _systemUserEmploymentContractId, _systemUserId);

            //BTC-2 for tuesday
            var cpBookingScheduleId2 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType2, 1, 2, 2, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId2, _systemUserEmploymentContractId, _systemUserId);

            //BTC-3 for wednesday
            var cpBookingScheduleId3 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType3, 1, 3, 3, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId3, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region create express booking for the coming week with status=1 pending
            DateTime StartDate = GetNextWeekFirstMonday();
            DateTime bookingStartDate = StartDate.AddDays(1);
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var employmentcontractname = (string)(dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId, "name"))["name"];
            _expressbookingcriteriaid = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", 1, null, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture().Date, _systemUserEmploymentContractId, "systemuseremploymentcontract", employmentcontractname);

            _expressBookingProcessed1 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId1, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed2 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId2, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed3 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId3, _expressbookingcriteriaid, DateTime.Now);

            #endregion

            #region execute the scheduled job to pin the records
            Guid systemUserEmploymentContractJobId = dbHelper.scheduledJob.GetScheduledJobByRecordId(_systemUserEmploymentContractId)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(systemUserEmploymentContractJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(systemUserEmploymentContractJobId);

            System.Threading.Thread.Sleep(2000);
            #endregion

            #region create OpenEndedAbsence

            DateTime openendedabsencestartdate = bookingStartDate.AddDays(-4);
            DateTime openendedabsenceenddate = openendedabsencestartdate.AddDays(1);
            var _openendedabsenceid = dbHelper.OpenEndedAbsence.CreateOpenEndedAbsence(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _providereId1, _bookingType4, _systemUserId, _systemUserEmploymentContractId, openendedabsencestartdate.Date);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserNoTeamsRecordPageToLoad()
                .NavigateToAbsencesSubPage();

            systemUserAbsencesPage
                .WaitForSystemUserAbsencesPageToLoad()
                .OpenRecord(_openendedabsenceid.ToString());

            systemUserAbsencesRecordPage
                .WaitForSystemUserAbsencesRecordPageToLoad()
                .InsertOpenEndedAbsenceEndDate(openendedabsenceenddate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceEndTime("00:00")
                .ClickSaveButton();

            var BookingId1 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId1)[0];
            var BookingId2 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId2)[0];
            var BookingId3 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId3)[0];

            openEndedAbsencesPopUp
                .WaitForOpenEndedAbsencesPopUpToLoad()
                .SelectReallocationOptionsByValue("all")
                .ValidateResultElementPresent(BookingId1)
                .ValidateResultElementPresent(BookingId2)
                .ValidateResultElementPresent(BookingId3)
                .ClickCancelButton();

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2858

        [TestProperty("JiraIssueID", "ACC-3592")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/CDV6-21775 - " +
           "As a care Coordinator verify the validations in displaying the list of bookings when a staff member’s open-ended absence is ended.Pre-Condition-> Open ended absence record should be created for required provider, staff, date & time" +
           "To create absence required Booking type should be of Absence booking type (Class -4, Absence -Yes, Open ended absence -Yes)" +
           "Selected Provider should be configured with Absence booking type)"
           + "Booking should be created through schedule using express book functionality.Only schedule bookings are available to reallocate & not able to reallocate standalone bookings created  through dairy")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Open ended Absences")]
        [TestProperty("Screen2", "Provider Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_02()
        {

            #region Create a System User Employment Contract for Employment Contract Type - Salaried
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types with the Employment Contract created previously
            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region Create CPBookingSchedule
            //Create the provider schedule records for the user
            //BTC-1 for monday

            var cpBookingScheduleId1 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType1, 1, 1, 1, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId1, _systemUserEmploymentContractId, _systemUserId);

            //BTC-2 for tuesday
            var cpBookingScheduleId2 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType2, 1, 2, 2, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId2, _systemUserEmploymentContractId, _systemUserId);

            //BTC-3 for wednesday
            var cpBookingScheduleId3 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType3, 1, 3, 3, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId3, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region create express booking for the coming week with status=1 pending
            DateTime StartDate = GetNextWeekFirstMonday();
            DateTime bookingStartDate = StartDate;
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var employmentcontractname = (string)(dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId, "name"))["name"];
            _expressbookingcriteriaid = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", 1, null, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture().Date, _systemUserEmploymentContractId, "systemuseremploymentcontract", employmentcontractname);

            _expressBookingProcessed1 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId1, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed2 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId2, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed3 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId3, _expressbookingcriteriaid, DateTime.Now);

            #endregion

            #region execute the scheduled job to pin the records
            Guid systemUserEmploymentContractJobId = dbHelper.scheduledJob.GetScheduledJobByRecordId(_systemUserEmploymentContractId)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(systemUserEmploymentContractJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(systemUserEmploymentContractJobId);

            System.Threading.Thread.Sleep(2000);
            #endregion

            #region Create OpenEndedAbsence

            DateTime openendedabsencestartdate = bookingStartDate.AddDays(-4);
            DateTime openendedabsenceenddate = openendedabsencestartdate.AddDays(1);
            var _openendedabsenceid = dbHelper.OpenEndedAbsence.CreateOpenEndedAbsence(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _providereId1, _bookingType4, _systemUserId, _systemUserEmploymentContractId, openendedabsencestartdate.Date);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserNoTeamsRecordPageToLoad()
                .NavigateToAbsencesSubPage();

            systemUserAbsencesPage
                .WaitForSystemUserAbsencesPageToLoad()
                .OpenRecord(_openendedabsenceid.ToString());

            systemUserAbsencesRecordPage
                .WaitForSystemUserAbsencesRecordPageToLoad()
                .InsertOpenEndedAbsenceEndDate(openendedabsenceenddate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceEndTime("00:00")
                .ClickSaveButton();

            var BookingId1 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId1)[0];
            var BookingId2 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId2)[0];
            var BookingId3 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId3)[0];

            //Reallocate all available bookings
            openEndedAbsencesPopUp
                .WaitForOpenEndedAbsencesPopUpToLoad()
                .ValidateHeaderText("This will add an End Date and create the Absence Booking. The Person is coming back to work. Please review future bookings for reallocation to this person.")
                .SelectReallocationOptionsByValue("all")
                .ClickOkBtn();

            systemUserAbsencesRecordPage
               .WaitForSystemUserAbsencesRecordPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

            DateTime providerDate = StartDate;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(provider + " - No Address")
                .ClickChangeDate(providerDate.ToString("yyyy"), providerDate.ToString("MMMM"), providerDate.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickMinusButton()
                .ClickMinusButton()
                .ClickMinusButton();//just a work around has there is an issue in calender pop up

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .MouseHoverDiaryBooking(BookingId1.ToString())
                .ValidateTimeLabelText("Planned Time: "+StartDate.ToString("dd'/'MM'/'yyyy") + " 09:15 - 10:00")

                .MouseHoverDiaryBooking(BookingId2.ToString())
                .ValidateTimeLabelText("Planned Time: "+StartDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + " 09:15 - 10:00")

                .MouseHoverDiaryBooking(BookingId3.ToString())
                .ValidateTimeLabelText("Planned Time: "+StartDate.AddDays(2).ToString("dd'/'MM'/'yyyy") + " 09:15 - 10:00");
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2858

        [TestProperty("JiraIssueID", "ACC-3598")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-3598 - " +
            "Verify Provider diary for all unallocated bookings due to ending an Open-ended absence without reallocating available bookings for selected staff" +
           "As a care Coordinator verify the validations in displaying the list of bookings when a staff member’s open-ended absence is ended.Pre-Condition-> Open ended absence record should be created for required provider, staff, date & time" +
           "To create absence required Booking type should be of Absence booking type (Class -4, Absence -Yes, Open ended absence -Yes)" +
           "Selected Provider should be configured with Absence booking type)"
           + "Booking should be created through schedule using express book functionality.Only schedule bookings are available to reallocate & not able to reallocate standalone bookings created  through dairy")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Open ended Absences")]
        [TestProperty("Screen2", "Provider Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_03()
        {
            #region Create a System User Employment
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region Create CPBooking Schedule
            //Create the provider schedule records for the user
            //BTC-1 for monday

            var cpBookingScheduleId1 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType1, 1, 1, 1, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId1, _systemUserEmploymentContractId, _systemUserId);

            //BTC-2 for tuesday
            var cpBookingScheduleId2 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType2, 1, 2, 2, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId2, _systemUserEmploymentContractId, _systemUserId);

            //BTC-3 for wednesday
            var cpBookingScheduleId3 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType3, 1, 3, 3, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId3, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region create express booking for the coming week with status=1 pending
            DateTime StartDate = GetNextWeekFirstMonday();
            DateTime bookingStartDate = StartDate;
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var employmentcontractname = (string)(dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId, "name"))["name"];
            _expressbookingcriteriaid = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", 1, null, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture().Date, _systemUserEmploymentContractId, "systemuseremploymentcontract", employmentcontractname);

            _expressBookingProcessed1 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId1, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed2 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId2, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed3 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId3, _expressbookingcriteriaid, DateTime.Now);

            #endregion

            #region execute the scheduled job to pin the records
            Guid systemUserEmploymentContractJobId = dbHelper.scheduledJob.GetScheduledJobByRecordId(_systemUserEmploymentContractId)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(systemUserEmploymentContractJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(systemUserEmploymentContractJobId);

            System.Threading.Thread.Sleep(2000);
            #endregion

            #region Create OpenEndedAbsence
            DateTime openendedabsencestartdate = bookingStartDate.AddDays(-4);
            DateTime openendedabsenceenddate = openendedabsencestartdate.AddDays(1);

            var _openendedabsenceid = dbHelper.OpenEndedAbsence.CreateOpenEndedAbsence(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _providereId1, _bookingType4, _systemUserId, _systemUserEmploymentContractId, openendedabsencestartdate.Date);
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserNoTeamsRecordPageToLoad()
                .NavigateToAbsencesSubPage();

            systemUserAbsencesPage
                .WaitForSystemUserAbsencesPageToLoad()
                .OpenRecord(_openendedabsenceid.ToString());

            systemUserAbsencesRecordPage
                .WaitForSystemUserAbsencesRecordPageToLoad()
                .InsertOpenEndedAbsenceEndDate(openendedabsenceenddate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceEndTime("00:00")
                .ClickSaveButton();

            var BookingId1 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId1)[0];
            var BookingId2 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId2)[0];
            var BookingId3 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId3)[0];

            //Reallocate all available bookings
            openEndedAbsencesPopUp
                .WaitForOpenEndedAbsencesPopUpToLoad()
                .ValidateHeaderText("This will add an End Date and create the Absence Booking. The Person is coming back to work. Please review future bookings for reallocation to this person.")
                .SelectReallocationOptionsByValue("none")//Do not reallocate any bookings
                .ClickOkBtn();

            System.Threading.Thread.Sleep(2000);

            systemUserAbsencesRecordPage
               .WaitForSystemUserAbsencesRecordPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

            DateTime providerDate = StartDate;

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(provider + " - No Address")
                .ClickChangeDate(providerDate.ToString("yyyy"), providerDate.ToString("MMMM"), providerDate.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickMinusButton()
                .ClickMinusButton()
                .ClickMinusButton();//just a work around has there is an issue in calender pop up

            System.Threading.Thread.Sleep(1000);

            providerDiaryPage
                .MouseHoverDiaryBooking(BookingId1.ToString())
                .ValidateTimeLabelText("Planned Time: "+StartDate.ToString("dd'/'MM'/'yyyy") + " 09:15 - 10:00")
                .ValidateStaffLabelText("Unassigned")
                .MouseHoverDiaryBooking(BookingId2.ToString());

            System.Threading.Thread.Sleep(500);

            providerDiaryPage
                .MouseHoverDiaryBooking(BookingId2.ToString())
                .ValidateTimeLabelText("Planned Time: "+StartDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + " 09:15 - 10:00")
                .ValidateStaffLabelText("Unassigned")
                .MouseHoverDiaryBooking(BookingId3.ToString());

            System.Threading.Thread.Sleep(500);

            providerDiaryPage
                .MouseHoverDiaryBooking(BookingId3.ToString())
                .ValidateTimeLabelText("Planned Time: " + StartDate.AddDays(2).ToString("dd'/'MM'/'yyyy") + " 09:15 - 10:00")
                .ValidateStaffLabelText("Unassigned");
        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2858

        [TestProperty("JiraIssueID", "ACC-3619")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-3619 - " +
            "Verify message on ‘End an Open-ended absence’ popup screen when no bookings available for selected staff while ending open-ended absence" +
           "As a care Coordinator verify the validations in displaying the list of bookings when a staff member’s open-ended absence is ended.Pre-Condition-> Open ended absence record should be created for required provider, staff, date & time" +
           "To create absence required Booking type should be of Absence booking type (Class -4, Absence -Yes, Open ended absence -Yes)" +
           "Selected Provider should be configured with Absence booking type)"
           + "Selected staff should not have booking for selected date (should be deleted later /should not have bookings)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Scheduling")]
        [TestProperty("Screen", "Open ended Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_04()
        {
            #region Create a System User Employment 

            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);
            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

            #endregion

            #region Link Booking Types with the Employment Contract
            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create the user work schedule

            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region Create CPBookingSchedule
            //Create the provider schedule records for the user
            //BTC-1 for monday

            var cpBookingScheduleId1 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType1, 1, 1, 1, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId1, _systemUserEmploymentContractId, _systemUserId);

            //BTC-2 for tuesday
            var cpBookingScheduleId2 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType2, 1, 2, 2, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId2, _systemUserEmploymentContractId, _systemUserId);

            //BTC-3 for wednesday
            var cpBookingScheduleId3 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType3, 1, 3, 3, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId3, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region Create OpenEndedAbsence

            DateTime StartDate = GetNextWeekFirstMonday();
            DateTime bookingStartDate = StartDate.AddDays(1);
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            DateTime openendedabsencestartdate = bookingStartDate.AddDays(-4);
            DateTime openendedabsenceenddate = openendedabsencestartdate.AddDays(1);

            var _openendedabsenceid = dbHelper.OpenEndedAbsence.CreateOpenEndedAbsence(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _providereId1, _bookingType4, _systemUserId, _systemUserEmploymentContractId, openendedabsencestartdate.Date);
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserNoTeamsRecordPageToLoad()
                .NavigateToAbsencesSubPage();

            systemUserAbsencesPage
                .WaitForSystemUserAbsencesPageToLoad()
                .OpenRecord(_openendedabsenceid.ToString());

            systemUserAbsencesRecordPage
                .WaitForSystemUserAbsencesRecordPageToLoad()
                .InsertOpenEndedAbsenceEndDate(openendedabsenceenddate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceEndTime("00:00")
                .ClickSaveButton();

            noBookingOpenEndedAbsencesPopUp
                .WaitForNoBookingOpenEndedAbsencesPopUpToLoad()
                .ValidateAlertText("This will add an End Date and create the Absence Booking. No bookings for this contract are available for reallocation.")
                .ClickOkBtn();

            systemUserAbsencesRecordPage
               .WaitForSystemUserAbsencesRecordPageToLoad();
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-2858

        [TestProperty("JiraIssueID", "ACC-3640")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-3640 - " +
            "Verify ‘End an Open-ended absence’ popup screen & Columns in data grid when bookings available for selected staff while ending open-ended absence" +
           "As a care Coordinator verify the validations in displaying the list of bookings when a staff member’s open-ended absence is ended.Pre-Condition-> Open ended absence record should be created for required provider, staff, date & time" +
           "To create absence required Booking type should be of Absence booking type (Class -4, Absence -Yes, Open ended absence -Yes)" +
           "Selected Provider should be configured with Absence booking type)"
           + "Booking should be created through schedule using express book functionality.Only schedule bookings are available to reallocate & not able to reallocate standalone bookings created  through dairy")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Scheduling")]
        [TestProperty("Screen", "Open ended Absences")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_05()
        {
            #region Create a System User Employment
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region Create CPBooking Schedule
            //Create the provider schedule records for the user
            //BTC-1 for monday

            var cpBookingScheduleId1 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType1, 1, 1, 1, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId1, _systemUserEmploymentContractId, _systemUserId);

            //BTC-2 for tuesday
            var cpBookingScheduleId2 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType2, 1, 2, 2, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId2, _systemUserEmploymentContractId, _systemUserId);

            //BTC-3 for wednesday
            var cpBookingScheduleId3 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType3, 1, 3, 3, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId3, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region create express booking for the coming week with status=1 pending
            DateTime StartDate = GetNextWeekFirstMonday();
            DateTime bookingStartDate = StartDate;
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var employmentcontractname = (string)(dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId, "name"))["name"];
            _expressbookingcriteriaid = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", 1, null, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture().Date, _systemUserEmploymentContractId, "systemuseremploymentcontract", employmentcontractname);

            _expressBookingProcessed1 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId1, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed2 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId2, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed3 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId3, _expressbookingcriteriaid, DateTime.Now);

            #endregion

            #region execute the scheduled job to pin the records
            Guid systemUserEmploymentContractJobId = dbHelper.scheduledJob.GetScheduledJobByRecordId(_systemUserEmploymentContractId)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(systemUserEmploymentContractJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(systemUserEmploymentContractJobId);

            dbHelper = new DBHelper.DatabaseHelper();
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            System.Threading.Thread.Sleep(2000);
            #endregion

            #region Create OpenEndedAbsence
            DateTime openendedabsencestartdate = bookingStartDate.AddDays(-4);
            DateTime openendedabsenceenddate = openendedabsencestartdate.AddDays(1);

            var _openendedabsenceid = dbHelper.OpenEndedAbsence.CreateOpenEndedAbsence(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _providereId1, _bookingType4, _systemUserId, _systemUserEmploymentContractId, openendedabsencestartdate.Date);
            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserNoTeamsRecordPageToLoad()
                .NavigateToAbsencesSubPage();

            systemUserAbsencesPage
                .WaitForSystemUserAbsencesPageToLoad()
                .OpenRecord(_openendedabsenceid.ToString());

            systemUserAbsencesRecordPage
                .WaitForSystemUserAbsencesRecordPageToLoad()
                .InsertOpenEndedAbsenceEndDate(openendedabsenceenddate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceEndTime("00:00")
                .ClickSaveButton();

            var BookingId1 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId1)[0];
            var BookingId2 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId2)[0];
            var BookingId3 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId3)[0];
            System.Threading.Thread.Sleep(2000);

            //Validate Column Headers
            openEndedAbsencesPopUp
                .WaitForOpenEndedAbsencesPopUpToLoad()
                .ValidateHeaderText("This will add an End Date and create the Absence Booking. The Person is coming back to work. Please review future bookings for reallocation to this person.")

                //Added has the step was not loading in the page.
                .SelectReallocationOptionsByValue("some")

                .OEAbsencesRecordPageValidateHeaderCellText(2, "Booking")
                .OEAbsencesRecordPageValidateHeaderCellText(3, "Booking Type")
                .OEAbsencesRecordPageValidateHeaderCellText(4, "Start Date")
                .OEAbsencesRecordPageValidateHeaderCellText(5, "Start Time")
                .OEAbsencesRecordPageValidateHeaderCellText(6, "End Date")
                .OEAbsencesRecordPageValidateHeaderCellText(7, "End Time")

                .OEAbsencesRecordPageValidateCellText(1, BookingId1.ToString(), 4, StartDate.ToString("yyyy'-'MM'-'dd"))
                .OEAbsencesRecordPageValidateCellText(2, BookingId2.ToString(), 4, StartDate.AddDays(1).ToString("yyyy'-'MM'-'dd"))
                .OEAbsencesRecordPageValidateCellText(3, BookingId3.ToString(), 4, StartDate.AddDays(2).ToString("yyyy'-'MM'-'dd"))

                .ValidateOkButtonVisibility(true)
                .ValidateCancelButtonVisibility(true)

                .SelectReallocationOptionsByValue("some")
                .SelectBookingsToReallocate(1)//Do not reallocate any bookings
                .ClickOkBtn();

            systemUserAbsencesRecordPage
               .WaitForSystemUserAbsencesRecordPageToLoad()
               .ValidateStaffLookupButtondDisabled(true)
               .ValidateProviderLookupButtondDisabled(true)
               .ValidateBookingTypeLookupButtondDisabled(true)
               .ValidateContractLookupButtondDisabled(true)
               .ValidatePlannedStartDateDisabled(true)
               .ValidatePlannedStartDateTimeDisabled(true)
               .ValidatePlannedEndDateDisabled(true)
               .ValidatePlannedEndDateTimeDisabled(true)
               .ValidateCommentsFieldDisabled(true);

        }


        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-8867

        #region BTC-1 Booking

        [TestProperty("JiraIssueID", "ACC-9081")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-8867 - " +
        "Verifying the booking of all types including staff open ended - BTC4 and Person open ended BTC6. " +
        "Both manually creating the booking in the wallchart and diary auto created via open ended absence wizard." +
        "Verifying in Provider diary and employee wall charts for Staff records" +
        "Verifying in People diary and person diary wall charts for Person records.Verifying for BTC1")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Diary Booking")]
        [TestProperty("Screen1", "Employee Diary")]
        [TestProperty("Screen2", "Provider Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_06()
        {
            DateTime bookingStartDate = DateTime.Today;
            DateTime bookingEndDate = bookingStartDate.AddDays(1);
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];

            #region Create a System User Employment
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region create Diary Booking of BTC-1 for current date 

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _bookingType1, _providereId1, bookingStartDate, new TimeSpan(20, 0, 0), bookingEndDate, new TimeSpan(0, 0, 0));
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "", cpBookingDiaryId, _systemUserEmploymentContractId, _systemUserId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            #region validate booking in System User Page

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
               .WaitForSystemUserNoTeamsRecordPageToLoad()
               .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad();

            employeeDiaryPage
                 .MouseHoverDiaryBooking(cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("BTC-1")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .MouseHoverNextDayDiaryBooking(bookingEndDate.ToString("yyyyMMdd"), cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("BTC-1")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in Provider Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            providerDiaryPage
               .WaitForProviderDiaryPageToLoad()
               .SearchProviderRecord(_providerName + " - No Address");

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-1")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            providerDiaryPage
              .WaitForProviderDiaryPageToLoad()
              .ClickNextDateButton();

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-1")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion


        }

        #endregion

        #region BTC-2 booking
        [TestProperty("JiraIssueID", "ACC-9107")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-8867 - " +
            "Verifying the booking of all types including staff open ended - BTC4 and Person open ended BTC6. " +
           "Both manually creating the booking in the wallchart and diary auto created via open ended absence wizard." +
           "Verifying in Provider diary and employee wall charts for Staff records" +
           "Verifying in People diary and person diary wall charts for Person records.Verifying for BTC2")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Diary Booking")]
        [TestProperty("Screen1", "Employee Diary")]
        [TestProperty("Screen2", "Provider Diary")]
        [TestProperty("Screen3", "People Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_07()
        {
            DateTime bookingStartDate = DateTime.Today;
            DateTime bookingEndDate = bookingStartDate.AddDays(1);
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];

            #region Create a System User Employment
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region create Diary Booking of BTC-1 for current date 

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _bookingType2, _providereId1, bookingStartDate, new TimeSpan(20, 0, 0), bookingEndDate, new TimeSpan(0, 0, 0));
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", cpBookingDiaryId, _personID, _personcontractId);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "", cpBookingDiaryId, _systemUserEmploymentContractId, _systemUserId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            #region validate booking in System User Page

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
               .WaitForSystemUserNoTeamsRecordPageToLoad()
               .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad();

            employeeDiaryPage
                 .MouseHoverDiaryBooking(cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("Booking Type: BTC-2")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickTodayButton();

            System.Threading.Thread.Sleep(2000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .MouseHoverNextDayDiaryBooking(bookingEndDate.ToString("yyyyMMdd"), cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("Booking Type: BTC-2")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in Provider Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            providerDiaryPage
               .WaitForProviderDiaryPageToLoad()
               .SearchProviderRecord(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-2")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            providerDiaryPage
              .WaitForProviderDiaryPageToLoad()
              .ClickNextDateButton();

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-2")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in People Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-2");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ClickNextDateButton();

            System.Threading.Thread.Sleep(2000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-2");

            #endregion

        }
        #endregion

        #region BTC-3 Booking

        [TestProperty("JiraIssueID", "ACC-9111")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-8867 - " +
            "Verifying the booking of all types including staff open ended - BTC4 and Person open ended BTC6. " +
           "Both manually creating the booking in the wallchart and diary auto created via open ended absence wizard." +
           "Verifying in Provider diary and employee wall charts for Staff records" +
           "Verifying in People diary and person diary wall charts for Person records.Verifying for BTC3")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Diary Booking")]
        [TestProperty("Screen1", "Employee Diary")]
        [TestProperty("Screen2", "Provider Diary")]
        [TestProperty("Screen3", "People Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_08()
        {
            DateTime bookingStartDate = DateTime.Today;
            DateTime bookingEndDate = bookingStartDate.AddDays(1);
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];

            #region Create a System User Employment
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region create Diary Booking of BTC-1 for current date 

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _bookingType3, _providereId1, bookingStartDate, new TimeSpan(20, 0, 0), bookingEndDate, new TimeSpan(0, 0, 0));
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", cpBookingDiaryId, _personID, _personcontractId);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "", cpBookingDiaryId, _systemUserEmploymentContractId, _systemUserId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            #region validate booking in System User Page

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
               .WaitForSystemUserNoTeamsRecordPageToLoad()
               .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad();

            employeeDiaryPage
                 .MouseHoverDiaryBooking(cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("Booking Type: BTC-3")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickTodayButton();

            System.Threading.Thread.Sleep(2000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .MouseHoverNextDayDiaryBooking(bookingEndDate.ToString("yyyyMMdd"), cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("Booking Type: BTC-3")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in Provider Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            providerDiaryPage
               .WaitForProviderDiaryPageToLoad()
               .SearchProviderRecord(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-3")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            providerDiaryPage
              .WaitForProviderDiaryPageToLoad()
              .ClickNextDateButton();

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-3")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in People Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-3");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ClickNextDateButton();

            System.Threading.Thread.Sleep(2000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-3");

            #endregion

        }
        #endregion

        #region BTC-4 Booking

        [TestProperty("JiraIssueID", "ACC-9113")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-8867 - " +
            "Verifying the booking of all types including staff open ended - BTC4 and Person open ended BTC6. " +
           "Both manually creating the booking in the wallchart and diary auto created via open ended absence wizard." +
           "Verifying in Provider diary and employee wall charts for Staff records" +
           "Verifying in People diary and person diary wall charts for Person records.Verifying for BTC4")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Diary Booking")]
        [TestProperty("Screen1", "Employee Diary")]
        [TestProperty("Screen2", "Provider Diary")]
        [TestProperty("Screen3", "People Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_09()
        {
            DateTime bookingStartDate = DateTime.Today;
            DateTime bookingEndDate = bookingStartDate.AddDays(1);
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];

            #region Create a System User Employment
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region create Diary Booking of BTC-1 for current date 

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _bookingType4, _providereId1, bookingStartDate, new TimeSpan(20, 0, 0), bookingEndDate, new TimeSpan(0, 0, 0));
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", cpBookingDiaryId, _personID, _personcontractId);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "", cpBookingDiaryId, _systemUserEmploymentContractId, _systemUserId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            #region validate booking in System User Page

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
               .WaitForSystemUserNoTeamsRecordPageToLoad()
               .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad();

            employeeDiaryPage
                 .MouseHoverDiaryBooking(cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("Booking Type: BTC-04")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ClickTodayButton();

            System.Threading.Thread.Sleep(2000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .MouseHoverNextDayDiaryBooking(bookingEndDate.ToString("yyyyMMdd"), cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("Booking Type: BTC-04")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in Provider Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            providerDiaryPage
               .WaitForProviderDiaryPageToLoad()
               .SearchProviderRecord(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-04")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            providerDiaryPage
              .WaitForProviderDiaryPageToLoad()
              .ClickNextDateButton();

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-04")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in People Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-04");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ClickNextDateButton();

            System.Threading.Thread.Sleep(2000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-04");

            #endregion

        }
        #endregion

        #region BTC-5 Booking

        [TestProperty("JiraIssueID", "ACC-9115")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-8867 - " +
            "Verifying the booking of all types including staff open ended - BTC4 and Person open ended BTC6. " +
           "Both manually creating the booking in the wallchart and diary auto created via open ended absence wizard." +
           "Verifying in Provider diary and employee wall charts for Staff records" +
           "Verifying in People diary and person diary wall charts for Person records.Verifying for BTC5")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Diary Booking")]
        [TestProperty("Screen1", "Employee Diary")]
        [TestProperty("Screen2", "Provider Diary")]
        [TestProperty("Screen3", "People Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_10()
        {
            DateTime bookingStartDate = DateTime.Today;
            DateTime bookingEndDate = bookingStartDate.AddDays(1);
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];

            #region Create a System User Employment
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region create Diary Booking of BTC-5 for current date 

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _bookingType5, _providereId1, bookingStartDate, new TimeSpan(20, 0, 0), bookingEndDate, new TimeSpan(0, 0, 0));
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", cpBookingDiaryId, _personID, _personcontractId);
            dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "", cpBookingDiaryId, _systemUserEmploymentContractId, _systemUserId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            #region validate booking in System User Page

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_systemUsername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
               .WaitForSystemUserNoTeamsRecordPageToLoad()
               .NavigateToEmployeeDiaryPage();

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad();

            employeeDiaryPage
                 .MouseHoverDiaryBooking(cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(2000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("Booking Type: BTC-05")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad();

            System.Threading.Thread.Sleep(2000);

            employeeDiaryPage
                .ClickTodayButton()
                .WaitForEmployeeDiaryPageToLoad()
                .MouseHoverNextDayDiaryBooking(bookingEndDate.ToString("yyyyMMdd"), cpBookingDiaryId.ToString());

            System.Threading.Thread.Sleep(1000);

            employeeDiaryPage
                .WaitForEmployeeDiaryPageToLoad()
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateProviderLabelText(_providerName)
                .ValidateBookingTypeLabelText("Booking Type: BTC-05")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in Provider Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            providerDiaryPage
               .WaitForProviderDiaryPageToLoad()
               .SearchProviderRecord(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-05")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            providerDiaryPage
              .WaitForProviderDiaryPageToLoad()
              .ClickNextDateButton();

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("BTC-05")
                .ValidateStaffLabelText("Staff: expressbook testuser");

            #endregion

            #region Validate Booking in People Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-05");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ClickNextDateButton();

            System.Threading.Thread.Sleep(2000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-05");

            #endregion

        }
        #endregion

        #region BTC-6 Booking

        [TestProperty("JiraIssueID", "ACC-9135")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-8867 - " +
            "Verifying the booking of all types including staff open ended - BTC4 and Person open ended BTC6. " +
           "Both manually creating the booking in the wallchart and diary auto created via open ended absence wizard." +
           "Verifying in Provider diary and employee wall charts for Staff records" +
           "Verifying in People diary and person diary wall charts for Person records.Verifying for BTC6")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Diary Booking")]
        [TestProperty("Screen1", "Employee Diary")]
        [TestProperty("Screen2", "Provider Diary")]
        [TestProperty("Screen3", "People Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_11()
        {
            DateTime bookingStartDate = DateTime.Today;
            DateTime bookingEndDate = bookingStartDate.AddDays(1);
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];

            #region Create a System User Employment
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types

            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType6);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region create Diary Booking of BTC-6 for current date 

            var cpBookingDiaryId = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _bookingType6, _providereId1, bookingStartDate, new TimeSpan(20, 0, 0), bookingEndDate, new TimeSpan(0, 0, 0));
            dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", cpBookingDiaryId, _personID, _personcontractId);
            // dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "", cpBookingDiaryId, _systemUserEmploymentContractId, _systemUserId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToPeopleSection();

            #region validate the booking BTC-6 in Person Diary

            peoplePage
               .WaitForPeoplePageToLoad()
               .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
               .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapDiaryTab();

            personDiaryRecordPage
                .WaitForPersonDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-6");

            personDiaryRecordPage
                .WaitForPersonDiaryPageToLoad()
                .ClickTodayButton();

            System.Threading.Thread.Sleep(2000);

            personDiaryRecordPage
               .WaitForPersonDiaryPageToLoad()
               .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
               .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
               .ValidateBookingTypeLabelText("Booking Type: BTC-6");

            #endregion

            #region Validate Booking in People Diary Page

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToPeopleDiarySection();

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .selectProvider(_providerName + " - No Address");

            System.Threading.Thread.Sleep(3000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-6");

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .ClickNextDateButton();

            System.Threading.Thread.Sleep(2000);

            peopleDiaryPage
                .WaitForPeopleDiaryPageToLoad()
                .MouseHoverDiaryBooking(cpBookingDiaryId.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingEndDate.ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateBookingTypeLabelText("Booking Type: BTC-6");

            #endregion

        }
        #endregion

        #region BTC-4 Open Ended Absence Booking with Duration start time 20:00 and End time 00:00

        [TestProperty("JiraIssueID", "ACC-9149")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-8867 - " +
          "Create a  diary booking with open ended absence BTC 6 which ends at midnight Ex: Booking having duration of 4 hrs(20:00hrs to 00:00hrs).The booking should be created for the duration it is created for.Verify the display and booking details in possible diary wallcharts" +
          "To create absence required Booking type should be of Absence booking type (Class -4, Absence -Yes, Open ended absence -Yes)" +
          "Selected Provider should be configured with Absence booking type)"
          + "Booking should be created through schedule using express book functionality.Only schedule bookings are available to reallocate & not able to reallocate standalone bookings created  through dairy")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Open ended Absences")]
        [TestProperty("Screen2", "Provider Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_12()
        {

            #region Create a System User Employment Contract for Employment Contract Type - Salaried
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types with the Employment Contract created previously
            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region Create CPBookingSchedule
            //Create the provider schedule records for the user
            //BTC-1 for monday

            var cpBookingScheduleId1 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType1, 1, 1, 1, new TimeSpan(20, 0, 0), new TimeSpan(0, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId1, _systemUserEmploymentContractId, _systemUserId);

            //BTC-2 for tuesday
            var cpBookingScheduleId2 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType2, 1, 1, 1, new TimeSpan(20, 0, 0), new TimeSpan(0, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId2, _systemUserEmploymentContractId, _systemUserId);

            //BTC-3 for wednesday
            var cpBookingScheduleId3 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType3, 1, 1, 1, new TimeSpan(20, 0, 0), new TimeSpan(0, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId3, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region create express booking for the coming week with status=1 pending
            DateTime StartDate = GetNextWeekFirstMonday();
            DateTime bookingStartDate = StartDate;
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var employmentcontractname = (string)(dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId, "name"))["name"];
            _expressbookingcriteriaid = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", 1, null, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture().Date, _systemUserEmploymentContractId, "systemuseremploymentcontract", employmentcontractname);

            _expressBookingProcessed1 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId1, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed2 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId2, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed3 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId3, _expressbookingcriteriaid, DateTime.Now);
            #endregion

            #region execute the scheduled job to pin the records
            Guid systemUserEmploymentContractJobId = dbHelper.scheduledJob.GetScheduledJobByRecordId(_systemUserEmploymentContractId)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(systemUserEmploymentContractJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(systemUserEmploymentContractJobId);

            System.Threading.Thread.Sleep(2000);
            #endregion

            #region Create OpenEndedAbsence

            DateTime openendedabsencestartdate = DateTime.Now;
            DateTime openendedabsenceenddate = openendedabsencestartdate.AddDays(1);
            var _openendedabsenceid = dbHelper.OpenEndedAbsence.CreateOpenEndedAbsence(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _providereId1, _bookingType4, _systemUserId, _systemUserEmploymentContractId, openendedabsencestartdate);

            #endregion

            var _loginusername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "username")["username"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_loginusername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserNoTeamsRecordPageToLoad()
                .NavigateToAbsencesSubPage();

            systemUserAbsencesPage
                .WaitForSystemUserAbsencesPageToLoad()
                .OpenRecord(_openendedabsenceid.ToString());

            systemUserAbsencesRecordPage
                .WaitForSystemUserAbsencesRecordPageToLoad()
                .InsertOpenEndedAbsenceStartDate(openendedabsencestartdate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceStartTime("20:00")
                .InsertOpenEndedAbsenceEndDate(openendedabsenceenddate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceEndTime("00:00")
                .ClickSaveButton();

            var BookingId1 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId1)[0];
            var BookingId2 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId2)[0];
            var BookingId3 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId3)[0];

            //Reallocate all available bookings
            openEndedAbsencesPopUp
                .WaitForOpenEndedAbsencesPopUpToLoad()
                .ValidateHeaderText("This will add an End Date and create the Absence Booking. The Person is coming back to work. Please review future bookings for reallocation to this person.")
                .SelectReallocationOptionsByValue("none")
                .ClickOkBtn();

            systemUserAbsencesRecordPage
               .WaitForSystemUserAbsencesRecordPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

            DateTime providerDate = StartDate;

            #region Validate Booking in Provider Diary Page

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(provider + " - No Address")
                .ClickChangeDate(providerDate.ToString("yyyy"), providerDate.ToString("MMMM"), providerDate.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                 .ClickViewWeekButton();

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(BookingId1.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingStartDate.AddDays(7).ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateStaffLabelText(": Unassigned × 1")

               .WaitForProviderDiaryPageToLoad()
               .MouseHoverDiaryBooking(BookingId2.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingStartDate.AddDays(7).ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateStaffLabelText(": Unassigned × 1")

                .MouseHoverDiaryBooking(BookingId3.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingStartDate.AddDays(7).ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateStaffLabelText(": Unassigned × 1");


            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickNextDateButton();

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(BookingId1.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingStartDate.AddDays(7).ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateStaffLabelText(": Unassigned × 1")

                .MouseHoverDiaryBooking(BookingId2.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingStartDate.AddDays(7).ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateStaffLabelText(": Unassigned × 1")

                .MouseHoverDiaryBooking(BookingId3.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 20:00 - " + bookingStartDate.AddDays(7).ToString("dd'/'MM'/'yyyy") + " 00:00")
                .ValidateStaffLabelText(": Unassigned × 1");


            #endregion



        }

        #endregion

        #region BTC-4 Open Ended Absence Booking with Duration start time 20:00 and End time 00:00

        [TestProperty("JiraIssueID", "ACC-9149")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-8867 - " +
          "Create a  diary booking with open ended absence BTC 6 which ends at midnight Ex: Booking having duration of 4 hrs(24hrs).The booking should be created for the duration it is created for.Verify the display and booking details in possible diary wallcharts" +
          "To create absence required Booking type should be of Absence booking type (Class -4, Absence -Yes, Open ended absence -Yes)" +
          "Selected Provider should be configured with Absence booking type)"
          + "Booking should be created through schedule using express book functionality.Only schedule bookings are available to reallocate & not able to reallocate standalone bookings created  through dairy")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule", "Care Provider Scheduling")]
        [TestProperty("Screen1", "Open ended Absences")]
        [TestProperty("Screen2", "Provider Diary")]
        public void OpenEndedAbsence_VerifyAbsenceBooking_13()
        {

            #region Create a System User Employment Contract for Employment Contract Type - Salaried
            //Create a System User Employment Contract for Employment Contract Type - Salaried
            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());
            #endregion

            #region Link Booking Types with the Employment Contract created previously
            //Link Booking Types with the Employment Contract created previously
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType4);
            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

            #endregion

            #region Create User Work Schedule
            //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
            CreateUserWorkSchedule(_systemUserId, _careDirectorQA_TeamId, _systemUserEmploymentContractId, _recruitmentRoleApplicantId, _applicantId);

            #endregion

            #region Create CPBookingSchedule
            //Create the provider schedule records for the user
            //BTC-1 for monday

            var cpBookingScheduleId1 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType1, 1, 1, 2, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId1, _systemUserEmploymentContractId, _systemUserId);

            //BTC-2 for tuesday
            var cpBookingScheduleId2 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType2, 1, 1, 2, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId2, _systemUserEmploymentContractId, _systemUserId);

            //BTC-3 for wednesday
            var cpBookingScheduleId3 = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(_careDirectorQA_TeamId, _bookingType3, 1, 1, 2, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), _providereId1, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(_careDirectorQA_TeamId, cpBookingScheduleId3, _systemUserEmploymentContractId, _systemUserId);

            #endregion

            #region create express booking for the coming week with status=1 pending
            DateTime StartDate = GetNextWeekFirstMonday();
            DateTime bookingStartDate = StartDate;
            DateTime bookingEndDate = bookingStartDate.AddDays(6);

            var employmentcontractname = (string)(dbHelper.systemUserEmploymentContract.GetSystemUserEmploymentContractByID(_systemUserEmploymentContractId, "name"))["name"];
            _expressbookingcriteriaid = dbHelper.cpExpressBookingCriteria.CreateCPExpressBookingCriteria(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", 1, null, bookingStartDate, bookingEndDate, commonMethodsHelper.GetCurrentDateWithoutCulture().Date, _systemUserEmploymentContractId, "systemuseremploymentcontract", employmentcontractname);

            _expressBookingProcessed1 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId1, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed2 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId2, _expressbookingcriteriaid, DateTime.Now);
            _expressBookingProcessed3 = dbHelper.cpExpressBookingProcessed.CreateCPExpressBookingProcessed(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _systemUserEmploymentContractId, "SystemUserEmploymentContract", employmentcontractname, cpBookingScheduleId3, _expressbookingcriteriaid, DateTime.Now);
            #endregion

            #region execute the scheduled job to pin the records
            Guid systemUserEmploymentContractJobId = dbHelper.scheduledJob.GetScheduledJobByRecordId(_systemUserEmploymentContractId)[0];

            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Expand and Process GL Code Update Triggers" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(systemUserEmploymentContractJobId.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);

            //reset the dbHelper because of the athentication using the web api class
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(systemUserEmploymentContractJobId);

            System.Threading.Thread.Sleep(2000);
            #endregion

            #region Create OpenEndedAbsence

            DateTime openendedabsencestartdate = DateTime.Now;
            DateTime openendedabsenceenddate = openendedabsencestartdate.AddDays(1);
            var _openendedabsenceid = dbHelper.OpenEndedAbsence.CreateOpenEndedAbsence(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "", _providereId1, _bookingType4, _systemUserId, _systemUserEmploymentContractId, openendedabsencestartdate);

            #endregion

            var _loginusername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "username")["username"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                 .WaitForMainMenuToLoad()
                 .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_loginusername)
                .ClickSearchButton()
                .OpenRecord(_systemUserId);

            systemUserRecordPage
                .WaitForSystemUserNoTeamsRecordPageToLoad()
                .NavigateToAbsencesSubPage();

            systemUserAbsencesPage
                .WaitForSystemUserAbsencesPageToLoad()
                .OpenRecord(_openendedabsenceid.ToString());

            systemUserAbsencesRecordPage
                .WaitForSystemUserAbsencesRecordPageToLoad()
                .InsertOpenEndedAbsenceStartDate(openendedabsencestartdate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceStartTime("00:00")
                .InsertOpenEndedAbsenceEndDate(openendedabsenceenddate.ToString("dd'/'MM'/'yyyy"))
                .InsertOpenEndedAbsenceEndTime("00:00")
                .ClickSaveButton();

            var BookingId1 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId1)[0];
            var BookingId2 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId2)[0];
            var BookingId3 = dbHelper.cPBookingDiary.GetByScheduleid(cpBookingScheduleId3)[0];

            //Reallocate all available bookings
            openEndedAbsencesPopUp
                .WaitForOpenEndedAbsencesPopUpToLoad()
                .ValidateHeaderText("This will add an End Date and create the Absence Booking. The Person is coming back to work. Please review future bookings for reallocation to this person.")
                .SelectReallocationOptionsByValue("all")
                .ClickOkBtn();

            systemUserAbsencesRecordPage
               .WaitForSystemUserAbsencesRecordPageToLoad();

            mainMenu
               .WaitForMainMenuToLoad(true, true, true, false, false, false)
               .NavigateToProviderDiarySection();

            var provider = (string)(dbHelper.provider.GetProviderByID(_providereId1, "name"))["name"];

            DateTime providerDate = StartDate;

            #region Validate Booking in Provider Diary Page

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .SearchProviderRecord(provider + " - No Address")
                .ClickChangeDate(providerDate.ToString("yyyy"), providerDate.ToString("MMMM"), providerDate.Day.ToString());

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad();
               

            System.Threading.Thread.Sleep(2000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(BookingId1.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00 - " + bookingStartDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + " 00:00")
              
               .MouseHoverDiaryBooking(BookingId2.ToString())
               .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00 - " + bookingStartDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + " 00:00")
             
                .MouseHoverDiaryBooking(BookingId3.ToString())
               .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00 - " + bookingStartDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + " 00:00");


            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .ClickNextDateButton();

            System.Threading.Thread.Sleep(3000);

            providerDiaryPage
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(BookingId1.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00 - " + bookingStartDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + " 00:00")
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(BookingId2.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00 - " + bookingStartDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + " 00:00")
                .WaitForProviderDiaryPageToLoad()
                .MouseHoverDiaryBooking(BookingId3.ToString())
                .ValidateTimeLabelText("Planned Time: " + bookingStartDate.ToString("dd'/'MM'/'yyyy") + " 00:00 - " + bookingStartDate.AddDays(1).ToString("dd'/'MM'/'yyyy") + " 00:00");


            #endregion



        }

        #endregion

        #endregion
        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId, Guid recruitmentRoleApplicantId, Guid applicantid)
        {
            for (int i = 0; i < 7; i++)
            {
                var workScheduleDate = DateTime.Now.AddDays(i).Date;

                switch (workScheduleDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:

                        dbHelper.userWorkSchedule.CreateUserWorkSchedule("title", UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), recruitmentRoleApplicantId, applicantid, 1);
                        break;
                    case DayOfWeek.Monday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule("title", UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), recruitmentRoleApplicantId, applicantid, 1);
                        break;
                    case DayOfWeek.Tuesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule("title", UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), recruitmentRoleApplicantId, applicantid, 1);
                        break;
                    case DayOfWeek.Wednesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule("title", UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), recruitmentRoleApplicantId, applicantid, 1);
                        break;
                    case DayOfWeek.Thursday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule("title", UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), recruitmentRoleApplicantId, applicantid, 1);
                        break;
                    case DayOfWeek.Friday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule("title", UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), recruitmentRoleApplicantId, applicantid, 1);
                        break;
                    case DayOfWeek.Saturday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule("title", UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), recruitmentRoleApplicantId, applicantid, 1);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}


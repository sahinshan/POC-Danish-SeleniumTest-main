
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.SmokeTests
{
    //[TestClass]
    public class ExpressBook : FunctionalTest
    {
        internal string _tenantName;
        internal Guid _authenticationproviderid;
        internal Guid _productLanguageId;

        internal Guid _businessUnitId1;
        internal Guid _businessUnitId2;

        internal Guid _teamId1;
        internal Guid _teamId2;
        internal Guid _teamId3;
        internal Guid _teamId4;

        internal Guid _bookingType1;
        internal Guid _bookingType2;
        internal Guid _bookingType3;
        internal Guid _bookingType4;

        internal Guid _bookingType5;
        internal Guid _bookingType6;
        internal Guid _bookingType7;
        internal Guid _bookingType8;

        internal Guid _bookingType9;
        internal Guid _bookingType10;
        internal Guid _bookingType11;
        internal Guid _bookingType12;

        internal Guid _employmentContractTypeid1;
        internal Guid _employmentContractTypeid2;
        internal Guid _employmentContractTypeid3;
        internal Guid _employmentContractTypeid4;


        internal Guid _careProviderStaffRoleTypeid;

        internal Guid _providereId1;
        internal Guid _providereId2;
        internal Guid _providereId3;
        internal Guid _providereId4;


        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;


        internal Guid _availabilityTypes_StandardId;
        internal Guid _availabilityTypes_OverTimeId;

        //[TestInitialize()]
        public void LoadScript_Setup()
        {

            #region Environment Name

            _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);

            #endregion

            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion

            #region Default User

            var userid = dbHelper.systemUser.GetSystemUserByUserName("administrator").FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

            #endregion

            #region Business Units

            if (!dbHelper.businessUnit.GetByName("BU 1").Any())
                dbHelper.businessUnit.CreateBusinessUnit("BU 1");
            _businessUnitId1 = dbHelper.businessUnit.GetByName("BU 1")[0];

            if (!dbHelper.businessUnit.GetByName("BU 2").Any())
                dbHelper.businessUnit.CreateBusinessUnit("BU 2");
            _businessUnitId2 = dbHelper.businessUnit.GetByName("BU 2")[0];

            #endregion

            #region Teams

            if (!dbHelper.team.GetTeamIdByName("Team 1").Any())
                dbHelper.team.CreateTeam("Team 1", null, _businessUnitId1, "90400", "Team1@careworkstempmail.com", "Team 1", "020 123456");
            _teamId1 = dbHelper.team.GetTeamIdByName("Team 1")[0];

            if (!dbHelper.team.GetTeamIdByName("Team 2").Any())
                dbHelper.team.CreateTeam("Team 2", null, _businessUnitId1, "90400", "Team2@careworkstempmail.com", "Team 2", "020 123456");
            _teamId2 = dbHelper.team.GetTeamIdByName("Team 2")[0];

            if (!dbHelper.team.GetTeamIdByName("Team 3").Any())
                dbHelper.team.CreateTeam("Team 3", null, _businessUnitId2, "90400", "Team3@careworkstempmail.com", "Team 3", "020 123456");
            _teamId3 = dbHelper.team.GetTeamIdByName("Team 3")[0];

            if (!dbHelper.team.GetTeamIdByName("Team 4").Any())
                dbHelper.team.CreateTeam("Team 4", null, _businessUnitId2, "90400", "Team4@careworkstempmail.com", "Team 4", "020 123456");
            _teamId4 = dbHelper.team.GetTeamIdByName("Team 4")[0];

            #endregion

            #region Language

            var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
            if (!language)
                dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
            _productLanguageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

            #endregion Language




            #region Booking Type 1 -> "Booking (to location)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 1").Any())
                _bookingType1 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 1", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null);

            if (_bookingType1 == Guid.Empty)
                _bookingType1 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 1").First();

            #endregion

            #region Booking Type 2 -> "Booking (to internal care activity)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 2").Any())
                _bookingType2 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null);

            if (_bookingType2 == Guid.Empty)
                _bookingType2 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 2").First();

            #endregion

            #region Booking Type 3 -> "Booking (to external care activity)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 3").Any())
                _bookingType3 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null);

            if (_bookingType3 == Guid.Empty)
                _bookingType3 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 3").First();

            #endregion

            #region Booking Type 4 -> "Booking (to internal non-care booking e.g. annual leave, training)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 4").Any())
                _bookingType4 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 4", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, true, false, true, false, true, false);

            if (_bookingType4 == Guid.Empty)
                _bookingType4 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 4").First();

            #endregion


            #region Booking Type 5 -> "Booking (to location)" & "Cap Booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 5").Any())
                _bookingType5 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 5", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);

            if (_bookingType5 == Guid.Empty)
                _bookingType5 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 5").First();

            #endregion

            #region Booking Type 6 -> "Booking (to internal care activity)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 6").Any())
                _bookingType6 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 6", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, null);

            if (_bookingType6 == Guid.Empty)
                _bookingType6 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 6").First();

            #endregion

            #region Booking Type 7 -> "Booking (to external care activity)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 7").Any())
                _bookingType7 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 7", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, null);

            if (_bookingType7 == Guid.Empty)
                _bookingType7 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 7").First();

            #endregion

            #region Booking Type 8 -> "Booking (to internal non-care booking e.g. annual leave, training)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 8").Any())
                _bookingType8 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 8", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, false, true, false, true, false);

            if (_bookingType8 == Guid.Empty)
                _bookingType8 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 8").First();

            #endregion


            #region Booking Type 9 -> "Booking (to location)" & "Don’t include in "Working" hours"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 9").Any())
                _bookingType9 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 9", 1, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 3, false, null);

            if (_bookingType9 == Guid.Empty)
                _bookingType9 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 9").First();

            #endregion

            #region Booking Type 10 -> "Booking (to internal care activity)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 10").Any())
                _bookingType10 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 10", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 3, false, null);

            if (_bookingType10 == Guid.Empty)
                _bookingType10 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 10").First();

            #endregion

            #region Booking Type 11 -> "Booking (to external care activity)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 11").Any())
                _bookingType11 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 11", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 3, false, null);

            if (_bookingType11 == Guid.Empty)
                _bookingType11 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 11").First();

            #endregion

            #region Booking Type 12 -> "Booking (to internal non-care booking e.g. annual leave, training)" & "Count full booking length"

            if (!dbHelper.cpBookingType.GetByName("PerfTest Booking Type 12").Any())
                _bookingType12 = dbHelper.cpBookingType.CreateBookingType("PerfTest Booking Type 12", 4, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 3, true, false, true, false, true, false);

            if (_bookingType12 == Guid.Empty)
                _bookingType12 = dbHelper.cpBookingType.GetByName("PerfTest Booking Type 12").First();

            #endregion



            #region Care provider staff role type

            if (!dbHelper.careProviderStaffRoleType.GetByName("Express Book - Role 1").Any())
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(_teamId1, "Express Book - Role 1", "9999", null, new DateTime(2020, 1, 1), null);

            if (_careProviderStaffRoleTypeid == Guid.Empty)
                _careProviderStaffRoleTypeid = dbHelper.careProviderStaffRoleType.GetByName("Express Book - Role 1").FirstOrDefault();

            #endregion



            #region Employment Contract Type - Salaried

            if (!dbHelper.employmentContractType.GetByName("Salaried").Any())
                _employmentContractTypeid1 = dbHelper.employmentContractType.CreateEmploymentContractType(_teamId1, "Salaried", "", null, new DateTime(2020, 1, 1));

            if (_employmentContractTypeid1 == Guid.Empty)
                _employmentContractTypeid1 = dbHelper.employmentContractType.GetByName("Salaried").FirstOrDefault();

            #endregion

            #region Employment Contract Type - Hourly

            if (!dbHelper.employmentContractType.GetByName("Hourly").Any())
                _employmentContractTypeid2 = dbHelper.employmentContractType.CreateEmploymentContractType(_teamId1, "Hourly", "", null, new DateTime(2020, 1, 1));

            if (_employmentContractTypeid2 == Guid.Empty)
                _employmentContractTypeid2 = dbHelper.employmentContractType.GetByName("Hourly").FirstOrDefault();

            #endregion

            #region Employment Contract Type - Volunteer

            if (!dbHelper.employmentContractType.GetByName("Volunteer").Any())
                _employmentContractTypeid3 = dbHelper.employmentContractType.CreateEmploymentContractType(_teamId1, "Volunteer", "", null, new DateTime(2020, 1, 1));

            if (_employmentContractTypeid3 == Guid.Empty)
                _employmentContractTypeid3 = dbHelper.employmentContractType.GetByName("Volunteer").FirstOrDefault();

            #endregion

            #region Employment Contract Type - Contracted

            if (!dbHelper.employmentContractType.GetByName("Contracted").Any())
                _employmentContractTypeid4 = dbHelper.employmentContractType.CreateEmploymentContractType(_teamId1, "Contracted", "", null, new DateTime(2020, 1, 1));

            if (_employmentContractTypeid4 == Guid.Empty)
                _employmentContractTypeid4 = dbHelper.employmentContractType.GetByName("Contracted").FirstOrDefault();

            #endregion



            #region Provider 1

            if (!dbHelper.provider.GetProviderByName("Express Book Provider 1").Any())
            {
                _providereId1 = dbHelper.provider.CreateProvider("Express Book Provider 1", _teamId1, 3, true); //create a "Residential Establishment" provider

                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType1, true);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType2, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType3, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType4, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType5, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType6, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType7, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType8, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType9, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType10, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType11, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId1, _providereId1, _bookingType12, false);
            }

            if (_providereId1 == Guid.Empty)
                _providereId1 = dbHelper.provider.GetProviderByName("Express Book Provider 1").First();

            #endregion

            #region Provider 2

            if (!dbHelper.provider.GetProviderByName("Express Book Provider 2").Any())
            {
                _providereId2 = dbHelper.provider.CreateProvider("Express Book Provider 2", _teamId2, 3, true); //create a "Residential Establishment" provider

                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType1, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType2, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType3, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType4, true);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType5, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType6, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType7, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType8, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType9, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType10, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType11, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId2, _providereId2, _bookingType12, false);
            }

            if (_providereId2 == Guid.Empty)
                _providereId2 = dbHelper.provider.GetProviderByName("Express Book Provider 2").First();

            #endregion

            #region Provider 3

            if (!dbHelper.provider.GetProviderByName("Express Book Provider 3").Any())
            {
                _providereId3 = dbHelper.provider.CreateProvider("Express Book Provider 3", _teamId3, 3, true); //create a "Residential Establishment" provider

                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType1, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType2, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType3, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType4, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType5, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType6, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType7, true);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType8, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType9, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType10, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType11, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId3, _providereId3, _bookingType12, false);
            }

            if (_providereId3 == Guid.Empty)
                _providereId3 = dbHelper.provider.GetProviderByName("Express Book Provider 3").First();

            #endregion

            #region Provider 4

            if (!dbHelper.provider.GetProviderByName("Express Book Provider 4").Any())
            {
                _providereId4 = dbHelper.provider.CreateProvider("Express Book Provider 4", _teamId4, 3, true); //create a "Residential Establishment" provider

                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType1, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType2, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType3, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType4, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType5, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType6, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType7, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType8, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType9, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType10, true);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType11, false);
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(_teamId4, _providereId4, _bookingType12, false);
            }

            if (_providereId4 == Guid.Empty)
                _providereId4 = dbHelper.provider.GetProviderByName("Express Book Provider 4").First();

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

            _availabilityTypes_StandardId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Standard").First();
            _availabilityTypes_OverTimeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("OverTime").First();

            //TODO: verify if the "For Diary Bookings" and "For Schedule Bookings" are set to valid

            #endregion

        }



        //[Description("")]
        //[TestMethod]
        public void ExpressBook_InsertTestData()
        {
            //We are gonna create 50 users with an Employment Contract with the 'Type' field set to 'Salaried'
            for (int i = 1; i <= 50; i++)
            {
                var currentTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                var username = "expressbook_testuser_" + currentTime;
                var firstName = "expressbook";
                var lastName = "testuser_" + currentTime;
                var fullName = firstName + " " + lastName;
                var userid = dbHelper.systemUser.CreateSystemUser(username, firstName, lastName, fullName, "Passw0rd_!", username + "@mail.com", username + "@securemail.com", "GMT Standard Time", null, 1, _productLanguageId, _authenticationproviderid, _businessUnitId1, _teamId1, false, 1, 168);

                //Create a System User Employment Contract
                var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userid, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId1, _employmentContractTypeid1, 47);

                //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userid, GetThisWeekFirstMonday());

                //Link Booking Types with the Employment Contract created previously
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);


                //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
                CreateUserWorkSchedule(userid, _teamId1, _systemUserEmploymentContractId);


                //Create the provider schedule records for the user
                //CreateCPBookingSchedule_OverTime_Morning(_teamId1, _bookingType1, _providereId1, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_Standard(_teamId1, _bookingType2, _providereId1, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_OverTime_Night(_teamId1, _bookingType3, _providereId1, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_Afternoon(_teamId1, _bookingType4, _providereId1, _systemUserEmploymentContractId, userid);

            }

            //We are gonna create 50 users with an Employment Contract with the 'Type' field set to 'Hourly'
            for (int i = 1; i <= 50; i++)
            {
                var currentTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                var username = "expressbook_testuser_" + currentTime;
                var firstName = "expressbook";
                var lastName = "testuser_" + currentTime;
                var fullName = firstName + " " + lastName;
                var userid = dbHelper.systemUser.CreateSystemUser(username, firstName, lastName, fullName, "Passw0rd_!", username + "@mail.com", username + "@securemail.com", "GMT Standard Time", null, 1, _productLanguageId, _authenticationproviderid, _businessUnitId1, _teamId2, false, 1, 168);


                //Create a System User Employment Contract
                var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userid, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId2, _employmentContractTypeid2, 47);

                //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userid, GetThisWeekFirstMonday());

                //Link Booking Types with the Employment Contract created previously
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType6);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType7);


                //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
                CreateUserWorkSchedule(userid, _teamId2, _systemUserEmploymentContractId);

                //Create the provider schedule records for the user
                //CreateCPBookingSchedule_OverTime_Morning(_teamId2, _bookingType5, _providereId2, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_Standard(_teamId2, _bookingType6, _providereId2, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_OverTime_Night(_teamId2, _bookingType7, _providereId2, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_Afternoon(_teamId2, _bookingType8, _providereId2, _systemUserEmploymentContractId, userid);
            }

            //We are gonna create 50 users with an Employment Contract with the 'Type' field set to 'Volunteer'
            for (int i = 1; i <= 50; i++)
            {
                var currentTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                var username = "expressbook_testuser_" + currentTime;
                var firstName = "expressbook";
                var lastName = "testuser_" + currentTime;
                var fullName = firstName + " " + lastName;
                var userid = dbHelper.systemUser.CreateSystemUser(username, firstName, lastName, fullName, "Passw0rd_!", username + "@mail.com", username + "@securemail.com", "GMT Standard Time", null, 1, _productLanguageId, _authenticationproviderid, _businessUnitId1, _teamId1, false, 1, 168);

                //Create a System User Employment Contract
                var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userid, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId1, _employmentContractTypeid3, 47);

                //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userid, GetThisWeekFirstMonday());

                //Link Booking Types with the Employment Contract created previously
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType9);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType10);

                //set the "Works At" field
                dbHelper.systemUserEmploymentContractTeam.CreateSystemUserEmploymentContractTeam(_systemUserEmploymentContractId, _teamId3);

                //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
                CreateUserWorkSchedule(userid, _teamId1, _systemUserEmploymentContractId);

                //Create the provider schedule records for the user
                //CreateCPBookingSchedule_OverTime_Morning(_teamId3, _bookingType1, _providereId3, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_Standard(_teamId3, _bookingType9, _providereId3, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_OverTime_Night(_teamId3, _bookingType10, _providereId3, _systemUserEmploymentContractId, userid);
                CreateCPBookingSchedule_Afternoon(_teamId3, _bookingType12, _providereId3, _systemUserEmploymentContractId, userid);
            }


            //We are gonna create 50 users with an Employment Contract with the 'Type' field set to 'Contracted'
            for (int i = 1; i <= 50; i++)
            {
                var currentTime = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                var username = "expressbook_testuser_" + currentTime;
                var firstName = "expressbook";
                var lastName = "testuser_" + currentTime;
                var fullName = firstName + " " + lastName;
                var userid = dbHelper.systemUser.CreateSystemUser(username, firstName, lastName, fullName, "Passw0rd_!", username + "@mail.com", username + "@securemail.com", "GMT Standard Time", null, 1, _productLanguageId, _authenticationproviderid, _businessUnitId1, _teamId2, false, 1, 168);

                //Create a System User Employment Contract
                var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(userid, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId2, _employmentContractTypeid4, 47);

                //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(userid, GetThisWeekFirstMonday());

                //Link Booking Types with the Employment Contract created previously
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType11);

                //set the "Works At" field
                dbHelper.systemUserEmploymentContractTeam.CreateSystemUserEmploymentContractTeam(_systemUserEmploymentContractId, _teamId4);

                //Create the user work schedule for all days of the week (3 schedules are created per day: from 00:05:00 to 08:45:00 | from 09:00:00 to 18:00:00 | from 18:15:00 to 23:45:00)
                CreateUserWorkSchedule(userid, _teamId2, _systemUserEmploymentContractId);

                //Create the provider schedule records for the user
                //CreateCPBookingSchedule_OverTime_Morning(_teamId4, _bookingType3, _providereId4, _systemUserEmploymentContractId, userid, false);
                CreateCPBookingSchedule_Standard(_teamId4, _bookingType5, _providereId4, _systemUserEmploymentContractId, userid, false);
                CreateCPBookingSchedule_OverTime_Night(_teamId4, _bookingType11, _providereId4, _systemUserEmploymentContractId, userid, false);
                CreateCPBookingSchedule_AnnualLeave(_teamId4, _bookingType4, _providereId4, _systemUserEmploymentContractId, userid);
            }
        }

        internal void CreateUserWorkSchedule(Guid UserId, Guid TeamId, Guid SystemUserEmploymentContractId)
        {
            for (int i = 0; i < 7; i++)
            {
                var workScheduleDate = DateTime.Now.AddDays(i).Date;

                switch (workScheduleDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(9, 00, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSundayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(18, 00, 0), new TimeSpan(00, 00, 0), 1);
                        break;
                    case DayOfWeek.Monday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(9, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekMondayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(18, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Tuesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(9, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekTuesdayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(18, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Wednesday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(9, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekWednesdayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(18, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Thursday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(9, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekThursdayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(18, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Friday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(9, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekFridayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(18, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    case DayOfWeek.Saturday:
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, _availabilityTypes_StandardId, workScheduleDate, null, new TimeSpan(9, 0, 0), new TimeSpan(18, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(0, 0, 0), new TimeSpan(9, 0, 0), 1);
                        dbHelper.userWorkSchedule.CreateUserWorkSchedule(UserId, TeamId, _recurrencePattern_Every1WeekSaturdayId, SystemUserEmploymentContractId, _availabilityTypes_OverTimeId, workScheduleDate, null, new TimeSpan(18, 0, 0), new TimeSpan(0, 0, 0), 1);
                        break;
                    default:
                        break;
                }
            }
        }

        internal DateTime GetThisWeekFirstMonday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }

        internal void CreateCPBookingSchedule_OverTime_Morning(Guid TeamID, Guid BookingTypeId, Guid ProviderID, Guid SystemUserEmploymentContractId, Guid SystemUserId, bool InsertOnSunday = true)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(4, 15, 0), new TimeSpan(5, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(4, 15, 0), new TimeSpan(5, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(4, 15, 0), new TimeSpan(5, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(4, 15, 0), new TimeSpan(5, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(4, 15, 0), new TimeSpan(5, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(4, 15, 0), new TimeSpan(5, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(4, 15, 0), new TimeSpan(5, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            }




            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(5, 15, 0), new TimeSpan(6, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(5, 15, 0), new TimeSpan(6, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(5, 15, 0), new TimeSpan(6, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(5, 15, 0), new TimeSpan(6, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(5, 15, 0), new TimeSpan(6, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(5, 15, 0), new TimeSpan(6, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(5, 15, 0), new TimeSpan(6, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            }



            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(6, 15, 0), new TimeSpan(7, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(6, 15, 0), new TimeSpan(7, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(6, 15, 0), new TimeSpan(7, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(6, 15, 0), new TimeSpan(7, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(6, 15, 0), new TimeSpan(7, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(6, 15, 0), new TimeSpan(7, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(6, 15, 0), new TimeSpan(7, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            }



        }

        internal void CreateCPBookingSchedule_Standard(Guid TeamID, Guid BookingTypeId, Guid ProviderID, Guid SystemUserEmploymentContractId, Guid SystemUserId, bool InsertOnSunday = true)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(9, 15, 0), new TimeSpan(10, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            }




            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(10, 15, 0), new TimeSpan(11, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            }



            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(11, 15, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(11, 15, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(11, 15, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(11, 15, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(11, 15, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(11, 15, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(11, 15, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            }



        }

        internal void CreateCPBookingSchedule_Afternoon(Guid TeamID, Guid BookingTypeId, Guid ProviderID, Guid SystemUserEmploymentContractId, Guid SystemUserId, bool InsertOnSunday = true)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(14, 15, 0), new TimeSpan(19, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(14, 15, 0), new TimeSpan(19, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(14, 15, 0), new TimeSpan(19, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(14, 15, 0), new TimeSpan(19, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(14, 15, 0), new TimeSpan(19, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(14, 15, 0), new TimeSpan(19, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(14, 15, 0), new TimeSpan(19, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            }

        }

        internal void CreateCPBookingSchedule_OverTime_Night(Guid TeamID, Guid BookingTypeId, Guid ProviderID, Guid SystemUserEmploymentContractId, Guid SystemUserId, bool InsertOnSunday = true)
        {
            //var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(19, 15, 0), new TimeSpan(20, 0, 0), ProviderID, "Express Book Testing");
            //dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            //cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(19, 15, 0), new TimeSpan(20, 0, 0), ProviderID, "Express Book Testing");
            //dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            //cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(19, 15, 0), new TimeSpan(20, 0, 0), ProviderID, "Express Book Testing");
            //dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            //cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(19, 15, 0), new TimeSpan(20, 0, 0), ProviderID, "Express Book Testing");
            //dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            //cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(19, 15, 0), new TimeSpan(20, 0, 0), ProviderID, "Express Book Testing");
            //dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            //cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(19, 15, 0), new TimeSpan(20, 0, 0), ProviderID, "Express Book Testing");
            //dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            //if (InsertOnSunday == true)
            //{
            //    cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(19, 15, 0), new TimeSpan(20, 0, 0), ProviderID, "Express Book Testing");
            //    dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            //}




            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 2, new TimeSpan(20, 15, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 3, new TimeSpan(20, 15, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 4, new TimeSpan(20, 15, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 5, new TimeSpan(20, 15, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 6, new TimeSpan(20, 15, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 7, new TimeSpan(20, 15, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 1, new TimeSpan(20, 15, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);
            }



        }


        internal void CreateCPBookingSchedule_AnnualLeave(Guid TeamID, Guid BookingTypeId, Guid ProviderID, Guid SystemUserEmploymentContractId, Guid SystemUserId)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 1, new TimeSpan(0, 0, 0), new TimeSpan(0, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, SystemUserEmploymentContractId, SystemUserId);

        }

    }

}








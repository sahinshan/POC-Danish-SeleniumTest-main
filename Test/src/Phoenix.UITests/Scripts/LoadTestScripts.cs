
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.UITests.SmokeTests
{
    [TestClass]
    public class LoadTestScripts : FunctionalTest
    {
        #region Internal Properties

        internal List<string> firstNames = new List<string> { "James", "Robert", "John", "Michael", "William", "David", "Richard", "Joseph", "Thomas", "Charles", "Christopher", "Daniel", "Matthew", "Anthony", "Mark", "Donald", "Steven", "Paul", "Andrew", "Joshua", "Kenneth", "Kevin", "Brian", "George", "Edward", "Ronald", "Timothy", "Jason", "Jeffrey", "Ryan", "Jacob", "Gary", "Nicholas", "Eric", "Jonathan", "Stephen", "Larry", "Justin", "Scott", "Brandon", "Benjamin", "Samuel", "Gregory", "Frank", "Alexander", "Raymond", "Patrick", "Jack", "Dennis", "Jerry", "Tyler", "Aaron", "Jose", "Adam", "Henry", "Nathan", "Douglas", "Zachary", "Peter", "Kyle", "Walter", "Ethan", "Jeremy", "Harold", "Keith", "Christian", "Roger", "Noah", "Gerald", "Carl", "Terry", "Sean", "Austin", "Arthur", "Lawrence", "Jesse", "Dylan", "Bryan", "Joe", "Jordan", "Billy", "Bruce", "Albert", "Willie", "Gabriel", "Logan", "Alan", "Juan", "Wayne", "Roy", "Ralph", "Randy", "Eugene", "Vincent", "Russell", "Elijah", "Louis", "Bobby", "Philip", "Johnny" };
        internal List<string> lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzales", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez", "Phillips", "Evans", "Turner", "Diaz", "Parker", "Cruz", "Edwards", "Collins", "Reyes", "Stewart", "Morris", "Morales", "Murphy", "Cook", "Rogers", "Gutierrez", "Ortiz", "Morgan", "Cooper", "Peterson", "Bailey", "Reed", "Kelly", "Howard", "Ramos", "Kim", "Cox", "Ward", "Richardson", "Watson", "Brooks", "Chavez", "Wood", "James", "Bennet", "Gray", "Mendoza", "Ruiz", "Hughes", "Price", "Alvarez", "Castillo", "Sanders", "Patel", "Myers", "Long", "Ross", "Foster", "Jimenez" };

        internal Guid _productLanguageId;

        internal Guid defaultTeamId;
        internal Guid _authenticationproviderid;
        internal Guid _defaultUserId;

        internal Guid _bookingType2;
        internal Guid _bookingType3;
        internal Guid _bookingType5;

        internal Guid _employmentContractTypeid1;
        internal Guid _employmentContractTypeid2;
        internal Guid _employmentContractTypeid3;
        internal Guid _employmentContractTypeid4;


        internal Guid _careProviderStaffRoleTypeid;

        internal Guid _recurrencePattern_Every1WeekMondayId;
        internal Guid _recurrencePattern_Every1WeekTuesdayId;
        internal Guid _recurrencePattern_Every1WeekWednesdayId;
        internal Guid _recurrencePattern_Every1WeekThursdayId;
        internal Guid _recurrencePattern_Every1WeekFridayId;
        internal Guid _recurrencePattern_Every1WeekSaturdayId;
        internal Guid _recurrencePattern_Every1WeekSundayId;


        internal Guid _availabilityTypes_StandardId;
        internal Guid _availabilityTypes_OverTimeId;


        internal Guid _careProviderRateUnitId;
        internal Guid _careProviderVATCodeId;
        internal Guid _careProviderBatchGroupingId;

        internal Guid _ethnicityId;

        internal List<Guid> securityProfiles;

        #endregion

        [TestInitialize()]
        public void LoadScript_Setup()
        {
            #region Default User

            _defaultUserId = dbHelper.systemUser.GetSystemUserByUserName("administrator").FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultUserId, DateTime.Now.Date);

            #endregion

            #region Authentication Provider

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").First();

            #endregion

            #region Language

            _productLanguageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion

            #region Default Business Unit

            var defaultBusinessUnitId = commonMethodsDB.CreateBusinessUnit("Care Providers Default Business Unit");

            #endregion

            #region Default Team

            defaultTeamId = commonMethodsDB.CreateTeam("Performance Testing Tenant (Care Cloud)", _defaultUserId, defaultBusinessUnitId, "", "cpdt@oneadvancedemail.com", "", "");

            #endregion


            #region Booking Type 2 -> "Booking (to internal care activity)" & "Count full booking length"

            _bookingType2 = commonMethodsDB.CreateCPBookingType("BTC 2", 2, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type 3 -> "Booking (to external care activity)" & "Count full booking length"

            _bookingType3 = commonMethodsDB.CreateCPBookingType("BTC 3", 3, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion

            #region Booking Type 5 -> "Booking (to location)" & "Cap Booking length"

            _bookingType5 = commonMethodsDB.CreateCPBookingType("BTC 5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, null, null, null, 1);

            #endregion


            #region Care Provider Staff Role Type

            _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(defaultTeamId, "CPSRT 1", "9999", null, new DateTime(2020, 1, 1), null);

            #endregion


            #region Employment Contract Type - Salaried

            _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(defaultTeamId, "Salaried", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Employment Contract Type - Hourly

            _employmentContractTypeid2 = commonMethodsDB.CreateEmploymentContractType(defaultTeamId, "Hourly", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Employment Contract Type - Volunteer

            _employmentContractTypeid3 = commonMethodsDB.CreateEmploymentContractType(defaultTeamId, "Volunteer", "", null, new DateTime(2020, 1, 1));

            #endregion

            #region Employment Contract Type - Contracted

            _employmentContractTypeid4 = commonMethodsDB.CreateEmploymentContractType(defaultTeamId, "Contracted", "", null, new DateTime(2020, 1, 1));

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

            _availabilityTypes_StandardId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Salaried/Contracted").First();
            _availabilityTypes_OverTimeId = dbHelper.availabilityTypes.GetAvailabilityTypeByName("Hourly/Overtime").First();

            #endregion


            #region Care Provider Rate Unit

            _careProviderRateUnitId = commonMethodsDB.CreateCareProviderRateUnit(defaultTeamId, "Default Care Provider Rate Unit", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region VAT Code

            _careProviderVATCodeId = dbHelper.careProviderVatCode.GetByName("Standard Rated")[0];

            #endregion

            #region Care Provider Batch Grouping

            _careProviderBatchGroupingId = commonMethodsDB.CreateCareProviderBatchGrouping(defaultTeamId, "Default Care Provider Batch Grouping", new DateTime(2020, 1, 1), 9999999);

            #endregion

            #region Ethnicity

            _ethnicityId = commonMethodsDB.CreateEthnicity(defaultTeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Security Profiles

            securityProfiles = new List<Guid>();
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Express Book - Edit All Records"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("CP Bed Management (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("CP Bed Management Setup (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Create and Maintain End Reason Rules"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Create and Maintain Person Absence Rules"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Create and Maintain Providers"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Create Person Absences"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Create Person Contract and Person Contract Services"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Activities (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Alert/Hazard Module (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Applicant - Secure Fields (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Assign Records"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can Access Customizations"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can Edit Time-Critical Booking"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Dashboards And Views"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can Manage Reference Data"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can View Contact"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can View Core Users"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can View People We Support"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can View Prospect"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can View Provider Users"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can View Referral"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Can View Rostered Users"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Planning (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Availability Type View Only"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Dairy Booking - Edit"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Employee Open Ended Absence - View"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Recruitment Setup"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Org View)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Schedule Booking - Edit"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Scheduling Preference (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Scheduling Runs (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Scheduling Runs (View)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Scheduling Setup (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Settings Edit"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Staff Annual Leave (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Staff Availability (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Staff Demographics (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Staff Recruitment (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Staff Scheduling (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Delete Booking Diary"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Delete Booking Schedules"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Staff Training (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Training Setup"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Transport Type (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Worker Contract (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Carers (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Clone Activities"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Org View)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Dashboards and Charts Administrator"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Error Correction (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Export to Excel"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Finance Area Admin"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Forms (BU View)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Forms Setup (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Fully Accept Recruitment Application"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Mail Merge (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Manage Health Setup Data"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Observation And Monitoring (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Organisational Risk (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Organisational Risk Setup Data (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Pathway (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person - Secure Fields (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person Complaints Module (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person Confidential Address (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person Contracts (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person Forms (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person Primary Support Reason (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Provider (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Qualified to Authorise Care Plans"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Scheduled Bookings - View and Create"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Set Override Recruitment Document Attribute"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Social Care Person Profile"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Staff Reviews (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Subject Access Request (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("System Management Access"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("System User (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("System User Employment Contract (Field Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("System User Open Ended Absence (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Team Membership (Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Teams (BU View)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Timeline Record (BU View)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("User Dashboards"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("User Diaries (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("User Work Schedules (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("UserDataView"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Workflow (BU Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Workflow Job (View)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Maintain Contract Services and Person Contract Services"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person Prospect (View/Edit)"));
            securityProfiles.AddRange(dbHelper.securityProfile.GetSecurityProfileByName("Person Tracking"));


            #endregion
        }

        [Description("Method that can be used to load finance related data into a target tenant")]
        [TestMethod]
        public void LoadTestScript_InsertTestData_Workflow1()
        {
            var rnd = new Random();
            var totalFirstNames = firstNames.Count;
            var totalLastNames = lastNames.Count;

            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            var numberOfBusinessUnits = 10;
            var numberOfTeamsPerBU = 1;
            var numberOfProvidersPerTeam = 1;
            var numberOfSystemUsersPerProvider = 5;
            var numberOfPeoplePerProvider = 5;

            for (int bu = 1; bu <= numberOfBusinessUnits; bu++)
            {
                var currentTimeString = commonMethodsHelper.GetCurrentDateTimeString(true);

                #region Business Unit

                var businessUnitName = "BU " + currentTimeString;
                var businessUnitId = commonMethodsDB.CreateBusinessUnit(businessUnitName);

                #endregion

                for (int team = 1; team <= numberOfTeamsPerBU; team++)
                {
                    currentTimeString = commonMethodsHelper.GetCurrentDateTimeString(true);

                    #region Team

                    var teamName = " Team " + currentTimeString;
                    var teamEmail = "Team" + currentTimeString + "@oneadvancedmail.com";
                    var teamId = commonMethodsDB.CreateTeam(teamName, _defaultUserId, businessUnitId, "", teamEmail, teamName, "");

                    #endregion

                    for (int provider = 1; provider <= numberOfProvidersPerTeam; provider++)
                    {
                        currentTimeString = commonMethodsHelper.GetCurrentDateTimeString(true);

                        #region Provider

                        var providerName = "Provider " + currentTimeString;
                        var addressType = 10; //Home
                        var providerId = commonMethodsDB.CreateProvider(providerName, teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

                        var funderProviderName = "Funder Provider " + currentTimeString;
                        var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

                        #endregion

                        #region Provider Allowable Booking Types

                        commonMethodsDB.CreateProviderAllowableBookingTypes(teamId, providerId, _bookingType2, false);
                        commonMethodsDB.CreateProviderAllowableBookingTypes(teamId, providerId, _bookingType3, false);
                        commonMethodsDB.CreateProviderAllowableBookingTypes(teamId, providerId, _bookingType5, false);

                        #endregion

                        #region Care Provider Contract Scheme

                        var contractScheme1Name = "CPCS_" + currentTimeString;
                        var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
                        var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(teamId, _defaultUserId, businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

                        #endregion

                        #region Care Provider Service

                        var careProviderServiceName = "CPS A " + currentTimeString;
                        var careProviderServiceCode = dbHelper.careProviderService.GetHighestCode() + 1;
                        var careProviderService1Id = commonMethodsDB.CreateCareProviderService(teamId, careProviderServiceName, new DateTime(2020, 1, 1), careProviderServiceCode, null, true);

                        #endregion

                        #region Care Provider Service Mapping

                        var careProviderServiceMapping1Id = commonMethodsDB.CreateCareProviderServiceMapping(careProviderService1Id, teamId, null, _bookingType2, null, "");

                        #endregion

                        #region Care Provider Extract Name

                        var careProviderExtractName = "CPEN " + currentTimeString;
                        var careProviderExtractNameCode = dbHelper.careProviderService.GetHighestCode() + 1;
                        var careProviderExtractNameId = commonMethodsDB.CreateCareProviderExtractName(teamId, careProviderExtractName, careProviderExtractNameCode, null, new DateTime(2023, 1, 1), null, false, false);

                        #endregion

                        #region Finance Invoice Batch Setup

                        var invoicebyid = 1; //funder
                        var careproviderinvoicefrequencyid = 1; //Every Week
                        var createbatchwithin = 1;
                        var chargetodayid = 1; //Monday
                        var whentobatchfinancetransactionsid = 3; //Does Not Matter
                        var useenddatewhenbatchingfinancetransactions = true;
                        var financetransactionsupto = todayDate.AddYears(1);
                        var separateinvoices = false;

                        dbHelper.careProviderFinanceInvoiceBatchSetup.CreateCareProviderFinanceInvoiceBatchSetup(false,
                            careProviderContractScheme1Id, _careProviderBatchGroupingId,
                            new DateTime(2023, 1, 1), new TimeSpan(0, 0, 0),
                            invoicebyid, careproviderinvoicefrequencyid, createbatchwithin, chargetodayid, whentobatchfinancetransactionsid, useenddatewhenbatchingfinancetransactions, financetransactionsupto, separateinvoices,
                            careProviderExtractNameId, true,
                            teamId);

                        #endregion

                        #region Care Provider Contract Service

                        var careProviderContractServiceId = dbHelper.careProviderContractService.CreateCareProviderContractService(teamId, _defaultUserId, businessUnitId, "", providerId, funderProviderID, careProviderContractScheme1Id, careProviderService1Id, null, _bookingType2, _careProviderVATCodeId, _careProviderBatchGroupingId, _careProviderRateUnitId, 1, 1, false, false);

                        #endregion

                        #region Contract Service Rate Period

                        dbHelper.careProviderContractServiceRatePeriod.CreateCareProviderContractServiceRatePeriod(careProviderContractServiceId, new DateTime(2023, 1, 1), _careProviderRateUnitId, 15, teamId);

                        #endregion

                        var systemUserData = new List<SystemUser_EmploymentContract_Data>();
                        for (int systemU = 0; systemU < numberOfSystemUsersPerProvider; systemU++)
                        {
                            currentTimeString = commonMethodsHelper.GetCurrentDateTimeString(true);

                            #region System User

                            //set the user as roostered user and use a persona to link it to the user
                            var username = "cpsu_" + currentTimeString;
                            var userFirstName = "Care Provider";
                            var userLastName = "System User " + currentTimeString;
                            var systemUserId = commonMethodsDB.CreateSystemUserRecord(username, userFirstName, userLastName, "Passw0rd_!", businessUnitId, teamId, _productLanguageId, _authenticationproviderid, securityProfiles, 3);

                            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(systemUserId, GetThisWeekFirstMonday());

                            #endregion

                            #region Team Member

                            dbHelper.teamMember.CreateTeamMember(defaultTeamId, systemUserId, new DateTime(2023, 1, 1), null);

                            #endregion

                            #region System User Employment Contract

                            var _systemUserEmploymentContractId = commonMethodsDB.CreateSystemUserEmploymentContract(systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, teamId, _employmentContractTypeid1, 47);

                            #endregion

                            #region System User Employment Contract CP Booking Type

                            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType2);
                            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType3);
                            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

                            #endregion

                            #region User Work Schedule

                            //Create the user work schedule for all days of the week
                            //3 schedules are created per day: from 00:00:00 to 09:00:00 | from 09:00:00 to 18:00:00 | from 18:00:00 to 00:00:00)
                            CreateUserWorkSchedule(systemUserId, teamId, _systemUserEmploymentContractId);

                            #endregion

                            systemUserData.Add(new SystemUser_EmploymentContract_Data(systemUserId, _systemUserEmploymentContractId));

                        }

                        var personData = new List<Person_PersonContracts_Data>();
                        for (int person = 0; person < numberOfPeoplePerProvider; person++)
                        {
                            #region Person

                            var firstName = firstNames[rnd.Next(totalFirstNames)];
                            var lastName = lastNames[rnd.Next(totalLastNames)];
                            var _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1), _ethnicityId, teamId, 1, 1);

                            #endregion

                            #region Person Contract

                            var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(teamId, "title", _personID, systemUserData[0].SystemUserId, providerId, careProviderContractScheme1Id, funderProviderID, todayDate.AddDays(-5), null, true);

                            #endregion

                            #region Person Contract Service

                            dbHelper.careProviderPersonContractService.CreateCareProviderPersonContractService(_personcontractId, teamId, careProviderContractScheme1Id, careProviderService1Id, careProviderContractServiceId, todayDate, 1, 1, _careProviderRateUnitId);

                            #endregion

                            personData.Add(new Person_PersonContracts_Data(_personID, _personcontractId));
                        }

                        for (int p = 0; p < systemUserData.Count; p++)
                        {
                            //Create the provider schedule records for the user
                            CreateCPBookingSchedule_Min(teamId, _bookingType2, providerId, systemUserData[p], personData[p], careProviderContractServiceId);
                            CreateCPBookingSchedule_Standard(teamId, _bookingType2, providerId, systemUserData[p], personData[p], careProviderContractServiceId);
                            CreateCPBookingSchedule_OverTime_Night(teamId, _bookingType2, providerId, systemUserData[p], personData[p], careProviderContractServiceId);
                            CreateCPBookingSchedule_Afternoon(teamId, _bookingType2, providerId, systemUserData[p], personData[p], careProviderContractServiceId);
                        }


                    }
                }
            }
        }

        #region Internal Methods

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

        internal void CreateCPBookingSchedule_Min(Guid TeamID, Guid BookingTypeId, Guid ProviderID, SystemUser_EmploymentContract_Data systemUserData, Person_PersonContracts_Data personData, Guid careProviderContractServiceId, bool InsertOnSunday = true)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(7, 0, 0), new TimeSpan(7, 15, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(7, 0, 0), new TimeSpan(7, 15, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(7, 0, 0), new TimeSpan(7, 15, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(7, 0, 0), new TimeSpan(7, 15, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(7, 0, 0), new TimeSpan(7, 15, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(7, 0, 0), new TimeSpan(7, 15, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(7, 0, 0), new TimeSpan(7, 15, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
                dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);
            }
        }

        internal void CreateCPBookingSchedule_Standard(Guid TeamID, Guid BookingTypeId, Guid ProviderID, SystemUser_EmploymentContract_Data systemUserData, Person_PersonContracts_Data personData, Guid careProviderContractServiceId, bool InsertOnSunday = true)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(9, 0, 0), new TimeSpan(12, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
                dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            }
        }

        internal void CreateCPBookingSchedule_Afternoon(Guid TeamID, Guid BookingTypeId, Guid ProviderID, SystemUser_EmploymentContract_Data systemUserData, Person_PersonContracts_Data personData, Guid careProviderContractServiceId, bool InsertOnSunday = true)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 1, new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 2, new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 3, new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 4, new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 5, new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 6, new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 7, new TimeSpan(14, 0, 0), new TimeSpan(18, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
                dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);
            }

        }

        internal void CreateCPBookingSchedule_OverTime_Night(Guid TeamID, Guid BookingTypeId, Guid ProviderID, SystemUser_EmploymentContract_Data systemUserData, Person_PersonContracts_Data personData, Guid careProviderContractServiceId, bool InsertOnSunday = true)
        {
            var cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 1, 2, new TimeSpan(20, 0, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 2, 3, new TimeSpan(20, 0, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 3, 4, new TimeSpan(20, 0, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 4, 5, new TimeSpan(20, 0, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 5, 6, new TimeSpan(20, 0, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 6, 7, new TimeSpan(20, 0, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
            dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
            dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);

            if (InsertOnSunday == true)
            {
                cpBookingScheduleId = dbHelper.cpBookingSchedule.CreateCPBookingSchedule(TeamID, BookingTypeId, 1, 7, 1, new TimeSpan(20, 0, 0), new TimeSpan(4, 0, 0), ProviderID, "Express Book Testing");
                dbHelper.cpBookingScheduleStaff.CreateCPBookingScheduleStaff(TeamID, cpBookingScheduleId, systemUserData.SystemUserEmploymentContractId, systemUserData.SystemUserId);
                dbHelper.scheduleBookingToPeople.CreateScheduleBookingToPeople(TeamID, cpBookingScheduleId, personData.PersonId, personData.PersonContractId, careProviderContractServiceId);
            }

        }

        #endregion
    }

    internal class SystemUser_EmploymentContract_Data
    {
        public SystemUser_EmploymentContract_Data(Guid UserId, Guid ContractId)
        {
            this.SystemUserId = UserId;
            this.SystemUserEmploymentContractId = ContractId;
        }

        public Guid SystemUserId { get; set; }

        public Guid SystemUserEmploymentContractId { get; set; }
    }

    internal class Person_PersonContracts_Data
    {
        public Person_PersonContracts_Data(Guid personId, Guid personContractId)
        {
            this.PersonId = personId;
            this.PersonContractId = personContractId;
        }

        public Guid PersonId { get; set; }

        public Guid PersonContractId { get; set; }
    }

}








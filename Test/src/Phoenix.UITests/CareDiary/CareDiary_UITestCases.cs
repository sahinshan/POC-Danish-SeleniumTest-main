using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.CareDiary
{

    /// <summary>
    /// This class contains Automated UI test scripts for Care Diary
    /// </summary>
    [TestClass]
    public class CareDiary_UITestCases : FunctionalTest
    {
        #region Properties
        private Guid _languageId;
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _defaultUserId;
        private Guid _systemUserId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _carePlanType;
        private Guid _personCarePlanID;
        private Guid _personcareplanregularcaskid;
        private Guid _personcareplanregularcaskscheduleid;
        private Guid _careTaskid;
        private Guid _careTaskid1;
        private Guid _personcareplanregularcaskid_inactive;
        private string _systemUsername;
        private Guid _personCarePlanFormID;
        private Guid _bookingType5;
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
        private string _adminSystemUsername;
        private Guid _adminSystemUserId;
        private Guid cpPersonAbsenceReasonId;
        private Guid cpPersonAbsenceReasonId1;
        private Guid cpPersonAbsenceReasonId2;

        private Guid _bookingType6;

        #endregion

        [TestInitialize()]
        public void Person_CarePlan_SetupTest()
        {

            try
            {
                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                var _defaultSystemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];
                var _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);

                #endregion

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
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Advanced Search")[0]);



                #endregion

                #region Create SystemUser 

                _systemUsername = "TestUser6";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Test", "User6", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);

                #endregion

                #region Booking Type 5 -> "Booking (to service user)" 

                _bookingType5 = commonMethodsDB.CreateBookingType("BookingType-5", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);
                _bookingType6 = commonMethodsDB.CreateCPBookingType("BookingType-666", 6, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, true, 1440);

                #endregion

                #region update Care Provider scheduling set up
                var cPSchedulingSetupId = dbHelper.cPSchedulingSetup.GetAllActiveRecords().FirstOrDefault();
                dbHelper.cPSchedulingSetup.UpdateCPScheduleSetup(cPSchedulingSetupId, _bookingType6);
                dbHelper.cPSchedulingSetup.UpdateDefaultBookingStaffRoleType(cPSchedulingSetupId, true);
                #endregion

                #region Provider 1

                _providereId1 = commonMethodsDB.CreateProvider("Provider-001" + commonMethodsHelper.GetCurrentDateWithoutCulture().ToString("ddmmyyyyhhmmss"), _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);
                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType6, false);

                #endregion

                #region Person

                var firstName = "Person_CareDiary" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "ACC-2204";
                var addresstypeid = 6; //Home
                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();

                _personID = commonMethodsDB.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
                "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];


                _personFullName = "Person_CareDiary" + DateTime.Now.ToString("yyyyMMddHHmmss");

                #endregion

                #region create contract scheme

                var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
                _contractschemeid = commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), contractSchemeCode, _providereId1, _providereId1);

                #endregion

                #region create person contract

                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, new DateTime(2023, 1, 1, 7, 30, 0));
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

                #region care Tasks
                _careTaskid = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with bath or shower1", 001, DateTime.Now);

                _careTaskid1 = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with dressing1", 002, DateTime.Now);

                #endregion

                #region Person Absence Reason

                cpPersonAbsenceReasonId = commonMethodsDB.CreateCPPersonAbsenceReason(_careDirectorQA_TeamId, "Hospital_0", new DateTime(2020, 1, 1), 123);
                cpPersonAbsenceReasonId1 = commonMethodsDB.CreateCPPersonAbsenceReason(_careDirectorQA_TeamId, "Hospital_1", new DateTime(2020, 1, 1), 123);
                cpPersonAbsenceReasonId2 = commonMethodsDB.CreateCPPersonAbsenceReason(_careDirectorQA_TeamId, "Hospital_2", new DateTime(2020, 1, 1), 123);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-2204

        [TestProperty("JiraIssueID", "ACC-6531")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6531 - " +
            "As a care coordinator Verify resident’s Care Diary to update when they are absent from the home.So that outstanding tasks are not shown for a resident that is not there" +
            "PreCondition:People record should exist.Person Absence Created with only Actual/Expected Start Date (I.E.open ended Absence)For Example Date=Todaydate+1day.User should have Regular care record with care scheduled.Step 1-14 of the original Steps.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod01()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            #region Person Care Plan Need Domain

            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();

            #endregion

            #region Careplan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate, new TimeSpan(0, 0, 0), 2, 1, null);

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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");

            #region Person Absence created Todaydate+1day without enddate
            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(1), null, personContractIds, cpPersonAbsenceReasonId, todayDate);
            #endregion

            System.Threading.Thread.Sleep(1000);

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, true)
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home");

        }

        [TestProperty("JiraIssueID", "ACC-6532")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6532 - " +
           "As a care coordinator Verify resident’s Care Diary to update when they are absent from the home.So that outstanding tasks are not shown for a resident that is not there" +
           "PreCondition:People record should exist.Person Absence Created with only Actual/Expected Start Date (I.E.open ended Absence)For Example Date=Todaydate+1day.User should have Regular care record with care scheduled.Step 15 of the original Steps.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod02()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            #region Person Care Plan Need Domain
            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();
            #endregion

            #region Careplan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task
            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate, new TimeSpan(0, 0, 0), 2, 1, null);

            #endregion

            #region Person Absence created Todaydate+1day
            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(1), null, personContractIds, cpPersonAbsenceReasonId, todayDate);
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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ClickCareDiaryRecord(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"));

            CareDiaryRecordPage
               .WaitForPersonCarePlansSubPage_CareDiaryRecordPageToLoad()
               .CareDiaryRecordPage_ValidateLinkedRecordFld("");

        }

        [TestProperty("JiraIssueID", "ACC-6549")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6549 - " +
          "As a care coordinator Verify resident’s Care Diary to update when they are absent from the home.So that outstanding tasks are not shown for a resident that is not there" +
          "PreCondition:People record should exist.Person Absence Created with only Actual/Expected Start Date (I.E.open ended Absence)For Example Date=Todaydate+1day.User should have Regular care record with care scheduled.Step 15 of the original Steps.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod03()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Person Care Plan Need Domain
            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();
            #endregion

            #region Person Absence created 
            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(1), todayDate.AddDays(-2), personContractIds, cpPersonAbsenceReasonId, todayDate);
            #endregion

            #region Careplan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate.AddDays(-3), new TimeSpan(0, 0, 0), 2, 1, null);

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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();


            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home");

        }

        [TestProperty("JiraIssueID", "ACC-6551")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6551 - " +
         "As a care coordinator Verify resident’s Care Diary to update when they are absent from the home.So that outstanding tasks are not shown for a resident that is not there" +
         "PreCondition:People record should exist.Person Absence Created with only Actual/Expected Start Date (I.E.open ended Absence)For Example Date=Todaydate+1day.User should have Regular care record with care scheduled.Step 1-9 of the original Steps.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod04()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            #region Person Care Plan Need Domain

            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();

            #endregion

            #region Person Absence created Todaydate+1day
            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(-1), null, null, todayDate.AddDays(2), personContractIds, cpPersonAbsenceReasonId, todayDate);
            #endregion

            #region Careplan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate.AddDays(-1), new TimeSpan(0, 0, 0), 2, 1, null);

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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");

        }
        #endregion

        [TestProperty("JiraIssueID", "ACC-6556")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6556 - " +
         "As a care coordinator Verify resident’s Care Diary to update when they are absent from the home.So that outstanding tasks are not shown for a resident that is not there" +
         "PreCondition:People record should exist.Person Absence Created with only Actual/Expected Start Date (I.E.open ended Absence)For Example Date=Todaydate+1day.User should have Regular care record with care scheduled.Step 10-12 of the original Steps.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod05()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            #region Person Care Plan Need Domain
            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();
            #endregion

            #region Person Absence created Todaydate+1day
            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(-3), null, null, todayDate.AddDays(4), personContractIds, cpPersonAbsenceReasonId, todayDate);
            #endregion

            #region Careplan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate.AddDays(-3), new TimeSpan(0, 0, 0), 2, 1, null);

            #endregion

            #region Steps 10,11,12
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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();
            System.Threading.Thread.Sleep(2000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");

            #region update planned end date to less than prious date entered

            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();
            dbHelper.cpPersonAbsence.UpdatedePlannedenddateNTime(_cppersonabsencid, todayDate.AddDays(2), personContractIds);

            #endregion

            #region update scheduled start date to less than person absence start date

            var regularcarescheduleid = dbHelper.cpRegularCareTaskSchedule.GetByCPRegularCareTaskId(_personcareplanregularcaskid).FirstOrDefault();

            dbHelper.cpRegularCareTaskSchedule.UpdateStartDate(regularcarescheduleid, DateTime.Now.Date.AddDays(-4).AddHours(0).AddMinutes(0).AddSeconds(0));

            #endregion

            System.Threading.Thread.Sleep(2000);

            personRecordPage
              .WaitForPersonRecordPageToLoad(false, false, false, true)
              .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");

        }
        #endregion

        [TestProperty("JiraIssueID", "ACC-6579")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6579 - " +
         "Verify when Person Absence start date is moved later :Status on past records should be “Away from Home”.Step 1-10 of the original Steps.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod06()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            #region Person Care Plan Need Domain
            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();
            #endregion

            #region Person Absence created Todaydate+1day
            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(-3), null, null, todayDate.AddDays(4), personContractIds, cpPersonAbsenceReasonId, todayDate);
            #endregion

            #region Careplan
            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, todayDate.AddDays(-2), todayDate.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task
            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care
            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate.AddDays(-3), new TimeSpan(0, 0, 0), 2, 1, null);
            #endregion

            #region Steps 1-10
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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();
            System.Threading.Thread.Sleep(2000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");

            #region update planned end date to less than prious date entered

            var _cppersonabsencid = dbHelper.cpPersonAbsence.GetByPersonId(_personID).FirstOrDefault();
            dbHelper.cpPersonAbsence.UpdatedePlannedstartdateNTime(_cppersonabsencid, todayDate.AddDays(1).AddHours(23).AddMinutes(59), personContractIds);

            #endregion

            #region update scheduled start date to less than person absence start date

            var regularcarescheduleid = dbHelper.cpRegularCareTaskSchedule.GetByCPRegularCareTaskId(_personcareplanregularcaskid).FirstOrDefault();

            dbHelper.cpRegularCareTaskSchedule.UpdateStartDate(regularcarescheduleid, todayDate.Date.AddDays(-4));

            #endregion

            System.Threading.Thread.Sleep(2000);

            personRecordPage
              .WaitForPersonRecordPageToLoad(false, false, false, true)
              .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            System.Threading.Thread.Sleep(2000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");

        }
        #endregion

        [TestProperty("JiraIssueID", "ACC-6583")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6583 - " +
         "Verify when Care Schedule is ended ,remove any Care Diary records with start date in the future AND Status is “Future” OR “Away from Home”.Step 1-10 of the original Steps.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod07()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            #region Person Care Plan Need Domain
            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();
            #endregion

            #region Person Absence created Todaydate+1day
            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(-3), null, null, todayDate.AddDays(4), personContractIds, cpPersonAbsenceReasonId, todayDate);
            #endregion

            #region Care Plan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate.AddDays(-3), new TimeSpan(0, 0, 0), 2, 1, null);

            #endregion

            #region Step 1-8
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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();
            System.Threading.Thread.Sleep(2000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");


            #region update care schedule record =with the enddate

            var regularcarescheduleid = dbHelper.cpRegularCareTaskSchedule.GetByCPRegularCareTaskId(_personcareplanregularcaskid).FirstOrDefault();

            dbHelper.cpRegularCareTaskSchedule.UpdateEndDate(regularcarescheduleid, DateTime.Now.Date.AddDays(3).AddHours(0).AddMinutes(0).AddSeconds(0));

            #endregion

            System.Threading.Thread.Sleep(2000);

            personRecordPage
              .WaitForPersonRecordPageToLoad(false, false, false, true)
              .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            System.Threading.Thread.Sleep(2000);
            var CountOfCareDiary = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID);
            var CareDiaryCount = CountOfCareDiary.Count();
            System.Threading.Thread.Sleep(1000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .ValidateCareDiaryRecordCount("1 - " + CareDiaryCount.ToString())
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home");


        }

        //[TestProperty("JiraIssueID", "ACC-6593")]
        //[Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6593 - " +
        //"Verify when two absences exist at the same time, Care Diary status should consider earliest start date/time AND last end date/time..")]
        //[TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        //[TestMethod]
        //[TestProperty("BusinessModule1", "Care Provider Care Plan")]
        //[TestProperty("Screen1", "Regular Care")]
        //public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod08()
        //{
        //    var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
        //    #region Person Care Plan Need Domain
        //    Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();
        //    #endregion

        //    #region Person Absence created Todaydate+1day
        //    var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
        //    var cpPersonAbsenceId_1 = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(-2), todayDate.AddDays(-3), todayDate, todayDate.AddDays(-2), personContractIds, cpPersonAbsenceReasonId, todayDate);
        //    var cpPersonAbsenceId_2 = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, null, todayDate.AddDays(-3), null, null, personContractIds, cpPersonAbsenceReasonId1, todayDate);

        //    #endregion

        //    #region Careplan

        //    _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

        //    #endregion

        //    #region Regular Care Task

        //    _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

        //    #endregion

        //    #region Schedule Care

        //    _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate.AddDays(-2), new TimeSpan(0, 0, 0), 2, 1, null);
        //    #endregion

        //    #region Step 1-8
        //    loginPage
        //       .GoToLoginPage()
        //       .Login(_systemUsername, "Passw0rd_!");

        //    mainMenu
        //        .WaitForMainMenuToLoad()
        //        .NavigateToPeopleSection();

        //    peoplePage
        //        .WaitForPeoplePageToLoad()
        //        .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
        //        .OpenPersonRecord(_personID.ToString());

        //    personRecordPage
        //        .WaitForPersonRecordPageToLoad(false, false, false, true)
        //        .TapCarePlansTab();

        //    personCarePlansSubPage
        //        .WaitForPersonCarePlansSubPageToLoad()
        //        .ClickCareDiaryLink();
        //    System.Threading.Thread.Sleep(2000);

        //    CareDiaryPage
        //       .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
        //       .SelectRecordPicklist("Related Records")
        //       .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
        //       .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
        //       .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
        //       .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
        //       .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
        //       .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
        //       .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home");

        //    #endregion

        //}


        [TestProperty("JiraIssueID", "ACC-6595")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6595 - " +
        "Verify when two absences exist at the same time, Care Diary status should consider earliest start date/time AND last end date/time..")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod09()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();
            #region Person Care Plan Need Domain
            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();
            #endregion

            #region Person Absence created Todaydate+1day
            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId_1 = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(-2), todayDate.AddDays(-3), todayDate, todayDate.AddDays(-2), personContractIds, cpPersonAbsenceReasonId, todayDate);
            //var cpPersonAbsenceId_2 = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, null, todayDate.AddDays(-3), todayDate, null, personContractIds, cpPersonAbsenceReasonId1, todayDate);

            #endregion

            #region Careplan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate.AddDays(-2), new TimeSpan(0, 0, 0), 2, 1, null);

            #endregion

            #region Step 1-8
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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();
            System.Threading.Thread.Sleep(2000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-6596")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-6596 - " +
       "Verify when two absences exist at the same time, Care Diary status should consider earliest start date/time AND last end date/time..")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void PersonCarePlan_VerifyCareDiaryStatus_Testmethod010()
        {
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Person Care Plan Need Domain

            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();

            #endregion

            #region Person Absence created Todaydate+1day

            var personContractIds = new System.Collections.Generic.List<Guid> { _personcontractId };
            var cpPersonAbsenceId_1 = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(-4), todayDate.AddDays(-6), todayDate.AddDays(-1), todayDate.AddDays(-4), personContractIds, cpPersonAbsenceReasonId, todayDate);
            var cpPersonAbsenceId_2 = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, null, todayDate.AddDays(-7), todayDate.AddDays(-7), null, personContractIds, cpPersonAbsenceReasonId1, todayDate);
            var cpPersonAbsenceId_3 = dbHelper.cpPersonAbsence.CreateCPPersonAbsence(_careDirectorQA_TeamId, _personID, todayDate.AddDays(4), todayDate.AddMinutes(-1), null, todayDate.AddDays(5), personContractIds, cpPersonAbsenceReasonId2, todayDate);

            #endregion

            #region Careplan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, todayDate.AddDays(-2), todayDate.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

            #endregion

            #region Regular Care Task

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, todayDate.AddDays(-2), new TimeSpan(0, 0, 0), 2, 1, null);

            #endregion

            #region Step 1-8

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
                .TapCarePlansTab();

            personCarePlansSubPage
                .WaitForPersonCarePlansSubPageToLoad()
                .ClickCareDiaryLink();

            System.Threading.Thread.Sleep(2000);

            CareDiaryPage
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .SelectRecordPicklist("Related Records")
               .WaitForPersonCarePlansSubPage_CareDiaryPageToLoad()
               .ValidateCareDiaryStatus(todayDate.ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(1).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(2).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(3).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(4).ToString("dd'/'MM'/'yyyy"), "Away from Home", "Away from Home")
               .ValidateCareDiaryStatus(todayDate.AddDays(5).ToString("dd'/'MM'/'yyyy"), "Future", "Future")
               .ValidateCareDiaryStatus(todayDate.AddDays(6).ToString("dd'/'MM'/'yyyy"), "Future", "Future");

            #endregion

        }
        #endregion
    }
}

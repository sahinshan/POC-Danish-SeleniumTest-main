using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People.Related_Items
{
    [TestClass]
    public class PersonSpecificTrainings_UITestCases : FunctionalTest
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

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("PersonSpecificTraining BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("PersonSpecificTraining T1", null, _businessUnitId, "907678", "PersonSpecificTrainingT1@careworkstempmail.com", "PersonSpecificTraining T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create SystemUser Record

                _systemUserName = "PersonSpecificTrainingUser1";
                _systemUserFullName = "PersonSpecificTraining User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "PersonSpecificTraining", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

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


        #region https://advancedcsg.atlassian.net/browse/ACC-2337

        [TestProperty("JiraIssueID", "ACC-2025")]
        [Description("Step(s) 1 from the original test")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule1", "Care Provider Scheduling Preference")]
        [TestProperty("BusinessModule2", "Care Provider Staff Training")]
        [TestProperty("Screen1", "Person Specific Trainings")]
        [TestProperty("Screen2", "System User Training")]
        public void PersonSpecificTraining_UITestMethod01()
        {
            var currentDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Person

            var firstName = "Conor";
            var lastName = _currentDateSuffix;
            var _person_fullName = firstName + " " + lastName;
            var _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];

            #endregion

            #region Provider

            var providerName = "Residential Establishment " + _currentDateSuffix;
            var providerType = 13; //Residential Establishment
            var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, providerType, true);

            var provider2Name = "Residential Organisation2 " + _currentDateSuffix;
            var provider2Type = 13; //Master Organisation
            var provider2Id = commonMethodsDB.CreateProvider(provider2Name, _teamId, provider2Type, true);

            #endregion

            #region Care Provider Contract Scheme

            var contractSchemeName = "Default_" + _currentDateSuffix;
            var contractSchemeCode = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
            var careProviderContractSchemeId = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _systemUserId, _businessUnitId, contractSchemeName, new DateTime(2020, 1, 1), contractSchemeCode, providerId, provider2Id);

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
                _teamId, _systemUserId, _businessUnitId, "", providerId, provider2Id, careProviderContractSchemeId,
                careProviderServiceId, careProviderVATCodeId, careProviderBatchGroupingId, careProviderRateUnitId, 1, 1, false, false);

            #endregion

            #region Care Provider Person Contract

            var careProviderPersonContractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "", _personId, _systemUserId, providerId, careProviderContractSchemeId, provider2Id, new DateTime(2023, 6, 12), null, true);
            var careProviderPersonContractTitle = (string)(dbHelper.careProviderPersonContract.GetByID(careProviderPersonContractId, "title")["title"]);

            #endregion




            #region Booking Type

            int BookingTypeClassId = 4; //Booking (to internal non-care booking e.g. annual leave, training)
            var _bookingType1 = commonMethodsDB.CreateCPBookingType("BookingType-4", BookingTypeClassId, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 1, false, true, false, false, false, true);

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

            var _newSystemUserName = "PST_" + _currentDateSuffix;
            var _newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_newSystemUserName, "Person Specific Trainings", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_newSystemUserId, commonMethodsHelper.GetThisWeekFirstMonday());

            var _newSystemUser2Name = "PST2_" + _currentDateSuffix;
            var _newSystemUser2Id = commonMethodsDB.CreateSystemUserRecord(_newSystemUser2Name, "Person Specific Trainings2", "User " + _currentDateSuffix, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
            dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_newSystemUser2Id, commonMethodsHelper.GetThisWeekFirstMonday());

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_newSystemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 47);
            var _systemUserEmploymentContract2Id = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_newSystemUser2Id, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _teamId, _employmentContractTypeid, 47);

            #endregion

            #region System User Employment Contract CP Booking Type

            dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType1);

            #endregion

            #region User Work Schedule

            CreateUserWorkSchedule(_newSystemUserId, _teamId, _systemUserEmploymentContractId, _availabilityTypes_StandardId);
            CreateUserWorkSchedule(_newSystemUser2Id, _teamId, _systemUserEmploymentContract2Id, _availabilityTypes_StandardId);

            #endregion

            #region Staff Training Item

            var staffTrainingItemId = commonMethodsDB.CreateStaffTrainingItem(_teamId, "Default Staff Training Item", new DateTime(2020, 1, 1), 2);

            #endregion



            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            #endregion

            #region Step 3

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            #endregion

            #region Step 4

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonSpecificTrainingPage();

            #endregion

            #region Step 5

            personSpecificTrainingsPage
                .WaitForPersonSpecificTrainingsPageToLoad()
                .ClickNewRecordButton();

            #endregion

            #region Step 6

            personSpecificTrainingRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickPersonContractLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectResultElement(careProviderPersonContractId);

            personSpecificTrainingRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .ClickTrainingItemLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("Default Staff Training Item", staffTrainingItemId);

            personSpecificTrainingRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .SelectTrainingType("Person Specific")
                .InsertTextOnTrainingCourse("TC 1")
                .InsertTextOnValidFrom(currentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .SelectTrainingCompleted("No")
                .InsertTextOnTrainingDescription("training desc ...")
                .ClickInstructorLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookFor("System Users").SelectLookIn("Lookup View").SearchAndSelectRecord(_newSystemUserName, _newSystemUserId);

            personSpecificTrainingRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .SelectRecurrence("Weekly")
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectLookIn("Lookup View").SearchAndSelectRecord(_newSystemUserName, _newSystemUserId);

            personSpecificTrainingRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnLengthOfCourse("120")
                .ClickBookingTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord("BookingType-4", _bookingType1);

            personSpecificTrainingRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnCourseStartDate(currentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnCourseEndDate(currentDate.AddDays(1).ToString("dd'/'MM'/'yyyy"))
                .ClickProviderLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SearchAndSelectRecord(providerName, providerId);

            personSpecificTrainingRecordPage
                .WaitForPersonContractRecordPageToLoad()
                .InsertTextOnCourseStartTime("10:00")
                .InsertTextOnCourseEndTime("12:00")
                .ClickStaffContractsLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Person Specific Trainings User " + _currentDateSuffix).TapSearchButton().AddElementToList(_systemUserEmploymentContractId.ToString())
                .TypeSearchQuery("Person Specific Trainings2 User " + _currentDateSuffix).TapSearchButton().AddElementToList(_systemUserEmploymentContract2Id.ToString())
                .TapOKButton();

            personSpecificTrainingRecordPage
                .WaitForPersonContractRecordPageToLoad();

            #endregion

            #region Step 7

            personSpecificTrainingRecordPage
                .ClickSaveButton()
                .WaitForPersonContractRecordPageToLoad()
                .ClickBackButton();

            personSpecificTrainingsPage
                .WaitForPersonSpecificTrainingsPageToLoad()
                .ClickRefreshButton();

            #endregion

            #region Step 8

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_newSystemUserName)
                .ClickSearchButton()
                .OpenRecord(_newSystemUserId);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad();

            #endregion

            #region Step 9

            systemUserRecordPage
                .NavigateToTrainingSubPage();

            #endregion

            #region Step 10

            var userTrainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId, _newSystemUserId);
            Assert.AreEqual(1, userTrainingRecords.Count);
            var userTrainingRecordId = userTrainingRecords.First();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(userTrainingRecordId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingItemLinkFieldText("Default Staff Training Item")
                .ValidateTrainingCourseLinkFieldText("TC 1")
                .ValidateNotesFieldValue(careProviderPersonContractTitle)
                .ValidateStatusFieldSelectedText("Planned");

            #endregion

            #region Step 11

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemUserSection();

            systemUsersPage
                .WaitForSystemUsersPageToLoad()
                .ClickDoNotUseViewFilterCheckbox()
                .InsertUserName(_newSystemUser2Name)
                .ClickSearchButton()
                .OpenRecord(_newSystemUser2Id);

            systemUserRecordPage
                .WaitForSystemUserRecordPageToLoad()
                .NavigateToTrainingSubPage();

            userTrainingRecords = dbHelper.systemUserTraining.GetSystemUserTrainingByTrainingItemIdAndRegardingUserId(staffTrainingItemId, _newSystemUser2Id);
            Assert.AreEqual(1, userTrainingRecords.Count);
            userTrainingRecordId = userTrainingRecords.First();

            systemUserTrainingPage
                .WaitForSystemUserTrainingPageToLoad()
                .OpenRecord(userTrainingRecordId.ToString());

            systemUserTrainingRecordPage
                .WaitForSystemUserTrainingRecordPageToLoad()
                .ValidateTrainingItemLinkFieldText("Default Staff Training Item")
                .ValidateTrainingCourseLinkFieldText("TC 1")
                .ValidateNotesFieldValue(careProviderPersonContractTitle)
                .ValidateStatusFieldSelectedText("Planned");

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














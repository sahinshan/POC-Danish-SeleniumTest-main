using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest.Configuration;
using CareCloudTestFramework;
using Phoenix.DBHelper.Models;
using System.Configuration;
using System.Xml.Linq;
using CareCloudTestFramework.PageObjects;
using System.Runtime.InteropServices.ComTypes;



namespace CareCloud.UITests.LoginPage
{
    [TestFixture]
    [Category("Mobile_TabletMode_Offline"), Category("DomiciliaryCare")]
    public class DomicialaryETEOfflineTests : TestBase
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
        private string _loginUsername = "Domicialary_User_1";
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _carePlanType;
        private Guid _personCarePlanID;
        private Guid _personcareplanregularcaskid;
        private Guid _personcareplanregularcaskid1;
        private Guid _careTaskid;
        private Guid _activecareTaskid;
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
        private Guid cPBookingDiaryStaffId;
        private Guid BookingRegularCareTask1;
        private Guid _activityId;
        private string _personFullname;
        private int _personnumber;
        private Guid _DefaultTeam;
        #endregion


        UIHelper uIHelper;

        [SetUp]
        public void TestInitializationMethod()
        {
            CommonMethodsDB commonMethodsDB = new CommonMethodsDB(dbHelper);

            try
            {

                #region SecurityProfiles

                var userSecProfiles = new List<Guid>();

                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Mobile User (Rostered User)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Alert/Hazard Module (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View Contact")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View People We Support")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View Prospect")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View Referral")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Planning (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Plan Forms (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Scheduling Setup (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Settings (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Timeline Record (View)")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Staff Availability (View)")[0]);


                #endregion

                #region language


                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);


                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");
                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                #endregion

                #region Rostered System User

                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "Testing", "CareCloud", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);
                _DefaultTeam = dbHelper.team.GetTeamIdByName("Automation Domiciliary (Care Cloud)").FirstOrDefault();
                commonMethodsDB.CreateTeamMember(_DefaultTeam, _systemUserId, DateTime.Now, null);

                #endregion

                #region Booking Type 5 -> "Booking (to service user)" 

                _bookingType5 = commonMethodsDB.CreateCPBookingType("BookingType-005", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);

                #endregion

                #region Provider 1

                _providereId1 = commonMethodsDB.CreateProvider("Provider-002", _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider

                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);

                #endregion

                #region Person

                var firstName = "Person_CarePlan1" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "LN_ACC_992";
                var _personFullName = firstName + " " + lastName;
                var addresstypeid = 6; //Home
                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();
                var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1),
                    _ethnicityId, _careDirectorQA_TeamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
                    "PropertyName", "PropertyNo-123", "Akshay Nagar", "Bangalore", "Rammurthy Nagar", "India", "560016");

                //need to pin the record to see in the offline mode

                this.dbHelper.userFavouriteRecord.CreateUserFavouriteRecord(_systemUserId, _personID, "person");


                dbHelper.person.UpdateDOBAndAgeTypeId(_personID, 5);
                dbHelper.person.UpdatePreferredInvoiceDeliveryMethod(_personID, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

                _personFullName = "Person_CarePlan1_ACC_992" + DateTime.Now.ToString("yyyyMMddHHmmss");

                #endregion

                #region create contract scheme

                commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), 999, _providereId1, _providereId1);

                #endregion

                #region create person contract

                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, DateTime.Today);
                dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId, true);

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-002", "1234", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type - Salaried
                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));

                #endregion

                #region Recurrence Patterns

                _recurrencePattern_Every1WeekMondayId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 week(s) on monday", 1, 1);
                _recurrencePattern_Every1WeekTuesdayId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 week(s) on tuesday", 1, 1);
                _recurrencePattern_Every1WeekWednesdayId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 week(s) on wednesday", 1, 1);
                _recurrencePattern_Every1WeekThursdayId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 week(s) on thursday", 1, 1);
                _recurrencePattern_Every1WeekFridayId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 week(s) on friday", 1, 1);
                _recurrencePattern_Every1WeekSaturdayId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 week(s) on saturday", 1, 1);
                _recurrencePattern_Every1WeekSundayId = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 week(s) on sunday", 1, 1);
                _recurrencePatternId_EveryDay = commonMethodsDB.CreateRecurrencePattern("Occurs every 1 days", 1, 1);



                #endregion

                #region Availability Type

                _availabilityTypes_StandardId = commonMethodsDB.CreateAvailabilityType("Standard", 3, false, _careDirectorQA_TeamId, 1, 1, true);


                _availabilityTypes_OverTimeId = commonMethodsDB.CreateAvailabilityType("OverTime", 4, false, _careDirectorQA_TeamId, 1, 1, true);

                #endregion

                #region care Tasks

                _careTaskid = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with bath or shower", 001, DateTime.Now);


                _activecareTaskid = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Activities", 001, DateTime.Now);


                _careTaskid1 = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with dressing", 001, DateTime.Now);


                #endregion

                #region care plan and regular care task

                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, _careDirectorQA_TeamId);
                //dbHelper.personCarePlan.UpdateStatus(_personCarePlanID, 2);

                _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);
                _personcareplanregularcaskid1 = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _activecareTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

                _personcareplanregularcaskid_inactive = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, true, _personID, _careTaskid1, null, "InActiveRegularCareTask", _personCarePlanID);

                #endregion

                #region Activity

                _activityId = commonMethodsDB.CreateCarePlanNeedDomain(_careDirectorQA_TeamId, "Activity-001", DateTime.UtcNow);


                #endregion

                #region SystemUserEmploymentContract

                var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

                //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

                //Link Booking Types with the Employment Contract created previously
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

                #endregion

                #region create diary booking

                var todayDate = DateTime.Now.Date;
                var _applicantId = dbHelper.applicant.CreateApplicant("test", "lastname", _careDirectorQA_TeamId);
                var _roleApplication = dbHelper.recruitmentRoleApplicant.CreateRecruitmentRoleApplicant(_applicantId, _careProviderStaffRoleTypeid, _systemUserId, _careDirectorQA_TeamId, DateTime.Now, _careDirectorQA_TeamId, 1);

                var _UserScheduleIdTypeId = dbHelper.userWorkSchedule.CreateUserWorkSchedule("AutoGenerated", _systemUserId, _careDirectorQA_TeamId, _recurrencePattern_Every1WeekWednesdayId, _systemUserEmploymentContractId, _availabilityTypes_StandardId, todayDate, null, new TimeSpan(6, 0, 0), new TimeSpan(9, 0, 0), _roleApplication, _applicantId, 1);
                var _UserDiaryId = dbHelper.userDairy.createUserDairy(_systemUserId, _careDirectorQA_TeamId, _UserScheduleIdTypeId, _applicantId, _roleApplication, todayDate, todayDate, 60, 1380);

                string people = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
                string staff = "Testing CareCloud - Carer_CDV6_22623 (Employment Contract Type 22623) - 01/01/2020; Status: Active";

                _cpdiarybookingid = dbHelper.cPBookingDiary.CreateCPBookingDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", null, _bookingType5, _providereId1, todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(12, 0, 0), todayDate, new TimeSpan(15, 0, 0), staff, 0, 3, people, 890);

                cPBookingDiaryStaffId = dbHelper.cPBookingDiaryStaff.CreateCPBookingDiaryStaff(_careDirectorQA_TeamId, "test1", _cpdiarybookingid, _systemUserEmploymentContractId, _systemUserId);
                dbHelper.diaryBookingToPeople.CreateDiaryBookingToPeople(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _cpdiarybookingid, _personID, _personcontractId);
                BookingRegularCareTask1 = dbHelper.cPBookingRegularCareTask.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with bath or shower / BookingType-005 booking starting at Wednesday 12:00 at Provider-002", _personcareplanregularcaskid, 1, _cpdiarybookingid, _personID);
                var BookingRegularCareTask2 = dbHelper.cPBookingRegularCareTask.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Activities / BookingType-005 booking starting at Wednesday 12:00 at Provider-002", _personcareplanregularcaskid1, 1, _cpdiarybookingid, _personID);



                #endregion

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw e;
            }

            if (this.IgnoreSetUp)
                return;

            uIHelper = new UIHelper();

            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.Clear);

            //set the default URL
            this.SetDefaultEndpointURL();

            //Login with test user account
            var changeUserButtonVisible = loginPage.WaitForLoginPageToLoad().GetChangeUserButtonVisibility();
            if (changeUserButtonVisible)
            {
                //Login with test user account

                loginPage
                   .WaitForLoginPageToLoad()
                   .InsertUserName(_loginUsername)
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();


                //if the offline mode warning is displayed, then close it
                WarningPopUp.TapNoButtonIfPopupIsOpen();

                //wait for the homepage to load
                HomePage
                    .WaitForHomePageToLoad();
            }
            else
            {
                //Login with test user account
                loginPage
                    // .WaitForBasicLoginPageToLoad()
                    .InsertUserName(_loginUsername)
                    .InsertPassword("Passw0rd_!")
                    .TapLoginButton();

                //Set the PIN Code
                PinPage
                    .WaitForPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK()
                    .WaitForConfirmationPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK();

                //wait for the homepage to load
                HomePage
                    .WaitForHomePageToLoad();
            }
        }

        internal DateTime GetThisWeekFirstWednesday()
        {
            DateTime dt = DateTime.Now;
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Wednesday)) % 7;
            return dt.AddDays(-1 * diff).Date;

        }


        [Test]
        [Property("JiraIssueID", "ACC-6303")]
        [Description("Care Cloud Mobile app -Domiciliary Care -Regression test case for end to end functionality in online mode .Step1-Step6")]
        public void DomicialaryCareOfflineTest_001()
        {

            MainMenu
               .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyPlannedBookingDetails(_cpdiarybookingid.ToString());

            MainMenu.NavigateToSettingsPage("Testing CareCloud");

            SettingsPage.SetTheAppInOfflineMode();

            MainMenu
              .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .TapBookingRecord(_cpdiarybookingid.ToString());

            bookingDetailsPage
                .WaitForBookingDetailsPageToLoad()
                .TapStartVisitButton();

            visitDetailsPage
                .WaitForVisitDetailsPagePageToLoad()
                .ValidateMileageFieldVisible(true)
                .ValidateStaffOverrideFieldVisible(true)
                .SetStaffOverRideMileage("22");

            visitDetailsPage
               .WaitForVisitDetailsPagePageToLoad()
               .TapCareTab()
               .TapEndTheVisit();

            RegularCareTaskLookup
               .WaitForLookupPopupToLoad()
               .ValidateRegualarCareTaskMessage("Are you sure want to end this visit?", "Are you sure want to end this visit?")
               .TapEndTheVisitButton();
            var _personFullName1 = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];

            VisitCompletedPage
                .WaitFor_visitCompletedPageToLoad()
                .ValidateVisitDetailsHeader()
                .ValidateVisitCompletedClientDetails(_personFullName1)
                .clickBackToBookings();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyBookingDetails(_cpdiarybookingid.ToString());

            MainMenu.NavigateToSettingsPage("Testing CareCloud");
            SettingsPage.SetTheAppInOnlineMode();

            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(cPBookingDiaryStaffId, "staffoverridemileage");
            Assert.AreEqual(22m, fields["staffoverridemileage"]);
        }

        [Test]
        [Property("JiraIssueID", "ACC-6304")]
        [Description("Care Cloud Mobile app -Domiciliary Care -Regression test case for end to end functionality in online mode .Step7*Complete below tasks during visit* \r\nComplete a regular care task")]
        public void DomicialaryCareOfflineTest_002()
        {
            MainMenu
              .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyPlannedBookingDetails(_cpdiarybookingid.ToString());

            MainMenu.NavigateToSettingsPage("Testing CareCloud");

            SettingsPage.SetTheAppInOfflineMode();

            MainMenu
               .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .TapBookingRecord(_cpdiarybookingid.ToString());

            bookingDetailsPage
                .WaitForBookingDetailsPageToLoad()
                .TapStartVisitButton();

            visitDetailsPage
              .WaitForVisitDetailsPagePageToLoad()
              .ValidateMileageFieldVisible(true)
              .ValidateStaffOverrideFieldVisible(true)
              .TapCareTab()
              .WaitForVisitDetailsPagePageToLoad()
              .CheckRegualrCareTask(_careTaskid.ToString(), true);

            RegularCareTaskLookup
               .WaitForLookupPopupToLoad()
               .ValidateRegualarCareTaskMessage("Would you like to complete this task?", "Would you like to complete this task?")
               .TapOnSaveNClose();

            visitDetailsPage
                .WaitForVisitDetailsPagePageToLoad()
                .CheckRegualrCareTask(_activecareTaskid.ToString(), true);

            RegularCareTaskLookup
               .WaitForLookupPopupToLoad()
               .ValidateRegualarCareTaskMessage("Would you like to complete this task?", "Would you like to complete this task?")
               .TapOnSaveNClose();

            visitDetailsPage
              .WaitForVisitDetailsPagePageToLoad()
              .TapEndTheVisit();

            RegularCareTaskLookup
               .WaitForLookupPopupToLoad()
               .ValidateRegualarCareTaskMessage("Are you sure want to end this visit?", "Are you sure want to end this visit?")
               .TapEndTheVisitButton();

            var _personFullName1 = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];

            VisitCompletedPage
               .WaitFor_visitCompletedPageToLoad()
               .ValidateVisitDetailsHeader()
               .ValidateVisitCompletedClientDetails(_personFullName1)
               .clickBackToBookings();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyBookingDetails(_cpdiarybookingid.ToString());

            MainMenu.NavigateToSettingsPage("Testing CareCloud");
            SettingsPage.SetTheAppInOnlineMode();

            var fields = dbHelper.careTask.GetCareTaskByID(_activecareTaskid, "name");
            var fields1 = dbHelper.careTask.GetCareTaskByID(_careTaskid, "name");

            Assert.AreEqual("Activities", fields["name"]);
            Assert.AreEqual("Assist with bath or shower", fields1["name"]);


        }

        [Test]
        [Property("JiraIssueID", "ACC-6305")]
        [Description("Care Cloud Mobile app -Domiciliary Care -Regression test case for end to end functionality in online mode " +
                     ".Step7*Add three additional care tasks, complete two ,remove one also remove one completed task")]
        public void DomicialaryCareOfflineTest_003()
        {
            MainMenu
              .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyPlannedBookingDetails(_cpdiarybookingid.ToString());

            MainMenu.NavigateToSettingsPage("Testing CareCloud");

            SettingsPage.SetTheAppInOfflineMode();

            MainMenu
               .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .TapBookingRecord(_cpdiarybookingid.ToString());

            bookingDetailsPage
                .WaitForBookingDetailsPageToLoad()
                .TapStartVisitButton();

            visitDetailsPage
                 .WaitForVisitDetailsPagePageToLoad()
                 .TapCareTab()
                 .WaitForVisitDetailsPagePageToLoad()
                 .ScrollAddDailyCareItem()
                 .TapAddCareTask();

            var caretask1 = dbHelper.careTask.GetByName("Clean kitchen").FirstOrDefault();
            var caretask2 = dbHelper.careTask.GetByName("Assist with dressing").FirstOrDefault();
            var caretask3 = dbHelper.careTask.GetByName("Clean bathroom").FirstOrDefault();


            AdditionalCareTaskLookup
                .WaitForAdditionalCareTaskLookupPopupToLoad()
                .CheckAdditionalRegualrCareTask(caretask1.ToString(), true)
                .CheckAdditionalRegualrCareTask(caretask2.ToString(), true)
                .CheckAdditionalRegualrCareTask(caretask3.ToString(), true)
                .TapOnAddTask();

            visitDetailsPage
               .WaitForVisitDetailsPagePageToLoad()
                .CheckRegualrCareTask(caretask1.ToString(), true);

            RegularCareTaskLookup
               .WaitForLookupPopupToLoad()
               .ValidateRegualarCareTaskMessage("Would you like to complete this task?", "Would you like to complete this task?")
               .TapOnSaveNClose();

            visitDetailsPage
               .WaitForVisitDetailsPagePageToLoad()
                .CheckRegualrCareTask(caretask2.ToString(), true);

            RegularCareTaskLookup
               .WaitForLookupPopupToLoad()
               .ValidateRegualarCareTaskMessage("Would you like to complete this task?", "Would you like to complete this task?")
               .TapOnSaveNClose();

            visitDetailsPage
               .WaitForVisitDetailsPagePageToLoad()
               .TapDeleteAdditionalCareTask(caretask3.ToString());

            RegularCareTaskLookup
              .WaitForLookupPopupToLoad()
              .ValidateRegualarCareTaskMessage("Would you like to remove this task?", "Would you like to remove this task?")
              .TapOnRemoveTask();

            visitDetailsPage
             .WaitForVisitDetailsPagePageToLoad()
             .TapEndTheVisit();

            RegularCareTaskLookup
              .WaitForLookupPopupToLoad()
              .ValidateRegualarCareTaskMessage("Are you sure want to end this visit?", "Are you sure want to end this visit?")
              .TapEndTheVisitButton();

            var _personFullName1 = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];

            VisitCompletedPage
               .WaitFor_visitCompletedPageToLoad()
               .ValidateVisitDetailsHeader()
               .ValidateVisitCompletedClientDetails(_personFullName1)
               .clickBackToBookings();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyBookingDetails(_cpdiarybookingid.ToString());

            var fields = dbHelper.careTask.GetCareTaskByID(caretask1, "name");
            var fields1 = dbHelper.careTask.GetCareTaskByID(caretask2, "name");
            var fields2 = dbHelper.careTask.GetCareTaskByID(caretask3, "name");

            Assert.AreEqual("Clean kitchen", fields["name"]);
            Assert.AreEqual("Assist with dressing", fields1["name"]);
            Assert.IsFalse(false, "Element Doesnot exists", fields2["name"]);



        }

        [Test]
        [Property("JiraIssueID", "ACC-6306")]
        [Description("Care Cloud Mobile app -Domiciliary Care -Regression test case for end to end functionality in online mode " +
                     ".Step7*Add daily care items , update and delete")]
        public void DomicialaryCareOfflineTest_004()
        {
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            MainMenu
              .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyPlannedBookingDetails(_cpdiarybookingid.ToString());

            MainMenu.NavigateToSettingsPage("Testing CareCloud");

            SettingsPage.SetTheAppInOfflineMode();

            MainMenu
                .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .TapBookingRecord(_cpdiarybookingid.ToString());

            bookingDetailsPage
                .WaitForBookingDetailsPageToLoad()
                .TapStartVisitButton();

            visitDetailsPage
               .WaitForVisitDetailsPagePageToLoad()
               .TapCareTab()
               .WaitForVisitDetailsPagePageToLoad()
               .ScrollAddDailyCareItem()
               .TapAddDailyCareItem();

            dailyCareItemLookup
                .WaitForDailyCareItemLookupPopupToLoad(_personFullname, _personnumber)
                .TapDailyRecordIcon();

            personDailyRecordPage
                .WaitForPersonDailyRecordPageLookupToLoad()
                .TypeInNotesAreaTextBox("Notes Test")
                .TapFlagRecordforHandoverButton(1)
                .TapSaveNCloseButton();

            MainMenu.NavigateToSettingsPage("Testing CareCloud");
            SettingsPage.SetTheAppInOnlineMode();

            System.Threading.Thread.Sleep(2000);
            var DailyCareRecord = dbHelper.cPPersonDailyRecord.GetByPersonId(_personID).FirstOrDefault();
            System.Threading.Thread.Sleep(1000);

            MainMenu
                .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .TapBookingRecord(_cpdiarybookingid.ToString());

            visitDetailsPage
             .WaitForVisitDetailsPagePageToLoad()
             .TapCareTab()
             .ScrollAddDailyCareItem()
             .TapDailyCareRecord(DailyCareRecord.ToString());

            visitDetailsPage
              .WaitForVisitDetailsPagePageToLoad()
              .TapAddDailyCareItem()
              .ScrollAddDailyCareItem()
              .TapEndTheVisit();

            RegularCareTaskLookup
                .WaitForLookupPopupToLoad()
                .ValidateRegualarCareTaskMessage("Are you sure want to end this visit?", "Are you sure want to end this visit?")
                .TapEndTheVisitButton();

            var _personFullName1 = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];

            VisitCompletedPage
                .WaitFor_visitCompletedPageToLoad()
                .ValidateVisitDetailsHeader()
                .ValidateVisitCompletedClientDetails(_personFullName1)
                .clickBackToBookings();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyBookingDetails(_cpdiarybookingid.ToString());

        }

        [Test]
        [Property("JiraIssueID", "ACC-6307")]
        [Description("Care Cloud Mobile app -Domiciliary Care -Regression test case for end to end functionality in online mode " +
                     ".Step7*Enter data in Problems and notes & verify in web app")]
        public void DomicialaryCareOfflineTest_005()
        {
            MainMenu
             .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .VerifyPlannedBookingDetails(_cpdiarybookingid.ToString());

            MainMenu.NavigateToSettingsPage("Testing CareCloud");

            SettingsPage.SetTheAppInOfflineMode();

            MainMenu
               .NavigateToHomePage();

            MyBookingsPage
                .WaitForMyBookingsPageToLoad()
                .TapBookingRecord(_cpdiarybookingid.ToString());

            bookingDetailsPage
                .WaitForBookingDetailsPageToLoad()
                .TapStartVisitButton();

            visitDetailsPage
               .WaitForVisitDetailsPagePageToLoad()
               .TapCareTab()
               .ScrollAddDailyCareItem()
               .SetProblesNNotesTextArea("Problems And Notes Test-001")
               .TapEndTheVisit();

            RegularCareTaskLookup
                .WaitForLookupPopupToLoad()
                .ValidateRegualarCareTaskMessage("Are you sure want to end this visit?", "Are you sure want to end this visit?")
                .TapEndTheVisitButton();

            var _personFullName1 = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];

            VisitCompletedPage
               .WaitFor_visitCompletedPageToLoad()
               .ValidateVisitDetailsHeader()
               .ValidateVisitCompletedClientDetails(_personFullName1)
               .clickBackToBookings();

            MainMenu.NavigateToSettingsPage("Testing CareCloud");
            SettingsPage.SetTheAppInOnlineMode();

          
            var fields = dbHelper.cPBookingDiaryStaff.GetCPBookingDiaryStaffByID(cPBookingDiaryStaffId, "problemsandnotes");

            Assert.AreEqual("Problems And Notes Test-001", fields["problemsandnotes"]);


        }
    }
}

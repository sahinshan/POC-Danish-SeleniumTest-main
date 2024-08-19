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

namespace CareCloud.UITests.Resedential_Care_Tests
{
    [TestFixture]
    [Category("Mobile_TabletMode_Online"), Category("ResedentialCare")]
    public class MobilityHandoverCommentsUITestsBOUITests : TestBase
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
        private string _loginUsername = "Handover_User001";
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
        private Guid _carePlanNeedDomainId;
        private Guid _systemAdministratorUserID;
        private string _providerName;
        private string _personFullname;
        private int _personnumber;
        private Guid _carePhysicalLocationFrom;
        private Guid _carePhysicalLocationTo;
        private Guid _mobilityDiatanceUnitId1;
        private Guid _careEquipmentId;
        private Guid _careAssistanceNeededId;
        private Guid _careWellBeingdId;
        private Guid _DefaultTeam;
        private string _loggedInUser;
        #endregion

        UIHelper uIHelper;

        [SetUp]
        public void TestInitializationMethod()
        {
            CommonMethodsDB commonMethodsDB = new CommonMethodsDB(dbHelper);

            try
            {
                #region adminuser

                _systemAdministratorUserID = dbHelper.systemUser.GetSystemUserByUserName("administrator").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID, DateTime.Now.Date);

                #endregion

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
                _DefaultTeam = dbHelper.team.GetTeamIdByName("Automation Residential (Care Cloud)").FirstOrDefault();
                commonMethodsDB.CreateTeamMember(_DefaultTeam, _systemUserId, DateTime.Now, null);

                #endregion

                #region Booking Type 5 -> "Booking (to service user)" 

                _bookingType5 = commonMethodsDB.CreateCPBookingType("Handover_BookingType-005", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);

                #endregion

                #region Provider 1

                _providereId1 = commonMethodsDB.CreateProvider("ProviderHandoverTest_001", _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider

                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);

                #endregion

                #region Person

                var firstName = "Person_Resedential" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "handover";
                var _personFullName = firstName + " " + lastName;
                var addresstypeid = 6; //Home
                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();
                var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1),
                    _ethnicityId, _careDirectorQA_TeamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
                    "PropertyName", "PropertyNo-123", "Akshay Nagar", "Bangalore", "Rammurthy Nagar", "India", "560016");

                dbHelper.person.UpdateDOBAndAgeTypeId(_personID, 5);
                dbHelper.person.UpdatePreferredInvoiceDeliveryMethod(_personID, 2);
                _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];


                #endregion

                #region create contract scheme

                commonMethodsDB.CreateCareProviderContractScheme(_careDirectorQA_TeamId, _systemUserId, _careDirectorQA_BusinessUnitId, "Contract-Scheme-001", new DateTime(2000, 1, 2), 999, _providereId1, _providereId1);

                #endregion

                #region create person contract

                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, DateTime.Today.AddDays(-2));
                dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId, true);

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-001", "1234", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type - Salaried
                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));

                #endregion

                #region Activity

                _carePlanNeedDomainId = commonMethodsDB.CreateCarePlanNeedDomain(_careDirectorQA_TeamId, "Activity-001", DateTime.UtcNow);


                #endregion

                #region care plan and regular care task
                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);
                dbHelper.personCarePlan.UpdateStatus(_personCarePlanID, 2);


                #endregion

                #region SystemUserEmploymentContract

                var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

                //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

                //Link Booking Types with the Employment Contract created previously
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

                #endregion

                #region CarePhysicalLocation

                _carePhysicalLocationFrom = dbHelper.carePhysicalLocation.GetByName("bathroom").FirstOrDefault();
                _carePhysicalLocationTo = dbHelper.carePhysicalLocation.GetByName("bedroom").FirstOrDefault();

                #endregion

                #region CareEquipment

                _careEquipmentId = dbHelper.careEquipment.GetByName("No equipment").FirstOrDefault();

                #endregion

                #region careAssistanceNeededId


                _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Independent").FirstOrDefault();

                #endregion

                #region CareWellBeing

                _careWellBeingdId = dbHelper.careWellbeing.GetByName("Happy").FirstOrDefault();

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
        [Property("JiraIssueID", "ACC-7830")]
        [Description("Care Cloud Mobile app -Resedential Care -Add Handover from-Resident Details (handover tab).Step1-Step3")]
        public void HandoverComments_UITestMethod_001()
        {
            var personfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationTo, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilityDiatanceUnitId1 = dbHelper.mobilityDistanceUnit.GetByName("metres").First();
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];

            DateTime occureddate = DateTime.Now;
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMobilityid = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, occureddate.AddDays(-1), 1, _carePhysicalLocationFrom, _carePhysicalLocationTo, 3, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5, personfirstname + " moved from the " + _carePhysicalLocationfrom + " to the " + _carePhysicalLocationto + ", approximately 3 " + _mobilitydistanceunit + ".\r\n" +
personfirstname + " used the following equipment: " + _careEquipment + ".\r\n" +
personfirstname + " came across as " + _carewellbeing + ".\r\n" +
personfirstname + " required assistance: " + _careassistance + ". Amount given:.\r\n" +
"This care was given at " + occureddate.AddDays(-1).ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\n" +
personfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with " + personfirstname + ".\r\n", true);


            var handoverdetialsid = dbHelper.careProviderPersonHandoverDetail.CreateCareProviderPersonHandoverDetail(_personMobilityid, "", "cppersonmobility", "handover comments", _careDirectorQA_TeamId, false);
            _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID, "fullname")["fullname"];
            var mobilerecord = dbHelper.cPPersonMobility.GetByPersonId(_personID).FirstOrDefault();

            //Step 2
            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .TapHandovers(_providerName);

            //Step 3
            handoversDetailsPage
                .WaitForHandoverDetailsPageToLoad()
                .VerifyHandoverDetails(handoverdetialsid.ToString(), _personFullname, _personnumber, "Mobility")
                .ValidateHandoversystemuserinfo(handoverdetialsid.ToString(), _systemUsername)
                .ValidateHandoverComments(handoverdetialsid.ToString(), "handover comments")
                .VerifyHandoverSeeAllNotes(handoverdetialsid.ToString())
                .VerifyHandoverMarkAsRead(handoverdetialsid.ToString());

        }

        [Test]
        [Property("JiraIssueID", "ACC-7855")]
        [Description("Care Cloud Mobile app -Resedential Care -Add Handover from-Resident Details (handover tab).Step4")]
        public void HandoverComments_UITestMethod_004()
        {
            var personfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationTo, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilityDiatanceUnitId1 = dbHelper.mobilityDistanceUnit.GetByName("metres").First();
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];

            DateTime occureddate = DateTime.Now;
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMobilityid = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, occureddate.AddDays(-1), 1, _carePhysicalLocationFrom, _carePhysicalLocationTo, 3, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5, personfirstname + " moved from the " + _carePhysicalLocationfrom + " to the " + _carePhysicalLocationto + ", approximately 3 " + _mobilitydistanceunit + ".\r\n" +
personfirstname + " used the following equipment: " + _careEquipment + ".\r\n" +
personfirstname + " came across as " + _carewellbeing + ".\r\n" +
personfirstname + " required assistance: " + _careassistance + ". Amount given:.\r\n" +
"This care was given at " + occureddate.AddDays(-1).ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\n" +
personfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with " + personfirstname + ".\r\n", true);


            var handoverdetialsid = dbHelper.careProviderPersonHandoverDetail.CreateCareProviderPersonHandoverDetail(_personMobilityid, "", "cppersonmobility", "handover comments", _careDirectorQA_TeamId, false);
            _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID, "fullname")["fullname"];
            var mobilerecord = dbHelper.cPPersonMobility.GetByPersonId(_personID).FirstOrDefault();

            //Step 2
            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .TapHandovers(_providerName);
            System.Threading.Thread.Sleep(1000);
            //Step 4
            handoversDetailsPage
                .WaitForHandoverDetailsPageToLoad()
                .TapHandoverSeeAllNotes(handoverdetialsid.ToString());

            seeAllNotesPage
                .WaitForSeeAllNotesPageToLoad()
                .VerifySeeAllNotesPage(handoverdetialsid.ToString(), _personFullname, _personnumber, "Mobility")
                .VerifyHandoverAddNotes(handoverdetialsid.ToString())
                .TapHandoverAddNotes(handoverdetialsid.ToString())
                .WaitforNewHandoverNotesToLoad(_personFullname, _personnumber);

        }


        [Test]
        [Property("JiraIssueID", "ACC-7831")]
        [Description("Care Cloud Mobile app -Resedential Care -Add Handover from-Resident Details (handover tab).Step5")]
        public void HandoverComments_UITestMethod_002()
        {
            var personfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationTo, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilityDiatanceUnitId1 = dbHelper.mobilityDistanceUnit.GetByName("metres").First();
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];

            DateTime occureddate = DateTime.Now;
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMobilityid = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, occureddate.AddDays(-1), 1, _carePhysicalLocationFrom, _carePhysicalLocationTo, 3, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5, personfirstname + " moved from the " + _carePhysicalLocationfrom + " to the " + _carePhysicalLocationto + ", approximately 3 " + _mobilitydistanceunit + ".\r\n" +
personfirstname + " used the following equipment: " + _careEquipment + ".\r\n" +
personfirstname + " came across as " + _carewellbeing + ".\r\n" +
personfirstname + " required assistance: " + _careassistance + ". Amount given:.\r\n" +
"This care was given at " + occureddate.AddDays(-1).ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\n" +
personfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with " + personfirstname + ".\r\n", true);


            var handoverdetialsid = dbHelper.careProviderPersonHandoverDetail.CreateCareProviderPersonHandoverDetail(_personMobilityid, "", "cppersonmobility", "handover comments", _careDirectorQA_TeamId, false);
            _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID, "fullname")["fullname"];
            var mobilerecord = dbHelper.cPPersonMobility.GetByPersonId(_personID).FirstOrDefault();

            //Step 2
            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .TapHandovers(_providerName);

            //Step 4
            handoversDetailsPage
                .WaitForHandoverDetailsPageToLoad()
                .TapHandoverSeeAllNotes(handoverdetialsid.ToString());

            //step 5-set 3000 characters data
            seeAllNotesPage
                .WaitForSeeAllNotesPageToLoad()
                .TapHandoverAddNotes(handoverdetialsid.ToString())
                .SetHandoverNotes("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, quis gravida magna mi a libero. Fusce vulputate eleifend sapien. Vestibulum purus quam, scelerisque ut, mollis sed, nonummy id, metus. Nullam accumsan lorem in dui. Cras ultricies mi eu turpis hendrerit fringilla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In ac dui quis mi consectetuer lacinia. Nam pretium turpis et arcu. Duis arcu tortor, suscipit eget, imperdiet nec, imperdiet iaculis, ipsum. Sed aliquam ultrices mauris. Integer ante arcu, accumsan a, consectetuer eget, posuere ut, mauris. Praesent adipiscing. Phasellus ullamcorper ipsum rutrum nunc. Nunc nonummy metus. Vestibulum volutpat pretium libero. Cras id dui. Aenean ut eros et nisl sagittis vestibulum. Nullam nulla eros, ultricies sit amet, nonummy id, imperdiet feugiat, pede. Sed lectus. Donec mollis hendrerit risus. Phasellus nec sem in justo pellentesque facilisis. Etiam imperdiet imperdiet orci. Nunc nec neque. Phasellus leo dolor, tempus non, auctor et, hendrerit quis, nisi. Curabitur ligula sapien, tincidunt non, euismod vitae, posuere imperdiet, leo. Maecenas malesuada. Praesent congue erat at massa. Sed cursus turpis vitae tortor. Donec posuere vulputate arcu. Phasellus accumsan cursus velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed aliquam, nisi quis porttitor congue, elit erat euismod orci, ac placerat dolor lectus quis orci. Phasellus consectetuer vestibulum elit. Aenean tellus metus, bibendum sed, posuere ac, mattis non, nunc. Vestibulum fringilla pede sit amet augue. In turpis. Pellentesque posuere. Praesent turpis. Aenean posuere, tor")
                .TapSaveHandoveNotes();

            System.Threading.Thread.Sleep(1000);
            _loggedInUser = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "fullname")["fullname"];
            var _newhandoverrecordid = dbHelper.careProviderPersonHandoverDetail.GetByrecordid(mobilerecord).FirstOrDefault();
            var handovercomments = (string)dbHelper.careProviderPersonHandoverDetail.GetHandoverRecordDetailsByID(_newhandoverrecordid, "handovercomments")["handovercomments"];
            System.Threading.Thread.Sleep(2000);

            seeAllNotesPage
            .WaitForSeeAllNotesPageToLoad()
            .ValidateSeeAllNotessystemuserinfo(_newhandoverrecordid.ToString(), _loggedInUser)
            .ValidateSeeAllNotesComments(_newhandoverrecordid.ToString(), handovercomments);


        }

        [Test]
        [Property("JiraIssueID", "ACC-7832")]
        [Description("Care Cloud Mobile app -Resedential Care -Add Handover from-Resident Details (handover tab).Step8-9")]
        public void HandoverComments_UITestMethod_003()
        {
            var personfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationTo, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilityDiatanceUnitId1 = dbHelper.mobilityDistanceUnit.GetByName("metres").First();
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];

            DateTime occureddate = DateTime.Now;
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMobilityid = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, occureddate.AddDays(-1), 1, _carePhysicalLocationFrom, _carePhysicalLocationTo, 3, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5, personfirstname + " moved from the " + _carePhysicalLocationfrom + " to the " + _carePhysicalLocationto + ", approximately 3 " + _mobilitydistanceunit + ".\r\n" +
personfirstname + " used the following equipment: " + _careEquipment + ".\r\n" +
personfirstname + " came across as " + _carewellbeing + ".\r\n" +
personfirstname + " required assistance: " + _careassistance + ". Amount given:.\r\n" +
"This care was given at " + occureddate.AddDays(-1).ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\n" +
personfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with " + personfirstname + ".\r\n", true);


            var handoverdetialsid = dbHelper.careProviderPersonHandoverDetail.CreateCareProviderPersonHandoverDetail(_personMobilityid, "", "cppersonmobility", "handover comments", _careDirectorQA_TeamId, false);
            _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID, "fullname")["fullname"];
            var mobilerecord = dbHelper.cPPersonMobility.GetByPersonId(_personID).FirstOrDefault();

            //Step 2
            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .TapHandovers(_providerName);

            //Step 4
            handoversDetailsPage
                .WaitForHandoverDetailsPageToLoad()
                .TapHandoverSeeAllNotes(handoverdetialsid.ToString());

            //step 8
            seeAllNotesPage
                .WaitForSeeAllNotesPageToLoad()
                .TapHandoverAddNotes(handoverdetialsid.ToString())
                .SetHandoverNotes("")
                .TapSaveHandoveNotes()
                .ValidateErrorMessage("Please enter the notes")
                .SetHandoverNotes("new comments")
                .TapCancelHandoveNotes();

            var _newhandoverrecordid = dbHelper.careProviderPersonHandoverDetail.GetByrecordid(mobilerecord).FirstOrDefault();
            var handovercomments = (string)dbHelper.careProviderPersonHandoverDetail.GetHandoverRecordDetailsByID(_newhandoverrecordid, "handovercomments")["handovercomments"];
            System.Threading.Thread.Sleep(2000);

            seeAllNotesPage
            .WaitForSeeAllNotesPageToLoad()
            .ValidateSeeAllNotessystemuserinfo(_newhandoverrecordid.ToString(), _systemUsername)
            .ValidateSeeAllNotesComments(_newhandoverrecordid.ToString(), handovercomments);


        }

        [Test]
        [Property("JiraIssueID", "ACC-7894")]
        [Description("Handover screen (single resident view).Step1-4")]
        public void HandoverComments_UITestMethod_005()
        {
            var personfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationTo, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilityDiatanceUnitId1 = dbHelper.mobilityDistanceUnit.GetByName("metres").First();
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];

            DateTime occureddate = DateTime.Now;
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMobilityid = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, occureddate.AddDays(-1), 1, _carePhysicalLocationFrom, _carePhysicalLocationTo, 3, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5, personfirstname + " moved from the " + _carePhysicalLocationfrom + " to the " + _carePhysicalLocationto + ", approximately 3 " + _mobilitydistanceunit + ".\r\n" +
personfirstname + " used the following equipment: " + _careEquipment + ".\r\n" +
personfirstname + " came across as " + _carewellbeing + ".\r\n" +
personfirstname + " required assistance: " + _careassistance + ". Amount given:.\r\n" +
"This care was given at " + occureddate.AddDays(-1).ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\n" +
personfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with " + personfirstname + ".\r\n", true);


            var handoverdetialsid = dbHelper.careProviderPersonHandoverDetail.CreateCareProviderPersonHandoverDetail(_personMobilityid, "", "cppersonmobility", "handover comments", _careDirectorQA_TeamId, false);
            _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID, "fullname")["fullname"];
            var mobilerecord = dbHelper.cPPersonMobility.GetByPersonId(_personID).FirstOrDefault();
            var handovercomments = (string)dbHelper.careProviderPersonHandoverDetail.GetHandoverRecordDetailsByID(handoverdetialsid, "handovercomments")["handovercomments"];

            //Step 2
            MainMenu
              .NavigateToHomePage();

            //Step 3
            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());

            resedentDetailsPage
               .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
               .TapHandOverNotesTab();

            //Step 4
            resedentHandoverNotesPage
                .WaitForResedentHandoverNotesPageToLoad()
                .VerifyResHandoverDetails("Mobility")
                .ValidateResHandoversystemuserinfo(handoverdetialsid.ToString(), _systemUsername)
                .ValidateResHandoverComments(handoverdetialsid.ToString(), handovercomments)
                .VerifyResHandoverAddNotes(handoverdetialsid.ToString())
                .VerifyResHandoverMarkAsRead(handoverdetialsid.ToString())
                .TapResHandoverAddNotes(handoverdetialsid.ToString());


            seeAllNotesPage
                .WaitforNewResHandoverNotesToLoad(_personFullname, _personnumber);

        }

        [Test]
        [Property("JiraIssueID", "ACC-7895")]
        [Description("Handover screen (single resident view).Step 5")]
        public void HandoverComments_UITestMethod_006()
        {
            var personfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationTo, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilityDiatanceUnitId1 = dbHelper.mobilityDistanceUnit.GetByName("metres").First();
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];

            DateTime occureddate = DateTime.Now;
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMobilityid = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, occureddate.AddDays(-1), 1, _carePhysicalLocationFrom, _carePhysicalLocationTo, 3, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5, personfirstname + " moved from the " + _carePhysicalLocationfrom + " to the " + _carePhysicalLocationto + ", approximately 3 " + _mobilitydistanceunit + ".\r\n" +
personfirstname + " used the following equipment: " + _careEquipment + ".\r\n" +
personfirstname + " came across as " + _carewellbeing + ".\r\n" +
personfirstname + " required assistance: " + _careassistance + ". Amount given:.\r\n" +
"This care was given at " + occureddate.AddDays(-1).ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\n" +
personfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with " + personfirstname + ".\r\n", true);


            var handoverdetialsid = dbHelper.careProviderPersonHandoverDetail.CreateCareProviderPersonHandoverDetail(_personMobilityid, "", "cppersonmobility", "handover comments", _careDirectorQA_TeamId, false);
            var mobilerecord = dbHelper.cPPersonMobility.GetByPersonId(_personID).FirstOrDefault();
            var handovercomments = (string)dbHelper.careProviderPersonHandoverDetail.GetHandoverRecordDetailsByID(handoverdetialsid, "handovercomments")["handovercomments"];

            //Step 2
            MainMenu
              .NavigateToHomePage();

            //Step 3
            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());

            resedentDetailsPage
               .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
               .TapHandOverNotesTab();

            //Step 5
            resedentHandoverNotesPage
                .WaitForResedentHandoverNotesPageToLoad()
                .VerifyResHandoverDetails("Mobility")
                .TapResHandoverAddNotes(handoverdetialsid.ToString());


            seeAllNotesPage
                .WaitforNewResHandoverNotesToLoad(_personFullname, _personnumber)
                .SetHandoverNotes("Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc, quis gravida magna mi a libero. Fusce vulputate eleifend sapien. Vestibulum purus quam, scelerisque ut, mollis sed, nonummy id, metus. Nullam accumsan lorem in dui. Cras ultricies mi eu turpis hendrerit fringilla. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; In ac dui quis mi consectetuer lacinia. Nam pretium turpis et arcu. Duis arcu tortor, suscipit eget, imperdiet nec, imperdiet iaculis, ipsum. Sed aliquam ultrices mauris. Integer ante arcu, accumsan a, consectetuer eget, posuere ut, mauris. Praesent adipiscing. Phasellus ullamcorper ipsum rutrum nunc. Nunc nonummy metus. Vestibulum volutpat pretium libero. Cras id dui. Aenean ut eros et nisl sagittis vestibulum. Nullam nulla eros, ultricies sit amet, nonummy id, imperdiet feugiat, pede. Sed lectus. Donec mollis hendrerit risus. Phasellus nec sem in justo pellentesque facilisis. Etiam imperdiet imperdiet orci. Nunc nec neque. Phasellus leo dolor, tempus non, auctor et, hendrerit quis, nisi. Curabitur ligula sapien, tincidunt non, euismod vitae, posuere imperdiet, leo. Maecenas malesuada. Praesent congue erat at massa. Sed cursus turpis vitae tortor. Donec posuere vulputate arcu. Phasellus accumsan cursus velit. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Sed aliquam, nisi quis porttitor congue, elit erat euismod orci, ac placerat dolor lectus quis orci. Phasellus consectetuer vestibulum elit. Aenean tellus metus, bibendum sed, posuere ac, mattis non, nunc. Vestibulum fringilla pede sit amet augue. In turpis. Pellentesque posuere. Praesent turpis. Aenean posuere, tor")
                .TapSaveHandoveNotes();

            System.Threading.Thread.Sleep(2000);
            _loggedInUser = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "fullname")["fullname"];
            var _newhandoverrecordid = dbHelper.careProviderPersonHandoverDetail.GetByrecordid(mobilerecord).FirstOrDefault();
            var Newhandovercomments = (string)dbHelper.careProviderPersonHandoverDetail.GetHandoverRecordDetailsByID(_newhandoverrecordid, "handovercomments")["handovercomments"];
            System.Threading.Thread.Sleep(3000);

            seeAllNotesPage
            .ValidateSeeAllNotessystemuserinfo(_newhandoverrecordid.ToString(), _loggedInUser)
            .ValidateSeeAllNotesComments(_newhandoverrecordid.ToString(), Newhandovercomments);

          

        }

        [Test]
        [Property("JiraIssueID", "ACC-7896")]
        [Description("Handover screen (single resident view).Step 6-7")]
        public void HandoverComments_UITestMethod_007()
        {
            var personfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationTo, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilityDiatanceUnitId1 = dbHelper.mobilityDistanceUnit.GetByName("metres").First();
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];

            DateTime occureddate = DateTime.Now;
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];

            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMobilityid = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, occureddate.AddDays(-1), 1, _carePhysicalLocationFrom, _carePhysicalLocationTo, 3, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5, personfirstname + " moved from the " + _carePhysicalLocationfrom + " to the " + _carePhysicalLocationto + ", approximately 3 " + _mobilitydistanceunit + ".\r\n" +
personfirstname + " used the following equipment: " + _careEquipment + ".\r\n" +
personfirstname + " came across as " + _carewellbeing + ".\r\n" +
personfirstname + " required assistance: " + _careassistance + ". Amount given:.\r\n" +
"This care was given at " + occureddate.AddDays(-1).ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\n" +
personfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with " + personfirstname + ".\r\n", true);


            var handoverdetialsid = dbHelper.careProviderPersonHandoverDetail.CreateCareProviderPersonHandoverDetail(_personMobilityid, "", "cppersonmobility", "handover comments", _careDirectorQA_TeamId, false);
            _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemAdministratorUserID, "fullname")["fullname"];
            var mobilerecord = dbHelper.cPPersonMobility.GetByPersonId(_personID).FirstOrDefault();

            //Step 2
            MainMenu
              .NavigateToHomePage();

            //Step 3
            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());

            resedentDetailsPage
               .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
               .TapHandOverNotesTab();

            //Step 5
            resedentHandoverNotesPage
                .WaitForResedentHandoverNotesPageToLoad()
                .VerifyResHandoverDetails("Mobility")
                .TapResHandoverAddNotes(handoverdetialsid.ToString());


            seeAllNotesPage
                .WaitforNewResHandoverNotesToLoad(_personFullname, _personnumber)
                .SetHandoverNotes("")
                .TapSaveHandoveNotes()
                .ValidateErrorMessage("Please enter the notes")
                .SetHandoverNotes("new comments")
                .TapCancelHandoveNotes();

            var _newhandoverrecordid = dbHelper.careProviderPersonHandoverDetail.GetByrecordid(mobilerecord).FirstOrDefault();
            var handovercomments = (string)dbHelper.careProviderPersonHandoverDetail.GetHandoverRecordDetailsByID(_newhandoverrecordid, "handovercomments")["handovercomments"];
            System.Threading.Thread.Sleep(2000);

            seeAllNotesPage
            .ValidateSeeAllNotessystemuserinfo(_newhandoverrecordid.ToString(), _systemUsername)
            .ValidateSeeAllNotesComments(_newhandoverrecordid.ToString(), handovercomments);


        }
    }

}

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
    public class RepositioningBOUITests : TestBase
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
        private string _loginUsername = "Repositioning_User";
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
        private string _personFirstname;
        private Guid _personcareplanregularcaskscheduleid;

        #endregion




        UIHelper uIHelper;

        [SetUp]
        public void TestInitializationMethod()
        {
            CommonMethodsDB commonMethodsDB = new CommonMethodsDB(dbHelper);

            try
            {
                _systemAdministratorUserID = dbHelper.systemUser.GetSystemUserByUserName("administrator").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemAdministratorUserID, DateTime.Now.Date);


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

                _bookingType5 = commonMethodsDB.CreateCPBookingType("RepositioningBookingType-005", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);

                #endregion

                #region Provider 1

                _providereId1 = commonMethodsDB.CreateProvider("RepositioningProvider-001", _careDirectorQA_TeamId, 31, true); //create a "Residential Establishment" provider

                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);

                #endregion

                #region Person

                var FirstName = "Person_Resedential" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "WelCHK";
                var _personFullName = FirstName + " " + lastName;
                var addresstypeid = 6; //Home
                var personRecordExists = dbHelper.person.GetByFirstName(FirstName).Any();
                var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("African")[0];
                var dob = DateTime.Now.Date.AddYears(-15);
                Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
                Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
                int AddressTypeId_Primary = 7;
                int AccommodationStatusId = 1;
                int LivesAloneTypeId = 1;
                int GenderId = 1;

                // _personID = dbHelper.person.CreatePersonRecord("", FirstName, "", "AutomationQAPersonDeceased", "", dob, _ethnicityId, MaritalStatus, _languageId, AddressPropertyType, _careDirectorQA_TeamId, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);
                _personID = dbHelper.person.CreatePersonRecord("", FirstName, "", lastName, "", new DateTime(1990, 1, 1), _ethnicityId, _careDirectorQA_TeamId, 7, 2, 4);

                /* _personID = dbHelper.person.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1),
                     _ethnicityId, _careDirectorQA_TeamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
                     "PropertyName", "PropertyNo-123", "Akshay Nagar", "Bangalore", "Rammurthy Nagar", "India", "560016");*/

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

                _carePhysicalLocationFrom = dbHelper.carePhysicalLocation.GetByName("Bathroom").FirstOrDefault();
                _carePhysicalLocationTo = dbHelper.carePhysicalLocation.GetByName("Bedroom").FirstOrDefault();

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

                #region Create Care Task
                _careTaskid = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Repositioning", 001, DateTime.Now);
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
        [Property("JiraIssueID", "ACC-8201")]
        [Description("Care Cloud Mobile app -Resedential Care -As a rostered user verify repositioning full record, required fields & sections of full record when “Repositioning Required “field selected as  “Required “in the mobile app")]
        public void RepositionBO_UITestMethod_001()
        {
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            _personFirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];


            var todayDate = DateTime.Today.AddHours(DateTime.Now.Hour - 1);


            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());

            //Tap Mobility BO
            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .TapCareTab()
                .TapAddCareButton()
                .TapBO(_personFullname, _personnumber, "Repositioning")
                .TapCareConsentgivenYes(_personFullname, _personnumber);

            repositioningRecordPage
                .WaitForRepositioningRecordPageLookupToLoad(_personFullname, _personnumber)
                .validatePreferencesText(_personFirstname)
                .TapStartingPosition("Lying")
                .SelectIsRepositioningRequired("Repositioned")
                .TapRepositionedTo("Sitting")
                .SelectRepositionedToSide("Left side")
                .SelectAreTheyComfortable("Comfortable")
                .SelectTypeOfMatressInUse("Lateral Tilt Mattress")
                .SelectIsTheMatressOn("Yes")
                .SelectIsTheMatressInPosition("Yes")
                .SelectIsTheMatressWorking("Yes")
                .SelectConcernsWithSkinCondition("Yes")
                .SetWhereonTheBodyTxtArea("Legs")
                .TapSelectSkinCondition();

            selectSkinConditionPopUp
                .WaitForSkinConditionPopUpToLoad()
                .searchObservation("Dry Skin")
                .TapOnObservationsChk("Dry Skin")
                .searchObservation("Other")
                .TapOnObservationsChk("Other")
                .TapConfirmObservations();

            repositioningRecordPage
                .WaitForRepositioningRecordPageLookupToReLoad(_personFullname, _personnumber)
                .SetSkinConditionOtherTxtArea("Legment Tear")
                .TapLocationButton(_carePhysicalLocationFrom.ToString())
                .TapEquipmentButton(_careEquipmentId.ToString())
                .TapWellbeingButton(_careWellBeingdId.ToString())
                .TapAssistanceButton(_careAssistanceNeededId.ToString())
                .InsertTimeCareGiven();

            timePickerPopUp
                .WaitForTimePickerPopUpToLoad()
                .TapTimeInputMode()
                .setInputHourTime(todayDate.ToString("hh"))
                .setInputMinuteTime(todayDate.ToString("mm"))
                .TapTimeSetButton();

            repositioningRecordPage
               .WaitForRepositioningRecordPageLookupToReLoad(_personFullname, _personnumber)
               .InsertTimeSpent()
               .SetAdditionalNotesTxtArea("Additional Notes")
               .TapsaveNClose();
            System.Threading.Thread.Sleep(1000);
            var mobilerecord = dbHelper.cPPersonTurning.GetByPersonId(_personID).FirstOrDefault();
            var perfonfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "username")["username"];
            Guid _specialistmatress = dbHelper.specialistmattress.GetByName("Lateral Tilt Mattress").FirstOrDefault();
            var matressused = (string)dbHelper.specialistmattress.GetById(_specialistmatress, "name")["name"];

            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .VerifyConsentAgreed(mobilerecord.ToString(), todayDate.ToString("hh:mm tt"))
                .VerifyConsentAgreed(mobilerecord.ToString(), "Consent given")
                .VerifyConsentAgreed(mobilerecord.ToString(), _systemUsername)
                .TapCompletedRecord(mobilerecord.ToString())
                .validateCareNote(perfonfirstname + " was Lying.\n" +
perfonfirstname + " was repositioned to a Sitting position.\n" +
perfonfirstname + " was repositioned to their Left side.\n" +
perfonfirstname + " was Comfortable.\n" +
"A " + matressused + " was in use to relieve pressure.\n" +
"The mattress was switched on: Yes\n" +
"The mattress was working: Yes\n" +
"The mattress was in the correct position: Yes\n" +
"The following new skin concerns were noted on " + perfonfirstname + "'s Legs: Dry Skin and Legment Tear.\n" +
perfonfirstname + " was in the " + _carePhysicalLocationfrom + ".\n" +
perfonfirstname + " used the following equipment: " + _careEquipment + ".\n" +
perfonfirstname + " came across as " + _carewellbeing + ".\n" +
"This care was given at " + todayDate.ToString("HH:mm") + ".\n" +
perfonfirstname + " was assisted by 1 colleague(s).\n" +
"Overall, I spent 5 minutes with " + perfonfirstname + ".\n" +
"We would like to note that: Additional Notes."
);

        }

        [Test]
        [Property("JiraIssueID", "ACC-8237")]
        [Description("Care Cloud Mobile app -Resedential Care -As a rostered user verify repositioning full record, required fields & sections of full record when “Repositioning Required “field selected as  “Not Required “in the mobile app")]
        public void RepositionBO_UITestMethod_002()
        {
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            _personFirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];


            var todayDate = DateTime.Today.AddHours(DateTime.Now.Hour - 1);


            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());

            //Tap Mobility BO
            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .TapCareTab()
                .TapAddCareButton()
                .TapBO(_personFullname, _personnumber, "Repositioning")
                .TapCareConsentgivenYes(_personFullname, _personnumber);

            repositioningRecordPage
                .WaitForRepositioningRecordPageLookupToLoad(_personFullname, _personnumber)
                // .validatePreferencesText(_personFirstname + "\n\n\n" + "Read only" + "\n\r\r")
                .TapStartingPosition("Lying")
                .SelectIsRepositioningRequired("Not required")
                .SelectAreTheyComfortable("Comfortable")
                .SelectTypeOfMatressInUse("Lateral Tilt Mattress")
                .SelectIsTheMatressOn("Yes")
                .SelectIsTheMatressInPosition("Yes")
                .SelectIsTheMatressWorking("Yes")
                .SelectConcernsWithSkinCondition("Yes")
                .SetWhereonTheBodyTxtArea("Legs")
                .TapSelectSkinCondition();

            selectSkinConditionPopUp
                .WaitForSkinConditionPopUpToLoad()
                .searchObservation("Dry Skin")
                .TapOnObservationsChk("Dry Skin")
                .TapConfirmObservations();

            repositioningRecordPage
                 .WaitForRepositioningRecordPageLookupToReLoad(_personFullname, _personnumber)
                 .TapEditSkinCondition();

            selectSkinConditionPopUp
               .WaitForSkinConditionPopUpToLoad()
                .searchObservation("Other")
                .TapOnObservationsChk("Other")
                .TapConfirmObservations();

            repositioningRecordPage
                .WaitForRepositioningRecordPageLookupToReLoad(_personFullname, _personnumber)
                .SetSkinConditionOtherTxtArea("Legment Tear")
                .TapLocationButton(_carePhysicalLocationFrom.ToString())
                .TapEquipmentButton(_careEquipmentId.ToString())
                .TapWellbeingButton(_careWellBeingdId.ToString())
                .TapAssistanceButton(_careAssistanceNeededId.ToString())
                .InsertTimeCareGiven();

            timePickerPopUp
                .WaitForTimePickerPopUpToLoad()
                .TapTimeInputMode()
                .setInputHourTime(todayDate.ToString("hh"))
                .setInputMinuteTime(todayDate.ToString("mm"))
                .TapTimeSetButton();

            repositioningRecordPage
               .WaitForRepositioningRecordPageLookupToReLoad(_personFullname, _personnumber)
               .InsertTimeSpent()
               .SetAdditionalNotesTxtArea("Additional Notes")
               .TapsaveNClose();
            System.Threading.Thread.Sleep(1000);
            var mobilerecord = dbHelper.cPPersonTurning.GetByPersonId(_personID).FirstOrDefault();
            var perfonfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationFrom, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "username")["username"];
            Guid _specialistmatress = dbHelper.specialistmattress.GetByName("Lateral Tilt Mattress").FirstOrDefault();
            var matressused = (string)dbHelper.specialistmattress.GetById(_specialistmatress, "name")["name"];

            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .VerifyConsentAgreed(mobilerecord.ToString(), todayDate.ToString("hh:mm tt"))
                .VerifyConsentAgreed(mobilerecord.ToString(), "Consent given")
                .VerifyConsentAgreed(mobilerecord.ToString(), _systemUsername)
                .TapCompletedRecord(mobilerecord.ToString())
                .validateCareNote(perfonfirstname + " was Lying.\n" +
perfonfirstname + " was not repositioned.\n" +
perfonfirstname + " was Comfortable.\n" +
"A " + matressused + " was in use to relieve pressure.\n" +
"The mattress was switched on: Yes\n" +
"The mattress was working: Yes\n" +
"The mattress was in the correct position: Yes\n" +
"The following new skin concerns were noted on " + perfonfirstname + "'s Legs: Dry Skin and Legment Tear.\n" +
perfonfirstname + " was in the " + _carePhysicalLocationfrom + ".\n" +
perfonfirstname + " used the following equipment: " + _careEquipment + ".\n" +
perfonfirstname + " came across as " + _carewellbeing + ".\n" +
"This care was given at " + todayDate.ToString("HH:mm") + ".\n" +
perfonfirstname + " was assisted by 1 colleague(s).\n" +
"Overall, I spent 5 minutes with " + perfonfirstname + ".\n" +
"We would like to note that: Additional Notes."
);

        }

        [Test]
        [Property("JiraIssueID", "ACC-8244")]
        [Description("Care Cloud Mobile app -Resedential Care -As a rostered user verify UI and functionality when defer a scheduled Repositioning care task in the mobile app")]
        public void RepositionBO_UITestMethod_003()
        {
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            _personFirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var ScheduleDate = DateTime.Now;
            var scheduleTime = DateTime.Now.TimeOfDay;
            #region Regular Care Task

            _personcareplanregularcaskid = dbHelper.regularCareTask.CreateRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, false, _personID, _careTaskid, null, "ActiveRegularCareTask", _personCarePlanID);

            #endregion

            #region Schedule Care

            _personcareplanregularcaskscheduleid = dbHelper.cpRegularCareTaskSchedule.CreateCPBookingRegularCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "title", _personID, _personcareplanregularcaskid, _personCarePlanID, ScheduleDate, new TimeSpan(0, 05, 0), 2, 1, null);
            var Carediary = dbHelper.cpRegularCareTaskDiary.CreateRegularCareTaskDiary(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _personID, _personCarePlanID, 1, DateTime.Now, scheduleTime, _personcareplanregularcaskid, "");
            #endregion

            var todayDate = DateTime.Today.AddHours(DateTime.Now.Hour - 1);
            var DefferedDate = DateTime.Now.AddDays(1);

            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());

            //Tap Mobility BO
            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .TapCareTab()
                .TapScheduledBO(_personFullname, _personnumber)
                .TapCareConsentgivenNo(_personFullname, _personnumber)
                .TapCareConsentDeferred(_personFullname, _personnumber)
                .setReasonCareConsenDeferred(_personFullname, _personnumber);

            timePickerPopUp
               .WaitForTimePickerPopUpToLoad()
               .TapTimeInputMode()
               .setInputHourTime(DefferedDate.ToString("hh"))
               .setInputMinuteTime(DefferedDate.ToString("mm"))
               .TapTimeSetButton();

            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .TapsaveNClose();

            System.Threading.Thread.Sleep(1000);
            var mobilerecord = dbHelper.cPPersonTurning.GetByPersonId(_personID).FirstOrDefault();

            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .TapCompletedRecord(mobilerecord.ToString())
                .VerifyConsentAgreed(mobilerecord.ToString(), DefferedDate.ToString("hh:mm tt"))
                .VerifyConsentAgreed(mobilerecord.ToString(), "Deferred To")
                .VerifyConsentAgreed(mobilerecord.ToString(), _systemUsername)
                .validateCareDeferred("Deferred To:", DefferedDate.ToString("dd'/'MM'/'yyyy"), DefferedDate.ToString("hh:mm tt"));

            System.Threading.Thread.Sleep(1000);
            var DeferredCareDiaryID = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID,4);
            var DeferredCareDiaryCount = DeferredCareDiaryID.Count();
            Assert.AreEqual(1, DeferredCareDiaryCount);
            var FuturecareDiaryIDs = dbHelper.cpRegularCareTaskDiary.GetByPersonIdAndCarePlanId(_personID, _personCarePlanID,1);
            var FutureCareDiaryCount = FuturecareDiaryIDs.Count();
            Assert.AreEqual(1, FutureCareDiaryCount);
        }

    }

}

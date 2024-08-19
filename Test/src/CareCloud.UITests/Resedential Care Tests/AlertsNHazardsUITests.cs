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
    public class AlertsNHazardsUITests : TestBase
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
        private string _loginUsername = "AlertsUser_1";
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _carePlanType;
        private Guid _personCarePlanID;
        private string _systemUsername;
        private Guid _personCarePlanFormID;
        private Guid _providereId1;
        private Guid _personcontractId;
        private Guid _contractschemeid;
        private Guid _systemAdministratorUserID;
        private string _providerName;
        private string _personFullname;
        private int _personnumber;
        private Guid _alertAndHazardsType;
        private Guid personAlertAndHazards;
        private Guid _carePlanNeedDomainId;
        private Guid _bookingType5;
        private Guid _personAllergyId;
        private Guid _alergyTypeId;
        private Guid _diagnosisid;
        private Guid _personDiagnosisId;
        private Guid _DefaultTeam;
        private Guid _careProviderStaffRoleTypeid;
        private Guid _employmentContractTypeid1;


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

                _bookingType5 = commonMethodsDB.CreateCPBookingType("AlertBookingType-005", 5, 960, new TimeSpan(6, 0, 0), new TimeSpan(22, 0, 0), 2, false, 1440);

                #endregion

                #region Provider 1

                _providereId1 = commonMethodsDB.CreateProvider("AlertProvider-003", _careDirectorQA_TeamId, 13, true); //create a "Residential Establishment" provider

                commonMethodsDB.CreateProviderAllowableBookingTypes(_careDirectorQA_TeamId, _providereId1, _bookingType5, true);

                #endregion

                #region Person

                var firstName = "Person_Resedential" + DateTime.Now.ToString("yyyyMMddHHmmss");
                var lastName = "Alert";
                var _personFullName = firstName + " " + lastName;
                var addresstypeid = 6; //Home
                var personRecordExists = dbHelper.person.GetByFirstName(firstName).Any();
                var _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                _personID =commonMethodsDB.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 1),
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

                _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_careDirectorQA_TeamId, "title", _personID, _systemUserId, _providereId1, _contractschemeid, _providereId1, DateTime.Today);
                dbHelper.careProviderPersonContract.UpdatePcIsEnabledForScheduleBooking(_personcontractId, true);

                #endregion

                #region Activity

                _carePlanNeedDomainId = commonMethodsDB.CreateCarePlanNeedDomain(_careDirectorQA_TeamId, "Activity-001", DateTime.UtcNow);


                #endregion

                #region care plan and regular care task
                _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);
                dbHelper.personCarePlan.UpdateStatus(_personCarePlanID, 2);


                #endregion


                #region Alert And Hazard Type

                _alertAndHazardsType = commonMethodsDB.CreateAlertAndHazardType(_careDirectorQA_TeamId, "Dangerous Dog", new DateTime(2019, 2, 26));

                #endregion

                #region Person Alert And Hazards Record

                personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _careDirectorQA_TeamId, "CareDirector QA", _personID, _personFullName, _alertAndHazardsType, "Dangerous Dog", null, null, DateTime.Now, null, 1);

                #endregion

                #region Allergy Type

                _alergyTypeId = commonMethodsDB.CreateAllergyType(_careDirectorQA_TeamId, "DustAllergy_Automation", new DateTime(2019, 2, 26));

                #endregion

                #region Person Allergy

                _personAllergyId = dbHelper.personAllergy.CreatePersonAllergy(_careDirectorQA_TeamId, false, _personID, _personFullName, _alergyTypeId, "DustAllergy_Automation", "allergen details", new DateTime(2020, 5, 5), null, "description information", 1);

                #endregion

                #region diagnosis

                _diagnosisid = commonMethodsDB.CreateDiagnosis(_careDirectorQA_TeamId, "Alzheimer_automation", new DateTime(2019, 2, 26), true);

                #endregion

                #region Person Diagnosis

                _personDiagnosisId = dbHelper.personDiagnosis.CreatePersonDiagnosis(_careDirectorQA_TeamId, "", _personID, 1, 1, DateTime.Today, _diagnosisid);

                #endregion

                #region Care provider staff role type

                _careProviderStaffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_careDirectorQA_TeamId, "Role-002", "1234", null, new DateTime(2020, 1, 1), null);

                #endregion

                #region Employment Contract Type - Salaried
                _employmentContractTypeid1 = commonMethodsDB.CreateEmploymentContractType(_careDirectorQA_TeamId, "Salaried", "", null, new DateTime(2020, 1, 1));

                #endregion

                #region SystemUserEmploymentContract

                var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _careProviderStaffRoleTypeid, _careDirectorQA_TeamId, _employmentContractTypeid1, 47);

                //Set the Week 1 Cycle Start Date for the system user (needed for the Availability tab to work properly)
                dbHelper.systemUser.UpdateSAWeek1CycleStartDate(_systemUserId, GetThisWeekFirstWednesday());

                //Link Booking Types with the Employment Contract created previously
                dbHelper.systemUserEmploymentContractCPBookingType.CreateSystemUserEmploymentContractCPBookingType(_systemUserEmploymentContractId, _bookingType5);

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
        [Property("JiraIssueID", "ACC-7317")]
        [Description("Verify the Alerts and Hazards in mobile screen.")]
        public void KeyRisks_VerifyAlertsNHazardsUITest001()
        {
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            var todayDate = DateTime.Today.AddHours(DateTime.Now.Hour - 1);
            var alertnhazardtext = (string)dbHelper.alertAndHazardType.GetByAlertAndHazardTypeId(_alertAndHazardsType, "name")["name"];

            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());


            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .TapResedentInformationExpandButton()
                .VerfiyKeyRiskLabel()
                .TapViewAllRisksButton();

            keyRisksPage
                .TapAlertsNHazards()
                .ValidateAlertsNHazards(true, alertnhazardtext)
                .TapCloseButton();




        }


        [Test]
        [Property("JiraIssueID", "ACC-7334")]
        [Description("Verify the AAllergies in mobile screen.")]
        public void KeyRisks_VerifyAlertsNHazardsUITest002()
        {
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            var todayDate = DateTime.Today.AddHours(DateTime.Now.Hour - 1);
            var allergyname = (string)dbHelper.allergyType.GetByAllergyTypeId(_alergyTypeId, "name")["name"];
            var allergydetails = (string)dbHelper.personAllergy.GetPersonAllergyByID(_personAllergyId, "allergendetails")["allergendetails"];

            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());


            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .TapResedentInformationExpandButton()
                .VerfiyKeyRiskLabel()
                .TapViewAllRisksButton();

            keyRisksPage
                .TapAlergies()
                .ValidateAllergy(true, allergyname)
                .ValidateAllergyDescription(true, allergydetails)
                .TapCloseButton();




        }

        [Test]
        [Property("JiraIssueID", "ACC-7341")]
        [Description("Verify the Diagnosis in mobile screen.")]
        public void KeyRisks_VerifyAlertsNHazardsUITest003()
        {
            _providerName = (string)dbHelper.provider.GetProviderByID(_providereId1, "name")["name"];
            _personFullname = (string)dbHelper.person.GetPersonById(_personID, "fullname")["fullname"];
            _personnumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
            var todayDate = DateTime.Today.AddHours(DateTime.Now.Hour - 1);
            var diagnosis = (string)dbHelper.diagnosis.GetByID(_diagnosisid, "name")["name"];

            MainMenu
              .NavigateToHomePage();

            providerSelectionPage
              .TapProviderSelectionButton()
              .SelectProvider(_providereId1.ToString())
              .SelectPersonRecord(_providerName, _personID.ToString());


            resedentDetailsPage
                .WaitForResedentDetailsPageToLoad(_personFullname, _personnumber)
                .TapResedentInformationExpandButton()
                .VerfiyKeyRiskLabel()
                .TapViewAllRisksButton();

            keyRisksPage
                .TapDiagnosis()
                .ValidateDiagnosis(true, diagnosis)
                .TapCloseButton();




        }
    }

}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.DailyCareBOs
{

    /// <summary>
    /// This class contains Automated UI test scripts for Care Diary
    /// </summary>
    [TestClass]
    public class DailyCare_Mobility_UITestCases : FunctionalTest
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
        private Guid _carePhysicalLocationId;
        private Guid _carePhysicalLocationId1;
        private Guid _mobilityDiatanceUnitId1;
        private Guid _careEquipmentId;
        private Guid _careAssistanceNeededId;
        private Guid _careWellBeingdId;




        private Guid _bookingType6;

        #endregion

        [TestInitialize()]
        public void DailyCare_Mobility_SetupTest()
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
                var defaultTeam = dbHelper.team.GetFirstTeams(1, 1, true).First();

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
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View Contact")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View Prospect")[0]);
                userSecProfiles.Add(dbHelper.securityProfile.GetSecurityProfileByName("Can View Referral")[0]);
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

                #region Team Member

                commonMethodsDB.CreateTeamMember(defaultTeam, _systemUserId, new DateTime(2000, 1, 1), null);

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

                var firstName = "Person_Mobility" + DateTime.Now.ToString("yyyyMMddHHmmss");
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

                #region care Tasks
                _careTaskid = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with bath or shower1", 001, DateTime.Now);

                _careTaskid1 = commonMethodsDB.CreateCareTask(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "Assist with dressing1", 002, DateTime.Now);

                #endregion

                #region Option Set Value

                var optionSetId = dbHelper.optionSet.GetOptionSetIdByName("Daily Care Record Business Object")[0];
                var optionSetValueId = dbHelper.optionsetValue.GetOptionSetValueIdByOptionSetId_Text(optionSetId, "Mobility")[0];
                var numericCode = (int)(dbHelper.optionsetValue.GetOptionsetValueByID(optionSetValueId, "numericcode")["numericcode"]);
                List<int> optionSetValueIds = new List<int> { numericCode };

                #endregion

                #region CarePhysicalLocation

                _carePhysicalLocationId = commonMethodsDB.CreateCarePhysicalLocation("Bathroom", commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId, optionSetValueIds);
                _carePhysicalLocationId1 = commonMethodsDB.CreateCarePhysicalLocation("Bedroom", commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId, optionSetValueIds);

                #endregion

                #region MobiltyDistanceUnit

                _mobilityDiatanceUnitId1 = commonMethodsDB.CreateMobilityDistanceUnit("Metres", commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId);

                #endregion

                #region CareEquipment

                _careEquipmentId = dbHelper.careEquipment.GetByName("Walking Frame")[0];

                #endregion

                #region CareAssistance Neede

                _careAssistanceNeededId = commonMethodsDB.CreateCareAssistanceNeeded("Asked For Help_Automation", commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId);

                #endregion

                #region CareWellBeing

                _careWellBeingdId = commonMethodsDB.CreateCareWellbeing("Happy_Automation", commonMethodsHelper.GetDatePartWithoutCulture(), _careDirectorQA_TeamId);

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

        #region https://advancedcsg.atlassian.net/browse/ACC-2752

        [TestProperty("JiraIssueID", "ACC-7010")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-7010 - " +
            "To verify the record creation and update for Mobility BO." +
            "At least one existing person record should be present.Step 1 Automated")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void Person_DailyCare_Mobility_Testmethod01()
        {
            var _personFirstName = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationId, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationId1, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];
            var todayDate = commonMethodsHelper.GetDatePartWithoutCulture();

            #region Person Care Plan Need Domain

            Guid _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetCarePlanNeedDomainIdByName("Acute").FirstOrDefault();

            #endregion

            #region Careplan

            _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType, _personID, _systemUserId, _carePlanNeedDomainId, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1), 1, 1, "test", "test1", _careDirectorQA_TeamId);

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
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personMobilityRecordPage
                .WaitForPageToLoad()
                .SelectConsentGiven("Yes")
                .InsertTextOnDateAndTimeOccurred(todayDate.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time("00:00")
                .ClickMobilisedFromLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bathroom").TapSearchButton().SelectResultElement(_carePhysicalLocationId.ToString());

            personMobilityRecordPage
               .WaitForPageToLoad()
               .SetApproximateDistance("2")
               .ClickMobilisedToLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Bedroom").TapSearchButton().SelectResultElement(_carePhysicalLocationId1.ToString());


            personMobilityRecordPage
               .WaitForPageToLoad()
               .ClickEquipmentLookUpBtn();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Walking Frame").TapSearchButton().ClickAddSelectedButton(_careEquipmentId.ToString());

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookUpBtn();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Asked For Help_Automation").TapSearchButton().SelectResultElement(_careAssistanceNeededId.ToString());

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickWellbeingLookUpBtn();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Happy_Automation").TapSearchButton().SelectResultElement(_careWellBeingdId.ToString());

            personMobilityRecordPage
                .WaitForPageToLoad()
                .SetTotalTimeSpentWithClient("1")
                .SelectAssistanceAmount("Some")
                .SelectIncludeInNextHandoverOption(false)
                .SelectFlagRecordForHandoverOption(false)
                .ClickSaveButton()
                .WaitForPageToLoad()
                .ValidateTextInCareNoteTextArea(_personFirstName, _carePhysicalLocationfrom.ToString(), _carePhysicalLocationto.ToString(), _mobilitydistanceunit.ToString(), _careEquipment.ToString(), _carewellbeing.ToString(), _careassistance.ToString(), todayDate.AddDays(-1).ToString("dd'/'MM'/'yyyy") + " 00:00:00", "Some");

        }

        [TestProperty("JiraIssueID", "ACC-7011")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-7011 - " +
            "To verify the record creation and update for Mobility BO." +
            "At least one existing person record should be present.Step 2 Automated")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Regular Care")]
        public void Person_DailyCare_Mobility_Testmethod02()
        {
            var personfirstname = (string)dbHelper.person.GetPersonById(_personID, "firstname")["firstname"];
            var _carePhysicalLocationfrom = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationId, "name")["name"];
            var _carePhysicalLocationto = dbHelper.carePhysicalLocation.GetById(_carePhysicalLocationId1, "name")["name"];
            var _careEquipment = dbHelper.careEquipment.GetById(_careEquipmentId, "name")["name"];
            var _careassistance = dbHelper.careAssistanceNeeded.GetById(_careAssistanceNeededId, "name")["name"];
            var _carewellbeing = dbHelper.careWellbeing.GetById(_careWellBeingdId, "name")["name"];
            var _mobilitydistanceunit = dbHelper.mobilityDistanceUnit.GetById(_mobilityDiatanceUnitId1, "name")["name"];
            DateTime occureddate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region create Person moilty record

            var equipmentids = new Dictionary<Guid, string>();
            equipmentids.Add(_careEquipmentId, _careEquipment.ToString());

            var systemuserinfo = new Dictionary<Guid, string>();
            systemuserinfo.Add(_systemUserId, _systemUsername);

            var _personMonilityid = dbHelper.cPPersonMobility.CreatePersonMobility(_personID, _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, occureddate.AddDays(-1), 1, _carePhysicalLocationId, _carePhysicalLocationId1, 3, _mobilityDiatanceUnitId1, equipmentids, _careAssistanceNeededId, _careWellBeingdId, systemuserinfo, 5, personfirstname + " moved from the " + _carePhysicalLocationfrom + " to the " + _carePhysicalLocationto + ", approximately 3 " + _mobilitydistanceunit + ".\r\n" +
personfirstname + " used the following equipment: " + _careEquipment + ".\r\n" +
personfirstname + " came across as " + _carewellbeing + ".\r\n" +
personfirstname + " required assistance: " + _careassistance + ". Amount given:.\r\n" +
"This care was given at " + occureddate.AddDays(-1).ToString("dd'/'MM'/'yyyy HH:mm:ss") + ".\r\n" +
personfirstname + " was assisted by 1 colleague(s).\r\n" +
"Overall, I spent 5 minutes with " + personfirstname + ".\r\n");

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
                .NavigateToPersonMobilityPage();

            var PersonMobiltyId = dbHelper.cPPersonMobility.GetByPersonId(_personID).FirstOrDefault();
            System.Threading.Thread.Sleep(1000);

            personMobilityPage
                .WaitForPageToLoad()
                .OpenRecord(PersonMobiltyId.ToString());

            personMobilityRecordPage
                .WaitForPageToLoad()
                .SetTotalTimeSpentWithClient("1")
                .ClickSaveButton()
                .ValidateTotalTimeSpentFild("1");

        }


        #endregion

    }
}

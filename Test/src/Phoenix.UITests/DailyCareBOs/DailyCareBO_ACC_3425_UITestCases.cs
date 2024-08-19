using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.DailyCareBOs
{

    /// <summary>
    /// This class contains Automated UI test scripts for Daily Care BO
    /// </summary>
    [TestClass]
    public class DailyCareBO_ACC_3425_UITestCases : FunctionalTest
    {
        #region Properties
        private string _tenantName;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _maritalStatusId;
        private Guid _systemUserId;
        private Guid _personID;
        private string _systemUsername;
        private string currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private Guid defaultTeamId;

        #endregion

        [TestInitialize()]
        public void DailyCare_MobilityBO_SetupTest()
        {

            try
            {

                #region Environment Name

                _tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper);

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                var _defaultSystemUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];
                defaultTeamId = (Guid)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "defaultteamid")["defaultteamid"];
                var _localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, _localZone.StandardName);

                #endregion

                #region Business Unit
                _businessUnitId = commonMethodsDB.CreateBusinessUnit("DailyCareBU");

                #endregion

                #region Providers

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("DailyCareTeam", null, _businessUnitId, "907678", "DailyCareTeam@careworkstempmail.com", "DailyCareTeam", "020 123456");

                #endregion

                #region Marital Status

                _maritalStatusId = commonMethodsDB.CreateMaritalStatus("Civil Partner", new DateTime(2000, 1, 1), _teamId);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Lanuage

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

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

                _systemUsername = "DailyCareUser2";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DailyCare", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);


                #region Team membership

                commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2024, 6, 1), null);

                #endregion

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-8390

        [TestProperty("JiraIssueID", "ACC-3425")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-3425 - " +
            "Verify the care notes generated for Mobility BO in web app.")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Mobility")]
        public void DailyCareBO_ACC3425_UITestMethod001()
        {
            #region Person

            var firstName = "Juan";
            var lastName = currentTimeString;
            var addresstypeid = 6; //Home

            _personID = commonMethodsDB.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
            "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");

            #endregion

            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region Care Physical Location

            var carePhysicalLocation1Id = dbHelper.carePhysicalLocation.GetByName("Bedroom")[0];
            var carePhysicalLocation2Id = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];

            #endregion

            #region CareEquipment

            var _careEquipmentId = dbHelper.careEquipment.GetByName("Walking Stick")[0];

            #endregion

            #region CareAssistance Needed

            var _careAssistanceNeededId = dbHelper.careAssistanceNeeded.GetByName("Asked For Help")[0];

            #endregion

            #region CareWellBeing

            var _careWellBeingdId = dbHelper.careWellbeing.GetByName("Happy")[0];

            #endregion

            #endregion


            #region Step 1

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false);

            #endregion

            #region Step 3, 4, 5

            personRecordPage
                .NavigateToPersonMobilityPage();

            personMobilityPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            var time = todayDate.ToUniversalTime().AddHours(-1);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .InsertTextOnDateAndTimeOccurred(todayDate.ToString("dd'/'MM'/'yyyy"))
                .InsertTextOnDateAndTimeOccurred_Time(time.ToString("HH:mm"))
                .SelectConsentGiven("Yes")
                .ClickMobilisedFromLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Bedroom", carePhysicalLocation1Id);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickMobilisedToLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Living Room", carePhysicalLocation2Id);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .SetApproximateDistance("10")
                .ClickEquipmentLookUpBtn();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Walking Stick")
                .TapSearchButton()
                .ClickAddSelectedButton(_careEquipmentId.ToString());

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ClickAssistanceNeededLookUpBtn();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Asked For Help", _careAssistanceNeededId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .SelectAssistanceAmount("Some")
                .ClickWellbeingLookUpBtn();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SearchAndSelectRecord("Happy", _careWellBeingdId);

            personMobilityRecordPage
                .WaitForPageToLoad()
                .SetTotalTimeSpentWithClient("15")
                .InsertTextInAdditionalNotesTextArea(currentTimeString + " Movement from Bedroom to Living Room improving")
                .SelectIncludeInNextHandoverOption(false)
                .SelectFlagRecordForHandoverOption(false)
                .ClickSaveButton()
                .WaitForPageToLoad();

            personMobilityRecordPage
                .WaitForPageToLoad()
                .ValidateTextInCareNoteTextArea("Juan moved from the Bedroom to the Living Room, approximately 10 Metres.\r\n" +
                "Juan used the following equipment: Walking Stick.\r\n" +
                "Juan came across as Happy.\r\n" +
                "Juan required assistance: Asked For Help. Amount given: Some.\r\n" +
                "This care was given at " + todayDate.ToString("dd'/'MM'/'yyyy") + " " + time.ToString("HH:mm:00") + ".\r\n" +
                "Juan was assisted by 1 colleague(s).\r\n" +
                "Overall, I spent 15 minutes with Juan.\r\n" +
                "We would like to note that: " + currentTimeString + " Movement from Bedroom to Living Room improving.");


            #endregion

        }


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}

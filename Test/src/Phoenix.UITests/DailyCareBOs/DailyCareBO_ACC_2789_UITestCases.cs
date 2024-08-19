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
    public class DailyCareBO_ACC_2789_UITestCases : FunctionalTest
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
        public void DailyCare_BO_SetupTest()
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

                
                var userSecProfiles = new List<Guid>()
                {
                dbHelper.securityProfile.GetSecurityProfileByName("Security Management Access")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Settings Area Access")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Person (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Daily Care (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Edit)")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("Care Provider Reference Data (Edit)")[0],

            };

                #endregion

                #region Create SystemUser 

                _systemUsername = "DailyCareUser2";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "DailyCare", "User2", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
                dbHelper.systemUser.UpdateEmployeeTypeId(_systemUserId, 3);


                #region Team membership

                commonMethodsDB.CreateTeamMember(defaultTeamId, _systemUserId, new DateTime(2024, 6, 1), null);

                #endregion

                #endregion

                #region Person

                var firstName = "Juan";
                var lastName = currentTimeString;
                var addresstypeid = 6; //Home

                _personID = commonMethodsDB.CreatePersonRecord("", firstName, "", lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, new DateTime(2020, 10, 20), addresstypeid, 1, "9876543210", "", "1234567890", "",
                "pna", "pno", "st", "dist", "tow", "cou", "CR0 3RL");

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

        [TestProperty("JiraIssueID", "ACC-8431")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2789 - " +
            "To verify record title update to the existing BO's. - Welfare Check (Person Day Night Check)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Welfare Check")]
        public void DailyCareBO_ACC2789_UITestMethod001()
        {
            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region CarePhysicalLocation

            var _carePhysicalLocationId = dbHelper.carePhysicalLocation.GetByName("Living Room")[0];

            #endregion

            #region Step 5

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToWelfareChecksPage();

            personWelfareChecksPage
                .WaitForPageToLoad(false)
                .ClickNewRecordButton();

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .SetDateOccurred(todayDate.ToString("dd'/'MM'/'yyyy"))
                .SetTimeOccurred(todayDate.ToUniversalTime().AddHours(-1).ToString("HH:mm"))
                .SelectWereTheyAsleepOrAwake("Asleep")
                .ClickIsTheResidentInASafePlace_YesRadioButton()
                .ClickLocationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Living Room")
                .TapSearchButton()
                .ClickAddSelectedButton(_carePhysicalLocationId.ToString());

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .InsertTotalTimeSpentWithPersonMinutes("10")
                .ClickSave();

                
            personWelfareCheckRecordPage
                .WaitForPageToLoad();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            var _cpPersonDayNightCheckRecordId = dbHelper.cpPersonDayNightCheck.GetByPersonId(_personID)[0];
            var _title = dbHelper.cpPersonDayNightCheck.GetById(_cpPersonDayNightCheckRecordId, "title")["title"];

            personWelfareCheckRecordPage
                .WaitForPageToLoad()
                .VerifyPageHeaderText(_title.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8432")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2789 - " +
            "To verify record title update to the existing BO's. - Repositioning (Turning)")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Repositioning")]
        public void DailyCareBO_ACC2789_UITestMethod002()
        {
            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region Step 4

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToRepositioningPage();

            personRepositioningPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .SetDateOccurred(todayDate.ToString("dd'/'MM'/'yyyy"))
                .SetTimeOccurred(todayDate.ToUniversalTime().AddHours(-1).ToString("HH:mm"))
                .SelectConsentGivenPicklistValueByText("No")
                .SelectNonConsentDetailValueByText("Absent")
                .InsertTextInReasonForAbsence("Non Consent " + currentTimeString)
                .ClickSave()
                .WaitForPageToLoad();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            var _cpPersonRepositioniongkRecordId = dbHelper.cPPersonTurning.GetByPersonId(_personID)[0];
            var _title = dbHelper.cPPersonTurning.GetById(_cpPersonRepositioniongkRecordId, "title")["title"];

            personRepositioningRecordPage
                .WaitForPageToLoad()
                .VerifyPageHeaderText(_title.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8433")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2789 - " +
            "To verify record title update to the existing BO's. - Daily Record")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Person Daily Record")]
        public void DailyCareBO_ACC2789_UITestMethod003()
        {
            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region Step 3

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToDailyRecordPage();

            personDailyRecordsPage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            personDailyRecord_RecordPage
                .WaitForPageToLoad()
                .SetDateOccurred(todayDate.ToString("dd'/'MM'/'yyyy"))
                .SetTimeOccurred(todayDate.ToUniversalTime().AddHours(-1).ToString("HH:mm"))
                .InsertTextInNotesField("Notes: " + currentTimeString)
                .ClickSave()
                .WaitForPageToLoad();

            personDailyRecord_RecordPage
                .WaitForPageToLoad();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            var _cpPersonDailyRecordId = dbHelper.cPPersonDailyRecord.GetByPersonId(_personID)[0];
            var _name = dbHelper.cPPersonDailyRecord.GetById(_cpPersonDailyRecordId, "name")["name"];

            personDailyRecord_RecordPage
                .WaitForPageToLoad()
                .VerifyPageHeaderText(_name.ToString());

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-8434")]
        [Description("Automation for the test https://advancedcsg.atlassian.net/browse/ACC-2789 - " +
            "To verify record title update to the existing BO's. - Daily Record")]
        [TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestMethod]
        [TestProperty("BusinessModule1", "Care Provider Care Plan")]
        [TestProperty("Screen1", "Continence Care")]
        public void DailyCareBO_ACC2789_UITestMethod004()
        {
            var todayDate = commonMethodsHelper.GetCurrentDateWithoutCulture();

            #region Step 3

            loginPage
               .GoToLoginPage()
               .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByLastName(currentTimeString)
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .NavigateToContinenceCarePage();

            continenceCarePage
                .WaitForPageToLoad()
                .ClickNewRecordButton();

            continenceCareRecordPage
                .WaitForPageToLoad()
                .SetDateOccurred(todayDate.ToString("dd'/'MM'/'yyyy"))
                .SetTimeOccurred(todayDate.ToUniversalTime().AddHours(-1).ToString("HH:mm"))
                .SelectConsentGivenPicklistValueByText("No")
                .SelectNonConsentDetailValueByText("Declined")
                .InsertTextInReasonConsentDeclinedTextareaField("Declined " + currentTimeString)
                .InsertTextInEncouragementGivenTextareaField("Encouragement " + currentTimeString)
                .SelectCareProvidedWithoutConsent_NoOption()
                .ClickSave()
                .WaitForPageToLoad();

            continenceCareRecordPage
                .WaitForPageToLoad();

            dbHelper = new Phoenix.DBHelper.DatabaseHelper(_tenantName);
            var _cpPersonToiletingId = dbHelper.cpPersonToileting.GetByPersonId(_personID)[0];
            var _name = dbHelper.cpPersonToileting.GetById(_cpPersonToiletingId, "name")["name"];

            continenceCareRecordPage
                .WaitForPageToLoad()
                .VerifyPageHeaderText(_name.ToString());

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

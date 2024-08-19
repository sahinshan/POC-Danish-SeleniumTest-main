using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_Timeline_UITestCases : FunctionalTest
    {

        private Guid _authenticationproviderid;
        private Guid _productLanguageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private string _systemUsername;
        private Guid _systemUserId;

        [TestInitialize()]
        public void Person_CarePlan_SetupTest()
        {

            try
            {
                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _productLanguageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("People BU5");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("People T5", null, _businessUnitId, "907678", "PeopleT5@careworkstempmail.com", "People T5", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User

                _systemUsername = "PeopleUser5";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "People", "User5", "Passw0rd_!", _businessUnitId, _teamId, _productLanguageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-5023

        [TestProperty("JiraIssueID", "CDV6-24944")]
        [Description("Login in the web application - Navigate to the People page - Open a person record - Tap on the Timeline tab - " +
            "Validate that the Timeline page is displayed with the new Team and Profession Type search fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Timeline_UITestMethod01()
        {
            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Drago", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser5", "Passw0rd_!")
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad();

        }

        [TestProperty("JiraIssueID", "CDV6-24945")]
        [Description("Login in the web application - Navigate to the People page - Open a person record - Tap on the Timeline tab - " +
            "Validate that, by default, all records used in the automation test process are displayed if no filter is applied")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Timeline_UITestMethod02()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Drago", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Note

            var caseNote01 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", "...", personID, currentDate);
            var caseNote02 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 02", "...", personID, currentDate);

            #endregion

            #region Task

            var task01 = dbHelper.task.CreatePersonTask(personID, "Drago" + currentDateTimeString, "Task 01", "...", _teamId);
            var task02 = dbHelper.task.CreatePersonTask(personID, "Drago" + currentDateTimeString, "Task 01", "...", _teamId);

            #endregion

            #region Letter

            var letter01 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 01", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            var letter02 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 02", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser5", "Passw0rd_!")
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()

                .ValidateRecordPresent(caseNote01.ToString())
                .ValidateRecordPresent(caseNote02.ToString())
                .ValidateRecordPresent(task01.ToString())
                .ValidateRecordPresent(task02.ToString())
                .ValidateRecordPresent(letter01.ToString())
                .ValidateRecordPresent(letter02.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24946")]
        [Description("Login in the web application - Navigate to the People page - Open a person record - Tap on the Timeline tab - " +
            "Select a Team record - Tap on the Apply button - Validate that only records that belong to the selected team are displayed in the Timeline")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Timeline_UITestMethod03()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Team

            var _team2Id = commonMethodsDB.CreateTeam("People T5 B", null, _businessUnitId, "907678", "PeopleT5B@careworkstempmail.com", "People T5 B", "020 123456");

            #endregion

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Drago", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Note

            var caseNote01 = dbHelper.personCaseNote.CreatePersonCaseNote(_team2Id, "Case Note 01", "...", personID, currentDate);
            var caseNote02 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 02", "...", personID, currentDate);

            #endregion

            #region Task

            var task01 = dbHelper.task.CreatePersonTask(personID, "Drago" + currentDateTimeString, "Task 01", "...", _team2Id);
            var task02 = dbHelper.task.CreatePersonTask(personID, "Drago" + currentDateTimeString, "Task 01", "...", _teamId);

            #endregion

            #region Letter

            var letter01 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 01", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            var letter02 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 02", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            var letter03 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 03", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            var letter04 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 04", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser5", "Passw0rd_!")
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ClickTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("People T5 B").SelectResultElement(_team2Id);

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ClickApplyButton()

                .ValidateRecordPresent(caseNote01.ToString())
                .ValidateRecordPresent(task01.ToString())

                .ValidateRecordNotPresent(caseNote02.ToString())
                .ValidateRecordNotPresent(task02.ToString())
                .ValidateRecordNotPresent(letter01.ToString())
                .ValidateRecordNotPresent(letter02.ToString())
                .ValidateRecordNotPresent(letter03.ToString())
                .ValidateRecordNotPresent(letter04.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-24947")]
        [Description("Login in the web application - Navigate to the People page - Open a person record - Tap on the Timeline tab - " +
            "Select a Profession Type - Tap on the Apply button - Validate that only records created by users that belong to the selected profession are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Timeline_UITestMethod04()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Profession Type

            var professionTypeId = commonMethodsDB.CreateProfessionType(_teamId, "District Nurse", new DateTime(2000, 1, 1));

            #endregion

            #region System User

            var _systemUsername2 = "PeopleUser5B";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "People", "User5B", "Passw0rd_!", _businessUnitId, _teamId, _productLanguageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateProfessionType(_systemUser2Id, professionTypeId);

            #endregion

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Drago", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Note

            var caseNote01 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", "...", personID, currentDate);

            #endregion

            #region Task

            var task01 = dbHelper.task.CreatePersonTask(personID, "Drago" + currentDateTimeString, "Task 01", "...", _teamId);

            #endregion

            #region Letter

            var letter03 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 03", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            var letter04 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 04", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            #endregion

            dbHelper = new DBHelper.DatabaseHelper("PeopleUser5B", "Passw0rd_!");
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            #region Case Note

            var caseNote02 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 02", "...", personID, currentDate);

            #endregion

            #region Task

            var task02 = dbHelper.task.CreatePersonTask(personID, "Drago" + currentDateTimeString, "Task 01", "...", _teamId);

            #endregion

            #region Letter

            var letter01 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 01", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            var letter02 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 02", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("PeopleUser5", "Passw0rd_!")
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ClickProfessionTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("District N").TapSearchButton().SelectResultElement(professionTypeId);

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ClickApplyButton()

                .ValidateRecordPresent(caseNote02.ToString())
                .ValidateRecordPresent(task02.ToString())
                .ValidateRecordPresent(letter01.ToString())
                .ValidateRecordPresent(letter02.ToString())

                .ValidateRecordNotPresent(caseNote01.ToString())
                .ValidateRecordNotPresent(task01.ToString())
                .ValidateRecordNotPresent(letter03.ToString())
                .ValidateRecordNotPresent(letter04.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24948")]
        [Description("Login in the web application - Navigate to the People page - Open a person record - Tap on the Timeline tab - " +
            "Select a Profession Type and Team - Tap on the Apply button - " +
            "Validate that only records that belong to the selected team and that were created by users that belong to the selected profession are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Timeline_UITestMethod05()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;

            #region Team

            var _team2Id = commonMethodsDB.CreateTeam("People T5 B", null, _businessUnitId, "907678", "PeopleT5B@careworkstempmail.com", "People T5 B", "020 123456");

            #endregion

            #region Profession Type

            var professionTypeId = commonMethodsDB.CreateProfessionType(_teamId, "District Nurse", new DateTime(2000, 1, 1));

            #endregion

            #region System User

            var _systemUsername2 = "PeopleUser5B";
            var _systemUser2Id = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "People", "User5B", "Passw0rd_!", _businessUnitId, _teamId, _productLanguageId, _authenticationproviderid);
            dbHelper.systemUser.UpdateProfessionType(_systemUser2Id, professionTypeId);

            #endregion

            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Drago", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Note

            var caseNote01 = dbHelper.personCaseNote.CreatePersonCaseNote(_teamId, "Case Note 01", "...", personID, currentDate);

            #endregion

            #region Task

            var task01 = dbHelper.task.CreatePersonTask(personID, "Drago" + currentDateTimeString, "Task 01", "...", _teamId);

            #endregion

            #region Letter

            var letter03 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 03", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            var letter04 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 04", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            #endregion

            dbHelper = new DBHelper.DatabaseHelper("PeopleUser5B", "Passw0rd_!");
            commonMethodsDB = new CommonMethodsDB(dbHelper);

            #region Case Note

            var caseNote02 = dbHelper.personCaseNote.CreatePersonCaseNote(_team2Id, "Case Note 02", "...", personID, currentDate);

            #endregion

            #region Task

            var task02 = dbHelper.task.CreatePersonTask(personID, "Drago" + currentDateTimeString, "Task 01", "...", _team2Id);

            #endregion

            #region Letter

            var letter01 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 01", "...", null, _team2Id, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            var letter02 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), "Drago" + currentDateTimeString, "person", 1, "1",
                "Letter 02", "...", null, _teamId, _systemUserId,
                null, null, null, null, null, personID, null, personID, "Drago" + currentDateTimeString, "person",
                false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser5", "Passw0rd_!")
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ClickProfessionTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("District N").TapSearchButton().SelectResultElement(professionTypeId);

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ClickTeamLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("People T5 B").SelectResultElement(_team2Id);

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ClickApplyButton()

                .ValidateRecordPresent(caseNote02.ToString())
                .ValidateRecordPresent(task02.ToString())
                .ValidateRecordPresent(letter01.ToString())

                .ValidateRecordNotPresent(letter03.ToString())
                .ValidateRecordNotPresent(letter04.ToString())
                .ValidateRecordNotPresent(caseNote01.ToString())
                .ValidateRecordNotPresent(task01.ToString())
                .ValidateRecordNotPresent(letter02.ToString());
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-5283

        [TestProperty("JiraIssueID", "CDV6-24949")]
        [Description("Login in the web application - Navigate to the People page - Open a person record - Tap on the Timeline tab - " +
            "Validate that the Timeline page is displayed with the search elements on the left side and the results elements in the right side")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Timeline_UITestMethod06()
        {
            #region Person

            var currentDateTimeString = commonMethodsHelper.GetCurrentDateTimeString();
            var personID = dbHelper.person.CreatePersonRecord("", "Drago", "", currentDateTimeString, "", new DateTime(2000, 1, 2), _ethnicityId, _teamId, 7, 2);
            var personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("PeopleUser5", "Passw0rd_!")
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapTimeLineTab();

            personTimelineSubPage
                .WaitForPersonTimelineSubPageToLoad()
                .ValidateSearchAreaVisibleInLeftSide()
                .ValidateSearchAreaVisibleInRightSide();

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        //[TestCleanup]
        //public void ClassCleanUp()
        //{
        //    string jiraIssueID = (string)this.TestContext.Properties["JiraIssueID"];

        //    //if we have a jira id for the test then we will update its status in jira
        //    if (jiraIssueID != null)
        //    {
        //        bool testPassed = this.TestContext.CurrentTestOutcome == UnitTestOutcome.Passed;


        //        var zapi = new AtlassianServiceAPI.Models.Zapi()
        //        {
        //            AccessKey = ConfigurationManager.AppSettings["AccessKey"],
        //            SecretKey = ConfigurationManager.AppSettings["SecretKey"],
        //            User = ConfigurationManager.AppSettings["User"],
        //        };

        //        var jiraAPI = new AtlassianServiceAPI.Models.JiraApi()
        //        {
        //            Authentication = ConfigurationManager.AppSettings["Authentication"],
        //            JiraCloudUrl = ConfigurationManager.AppSettings["JiraCloudUrl"],
        //            ProjectKey = ConfigurationManager.AppSettings["ProjectKey"]
        //        };

        //        AtlassianServicesAPI.AtlassianService atlassianService = new AtlassianServicesAPI.AtlassianService(zapi, jiraAPI);

        //        string versionName = ConfigurationManager.AppSettings["CurrentVersionName"];

        //        if(testPassed)
        //            atlassianService.UpdateTestStatus(jiraIssueID, versionName, AtlassianServiceAPI.Models.JiraTestOutcome.Passed);
        //        else
        //            atlassianService.UpdateTestStatus(jiraIssueID, versionName, AtlassianServiceAPI.Models.JiraTestOutcome.Failed);


        //    }

        //}

    }
}

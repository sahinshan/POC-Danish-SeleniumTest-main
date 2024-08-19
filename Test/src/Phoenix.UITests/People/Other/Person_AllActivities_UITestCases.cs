using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for All Activities
    /// </summary>
    [TestClass]
    public class Person_AllActivities_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid userid;
        private Guid securitytestuserid;
        private Guid _systemUserId;
        private string _systemUsername;
        private String _defaultUserFullname;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");

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

                string decodedUsername = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(decodedUsername)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);
                _defaultUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User AllActivitiesUser1

                _systemUserId = commonMethodsDB.CreateSystemUserRecord("AllActivitiesUser1", "AllActivities", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region System User securitytestuseradmin

                userid = commonMethodsDB.CreateSystemUserRecord("SecurityTestUserAdmin", "Security", "Test User Admin", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region System User securitytestuser

                securitytestuserid = commonMethodsDB.CreateSystemUserRecord("SecurityTestUser", "Security", "Test User", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-3731

        [TestProperty("JiraIssueID", "CDV6-11813")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select all activity records - Tap on the Print Selected button - " +
            "Validate that a docx file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod01()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .TapSelectAllActivitiesCheckBox()
                .TapPrintSelectedButton();

            System.Threading.Thread.Sleep(2000);
            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "AllActivities.docx");
            Assert.IsTrue(fileExists);
        }

        [TestProperty("JiraIssueID", "CDV6-11814")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = PDF" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select all activity records - Tap on the Print Selected button - " +
            "Validate that no docx or pdf file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod02()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "PDF");

            #region Person

            var _firstName = "Jeremy";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .TapSelectAllActivitiesCheckBox()
                .TapPrintSelectedButton();

            System.Threading.Thread.Sleep(2000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.pdf");
            Assert.IsTrue(fileExists); //the browser behaviour may automatically download the PDF file instead of displaying it
        }

        [TestProperty("JiraIssueID", "CDV6-11815")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select all activity records - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains info regarding all available activities")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod03()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .TapSelectAllActivitiesCheckBox()
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";

            msWordHelper.ValidateWordsPresent(filePath, "Appointment 01", "Case Note 01", "Email 01", "Letter 01", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11816")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Tap on the Print All button - " +
            "Validate that the downloaded docx file contains info regarding all available activities")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod04()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .TapPrintAllButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Appointment 01", "Case Note 01", "Email 01", "Letter 01", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11817")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select one appointment record - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod05()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            Guid appointmentID1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(appointmentID1.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Appointment 01");
            msWordHelper.ValidateWordsNotPresent(filePath, "Case Note 01", "Email 01", "Letter 01", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11818")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select two appointment record - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod06()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            Guid appointmentID1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid appointmentID2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(appointmentID1.ToString())
                .SelectActivityRecord(appointmentID2.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Appointment 01", "Appointment 02");
            msWordHelper.ValidateWordsNotPresent(filePath, "Case Note 01", "Email 01", "Letter 01", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11819")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select one case note record - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod07()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid caseNoteID1 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(caseNoteID1.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Case Note 01");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Email 01", "Letter 01", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11820")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select two appointment records - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod08()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid caseNoteID1 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            Guid caseNoteID2 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(caseNoteID1.ToString())
                .SelectActivityRecord(caseNoteID2.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Case Note 01", "Case Note 02");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Email 01", "Letter 01", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11821")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select one email record - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod09()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            Guid emailRecordID1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(emailRecordID1.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Email 01");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Case Note 01", "Case Note 02", "Letter 01", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11822")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select two email records - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod10()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            Guid emailRecordID1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            Guid emailRecordID2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(emailRecordID1.ToString())
                .SelectActivityRecord(emailRecordID2.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Email 01", "Email 02");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Case Note 01", "Case Note 02", "Letter 01", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11823")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select one letter record - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod11()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            Guid letterRecordID1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(letterRecordID1.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Letter 01");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Case Note 01", "Case Note 02", "Email 01", "Email 02", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11824")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select two letter records - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod12()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            Guid letterRecordID1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            Guid letterRecordID2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(letterRecordID1.ToString())
                .SelectActivityRecord(letterRecordID2.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Letter 01", "Letter 02");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Case Note 01", "Case Note 02", "Email 01", "Email 02", "Phone Call 01", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11825")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select one phone call record - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod13()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(phoneCallID1.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Phone Call 01");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Case Note 01", "Case Note 02", "Email 01", "Email 02", "Letter 01", "Letter 02", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11826")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select two phone call records - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod14()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid phoneCallID2 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 02", "Notes 02", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(phoneCallID1.ToString())
                .SelectActivityRecord(phoneCallID2.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Phone Call 01", "Phone Call 02");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Case Note 01", "Case Note 02", "Email 01", "Email 02", "Letter 01", "Letter 02", "Task 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11827")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select one task record - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod15()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 02", "Notes 02", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid taskID1 = dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(taskID1.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Task 01");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Case Note 01", "Case Note 02", "Email 01", "Email 02", "Letter 01", "Letter 02", "Phone Call 01", "Phone Call 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11828")]
        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3731 - " +
            "AllActivities.PrintFormat = Word" +
            "Open Person Record -> Navigate to the All Activities section - clear all filters - Select two task records - Tap on the Print Selected button - " +
            "Validate that the downloaded docx file contains only the info for the selected records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod16()
        {
            Guid systemSettingID1 = commonMethodsDB.CreateSystemSetting("AllActivities.PrintFormat", "Word", "Describe print format for all activities. Valid values PDF or Word", false, "");
            dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID1, "Word");

            #region Person

            var _firstName = "Mariana";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 02", "Notes 02", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid taskID1 = dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");
            Guid taskID2 = dbHelper.task.CreateTask("Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 21, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton()
                .SelectActivityRecord(taskID1.ToString())
                .SelectActivityRecord(taskID2.ToString())
                .TapPrintSelectedButton();

            string filePath = this.DownloadsDirectory + "\\AllActivities.docx";
            msWordHelper.ValidateWordsPresent(filePath, "Task 01", "Task 02");
            msWordHelper.ValidateWordsNotPresent(filePath, "Appointment 01", "Appointment 02", "Case Note 01", "Case Note 02", "Email 01", "Email 02", "Letter 01", "Letter 02", "Phone Call 01", "Phone Call 02");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-4187

        [TestProperty("JiraIssueID", "CDV6-4190")]
        [Description("Bug Fix  - https://advancedcsg.atlassian.net/browse/CDV6-4187" +
            "User and Team do not have security profile for activities - " +
            "Login with user - open a person record (with all activity types) - navigate to All Activities area - tap on the Clear Filters button - Tap on the Search Button - " +
            "Validate that no error message is displayed - Validate that no record is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_CDV6_4187_UITestMethod01()
        {
            #region Person

            var _firstName = "Bernardina";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);
            #endregion

            #region Activities
            Guid appointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid appointment2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid caseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            Guid caseNote2 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            Guid email1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            Guid email2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            Guid letter1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            Guid letter2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            Guid phoneCall1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid phoneCall2 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 02", "Notes 02", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid task1 = dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");
            Guid task2 = dbHelper.task.CreateTask("Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 21, 7, 0, 0), personID, _personFullName, "person");

            #endregion

            //remove all security profiles for the user 
            foreach (Guid recordID in this.dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(securitytestuserid))
                this.dbHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in dbHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(_careDirectorQA_TeamId))
                dbHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Org Edit)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(securitytestuserid, securityProfileId);


            loginPage
                .GoToLoginPage()
                .Login("securitytestuser", "Passw0rd_!")
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton();

            dynamicDialogPopup.ValidateDynamicDialogPopupNotDisplayed();

            personAllActivitiesSubPage
                .ValidateRecordNotPresent(appointment2.ToString())
                .ValidateRecordNotPresent(appointment1.ToString())

                .ValidateRecordNotPresent(caseNote1.ToString())
                .ValidateRecordNotPresent(caseNote2.ToString())

                .ValidateRecordNotPresent(email1.ToString())
                .ValidateRecordNotPresent(email2.ToString())

                .ValidateRecordNotPresent(letter1.ToString())
                .ValidateRecordNotPresent(letter2.ToString())

                .ValidateRecordNotPresent(phoneCall1.ToString())
                .ValidateRecordNotPresent(phoneCall2.ToString())

                .ValidateRecordNotPresent(task1.ToString())
                .ValidateRecordNotPresent(task2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-4191")]
        [Description("Bug Fix  - https://advancedcsg.atlassian.net/browse/CDV6-4187" +
            "User have security profile for activities - CW Activities (Org View) - " +
            "Login with user - open a person record (with all activity types) - navigate to All Activities area - tap on the Clear Filters button - Tap on the Search Button - " +
            "Validate that no error message is displayed - Validate that all activity records for the person are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_CDV6_4187_UITestMethod02()
        {
            #region Person

            var _firstName = "Bernardina";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);
            #endregion

            #region Activities
            Guid appointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid appointment2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid caseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            Guid caseNote2 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            Guid email1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            Guid email2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            Guid letter1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            Guid letter2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            Guid phoneCall1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid phoneCall2 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 02", "Notes 02", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid task1 = dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");
            Guid task2 = dbHelper.task.CreateTask("Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 21, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            //remove all security profiles for the user 
            foreach (Guid recordID in this.dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(securitytestuserid))
                this.dbHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in dbHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(_careDirectorQA_TeamId))
                dbHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Activities (Org View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(securitytestuserid, securityProfileId);

            loginPage
                .GoToLoginPage()
                .Login("securitytestuser", "Passw0rd_!")
                .WaitFormHomePageToLoad(true, false, true);

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton();

            dynamicDialogPopup.ValidateDynamicDialogPopupNotDisplayed();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRecordPresent(appointment2.ToString(), "Appointment 02")
                .ValidateRecordPresent(appointment1.ToString(), "Appointment 01");

            personAllActivitiesSubPage
                .ValidateRecordPresent(caseNote1.ToString(), "Case Note 01")
                .ValidateRecordPresent(caseNote2.ToString(), "Case Note 02");

            personAllActivitiesSubPage
                .ValidateRecordPresent(email1.ToString(), "Email 01")
                .ValidateRecordPresent(email2.ToString(), "Email 02");

            personAllActivitiesSubPage
                .ValidateRecordPresent(letter1.ToString(), "Letter 01")
                .ValidateRecordPresent(letter2.ToString(), "Letter 02");

            personAllActivitiesSubPage
                .ValidateRecordPresent(phoneCall1.ToString(), "Phone Call 01")
                .ValidateRecordPresent(phoneCall2.ToString(), "Phone Call 02");

            personAllActivitiesSubPage
                .ValidateRecordPresent(task1.ToString(), "Task 01")
                .ValidateRecordPresent(task2.ToString(), "Task 02");
        }

        [TestProperty("JiraIssueID", "CDV6-4192")]
        [Description("Bug Fix  - https://advancedcsg.atlassian.net/browse/CDV6-4187" +
            "User Team have security profile for activities - CW Activities (Org View) - " +
            "Login with user - open a person record (with all activity types) - navigate to All Activities area - tap on the Clear Filters button - Tap on the Search Button - " +
            "Validate that no error message is displayed - Validate that all activity records for the person are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_CDV6_4187_UITestMethod03()
        {
            #region Person

            var _firstName = "Bernardina";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);
            #endregion

            #region Activities
            Guid appointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid appointment2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            Guid caseNote1 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            Guid caseNote2 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            Guid email1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            Guid email2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            Guid letter1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            Guid letter2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            Guid phoneCall1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid phoneCall2 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 02", "Notes 02", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid task1 = dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");
            Guid task2 = dbHelper.task.CreateTask("Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 21, 7, 0, 0), personID, _personFullName, "person");

            #endregion

            //remove all security profiles for the user 
            foreach (Guid recordID in this.dbHelper.userSecurityProfile.GetUserSecurityProfileByUserID(securitytestuserid))
                this.dbHelper.userSecurityProfile.DeleteUserSecurityProfile(recordID);

            //remove all security profiles for the team 
            foreach (Guid recordID in dbHelper.teamSecurityProfile.GetTeamSecurityProfileByTeamID(_careDirectorQA_TeamId))
                dbHelper.teamSecurityProfile.DeleteTeamSecurityProfile(recordID);

            //get the security profile ids
            List<Guid> securityProfilesToAdd = new List<Guid>();
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Care Cloud User")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Core Reference Data (Org View)")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person (BU Edit)")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Person Module (Org Edit)")[0]);
            securityProfilesToAdd.Add(dbHelper.securityProfile.GetSecurityProfileByName("Activities (Org View)")[0]);

            //add the security profiles to the user
            foreach (Guid securityProfileId in securityProfilesToAdd)
                dbHelper.teamSecurityProfile.CreateTeamSecurityProfile(_careDirectorQA_TeamId, securityProfileId);



            loginPage
                .GoToLoginPage()
                .Login("securitytestuser", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false, false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapClearFiltersButton()
                .TapSearchButton();

            dynamicDialogPopup.ValidateDynamicDialogPopupNotDisplayed();

            personAllActivitiesSubPage
                .ValidateRecordPresent(appointment2.ToString(), "Appointment 02")
                .ValidateRecordPresent(appointment1.ToString(), "Appointment 01")

                .ValidateRecordPresent(caseNote1.ToString(), "Case Note 01")
                .ValidateRecordPresent(caseNote2.ToString(), "Case Note 02")

                .ValidateRecordPresent(email1.ToString(), "Email 01")
                .ValidateRecordPresent(email2.ToString(), "Email 02")

                .ValidateRecordPresent(letter1.ToString(), "Letter 01")
                .ValidateRecordPresent(letter2.ToString(), "Letter 02")

                .ValidateRecordPresent(phoneCall1.ToString(), "Phone Call 01")
                .ValidateRecordPresent(phoneCall2.ToString(), "Phone Call 02")

                .ValidateRecordPresent(task1.ToString(), "Task 01")
                .ValidateRecordPresent(task2.ToString(), "Task 02");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-4878

        [TestProperty("JiraIssueID", "CDV6-11829")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Validate that the All Activities Screen page is displayed (with no results by default)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod17()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 02", "Notes 02", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");
            dbHelper.task.CreateTask("Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 21, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateNoRecordsMessageVisible()
                .ValidateNoSearchPerformedMessageVisible();


        }

        [TestProperty("JiraIssueID", "CDV6-11830")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set a Date From and Date To that should not match any activity record - Tap on the search button - " +
            "Validate that no record is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod18()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 01", "Note01", null, new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Appointment 02", "Note02", null, new DateTime(2020, 7, 21), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 21), new TimeSpan(11, 35, 0),
                personID, "person", _personFullName, 4, 5, false, null, null, null);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 01", "Note 01", personID, new DateTime(2020, 7, 20), _careDirectorQA_TeamId);
            dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Case Note 02", "Note 02", personID, new DateTime(2020, 7, 21), _careDirectorQA_TeamId);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 01", "Note 01", 2);
            dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Email 02", "Note 02", 2);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 02", "Notes 02", personID, "person", _personFullName,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            dbHelper.task.CreateTask("Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");
            dbHelper.task.CreateTask("Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 21, 7, 0, 0), personID, _personFullName, "person");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("02/09/2020")
                .InsertToDate("04/09/2020")
                .TapSearchButton()

                .ValidateRegardingHeaderText("Regarding")
                .ValidateSubjectHeaderText("Subject")
                .ValidateActivityHeaderText("Activity")
                .ValidateStatusHeaderText("Status")
                .ValidateDueDateHeaderText("Start/Due Date")
                .ValidateActualEndHeaderText("Actual End")
                .ValidateCaseNoteHeaderText("Case Note")
                .ValidateRegardingTypeHeaderText("Regarding Type")
                .ValidateResponsibleUserHeaderText("Responsible User");


        }



        [TestProperty("JiraIssueID", "CDV6-11831")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Case Notes (For Cases) records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod19()
        {

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Case and Case Note

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            Guid caseNote1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.caseCaseNote.GetByID(caseNote1, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .ValidateRegardingCellText(caseNote1.ToString(), _caseTitle)
                .ValidateSubjectCellText(caseNote1.ToString(), "Case 01 Case Note 01")
                .ValidateActivityCellText(caseNote1.ToString(), "Case Note (For Case)")
                .ValidateStatusCellText(caseNote1.ToString(), "Open")
                .ValidateDueDateCellText(caseNote1.ToString(), "01/09/2020 00:00:00")
                .ValidateActualEndCellText(caseNote1.ToString(), "")
                .ValidateCaseNoteCellText(caseNote1.ToString(), "Yes")
                .ValidateRegardingTypeCellText(caseNote1.ToString(), "Case")
                .ValidateResponsibleUserCellText(caseNote1.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(caseNote1.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(caseNote1.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(caseNote1.ToString(), modifiedon)
                .ValidateCreatedByCellText(caseNote1.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(caseNote1.ToString(), createdon);


        }

        [TestProperty("JiraIssueID", "CDV6-11832")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Case Note (For Case) records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod20()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Case and Case Note

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            Guid caseNote1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId1, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 1));
            Guid caseNote2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 02", "Note 02", _caseId1, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 2));

            Guid _caseId2 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 3, 8, 0, 0), new DateTime(2020, 9, 3, 9, 0, 0), 20);
            Guid case2Note1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 02 Case Note 01", "Note 01", _caseId2, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 1));
            Guid case2Note2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 02 Case Note 02", "Note 02", _caseId2, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            personAllActivitiesSubPage
                .ValidateRecordPresent(caseNote1.ToString(), "Case 01 Case Note 01")
                .ValidateRecordPresent(caseNote2.ToString(), "Case 01 Case Note 02")
                .ValidateRecordPresent(case2Note1.ToString(), "Case 02 Case Note 01")
                .ValidateRecordPresent(case2Note2.ToString(), "Case 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11833")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Email records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod21()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Case and Case Email

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId1, "title")["title"];
            Guid caseEmail1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "AllActivities User1", "systemuser", _caseId1, "case",
                 _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 1, 12, 0, 0));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.email.GetEmailByID(caseEmail1, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(caseEmail1.ToString(), _caseTitle)
                .ValidateSubjectCellText(caseEmail1.ToString(), "Case Email 01")
                .ValidateActivityCellText(caseEmail1.ToString(), "Email")
                .ValidateStatusCellText(caseEmail1.ToString(), "Sent")
                .ValidateDueDateCellText(caseEmail1.ToString(), "01/09/2020 12:00:00")
                .ValidateActualEndCellText(caseEmail1.ToString(), "")
                .ValidateCaseNoteCellText(caseEmail1.ToString(), "No")
                .ValidateRegardingTypeCellText(caseEmail1.ToString(), "Case")
                .ValidateResponsibleUserCellText(caseEmail1.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(caseEmail1.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(caseEmail1.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(caseEmail1.ToString(), modifiedon)
                .ValidateCreatedByCellText(caseEmail1.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(caseEmail1.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11834")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Email records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod22()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            Guid personEmailRecordID1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Person Email 01", "Note 01", 2);
            Guid personEmailRecordID2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person",
                 _personFullName, "Person Email 02", "Note 02", 2);

            #endregion

            #region Case and Case Email

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId1, "title")["title"];
            Guid caseEmail1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "AllActivities User1", "systemuser", _caseId1, "case",
                 _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid caseEmail2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "AllActivities User1", "systemuser", _caseId1, "case",
                 _caseTitle, "Case Email 02", "Note 02", 2, new DateTime(2020, 9, 1, 13, 0, 0));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(caseEmail1.ToString(), "Case Email 01")
                .ValidateRecordPresent(caseEmail2.ToString(), "Case Email 02")
                .ValidateRecordPresent(personEmailRecordID1.ToString(), "Person Email 01")
                .ValidateRecordPresent(personEmailRecordID2.ToString(), "Person Email 02");


        }

        [TestProperty("JiraIssueID", "CDV6-11835")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Letter records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod23()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Case and Case Letter

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId1, "title")["title"];
            Guid caseLetter = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Case Letter 01", "Notes 01",
                _caseId1, _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, personID, new DateTime(2020, 7, 20), _caseId1, _caseTitle,
                "case", false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.letter.GetLetterByID(caseLetter, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(caseLetter.ToString(), _caseTitle)
                .ValidateSubjectCellText(caseLetter.ToString(), "Case Letter 01")
                .ValidateActivityCellText(caseLetter.ToString(), "Letter")
                .ValidateStatusCellText(caseLetter.ToString(), "In Progress")
                .ValidateDueDateCellText(caseLetter.ToString(), "20/07/2020 00:00:00")
                .ValidateActualEndCellText(caseLetter.ToString(), "")
                .ValidateCaseNoteCellText(caseLetter.ToString(), "No")
                .ValidateRegardingTypeCellText(caseLetter.ToString(), "Case")
                .ValidateResponsibleUserCellText(caseLetter.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(caseLetter.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(caseLetter.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(caseLetter.ToString(), modifiedon)
                .ValidateCreatedByCellText(caseLetter.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(caseLetter.ToString(), createdon);


        }

        [TestProperty("JiraIssueID", "CDV6-11836")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Letter records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod24()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            Guid personLetter1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Person Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 20), personID, _personFullName,
                "person", false, null, null, null);
            Guid personLetter2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Person Letter 02", "Notes 02",
                            null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 7, 21), personID, _personFullName,
                            "person", false, null, null, null);

            #endregion

            #region Case and Case Letter

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId1, "title")["title"];
            Guid caseLetter1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Case Letter 01", "Notes 01",
                _caseId1, _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, personID, new DateTime(2020, 7, 20), _caseId1, _caseTitle,
                "case", false, null, null, null);
            Guid caseLetter2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullName, "person", 1, "1", "Case Letter 02", "Notes 02",
                _caseId1, _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, personID, new DateTime(2020, 7, 21), _caseId1, _caseTitle,
                "case", false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(caseLetter1.ToString(), "Case Letter 01")
                .ValidateRecordPresent(caseLetter2.ToString(), "Case Letter 02")
                .ValidateRecordPresent(personLetter1.ToString(), "Person Letter 01")
                .ValidateRecordPresent(personLetter2.ToString(), "Person Letter 02");


        }

        [TestProperty("JiraIssueID", "CDV6-11837")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Phone Call records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod25()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Case and Phone Call

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId1, "title")["title"];
            //Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Notes 01", personID, "person", _personFullName,
            //                userid, "systemuser", "Security Test User Admin", "", personID, _personFullName, _careDirectorQA_TeamId, "person");

            Guid phoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 01", "Notes 01", personID, "person", _personFullName,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId1, _caseTitle, personID, _personFullName,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 1), _systemUserId, "AllActivities User1");
            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.phoneCall.GetPhoneCallByID(phoneCallID1, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(phoneCallID1.ToString(), _caseTitle)
                .ValidateSubjectCellText(phoneCallID1.ToString(), "Case Phone Call 01")
                .ValidateActivityCellText(phoneCallID1.ToString(), "Phone Call")
                .ValidateStatusCellText(phoneCallID1.ToString(), "In Progress")
                .ValidateDueDateCellText(phoneCallID1.ToString(), "01/09/2020 00:00:00")
                .ValidateActualEndCellText(phoneCallID1.ToString(), "")
                .ValidateCaseNoteCellText(phoneCallID1.ToString(), "No")
                .ValidateRegardingTypeCellText(phoneCallID1.ToString(), "Case")
                .ValidateResponsibleUserCellText(phoneCallID1.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(phoneCallID1.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(phoneCallID1.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(phoneCallID1.ToString(), modifiedon)
                .ValidateCreatedByCellText(phoneCallID1.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(phoneCallID1.ToString(), createdon);

        }

        [TestProperty("JiraIssueID", "CDV6-11838")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Phone Call records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod26()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            Guid personPhoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Person Phone Call 01", "Notes 01", personID, "person", _personFullName,
                            _systemUserId, "systemuser", "AllActivities User1", "", personID, _personFullName, _careDirectorQA_TeamId, "person");
            Guid personPhoneCallID2 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Person Phone Call 02", "Notes 02", personID, "person", _personFullName,
                            _systemUserId, "systemuser", "AllActivities User1", "", personID, _personFullName, _careDirectorQA_TeamId, "person");

            #endregion

            #region Case and Phone Call

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId1, "title")["title"];
            Guid casePhoneCallID1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 01", "Notes 01", personID, "person", _personFullName,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId1, _caseTitle, personID, _personFullName,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 1), _systemUserId, "AllActivities User1");
            Guid casePhoneCallID2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 02", "Notes 02", personID, "person", _personFullName,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId1, _caseTitle, personID, _personFullName,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 2), _systemUserId, "AllActivities User1");
            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(casePhoneCallID1.ToString(), "Case Phone Call 01")
                .ValidateRecordPresent(casePhoneCallID2.ToString(), "Case Phone Call 02")
                .ValidateRecordPresent(personPhoneCallID1.ToString(), "Person Phone Call 01")
                .ValidateRecordPresent(personPhoneCallID2.ToString(), "Person Phone Call 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11839")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Task records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod27()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Case and Task

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId1, "title")["title"];
            Guid caseTaskRecordId = dbHelper.task.CreateTask("Case Task 01", "Note 01", _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, _caseId1,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId1, _caseTitle, "case");
            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.task.GetTaskByID(caseTaskRecordId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(caseTaskRecordId.ToString(), _caseTitle)
                .ValidateSubjectCellText(caseTaskRecordId.ToString(), "Case Task 01")
                .ValidateActivityCellText(caseTaskRecordId.ToString(), "Task")
                .ValidateStatusCellText(caseTaskRecordId.ToString(), "Open")
                .ValidateDueDateCellText(caseTaskRecordId.ToString(), "20/07/2020 07:00:00")
                .ValidateActualEndCellText(caseTaskRecordId.ToString(), "")
                .ValidateCaseNoteCellText(caseTaskRecordId.ToString(), "No")
                .ValidateRegardingTypeCellText(caseTaskRecordId.ToString(), "Case")
                .ValidateResponsibleUserCellText(caseTaskRecordId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(caseTaskRecordId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(caseTaskRecordId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(caseTaskRecordId.ToString(), modifiedon)
                .ValidateCreatedByCellText(caseTaskRecordId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(caseTaskRecordId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11840")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Task records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod28()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            Guid personTaskId1 = dbHelper.task.CreateTask("Person Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullName, "person");
            Guid personTaskId2 = dbHelper.task.CreateTask("Person Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 21, 7, 0, 0), personID, _personFullName, "person");

            #endregion

            #region Case and Task

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId1, "title")["title"];
            Guid caseTaskId1 = dbHelper.task.CreateTask("Case Task 01", "Note 01", _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, _caseId1,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId1, _caseTitle, "case");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Note 02", _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, _caseId1,
                personID, new DateTime(2020, 7, 21, 7, 0, 0), _caseId1, _caseTitle, "case");
            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(caseTaskId1.ToString(), "Case Task 01")
                .ValidateRecordPresent(caseTaskId2.ToString(), "Case Task 02")
                .ValidateRecordPresent(personTaskId1.ToString(), "Person Task 01")
                .ValidateRecordPresent(personTaskId2.ToString(), "Person Task 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11841")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Contact Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod29()
        {
            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Contact Type

            Guid _contactTypeId = commonMethodsDB.CreateContactType(_careDirectorQA_TeamId, "Contact Centre", DateTime.Now.Date.AddYears(-1), true);

            #endregion

            #region Presenting Priority

            Guid _presentingPriorityId = commonMethodsDB.CreateContactPresentingPriority(_careDirectorQA_TeamId, "Routine");

            #endregion


            #region Contact Status

            Guid _contactStatus = commonMethodsDB.CreateContactStatus(_careDirectorQA_TeamId, "New Contact", "", new DateTime(2019, 5, 14), 1, true);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            Guid contactId = dbHelper.contact.CreateContact(_careDirectorQA_TeamId, personID, _contactTypeId, _contactReasonId, _presentingPriorityId,
                _contactStatus, _systemUserId, personID, "person", _personFullName, new DateTime(2020, 9, 1), "Need1", 2, 2);
            //string contactTitle = (string)dbHelper.contact.GetByID(contactId, "title")["title"];
            Guid recordID = dbHelper.contactCaseNote.CreateContactCaseNote(_careDirectorQA_TeamId, _systemUserId, "Contact 01 Case Note 01",
                "Note 01", contactId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("AllActivitiesUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.contactCaseNote.GetByID(recordID, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                //.ValidateRegardingCellText(recordID.ToString(), contactTitle)
                .ValidateSubjectCellText(recordID.ToString(), "Contact 01 Case Note 01")
                .ValidateActivityCellText(recordID.ToString(), "Contact Case Note")
                .ValidateStatusCellText(recordID.ToString(), "Open")
                .ValidateDueDateCellText(recordID.ToString(), "01/09/2020 00:00:00")
                .ValidateActualEndCellText(recordID.ToString(), "")
                .ValidateCaseNoteCellText(recordID.ToString(), "Yes")
                .ValidateRegardingTypeCellText(recordID.ToString(), "Contact")
                .ValidateResponsibleUserCellText(recordID.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(recordID.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(recordID.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(recordID.ToString(), modifiedon)
                .ValidateCreatedByCellText(recordID.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(recordID.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11842")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Contact Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod30()
        {
            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Contact Type

            Guid _contactTypeId = commonMethodsDB.CreateContactType(_careDirectorQA_TeamId, "Contact Centre", DateTime.Now.Date.AddYears(-1), true);

            #endregion

            #region Presenting Priority

            Guid _presentingPriorityId = commonMethodsDB.CreateContactPresentingPriority(_careDirectorQA_TeamId, "Routine");

            #endregion


            #region Contact Status

            Guid _contactStatus = commonMethodsDB.CreateContactStatus(_careDirectorQA_TeamId, "New Contact", "", new DateTime(2019, 5, 14), 1, true);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            #region Contact & Contact Case Note

            Guid contactId = dbHelper.contact.CreateContact(_careDirectorQA_TeamId, personID, _contactTypeId, _contactReasonId, _presentingPriorityId,
                                                            _contactStatus, _systemUserId, personID, "person", _personFullName, new DateTime(2020, 9, 1), "Need1", 2, 2);

            Guid contactCaseNoteId1 = dbHelper.contactCaseNote.CreateContactCaseNote(_careDirectorQA_TeamId, _systemUserId, "Contact 01 Case Note 01",
                            "Contact 01 Case Note 01", contactId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));

            Guid contactCaseNoteId2 = dbHelper.contactCaseNote.CreateContactCaseNote(_careDirectorQA_TeamId, _systemUserId, "Contact 01 Case Note 02",
                            "Contact 01 Case Note 02", contactId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));

            Guid contactCaseNoteId3 = dbHelper.contactCaseNote.CreateContactCaseNote(_careDirectorQA_TeamId, _systemUserId, "Contact 02 Case Note 01",
                            "Contact 02 Case Note 01", contactId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));

            Guid contactCaseNoteId4 = dbHelper.contactCaseNote.CreateContactCaseNote(_careDirectorQA_TeamId, _systemUserId, "Contact 02 Case Note 02",
                            "Contact 02 Case Note 02", contactId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(contactCaseNoteId1.ToString(), "Contact 01 Case Note 01")
                .ValidateRecordPresent(contactCaseNoteId2.ToString(), "Contact 01 Case Note 02")
                .ValidateRecordPresent(contactCaseNoteId3.ToString(), "Contact 02 Case Note 01")
                .ValidateRecordPresent(contactCaseNoteId4.ToString(), "Contact 02 Case Note 02");

        }

        [TestProperty("JiraIssueID", "CDV6-11843")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Court Dates and Outcomes Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod31()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);
            #endregion


            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeId = dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(_careDirectorQA_TeamId, personMhaLegalStatusId, personID, new DateTime(2020, 9, 1));
            string courtDataAndOutcomeTitle = (string)dbHelper.mhaCourtDateOutcome.GetMHACourtDateOutcomeByID(mhaCourtDateAndOutcomeId, "title")["title"];
            Guid mhaCourtDateAndOutcomeCaseNoteId = dbHelper.mhaCourtDateOutcomeCaseNote.CreateMHACourtDateOutcomeCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Court Dates and Outcomes 01 Case Note 01", "Note 01", mhaCourtDateAndOutcomeId, null, null, null, null, null,
                false, null, null, null, new DateTime(2020, 9, 1));


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.mhaCourtDateOutcomeCaseNote.GetByID(mhaCourtDateAndOutcomeCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), courtDataAndOutcomeTitle)
                .ValidateSubjectCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "Court Dates and Outcomes 01 Case Note 01")
                .ValidateActivityCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "Court Dates and Outcomes Case Note")
                .ValidateStatusCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "01/09/2020 00:00:00")
                .ValidateActualEndCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "Court Date and Outcome")
                .ValidateResponsibleUserCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(mhaCourtDateAndOutcomeCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11844")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Court Dates and Outcomes Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod32()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);
            #endregion


            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeId1 = dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(_careDirectorQA_TeamId, personMhaLegalStatusId, personID, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeCaseNoteId1 = dbHelper.mhaCourtDateOutcomeCaseNote.CreateMHACourtDateOutcomeCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Court Dates and Outcomes 01 Case Note 01", "Note 01", mhaCourtDateAndOutcomeId1, null, null, null, null, null,
                false, null, null, null, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeCaseNoteId2 = dbHelper.mhaCourtDateOutcomeCaseNote.CreateMHACourtDateOutcomeCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Court Dates and Outcomes 01 Case Note 02", "Note 02", mhaCourtDateAndOutcomeId1, null, null, null, null, null,
                false, null, null, null, new DateTime(2020, 9, 2));

            Guid mhaCourtDateAndOutcomeId2 = dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(_careDirectorQA_TeamId, personMhaLegalStatusId, personID, new DateTime(2020, 9, 3));
            Guid mhaCourtDateAndOutcome2CaseNoteId1 = dbHelper.mhaCourtDateOutcomeCaseNote.CreateMHACourtDateOutcomeCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Court Dates and Outcomes 02 Case Note 01", "Note 01", mhaCourtDateAndOutcomeId2, null, null, null, null, null,
                false, null, null, null, new DateTime(2020, 9, 3));
            Guid mhaCourtDateAndOutcome2CaseNoteId2 = dbHelper.mhaCourtDateOutcomeCaseNote.CreateMHACourtDateOutcomeCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Court Dates and Outcomes 02 Case Note 02", "Note 02", mhaCourtDateAndOutcomeId2, null, null, null, null, null,
                false, null, null, null, new DateTime(2020, 9, 4));


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(mhaCourtDateAndOutcomeCaseNoteId1.ToString(), "Court Dates and Outcomes 01 Case Note 01")
                .ValidateRecordPresent(mhaCourtDateAndOutcomeCaseNoteId2.ToString(), "Court Dates and Outcomes 01 Case Note 02")
                .ValidateRecordPresent(mhaCourtDateAndOutcome2CaseNoteId1.ToString(), "Court Dates and Outcomes 02 Case Note 01")
                .ValidateRecordPresent(mhaCourtDateAndOutcome2CaseNoteId2.ToString(), "Court Dates and Outcomes 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11845")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Financial Assessment Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod33()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);
            #endregion

            Guid financeAssessmentStatusId = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Draft")[0];
            Guid chargingRuleTypeId = commonMethodsDB.CreateChargingRuleType("Residential", _careDirectorQA_TeamId, new DateTime(2017, 9, 1));
            Guid financialAssessmentTypeId = dbHelper.financialAssessmentType.GetByName("No Financial Details Supplied")[0];
            Guid incomeSupportTypeId = dbHelper.incomeSupportType.GetByName("ESA - Assessment Phase (Under 25)")[0];
            Guid financialAssessmentRecordID = dbHelper.financialAssessment.CreateFinancialAssessment(personID, financeAssessmentStatusId, _systemUserId, _careDirectorQA_TeamId,
                new DateTime(2020, 9, 1), new DateTime(2020, 9, 6), chargingRuleTypeId, incomeSupportTypeId, financialAssessmentTypeId, 0, 240);
            string financialAssessmentTitle = (string)dbHelper.financialAssessment.GetByID(financialAssessmentRecordID, "name")["name"];
            Guid financialAssessmentCaseNoteId = dbHelper.financialAssessmentCaseNote.CreateFinancialAssessmentCaseNote(_careDirectorQA_TeamId, "Financial Assessment 01 Case Note 01", "Note 01",
                financialAssessmentRecordID, personID, new DateTime(2020, 9, 1), _systemUserId);


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.financialAssessmentCaseNote.GetByID(financialAssessmentCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(financialAssessmentCaseNoteId.ToString(), financialAssessmentTitle)
                .ValidateSubjectCellText(financialAssessmentCaseNoteId.ToString(), "Financial Assessment 01 Case Note 01")
                .ValidateActivityCellText(financialAssessmentCaseNoteId.ToString(), "Financial Assessment Case Note")
                .ValidateStatusCellText(financialAssessmentCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(financialAssessmentCaseNoteId.ToString(), "01/09/2020 00:00:00")
                .ValidateActualEndCellText(financialAssessmentCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(financialAssessmentCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(financialAssessmentCaseNoteId.ToString(), "Financial Assessment")
                .ValidateResponsibleUserCellText(financialAssessmentCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(financialAssessmentCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(financialAssessmentCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(financialAssessmentCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(financialAssessmentCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(financialAssessmentCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11846")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Financial Assessment Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod34()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);
            #endregion

            Guid financeAssessmentStatusId = dbHelper.financialAssessmentStatus.GetFinancialAssessmentStatusByName("Draft")[0];
            Guid chargingRuleTypeId = commonMethodsDB.CreateChargingRuleType("Residential", _careDirectorQA_TeamId, new DateTime(2017, 9, 1));
            Guid financialAssessmentTypeId = dbHelper.financialAssessmentType.GetByName("No Financial Details Supplied")[0];
            Guid incomeSupportTypeId = dbHelper.incomeSupportType.GetByName("ESA - Assessment Phase (Under 25)")[0];
            Guid financialAssessmentRecordID1 = dbHelper.financialAssessment.CreateFinancialAssessment(personID, financeAssessmentStatusId, _systemUserId, _careDirectorQA_TeamId,
                new DateTime(2020, 9, 1), new DateTime(2020, 9, 6), chargingRuleTypeId, incomeSupportTypeId, financialAssessmentTypeId, 0, 240);
            Guid financialAssessment1CaseNoteId1 = dbHelper.financialAssessmentCaseNote.CreateFinancialAssessmentCaseNote(_careDirectorQA_TeamId, "Financial Assessment 01 Case Note 01", "Note 01",
                financialAssessmentRecordID1, personID, new DateTime(2020, 9, 1), _systemUserId);
            Guid financialAssessment1CaseNoteId2 = dbHelper.financialAssessmentCaseNote.CreateFinancialAssessmentCaseNote(_careDirectorQA_TeamId, "Financial Assessment 01 Case Note 02", "Note 02",
                financialAssessmentRecordID1, personID, new DateTime(2020, 9, 2), _systemUserId);

            Guid financialAssessmentRecordID2 = dbHelper.financialAssessment.CreateFinancialAssessment(personID, financeAssessmentStatusId, _systemUserId, _careDirectorQA_TeamId,
                new DateTime(2020, 9, 3), new DateTime(2020, 9, 9), chargingRuleTypeId, incomeSupportTypeId, financialAssessmentTypeId, 0, 240);
            Guid financialAssessment2CaseNoteId1 = dbHelper.financialAssessmentCaseNote.CreateFinancialAssessmentCaseNote(_careDirectorQA_TeamId, "Financial Assessment 02 Case Note 01", "Note 01",
                financialAssessmentRecordID2, personID, new DateTime(2020, 9, 3), _systemUserId);
            Guid financialAssessment2CaseNoteId2 = dbHelper.financialAssessmentCaseNote.CreateFinancialAssessmentCaseNote(_careDirectorQA_TeamId, "Financial Assessment 02 Case Note 02", "Note 02",
                financialAssessmentRecordID2, personID, new DateTime(2020, 9, 4), _systemUserId);

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(financialAssessment1CaseNoteId1.ToString(), "Financial Assessment 01 Case Note 01")
                .ValidateRecordPresent(financialAssessment1CaseNoteId2.ToString(), "Financial Assessment 01 Case Note 02")
                .ValidateRecordPresent(financialAssessment2CaseNoteId1.ToString(), "Financial Assessment 02 Case Note 01")
                .ValidateRecordPresent(financialAssessment2CaseNoteId2.ToString(), "Financial Assessment 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11847")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Health Appointment Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod35()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Recurrence pattern

            var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
            if (!recurrencePatternExists)
                dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion            

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            #endregion

            #region Community/Clinic Appointment Contact Types

            var contactTypeId = commonMethodsDB.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Face To Face", new DateTime(2020, 1, 1), "1");

            #endregion

            #region Community/Clinic Appointment Location Types

            var _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Clients or patients home")[0];

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            var _appointmentsDataFormId = dbHelper.dataForm.GetByName("Appointments")[0];

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion

            #region Provider (Hospital)            
            var _providerId_Hospital = commonMethodsDB.CreateProvider("ActivitiesHospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "Test " + _currentDateSuffix, "Test " + _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "Team " + _currentDateSuffix, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);


            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2020, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0),
                                                            new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            #endregion

            #region Community Clinic Care Intervention            
            var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Therapy Group", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group")[0];

            communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Physical Rehab", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId2 = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab")[0];

            #endregion

            #region Community Case record
            var caseDate = new DateTime(2020, 2, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            #endregion

            #region Health Appointment Record
            DateTime healthAppointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment(
                _careDirectorQA_TeamId, personID, _firstName + " " + _lastName, _appointmentsDataFormId, contactTypeId, _healthAppointmentReasonId, "Assessment", _communityCaseId1, newSystemUserId,
                communityClinicTeam, _communityClinicLocationTypesId, "Clients or patients home", newSystemUserId,
                "appointment information ...", healthAppointmentDate, new TimeSpan(11, 0, 0), new TimeSpan(11, 15, 0), healthAppointmentDate,
                false, null, null,
                null, null, null, null, null, null, null, null, null, null,
                false, false, false);


            string healthAppointmentTitle = dbHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID, "title")["title"].ToString();
            Guid recordID = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID,
                _communityClinicCareInterventionId, null, null, null, null, null, healthAppointmentDate, "Health Appointment 01 Case Note 01", 2, "Note 01", false, false, false, null, null, null);


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByID(recordID, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(recordID.ToString(), healthAppointmentTitle)
                .ValidateSubjectCellText(recordID.ToString(), "Health Appointment 01 Case Note 01")
                .ValidateActivityCellText(recordID.ToString(), "Health Appointment Case Note")
                .ValidateStatusCellText(recordID.ToString(), "Completed")
                .ValidateDueDateCellText(recordID.ToString(), healthAppointmentDate.ToString("dd'/'MM'/'yyyy HH:mm:ss"))
                .ValidateActualEndCellText(recordID.ToString(), "")
                .ValidateCaseNoteCellText(recordID.ToString(), "Yes")
                .ValidateRegardingTypeCellText(recordID.ToString(), "Health Appointment")
                .ValidateResponsibleUserCellText(recordID.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(recordID.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(recordID.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(recordID.ToString(), modifiedon)
                .ValidateCreatedByCellText(recordID.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(recordID.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11848")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Health Appointment Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod36()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Recurrence pattern

            var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
            if (!recurrencePatternExists)
                dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion            

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            #endregion

            #region Community/Clinic Appointment Contact Types

            var contactTypeId = commonMethodsDB.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Face To Face", new DateTime(2020, 1, 1), "1");

            #endregion

            #region Community/Clinic Appointment Location Types

            var _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Clients or patients home")[0];

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            var _appointmentsDataFormId = dbHelper.dataForm.GetByName("Appointments")[0];

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion

            #region Provider (Hospital)            
            var _providerId_Hospital = commonMethodsDB.CreateProvider("ActivitiesHospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "Test " + _currentDateSuffix, "Test " + _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "Team " + _currentDateSuffix, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);


            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2020, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0),
                                                            new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            #endregion

            #region Community Clinic Care Intervention            
            var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Therapy Group", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group")[0];

            communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Physical Rehab", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId2 = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab")[0];

            #endregion

            #region Community Case record
            var caseDate = new DateTime(2020, 2, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            #endregion

            #region Health Appointment Record
            DateTime healthAppointmentDate1 = new DateTime(2020, 9, 1);
            DateTime healthAppointmentDate2 = new DateTime(2020, 9, 2);

            Guid healthAppointmentID1 = dbHelper.healthAppointment.CreateHealthAppointment(
                _careDirectorQA_TeamId, personID, _firstName + " " + _lastName, _appointmentsDataFormId, contactTypeId, _healthAppointmentReasonId, "Assessment", _communityCaseId1, newSystemUserId,
                communityClinicTeam, _communityClinicLocationTypesId, "Clients or patients home", newSystemUserId,
                "appointment information ...", healthAppointmentDate1, new TimeSpan(11, 0, 0), new TimeSpan(11, 15, 0), healthAppointmentDate1,
                false, null, null,
                null, null, null, null, null, null, null, null, null, null,
                false, false, false);

            Guid healthAppointmentCaseNoteRecord1ID1 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID1,
                _communityClinicCareInterventionId, null, null, null, null, null, new DateTime(2020, 9, 1, 11, 0, 0), "Health Appointment 01 Case Note 01", 2, "Note 01", false, false, false, null, null, null);
            Guid healthAppointmentCaseNoteRecord1ID2 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID1,
                _communityClinicCareInterventionId2, null, null, null, null, null, new DateTime(2020, 9, 17, 11, 0, 0), "Health Appointment 01 Case Note 02", 3, "Note 02", false, false, false, null, null, null);

            Guid healthAppointmentID2 = dbHelper.healthAppointment.CreateHealthAppointment(
                _careDirectorQA_TeamId, personID, _firstName + " " + _lastName, _appointmentsDataFormId, contactTypeId, _healthAppointmentReasonId, "Assessment", _communityCaseId1, newSystemUserId,
                communityClinicTeam, _communityClinicLocationTypesId, "Clients or patients home", newSystemUserId,
                "appointment information ...", healthAppointmentDate2, new TimeSpan(11, 0, 0), new TimeSpan(11, 15, 0), healthAppointmentDate2,
                false, null, null,
                null, null, null, null, null, null, null, null, null, null,
                false, false, false);

            Guid healthAppointmentCaseNoteRecord2ID1 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID2,
                _communityClinicCareInterventionId, null, null, null, null, null, new DateTime(2020, 9, 1, 11, 0, 0), "Health Appointment 02 Case Note 01", 1, "Note 01", false, false, false, null, null, null);
            Guid healthAppointmentCaseNoteRecord2ID2 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID2,
                _communityClinicCareInterventionId2, null, null, null, null, null, new DateTime(2020, 9, 17, 11, 0, 0), "Health Appointment 02 Case Note 02", 1, "Note 02", false, false, false, null, null, null);


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(healthAppointmentCaseNoteRecord1ID1.ToString(), "Health Appointment 01 Case Note 01")
                .ValidateRecordPresent(healthAppointmentCaseNoteRecord1ID2.ToString(), "Health Appointment 01 Case Note 02")
                .ValidateRecordPresent(healthAppointmentCaseNoteRecord2ID1.ToString(), "Health Appointment 02 Case Note 01")
                .ValidateRecordPresent(healthAppointmentCaseNoteRecord2ID2.ToString(), "Health Appointment 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11849")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Case Note (For Inpatient Leave Awol) records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod37()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Case Status
            Guid _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Depression", _careDirectorQA_TeamId, 140000001, false);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Contact Source

            Guid _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("LeaveAWOL_ContactSource", _careDirectorQA_TeamId);

            #endregion

            #region Inpatient Bed Type
            Guid _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Hospital Ward Specialty
            Guid _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("LeaveAWOL_InpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "LeaveAWOL_InpatientAdmissionSource", new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("LeaveAWOL_InpatientAdmissionSource")[0];

            #endregion

            #region Inpatient Leave Type

            var inpatientLeaveTypeExists = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType").Any();
            if (!inpatientLeaveTypeExists)
                dbHelper.inpatientLeaveType.CreateInpatientLeaveType("Automation_LeaveAWOLType", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientLeaveTypeId = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType")[0];

            #endregion

            #region Provider_Hospital

            string _providerName = "AWOLHospital" + _currentDateSuffix;
            Guid _provideHospitalId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId);

            #endregion

            #region Ward

            string _inpatientWardName = "Ward_" + _currentDateSuffix;
            Guid _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provideHospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            string _inpatientBayName = "Bay_" + _currentDateSuffix;
            Guid _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
            if (!inpatientBedExists)
                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
            Guid _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

            #endregion

            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionLeaveAWOL").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionLeaveAWOL", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionLeaveAWOL")[0];

            #endregion

            #region To Create Inpatient Case record

            DateTime admissionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, DateTime.Now.Date, _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpatientAdmissionMethodId, _systemUserId, admissionDate, _provideHospitalId, _inpatientWardId, 1, DateTime.Now.Date, false, false, false, false, false, false, false, false, false, false);

            #endregion

            #region Leave/AWOL record
            var LeaveAWOLID = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, personID, _caseId,
                DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _systemUserId, _systemUsername);
            string LeaveAWOLTitle = dbHelper.inpatientLeaveAwol.GetByID(LeaveAWOLID, "title")["title"].ToString();

            #endregion

            #region Leave/AWOL Case Note
            var LeaveAWOLCaseNoteID = dbHelper.inpatientLeaveAwolCaseNote.CreateInpatientLeaveAwolCaseNote(_careDirectorQA_TeamId, _systemUserId, "Leave AWOL 01 Case Note 01",
                "Case Note (For Inpatient Leave Awol) Description", LeaveAWOLID, _caseId, personID, null, null, null, null, null,
                false, null, null, null,
                new DateTime(2020, 9, 1, 12, 0, 0));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.inpatientLeaveAwolCaseNote.GetByID(LeaveAWOLCaseNoteID, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");


            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(LeaveAWOLCaseNoteID.ToString(), LeaveAWOLTitle)
                .ValidateSubjectCellText(LeaveAWOLCaseNoteID.ToString(), "Leave AWOL 01 Case Note 01")
                .ValidateActivityCellText(LeaveAWOLCaseNoteID.ToString(), "Case Note (For Inpatient Leave Awol)")
                .ValidateStatusCellText(LeaveAWOLCaseNoteID.ToString(), "Open")
                .ValidateDueDateCellText(LeaveAWOLCaseNoteID.ToString(), "01/09/2020 12:00:00")
                .ValidateActualEndCellText(LeaveAWOLCaseNoteID.ToString(), "")
                .ValidateCaseNoteCellText(LeaveAWOLCaseNoteID.ToString(), "Yes")
                .ValidateRegardingTypeCellText(LeaveAWOLCaseNoteID.ToString(), "Leave/AWOL")
                .ValidateResponsibleUserCellText(LeaveAWOLCaseNoteID.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(LeaveAWOLCaseNoteID.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(LeaveAWOLCaseNoteID.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(LeaveAWOLCaseNoteID.ToString(), modifiedon)
                .ValidateCreatedByCellText(LeaveAWOLCaseNoteID.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(LeaveAWOLCaseNoteID.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11850")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Case Note (For Inpatient Leave Awol) records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod38()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Case Status
            Guid _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Depression", _careDirectorQA_TeamId, 140000001, false);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Contact Source

            Guid _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("LeaveAWOL_ContactSource", _careDirectorQA_TeamId);

            #endregion

            #region Inpatient Bed Type
            Guid _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Hospital Ward Specialty
            Guid _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("LeaveAWOL_InpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "LeaveAWOL_InpatientAdmissionSource", new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("LeaveAWOL_InpatientAdmissionSource")[0];

            #endregion

            #region Inpatient Leave Type

            var inpatientLeaveTypeExists = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType").Any();
            if (!inpatientLeaveTypeExists)
                dbHelper.inpatientLeaveType.CreateInpatientLeaveType("Automation_LeaveAWOLType", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientLeaveTypeId = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType")[0];

            #endregion

            #region Provider_Hospital

            string _providerName = "AWOLHospital" + _currentDateSuffix;
            Guid _provideHospitalId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId);

            #endregion

            #region Ward

            string _inpatientWardName = "Ward_" + _currentDateSuffix;
            Guid _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provideHospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            string _inpatientBayName = "Bay_" + _currentDateSuffix;
            Guid _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
            if (!inpatientBedExists)
                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
            Guid _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

            #endregion

            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionLeaveAWOL").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionLeaveAWOL", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionLeaveAWOL")[0];

            #endregion

            #region To Create Inpatient Case record

            DateTime admissionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, DateTime.Now.Date, _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpatientAdmissionMethodId, _systemUserId, admissionDate, _provideHospitalId, _inpatientWardId, 1, DateTime.Now.Date, false, false, false, false, false, false, false, false, false, false);

            #endregion

            #region Leave/AWOL record
            var LeaveAWOL1ID = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, personID, _caseId,
                DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _systemUserId, _systemUsername);

            var LeaveAWOL2ID = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, personID, _caseId,
                DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _systemUserId, _systemUsername);

            #endregion

            #region Leave/AWOL1 Case Note
            var LeaveAWOL1CaseNote1ID = dbHelper.inpatientLeaveAwolCaseNote.CreateInpatientLeaveAwolCaseNote(_careDirectorQA_TeamId, _systemUserId, "Leave AWOL 01 Case Note 01",
                "Case Note (For Inpatient Leave Awol) Description", LeaveAWOL1ID, _caseId, personID, null, null, null, null, null,
                false, null, null, null,
                new DateTime(2020, 9, 1, 12, 0, 0));

            var LeaveAWOL1CaseNote2ID = dbHelper.inpatientLeaveAwolCaseNote.CreateInpatientLeaveAwolCaseNote(_careDirectorQA_TeamId, _systemUserId, "Leave AWOL 01 Case Note 02",
                "Case Note (For Inpatient Leave Awol) Description", LeaveAWOL1ID, _caseId, personID, null, null, null, null, null,
                false, null, null, null,
                new DateTime(2020, 9, 2, 12, 0, 0));

            #endregion

            #region Leave/AWOL2 Case Note
            var LeaveAWOL2CaseNote1ID = dbHelper.inpatientLeaveAwolCaseNote.CreateInpatientLeaveAwolCaseNote(_careDirectorQA_TeamId, _systemUserId, "Leave AWOL 02 Case Note 01",
                "Case Note (For Inpatient Leave Awol) Description", LeaveAWOL1ID, _caseId, personID, null, null, null, null, null,
                false, null, null, null,
                new DateTime(2020, 9, 1, 12, 0, 0));

            var LeaveAWOL2CaseNote2ID = dbHelper.inpatientLeaveAwolCaseNote.CreateInpatientLeaveAwolCaseNote(_careDirectorQA_TeamId, _systemUserId, "Leave AWOL 02 Case Note 02",
                "Case Note (For Inpatient Leave Awol) Description", LeaveAWOL1ID, _caseId, personID, null, null, null, null, null,
                false, null, null, null,
                new DateTime(2020, 9, 2, 12, 0, 0));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(LeaveAWOL1CaseNote1ID.ToString(), "Leave AWOL 01 Case Note 01")
                .ValidateRecordPresent(LeaveAWOL1CaseNote2ID.ToString(), "Leave AWOL 01 Case Note 02")
                .ValidateRecordPresent(LeaveAWOL2CaseNote1ID.ToString(), "Leave AWOL 02 Case Note 01")
                .ValidateRecordPresent(LeaveAWOL2CaseNote2ID.ToString(), "Leave AWOL 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11851")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Person MHA Legal Status Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod39()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Person MHA Legal Status Case Note
            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            string personMhaLegalStatusTitle = (string)dbHelper.personMHALegalStatus.GetPersonMHALegalStatusByID(personMhaLegalStatusId, "title")["title"];
            Guid personMhaLegalStatusCaseNoteId = dbHelper.personMHALegalStatusCaseNote.CreatePersonMHALegalStatusCaseNote(_careDirectorQA_TeamId, _systemUserId, "MHA Legal Status 01 Case Note 01", "Note 01",
                personMhaLegalStatusId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.personMHALegalStatusCaseNote.GetByID(personMhaLegalStatusCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(personMhaLegalStatusCaseNoteId.ToString(), personMhaLegalStatusTitle)
                .ValidateSubjectCellText(personMhaLegalStatusCaseNoteId.ToString(), "MHA Legal Status 01 Case Note 01")
                .ValidateActivityCellText(personMhaLegalStatusCaseNoteId.ToString(), "Person MHA Legal Status Case Note")
                .ValidateStatusCellText(personMhaLegalStatusCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(personMhaLegalStatusCaseNoteId.ToString(), "01/09/2020 00:00:00")
                .ValidateActualEndCellText(personMhaLegalStatusCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(personMhaLegalStatusCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(personMhaLegalStatusCaseNoteId.ToString(), "MHA Legal Status")
                .ValidateResponsibleUserCellText(personMhaLegalStatusCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(personMhaLegalStatusCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(personMhaLegalStatusCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(personMhaLegalStatusCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(personMhaLegalStatusCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(personMhaLegalStatusCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11852")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Person MHA Legal Status Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod40()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Person MHA Legal Status Case Note
            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid personMhaLegalStatusCaseNoteId1 = dbHelper.personMHALegalStatusCaseNote.CreatePersonMHALegalStatusCaseNote(_careDirectorQA_TeamId, _systemUserId, "MHA Legal Status 01 Case Note 01", "Note 01",
                personMhaLegalStatusId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid personMhaLegalStatusCaseNoteId2 = dbHelper.personMHALegalStatusCaseNote.CreatePersonMHALegalStatusCaseNote(_careDirectorQA_TeamId, _systemUserId, "MHA Legal Status 01 Case Note 02", "Note 02",
                personMhaLegalStatusId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 2));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(personMhaLegalStatusCaseNoteId1.ToString(), "MHA Legal Status 01 Case Note 01")
                .ValidateRecordPresent(personMhaLegalStatusCaseNoteId2.ToString(), "MHA Legal Status 01 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11853")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Appointment records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod41()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            Guid personAppointmentID = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            var fields = dbHelper.appointment.GetAppointmentByID(personAppointmentID, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRegardingCellText(personAppointmentID.ToString(), _personFullname)
                .ValidateSubjectCellText(personAppointmentID.ToString(), "Person Appointment 01")
                .ValidateActivityCellText(personAppointmentID.ToString(), "Appointment")
                .ValidateStatusCellText(personAppointmentID.ToString(), "Scheduled")
                .ValidateDueDateCellText(personAppointmentID.ToString(), "01/09/2020 11:30:00")
                .ValidateActualEndCellText(personAppointmentID.ToString(), "01/09/2020 12:00:00")
                .ValidateCaseNoteCellText(personAppointmentID.ToString(), "No")
                .ValidateRegardingTypeCellText(personAppointmentID.ToString(), "Person")
                .ValidateResponsibleUserCellText(personAppointmentID.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(personAppointmentID.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(personAppointmentID.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(personAppointmentID.ToString(), modifiedon)
                .ValidateCreatedByCellText(personAppointmentID.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(personAppointmentID.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11854")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Appointment records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod42()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            Guid personAppointmentID1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid personAppointmentID2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 02", "Note 02", "",
                new DateTime(2020, 9, 17), new TimeSpan(18, 0, 0), new DateTime(2020, 9, 17), new TimeSpan(18, 30, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(personAppointmentID1.ToString(), "Person Appointment 01")
                .ValidateRecordPresent(personAppointmentID2.ToString(), "Person Appointment 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11855")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Person Care Plan Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod43()
        {
            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion

            #region Provider (Hospital)            
            var _providerId_Hospital = commonMethodsDB.CreateProvider("ActivitiesHospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "Test " + _currentDateSuffix, "Test " + _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "Team " + _currentDateSuffix, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

            #endregion

            #region Care Plan Type
            Guid _carePlanTypeId = dbHelper.carePlanType.GetByName("Care Programme Approach Care Plan")[0];

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Community Case record
            var caseDate = new DateTime(2020, 2, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            #endregion

            #region Person Care Plan and Person Care Plan Case Note
            Guid _personCarePlan = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanTypeId, _systemUserId, personID, _communityCaseId1, _systemUserId, new DateTime(2020, 9, 17), 1, null, _careDirectorQA_TeamId);
            Guid _personCarePlanCaseNoteId = dbHelper.personCarePlanCaseNote.CreatePersonCarePlanCaseNote(_careDirectorQA_TeamId, _systemUserId, "Person Care Plan 01 Case Note 01",
                "Note 01", _personCarePlan, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            string _personCarePlanTitle = (string)dbHelper.personCarePlan.GetByID(_personCarePlan, "title")["title"];

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.personCarePlanCaseNote.GetByID(_personCarePlanCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(_personCarePlanCaseNoteId.ToString(), _personCarePlanTitle)
                .ValidateSubjectCellText(_personCarePlanCaseNoteId.ToString(), "Person Care Plan 01 Case Note 01")
                .ValidateActivityCellText(_personCarePlanCaseNoteId.ToString(), "Person Care Plan Case Note")
                .ValidateStatusCellText(_personCarePlanCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(_personCarePlanCaseNoteId.ToString(), "01/09/2020 12:00:00")
                .ValidateActualEndCellText(_personCarePlanCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(_personCarePlanCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(_personCarePlanCaseNoteId.ToString(), "Person Care Plan")
                .ValidateResponsibleUserCellText(_personCarePlanCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(_personCarePlanCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(_personCarePlanCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(_personCarePlanCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(_personCarePlanCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(_personCarePlanCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11856")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Person Care Plan Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod44()
        {
            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion

            #region Provider (Hospital)            
            var _providerId_Hospital = commonMethodsDB.CreateProvider("ActivitiesHospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "Test " + _currentDateSuffix, "Test " + _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "Team " + _currentDateSuffix, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);

            #endregion

            #region Care Plan Type
            Guid _carePlanTypeId = dbHelper.carePlanType.GetByName("Urgent & Emergency Mental Health Care Plan")[0];
            Guid _carePlanType2Id = dbHelper.carePlanType.GetByName("Care Programme Approach Care Plan")[0];

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Community Case record
            var caseDate = new DateTime(2020, 2, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            #endregion

            #region Person Care Plan and Person Care Plan Case Note
            Guid _personCarePlan1 = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanTypeId, _systemUserId, personID, _communityCaseId1, _systemUserId, new DateTime(2020, 9, 17), 1, null, _careDirectorQA_TeamId);
            Guid _personCarePlan1CaseNote1Id = dbHelper.personCarePlanCaseNote.CreatePersonCarePlanCaseNote(_careDirectorQA_TeamId, _systemUserId, "Person Care Plan 01 Case Note 01",
                "Note 01", _personCarePlan1, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid _personCarePlan1CaseNote2Id = dbHelper.personCarePlanCaseNote.CreatePersonCarePlanCaseNote(_careDirectorQA_TeamId, _systemUserId, "Person Care Plan 01 Case Note 02",
                "Note 02", _personCarePlan1, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 17, 12, 0, 0));


            Guid _personCarePlan2 = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanType2Id, _systemUserId, personID, _communityCaseId1, _systemUserId, new DateTime(2020, 9, 17), 1, null, _careDirectorQA_TeamId);
            Guid _personCarePlan2CaseNote1Id = dbHelper.personCarePlanCaseNote.CreatePersonCarePlanCaseNote(_careDirectorQA_TeamId, _systemUserId, "Person Care Plan 02 Case Note 01",
                "Note 01", _personCarePlan2, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid _personCarePlan2CaseNote2Id = dbHelper.personCarePlanCaseNote.CreatePersonCarePlanCaseNote(_careDirectorQA_TeamId, _systemUserId, "Person Care Plan 02 Case Note 02",
                "Note 02", _personCarePlan2, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 17, 12, 0, 0));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(_personCarePlan1CaseNote1Id.ToString(), "Person Care Plan 01 Case Note 01")
                .ValidateRecordPresent(_personCarePlan1CaseNote2Id.ToString(), "Person Care Plan 01 Case Note 02")
                .ValidateRecordPresent(_personCarePlan2CaseNote1Id.ToString(), "Person Care Plan 02 Case Note 01")
                .ValidateRecordPresent(_personCarePlan2CaseNote2Id.ToString(), "Person Care Plan 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11857")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Person Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod45()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            var _personCaseNoteId = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 01", "Note 01", personID, new DateTime(2020, 9, 1, 12, 0, 0), _systemUserId);
            var fields = dbHelper.personCaseNote.GetPersonCaseNoteByID(_personCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();



            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(_personCaseNoteId.ToString(), _personFullname)
                .ValidateSubjectCellText(_personCaseNoteId.ToString(), "Person Case Note 01")
                .ValidateActivityCellText(_personCaseNoteId.ToString(), "Person Case Note")
                .ValidateStatusCellText(_personCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(_personCaseNoteId.ToString(), "01/09/2020 12:00:00")
                .ValidateActualEndCellText(_personCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(_personCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(_personCaseNoteId.ToString(), "Person")
                .ValidateResponsibleUserCellText(_personCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(_personCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(_personCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(_personCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(_personCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(_personCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11858")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Person Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod46()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            Guid _personCaseNoteId1 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 01", "Note 01", personID, new DateTime(2020, 9, 1, 12, 0, 0), _systemUserId);
            Guid _personCaseNoteId2 = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 02", "Note 02", personID, new DateTime(2020, 9, 17, 12, 0, 0), _systemUserId);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(_personCaseNoteId1.ToString(), "Person Case Note 01")
                .ValidateRecordPresent(_personCaseNoteId2.ToString(), "Person Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11859")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Person Clinical Risk Status Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod47()
        {
            #region Clinical Risk Status
            Guid _clinicalRiskStatus = commonMethodsDB.CreateClinicalRiskStatus(_careDirectorQA_TeamId, "Overall High", new DateTime(2020, 3, 18));

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Person Clinical Risk Status
            Guid _personClinicalRiskStatusId = dbHelper.personClinicalRiskStatus.CreatePersonClinicalRiskStatus(personID, _careDirectorQA_TeamId,
                _clinicalRiskStatus, new DateTime(2020, 9, 1));
            string _personClinicalRiskStatusTitle = (string)dbHelper.personClinicalRiskStatus.GetPersonClinicalRiskStatusByID(_personClinicalRiskStatusId, "title")["title"];

            Guid _personClinicalRiskStatusCaseNoteId = dbHelper.personClinicalRiskStatusCaseNote.CreatePersonClinicalRiskStatusCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Clinical Risk Status 01 Case Note 01", "Note 01", _personClinicalRiskStatusId, null, null, null, null, null, false, null, null, null,
                new DateTime(2020, 9, 1, 12, 0, 0));


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.personClinicalRiskStatusCaseNote.GetByID(_personClinicalRiskStatusCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(_personClinicalRiskStatusCaseNoteId.ToString(), _personClinicalRiskStatusTitle)
                .ValidateSubjectCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "Person Clinical Risk Status 01 Case Note 01")
                .ValidateActivityCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "Person Clinical Risk Status Case Note")
                .ValidateStatusCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "01/09/2020 12:00:00")
                .ValidateActualEndCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "Person Clinical Risk Status")
                .ValidateResponsibleUserCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(_personClinicalRiskStatusCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(_personClinicalRiskStatusCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(_personClinicalRiskStatusCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(_personClinicalRiskStatusCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(_personClinicalRiskStatusCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11860")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Person Clinical Risk Status Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod48()
        {
            #region Clinical Risk Status
            Guid _overallHighclinicalRiskStatus = commonMethodsDB.CreateClinicalRiskStatus(_careDirectorQA_TeamId, "Overall High", new DateTime(2020, 3, 18));
            Guid _overallModerateclinicalRiskStatus = commonMethodsDB.CreateClinicalRiskStatus(_careDirectorQA_TeamId, "Overall Moderate", new DateTime(2019, 6, 26));

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Person Clinical Risk Status
            Guid _personClinicalRiskStatus1Id = dbHelper.personClinicalRiskStatus.CreatePersonClinicalRiskStatus(personID, _careDirectorQA_TeamId,
                _overallHighclinicalRiskStatus, new DateTime(2020, 9, 1), new DateTime(2020, 9, 3));
            Guid _personClinicalRiskStatus1CaseNote1Id = dbHelper.personClinicalRiskStatusCaseNote.CreatePersonClinicalRiskStatusCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Clinical Risk Status 01 Case Note 01", "Note 01", _personClinicalRiskStatus1Id, null, null, null, null, null, false, null, null, null,
                new DateTime(2020, 9, 1, 12, 0, 0));
            Guid _personClinicalRiskStatus1CaseNote2Id = dbHelper.personClinicalRiskStatusCaseNote.CreatePersonClinicalRiskStatusCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Clinical Risk Status 01 Case Note 02", "Note 02", _personClinicalRiskStatus1Id, null, null, null, null, null, false, null, null, null,
                new DateTime(2020, 9, 17, 12, 0, 0));

            Guid _personClinicalRiskStatus2Id = dbHelper.personClinicalRiskStatus.CreatePersonClinicalRiskStatus(personID, _careDirectorQA_TeamId,
                _overallModerateclinicalRiskStatus, new DateTime(2020, 9, 4), new DateTime(2020, 9, 6));
            Guid _personClinicalRiskStatus2CaseNote1Id = dbHelper.personClinicalRiskStatusCaseNote.CreatePersonClinicalRiskStatusCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Clinical Risk Status 02 Case Note 01", "Note 01", _personClinicalRiskStatus2Id, null, null, null, null, null, false, null, null, null,
                new DateTime(2020, 9, 1, 12, 0, 0));
            Guid _personClinicalRiskStatus2CaseNote2Id = dbHelper.personClinicalRiskStatusCaseNote.CreatePersonClinicalRiskStatusCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Clinical Risk Status 02 Case Note 02", "Note 02", _personClinicalRiskStatus2Id, null, null, null, null, null, false, null, null, null,
                new DateTime(2020, 9, 17, 12, 0, 0));


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(_personClinicalRiskStatus1CaseNote1Id.ToString(), "Person Clinical Risk Status 01 Case Note 01")
                .ValidateRecordPresent(_personClinicalRiskStatus1CaseNote2Id.ToString(), "Person Clinical Risk Status 01 Case Note 02")
                .ValidateRecordPresent(_personClinicalRiskStatus2CaseNote1Id.ToString(), "Person Clinical Risk Status 02 Case Note 01")
                .ValidateRecordPresent(_personClinicalRiskStatus2CaseNote2Id.ToString(), "Person Clinical Risk Status 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11861")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Person Height And Weight Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod49()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Person Height and Weight, Person Height and Weight Case Note
            Guid _personHeightAndWeightId = dbHelper.personHeightAndWeight.CreatePersonHeightAndWeight(_careDirectorQA_TeamId, _systemUserId,
                personID, new DateTime(2020, 9, 1), 20, false, 80m, false, 1.80m, null, null, null, null);
            string _personHeightAndWeightTitle = (string)dbHelper.personHeightAndWeight.GetByID(_personHeightAndWeightId, "title")["title"];
            Guid _personHeightAndWeightCaseNoteId = dbHelper.personHeightAndWeightCaseNote.CreatePersonHeightAndWeightCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Height And Weight 01 Case Note 01", "Note 01", _personHeightAndWeightId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.personHeightAndWeightCaseNote.GetByID(_personHeightAndWeightCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(_personHeightAndWeightCaseNoteId.ToString(), _personHeightAndWeightTitle)
                .ValidateSubjectCellText(_personHeightAndWeightCaseNoteId.ToString(), "Person Height And Weight 01 Case Note 01")
                .ValidateActivityCellText(_personHeightAndWeightCaseNoteId.ToString(), "Person Height And Weight Case Note")
                .ValidateStatusCellText(_personHeightAndWeightCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(_personHeightAndWeightCaseNoteId.ToString(), "01/09/2020 12:00:00")
                .ValidateActualEndCellText(_personHeightAndWeightCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(_personHeightAndWeightCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(_personHeightAndWeightCaseNoteId.ToString(), "Height & Weight Observation")
                .ValidateResponsibleUserCellText(_personHeightAndWeightCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(_personHeightAndWeightCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(_personHeightAndWeightCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(_personHeightAndWeightCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(_personHeightAndWeightCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(_personHeightAndWeightCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11862")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Person Height And Weight Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod50()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Person Height and Weight, Person Height and Weight Case Note
            Guid _personHeightAndWeightId1 = dbHelper.personHeightAndWeight.CreatePersonHeightAndWeight(_careDirectorQA_TeamId, _systemUserId,
                personID, new DateTime(2020, 9, 1), 20, false, 80m, false, 1.80m, null, null, null, null);
            Guid _personHeightAndWeight1CaseNoteId1 = dbHelper.personHeightAndWeightCaseNote.CreatePersonHeightAndWeightCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Height And Weight 01 Case Note 01", "Note 01", _personHeightAndWeightId1, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid _personHeightAndWeight1CaseNoteId2 = dbHelper.personHeightAndWeightCaseNote.CreatePersonHeightAndWeightCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Height And Weight 01 Case Note 02", "Note 02", _personHeightAndWeightId1, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 17, 12, 0, 0));


            Guid _personHeightAndWeightId2 = dbHelper.personHeightAndWeight.CreatePersonHeightAndWeight(_careDirectorQA_TeamId, _systemUserId,
                personID, new DateTime(2020, 9, 2), 20, false, 80m, false, 1.80m, null, null, null, null);
            Guid _personHeightAndWeight2CaseNoteId1 = dbHelper.personHeightAndWeightCaseNote.CreatePersonHeightAndWeightCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Height And Weight 02 Case Note 01", "Note 01", _personHeightAndWeightId2, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid _personHeightAndWeight2CaseNoteId2 = dbHelper.personHeightAndWeightCaseNote.CreatePersonHeightAndWeightCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Height And Weight 02 Case Note 02", "Note 02", _personHeightAndWeightId2, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 17, 12, 0, 0));

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(_personHeightAndWeight1CaseNoteId1.ToString(), "Person Height And Weight 01 Case Note 01")
                .ValidateRecordPresent(_personHeightAndWeight1CaseNoteId2.ToString(), "Person Height And Weight 01 Case Note 02")
                .ValidateRecordPresent(_personHeightAndWeight2CaseNoteId1.ToString(), "Person Height And Weight 02 Case Note 01")
                .ValidateRecordPresent(_personHeightAndWeight2CaseNoteId2.ToString(), "Person Height And Weight 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11863")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Private Fostering Arrangement Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod51()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case and Private Fostering Arrangement

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);

            Guid _privateFosteringArrangementId = dbHelper.privateFosteringArrangement.CreatePrivateFosteringArrangement(_careDirectorQA_TeamId, _systemUserId,
                _caseId1, personID, new DateTime(2019, 9, 1));
            string __privateFosteringArrangementTitle = (string)dbHelper.privateFosteringArrangement.GetByID(_privateFosteringArrangementId, "title")["title"];

            Guid _privateFosteringArrangementCaseNoteId = dbHelper
                .privateFosteringArrangementCaseNote
                .CreatePrivateFosteringArrangementCaseNote(_careDirectorQA_TeamId, _systemUserId, _caseId1, personID,
                "Private Fostering Arrangement 01 Case Note 01", "Private Fostering Arrangement Case Note", _privateFosteringArrangementId,
                null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 16, 30, 0));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.privateFosteringArrangementCaseNote.GetByID(_privateFosteringArrangementCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(_privateFosteringArrangementCaseNoteId.ToString(), __privateFosteringArrangementTitle)
                .ValidateSubjectCellText(_privateFosteringArrangementCaseNoteId.ToString(), "Private Fostering Arrangement 01 Case Note 01")
                .ValidateActivityCellText(_privateFosteringArrangementCaseNoteId.ToString(), "Private Fostering Arrangement Case Note")
                .ValidateStatusCellText(_privateFosteringArrangementCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(_privateFosteringArrangementCaseNoteId.ToString(), "01/09/2020 16:30:00")
                .ValidateActualEndCellText(_privateFosteringArrangementCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(_privateFosteringArrangementCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(_privateFosteringArrangementCaseNoteId.ToString(), "Private Fostering Arrangement")
                .ValidateResponsibleUserCellText(_privateFosteringArrangementCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(_privateFosteringArrangementCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(_privateFosteringArrangementCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(_privateFosteringArrangementCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(_privateFosteringArrangementCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(_privateFosteringArrangementCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11864")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Private Fostering Arrangement Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod52()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case and Private Fostering Arrangement

            Guid _caseId1 = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);

            Guid _privateFosteringArrangementId1 = dbHelper.privateFosteringArrangement.CreatePrivateFosteringArrangement(_careDirectorQA_TeamId, _systemUserId,
                _caseId1, personID, new DateTime(2019, 9, 1));

            Guid _privateFosteringArrangementCaseNoteId1 = dbHelper
                .privateFosteringArrangementCaseNote
                .CreatePrivateFosteringArrangementCaseNote(_careDirectorQA_TeamId, _systemUserId, _caseId1, personID,
                "Private Fostering Arrangement 01 Case Note 01", "Private Fostering Arrangement Case Note", _privateFosteringArrangementId1,
                null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 16, 30, 0));

            Guid _privateFosteringArrangementCaseNoteId2 = dbHelper
                .privateFosteringArrangementCaseNote
                .CreatePrivateFosteringArrangementCaseNote(_careDirectorQA_TeamId, _systemUserId, _caseId1, personID,
                "Private Fostering Arrangement 01 Case Note 02", "Private Fostering Arrangement Case Note", _privateFosteringArrangementId1,
                null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 17, 16, 30, 0));

            Guid _privateFosteringArrangementId2 = dbHelper.privateFosteringArrangement.CreatePrivateFosteringArrangement(_careDirectorQA_TeamId, _systemUserId,
                _caseId1, personID, new DateTime(2019, 9, 2));

            Guid _privateFosteringArrangement2CaseNoteId1 = dbHelper
                .privateFosteringArrangementCaseNote
                .CreatePrivateFosteringArrangementCaseNote(_careDirectorQA_TeamId, _systemUserId, _caseId1, personID,
                "Private Fostering Arrangement 02 Case Note 01", "Private Fostering Arrangement Case Note", _privateFosteringArrangementId2,
                null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 16, 30, 0));

            Guid _privateFosteringArrangement2CaseNoteId2 = dbHelper
                .privateFosteringArrangementCaseNote
                .CreatePrivateFosteringArrangementCaseNote(_careDirectorQA_TeamId, _systemUserId, _caseId1, personID,
                "Private Fostering Arrangement 02 Case Note 02", "Private Fostering Arrangement Case Note", _privateFosteringArrangementId2,
                null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 17, 16, 30, 0));

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(_privateFosteringArrangementCaseNoteId1.ToString(), "Private Fostering Arrangement 01 Case Note 01")
                .ValidateRecordPresent(_privateFosteringArrangementCaseNoteId2.ToString(), "Private Fostering Arrangement 01 Case Note 02")
                .ValidateRecordPresent(_privateFosteringArrangement2CaseNoteId1.ToString(), "Private Fostering Arrangement 02 Case Note 01")
                .ValidateRecordPresent(_privateFosteringArrangement2CaseNoteId2.ToString(), "Private Fostering Arrangement 02 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11865")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Rights and Request for an IMHA and MHA Appeal Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod53()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Status
            Guid _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Depression", _careDirectorQA_TeamId, 140000001, false);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Contact Source

            Guid _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Auto_ContactSource", _careDirectorQA_TeamId);

            #endregion

            #region Inpatient Bed Type
            Guid _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Hospital Ward Specialty
            Guid _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "AutoInpatientAdmissionSource", new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource")[0];

            #endregion

            #region Provider_Hospital

            string _providerName = "Hospital" + _currentDateSuffix;
            Guid _provideHospitalId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId);

            #endregion

            #region Ward

            string _inpatientWardName = "Ward_" + _currentDateSuffix;
            Guid _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provideHospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            string _inpatientBayName = "Bay_" + _currentDateSuffix;
            Guid _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
            if (!inpatientBedExists)
                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
            Guid _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

            #endregion

            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionMethod", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod")[0];

            #endregion

            #region To Create Inpatient Case record

            DateTime admissionDate = new DateTime(2020, 9, 1);
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, new DateTime(2020, 9, 1), _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpatientAdmissionMethodId, _systemUserId, admissionDate, _provideHospitalId, _inpatientWardId, 1, new DateTime(2020, 9, 1), false, false, false, false, false, false, false, false, false, false);

            #endregion

            #region MHA Legal Status, Rights and Requests for an IMHA and MHA appeal
            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));

            Guid mhaRightsAndRequestsId = dbHelper.mhaRightsAndRequests.CreateMHARightsAndRequests(_careDirectorQA_TeamId,
                personID, _caseId, new DateTime(2020, 9, 17), personMhaLegalStatusId, _systemUserId);
            string _mhaRightsAndRequestsTitle = (string)dbHelper.mhaRightsAndRequests.GetByID(mhaRightsAndRequestsId, "title")["title"];

            Guid mhaRightsAndRequestsCaseNoteId = dbHelper.mhaRightsAndRequestsCaseNote.CreateMHARightsAndRequestsCaseNote(_careDirectorQA_TeamId, _systemUserId,
                personID, "Rights and Request for an IMHA and MHA Appeal 01 Case Note 01", "Rights and Request for an IMHA and MHA Appeal Case Note",
                mhaRightsAndRequestsId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.mhaRightsAndRequestsCaseNote.GetByID(mhaRightsAndRequestsCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(mhaRightsAndRequestsCaseNoteId.ToString(), _mhaRightsAndRequestsTitle)
                .ValidateSubjectCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "Rights and Request for an IMHA and MHA Appeal 01 Case Note 01")
                .ValidateActivityCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "Rights and Request for an IMHA and MHA Appeal Case Note")
                .ValidateStatusCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "01/09/2020 00:00:00")
                .ValidateActualEndCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "Rights and Request for an IMHA and MHA Appeal")
                .ValidateResponsibleUserCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(mhaRightsAndRequestsCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(mhaRightsAndRequestsCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(mhaRightsAndRequestsCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(mhaRightsAndRequestsCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(mhaRightsAndRequestsCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11866")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Rights and Request for an IMHA and MHA Appeal Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod54()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Status
            Guid _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Depression", _careDirectorQA_TeamId, 140000001, false);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Contact Source

            Guid _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Auto_ContactSource", _careDirectorQA_TeamId);

            #endregion

            #region Inpatient Bed Type
            Guid _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Hospital Ward Specialty
            Guid _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "AutoInpatientAdmissionSource", new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource")[0];

            #endregion

            #region Provider_Hospital

            string _providerName = "Hospital" + _currentDateSuffix;
            Guid _provideHospitalId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId);

            #endregion

            #region Ward

            string _inpatientWardName = "Ward_" + _currentDateSuffix;
            Guid _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provideHospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            string _inpatientBayName = "Bay_" + _currentDateSuffix;
            Guid _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
            if (!inpatientBedExists)
                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
            Guid _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

            #endregion

            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionMethod", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod")[0];

            #endregion

            #region To Create Inpatient Case record

            DateTime admissionDate = new DateTime(2020, 9, 1);
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, new DateTime(2020, 9, 1), _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpatientAdmissionMethodId, _systemUserId, admissionDate, _provideHospitalId, _inpatientWardId, 1, new DateTime(2020, 9, 1), false, false, false, false, false, false, false, false, false, false);

            #endregion

            #region MHA Legal Status, Rights and Requests for an IMHA and MHA appeal
            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));

            Guid mhaRightsAndRequestsId1 = dbHelper.mhaRightsAndRequests.CreateMHARightsAndRequests(_careDirectorQA_TeamId,
                personID, _caseId, new DateTime(2020, 9, 17), personMhaLegalStatusId, _systemUserId);

            Guid mhaRightsAndRequestsCaseNoteId1 = dbHelper.mhaRightsAndRequestsCaseNote.CreateMHARightsAndRequestsCaseNote(_careDirectorQA_TeamId, _systemUserId,
                personID, "Rights and Request for an IMHA and MHA Appeal 01 Case Note 01", "Rights and Request for an IMHA and MHA Appeal Case Note",
                mhaRightsAndRequestsId1, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid mhaRightsAndRequestsCaseNoteId2 = dbHelper.mhaRightsAndRequestsCaseNote.CreateMHARightsAndRequestsCaseNote(_careDirectorQA_TeamId, _systemUserId,
                personID, "Rights and Request for an IMHA and MHA Appeal 01 Case Note 02", "Rights and Request for an IMHA and MHA Appeal Case Note",
                mhaRightsAndRequestsId1, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 17));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!")
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
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(mhaRightsAndRequestsCaseNoteId1.ToString(), "Rights and Request for an IMHA and MHA Appeal 01 Case Note 01")
                .ValidateRecordPresent(mhaRightsAndRequestsCaseNoteId2.ToString(), "Rights and Request for an IMHA and MHA Appeal 01 Case Note 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11867")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate the cell content for Section 117 Entitlement Case Note records")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod55()
        {
            #region Provider - Supplier

            string _providerName = "Provider_" + _currentDateSuffix;
            Guid _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Section 117 Entitlements
            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid mhaAftercareEntitlementId = dbHelper.mhaAftercareEntitlement.CreateMHAAftercareEntitlement(_careDirectorQA_TeamId, personID,
                personMhaLegalStatusId, _providerId, true, new DateTime(2020, 9, 1, 4, 30, 0), null, false);
            string mhaAftercareEntitlementTitle = (string)dbHelper.mhaAftercareEntitlement.GetByID(mhaAftercareEntitlementId, "title")["title"];

            Guid mhaAftercareEntitlementCaseNoteId = dbHelper.mhaAftercareEntitlementCaseNote.CreateMHAAftercareEntitlementCaseNote(_careDirectorQA_TeamId, _systemUserId,
                personID, "Section 117 Case Note 01", "Section 117 Case Note", mhaAftercareEntitlementId,
                null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton();

            var fields = dbHelper.mhaAftercareEntitlementCaseNote.GetByID(mhaAftercareEntitlementCaseNoteId, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .ValidateRegardingCellText(mhaAftercareEntitlementCaseNoteId.ToString(), mhaAftercareEntitlementTitle)
                .ValidateSubjectCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "Section 117 Case Note 01")
                .ValidateActivityCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "Section 117 Entitlement Case Note")
                .ValidateStatusCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "Open")
                .ValidateDueDateCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "01/09/2020 00:00:00")
                .ValidateActualEndCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "")
                .ValidateCaseNoteCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "Yes")
                .ValidateRegardingTypeCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "Section 117 Entitlement")
                .ValidateResponsibleUserCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "AllActivities User1")
                .ValidateResponsibleTeamCellText(mhaAftercareEntitlementCaseNoteId.ToString(), "CareDirector QA")
                .ValidateModifiedByCellText(mhaAftercareEntitlementCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateModifiedOnCellText(mhaAftercareEntitlementCaseNoteId.ToString(), modifiedon)
                .ValidateCreatedByCellText(mhaAftercareEntitlementCaseNoteId.ToString(), _defaultUserFullname)
                .ValidateCreatedOnCellText(mhaAftercareEntitlementCaseNoteId.ToString(), createdon);
        }

        [TestProperty("JiraIssueID", "CDV6-11868")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Tap on the search button - " +
            "Validate that all Section 117 Entitlement Case Note records are present")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod56()
        {
            #region Provider - Supplier

            string _providerName = "Provider_" + _currentDateSuffix;
            Guid _providerId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId, 2, _providerName + "@test.com");

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Section 117 Entitlements
            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid mhaAftercareEntitlementId = dbHelper.mhaAftercareEntitlement.CreateMHAAftercareEntitlement(_careDirectorQA_TeamId, personID,
                personMhaLegalStatusId, _providerId, true, new DateTime(2020, 9, 1, 4, 30, 0), null, false);

            Guid mhaAftercareEntitlementCaseNoteId1 = dbHelper.mhaAftercareEntitlementCaseNote.CreateMHAAftercareEntitlementCaseNote(_careDirectorQA_TeamId, _systemUserId,
                personID, "Section 117 Case Note 01", "Section 117 Case Note", mhaAftercareEntitlementId,
                null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid mhaAftercareEntitlementCaseNoteId2 = dbHelper.mhaAftercareEntitlementCaseNote.CreateMHAAftercareEntitlementCaseNote(_careDirectorQA_TeamId, _systemUserId,
                personID, "Section 117 Case Note 02", "Section 117 Case Note", mhaAftercareEntitlementId,
                null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 17));


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSearchButton()

                .ValidateRecordPresent(mhaAftercareEntitlementCaseNoteId1.ToString(), "Section 117 Case Note 01")
                .ValidateRecordPresent(mhaAftercareEntitlementCaseNoteId2.ToString(), "Section 117 Case Note 02");
        }






        [TestProperty("JiraIssueID", "CDV6-11869")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Select Appointment as the Activity Type - " +
            "Tap on the search button - Validate only appointment records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod57()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Status
            Guid _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Depression", _careDirectorQA_TeamId, 140000001, false);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Contact Source

            Guid _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Auto_ContactSource", _careDirectorQA_TeamId);

            #endregion

            #region Inpatient Bed Type
            Guid _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Hospital Ward Specialty
            Guid _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "AutoInpatientAdmissionSource", new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource")[0];

            #endregion

            #region Provider_Hospital

            string _providerName = "Hospital" + _currentDateSuffix;
            Guid _provideHospitalId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId);

            #endregion

            #region Ward

            string _inpatientWardName = "Ward_" + _currentDateSuffix;
            Guid _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provideHospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            string _inpatientBayName = "Bay_" + _currentDateSuffix;
            Guid _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
            if (!inpatientBedExists)
                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
            Guid _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

            #endregion

            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionMethod", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod")[0];

            #endregion

            #region To Create Inpatient Case record

            DateTime admissionDate = new DateTime(2020, 9, 1);
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, new DateTime(2020, 9, 1), _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpatientAdmissionMethodId, _systemUserId, admissionDate, _provideHospitalId, _inpatientWardId, 1, new DateTime(2020, 9, 1), false, false, false, false, false, false, false, false, false, false);
            string _caseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

            #endregion


            #region Person Appointment
            Guid _personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid _personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(18, 0, 0), new DateTime(2020, 9, 17), new TimeSpan(18, 30, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            #endregion

            #region Case Appointment
            Guid _caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid _caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(18, 0, 0), new DateTime(2020, 9, 17), new TimeSpan(18, 30, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            #region Case Note, Email, Letter, Phone Call, Task for Case
            Guid caseCaseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1));

            Guid caseEmailRecordId = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2);

            Guid caseLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);

            Guid casePhoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", _caseId, _caseTitle, _careDirectorQA_TeamId, "case");

            Guid caseTaskId = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId, _caseTitle, "case");

            Guid personCaseNoteId = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 01", "Note 01", personID, new DateTime(2020, 9, 1), _careDirectorQA_TeamId);


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Appointment")
                .TapSearchButton()

                .ValidateRecordPresent(_personAppointmentId2.ToString(), "Person Appointment 02")
                .ValidateRecordPresent(_caseAppointmentId2.ToString(), "Case 02 Appointment 02")
                .ValidateRecordPresent(_personAppointmentId1.ToString(), "Person Appointment 01")
                .ValidateRecordPresent(_caseAppointmentId1.ToString(), "Case 02 Appointment 01")

                .ValidateRecordNotPresent(caseCaseNoteId.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId.ToString())
                .ValidateRecordNotPresent(caseLetterId.ToString())
                .ValidateRecordNotPresent(casePhoneCallId.ToString())
                .ValidateRecordNotPresent(caseTaskId.ToString())
                .ValidateRecordNotPresent(personCaseNoteId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11870")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Select Email as the Activity Type - " +
            "Tap on the search button - Validate only Email records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod58()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Status
            Guid _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Depression", _careDirectorQA_TeamId, 140000001, false);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Contact Source

            Guid _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Auto_ContactSource", _careDirectorQA_TeamId);

            #endregion

            #region Inpatient Bed Type
            Guid _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Hospital Ward Specialty
            Guid _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "AutoInpatientAdmissionSource", new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource")[0];

            #endregion

            #region Provider_Hospital

            string _providerName = "Hospital" + _currentDateSuffix;
            Guid _provideHospitalId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId);

            #endregion

            #region Ward

            string _inpatientWardName = "Ward_" + _currentDateSuffix;
            Guid _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provideHospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            string _inpatientBayName = "Bay_" + _currentDateSuffix;
            Guid _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
            if (!inpatientBedExists)
                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
            Guid _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

            #endregion

            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionMethod", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod")[0];

            #endregion

            #region To Create Inpatient Case record

            DateTime admissionDate = new DateTime(2020, 9, 1);
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, new DateTime(2020, 9, 1), _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpatientAdmissionMethodId, _systemUserId, admissionDate, _provideHospitalId, _inpatientWardId, 1, new DateTime(2020, 9, 1), false, false, false, false, false, false, false, false, false, false);
            string _caseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

            #endregion


            #region Person and Case Email             
            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 01", "Note 01", 2);
            Guid personEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 02", "Note 02", 2);
            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2);
            Guid caseEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 02", "Note 02", 2);

            #endregion

            #region Person Appointment
            Guid _personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            #endregion

            #region Case Note, Email, Letter, Phone Call, Task for Case
            Guid caseCaseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1));

            Guid caseLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);

            Guid casePhoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", _caseId, _caseTitle, _careDirectorQA_TeamId, "case");

            Guid caseTaskId = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId, _caseTitle, "case");

            Guid personCaseNoteId = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 01", "Note 01", personID, new DateTime(2020, 9, 1), _careDirectorQA_TeamId);


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Email")
                .TapSearchButton()

                .ValidateRecordPresent(personEmailRecordId1.ToString())
                .ValidateRecordPresent(personEmailRecordId2.ToString())
                .ValidateRecordPresent(caseEmailRecordId1.ToString())
                .ValidateRecordPresent(caseEmailRecordId2.ToString())

                .ValidateRecordNotPresent(caseCaseNoteId.ToString())
                .ValidateRecordNotPresent(_personAppointmentId1.ToString())
                .ValidateRecordNotPresent(caseLetterId.ToString())
                .ValidateRecordNotPresent(casePhoneCallId.ToString())
                .ValidateRecordNotPresent(caseTaskId.ToString())
                .ValidateRecordNotPresent(personCaseNoteId.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-11871")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Select Letter as the Activity Type - " +
            "Tap on the search button - Validate only Letter records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod59()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person Letter and Case Letter
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null);
            Guid personLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 02", "Notes 02",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 2), personID, _personFullname,
                "person", false, null, null, null);

            Guid caseLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 02",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 2), _caseId, _caseTitle,
                "case", false, null, null, null);

            #endregion

            #region Case Note, Appointment, Email, Phone call, Task, Person Case Note
            Guid caseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid _personAppointmentId = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);
            Guid caseEmailRecordId = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2);
            Guid casePhoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", _caseId, _caseTitle, _careDirectorQA_TeamId, "case");
            Guid caseTaskId = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId, _caseTitle, "case");
            Guid personCaseNoteId = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 01", "Note 01", personID, new DateTime(2020, 9, 1), _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Letter")
                .TapSearchButton()

                .ValidateRecordPresent(personLetterId1.ToString())
                .ValidateRecordPresent(personLetterId2.ToString())
                .ValidateRecordPresent(caseLetterId1.ToString())
                .ValidateRecordPresent(caseLetterId2.ToString())

                .ValidateRecordNotPresent(caseNoteId.ToString())
                .ValidateRecordNotPresent(_personAppointmentId.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId.ToString())
                .ValidateRecordNotPresent(casePhoneCallId.ToString())
                .ValidateRecordNotPresent(caseTaskId.ToString())
                .ValidateRecordNotPresent(personCaseNoteId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11872")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Select Phone Call as the Activity Type - " +
            "Tap on the search button - Validate only Phone Call records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod60()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person and Case Phone Call
            Guid personPhoneCallId1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Person Phone Call 01", "Notes 01", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullname, _careDirectorQA_TeamId, "person");
            Guid personPhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Person Phone Call 02", "Notes 02", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", personID, _personFullname, _careDirectorQA_TeamId, "person");
            Guid casePhoneCallId1 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", _caseId, _caseTitle, _careDirectorQA_TeamId, "case");
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Case Phone Call 02", "Notes 02", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", _caseId, _caseTitle, _careDirectorQA_TeamId, "case");

            #endregion

            #region Case Note, Appointment, Email, Letter, Task, Person Case Note
            Guid caseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid _personAppointmentId = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);
            Guid caseEmailRecordId = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2);
            Guid caseLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid caseTaskId = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId, _caseTitle, "case");
            Guid personCaseNoteId = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 01", "Note 01", personID, new DateTime(2020, 9, 1), _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Phone Call")
                .TapSearchButton()

                .ValidateRecordPresent(personPhoneCallId1.ToString())
                .ValidateRecordPresent(personPhoneCallId2.ToString())
                .ValidateRecordPresent(casePhoneCallId1.ToString())
                .ValidateRecordPresent(casePhoneCallId2.ToString())

                .ValidateRecordNotPresent(caseNoteId.ToString())
                .ValidateRecordNotPresent(_personAppointmentId.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId.ToString())
                .ValidateRecordNotPresent(caseLetterId.ToString())
                .ValidateRecordNotPresent(caseTaskId.ToString())
                .ValidateRecordNotPresent(personCaseNoteId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11873")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Select Task as the Activity Type - " +
            "Tap on the search button - Validate only Task records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod61()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person and Case Task
            Guid personTaskId1 = dbHelper.task.CreateTask("Person Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullname, "person");
            Guid personTaskId2 = dbHelper.task.CreateTask("Person Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), personID, _personFullname, "person");
            Guid caseTaskId1 = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId, _caseTitle, "case");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId, _caseTitle, "case");
            #endregion

            #region Case Note, Appointment, Email, Letter, Phone Call, Person Case Note
            Guid caseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid personAppointmentId = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);
            Guid caseEmailRecordId = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2);
            Guid caseLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", _caseId, _caseTitle, _careDirectorQA_TeamId, "case");
            Guid personCaseNoteId = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 01", "Note 01", personID, new DateTime(2020, 9, 1), _careDirectorQA_TeamId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Task")
                .TapSearchButton()

                .ValidateRecordPresent(personTaskId1.ToString())
                .ValidateRecordPresent(personTaskId2.ToString())
                .ValidateRecordPresent(caseTaskId1.ToString())
                .ValidateRecordPresent(caseTaskId2.ToString())

                .ValidateRecordNotPresent(caseNoteId.ToString())
                .ValidateRecordNotPresent(personAppointmentId.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId.ToString())
                .ValidateRecordNotPresent(caseLetterId.ToString())
                .ValidateRecordNotPresent(casePhoneCallId.ToString())
                .ValidateRecordNotPresent(personCaseNoteId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11874")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Remove the Date From and Date To field values - Select Case Note as the Activity Type - " +
            "Tap on the search button - Validate only Case Note records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod62()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Status
            Guid _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Depression", _careDirectorQA_TeamId, 140000001, false);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Contact Source

            Guid _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Auto_ContactSource", _careDirectorQA_TeamId);

            #endregion

            #region Inpatient Bed Type
            Guid _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Hospital Ward Specialty
            Guid _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "AutoInpatientAdmissionSource", new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource")[0];

            #endregion

            #region Provider_Hospital

            string _providerName = "Hospital" + _currentDateSuffix;
            Guid _provideHospitalId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId);

            #endregion

            #region Ward

            string _inpatientWardName = "Ward_" + _currentDateSuffix;
            Guid _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provideHospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            string _inpatientBayName = "Bay_" + _currentDateSuffix;
            Guid _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
            if (!inpatientBedExists)
                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
            Guid _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

            #endregion

            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionMethod", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod")[0];

            #endregion

            #region Inpatient Leave Type

            var inpatientLeaveTypeExists = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType").Any();
            if (!inpatientLeaveTypeExists)
                dbHelper.inpatientLeaveType.CreateInpatientLeaveType("Automation_LeaveAWOLType", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientLeaveTypeId = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType")[0];

            #endregion

            #region To Create Inpatient Case record

            DateTime admissionDate = new DateTime(2020, 9, 1);
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, new DateTime(2020, 9, 1), _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpatientAdmissionMethodId, _systemUserId, admissionDate, _provideHospitalId, _inpatientWardId, 1, new DateTime(2020, 9, 1), false, false, false, false, false, false, false, false, false, false);
            string _caseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

            #endregion

            #region Case Case Note
            Guid caseCaseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid personCaseNoteId = dbHelper.personCaseNote.CreatePersonCaseNote(_careDirectorQA_TeamId, "Person Case Note 01", "Note 01", personID, new DateTime(2020, 9, 1), _careDirectorQA_TeamId);

            #endregion

            #region Court Dates and Outcomes Case Note

            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeId = dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(_careDirectorQA_TeamId, personMhaLegalStatusId, personID, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeCaseNoteId = dbHelper.mhaCourtDateOutcomeCaseNote.CreateMHACourtDateOutcomeCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Court Dates and Outcomes 01 Case Note 01", "Note 01", mhaCourtDateAndOutcomeId, null, null, null, null, null,
                false, null, null, null, new DateTime(2020, 9, 1));
            #endregion

            #region Health Appointment Case Note
            #region Recurrence pattern

            var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
            if (!recurrencePatternExists)
                dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion            

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            #endregion

            #region Community/Clinic Appointment Contact Types

            var contactTypeId = commonMethodsDB.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Face To Face", new DateTime(2020, 1, 1), "1");

            #endregion

            #region Community/Clinic Appointment Location Types

            var _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Clients or patients home")[0];

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            var _appointmentsDataFormId = dbHelper.dataForm.GetByName("Appointments")[0];

            #endregion

            #region Contact Reason

            var _contactReasonId2 = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var _contactSourceId2 = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion

            #region Provider (Hospital)            
            var _providerId_Hospital = commonMethodsDB.CreateProvider("ActivitiesHospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "Test " + _currentDateSuffix, "Test " + _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "Team " + _currentDateSuffix, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);


            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2020, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0),
                                                            new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            #endregion

            #region Community Clinic Care Intervention            
            var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Therapy Group", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group")[0];

            communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Physical Rehab", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId2 = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab")[0];

            #endregion

            #region Community Case record
            var caseDate = new DateTime(2020, 2, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId2, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId2, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");

            #endregion

            #region Health Appointment Record
            DateTime healthAppointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Guid healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment(
                _careDirectorQA_TeamId, personID, _firstName + " " + _lastName, _appointmentsDataFormId, contactTypeId, _healthAppointmentReasonId, "Assessment", _communityCaseId1, newSystemUserId,
                communityClinicTeam, _communityClinicLocationTypesId, "Clients or patients home", newSystemUserId,
                "appointment information ...", healthAppointmentDate, new TimeSpan(11, 0, 0), new TimeSpan(11, 15, 0), healthAppointmentDate,
                false, null, null,
                null, null, null, null, null, null, null, null, null, null,
                false, false, false);


            string healthAppointmentTitle = dbHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID, "title")["title"].ToString();
            Guid healthAppointmentCaseNoteId = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID,
                _communityClinicCareInterventionId, null, null, null, null, null, healthAppointmentDate, "Health Appointment 01 Case Note 01", 2, "Note 01", false, false, false, null, null, null);

            #endregion

            #endregion

            #region Leave AWOL Case Note            
            var LeaveAWOLID = dbHelper.inpatientLeaveAwol.CreateLeaveAWOLRecord(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, personID, _caseId,
                DateTime.Now.Date, _inpatientLeaveTypeId, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, "systemuser", _systemUserId, _systemUsername);

            var leaveAWOLCaseNoteId = dbHelper.inpatientLeaveAwolCaseNote.CreateInpatientLeaveAwolCaseNote(_careDirectorQA_TeamId, _systemUserId, "Leave AWOL 01 Case Note 01",
                "Case Note (For Inpatient Leave Awol) Description", LeaveAWOLID, _caseId, personID, null, null, null, null, null,
                false, null, null, null,
                new DateTime(2020, 9, 1, 12, 0, 0));

            #endregion

            #region MHA Legal Status Case Note
            Guid personMhaLegalStatusCaseNoteId = dbHelper.personMHALegalStatusCaseNote.CreatePersonMHALegalStatusCaseNote(_careDirectorQA_TeamId, _systemUserId, "MHA Legal Status 01 Case Note 01", "Note 01",
                personMhaLegalStatusId, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1));

            #endregion

            #region Clinical Risk Status Case Note

            Guid _clinicalRiskStatus = commonMethodsDB.CreateClinicalRiskStatus(_careDirectorQA_TeamId, "Overall High", new DateTime(2020, 3, 18));

            Guid _personClinicalRiskStatusId = dbHelper.personClinicalRiskStatus.CreatePersonClinicalRiskStatus(personID, _careDirectorQA_TeamId,
                _clinicalRiskStatus, new DateTime(2020, 9, 1));
            Guid _personClinicalRiskStatusCaseNoteId = dbHelper.personClinicalRiskStatusCaseNote.CreatePersonClinicalRiskStatusCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Person Clinical Risk Status 01 Case Note 01", "Note 01", _personClinicalRiskStatusId, null, null, null, null, null, false, null, null, null,
                new DateTime(2020, 9, 1, 12, 0, 0));

            #endregion

            #region Person Care Plan Case Note                 

            Guid _carePlanTypeId = dbHelper.carePlanType.GetByName("Care Programme Approach Care Plan")[0];
            Guid _personCarePlan = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanTypeId, _systemUserId, personID, _communityCaseId1, _systemUserId, new DateTime(2020, 9, 17), 1, null, _careDirectorQA_TeamId);
            Guid _personCarePlanCaseNoteId = dbHelper.personCarePlanCaseNote.CreatePersonCarePlanCaseNote(_careDirectorQA_TeamId, _systemUserId, "Person Care Plan 01 Case Note 01",
                "Note 01", _personCarePlan, null, null, null, null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));

            #endregion

            #region Appointment, Email, Letter, Phone Call, Person Case Note, Task
            Guid personAppointmentId = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);
            Guid caseEmailRecordId = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2);
            Guid caseLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", _caseId, _caseTitle, _careDirectorQA_TeamId, "case");
            Guid caseTaskId = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId, _caseTitle, "case");
            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!")
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
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Case Note")
                .TapSearchButton()

                .ValidateRecordPresent(caseCaseNoteId.ToString(), "Case 01 Case Note 01")
                .ValidateRecordPresent(mhaCourtDateAndOutcomeCaseNoteId.ToString(), "Court Dates and Outcomes 01 Case Note 01")
                .ValidateRecordPresent(healthAppointmentCaseNoteId.ToString(), "Health Appointment 01 Case Note 01")
                .ValidateRecordPresent(leaveAWOLCaseNoteId.ToString(), "Leave AWOL 01 Case Note 01")
                .ValidateRecordPresent(personMhaLegalStatusCaseNoteId.ToString(), "MHA Legal Status 01 Case Note 01")
                .ValidateRecordPresent(_personCarePlanCaseNoteId.ToString(), "Person Care Plan 01 Case Note 01")
                .ValidateRecordPresent(personCaseNoteId.ToString(), "Person Case Note 01")
                .ValidateRecordPresent(_personClinicalRiskStatusCaseNoteId.ToString(), "Person Clinical Risk Status 01 Case Note 01")

                .ValidateRecordNotPresent(personAppointmentId.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId.ToString())
                .ValidateRecordNotPresent(caseLetterId.ToString())
                .ValidateRecordNotPresent(casePhoneCallId.ToString())
                .ValidateRecordNotPresent(caseTaskId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11875")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set the From and To dates - Tap on the search button - " +
            "Validate that only records that match the dates are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod63()
        {

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Case Note, Appointment, Email, Letter, Phone Call, Person Case Note
            Guid caseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid caseAppointmentId = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 2));
            Guid caseLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId, _caseTitle, personID, _personFullname,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 1, 12, 0, 0), _systemUserId, "AllActivities User1");
            Guid caseTaskId = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");

            Guid caseNoteId2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 02", "Note 02", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 4, 12, 0, 0));
            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 4), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 02", "Note 02", 2, new DateTime(2020, 9, 4));
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 02",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 4), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 02", "Notes 02", personID, "person", _personFullname,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId, _caseTitle, personID, _personFullname,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 4, 12, 0, 0), _systemUserId, "AllActivities User1");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 4, 7, 0, 0), _caseId, _caseTitle, "case");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .SelectDateType("Start/Due Date")
                .InsertFromDate("01/09/2020")
                .InsertToDate("02/09/2020")
                .TapSearchButton()

                .ValidateRecordPresent(caseNoteId.ToString(), "Case 01 Case Note 01")
                .ValidateRecordPresent(caseAppointmentId.ToString(), "Case 02 Appointment 01")
                .ValidateRecordPresent(caseEmailRecordId.ToString(), "Case Email 01")
                .ValidateRecordPresent(caseLetterId.ToString(), "Case Letter 01")
                .ValidateRecordPresent(casePhoneCallId.ToString(), "Case Phone Call 01")
                .ValidateRecordPresent(caseTaskId.ToString(), "Case Task 01")

                .ValidateRecordNotPresent(caseNoteId2.ToString())
                .ValidateRecordNotPresent(caseAppointmentId2.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId2.ToString())
                .ValidateRecordNotPresent(caseLetterId2.ToString())
                .ValidateRecordNotPresent(casePhoneCallId2.ToString())
                .ValidateRecordNotPresent(caseTaskId2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11876")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Created Date as the Date Type - Set the From and To dates - Tap on the search button - " +
            "Validate that only records created between the selected dates are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod64()
        {

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Case Note, Appointment, Email, Letter, Phone Call, Person Case Note
            Guid caseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid caseAppointmentId = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 2));
            Guid caseLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId, _caseTitle, personID, _personFullname,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 1, 12, 0, 0), _systemUserId, "AllActivities User1");
            Guid caseTaskId = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");

            Guid caseNoteId2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 02", "Note 02", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 4, 12, 0, 0));
            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 4), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 02", "Note 02", 2, new DateTime(2020, 9, 4));
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 02",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 4), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 02", "Notes 02", personID, "person", _personFullname,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId, _caseTitle, personID, _personFullname,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 4, 12, 0, 0), _systemUserId, "AllActivities User1");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 4, 7, 0, 0), _caseId, _caseTitle, "case");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .SelectDateType("Created Date")
                .InsertFromDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .InsertToDate(DateTime.Now.ToString("dd'/'MM'/'yyyy"))
                .TapSearchButton()

                .ValidateRecordPresent(caseNoteId.ToString())
                .ValidateRecordPresent(caseAppointmentId.ToString())
                .ValidateRecordPresent(caseEmailRecordId.ToString())
                .ValidateRecordPresent(caseLetterId.ToString())
                .ValidateRecordPresent(casePhoneCallId.ToString())
                .ValidateRecordPresent(caseTaskId.ToString())

                .ValidateRecordPresent(caseNoteId2.ToString())
                .ValidateRecordPresent(caseAppointmentId2.ToString())
                .ValidateRecordPresent(caseEmailRecordId2.ToString())
                .ValidateRecordPresent(caseLetterId2.ToString())
                .ValidateRecordPresent(casePhoneCallId2.ToString())
                .ValidateRecordPresent(caseTaskId2.ToString());

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .InsertToDate(DateTime.Now.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .TapSearchButton()

                .ValidateRecordNotPresent(caseNoteId.ToString())
                .ValidateRecordNotPresent(caseAppointmentId.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId.ToString())
                .ValidateRecordNotPresent(caseLetterId.ToString())
                .ValidateRecordNotPresent(casePhoneCallId.ToString())
                .ValidateRecordNotPresent(caseTaskId.ToString())

                .ValidateRecordNotPresent(caseNoteId2.ToString())
                .ValidateRecordNotPresent(caseAppointmentId2.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId2.ToString())
                .ValidateRecordNotPresent(caseLetterId2.ToString())
                .ValidateRecordNotPresent(casePhoneCallId2.ToString())
                .ValidateRecordNotPresent(caseTaskId2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11877")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set the Actual End (From) and Actual End (To) dates - Tap on the search button - " +
            "Validate that only records that match the Actual Dates are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod65()
        {

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId,
                _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Activities

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 16), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 16), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 02", "Note01", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 01", "Note01", null, new DateTime(2020, 9, 16), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 16), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);
            Guid personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Person Appointment 02", "Note01", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!")
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
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .InsertActualEndDateFrom("17/09/2020")
                .InsertActualEndDateTo("18/09/2020")
                .TapSearchButton()

                .ValidateRecordPresent(caseAppointmentId2.ToString(), "Case 02 Appointment 02")
                .ValidateRecordPresent(personAppointmentId2.ToString(), "Person Appointment 02")

                .ValidateRecordNotPresent(caseAppointmentId1.ToString())
                .ValidateRecordNotPresent(personAppointmentId1.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11878")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Select a Category - Tap on the search button - " +
            "Validate that only records that match the Category are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod66()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case and Case Note, All Activities

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            Guid caseNoteId1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 1));
            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, null, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 2),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid caseLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, _activityCategoryId, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_careDirectorQA_TeamId, userid, "Case Phone Call 01", _caseId, _caseTitle, "case", "Note 01", _activityCategoryId, null, null, null, null, _systemUserId, "systemuser", "AllActivities User1", 1, new DateTime(2020, 9, 1, 12, 0, 0), 1, personID, _caseId);
            Guid caseTaskId1 = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, _activityCategoryId, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");


            Guid caseNoteId2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 02", "Note 02", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 02", "Note 02", 2, new DateTime(2020, 9, 4));
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 02",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 02", "Notes 02", personID, "person", _personFullname,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId, _caseTitle, personID, _personFullname,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 1, 12, 0, 0), _systemUserId, "AllActivities User1");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapCategoryLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement("79a81b8a-9d45-e911-a2c5-005056926fe4");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapSearchButton()

                .ValidateRecordPresent(caseNoteId1.ToString())
                .ValidateRecordPresent(caseAppointmentId1.ToString())
                .ValidateRecordPresent(caseEmailRecordId1.ToString())
                .ValidateRecordPresent(caseLetterId1.ToString())
                .ValidateRecordPresent(casePhoneCallId1.ToString())
                .ValidateRecordPresent(caseTaskId1.ToString())

                .ValidateRecordNotPresent(caseNoteId2.ToString())
                .ValidateRecordNotPresent(caseAppointmentId2.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId2.ToString())
                .ValidateRecordNotPresent(caseLetterId2.ToString())
                .ValidateRecordNotPresent(casePhoneCallId2.ToString())
                .ValidateRecordNotPresent(caseTaskId2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11879")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Select a Sub-Category - Tap on the search button - " +
            "Validate that only records that match the Sub-Category are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod67()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case and Case Note, All Activities

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            Guid caseNoteId1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 1));
            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 2),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid caseLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, _activityCategoryId, _activitySubCategoryId, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_careDirectorQA_TeamId, userid, "Case Phone Call 01", _caseId, _caseTitle, "case", "Note 01", _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, "systemuser", "AllActivities User1", 1, new DateTime(2020, 9, 1, 12, 0, 0), 1, personID, _caseId);
            Guid caseTaskId1 = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, _activityCategoryId, _activitySubCategoryId, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");


            Guid caseNoteId2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 02", "Note 02", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 02", "Note 02", 2, new DateTime(2020, 9, 4));
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 02",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 02", "Notes 02", personID, "person", _personFullname,
                _systemUserId, "systemuser", "AllActivities User1", "", _caseId, _caseTitle, personID, _personFullname,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 1, 12, 0, 0), _systemUserId, "AllActivities User1");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapSubCategoryLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("home support").TapSearchButton().SelectResultElement("1515dfdd-9d45-e911-a2c5-005056926fe4");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapSearchButton()

                .ValidateRecordPresent(caseNoteId1.ToString())
                .ValidateRecordPresent(caseAppointmentId1.ToString())
                .ValidateRecordPresent(caseEmailRecordId1.ToString())
                .ValidateRecordPresent(caseLetterId1.ToString())
                .ValidateRecordPresent(casePhoneCallId1.ToString())
                .ValidateRecordPresent(caseTaskId1.ToString())

                .ValidateRecordNotPresent(caseNoteId2.ToString())
                .ValidateRecordNotPresent(caseAppointmentId2.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId2.ToString())
                .ValidateRecordNotPresent(caseLetterId2.ToString())
                .ValidateRecordNotPresent(casePhoneCallId2.ToString())
                .ValidateRecordNotPresent(caseTaskId2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11880")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Select a Responsible User - Tap on the search button - " +
            "Validate that only records that match the Responsible User are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod68()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case and Case Note, All Activities

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            Guid caseNoteId1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 1));
            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 2),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid caseLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, _systemUserId, _activityCategoryId, _activitySubCategoryId, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_careDirectorQA_TeamId, _systemUserId, "Case Phone Call 01", _caseId, _caseTitle, "case", "Note 01", _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, "systemuser", "AllActivities User1", 1, new DateTime(2020, 9, 1, 12, 0, 0), 1, personID, _caseId);
            Guid caseTaskId1 = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, _systemUserId, _activityCategoryId, _activitySubCategoryId, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");


            Guid caseNoteId2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, userid, "Case 01 Case Note 02", "Note 02", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, userid, userid, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 02", "Note 02", 2, new DateTime(2020, 9, 4));
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 02",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 02", "Notes 02", personID, "person", _personFullname,
                userid, "systemuser", "AllActivities User1", "", _caseId, _caseTitle, personID, _personFullname,
                _careDirectorQA_TeamId, "CareDirector QA", new DateTime(2020, 9, 1, 12, 0, 0), userid, "AllActivities User1");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 02", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapResponsibleUserLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("AllActivities User1").TapSearchButton().SelectResultElement(_systemUserId.ToString());

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapSearchButton()

                .ValidateRecordPresent(caseNoteId1.ToString())
                .ValidateRecordPresent(caseAppointmentId1.ToString())
                .ValidateRecordPresent(caseEmailRecordId1.ToString())
                .ValidateRecordPresent(caseLetterId1.ToString())
                .ValidateRecordPresent(casePhoneCallId1.ToString())
                .ValidateRecordPresent(caseTaskId1.ToString())

                .ValidateRecordNotPresent(caseNoteId2.ToString())
                .ValidateRecordNotPresent(caseAppointmentId2.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId2.ToString())
                .ValidateRecordNotPresent(caseLetterId2.ToString())
                .ValidateRecordNotPresent(casePhoneCallId2.ToString())
                .ValidateRecordNotPresent(caseTaskId2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11881")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Select a Responsible Team - Tap on the search button - " +
            "Validate that only records that match the Responsible Team are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod69()
        {

            #region Team

            var _careDirectorQA_TeamId2 = commonMethodsDB.CreateTeam("CareDirectorTeamB", null, _careDirectorQA_BusinessUnitId, "905668", "CareDirectorTeamB@careworkstempmail.com", "CareDirectorTeamB", "020 123436");
            var _systemUserId2 = commonMethodsDB.CreateSystemUserRecord("AllActivitiesUser2", "AllActivities", "User2", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId2, _languageId, _authenticationproviderid);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case and Case Note, All Activities

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            Guid caseNoteId1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 1));
            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 2),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid caseLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, _systemUserId, _activityCategoryId, _activitySubCategoryId, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_careDirectorQA_TeamId, _systemUserId, "Case Phone Call 01", _caseId, _caseTitle, "case", "Note 01", _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, "systemuser", "AllActivities User1", 1, new DateTime(2020, 9, 1, 12, 0, 0), 1, personID, _caseId);
            Guid caseTaskId1 = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, _systemUserId, _activityCategoryId, _activitySubCategoryId, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");


            Guid caseNoteId2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId2, _systemUserId2, "Case 01 Case Note 02", "Note 02", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1, 12, 0, 0));
            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId2, personID, null, null, null, null, null, _systemUserId2, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId2, personID, _systemUserId2, _systemUserId2, "AllActivities User2", "systemuser", _caseId, "case", _caseTitle, "Case Email 02", "Note 02", 2, new DateTime(2020, 9, 4));
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 02",
                null, _careDirectorQA_TeamId2, _systemUserId2, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null);
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase("Case Phone Call 02", "Notes 02", personID, "person", _personFullname,
                _systemUserId2, "systemuser", "AllActivities User2", "", _caseId, _caseTitle, personID, _personFullname,
                _careDirectorQA_TeamId2, "CareDirector QA", new DateTime(2020, 9, 1, 12, 0, 0), _systemUserId2, "AllActivities User2");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 02", _careDirectorQA_TeamId2, _systemUserId2, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapResponsibleTeamLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad().TypeSearchQuery("CareDirectorTeamB").TapSearchButton().SelectResultElement(_careDirectorQA_TeamId2.ToString());

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapSearchButton()

                .ValidateRecordPresent(caseNoteId2.ToString())
                .ValidateRecordPresent(caseAppointmentId2.ToString())
                .ValidateRecordPresent(caseEmailRecordId2.ToString())
                .ValidateRecordPresent(caseLetterId2.ToString())
                .ValidateRecordPresent(casePhoneCallId2.ToString())
                .ValidateRecordPresent(caseTaskId2.ToString())

                .ValidateRecordNotPresent(caseNoteId1.ToString())
                .ValidateRecordNotPresent(caseAppointmentId1.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId1.ToString())
                .ValidateRecordNotPresent(caseLetterId1.ToString())
                .ValidateRecordNotPresent(casePhoneCallId1.ToString())
                .ValidateRecordNotPresent(caseTaskId1.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11882")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - set the Status to 'Cancelled/Failed to Send' - Tap on the search button - " +
            "Validate that only records that match the Status are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod70()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Recurrence pattern

            var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
            if (!recurrencePatternExists)
                dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion            

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            #endregion

            #region Community/Clinic Appointment Contact Types

            var contactTypeId = commonMethodsDB.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Face To Face", new DateTime(2020, 1, 1), "1");

            #endregion

            #region Community/Clinic Appointment Location Types

            var _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Clients or patients home")[0];

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            var _appointmentsDataFormId = dbHelper.dataForm.GetByName("Appointments")[0];

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion

            #region Provider (Hospital)            
            var _providerId_Hospital = commonMethodsDB.CreateProvider("ActivitiesHospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "Test " + _currentDateSuffix, "Test " + _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "Team " + _currentDateSuffix, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);


            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2020, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0),
                                                            new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            #endregion

            #region Community Clinic Care Intervention            
            var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Therapy Group", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group")[0];

            communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Physical Rehab", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId2 = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab")[0];

            #endregion

            #region Community Case record
            var caseDate = new DateTime(2020, 9, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_communityCaseId1, "title")["title"];

            #endregion

            #region Health Appointment Record and Activities
            DateTime healthAppointmentDate = new DateTime(2020, 9, 1);

            Guid healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment(
                _careDirectorQA_TeamId, personID, _firstName + " " + _lastName, _appointmentsDataFormId, contactTypeId, _healthAppointmentReasonId, "Assessment", _communityCaseId1, newSystemUserId,
                communityClinicTeam, _communityClinicLocationTypesId, "Clients or patients home", newSystemUserId,
                "appointment information ...", healthAppointmentDate, new TimeSpan(11, 0, 0), new TimeSpan(11, 15, 0), healthAppointmentDate,
                false, null, null,
                null, null, null, null, null, null, null, null, null, null,
                false, false, false);
            Guid healthAppointmentCaseNoteId1 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID,
                _communityClinicCareInterventionId, null, null, null, null, null, healthAppointmentDate, "Health Appointment 01 Case Note 01", 3, "Note 01", false, false, false, null, null, null);
            Guid healthAppointmentCaseNoteId2 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID,
                _communityClinicCareInterventionId, null, null, null, null, null, healthAppointmentDate, "Health Appointment 01 Case Note 01", 2, "Note 01", false, false, false, null, null, null);

            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _communityCaseId1, "case", _caseTitle, "Case Email 01", "Note 01", 1, new DateTime(2020, 9, 1),
                null, null, null, null, null);
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null);
            Guid personTaskId1 = dbHelper.task.CreateTask("Person Task 01", "Notes 01", _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), personID, _personFullname, "person");
            Guid _caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _communityCaseId1, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectStatusByText("Cancelled/Failed to Send")
                .TapSearchButton()

                .ValidateRecordPresent(healthAppointmentCaseNoteId1.ToString())

                .ValidateRecordNotPresent(caseEmailRecordId1.ToString())
                .ValidateRecordNotPresent(personLetterId1.ToString())
                .ValidateRecordNotPresent(personTaskId1.ToString())
                .ValidateRecordNotPresent(healthAppointmentCaseNoteId2.ToString())//
                .ValidateRecordNotPresent(_caseAppointmentId1.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11883")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - set the Status to 'Completed/Sent' - Tap on the search button - " +
            "Validate that only records that match the Status are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod71()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Recurrence pattern

            var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
            if (!recurrencePatternExists)
                dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion            

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            #endregion

            #region Community/Clinic Appointment Contact Types

            var contactTypeId = commonMethodsDB.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Face To Face", new DateTime(2020, 1, 1), "1");

            #endregion

            #region Community/Clinic Appointment Location Types

            var _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Clients or patients home")[0];

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            var _appointmentsDataFormId = dbHelper.dataForm.GetByName("Appointments")[0];

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion

            #region Provider (Hospital)            
            var _providerId_Hospital = commonMethodsDB.CreateProvider("ActivitiesHospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "Test " + _currentDateSuffix, "Test " + _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "Team " + _currentDateSuffix, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);


            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2020, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0),
                                                            new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            #endregion

            #region Community Clinic Care Intervention            
            var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Therapy Group", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group")[0];

            communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Physical Rehab", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId2 = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab")[0];

            #endregion

            #region Community Case record
            var caseDate = new DateTime(2020, 9, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_communityCaseId1, "title")["title"];

            #endregion

            #region Health Appointment Record and Activities
            DateTime healthAppointmentDate = new DateTime(2020, 9, 1);

            Guid healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment(
                _careDirectorQA_TeamId, personID, _firstName + " " + _lastName, _appointmentsDataFormId, contactTypeId, _healthAppointmentReasonId, "Assessment", _communityCaseId1, newSystemUserId,
                communityClinicTeam, _communityClinicLocationTypesId, "Clients or patients home", newSystemUserId,
                "appointment information ...", healthAppointmentDate, new TimeSpan(11, 0, 0), new TimeSpan(11, 15, 0), healthAppointmentDate,
                false, null, null,
                null, null, null, null, null, null, null, null, null, null,
                false, false, false);
            Guid healthAppointmentCaseNoteId1 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID,
                _communityClinicCareInterventionId, null, null, null, null, null, healthAppointmentDate, "Health Appointment 01 Case Note 01", 3, "Note 01", false, false, false, null, null, null);
            Guid healthAppointmentCaseNoteId2 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID,
                _communityClinicCareInterventionId, null, null, null, null, null, healthAppointmentDate, "Health Appointment 01 Case Note 01", 2, "Note 01", false, false, false, null, null, null);

            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _communityCaseId1, "case", _caseTitle, "Case Email 01", "Note 01", 1, new DateTime(2020, 9, 1),
                null, null, null, null, null);
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null);
            Guid personTaskId1 = dbHelper.task.CreateTask("Person Task 01", "Notes 01", _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), personID, _personFullname, "person");
            Guid _caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _communityCaseId1, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectStatusByText("Completed/Sent")
                .TapSearchButton()

                .ValidateRecordPresent(healthAppointmentCaseNoteId2.ToString())

                .ValidateRecordNotPresent(caseEmailRecordId1.ToString())
                .ValidateRecordNotPresent(personLetterId1.ToString())
                .ValidateRecordNotPresent(personTaskId1.ToString())
                .ValidateRecordNotPresent(healthAppointmentCaseNoteId1.ToString())//
                .ValidateRecordNotPresent(_caseAppointmentId1.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11884")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - set the Status to 'Draft/In Progress/Open' - Tap on the search button - " +
            "Validate that only records that match the Status are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod72()
        {

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();
            #endregion

            #region Recurrence pattern

            var recurrencePatternExists = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").Any();
            if (!recurrencePatternExists)
                dbHelper.recurrencePattern.CreateRecurrencePattern(1, 1);

            var _recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 1 days").FirstOrDefault();

            #endregion            

            #region Health Appointment Reason

            var _healthAppointmentReasonId = commonMethodsDB.CreateHealthAppointmentReason(_careDirectorQA_TeamId, "Assessment", new DateTime(2020, 1, 1), "1", null);

            #endregion

            #region Community/Clinic Appointment Contact Types

            var contactTypeId = commonMethodsDB.CreateHealthAppointmentContactType(_careDirectorQA_TeamId, "Face To Face", new DateTime(2020, 1, 1), "1");

            #endregion

            #region Community/Clinic Appointment Location Types

            var _communityClinicLocationTypesId = dbHelper.healthAppointmentLocationType.GetByName("Clients or patients home")[0];

            #endregion

            #region Contact Administrative Category

            var _contactAdministrativeCategory = commonMethodsDB.CreateContactAdministrativeCategory(_careDirectorQA_TeamId, "Test_Administrative Category", new DateTime(2020, 1, 1));

            #endregion

            #region Case Service Type Requested

            var _caseServiceTypeRequestedid = commonMethodsDB.CreateCaseServiceTypeRequested(_careDirectorQA_TeamId, "Advice and Consultation", new DateTime(2020, 1, 1));

            #endregion

            #region Case Status

            var _caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Further Information").First();

            #endregion

            #region Data Form Community Health Case

            var _dataFormId_CommunityHealthCase = dbHelper.dataForm.GetByName("CommunityHealthCase").FirstOrDefault();

            var _appointmentsDataFormId = dbHelper.dataForm.GetByName("Appointments")[0];

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Test_Contact (Comm)", _careDirectorQA_TeamId);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Family", _careDirectorQA_TeamId);

            #endregion

            #region Provider (Hospital)            
            var _providerId_Hospital = commonMethodsDB.CreateProvider("ActivitiesHospital" + _currentDateSuffix, _careDirectorQA_TeamId, 3);

            #endregion

            #region Community Clinic Team
            var communityClinicTeam = dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(_careDirectorQA_TeamId, _providerId_Hospital, _careDirectorQA_TeamId, "Test " + _currentDateSuffix, "Test " + _currentDateSuffix);
            var _diaryViewSetupId = dbHelper.communityClinicDiaryViewSetup.CreateCommunityClinicDiaryViewSetup(_careDirectorQA_TeamId, communityClinicTeam, "Team " + _currentDateSuffix, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0), 500, 100, 500);


            #endregion

            #region Create User
            string _systemUsername2 = "HealthAppointmentUser2_" + _currentDateSuffix;
            string _systemUserFullName2 = "HealthAppointmentUser2 " + _currentDateSuffix;
            var newSystemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "HealthAppointmentUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);


            #endregion

            #region Create New User WorkSchedule

            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(newSystemUserId).Any())
                dbHelper.userWorkSchedule.CreateUserWorkSchedule("Work Schedule", newSystemUserId, _careDirectorQA_TeamId, _recurrencePatternId, new DateTime(2020, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 55, 0));

            #endregion

            #region Community Clinic Linked Professional

            dbHelper.communityClinicLinkedProfessional.CreateCommunityClinicLinkedProfessional(_careDirectorQA_TeamId, _diaryViewSetupId, newSystemUserId, new DateTime(2020, 1, 1), new TimeSpan(1, 0, 0),
                                                            new TimeSpan(23, 55, 0), _recurrencePatternId, _systemUserFullName2);
            #endregion

            #region Community Clinic Care Intervention            
            var communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Therapy Group", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId = dbHelper.communityClinicCareIntervention.GetByName("Therapy Group")[0];

            communityClinicCareIntervention = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab").Any();
            if (!communityClinicCareIntervention)
                dbHelper.communityClinicCareIntervention.CreateCommunityClinicCareIntervention(_careDirectorQA_TeamId, "Physical Rehab", new DateTime(2020, 1, 1));
            var _communityClinicCareInterventionId2 = dbHelper.communityClinicCareIntervention.GetByName("Physical Rehab")[0];

            #endregion

            #region Community Case record
            var caseDate = new DateTime(2020, 9, 1, 9, 0, 0);
            var _communityCaseId1 = dbHelper.Case.CreateCommunityHealthCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, communityClinicTeam, _systemUserId, _caseStatusId, _contactReasonId, _contactAdministrativeCategory,
                    _caseServiceTypeRequestedid, _dataFormId_CommunityHealthCase, _contactSourceId, caseDate, caseDate, caseDate, caseDate, "a relevant directory where a user would be entitled to make thoroughly clear what is");
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_communityCaseId1, "title")["title"];

            #endregion

            #region Health Appointment Record and Activities
            DateTime healthAppointmentDate = new DateTime(2020, 9, 1);

            Guid healthAppointmentID = dbHelper.healthAppointment.CreateHealthAppointment(
                _careDirectorQA_TeamId, personID, _firstName + " " + _lastName, _appointmentsDataFormId, contactTypeId, _healthAppointmentReasonId, "Assessment", _communityCaseId1, newSystemUserId,
                communityClinicTeam, _communityClinicLocationTypesId, "Clients or patients home", newSystemUserId,
                "appointment information ...", healthAppointmentDate, new TimeSpan(11, 0, 0), new TimeSpan(11, 15, 0), healthAppointmentDate,
                false, null, null,
                null, null, null, null, null, null, null, null, null, null,
                false, false, false);
            Guid healthAppointmentCaseNoteId1 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID,
                _communityClinicCareInterventionId, null, null, null, null, null, healthAppointmentDate, "Health Appointment 01 Case Note 01", 3, "Note 01", false, false, false, null, null, null);
            Guid healthAppointmentCaseNoteId2 = dbHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(_careDirectorQA_TeamId, _systemUserId, _communityCaseId1, personID, healthAppointmentID,
                _communityClinicCareInterventionId, null, null, null, null, null, healthAppointmentDate, "Health Appointment 01 Case Note 01", 2, "Note 01", false, false, false, null, null, null);

            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _communityCaseId1, "case", _caseTitle, "Case Email 01", "Note 01", 1, new DateTime(2020, 9, 1),
                null, null, null, null, null);
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null);
            Guid personTaskId1 = dbHelper.task.CreateTask("Person Task 01", "Notes 01", _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), personID, _personFullname, "person");
            Guid _caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, userid, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 1, 20), new TimeSpan(12, 0, 0),
                _communityCaseId1, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectStatusByText("Draft/In Progress/Open")
                .TapSearchButton()

                .ValidateRecordPresent(caseEmailRecordId1.ToString())
                .ValidateRecordPresent(personLetterId1.ToString())
                .ValidateRecordPresent(personTaskId1.ToString())

                .ValidateRecordNotPresent(healthAppointmentCaseNoteId2.ToString())
                .ValidateRecordNotPresent(healthAppointmentCaseNoteId1.ToString())
                .ValidateRecordNotPresent(_caseAppointmentId1.ToString())
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-11885")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - set the Status to 'Scheduled/Pending Send' - Tap on the search button - " +
            "Validate that only records that match the Status are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod73()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case and All Activities

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);
            Guid personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 02", "Note 02", "",
                new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 1),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid caseEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 02", "Note 02", 2, new DateTime(2020, 9, 17),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 01", "Note 01", 2, new DateTime(2020, 9, 1),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid personEmailRecordId2 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _caseTitle, "Person Email 02", "Note 02", 2, new DateTime(2020, 9, 17),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectStatusByText("Scheduled/Pending Send")
                .TapSearchButton()

                .ValidateRecordPresent(caseAppointmentId1.ToString())
                .ValidateRecordPresent(caseAppointmentId2.ToString())
                .ValidateRecordPresent(personAppointmentId1.ToString())
                .ValidateRecordPresent(personAppointmentId2.ToString())

                .ValidateRecordNotPresent(caseEmailRecordId1.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId2.ToString())
                .ValidateRecordNotPresent(personEmailRecordId1.ToString())
                .ValidateRecordNotPresent(personEmailRecordId2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11886")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Select Case Notes Only - Tap on the search button - " +
            "Validate that only records that match the Case Notes Only are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_AllActivities_UITestMethod77()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            string _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            string personNumber = dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"].ToString();

            #endregion

            #region Case Status
            Guid _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Depression", _careDirectorQA_TeamId, 140000001, false);

            #endregion

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Contact Source

            Guid _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Auto_ContactSource", _careDirectorQA_TeamId);

            #endregion

            #region Inpatient Bed Type
            Guid _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Hospital Ward Specialty
            Guid _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Contact Inpatient Admission Source

            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource").Any();
            if (!inpatientAdmissionSourceExists)
                dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(_careDirectorQA_TeamId, "AutoInpatientAdmissionSource", new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionSourceId = dbHelper.inpatientAdmissionSource.GetByName("AutoInpatientAdmissionSource")[0];

            #endregion

            #region Provider_Hospital

            string _providerName = "Hospital" + _currentDateSuffix;
            Guid _provideHospitalId = commonMethodsDB.CreateProvider(_providerName, _careDirectorQA_TeamId);

            #endregion

            #region Ward

            string _inpatientWardName = "Ward_" + _currentDateSuffix;
            Guid _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_careDirectorQA_TeamId, _provideHospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            string _inpatientBayName = "Bay_" + _currentDateSuffix;
            Guid _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_careDirectorQA_TeamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var inpatientBedExists = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId).Any();
            if (!inpatientBedExists)
                dbHelper.inpatientBed.CreateInpatientBed(_careDirectorQA_TeamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");
            Guid _inpatientBedId = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(_inpatientBayId)[0];

            #endregion

            #region InpatientAdmissionMethod

            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod").Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod("Automation_AdmissionMethod", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientAdmissionMethodId = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName("Automation_AdmissionMethod")[0];

            #endregion

            #region Inpatient Leave Type

            var inpatientLeaveTypeExists = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType").Any();
            if (!inpatientLeaveTypeExists)
                dbHelper.inpatientLeaveType.CreateInpatientLeaveType("Automation_LeaveAWOLType", _careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, new DateTime(2020, 1, 1));
            Guid _inpatientLeaveTypeId = dbHelper.inpatientLeaveType.GetInpatientLeaveTypeByName("Automation_LeaveAWOLType")[0];

            #endregion

            #region To Create Inpatient Case record

            DateTime admissionDate = new DateTime(2020, 9, 1);
            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_careDirectorQA_TeamId, personID, DateTime.Now.Date, _systemUserId, "hdsa", _systemUserId, _admission_CaseStatusId, _contactReasonId, new DateTime(2020, 9, 1), _dataFormId, _contactSourceId, _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpatientAdmissionMethodId, _systemUserId, admissionDate, _provideHospitalId, _inpatientWardId, 1, new DateTime(2020, 9, 1), false, false, false, false, false, false, false, false, false, false);
            string _caseTitle = (string)dbHelper.Case.GetCaseByID(_caseId, "title")["title"];

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Case Notes and Activities

            Guid caseNoteId = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId,
                _activityReasonId, _activityPriorityId, false, null, null, null, new DateTime(2020, 9, 1));
            Guid mhaSectionSetupId = commonMethodsDB.CreateMHASectionSetup("Adhoc Section", "1234", _careDirectorQA_TeamId, "Testing");
            Guid personMhaLegalStatusId = dbHelper.personMHALegalStatus.CreatePersonMHALegalStatus(_careDirectorQA_TeamId, personID, mhaSectionSetupId, _systemUserId, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeId = dbHelper.mhaCourtDateOutcome.CreateMHACourtDateOutcome(_careDirectorQA_TeamId, personMhaLegalStatusId, personID, new DateTime(2020, 9, 1));
            Guid mhaCourtDateAndOutcomeCaseNoteId = dbHelper.mhaCourtDateOutcomeCaseNote.CreateMHACourtDateOutcomeCaseNote(_careDirectorQA_TeamId, _systemUserId,
                "Court Dates and Outcomes 01 Case Note 01", "Note 01", mhaCourtDateAndOutcomeId, null, null, null, null, null,
                false, null, null, null, new DateTime(2020, 9, 1));
            Guid caseLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null, true);

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, null,
                "Case 01 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);
            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 1),
                _activityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid personLetterId = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null, false);
            Guid casePhoneCallId = dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Case Phone Call 01", "Notes 01", personID, "person", _personFullname,
                userid, "systemuser", "Security Test User Admin", "", _caseId, _caseTitle, _careDirectorQA_TeamId, "case");
            Guid caseTaskId = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 7, 20, 7, 0, 0), _caseId, _caseTitle, "case");
            #endregion


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapCaseNoteOnlyRadioButton()
                .TapSearchButton()

                .ValidateRecordPresent(caseNoteId.ToString())
                .ValidateRecordPresent(mhaCourtDateAndOutcomeCaseNoteId.ToString())
                .ValidateRecordPresent(caseLetterId.ToString())

                .ValidateRecordNotPresent(caseAppointmentId1.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId1.ToString())
                .ValidateRecordNotPresent(personLetterId.ToString())
                .ValidateRecordNotPresent(casePhoneCallId.ToString())
                .ValidateRecordNotPresent(caseTaskId.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11887")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Select a multiple Categories - Tap on the search button - " +
            "Validate that only records that match the Categories are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod78()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
            Guid _otherActivityReasonId = commonMethodsDB.CreateActivityReason(new Guid("5399de25-9d45-e911-a2c5-005056926fe4"), "Other", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Priority

            Guid _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("1e164c51-9d45-e911-a2c5-005056926fe4"), "High", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Outcome

            Guid _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case and All Activities

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            Guid caseCaseNoteId1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                _activityReasonId, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid caseCaseNoteId2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 02", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 2));

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, null, _activityReasonId, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note01", null, new DateTime(2020, 9, 2), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 2), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);


            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 1),
                _otherActivityReasonId, _activityPriorityId, _activityOutcomeId, _activityCategoryId, _activitySubCategoryId);
            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 01", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, null, null);

            Guid caseLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, _otherActivityReasonId, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null, false);
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 2), _caseId, _caseTitle,
                "case", false, null, null, null, false);

            Guid casePhoneCallId1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_careDirectorQA_TeamId, userid, "Case Phone Call 01", _caseId, _caseTitle, "case", "Note 01", _activityCategoryId, null, null, _activityReasonId, null, _systemUserId, "systemuser", "AllActivities User1", 1, new DateTime(2020, 9, 1, 12, 0, 0), 1, personID, _caseId);
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_careDirectorQA_TeamId, userid, "Case Phone Call 02", _caseId, _caseTitle, "case", "Note 01", _activityCategoryId, null, null, null, null, _systemUserId, "systemuser", "AllActivities User1", 1, new DateTime(2020, 9, 2, 12, 0, 0), 1, personID, _caseId);

            Guid caseTaskId1 = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, _activityCategoryId, null, null, _activityReasonId, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 01", _careDirectorQA_TeamId, userid, _activityCategoryId, null, null, null, null, null,
                personID, new DateTime(2020, 9, 2, 7, 0, 0), _caseId, _caseTitle, "case");


            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .TapReasonLookupbutton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Assessment").TapSearchButton().AddElementToList(_activityReasonId.ToString())
                .TypeSearchQuery("Other").TapSearchButton().SelectResultElement(_otherActivityReasonId.ToString());

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapSearchButton()

                .ValidateRecordPresent(caseCaseNoteId1.ToString())
                .ValidateRecordPresent(caseAppointmentId1.ToString())
                .ValidateRecordPresent(caseEmailRecordId1.ToString())
                .ValidateRecordPresent(caseLetterId1.ToString())
                .ValidateRecordPresent(casePhoneCallId1.ToString())
                .ValidateRecordPresent(caseTaskId1.ToString())

                .ValidateRecordNotPresent(caseCaseNoteId2.ToString())
                .ValidateRecordNotPresent(caseAppointmentId2.ToString())
                .ValidateRecordNotPresent(personEmailRecordId1.ToString())
                .ValidateRecordNotPresent(caseLetterId2.ToString())
                .ValidateRecordNotPresent(casePhoneCallId2.ToString())
                .ValidateRecordNotPresent(caseTaskId2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11888")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Activity Type to Appointment - Set Category to advice - Tap on the search button - " +
            "Validate that only records that match the Activity Type and Category are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod79()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case and All Activities

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            Guid caseCaseNoteId1 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 01", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 1));
            Guid caseCaseNoteId2 = dbHelper.caseCaseNote.CreateCaseCaseNote(_careDirectorQA_TeamId, _systemUserId, "Case 01 Case Note 02", "Note 01", _caseId, personID, null, null, null,
                null, null, false, null, null, null, new DateTime(2020, 9, 2));

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, null, null, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note01", null, new DateTime(2020, 9, 2), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 2), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);


            Guid caseEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", _caseId, "case", _caseTitle, "Case Email 01", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, null, null);
            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 01", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, null, null);

            Guid caseLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 01", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), _caseId, _caseTitle,
                "case", false, null, null, null, false);
            Guid caseLetterId2 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Case Letter 02", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 2), _caseId, _caseTitle,
                "case", false, null, null, null, false);

            Guid casePhoneCallId1 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_careDirectorQA_TeamId, userid, "Case Phone Call 01", _caseId, _caseTitle, "case", "Note 01", _activityCategoryId, null, null, null, null, _systemUserId, "systemuser", "AllActivities User1", 1, new DateTime(2020, 9, 1, 12, 0, 0), 1, personID, _caseId);
            Guid casePhoneCallId2 = dbHelper.phoneCall.CreatePhoneCallRecordForCase(_careDirectorQA_TeamId, userid, "Case Phone Call 02", _caseId, _caseTitle, "case", "Note 01", _activityCategoryId, null, null, null, null, _systemUserId, "systemuser", "AllActivities User1", 1, new DateTime(2020, 9, 2, 12, 0, 0), 1, personID, _caseId);

            Guid caseTaskId1 = dbHelper.task.CreateTask("Case Task 01", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 1, 7, 0, 0), _caseId, _caseTitle, "case");
            Guid caseTaskId2 = dbHelper.task.CreateTask("Case Task 02", "Notes 01", _careDirectorQA_TeamId, userid, null, null, null, null, null, null,
                personID, new DateTime(2020, 9, 2, 7, 0, 0), _caseId, _caseTitle, "case");


            #endregion



            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Appointment")
                .TapCategoryLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapSearchButton()

                .ValidateRecordPresent(caseAppointmentId1.ToString())

                .ValidateRecordNotPresent(caseCaseNoteId1.ToString())
                .ValidateRecordNotPresent(caseCaseNoteId2.ToString())
                .ValidateRecordNotPresent(caseAppointmentId2.ToString())
                .ValidateRecordNotPresent(caseEmailRecordId1.ToString())
                .ValidateRecordNotPresent(personEmailRecordId1.ToString())
                .ValidateRecordNotPresent(caseLetterId1.ToString())
                .ValidateRecordNotPresent(caseLetterId2.ToString())
                .ValidateRecordNotPresent(casePhoneCallId1.ToString())
                .ValidateRecordNotPresent(casePhoneCallId2.ToString())
                .ValidateRecordNotPresent(caseTaskId1.ToString())
                .ValidateRecordNotPresent(caseTaskId2.ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-11889")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Activity Type to Email - Set Category to Mental Heath Care Contacts - Tap on the search button - " +
            "Tap on a person case note record - validate that the user is redirected to the Person Case Note record page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod80()
        {
            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Activity Categories                

            Guid _mentalHealthCareContactActivityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Health Care Contacts", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person Email

            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 02", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, _mentalHealthCareContactActivityCategoryId, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad(true, true, false, true, true, true)
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Email")
                .TapCategoryLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery("Mental Health Care Contacts").TapSearchButton().SelectResultElement(_mentalHealthCareContactActivityCategoryId.ToString());

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .TapSearchButton()
                .OpenActivityRecord(personEmailRecordId1.ToString());

            personEmailRecordPage
                .WaitForPersonEmailRecordPageToLoad("Person Email 02");
        }

        [TestProperty("JiraIssueID", "CDV6-11890")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Activity Type to Appointment - Tap on the search button - " +
            "Tap on the Show List Button - Validate that the appointment records are displayed in the List View Mode")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod81()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
            Guid _mentalHealthCareContactActivityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Health Care Contacts", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person and Case Appointment
            Guid personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 02", "Note 02", "",
                new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, _activityReasonId, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            #region Person Email

            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 02", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, _mentalHealthCareContactActivityCategoryId, null);

            #endregion

            #region Person Letter
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 02", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Appointment")
                .TapSearchButton()

                .ValidateExpandCollapseAllButtonVisibility_ListViewMode(false)//at this point the list view mode should not be active yet

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), false) //at this point the list view mode should not be active yet
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), false) //at this point the list view mode should not be active yet
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), false) //at this point the list view mode should not be active yet
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), false) //at this point the list view mode should not be active yet
                .ValidateEditRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateEditRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), false) //at this point the list view mode should not be active yet
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), false) //at this point the list view mode should not be active yet
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), false) //at this point the list view mode should not be active yet
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), false) //at this point the list view mode should not be active yet
                .ValidateExpandRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateExpandRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, false) //at this point the list view mode should not be active yet
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, false) //at this point the list view mode should not be active yet
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, false) //at this point the list view mode should not be active yet
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, false) //at this point the list view mode should not be active yet
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .TapChangeViewButton()

                .ValidateExpandCollapseAllButtonVisibility_ListViewMode(true)//List view mode should be active now

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateEditRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateExpandRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 2, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 2, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible


                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 2, "Subject: Person Appointment 02")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 2, "Subject: Case 02 Appointment 02")

                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 2, "Subject: Person Appointment 01")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 2, "Subject: Case 02 Appointment 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11891")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Activity Type to Appointment - Tap on the search button - " +
            "Tap on the Show List Button - What for the List View Mode to be displayed - Click on the Expand button for the second appointment record - " +
            "Validate that only that record gets expanded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod82()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
            Guid _mentalHealthCareContactActivityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Health Care Contacts", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person and Case Appointment
            Guid personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 02", "Note 02", "",
                new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, _activityReasonId, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            #region Person Email

            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 02", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, _mentalHealthCareContactActivityCategoryId, null);

            #endregion

            #region Person Letter
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 02", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            var fields = dbHelper.appointment.GetAppointmentByID(caseAppointmentId2, "createdon", "modifiedon");
            string createdon = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");
            string modifiedon = ((DateTime)fields["modifiedon"]).ToLocalTime().ToString("dd'/'MM'/'yyyy HH:mm:ss");

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Appointment")
                .TapSearchButton()
                .TapChangeViewButton()

                .ValidateExpandCollapseAllButtonVisibility_ListViewMode(true)//List view mode should be active now

                .TapExpandRecordButton_ListViewMode(caseAppointmentId2.ToString()) //expand the 2nd appointment record

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateEditRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateExpandRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 2, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 2, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 3, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 4, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 5, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 6, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 7, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 8, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 9, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 10, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 11, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 12, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 13, true) //this record is expanded, the line should be visible


                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 2, "Subject: Person Appointment 02")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 2, "Subject: Case 02 Appointment 02")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 1, "Activity: Appointment")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 2, "Description: Note02")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 3, "Status: Scheduled")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 4, "Start/Due Date: 17/09/2020 11:30:00")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 5, "Actual End: 17/09/2020 12:00:00")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 6, "Case Note: No")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 7, "Regarding Type: Case")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 8, "Responsible User: AllActivities User1")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 9, "Responsible Team: CareDirector QA")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 10, "Modified By: " + _defaultUserFullname)
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 11, "Modified On: " + modifiedon)
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 12, "Created By: " + _defaultUserFullname)
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 13, "Created On: " + createdon)

                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 2, "Subject: Person Appointment 01")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 2, "Subject: Case 02 Appointment 01");
        }

        [TestProperty("JiraIssueID", "CDV6-11892")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Activity Type to Appointment - Tap on the search button - " +
            "Tap on the Show List Button - What for the List View Mode to be displayed - Click on the Expand button for the second appointment record - " +
            "Validate that only that record gets expanded - Click on the second appointment Collapse button - Validate that the record gets collapsed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod83()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
            Guid _mentalHealthCareContactActivityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Health Care Contacts", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person and Case Appointment
            Guid personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 02", "Note 02", "",
                new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, _activityReasonId, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            #region Person Email

            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 02", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, _mentalHealthCareContactActivityCategoryId, null);

            #endregion

            #region Person Letter
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 02", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Appointment")
                .TapSearchButton()
                .TapChangeViewButton()

                .ValidateExpandCollapseAllButtonVisibility_ListViewMode(true)//List view mode should be active now

                .TapExpandRecordButton_ListViewMode(caseAppointmentId2.ToString()) //expand the 2nd appointment record

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateEditRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateExpandRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 2, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 2, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 3, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 4, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 5, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 6, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 7, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 8, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 9, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 10, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 11, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 12, true) //this record is expanded, the line should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 13, true) //this record is expanded, the line should be visible

                .TapExpandRecordButton_ListViewMode(caseAppointmentId2.ToString()) //collapse the 2nd appointment record

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateEditRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateExpandRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 2, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 2, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, false) //List view mode should be active now but only 2 lines should be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible


                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 2, "Subject: Person Appointment 02")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 2, "Subject: Case 02 Appointment 02")

                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 2, "Subject: Person Appointment 01")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 2, "Subject: Case 02 Appointment 01");
        }


        [TestProperty("JiraIssueID", "CDV6-11893")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Activity Type to Appointment - Tap on the search button - " +
            "Tap on the Show List Button - What for the List View Mode to be displayed - Click on the Expand All button - " +
            "Validate that all records get expanded")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod84()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
            Guid _mentalHealthCareContactActivityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Health Care Contacts", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person and Case Appointment
            Guid personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 02", "Note 02", "",
                new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, _activityReasonId, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            #region Person Email

            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 02", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, _mentalHealthCareContactActivityCategoryId, null);

            #endregion

            #region Person Letter
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 02", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Appointment")
                .TapSearchButton()
                .TapChangeViewButton()

                .ValidateExpandCollapseAllButtonVisibility_ListViewMode(true)//List view mode should be active now

                .TapExpandCollapseAllButton_ListViewMode() //expand all records

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateEditRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateExpandRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 2, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 2, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 3, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 4, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 5, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 6, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 7, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 8, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 9, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 10, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 11, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 12, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 13, true) //all records should be expanded

                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 3, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 4, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 5, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 6, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 7, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 8, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 9, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 10, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 11, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 12, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 13, true) //all records should be expanded

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 3, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 4, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 5, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 6, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 7, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 8, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 9, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 10, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 11, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 12, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 13, true) //all records should be expanded

                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 3, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 4, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 5, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 6, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 7, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 8, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 9, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 10, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 11, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 12, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 13, true) //all records should be expanded


                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 2, "Subject: Person Appointment 02")
                .ValidateRecordLineSubCardText_ListViewMode(personAppointmentId2.ToString(), 1, "Activity: Appointment")
                .ValidateRecordLineSubCardText_ListViewMode(personAppointmentId2.ToString(), 2, "Description: Note 02")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 2, "Subject: Case 02 Appointment 02")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 1, "Activity: Appointment")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId2.ToString(), 2, "Description: Note02")

                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 2, "Subject: Person Appointment 01")
                .ValidateRecordLineSubCardText_ListViewMode(personAppointmentId1.ToString(), 1, "Activity: Appointment")
                .ValidateRecordLineSubCardText_ListViewMode(personAppointmentId1.ToString(), 2, "Description: Note 01")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 2, "Subject: Case 02 Appointment 01")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId1.ToString(), 1, "Activity: Appointment")
                .ValidateRecordLineSubCardText_ListViewMode(caseAppointmentId1.ToString(), 2, "Description: Note01");
        }


        [TestProperty("JiraIssueID", "CDV6-11894")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Activity Type to Appointment - Tap on the search button - " +
            "Tap on the Show List Button - What for the List View Mode to be displayed - Click on the Expand All button - " +
            "Validate that all records get expanded - Click on the Expand All button a second time - Validate that all records get collapsed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod85()
        {

            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
            Guid _mentalHealthCareContactActivityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Health Care Contacts", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person and Case Appointment
            Guid personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 02", "Note 02", "",
                new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, _activityReasonId, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            #region Person Email

            Guid personEmailRecordId1 = dbHelper.email.CreateEmail(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, "Security Test User Admin", "systemuser", personID, "person", _personFullname, "Person Email 02", "Note 01", 2, new DateTime(2020, 9, 1),
                null, null, null, _mentalHealthCareContactActivityCategoryId, null);

            #endregion

            #region Person Letter
            Guid personLetterId1 = dbHelper.letter.CreateLetter("", "", "", "", personID.ToString(), _personFullname, "person", 1, "1", "Person Letter 02", "Notes 01",
                null, _careDirectorQA_TeamId, userid, null, null, null, null, null, personID, new DateTime(2020, 9, 1), personID, _personFullname,
                "person", false, null, null, null, false);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Appointment")
                .TapSearchButton()
                .TapChangeViewButton()

                .ValidateExpandCollapseAllButtonVisibility_ListViewMode(true)//List view mode should be active now

                .TapExpandCollapseAllButton_ListViewMode() //expand all records

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateEditRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateExpandRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 2, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 2, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 3, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 4, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 5, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 6, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 7, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 8, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 9, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 10, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 11, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 12, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 13, true) //all records should be expanded

                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 3, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 4, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 5, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 6, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 7, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 8, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 9, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 10, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 11, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 12, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 13, true) //all records should be expanded

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 3, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 4, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 5, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 6, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 7, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 8, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 9, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 10, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 11, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 12, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 13, true) //all records should be expanded

                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 3, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 4, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 5, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 6, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 7, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 8, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 9, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 10, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 11, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 12, true) //all records should be expanded
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 13, true) //all records should be expanded


                .TapExpandCollapseAllButton_ListViewMode() //Collapse all records

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateEditRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personEmailRecordId1.ToString(), false) //The control records should never be visible
                .ValidateExpandRecordButtonVisibility_ListViewMode(personLetterId1.ToString(), false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible

                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 2, true) //List view mode should be active now
                .ValidateRecordLineMainCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 2, false) //The control records should never be visible
                .ValidateRecordLineMainCardVisibility_ListViewMode(personLetterId1.ToString(), 2, false) //The control records should never be visible

                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId2.ToString(), 1, false) //all records should be collapsed now
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId2.ToString(), 1, false) //all records should be collapsed now
                .ValidateRecordLineSubCardVisibility_ListViewMode(personAppointmentId1.ToString(), 1, false) //all records should be collapsed now
                .ValidateRecordLineSubCardVisibility_ListViewMode(caseAppointmentId1.ToString(), 1, false) //all records should be collapsed now
                .ValidateRecordLineSubCardVisibility_ListViewMode(personEmailRecordId1.ToString(), 1, false) //The control records should never be visible
                .ValidateRecordLineSubCardVisibility_ListViewMode(personLetterId1.ToString(), 1, false) //The control records should never be visible


                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId2.ToString(), 2, "Subject: Person Appointment 02")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId2.ToString(), 2, "Subject: Case 02 Appointment 02")

                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 1, "Regarding: " + _personFullname)
                .ValidateRecordLineMainCardText_ListViewMode(personAppointmentId1.ToString(), 2, "Subject: Person Appointment 01")

                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 1, "Regarding: " + _caseTitle)
                .ValidateRecordLineMainCardText_ListViewMode(caseAppointmentId1.ToString(), 2, "Subject: Case 02 Appointment 01");
        }


        [TestProperty("JiraIssueID", "CDV6-11895")]
        [Description("UI Test for the All Activities Screen" +
            "Open a person record - Navigate to the All Activities screen - Set Activity Type to Appointment - Tap on the search button - " +
            "Tap on the Show List Button - What for the List View Mode to be displayed - Click on a record edit button - " +
            "Validate that the user is redirected to the appointment record page - Click on the Back button - Validate that the user is redirected back to the All Activities page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_AllActivities_UITestMethod86()
        {
            #region Data Form

            Guid _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

            #endregion

            #region Case Status

            Guid _caseStatusId = dbHelper.caseStatus.GetByName("First Appointment Booked").FirstOrDefault();

            #endregion

            #region Contact Reason

            Guid _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _careDirectorQA_TeamId);

            #endregion

            #region Activity Categories                

            Guid _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);
            Guid _mentalHealthCareContactActivityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("fc599e31-5f75-e911-a2c5-005056926fe4"), "Mental Health Care Contacts", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Activity Sub Categories

            Guid _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

            #endregion

            #region Activity Reason

            Guid _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

            #endregion

            #region Person

            var _firstName = "Christine";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullname = _firstName + " " + _lastName;
            var personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _careDirectorQA_TeamId);
            var personNumber = (dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]).ToString();

            #endregion

            #region Case

            Guid _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_careDirectorQA_TeamId, personID, _systemUserId, _systemUserId, _caseStatusId, _contactReasonId, _dataFormId, null, new DateTime(2020, 9, 1, 8, 0, 0), new DateTime(2020, 9, 1, 9, 0, 0), 20);
            string _caseTitle = (string)dbHelper.Case.GetCaseById(_caseId, "title")["title"];

            #endregion

            #region Person and Case Appointment
            Guid personAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 01", "Note 01", "",
                new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid personAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId,
                null, "Person Appointment 02", "Note 02", "",
                new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                personID, "person", _personFullname, 4, 5, false, null, null, null);

            Guid caseAppointmentId1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, _activityCategoryId, _activitySubCategoryId, _activityReasonId, null, null, _systemUserId, null,
                "Case 02 Appointment 01", "Note01", null, new DateTime(2020, 9, 1), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 1), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            Guid caseAppointmentId2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, personID, null, null, null, null, null, _systemUserId, null,
                "Case 02 Appointment 02", "Note02", null, new DateTime(2020, 9, 17), new TimeSpan(11, 30, 0), new DateTime(2020, 9, 17), new TimeSpan(12, 0, 0),
                _caseId, "case", _caseTitle, 4, 5, false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("securitytestuseradmin", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber, personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad(false, false)
                .TapAllActivitiesTab();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()
                .InsertFromDate("")
                .InsertToDate("")
                .SelectActivityType("Appointment")
                .TapSearchButton()
                .TapChangeViewButton()

                .ValidateExpandCollapseAllButtonVisibility_ListViewMode(true)//List view mode should be active now

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now

                .TapEditRecordButton_ListViewMode(personAppointmentId2.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Person Appointment 02")
                .ClickBackButton();

            personAllActivitiesSubPage
                .WaitForPersonAllActivitiesSubPageToLoad()

                .ValidateExpandCollapseAllButtonVisibility_ListViewMode(true)//List view mode should be active now

                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateEditRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now

                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId2.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(personAppointmentId1.ToString(), true) //List view mode should be active now
                .ValidateExpandRecordButtonVisibility_ListViewMode(caseAppointmentId1.ToString(), true) //List view mode should be active now
                ;

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

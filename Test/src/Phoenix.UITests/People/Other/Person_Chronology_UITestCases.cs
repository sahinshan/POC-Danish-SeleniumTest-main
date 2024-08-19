using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    [TestClass]
    public class Person_Chronology_UITestCases : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _systemUserFullName;
        private Guid _personId;
        private int _personNumber;
        private string _person_fullName;
        private Guid _systemSettingId;
        private Guid _personChronologyId;
        private Guid _significantEventCategoryId_ChildInNeed;
        private string _significantEventCategoryName_ChildInNeed;
        private Guid _significantEventSubCategoryId_2_1;
        private string _significantEventSubCategoryName_2_1;
        private Guid _significantEventSubCategoryId_2_2;
        private string _significantEventSubCategoryName_2_2;
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

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

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

                _ethnicityId = commonMethodsDB.CreateEthnicity(_careDirectorQA_TeamId, "English", new DateTime(2020, 1, 1));

                #endregion

                #region Create System User Record

                _systemUserName = "Person_Chronologies_User1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Chronologies", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "Person Chronologies User1";

                #endregion

                #region Person

                var firstName = "First";
                var lastName = "LN_" + _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                _personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = firstName + " " + lastName;

                #endregion

                #region Significant Event Category

                _significantEventCategoryName_ChildInNeed = "Child in Need";
                _significantEventCategoryId_ChildInNeed = commonMethodsDB.CreateSignificantEventCategory(_significantEventCategoryName_ChildInNeed, new DateTime(2020, 1, 1), _careDirectorQA_TeamId, "", null, null, true);

                #endregion

                #region Significant Event Category

                _significantEventSubCategoryName_2_1 = "Sub Cat 2_1";
                _significantEventSubCategoryId_2_1 = commonMethodsDB.CreateSignificantEventSubCategory(_careDirectorQA_TeamId, _significantEventSubCategoryName_2_1, _significantEventCategoryId_ChildInNeed, new DateTime(2022, 1, 1), null, null);

                _significantEventSubCategoryName_2_2 = "Sub Cat 2_2";
                _significantEventSubCategoryId_2_2 = commonMethodsDB.CreateSignificantEventSubCategory(_careDirectorQA_TeamId, _significantEventSubCategoryName_2_2, _significantEventCategoryId_ChildInNeed, new DateTime(2022, 1, 1), null, null);

                #endregion

                #region System Setting

                _systemSettingId = commonMethodsDB.CreateSystemSetting("Chronology.PrintFormat", "PDF", "Describe print format for chronology. Valid values PDF or Word", false, "false");

                #endregion

                #region Letter

                var letterNotes = "Click on the Zephyr Option in the Menu available in the left send of your Jira, Under the Automation Section there is an Option called API ";
                var _sender_Id = commonMethodsDB.CreatePersonRecord("Ralph", "Abbott", _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));

                dbHelper.letter.CreateLetter(_sender_Id.ToString(), "Ralph Abbott", "person", "Address", _personId.ToString(), _person_fullName, "person", 1, "In Progress", "Letter 01", letterNotes,
                    null, _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, _personId, new DateTime(2020, 7, 20), _personId,
                    _person_fullName, "person", true, new DateTime(2020, 7, 4), _significantEventCategoryId_ChildInNeed, _significantEventSubCategoryId_2_1);

                #endregion

                #region Phone Call

                dbHelper.phoneCall.CreatePhoneCallRecordForPerson("Phone Call 01", "Phone Call 01", null, "", "", _systemUserId, "systemuser", _systemUserFullName,
                    "", _personId, _person_fullName, _careDirectorQA_TeamId, new DateTime(2020, 7, 20), _systemUserId, null, false,
                    1, true, new DateTime(2020, 7, 5), _significantEventCategoryId_ChildInNeed, false, _significantEventSubCategoryId_2_2);

                #endregion

                #region Person Case Note

                dbHelper.personCaseNote.CreatePersonCaseNote("Case Note 01", "Case Note 01", _careDirectorQA_TeamId, _systemUserId,
                    null, null, null, null, null, null, _personId, new DateTime(2020, 7, 20), _personId, _person_fullName,
                    "person", true, new DateTime(2020, 7, 1), _significantEventCategoryId_ChildInNeed, _significantEventSubCategoryId_2_1);

                #endregion

                #region Email

                dbHelper.email.CreateEmail(_careDirectorQA_TeamId, _personId, _systemUserId, _systemUserId, _systemUserFullName, "systemuser", _personId,
                    "person", _person_fullName, "Email 01", "Email 01", 2, new DateTime(2020, 7, 20), null, null, null, null, null, true,
                    new DateTime(2020, 7, 3), _significantEventCategoryId_ChildInNeed, _significantEventSubCategoryId_2_1, false, true);


                #endregion

                #region Person Appointment

                dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId, null, null, null, null, null, _systemUserId, null, "Appointment 01", "Appointment 01",
                    "", new DateTime(2020, 7, 20), new TimeSpan(11, 30, 0), new DateTime(2020, 7, 20), new TimeSpan(11, 35, 0), _personId, "person", _person_fullName,
                    4, 5, true, new DateTime(2020, 7, 2), _significantEventCategoryId_ChildInNeed, _significantEventSubCategoryId_2_2, true, false, true, false);

                #endregion

                #region Person Task

                dbHelper.task.CreateTask("Task 01", "Task 01", _careDirectorQA_TeamId, _systemUserId, null, null, null, null, null, null,
                    _personId, new DateTime(2020, 7, 20), _personId, _person_fullName, "person", true, new DateTime(2020, 7, 2),
                    _significantEventCategoryId_ChildInNeed, _significantEventSubCategoryId_2_1, false);

                #endregion

                #region Significant Event Record

                Guid PersonSignificantEventId_Letter = dbHelper.personSignificantEvent.GetByPersonIdAndSourceActivityIdTableName(_personId, "letter").FirstOrDefault();
                Guid PersonSignificantEventId_PhoneCall = dbHelper.personSignificantEvent.GetByPersonIdAndSourceActivityIdTableName(_personId, "phonecall").FirstOrDefault();
                Guid PersonSignificantEventId_PersonCaseNote = dbHelper.personSignificantEvent.GetByPersonIdAndSourceActivityIdTableName(_personId, "personcasenote").FirstOrDefault();
                Guid PersonSignificantEventId_Email = dbHelper.personSignificantEvent.GetByPersonIdAndSourceActivityIdTableName(_personId, "email").FirstOrDefault();
                Guid PersonSignificantEventId_Appointment = dbHelper.personSignificantEvent.GetByPersonIdAndSourceActivityIdTableName(_personId, "appointment").FirstOrDefault();
                Guid PersonSignificantEventId_Task = dbHelper.personSignificantEvent.GetByPersonIdAndSourceActivityIdTableName(_personId, "task").FirstOrDefault();

                #endregion

                #region Person Chronology Record

                _personChronologyId = dbHelper.personChronology.CreatePersonChronology("Chronology Child in Need", new DateTime(2020, 7, 1), new DateTime(2020, 7, 31), _careDirectorQA_TeamId, _personId);

                #endregion

                #region Person Significant Event Chronology

                dbHelper.personSignificantEventChronology.CreatePersonSignificantEventChronology(_personChronologyId, PersonSignificantEventId_Letter);
                dbHelper.personSignificantEventChronology.CreatePersonSignificantEventChronology(_personChronologyId, PersonSignificantEventId_PhoneCall);

                dbHelper.personSignificantEventChronology.CreatePersonSignificantEventChronology(_personChronologyId, PersonSignificantEventId_PersonCaseNote);
                dbHelper.personSignificantEventChronology.CreatePersonSignificantEventChronology(_personChronologyId, PersonSignificantEventId_Email);
                dbHelper.personSignificantEventChronology.CreatePersonSignificantEventChronology(_personChronologyId, PersonSignificantEventId_Appointment);
                dbHelper.personSignificantEventChronology.CreatePersonSignificantEventChronology(_personChronologyId, PersonSignificantEventId_Task);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region Print to PDF for Chronology https://advancedcsg.atlassian.net/browse/CDV6-3734

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3734 - " +
            "Chronology.PrintFormat = Word" +
            "Open Person Record -> Navigate to the Chronologies sub-section - Open a Chronology record - Tap on the Print button - " +
            "Validate that a docx file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-20372")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronology_UITestMethod01()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(_systemSettingId, "Word");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .OpenPersonChronologyRecord(_personChronologyId.ToString());

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .TapPrintButton();

            personChronologyRecordPrintPopup
                .WaitForPersonChronologyRecordPrintPopupToLoad()
                .TapPrintButton();

            System.Threading.Thread.Sleep(3000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.docx");
            Assert.IsTrue(fileExists);

        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3734 - " +
            "Chronology.PrintFormat = PDF" +
            "Open Person Record -> Navigate to the Chronologies sub-section - Open a Chronology record - Tap on the Print button - " +
            "Validate that no docx or pdf file is downloaded to the browser Downloads folder")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-20373")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronology_UITestMethod02()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(_systemSettingId, "PDF");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .OpenPersonChronologyRecord(_personChronologyId.ToString());

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .TapPrintButton();

            personChronologyRecordPrintPopup
                .WaitForPersonChronologyRecordPrintPopupToLoad()
                .TapPrintButton();

            bool fileExists = fileIOHelper.ValidateIfFileExists(this.DownloadsDirectory, "*.pdf");
            Assert.IsFalse(fileExists);

        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3734 - " +
            "Chronology.PrintFormat = Word" +
            "Open Person Record -> Navigate to the Chronologies sub-section - Open a Chronology record - Tap on the Print button - Select 'No Grouping' for the print type - " +
            "Validate that the downloaded docx file contains info regarding all available significant events with (records should not be ordered)")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-20374")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronology_UITestMethod03()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(_systemSettingId, "Word");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .OpenPersonChronologyRecord(_personChronologyId.ToString());

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .TapPrintButton();

            personChronologyRecordPrintPopup
                .WaitForPersonChronologyRecordPrintPopupToLoad()
                .TapPrintButton();

            System.Threading.Thread.Sleep(3000);
            List<string> filesPath = fileIOHelper.GetFilesPath(this.DownloadsDirectory, "*.docx");
            Assert.AreEqual(1, filesPath.Count); //only 1 file should exist

            string filePath = filesPath.FirstOrDefault();
            msWordHelper.ValidateWordsPresent(filePath, _person_fullName, "Chronology Child in Need", "Categories:", "Child in Need", "Date From:", "01/07/2020", "Date To:", "31/07/2020", "", "");
            msWordHelper.ValidateWordsPresent(filePath, "04/07/2020", "03/07/2020", "02/07/2020", "Click on the Zephyr Option in the Menu available", "Phone Call 01", "Case Note 01", "Email 01", "Appointment 01", "Task 01");

        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3734 - " +
            "Chronology.PrintFormat = Word" +
            "Open Person Record -> Navigate to the Chronologies sub-section - Open a Chronology record - Tap on the Print button - Select 'Event date' for the print type - " +
            "Validate that the downloaded docx file contains info regarding all available significant events with (records should not be ordered)")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-20375")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronology_UITestMethod04()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(_systemSettingId, "Word");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .OpenPersonChronologyRecord(_personChronologyId.ToString());

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .TapPrintButton();

            personChronologyRecordPrintPopup
                .WaitForPersonChronologyRecordPrintPopupToLoad()
                .SelectGroupByByText("Event date")
                .TapPrintButton();

            System.Threading.Thread.Sleep(3000);
            List<string> filesPath = fileIOHelper.GetFilesPath(this.DownloadsDirectory, "*.docx");
            Assert.AreEqual(1, filesPath.Count); //only 1 file should exist

            string filePath = filesPath.FirstOrDefault();
            msWordHelper.ValidateWordsPresent(filePath, _person_fullName, "Chronology Child in Need", "Categories:", "Child in Need", "Date From:", "01/07/2020", "Date To:", "31/07/2020", "", "");
            msWordHelper.ValidateWordsPresent(filePath, "Event Date: 04/07/2020", "Event Date: 03/07/2020", "Event Date: 02/07/2020", "Click on the Zephyr Option in the Menu available", "Phone Call 01", "Case Note 01", "Email 01", "Appointment 01", "Task 01");

        }

        [Description("Bug Fix https://advancedcsg.atlassian.net/browse/CDV6-3734 - " +
            "Chronology.PrintFormat = Word" +
            "Open Person Record -> Navigate to the Chronologies sub-section - Open a Chronology record - Tap on the Print button - Select 'Category' for the print type - " +
            "Validate that the downloaded docx file contains info regarding all available significant events with (records should not be ordered)")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-20376")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Chronology_UITestMethod05()
        {
            dbHelper.systemSetting.UpdateSystemSettingValue(_systemSettingId, "Word");

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToChronologiesPage();

            personChronologiesPage
                .WaitForPersonChronologiesPageToLoad()
                .OpenPersonChronologyRecord(_personChronologyId.ToString());

            personChronologyRecordPage
                .WaitForPersonChronologyRecordPageToLoad()
                .TapPrintButton();

            personChronologyRecordPrintPopup
                .WaitForPersonChronologyRecordPrintPopupToLoad()
                .SelectGroupByByText("Category")
                .TapPrintButton();

            System.Threading.Thread.Sleep(3000);

            List<string> filesPath = fileIOHelper.GetFilesPath(this.DownloadsDirectory, "*.docx");
            Assert.AreEqual(1, filesPath.Count); //only 1 file should exist

            string filePath = filesPath.FirstOrDefault();
            msWordHelper.ValidateWordsPresent(filePath, _person_fullName, "Chronology Child in Need", "Categories:", "Child in Need", "Date From:", "01/07/2020", "Date To:", "31/07/2020", "", "");
            msWordHelper.ValidateWordsPresent(filePath, "Category: Child in Need", "04/07/2020", "03/07/2020", "02/07/2020", "Click on the Zephyr Option in the Menu available", "Phone Call 01", "Case Note 01", "Email 01", "Appointment 01", "Task 01");

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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Phoenix.UITests.People
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    [DeploymentItem("chromedriver.exe")]
    [DeploymentItem("Files\\Automation - Person Form 1.Zip")]

    public class Person_PersonFormAppointments_UITestCases : FunctionalTest
    {

        private string _environmentName;
        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string _firstName;
        private string _lastName;
        private string _personFullname;
        private Guid _personId;
        private string _personNumber;
        private Guid _documentId1;
        private Guid _activityPriorityId_Normal;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityOutcomeId;
        private Guid _appointmentTypeId;

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

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

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Security BU2");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Security T2", null, _businessUnitId, "907678", "SecurityT2@careworkstempmail.com", "Security T2", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User PFAppointmentUser1

                _systemUsername = "PFAppointmentUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "PFAppointment", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Activity Categories                

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _teamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Activity Priority

                _activityPriorityId_Normal = commonMethodsDB.CreateActivityPriority(new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"), "Normal", new DateTime(2019, 1, 1), _teamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _teamId);

                #endregion

                #region Appointment Type

                _appointmentTypeId = commonMethodsDB.CreateAppointmentTypeIfNeeded("Conference", _teamId);

                #endregion

                #region Person

                _firstName = "John";
                _lastName = "LN_" + _currentDateString;
                _personFullname = _firstName + " " + _lastName;
                _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId, new DateTime(2003, 1, 2));
                _personNumber = dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"].ToString();

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-8614


        [TestProperty("JiraIssueID", "CDV6-24886")]
        [Description(
           "Login in the web app - Open a Person Form record - Navigate to the Person Form Appointments area - " +
            "Validate that the Person Form Appointments page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormAppointments_UITestMethod01()
        {

            #region Document

            string document1Name = "Automation - Person Form 1";
            _documentId1 = commonMethodsDB.CreateDocumentIfNeeded(document1Name, "Automation - Person Form 1.Zip");

            #endregion

            #region Person Form
            Guid personFormID = dbHelper.personForm.CreatePersonForm(_teamId, _personId, _documentId1, new DateTime(2021, 2, 19), true);
            var personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            #endregion

            #region Person Form Appointment
            Guid personFormAppointment1ID = dbHelper.appointment.CreateAppointment(_teamId, _personId, null, null, null, null, _activityPriorityId_Normal, _systemUserId,
                null, "Person Form Appointment 01", "Note 01", "location ....",
                new DateTime(2021, 02, 22), new TimeSpan(11, 0, 0), new DateTime(2021, 02, 22), new TimeSpan(11, 30, 0),
                personFormID, "personform", personFormTitle, 4, 5, false, null, null, null);

            Guid personFormAppointment2ID = dbHelper.appointment.CreateAppointment(_teamId, _personId, null, null, null, null, null, _systemUserId,
                null, "Person Form Appointment 02", "Note 02", "",
                new DateTime(2021, 02, 23), new TimeSpan(11, 0, 0), new DateTime(2021, 02, 23), new TimeSpan(11, 30, 0),
                personFormID, "personform", personFormTitle, 4, 5, false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormAppointmentsArea();

            personFormAppointmentsPage
                .WaitForPersonFormAppointmentsPageToLoad()

                .ValidateSubjectCellText(personFormAppointment1ID.ToString(), "Person Form Appointment 01")
                .ValidateStartDateCellText(personFormAppointment1ID.ToString(), "22/02/2021")
                .ValidateStartTimeCellText(personFormAppointment1ID.ToString(), "11:00")
                .ValidateEndDateCellText(personFormAppointment1ID.ToString(), "22/02/2021")
                .ValidateEndTimeCellText(personFormAppointment1ID.ToString(), "11:30")
                .ValidateLocationCellText(personFormAppointment1ID.ToString(), "location ....")
                .ValidatePriorityCellText(personFormAppointment1ID.ToString(), "Normal")

                .ValidateSubjectCellText(personFormAppointment2ID.ToString(), "Person Form Appointment 02")
                .ValidateStartDateCellText(personFormAppointment2ID.ToString(), "23/02/2021")
                .ValidateStartTimeCellText(personFormAppointment2ID.ToString(), "11:00")
                .ValidateEndDateCellText(personFormAppointment2ID.ToString(), "23/02/2021")
                .ValidateEndTimeCellText(personFormAppointment2ID.ToString(), "11:30")
                .ValidateLocationCellText(personFormAppointment2ID.ToString(), "")
                .ValidatePriorityCellText(personFormAppointment2ID.ToString(), "");
        }

        [TestProperty("JiraIssueID", "CDV6-24887")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Appointments area - " +
            "Search for Person Form Appointment record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormAppointments_UITestMethod02()
        {
            #region Document

            string document1Name = "Automation - Person Form 1";
            _documentId1 = commonMethodsDB.CreateDocumentIfNeeded(document1Name, "Automation - Person Form 1.Zip");

            #endregion

            #region Person Form
            Guid personFormID = dbHelper.personForm.CreatePersonForm(_teamId, _personId, _documentId1, new DateTime(2021, 2, 19), true);
            var personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            #endregion

            #region Person Form Appointment
            Guid personFormAppointment1ID = dbHelper.appointment.CreateAppointment(_teamId, _personId, null, null, null, null, _activityPriorityId_Normal, _systemUserId,
                null, "Person Form Appointment 01", "Note 01", "location ....",
                new DateTime(2021, 02, 22), new TimeSpan(11, 0, 0), new DateTime(2021, 02, 22), new TimeSpan(11, 30, 0),
                personFormID, "personform", personFormTitle, 4, 5, false, null, null, null);

            Guid personFormAppointment2ID = dbHelper.appointment.CreateAppointment(_teamId, _personId, null, null, null, null, null, _systemUserId,
                null, "Person Form Appointment 02", "Note 02", "",
                new DateTime(2021, 02, 23), new TimeSpan(11, 0, 0), new DateTime(2021, 02, 23), new TimeSpan(11, 30, 0),
                personFormID, "personform", personFormTitle, 4, 5, false, null, null, null);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormAppointmentsArea();

            personFormAppointmentsPage
                .WaitForPersonFormAppointmentsPageToLoad()
                .SearchPersonFormAppointmentRecord("Person Form Appointment 01")
                .ValidateRecordPresent(personFormAppointment1ID.ToString())
                .ValidateRecordNotPresent(personFormAppointment2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24888")]
        [Description(
            "Login in the web app - Open a Person Form record - Navigate to the Person Form Appointments area - Open a Person Form Appointment record (all fields must have values) - " +
            "Validate that the user is redirected to the Person Form Appointments record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonFormAppointments_UITestMethod03()
        {
            List<Guid> userSecProfiles = new List<Guid>
            {
                dbHelper.securityProfile.GetSecurityProfileByName("System Administrator")[0],
                dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)")[0],

            };
            var optionalSystemUserId = commonMethodsDB.CreateSystemUserRecord("ALBERTEinstein", "ALBERT", "Einstein", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);
            var requiredSystemUserId = commonMethodsDB.CreateSystemUserRecord("JohnStones", "John", "Stones", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid, userSecProfiles);

            #region Document

            string document1Name = "Automation - Person Form 1";
            _documentId1 = commonMethodsDB.CreateDocumentIfNeeded(document1Name, "Automation - Person Form 1.Zip");

            #endregion

            #region Person Form
            Guid personFormID = dbHelper.personForm.CreatePersonForm(_teamId, _personId, _documentId1, new DateTime(2021, 2, 19), true);
            var personFormTitle = (string)dbHelper.personForm.GetPersonFormByID(personFormID, "title")["title"];

            #endregion

            #region Person Form Appointment
            Guid personFormAppointment1ID = dbHelper.appointment.CreateAppointment(_teamId, _personId, _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId_Normal, _systemUserId,
                _appointmentTypeId, "Person Form Appointment 01", "Person Form Appointment 01 meeting notes", "location ....",
                new DateTime(2021, 02, 22), new TimeSpan(11, 0, 0), new DateTime(2021, 02, 22), new TimeSpan(11, 30, 0),
                personFormID, "personform", personFormTitle, 4, 5, false, null, null, null, false, false, false, true);

            #endregion

            #region

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personFormAppointment1ID, requiredSystemUserId, "systemuser", "John Stones");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personFormAppointment1ID, optionalSystemUserId, "systemuser", "ALBERT Einstein");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber, _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToPersonFormsPage();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .OpenRecord(personFormID.ToString());

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad(false)
                .NavigateToPersonFormAppointmentsArea();

            personFormAppointmentsPage
                .WaitForPersonFormAppointmentsPageToLoad()
                .OpenPersonFormAppointmentRecord(personFormAppointment1ID.ToString());

            personFormAppointmentRecordPage
                .WaitForPersonFormAppointmentRecordPageToLoad("Person Form Appointment 01")

                .ValidateSubject("Person Form Appointment 01")
                .ValidateRequired(requiredSystemUserId.ToString(), "John Stones\r\nRemove")
                .ValidateOptional(optionalSystemUserId.ToString(), "ALBERT Einstein\r\nRemove")
                .ValidateMeetingNotes("Person Form Appointment 01 meeting notes")

                .ValidateStartDate("22/02/2021")
                .ValidateEndDate("22/02/2021")
                .ValidateShowTimeAs("Busy")
                .ValidateStartTime("11:00")
                .ValidateEndTime("11:30")
                .ValidateAllowConcurrentAppointmentCheckedOption(true)

                .ValidateRegardingFieldLinkText(personFormTitle)
                .ValidateAppointmentTypeFieldLinkText("Conference")
                .ValidateLocation("location ....")
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateResponsibleTeamFieldLinkText("Security T2")
                .ValidateResponsibleUserFieldLinkText("PFAppointment User1")
                .ValidateIsCaseNoteCheckedOption(false)
                .ValidateCategoryFieldLinkText("Advice")
                .ValidateSubCategoryFieldLinkText("Home Support")
                .ValidateReasonFieldLinkText("Assessment")
                .ValidateOutcomeFieldLinkText("More information needed")
                .ValidateStatus("Scheduled")
                .ValidateContainsInformationProvidedByAThirdPartyCheckedOption(false)
                .ValidateSignificantEventCheckedOption(false);
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

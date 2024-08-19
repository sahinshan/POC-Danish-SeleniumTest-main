using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{

    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_Appointments_UITestCases : FunctionalTest
    {

        #region Properties

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _ethnicityId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _defaultLoginUserID;
        private string _loginUsername;
        private string _loginUserFullName;
        private Guid _personId;
        private string _currentDateSuffix = DateTime.Now.ToString("yyyyMMddHHmmss");
        private string firstName;
        private string lastName;
        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityReasonId;
        private Guid _activityPriorityId;
        private Guid _activityOutcomeId;
        private Guid _significantEventCategoryId;
        private Guid _significantEventSubCategoryId;
        private Guid _appointmentTypeId;
        private Guid _professionTypeId;
        private int personNumber;
        private string _person_fullName;
        private Guid requiredSystemUserId1;
        private string _requiredSystemUserFullname;

        #endregion

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

                #region System User

                _loginUsername = "PersonAppointmentsUser1";
                _defaultLoginUserID = commonMethodsDB.CreateSystemUserRecord(_loginUsername, "PersonAppointments", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _loginUserFullName = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_defaultLoginUserID, "fullname")["fullname"];

                #endregion

                #region Person

                firstName = "Automation";
                lastName = _currentDateSuffix;
                _personId = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _careDirectorQA_TeamId, new DateTime(2000, 1, 2));
                personNumber = (int)dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"];
                _person_fullName = (string)dbHelper.person.GetPersonById(_personId, "fullname")["fullname"];

                #endregion

                #region Activity Categories                

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Sub Categories

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2020, 1, 1), _activityCategoryId, _careDirectorQA_TeamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"), "Normal", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2020, 1, 1), _careDirectorQA_TeamId);

                #endregion

                #region Significant Event Category

                _significantEventCategoryId = commonMethodsDB.CreateSignificantEventCategory("Category 1", DateTime.Now.Date, _careDirectorQA_TeamId, null, null, null);

                #endregion

                #region Significant Event Sub Category

                _significantEventSubCategoryId = commonMethodsDB.CreateSignificantEventSubCategory(_careDirectorQA_TeamId, "Sub Cat 1_1", _significantEventCategoryId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), null, null);

                #endregion

                #region Appointment Type

                _appointmentTypeId = commonMethodsDB.CreateAppointmentTypeIfNeeded("Meeting", _careDirectorQA_TeamId);

                #endregion

                #region Profession Type

                _professionTypeId = dbHelper.professionType.GetByName("General Practitioner").FirstOrDefault();

                #endregion

                #region Required System User

                var _requiredUsername = "RequiredSystemUser1";
                requiredSystemUserId1 = commonMethodsDB.CreateSystemUserRecord(_requiredUsername, "RequiredSystem", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
                _requiredSystemUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(requiredSystemUserId1, "fullname")["fullname"];

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-11570

        [TestProperty("JiraIssueID", "CDV6-24644")]
        [Description("Open a person record (person has no appointments linked to it) - Navigate to the Person Appointments screen - Validate that the No Records message is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoadEmpty()
                .ValidateNoRecordsMessageVisibility(true);

        }

        [TestProperty("JiraIssueID", "CDV6-24645")]
        [Description("Open a person record (person has one appointments linked to it) - Navigate to the Person Appointments screen - Validate that the No Records message is NOT displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod02()
        {
            var StartDate = new DateTime(2020, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2020, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = DateTime.Now.Date;

            #region Person Appointment

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
              _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
              "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
              statusid, showTimeAsId,
              IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            #endregion

            #region Appointment Required Attendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false);

        }

        [TestProperty("JiraIssueID", "CDV6-24646")]
        [Description("Open a person record (person has 1 appointments linked to it) - Navigate to the Person Appointments screen - Validate that the record is correctly displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod03()
        {

            var StartDate = new DateTime(2020, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2020, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = DateTime.Now.Date;

            #region Person Appointment

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
              _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId
              , "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
              statusid, showTimeAsId,
              IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            #endregion

            #region Appointment Required Attendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateRecordVisible(personAppointment1.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24647")]
        [Description("Open a person record (person has 1 appointments linked to it with all fields set) - Navigate to the Person Appointments screen - Validate the content of each cell for the record row")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod04()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = DateTime.Now.Date;

            #region Person Appointment

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
              _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId
              , "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
              statusid, showTimeAsId,
              IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            #endregion

            #region Appointment Required Attendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateRecordCellText(personAppointment1.ToString(), 2, "Appointment 001")
                .ValidateRecordCellText(personAppointment1.ToString(), 3, "20/07/2021")
                .ValidateRecordCellText(personAppointment1.ToString(), 4, "09:00")
                .ValidateRecordCellText(personAppointment1.ToString(), 5, "20/07/2021")
                .ValidateRecordCellText(personAppointment1.ToString(), 6, "09:15")
                .ValidateRecordCellText(personAppointment1.ToString(), 7, "Location info ...")
                .ValidateRecordCellText(personAppointment1.ToString(), 8, "Normal")
                .ValidateRecordCellText(personAppointment1.ToString(), 9, "Scheduled")
                .ValidateRecordCellText(personAppointment1.ToString(), 10, "CareDirector QA");

        }

        [TestProperty("JiraIssueID", "CDV6-24648")]
        [Description("Open a person record (person has 1 appointments linked to it with only the mandatory fields set) - Navigate to the Person Appointments screen - Validate the content of each cell for the record row")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod05()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = false;

            #region Person Appointment

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                null, null, null, null, null, null, null
                , "Appointment 001", null, null, StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, null, null, null, false, false, false, false);

            #endregion

            #region Appointment Required Attendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateRecordCellText(personAppointment1.ToString(), 2, "Appointment 001")
                .ValidateRecordCellText(personAppointment1.ToString(), 3, "20/07/2021")
                .ValidateRecordCellText(personAppointment1.ToString(), 4, "09:00")
                .ValidateRecordCellText(personAppointment1.ToString(), 5, "20/07/2021")
                .ValidateRecordCellText(personAppointment1.ToString(), 6, "09:15")
                .ValidateRecordCellText(personAppointment1.ToString(), 7, "")
                .ValidateRecordCellText(personAppointment1.ToString(), 8, "")
                .ValidateRecordCellText(personAppointment1.ToString(), 9, "Scheduled")
                .ValidateRecordCellText(personAppointment1.ToString(), 10, "CareDirector QA");

        }

        [TestProperty("JiraIssueID", "CDV6-24649")]
        [Description("Open a person record (person has 3 appointments linked to it) - Navigate to the Person Appointments screen - Select 2 person appointments records - click on the delete button - " +
            "Confirm the delete operation - Validate that only the selected records are deleted")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod06()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = DateTime.Now.Date;

            #region Person Appointment / Required Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            StartTime = new TimeSpan(9, 15, 0);
            EndTime = new TimeSpan(9, 30, 0);

            var personAppointment2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 002", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment2, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            StartTime = new TimeSpan(9, 30, 0);
            EndTime = new TimeSpan(9, 45, 0);

            var personAppointment3 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 003", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment3, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .SelectPersonAppointmentRecord(personAppointment1.ToString())
                .SelectPersonAppointmentRecord(personAppointment2.ToString())
                .ClickDeletedButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("2 item(s) deleted.").TapOKButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateRecordNotVisible(personAppointment1.ToString())
                .ValidateRecordNotVisible(personAppointment2.ToString())
                .ValidateRecordVisible(personAppointment3.ToString());

            var appointments = dbHelper.appointment.GetAppointmentByPersonID(_personId);
            Assert.AreEqual(1, appointments.Count);
            Assert.IsTrue(appointments.Contains(personAppointment3));

        }

        [TestProperty("JiraIssueID", "CDV6-24650")]
        [Description("Open a person record (person has 3 appointments linked to it) - Navigate to the Person Appointments screen - Search for an appointment record using the appointments subject text - " +
            "Validate that only the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod07()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = DateTime.Now.Date;

            #region Person Appointment & Requiered Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            StartTime = new TimeSpan(9, 15, 0);
            EndTime = new TimeSpan(9, 30, 0);

            var personAppointment2 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 002", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment2, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            StartTime = new TimeSpan(9, 30, 0);
            EndTime = new TimeSpan(9, 45, 0);

            var personAppointment3 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 003", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment3, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .SearchPersonAppointmentRecord("Appointment 002")

                .ValidateRecordVisible(personAppointment2.ToString())
                .ValidateRecordNotVisible(personAppointment1.ToString())
                .ValidateRecordNotVisible(personAppointment3.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-24651")]
        [Description("Open a person record (person has 1 appointments linked to it) - Navigate to the Person Appointments screen - Click on the person appointments record - " +
            "Validate that the user is redirected to the appointments record page")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod08()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = DateTime.Now.Date;

            #region Person Appointment

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            #endregion

            #region Appointment Required Attendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001");

        }

        [TestProperty("JiraIssueID", "CDV6-24652")]
        [Description("Open a person record (person has 1 appointments linked to it with data in all fields) - Navigate to the Person Appointments screen - Click on the person appointments record - " +
            "wait for the record page to load - Validate that all fields are correctly displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod09()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Person Appointment & Required Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", _person_fullName);

            #endregion

            #region Create Professional & System User / Appointment Optional Attendee 

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSubjectFieldText("Appointment 001")
                .ValidateRequiredRecordText(requiredSystemUserId1.ToString(), _requiredSystemUserFullname)
                .ValidateRequiredRecordText(_personId.ToString(), _person_fullName)
                .ValidateOptionalRecordText(optionalProfessionalId1.ToString(), "Mr Professional User1")
                .ValidateOptionalRecordText(optionalSystemUserId1.ToString(), "Optional User1")

                .LoadMeetingNotesRichTextBox()
                .ValidateMeetingNotesFieldText("1", "line 1")
                .ValidateMeetingNotesFieldText("2", "line 2")

                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")

                .ValidateStartDateFieldText("20/07/2021")
                .ValidateStartTimeFieldText("09:00")
                .ValidateShowTimeAsFieldText("Busy")
                .ValidateEndDateFieldText("20/07/2021")
                .ValidateEndTimeFieldText("09:15")
                .ValidateAllowConcurrentAppointmentYesOptionChecked(false)
                .ValidateAllowConcurrentAppointmentNoOptionChecked(true)

                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateAppointmentTypeLinkFieldText("Meeting")
                .ValidateLocationFieldText("Location info ...")
                .ValidatePriorityLinkFieldText("Normal")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_loginUserFullName)
                .ValidateIsCaseNoteYesOptionChecked(true)
                .ValidateIsCaseNoteNoOptionChecked(false)
                .ValidateCategoryLinkFieldText("Advice")
                .ValidateSubCategoryLinkFieldText("Home Support")
                .ValidateReasonLinkFieldText("Assessment")
                .ValidateOutcomeLinkFieldText("More information needed")
                .ValidateSelectedStatus("Scheduled")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(false)

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateText("21/07/2021")
                .ValidateEventCategoryLinkFieldText("Category 1")
                .ValidateEventSubCategoryLinkFieldText("Sub Cat 1_1");

        }

        [TestProperty("JiraIssueID", "CDV6-24653")]
        [Description("Open a person record (person has 1 appointments linked to it with data only in the mandatory fields) - Navigate to the Person Appointments screen - " +
            "Click on the person appointments record - Wait for the record page to load - Validate that all fields are correctly displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod10()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = false;

            #region Person Appointment

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                null, null, null, null, null, null, null
                , "Appointment 001", null, null, StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, null, null, null, false, false, false, false);

            #endregion

            #region Appointment Required Attendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSubjectFieldText("Appointment 001")
                .ValidateRequiredRecordText(requiredSystemUserId1.ToString(), _requiredSystemUserFullname)

                .LoadMeetingNotesRichTextBox()
                .ValidateMeetingNotesFieldText("1", "")

                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")

                .ValidateStartDateFieldText("20/07/2021")
                .ValidateStartTimeFieldText("09:00")
                .ValidateShowTimeAsFieldText("Busy")
                .ValidateEndDateFieldText("20/07/2021")
                .ValidateEndTimeFieldText("09:15")
                .ValidateAllowConcurrentAppointmentYesOptionChecked(false)
                .ValidateAllowConcurrentAppointmentNoOptionChecked(true)

                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateAppointmentTypeLinkFieldText("")
                .ValidateLocationFieldText("")
                .ValidatePriorityLinkFieldText("")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText("")
                .ValidateIsCaseNoteYesOptionChecked(false)
                .ValidateIsCaseNoteNoOptionChecked(true)
                .ValidateCategoryLinkFieldText("")
                .ValidateSubCategoryLinkFieldText("")
                .ValidateReasonLinkFieldText("")
                .ValidateOutcomeLinkFieldText("")
                .ValidateSelectedStatus("Scheduled")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(true)

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateEventDateFieldVisibility(false)
                .ValidateEventCategoryFieldVisibility(false)
                .ValidateEventSubCategoryFieldVisibility(false)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24654")]
        [Description("Open a person record (person has 1 appointments linked to it with data in all fields) - Navigate to the Person Appointments screen - Click on the person appointments record - " +
            "Wait for the record page to load - Remove the data from all mandatory fields - Click on the save and close button - validate that the record is correctly saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod11()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Appointment 

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            #endregion

            #region Create System User and Work Schedule

            var recurrencePatternId = dbHelper.recurrencePattern.GetRecurrencePatternIdByName("Occurs every 1 days")[0];
            var _requiredUsername = "RequiredSystemUser2" + _currentDateSuffix;
            var requiredSystemUserId1 = commonMethodsDB.CreateSystemUserRecord(_requiredUsername, "RequiredSystemUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            _requiredSystemUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(requiredSystemUserId1, "fullname")["fullname"];
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default", requiredSystemUserId1, _careDirectorQA_TeamId, recurrencePatternId, new DateTime(2021, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            #endregion

            #region Appointment Attendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, _person_fullName, "person");

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickRequiredRecordRemoveButton(_personId.ToString())
                .ClickOptionalRecordRemoveButton(optionalProfessionalId1.ToString())
                .ClickOptionalRecordRemoveButton(optionalSystemUserId1.ToString())
                .InsertMeetingNotes("")
                .ClickAppointmentTypeRemoveButton()
                .InsertLocation("")
                .ClickPriorityRemoveButton()
                .ClickResponsibleUserRemoveButton()
                .ClickCategoryRemoveButton()
                .ClickReasonRemoveButton()
                .ClickOutcomeRemoveButton()
                .ClickSaveAndCloseButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad();

            var appointmentRequiredAttendes = dbHelper.appointmentRequiredAttendee.GetByAppointmentID(personAppointment1);
            Assert.AreEqual(1, appointmentRequiredAttendes.Count());

            var appointmentOptionalAttendes = dbHelper.appointmentOptionalAttendee.GetByAppointmentID(personAppointment1);
            Assert.AreEqual(0, appointmentOptionalAttendes.Count());

            personAppointmentsPage
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSubjectFieldText("Appointment 001")
                .ValidateRequiredRecordText(requiredSystemUserId1.ToString(), _requiredSystemUserFullname)

                .LoadMeetingNotesRichTextBox()
                .ValidateMeetingNotesFieldText("1", "")

                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")

                .ValidateStartDateFieldText("20/07/2021")
                .ValidateStartTimeFieldText("09:00")
                .ValidateShowTimeAsFieldText("Busy")
                .ValidateEndDateFieldText("20/07/2021")
                .ValidateEndTimeFieldText("09:15")
                .ValidateAllowConcurrentAppointmentYesOptionChecked(false)
                .ValidateAllowConcurrentAppointmentNoOptionChecked(true)

                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateAppointmentTypeLinkFieldText("")
                .ValidateLocationFieldText("")
                .ValidatePriorityLinkFieldText("")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText("")
                .ValidateIsCaseNoteYesOptionChecked(true)
                .ValidateIsCaseNoteNoOptionChecked(false)
                .ValidateCategoryLinkFieldText("")
                .ValidateSubCategoryLinkFieldText("")
                .ValidateReasonLinkFieldText("")
                .ValidateOutcomeLinkFieldText("")
                .ValidateSelectedStatus("Scheduled")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(false)

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateFieldVisibility(true)
                .ValidateEventCategoryFieldVisibility(true)
                .ValidateEventSubCategoryFieldVisibility(true);

        }

        [TestProperty("JiraIssueID", "CDV6-24655")]
        [Description("Open a person record (person has 1 appointments linked to it with data only in the mandatory fields) - Navigate to the Person Appointments screen - Click on the person appointments record - " +
            "Wait for the record page to load - Update all editable fields - Click on the save button - Click on the Back button - Reopen the record - " +
            "Validate that the record is correctly Updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod12()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = false;

            #region Appointment

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                null, null, null, null, null, null, null
                , "Appointment 001", null, null, StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, null, null, null, false, false, false, false);

            #endregion

            #region Create System User and Work Schedule

            var recurrencePatternId = dbHelper.recurrencePattern.GetRecurrencePatternIdByName("Occurs every 1 days")[0];
            var _requiredUsername = "RequiredSystemUser2" + _currentDateSuffix;
            var requiredSystemUserId1 = commonMethodsDB.CreateSystemUserRecord(_requiredUsername, "RequiredSystemUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            _requiredSystemUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(requiredSystemUserId1, "fullname")["fullname"];
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default", requiredSystemUserId1, _careDirectorQA_TeamId, recurrencePatternId, new DateTime(2021, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            #endregion

            #region Appointment Attendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);

            var optionalProfessionalFirstName = "Appointment_Professional";
            var optionalProfessionalFullName = "Mr Appointment_Professional User1";
            var optionalProfessionalId1 = commonMethodsDB.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", optionalProfessionalFirstName, "User1", "professionaluser1@testmail.com");

            var optionalSystemUserName = "OptionalSystemUser1" + _currentDateSuffix;
            var optionalSystemUserFullName = "OptionalSystemUser1 " + _currentDateSuffix;
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord(optionalSystemUserName, "OptionalSystemUser1", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            commonMethodsDB.CreateUserWorkSchedule("Default", optionalSystemUserId1, _careDirectorQA_TeamId, recurrencePatternId, new DateTime(2021, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", optionalProfessionalFullName);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", optionalSystemUserFullName);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .InsertSubject("Appointment 001 Updated")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(personNumber.ToString()).TapSearchButton().AddElementToList(_personId.ToString()).TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickOptionalLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .SelectBusinessObjectByText("System Users").TypeSearchQuery(optionalSystemUserFullName.ToString()).TapSearchButton().AddElementToList(optionalSystemUserId1.ToString())
                .SelectBusinessObjectByText("Professionals").TypeSearchQuery(optionalProfessionalFirstName.ToString()).TapSearchButton().AddElementToList(optionalProfessionalId1.ToString())
                .TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .InsertMeetingNotes("<p>line 1</p>  <p>line 2</p>")
                .InsertEndTime("")
                .InsertEndDate("")
                .InsertStartTime("")
                .InsertStartDate("")
                .InsertStartTime("10:00")
                .InsertEndTime("10:20")
                .InsertStartDate("23/07/2021")
                .InsertEndDate("23/07/2021")
                .ClickAppointmentTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Meeting").TapSearchButton().SelectResultElement(_appointmentTypeId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .InsertLocation("location info ...");

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_loginUsername).TapSearchButton().SelectResultElement(_defaultLoginUserID.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickIsCaseNoteYesRadioButton()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickContainsInformationProvidedByThirdPartyYesRadioButton()
                .ClickSignificantEventYesRadioButton()
                .InsertEventDate("21/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_significantEventCategoryId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_1").TapSearchButton().SelectResultElement(_significantEventSubCategoryId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickSaveButton()
                .ClickBackButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad();

            var requiredAtendees = dbHelper.appointmentRequiredAttendee.GetByAppointmentID(personAppointment1);
            Assert.AreEqual(2, requiredAtendees.Count());
            var optionalAtendees = dbHelper.appointmentOptionalAttendee.GetByAppointmentID(personAppointment1);
            Assert.AreEqual(2, optionalAtendees.Count());

            personAppointmentsPage
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001 Updated")
                .ValidateSubjectFieldText("Appointment 001 Updated")
                .ValidateRequiredRecordText(requiredSystemUserId1.ToString(), _requiredSystemUserFullname)
                .ValidateRequiredRecordText(_personId.ToString(), _person_fullName)
                .ValidateOptionalRecordText(optionalProfessionalId1.ToString(), optionalProfessionalFullName)
                .ValidateOptionalRecordText(optionalSystemUserId1.ToString(), optionalSystemUserFullName)

                .LoadMeetingNotesRichTextBox()
                .ValidateMeetingNotesFieldText("1", "line 1")
                .ValidateMeetingNotesFieldText("2", "line 2")

                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001 Updated")

                .ValidateStartDateFieldText("23/07/2021")
                .ValidateStartTimeFieldText("10:00")
                .ValidateShowTimeAsFieldText("Busy")
                .ValidateEndDateFieldText("23/07/2021")
                .ValidateEndTimeFieldText("10:20")
                .ValidateAllowConcurrentAppointmentYesOptionChecked(false)
                .ValidateAllowConcurrentAppointmentNoOptionChecked(true)

                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateAppointmentTypeLinkFieldText("Meeting")
                .ValidateLocationFieldText("location info ...")
                .ValidatePriorityLinkFieldText("Normal")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_loginUserFullName)
                .ValidateIsCaseNoteYesOptionChecked(true)
                .ValidateIsCaseNoteNoOptionChecked(false)
                .ValidateCategoryLinkFieldText("Advice")
                .ValidateSubCategoryLinkFieldText("Home Support")
                .ValidateReasonLinkFieldText("Assessment")
                .ValidateOutcomeLinkFieldText("More information needed")
                .ValidateSelectedStatus("Scheduled")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(false)

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateText("21/07/2021")
                .ValidateEventCategoryLinkFieldText("Category 1")
                .ValidateEventSubCategoryLinkFieldText("Sub Cat 1_1");

        }

        [TestProperty("JiraIssueID", "CDV6-24656")]
        [Description("Open a person record (person has 1 appointments linked to it with data in all fields) - Navigate to the Person Appointments screen - Click on the person appointments record - " +
            "wait for the record page to load - Remove the value from the mandatory fields - click on the save and close button - Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod13()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Appointment & Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", _person_fullName);

            #endregion

            #region Create Professional & System User / Appointment Attendee

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .InsertSubject("")
                .ClickRequiredRecordRemoveButton(requiredSystemUserId1.ToString())
                .ClickRequiredRecordRemoveButton(_personId.ToString())
                .InsertStartDate("")
                .InsertStartTime("")
                .InsertEndDate("")
                .InsertEndTime("")
                .InsertEventDate("")
                .ClickEventCategoryRemoveButton()

                .ClickSaveAndCloseButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")

                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateRequiredErrorLabelVisible(true)
                .ValidateRequiredErrorLabelText("Please fill out this field.")

                .ValidateStartDateErrorLabelVisible(true)
                .ValidateStartDateErrorLabelText("Please fill out this field.")
                .ValidateStartTimeErrorLabelVisible(true)
                .ValidateStartTimeErrorLabelText("Please fill out this field.")
                .ValidateEndDateErrorLabelVisible(true)
                .ValidateEndDateErrorLabelText("Please fill out this field.")
                .ValidateEndTimeErrorLabelVisible(true)
                .ValidateEndTimeErrorLabelText("Please fill out this field.")
                .ValidateEventDateErrorLabelVisible(true)
                .ValidateEventDateErrorLabelText("Please fill out this field.")
                .ValidateEventCategoryErrorLabelVisible(true)
                .ValidateEventCategoryErrorLabelText("Please fill out this field.");

        }

        [TestProperty("JiraIssueID", "CDV6-24657")]
        [Description("Open a person record (person has 1 appointments linked to it with data in all fields) - Navigate to the Person Appointments screen - Click on the person appointments record - " +
            "wait for the record page to load - Update the value from the subject field - click on the back button - Validate that a warning is displayed to the user - " +
            "Confirm the back operation - Validate that the subject field was not updated")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod14()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Appointment / Attenedee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", _person_fullName);

            #endregion

            #region Create Professional & System User / Appointment Attendee

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .InsertSubject("Appointment 001 Updated")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageHeaderSectionToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSubjectFieldText("Appointment 001");

        }

        [TestProperty("JiraIssueID", "CDV6-24658")]
        [Description("Open a person record (person has 1 appointments linked to it with data in all fields) - Navigate to the Person Appointments screen - Click on the person appointments record - " +
            "wait for the record page to load - Update the value from the subject field - click on the back button - Validate that a warning is displayed to the user - " +
            "click on the cancel button on the alert - validate that the alert is closed - click on the back button again - Validate that the alert is displayed again")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod15()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Appointment / Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", _person_fullName);

            #endregion

            #region Create Professional & System User / Appointment Attendee

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .InsertSubject("Appointment 001 Updated")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapCancelButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageHeaderSectionToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSubjectFieldText("Appointment 001");

        }

        [TestProperty("JiraIssueID", "CDV6-24659")]
        [Description("Open a person record - Navigate to the Person Appointments screen - Click on the add new record button - " +
            "Wait for the new record page to load - Set data in all fields - Click on the save and close button - Validate that a new record is displayed - Open the record - " +
            "Validate that the record is correctly Saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod16()
        {
            #region Create System User and Work Schedule

            var recurrencePatternId = dbHelper.recurrencePattern.GetRecurrencePatternIdByName("Occurs every 1 days")[0];
            var _requiredUsername = "RequiredSystemUser2" + _currentDateSuffix;
            var requiredSystemUserId1 = commonMethodsDB.CreateSystemUserRecord(_requiredUsername, "RequiredSystemUser2", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            _requiredSystemUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(requiredSystemUserId1, "fullname")["fullname"];
            dbHelper.userWorkSchedule.CreateUserWorkSchedule("Default", requiredSystemUserId1, _careDirectorQA_TeamId, recurrencePatternId, new DateTime(2021, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            #endregion

            #region Appointment Attendee

            var optionalProfessionalFirstName = "Appointment_Professional";
            var optionalProfessionalFullName = "Mr Appointment_Professional User1";
            var optionalProfessionalId1 = commonMethodsDB.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", optionalProfessionalFirstName, "User1", "professionaluser1@testmail.com");

            var optionalSystemUserName = "OptionalSystemUser1" + _currentDateSuffix;
            var optionalSystemUserFullName = "OptionalSystemUser1 " + _currentDateSuffix;
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord(optionalSystemUserName, "OptionalSystemUser1", _currentDateSuffix, "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            commonMethodsDB.CreateUserWorkSchedule("Default", optionalSystemUserId1, _careDirectorQA_TeamId, recurrencePatternId, new DateTime(2021, 1, 1), null, new TimeSpan(1, 0, 0), new TimeSpan(23, 0, 0));

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ClickNewRecordButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .InsertSubject("Appointment 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .SelectBusinessObjectByText("System Users").TypeSearchQuery(_requiredSystemUserFullname).TapSearchButton().AddElementToList(requiredSystemUserId1.ToString())
                .SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(personNumber.ToString()).TapSearchButton().AddElementToList(_personId.ToString())
                .TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickOptionalLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .SelectBusinessObjectByText("System Users").TypeSearchQuery(optionalSystemUserName.ToString()).TapSearchButton().AddElementToList(optionalSystemUserId1.ToString())
                .SelectBusinessObjectByText("Professionals").TypeSearchQuery(optionalProfessionalFirstName.ToString()).TapSearchButton().AddElementToList(optionalProfessionalId1.ToString())
                .TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .InsertMeetingNotes("<p>line 1</p>  <p>line 2</p>")
                .InsertStartDate("23/07/2021")
                .InsertStartTime("10:00")
                .InsertEndDate("23/07/2021")
                .InsertEndTime("10:20")
                .SelectShowTimeAs("Tentative")
                .ClickAllowConcurrentAppointmentYesRadioButton()
                .ClickAppointmentTypeLookupButton();
            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Meeting").TapSearchButton().SelectResultElement(_appointmentTypeId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .InsertLocation("location info ...");

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Normal").TapSearchButton().SelectResultElement(_activityPriorityId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_loginUsername).TapSearchButton().SelectResultElement(_defaultLoginUserID.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickIsCaseNoteYesRadioButton()
                .ClickCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Advice").TapSearchButton().SelectResultElement(_activityCategoryId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Home Support").TapSearchButton().SelectResultElement(_activitySubCategoryId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Assessment").TapSearchButton().SelectResultElement(_activityReasonId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("More information needed").TapSearchButton().SelectResultElement(_activityOutcomeId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickContainsInformationProvidedByThirdPartyYesRadioButton()
                .ClickSignificantEventYesRadioButton()
                .InsertEventDate("21/07/2021")
                .ClickEventCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Category 1").TapSearchButton().SelectResultElement(_significantEventCategoryId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickEventSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Sub Cat 1_1").TapSearchButton().SelectResultElement(_significantEventSubCategoryId.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickSaveButton()
                .ClickBackButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad();

            var appointments = dbHelper.appointment.GetAppointmentByPersonID(_personId);
            Assert.AreEqual(1, appointments.Count);
            var personAppointment1 = appointments.FirstOrDefault();

            var requiredAtendees = dbHelper.appointmentRequiredAttendee.GetByAppointmentID(personAppointment1);
            Assert.AreEqual(2, requiredAtendees.Count());
            var optionalAtendees = dbHelper.appointmentOptionalAttendee.GetByAppointmentID(personAppointment1);
            Assert.AreEqual(2, optionalAtendees.Count());

            personAppointmentsPage
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSubjectFieldText("Appointment 001")
                .ValidateRequiredRecordText(requiredSystemUserId1.ToString(), _requiredSystemUserFullname)
                .ValidateRequiredRecordText(_personId.ToString(), _person_fullName)
                .ValidateOptionalRecordText(optionalProfessionalId1.ToString(), optionalProfessionalFullName)
                .ValidateOptionalRecordText(optionalSystemUserId1.ToString(), optionalSystemUserFullName)

                .LoadMeetingNotesRichTextBox()
                .ValidateMeetingNotesFieldText("1", "line 1")
                .ValidateMeetingNotesFieldText("2", "line 2")

                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")

                .ValidateStartDateFieldText("23/07/2021")
                .ValidateStartTimeFieldText("10:00")
                .ValidateShowTimeAsFieldText("Tentative")
                .ValidateEndDateFieldText("23/07/2021")
                .ValidateEndTimeFieldText("10:20")
                .ValidateAllowConcurrentAppointmentYesOptionChecked(true)
                .ValidateAllowConcurrentAppointmentNoOptionChecked(false)

                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateAppointmentTypeLinkFieldText("Meeting")
                .ValidateLocationFieldText("location info ...")
                .ValidatePriorityLinkFieldText("Normal")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText(_loginUserFullName)
                .ValidateIsCaseNoteYesOptionChecked(true)
                .ValidateIsCaseNoteNoOptionChecked(false)
                .ValidateCategoryLinkFieldText("Advice")
                .ValidateSubCategoryLinkFieldText("Home Support")
                .ValidateReasonLinkFieldText("Assessment")
                .ValidateOutcomeLinkFieldText("More information needed")
                .ValidateSelectedStatus("Scheduled")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(true)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(false)

                .ValidateSignificantEventYesOptionChecked(true)
                .ValidateSignificantEventNoOptionChecked(false)
                .ValidateEventDateText("21/07/2021")
                .ValidateEventCategoryLinkFieldText("Category 1")
                .ValidateEventSubCategoryLinkFieldText("Sub Cat 1_1")
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24660")]
        [Description("Open a person record - Navigate to the Person Appointments screen - Click on the add new record button - " +
            "Wait for the new record page to load - Set data in the mandatory fields only - Click on the save and close button - Validate that a new record is displayed - Open the record - " +
            "Validate that the record is correctly Saved")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod17()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ClickNewRecordButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .InsertSubject("Appointment 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup.WaitForLookupMultiSelectPopupToLoad()
                .SelectBusinessObjectByText("People").SelectViewByText("All Active People").TypeSearchQuery(personNumber.ToString()).TapSearchButton().AddElementToList(_personId.ToString())
                .TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .InsertStartDate("23/07/2021")
                .InsertStartTime("10:00")
                .SelectShowTimeAs("Tentative")
                .InsertEndDate("23/07/2021")
                .InsertEndTime("10:20")
                .ClickAllowConcurrentAppointmentYesRadioButton()
                .ClickSaveAndCloseButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad();

            var appointments = dbHelper.appointment.GetAppointmentByPersonID(_personId);
            Assert.AreEqual(1, appointments.Count);
            var personAppointment1 = appointments.FirstOrDefault();

            var requiredAtendees = dbHelper.appointmentRequiredAttendee.GetByAppointmentID(personAppointment1);
            Assert.AreEqual(1, requiredAtendees.Count());
            var optionalAtendees = dbHelper.appointmentOptionalAttendee.GetByAppointmentID(personAppointment1);
            Assert.AreEqual(0, optionalAtendees.Count());

            personAppointmentsPage
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSubjectFieldText("Appointment 001")
                .ValidateRequiredRecordText(_personId.ToString(), _person_fullName)

                .LoadMeetingNotesRichTextBox()
                .ValidateMeetingNotesFieldText("1", "")

                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")

                .ValidateStartDateFieldText("23/07/2021")
                .ValidateStartTimeFieldText("10:00")
                .ValidateShowTimeAsFieldText("Tentative")
                .ValidateEndDateFieldText("23/07/2021")
                .ValidateEndTimeFieldText("10:20")
                .ValidateAllowConcurrentAppointmentYesOptionChecked(true)
                .ValidateAllowConcurrentAppointmentNoOptionChecked(false)

                .ValidateRegardingLinkFieldText(_person_fullName)
                .ValidateAppointmentTypeLinkFieldText("")
                .ValidateLocationFieldText("")
                .ValidatePriorityLinkFieldText("")
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleUserLinkFieldText("")
                .ValidateIsCaseNoteYesOptionChecked(false)
                .ValidateIsCaseNoteNoOptionChecked(true)
                .ValidateCategoryLinkFieldText("")
                .ValidateSubCategoryLinkFieldText("")
                .ValidateReasonLinkFieldText("")
                .ValidateOutcomeLinkFieldText("")
                .ValidateSelectedStatus("Scheduled")
                .ValidateContainsInformationProvidedByThirdPartyYesOptionChecked(false)
                .ValidateContainsInformationProvidedByThirdPartyNoOptionChecked(true)

                .ValidateSignificantEventYesOptionChecked(false)
                .ValidateSignificantEventNoOptionChecked(true)
                .ValidateEventDateFieldVisibility(false)
                .ValidateEventCategoryFieldVisibility(false)
                .ValidateEventSubCategoryFieldVisibility(false)
                ;
        }

        [TestProperty("JiraIssueID", "CDV6-24661")]
        [Description("Open a person record - Navigate to the Person Appointments screen - Click on the add new record button - " +
            "Wait for the new record page to load - Set Significant Event to Yes - Remove the Responsible Team - Do not set data in any of the mandatory fields - " +
            "Click on the save and close button - Validate that the user is prevented from saving the record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod18()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ClickNewRecordButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("New")
                .ClickResponsibleTeamRemoveButton()
                .ClickResponsibleUserRemoveButton()
                .ClickSignificantEventYesRadioButton()
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartDate("")
                .InsertEndDate("")
                .ClickSaveAndCloseButton();

            personAppointmentRecordPage
                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.");

            personAppointmentRecordPage
                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateRequiredErrorLabelVisible(true)
                .ValidateRequiredErrorLabelText("Please fill out this field.");

            personAppointmentRecordPage
                .ValidateStartDateErrorLabelVisible(true)
                .ValidateStartDateErrorLabelText("Please fill out this field.")
                .ValidateStartTimeErrorLabelVisible(true)
                .ValidateStartTimeErrorLabelText("Please fill out this field.")
                .ValidateEndDateErrorLabelVisible(true)
                .ValidateEndDateErrorLabelText("Please fill out this field.")
                .ValidateEndTimeErrorLabelVisible(true)
                .ValidateEndTimeErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamErrorLabelVisible(true)
                .ValidateResponsibleTeamErrorLabelText("Please fill out this field.")
                .ValidateEventDateErrorLabelVisible(true)
                .ValidateEventDateErrorLabelText("Please fill out this field.")
                .ValidateEventCategoryErrorLabelVisible(true)
                .ValidateEventCategoryErrorLabelText("Please fill out this field.");

            var records = dbHelper.appointment.GetAppointmentByRegardingID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-24662")]
        [Description("Open a person record (person has 1 appointments linked to it) - Navigate to the Person Appointments screen - " +
            "Click on the person appointments record - Wait for the record page to load - Click on the delete button - Confirm the delete operation - Validate that the record is removed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod19()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Appointment / Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", _person_fullName);

            #endregion

            #region Create Professional & System User / Appointment Attendee

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickDeleteButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("The system will delete this record. This action cannot be undone. To continue, click ok.").TapOKButton();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .ValidateRecordNotVisible(personAppointment1.ToString());

            var records = dbHelper.appointment.GetAppointmentByPersonID(_personId);
            Assert.AreEqual(0, records.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-24663")]
        [Description("Open a person record (person has 1 appointments linked to it) - Navigate to the Person Appointments screen - " +
            "Click on the person appointments record - Wait for the record page to load - Click on the Cancel button - Confirm the cancel operation - Validate that the record gets disabled - " +
            "Validate that the record status is updated to Cancelled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod20()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Appointment / Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", _person_fullName);

            #endregion

            #region Create Professional & System User / Appointment Attendee

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickCancelButton()
                .WaitForDisabledPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSelectedStatus("Cancelled");

            var fields = dbHelper.appointment.GetAppointmentByID(personAppointment1, "inactive");
            Assert.AreEqual(true, fields["inactive"]);

        }

        [TestProperty("JiraIssueID", "CDV6-24664")]
        [Description("Open a person record (person has 1 appointments linked to it) - Navigate to the Person Appointments screen - " +
            "Click on the person appointments record - Wait for the record page to load - Click on the Complete button - Confirm the Complete operation - Validate that the record gets disabled - " +
            "Validate that the record status is updated to Completed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod21()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Appointment / Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", _person_fullName);

            #endregion

            #region Create Professional & System User / Appointment Attendee

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickCompleteButton()
                .WaitForDisabledPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSelectedStatus("Completed");

            var fields = dbHelper.appointment.GetAppointmentByID(personAppointment1, "inactive");
            Assert.AreEqual(true, fields["inactive"]);

        }

        [TestProperty("JiraIssueID", "CDV6-24665")]
        [Description("Open a person record (person has 1 appointments linked to it) - Navigate to the Person Appointments screen - " +
            "Click on the person appointments record - Wait for the record page to load - Click on the Complete button - Confirm the Complete operation - Validate that the record gets disabled - " +
            "Click on the Activate button - Confirm the activate operation - Validate that the record is active and editable")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod22()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            #region Appointment / Attendee

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 001", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, "person", _person_fullName,
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, requiredSystemUserId1, "systemuser", _requiredSystemUserFullname);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", _person_fullName);

            #endregion

            #region Create Professional & System User / Appointment Attendee

            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .ClickCompleteButton()
                .WaitForDisabledPersonAppointmentRecordPageToLoad("Appointment 001")
                .ValidateSelectedStatus("Completed");

            var fields = dbHelper.appointment.GetAppointmentByID(personAppointment1, "inactive");
            Assert.AreEqual(true, fields["inactive"]);

            personAppointmentRecordPage
                .ClickActivateButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 001")
                .InsertSubject("Appointment 001"); //if the driver can insert text it means that the record is active

            fields = dbHelper.appointment.GetAppointmentByID(personAppointment1, "inactive");
            Assert.AreEqual(false, fields["inactive"]);

        }

        [TestProperty("JiraIssueID", "CDV6-666")]
        [Description("To verify user is able to 'Reactivate' the Appointment Activity record upon clicking the 'Activate' icon in the toolbar, when the status is set to 'Complete' in the Appointment Activity which is associated with the Person record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonAppointment_UITestMethod23()
        {
            var StartDate = new DateTime(2021, 7, 20);
            var StartTime = new TimeSpan(9, 0, 0);
            var EndDate = new DateTime(2021, 7, 20);
            var EndTime = new TimeSpan(9, 15, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            bool IsSignificantEvent = true;
            var significanteventdate = new DateTime(2021, 7, 21);

            var personAppointment1 = dbHelper.appointment.CreateAppointment(_careDirectorQA_TeamId, _personId,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId, _defaultLoginUserID, _appointmentTypeId,
                "Appointment 671", "<p>line 1</p>  <p>line 2</p>", "Location info ...", StartDate, StartTime, EndDate, EndTime, _personId, firstName + " " + lastName, "person",
                statusid, showTimeAsId,
                IsSignificantEvent, significanteventdate, _significantEventCategoryId, _significantEventSubCategoryId, true, false, true, false);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _defaultLoginUserID, "systemuser", _loginUserFullName);
            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(personAppointment1, _personId, "person", firstName + " " + lastName);
            var optionalProfessionalId1 = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, _professionTypeId, "Mr", "Professional", "User1", "professionaluser1@testmail.com");
            var optionalSystemUserId1 = commonMethodsDB.CreateSystemUserRecord("OptionalUser1", "Optional", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalProfessionalId1, "professional", "Mr Professional User1");
            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(personAppointment1, optionalSystemUserId1, "systemuser", "Optional User1");

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByFullName(firstName, lastName)
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(personAppointment1.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 671")
                .ClickCompleteButton()
                .WaitForDisabledPersonAppointmentRecordPageToLoad("Appointment 671")
                .ValidateSelectedStatus("Completed");

            var fields = dbHelper.appointment.GetAppointmentByID(personAppointment1, "inactive");
            Assert.AreEqual(true, fields["inactive"]);

            personAppointmentRecordPage
                .ClickActivateButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Appointment 671")
                .ValidateSelectedStatus("Scheduled");

            fields = dbHelper.appointment.GetAppointmentByID(personAppointment1, "inactive");
            Assert.AreEqual(false, fields["inactive"]);

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickSignOutButton();
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

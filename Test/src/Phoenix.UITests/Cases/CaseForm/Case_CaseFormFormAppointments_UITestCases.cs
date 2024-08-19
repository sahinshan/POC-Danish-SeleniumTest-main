using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases.CaseForm
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [DeploymentItem("Files\\Testing CDV6-8614.Zip"), DeploymentItem("chromedriver.exe")]
    [TestClass]
    public class Case_CaseFormFormAppointments_UITestCases : FunctionalTest
    {

        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private Guid _documentId;
        private Guid _ethnicityId;
        private Guid _personID;
        private Guid _caseId;
        private string _caseNumber;
        private Guid _closedCaseStatusId;
        private Guid _contactReasonId;
        private Guid _dataFormId;
        private Guid _contactSourceId;
        private Guid _caseFormId;
        private string _caseFormTitle;
        private Guid _appointmentTypeId;
        private Guid _activityPriorityId;

        private Guid _activityCategoryId;
        private Guid _activitySubCategoryId;
        private Guid _activityOutcomeId;
        private Guid _activityReasonId;

        [TestInitialize]
        public void TestInitializationMethod()
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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CaseFormApt BU");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CaseFormApt Team", null, _businessUnitId, "907678", "CaseFormAptTeam@careworkstempmail.com", "CaseFormApt Team", "020 123456");

                #endregion

                #region System User CaseFormAppointmentUser1

                _systemUserId = commonMethodsDB.CreateSystemUserRecord("CaseFormAppointmentUser1", "CaseFormAppointment", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

                #region Document 

                var DocumentExist = dbHelper.document.GetDocumentByName("Testing CDV6-8614").Any();
                if (!DocumentExist)
                {
                    var documentByteArray1 = fileIOHelper.ReadFileIntoByteArray(TestContext.DeploymentDirectory + "\\Testing CDV6-8614.Zip");

                    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray1, "Testing CDV6-8614.Zip");

                    _documentId = dbHelper.document.GetDocumentByName("Testing CDV6-8614").FirstOrDefault();

                    dbHelper.document.UpdateStatus(_documentId, 100000000); //Set the status to published 
                }

                if (_documentId == Guid.Empty)
                    _documentId = dbHelper.document.GetDocumentByName("Testing CDV6-8614").FirstOrDefault();

                #endregion

                #region Ethnicity

                if (!dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any())
                    dbHelper.ethnicity.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion

                #region Person

                var firstName = "Pat";
                var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
                var fullName = firstName + " " + lastName;
                _personID = commonMethodsDB.CreatePersonRecord(firstName, lastName, _ethnicityId, _teamId);

                #endregion

                #region Case Status

                _closedCaseStatusId = dbHelper.caseStatus.GetByName("Closed")[0];

                #endregion

                #region Contact Reason

                _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

                #endregion

                #region Data Form

                _dataFormId = dbHelper.dataForm.GetByName("SocialCareCase")[0];

                #endregion

                #region Contact Source

                _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Anonymus", _teamId);

                #endregion

                #region Case

                _caseId = dbHelper.Case.CreateSocialCareCaseRecord(_teamId, _personID, _systemUserId, _systemUserId, _closedCaseStatusId, _contactReasonId, _dataFormId, _contactSourceId, new DateTime(2020, 02, 02), new DateTime(2020, 02, 02), 21);
                _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];


                #endregion

                #region Case Form

                int assessmentstatusid = 1; //In Progress
                var assessmentStartDate = new DateTime(2021, 3, 19);
                _caseFormId = dbHelper.caseForm.CreateCaseForm(_teamId, _personID, fullName, _systemUserId, _caseId, _caseNumber, _documentId, "Testing CDV6-8614", assessmentstatusid, assessmentStartDate, null, null);
                _caseFormTitle = (string)(dbHelper.caseForm.GetCaseFormByID(_caseFormId, "title")["title"]);

                #endregion

                #region Appointment Type

                _appointmentTypeId = commonMethodsDB.CreateAppointmentTypeIfNeeded("Conference", _teamId);

                #endregion

                #region Activity Priority

                _activityPriorityId = commonMethodsDB.CreateActivityPriority(new Guid("5246a13f-9d45-e911-a2c5-005056926fe4"), "Normal", new DateTime(2022, 1, 1), _teamId);

                #endregion

                #region Activity Category

                _activityCategoryId = commonMethodsDB.CreateActivityCategory(new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"), "Advice", new DateTime(2021, 1, 1), _teamId);

                #endregion

                #region Activity Sub Category

                _activitySubCategoryId = commonMethodsDB.CreateActivitySubCategory(new Guid("1515dfdd-9d45-e911-a2c5-005056926fe4"), "Home Support", new DateTime(2021, 1, 1), _activityCategoryId, _teamId);

                #endregion

                #region Activity Outcome

                _activityOutcomeId = commonMethodsDB.CreateActivityOutcome(new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"), "More information needed", new DateTime(2021, 1, 1), _teamId);

                #endregion

                #region Activity Reason

                _activityReasonId = commonMethodsDB.CreateActivityReason(new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"), "Assessment", new DateTime(2021, 1, 1), _teamId);

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


        [TestProperty("JiraIssueID", "CDV6-9939")]
        [Description(
           "Login in the web app - Open a Case Form record - Navigate to the Case Form Appointments area - " +
            "Validate that the Case Form Appointments page is displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormAppointments_UITestMethod01()
        {
            #region Appointment 01

            var startDate = new DateTime(2021, 02, 22);
            var startTime = new TimeSpan(11, 05, 0);
            var endTime = new TimeSpan(11, 10, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            var IsSignificantEvent = false;

            var caseFormAppointment1ID = dbHelper.appointment.CreateAppointment(_teamId, _personID,
                null, null, null, null, _activityPriorityId,
                _systemUserId, _appointmentTypeId,
                "Case Form Appointment 01", "Notes ...", "location ....",
                startDate, startTime, startDate, endTime,
                _caseFormId, _caseFormTitle, "CaseForm",
                statusid, showTimeAsId,
                IsSignificantEvent, null, null, null,
                false, false, false, false);

            #endregion

            #region Appointment 02

            startDate = new DateTime(2021, 02, 23);
            startTime = new TimeSpan(11, 30, 0);
            endTime = new TimeSpan(12, 00, 0);

            var caseFormAppointment2ID = dbHelper.appointment.CreateAppointment(_teamId, _personID,
                null, null, null, null, null,
                _systemUserId, _appointmentTypeId,
                "Case Form Appointment 02", "Notes ...", "",
                startDate, startTime, startDate, endTime,
                _caseFormId, _caseFormTitle, "CaseForm",
                statusid, showTimeAsId,
                IsSignificantEvent, null, null, null,
                false, false, false, false);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CaseFormAppointmentUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormAppointmentsArea();

            caseFormAppointmentsPage
                .WaitForCaseFormAppointmentsPageToLoad()

                .ValidateSubjectCellText(caseFormAppointment1ID.ToString(), "Case Form Appointment 01")
                .ValidateStartDateCellText(caseFormAppointment1ID.ToString(), "22/02/2021")
                .ValidateStartTimeCellText(caseFormAppointment1ID.ToString(), "11:05")
                .ValidateEndDateCellText(caseFormAppointment1ID.ToString(), "22/02/2021")
                .ValidateEndTimeCellText(caseFormAppointment1ID.ToString(), "11:10")
                .ValidateLocationCellText(caseFormAppointment1ID.ToString(), "location ....")
                .ValidatePriorityCellText(caseFormAppointment1ID.ToString(), "Normal")

                .ValidateSubjectCellText(caseFormAppointment2ID.ToString(), "Case Form Appointment 02")
                .ValidateStartDateCellText(caseFormAppointment2ID.ToString(), "23/02/2021")
                .ValidateStartTimeCellText(caseFormAppointment2ID.ToString(), "11:30")
                .ValidateEndDateCellText(caseFormAppointment2ID.ToString(), "23/02/2021")
                .ValidateEndTimeCellText(caseFormAppointment2ID.ToString(), "12:00")
                .ValidateLocationCellText(caseFormAppointment2ID.ToString(), "")
                .ValidatePriorityCellText(caseFormAppointment2ID.ToString(), "");
        }

        [TestProperty("JiraIssueID", "CDV6-9940")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Appointments area - " +
            "Search for Case Form Appointment record using a search query that should match a record subject - " +
            "Tap on the search button - Validate that the matching records are displayed")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormAppointments_UITestMethod02()
        {
            #region Appointment 01

            var startDate = new DateTime(2021, 02, 22);
            var startTime = new TimeSpan(11, 05, 0);
            var endTime = new TimeSpan(11, 10, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            var IsSignificantEvent = false;

            var caseFormAppointment1ID = dbHelper.appointment.CreateAppointment(_teamId, _personID,
                null, null, null, null, _activityPriorityId,
                _systemUserId, _appointmentTypeId,
                "Case Form Appointment 01", "Notes ...", "location ....",
                startDate, startTime, startDate, endTime,
                _caseFormId, _caseFormTitle, "CaseForm",
                statusid, showTimeAsId,
                IsSignificantEvent, null, null, null,
                false, false, false, false);

            #endregion

            #region Appointment 02

            startDate = new DateTime(2021, 02, 23);
            startTime = new TimeSpan(11, 30, 0);
            endTime = new TimeSpan(12, 00, 0);

            var caseFormAppointment2ID = dbHelper.appointment.CreateAppointment(_teamId, _personID,
                null, null, null, null, null,
                _systemUserId, _appointmentTypeId,
                "Case Form Appointment 02", "Notes ...", "",
                startDate, startTime, startDate, endTime,
                _caseFormId, _caseFormTitle, "CaseForm",
                statusid, showTimeAsId,
                IsSignificantEvent, null, null, null,
                false, false, false, false);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseFormAppointmentUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormAppointmentsArea();

            caseFormAppointmentsPage
                .WaitForCaseFormAppointmentsPageToLoad()
                .SearchCaseFormAppointmentRecord("Case Form Appointment 01")
                .ValidateRecordPresent(caseFormAppointment1ID.ToString())
                .ValidateRecordNotPresent(caseFormAppointment2ID.ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-9941")]
        [Description(
            "Login in the web app - Open a Case Form record - Navigate to the Case Form Appointments area - Open a Case Form Appointment record (all fields must have values) - " +
            "Validate that the user is redirected to the Case Form Appointments record Page - Validate all field values")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void CaseFormAppointments_UITestMethod03()
        {
            #region System User CaseFormAppointmentUser1

            var AaronKirk_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AaronKirk", "Aaron", "Kirk", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            var AlbertEinstein_SystemUserID = commonMethodsDB.CreateSystemUserRecord("AlbertEinstein", "ALBERT", "Einstein", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Appointment 01

            var startDate = new DateTime(2021, 02, 22);
            var startTime = new TimeSpan(11, 05, 0);
            var endTime = new TimeSpan(11, 10, 0);
            var statusid = 4; //Scheduled
            var showTimeAsId = 5; //Busy
            var IsSignificantEvent = false;

            var caseFormAppointment1ID = dbHelper.appointment.CreateAppointment(_teamId, _personID,
                _activityCategoryId, _activitySubCategoryId, _activityOutcomeId, _activityReasonId, _activityPriorityId,
                _systemUserId, _appointmentTypeId,
                "Case Form Appointment 01", "<p>Case Form Appointment 01 meeting notes</p>", "location ....",
                startDate, startTime, startDate, endTime,
                _caseFormId, "CaseForm", _caseFormTitle,
                statusid, showTimeAsId,
                IsSignificantEvent, null, null, null,
                false, false, false, false);

            #endregion

            #region Appointment Required Atendee

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(caseFormAppointment1ID, AaronKirk_SystemUserID, "systemuser", "Aaron Kirk");

            #endregion

            #region Appointment Optional Atendee

            dbHelper.appointmentOptionalAttendee.CreateAppointmentOptionalAttendee(caseFormAppointment1ID, AlbertEinstein_SystemUserID, "systemuser", "ALBERT Einstein");

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CaseFormAppointmentUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .NavigateToFormsCase();

            caseCasesFormPage
                .WaitForCaseCaseFormPageToLoad()
                .OpenRecord(_caseFormId.ToString());

            caseFormPage
                .WaitForCaseFormPageToLoad()
                .NavigateToCaseFormAppointmentsArea();

            caseFormAppointmentsPage
                .WaitForCaseFormAppointmentsPageToLoad()
                .OpenCaseFormAppointmentRecord(caseFormAppointment1ID.ToString());

            caseFormAppointmentRecordPage
                .WaitForCaseFormAppointmentRecordPageToLoad("Case Form Appointment 01")

                .ValidateSubject("Case Form Appointment 01")
                .ValidateRequired(AaronKirk_SystemUserID.ToString(), "Aaron Kirk\r\nRemove")
                .ValidateOptional(AlbertEinstein_SystemUserID.ToString(), "ALBERT Einstein\r\nRemove")
                .ValidateMeetingNotes("<p>Case Form Appointment 01 meeting notes</p>")

                .ValidateStartDate("22/02/2021")
                .ValidateEndDate("22/02/2021")
                .ValidateShowTimeAs("Busy")
                .ValidateStartTime("11:05")
                .ValidateEndTime("11:10")
                .ValidateAllowConcurrentAppointmentCheckedOption(false)

                .ValidateRegardingFieldLinkText(_caseFormTitle)
                .ValidateAppointmentTypeFieldLinkText("Conference")
                .ValidateLocation("location ....")
                .ValidatePriorityFieldLinkText("Normal")
                .ValidateResponsibleTeamFieldLinkText("CaseFormApt Team")
                .ValidateResponsibleUserFieldLinkText("CaseFormAppointment User1")
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

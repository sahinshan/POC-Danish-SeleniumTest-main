using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{

    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class Person_RecurringAppointments_UITestCases : FunctionalTest
    {
        private Guid _businessUnitId;
        private Guid _languageId;
        private string _teamName;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private string _systemUsername;
        private string _systemUserFullName;
        private Guid _systemUserId2;
        private string _systemUsername2;
        private string _systemUserFullName2;
        private string _currentDateTime;
        private Guid recurrencePatternId;
        private Guid _personId;
        private int _personNumber;
        private string _personFullName;

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

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Recurring Appointment BU1");

                #endregion

                #region Team

                _teamName = "Recurring Appointment T1";
                _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, "907678", "RecurringAppointmentT1@careworkstempmail.com", "RecurringAppointment T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Recurrence Pattern

                recurrencePatternId = dbHelper.recurrencePattern.GetRecurrencePatternIdByName("Occurs every 1 days")[0];

                #endregion

                #region System User Recurring Appointment User 1

                _currentDateTime = commonMethodsHelper.GetCurrentDateTimeString();
                _systemUsername = "RAU1_" + _currentDateTime;
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "RAU1", _currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                _systemUserFullName = "RAU1 " + _currentDateTime;

                #endregion

                #region System User Recurring Appointment User 2

                _systemUsername2 = "RAU2_" + _currentDateTime;
                _systemUserId2 = commonMethodsDB.CreateSystemUserRecord(_systemUsername2, "RAU2", _currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
                _systemUserFullName2 = "RAU2 " + _currentDateTime;

                #endregion

                #region User Work Schedule

                commonMethodsDB.CreateUserWorkSchedule("Default", _systemUserId, _teamId, recurrencePatternId, new DateTime(2021, 1, 1), null, new TimeSpan(0, 5, 0), new TimeSpan(23, 55, 0));
                commonMethodsDB.CreateUserWorkSchedule("Default", _systemUserId2, _teamId, recurrencePatternId, new DateTime(2021, 1, 1), null, new TimeSpan(0, 5, 0), new TimeSpan(23, 55, 0));

                #endregion

                #region Person

                var _firstName = "Person_Recurring";
                var _lastName = _currentDateTime;
                _personId = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
                _personNumber = (int)(dbHelper.person.GetPersonById(_personId, "personnumber")["personnumber"]);
                _personFullName = "Person_Recurring " + _currentDateTime;

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12502

        [TestProperty("JiraIssueID", "CDV6-12406")]
        [Description("Create a new recurring appointment with only the mandatory fields set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString())
                .TypeSearchQuery(_systemUsername2).TapSearchButton().AddElementToList(_systemUserId2.ToString()).TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("09:15")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("occurs every 1 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("01/09/2021")
                .InsertEndDate("03/09/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false);

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(1, recurringAppointments.Count);

            personRecurringAppointmentsPage
                .OpenPersonRecurringAppointmentRecord(recurringAppointments[0].ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("Recurring Appointment testing 001")
                .ValidateSubjectFieldText("Recurring Appointment testing 001")
                .ValidateRequiredRecordText(_systemUserId.ToString(), _systemUserFullName + "\r\nRemove")
                .ValidateRequiredRecordText(_systemUserId2.ToString(), _systemUserFullName2 + "\r\nRemove")

                .ValidateStartTimeFieldText("09:00")
                .ValidateEndTimeFieldText("09:15")
                .ValidateRecurrencePattern("Occurs every 1 days")
                .ValidateShowTimeAsFieldText("Busy")

                .ValidateStartDateFieldText("01/09/2021")
                .ValidateEndDateFieldText("03/09/2021")
                .ValidateFirstAppointmentStartDateFieldText("01/09/2021")
                .ValidateEndRangeSelectedText("By End Date")
                .ValidateNumberOfOccurrencesFieldText("")
                .ValidateLastAppointmentEndDateFieldText("03/09/2021")

                .ValidateRegardingLinkFieldText(_personFullName)
                .ValidateAppointmentTypeLinkFieldText("")
                .ValidateLocationFieldText("")
                .ValidatePriorityLinkFieldText("")
                .ValidateResponsibleTeamLinkFieldText(_teamName)
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateCategoryLinkFieldText("")
                .ValidateSubCategoryLinkFieldText("")
                .ValidateReasonLinkFieldText("")
                .ValidateOutcomeLinkFieldText("")
                .ValidateSelectedStatus("Scheduled");
        }

        [TestProperty("JiraIssueID", "CDV6-12407")]
        [Description("Try to save a new recurring appointment with missing mandatory fields")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod02()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartDate("")
                .InsertEndDate("")
                .SelectEndRange("")
                .ClickRegardingRemoveButton()
                .ClickResponsibleTeamRemoveButton()
                .ClickResponsibleUserRemoveButton()
                .ClickSaveButton()

                .ValidateMessageAreaVisible(true)
                .ValidateMessageAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateSubjectErrorLabelVisible(true)
                .ValidateSubjectErrorLabelText("Please fill out this field.")
                .ValidateRequiredErrorLabelVisible(true)
                .ValidateRequiredErrorLabelText("Please fill out this field.")
                .ValidateStartTimeErrorLabelVisible(true)
                .ValidateStartTimeErrorLabelText("Please fill out this field.")
                .ValidateEndTimeErrorLabelVisible(true)
                .ValidateEndTimeErrorLabelText("Please fill out this field.")
                .ValidateRecurrencePatternErrorLabelVisible(true)
                .ValidateRecurrencePatternErrorLabelText("Please fill out this field.")
                .ValidateStartDateErrorLabelVisible(true)
                .ValidateStartDateErrorLabelText("Please fill out this field.")
                .ValidateEndRangeErrorLabelVisible(true)
                .ValidateEndRangeErrorLabelText("Please fill out this field.")
                .ValidateRegardingErrorLabelVisible(true)
                .ValidateRegardingErrorLabelText("Please fill out this field.")
                .ValidateResponsibleTeamErrorLabelVisible(true)
                .ValidateResponsibleTeamErrorLabelText("Please fill out this field.")

                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(0, recurringAppointments.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12410")]
        [Description("Create a  Recurring Appointment Activity record in valid date range with 12 hours duration and hit save Icon")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod03()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString())
                .TypeSearchQuery(_systemUsername2).TapSearchButton().AddElementToList(_systemUserId2.ToString()).TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("21:00")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("occurs every 1 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("05/09/2021")
                .InsertEndDate("06/09/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false);

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(1, recurringAppointments.Count);

            personRecurringAppointmentsPage
                .OpenPersonRecurringAppointmentRecord(recurringAppointments[0].ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("Recurring Appointment testing 001")
                .ValidateSubjectFieldText("Recurring Appointment testing 001")
                .ValidateRequiredRecordText(_systemUserId.ToString(), _systemUserFullName + "\r\nRemove")
                .ValidateRequiredRecordText(_systemUserId2.ToString(), _systemUserFullName2 + "\r\nRemove")

                .ValidateStartTimeFieldText("09:00")
                .ValidateEndTimeFieldText("21:00")
                .ValidateRecurrencePattern("Occurs every 1 days")
                .ValidateShowTimeAsFieldText("Busy")

                .ValidateStartDateFieldText("05/09/2021")
                .ValidateEndDateFieldText("06/09/2021")
                .ValidateFirstAppointmentStartDateFieldText("05/09/2021")
                .ValidateEndRangeSelectedText("By End Date")
                .ValidateNumberOfOccurrencesFieldText("")
                .ValidateLastAppointmentEndDateFieldText("06/09/2021")

                .ValidateRegardingLinkFieldText(_personFullName)
                .ValidateAppointmentTypeLinkFieldText("")
                .ValidateLocationFieldText("")
                .ValidatePriorityLinkFieldText("")
                .ValidateResponsibleTeamLinkFieldText("Recurring Appointment T1")
                .ValidateResponsibleUserLinkFieldText(_systemUserFullName)
                .ValidateCategoryLinkFieldText("")
                .ValidateSubCategoryLinkFieldText("")
                .ValidateReasonLinkFieldText("")
                .ValidateOutcomeLinkFieldText("")
                .ValidateSelectedStatus("Scheduled");
        }

        [TestProperty("JiraIssueID", "CDV6-12411")]
        [Description("Try to create a Recurring Appointment Activity record by selecting the time slot more than 12 hours")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod04()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString())
                .TypeSearchQuery(_systemUsername2).TapSearchButton().AddElementToList(_systemUserId2.ToString()).TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("22:00")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("occurs every 1 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("05/09/2021")
                .InsertEndDate("06/09/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The maximum length of the Appoinment is 12 hours.")
                .TapCloseButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New");

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(0, recurringAppointments.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12412")]
        [Description("Try to create a Recurring Appointment with user A  selected in Required field overlapping with existing appointment activity - " +
            "For this test to work we need CW Forms Test User 1 to have a conflicting appointment on 02/09/2021 at 09:00 to 09:15 with another person record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod05()
        {
            #region Recurring appointment

            var requiredAttendees = new Dictionary<Guid, string>();
            requiredAttendees.Add(_systemUserId, _systemUsername);

            var recurringAppointmentId = dbHelper.recurringAppointment.CreateRecurringAppointment(_teamId, _personId,
                null, null, null, null, null, null, null,
                "Apt 01", "notes ...", "localtion ...",
                new DateTime(2021, 8, 24), new TimeSpan(9, 0, 0), new DateTime(2021, 8, 26), new TimeSpan(9, 30, 0),
                _personId, "person", _personFullName,
                4, 5, false, null, null, null,
                recurrencePatternId, 2, new DateTime(2021, 8, 24), new DateTime(2021, 8, 26), requiredAttendees);

            #endregion

            #region Person 2

            var _firstName2 = "Jhoana";
            var _lastName2 = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personID2 = commonMethodsDB.CreatePersonRecord(_firstName2, _lastName2, _ethnicityId, _teamId);
            var _personNumber2 = (int)(dbHelper.person.GetPersonById(_personID2, "personnumber")["personnumber"]);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber2.ToString(), _personID2.ToString())
                .OpenPersonRecord(_personID2.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString())
                .TypeSearchQuery(_systemUsername2).TapSearchButton().AddElementToList(_systemUserId2.ToString())
                .TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("09:30")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("occurs every 1 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("24/08/2021")
                .InsertEndDate("26/08/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("Required User " + _systemUserFullName + " has a conflicting Appointment at this time.")
                .TapCloseButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New");

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personID2);
            Assert.AreEqual(0, recurringAppointments.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12414")]
        [Description("Cancel a recurring appointment and validate that all appointments linked to the series are canceled")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod06()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString())
                .TypeSearchQuery(_systemUsername2).TapSearchButton().AddElementToList(_systemUserId2.ToString()).TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("09:15")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("occurs every 1 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("01/09/2021")
                .InsertEndDate("03/09/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad();

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(1, recurringAppointments.Count);

            //validate that the recurring appointment status is set to Scheduled
            var statusid = (int)dbHelper.recurringAppointment.GetByID(recurringAppointments[0], "statusid")["statusid"];
            Assert.AreEqual(4, statusid);

            var appointments = dbHelper.appointment.GetByRecurringAppointmentId(recurringAppointments[0]);
            Assert.AreEqual(3, appointments.Count);

            //all appointments linked to the series should have the Scheduled status
            statusid = (int)dbHelper.appointment.GetAppointmentByID(appointments[0], "statusid")["statusid"];
            Assert.AreEqual(4, statusid);
            statusid = (int)dbHelper.appointment.GetAppointmentByID(appointments[1], "statusid")["statusid"];
            Assert.AreEqual(4, statusid);
            statusid = (int)dbHelper.appointment.GetAppointmentByID(appointments[2], "statusid")["statusid"];
            Assert.AreEqual(4, statusid);

            personRecurringAppointmentsPage
                .OpenPersonRecurringAppointmentRecord(recurringAppointments[0].ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("Recurring Appointment testing 001")
                .ClickCancelButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("This will cancel all Appointments in this series, click OK to continue.").TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForCancelledPersonRecurringAppointmentRecordPageToLoad("Recurring Appointment testing 001");


            //validate that the recurring appointment status is set to Scheduled
            statusid = (int)dbHelper.recurringAppointment.GetByID(recurringAppointments[0], "statusid")["statusid"];
            Assert.AreEqual(3, statusid);

            //all appointments linked to the series should have the Scheduled status
            statusid = (int)dbHelper.appointment.GetAppointmentByID(appointments[0], "statusid")["statusid"];
            Assert.AreEqual(3, statusid);
            statusid = (int)dbHelper.appointment.GetAppointmentByID(appointments[1], "statusid")["statusid"];
            Assert.AreEqual(3, statusid);
            statusid = (int)dbHelper.appointment.GetAppointmentByID(appointments[2], "statusid")["statusid"];
            Assert.AreEqual(3, statusid);

        }

        [TestProperty("JiraIssueID", "CDV6-12415")]
        [Description("Try to create an Appointment by selecting user record who does not contain User diary ")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod07()
        {
            #region System User

            _systemUsername = "System_User" + _currentDateTime;
            _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "System_User", _currentDateTime, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);
            _systemUserFullName = "System_User " + _currentDateTime;

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString()).TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("09:15")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("occurs every 1 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("05/09/2021")
                .InsertEndDate("06/09/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            dynamicDialogPopup
                .WaitForDynamicDialogPopupToLoad()
                .ValidateMessage("The Required Attendee, " + _systemUserFullName + ", does not contain a User Diary record for the Appointment time.")
                .TapCloseButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New");

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(0, recurringAppointments.Count);

        }

        [TestProperty("JiraIssueID", "CDV6-12416")]
        [Description("Verify that a Recurring Appointment should be created with respect to the pattern")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod08()
        {
            #region Recurrence pattern

            if (!dbHelper.recurrencePattern.GetByTitle("Occurs every 2 days").Any())
                recurrencePatternId = dbHelper.recurrencePattern.CreateRecurrencePattern(1, 2);
            recurrencePatternId = dbHelper.recurrencePattern.GetByTitle("Occurs every 2 days").FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString())
                .TypeSearchQuery(_systemUsername2).TapSearchButton().AddElementToList(_systemUserId2.ToString()).TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("09:15")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Occurs every 2 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("01/09/2021")
                .InsertEndDate("03/09/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false);

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(1, recurringAppointments.Count);

            //User should have only 2 appointments created
            var appointments = dbHelper.appointment.GetByRecurringAppointmentId(recurringAppointments[0]);
            Assert.AreEqual(2, appointments.Count);

            //appointments should be spaced by 2 days each
            var startDate = (DateTime)dbHelper.appointment.GetAppointmentByID(appointments[0], "startdate")["startdate"];
            Assert.AreEqual(new DateTime(2021, 9, 3), startDate);

            startDate = (DateTime)dbHelper.appointment.GetAppointmentByID(appointments[1], "startdate")["startdate"];
            Assert.AreEqual(new DateTime(2021, 9, 1), startDate);


        }

        [TestProperty("JiraIssueID", "CDV6-12409")]
        [Description("Create a new recurring appointment with only the mandatory fields set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod09()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString())
                .TypeSearchQuery(_systemUsername2).TapSearchButton().AddElementToList(_systemUserId2.ToString()).TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("09:15")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("occurs every 1 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("01/09/2021")
                .InsertEndDate("03/09/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false);

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(1, recurringAppointments.Count);

            personRecurringAppointmentsPage
                .OpenPersonRecurringAppointmentRecord(recurringAppointments[0].ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("Recurring Appointment testing 001")
                .ValidateSubjectFieldText("Recurring Appointment testing 001")
                .ValidateRequiredRecordText(_systemUserId.ToString(), _systemUserFullName + "\r\nRemove")
                .ValidateRequiredRecordText(_systemUserId2.ToString(), _systemUserFullName2 + "\r\nRemove");

        }

        [TestProperty("JiraIssueID", "CDV6-12408")]
        [Description("Create a new recurring appointment with only the mandatory fields set")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void PersonRecurringAppointment_UITestMethod10()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUsername, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personId.ToString())
                .OpenPersonRecord(_personId.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToRecurringAppointmentsPage();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true)
                .ClickNewRecordButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertSubject("Recurring Appointment testing 001")
                .ClickRequiredLookupButton();

            lookupMultiSelectPopup
                .WaitForLookupMultiSelectPopupToLoad()
                .TypeSearchQuery(_systemUsername).TapSearchButton().AddElementToList(_systemUserId.ToString()).TapOKButton();

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartTime("")
                .InsertEndTime("")
                .InsertStartTime("09:00")
                .InsertEndTime("09:15")
                .ClickRecurrencePatternLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("occurs every 1 days").TapSearchButton().SelectResultElement(recurrencePatternId.ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("New")
                .InsertStartDate("01/09/2021")
                .InsertEndDate("03/09/2021")
                .SelectEndRange("By End Date")
                .ClickSaveAndCloseButton();

            personRecurringAppointmentsPage
                .WaitForPersonRecurringAppointmentsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false);

            var recurringAppointments = dbHelper.recurringAppointment.GetByRegardingID(_personId);
            Assert.AreEqual(1, recurringAppointments.Count);

            personRecurringAppointmentsPage
                .OpenPersonRecurringAppointmentRecord(recurringAppointments[0].ToString());

            personRecurringAppointmentRecordPage
                .WaitForPersonRecurringAppointmentRecordPageToLoad("Recurring Appointment testing 001")
                .ValidateSubjectFieldText("Recurring Appointment testing 001")
                .ValidateRequiredRecordText(_systemUserId.ToString(), _systemUserFullName + "\r\nRemove");

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

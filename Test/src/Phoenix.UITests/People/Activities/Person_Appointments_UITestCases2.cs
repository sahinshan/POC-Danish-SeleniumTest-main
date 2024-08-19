using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.People
{

    [TestClass]
    public class Person_Appointments_UITestCases2 : FunctionalTest
    {

        private Guid _businessUnitId;
        private Guid _languageId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _authenticationproviderid;
        private Guid _systemUserId;
        private String _systemUsername;


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

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("Appointments BU1");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("Appointments T1", null, _businessUnitId, "907678", "Appointments@careworkstempmail.com", "Appointments T1", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region System User AppointmentsUser1

                _systemUsername = "AppointmentsUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUsername, "Appointments", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion             
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/ACC-414

        [TestProperty("JiraIssueID", "CDV6-664")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [Description("To verify user is able to 'Reactivate' the Appointment Activity record upon clicking the 'Activate' icon in the toolbar, when the status is set to 'Cancelled' in the Appointment Activity which is associated with the Person record.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Appointment_Reactivation_UITestMethod01()
        {
            #region Person

            var _firstName = "Jeremy";
            var _lastName = DateTime.Now.ToString("LN_yyyyMMddHHmmss");
            var _personFullName = _firstName + " " + _lastName;
            var _personID = commonMethodsDB.CreatePersonRecord(_firstName, _lastName, _ethnicityId, _teamId);
            var _personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Appointment

            var status = 4; //Scheduled
            var showTimeAs = 5; //Busy
            var todaysDate = commonMethodsHelper.GetDateWithoutCulture(DateTime.Now.Date);
            var appointmentID = dbHelper.appointment.CreateAppointment(_teamId, _personID,
                null, null, null, null, null, _systemUserId, null, "Apt 01", "some notes ...", "loc 1",
                todaysDate, new TimeSpan(9, 0, 0), todaysDate, new TimeSpan(9, 15, 0), _personID, "person", _personFullName,
                status, showTimeAs, false, null, null, null);

            dbHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(appointmentID, _systemUserId, "systemuser", "Appointments User1");

            status = 2; //Completed
            dbHelper.appointment.UpdateStatus(appointmentID, status);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("AppointmentsUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(_personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .NavigateToAppointmentsPage();

            personAppointmentsPage
                .WaitForPersonAppointmentsPageToLoad()
                .OpenPersonAppointmentRecord(appointmentID);

            personAppointmentRecordPage
                .WaitForDisabledPersonAppointmentRecordPageToLoad("Apt 01")
                .ClickActivateButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("Apt 01")
                .InsertSubject("Appointment 001") //if the driver can insert text it means that the record is active
                .ValidateSelectedStatus("Scheduled");

            var fields = dbHelper.appointment.GetAppointmentByID(appointmentID, "inactive");
            Assert.AreEqual(false, fields["inactive"]);
        }

        #endregion


        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}

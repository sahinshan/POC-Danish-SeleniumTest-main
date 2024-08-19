using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Appointments
{
    /// <summary>
    /// This class contains all test methods for cases health appointments validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class AppointmentsPage_TabletModeTests : TestBase
    {
        static UIHelper uIHelper;


        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper = new PlatformServicesHelper("mobile_test_user_1", "Passw0rd_!");

            //start the APP
            uIHelper = new UIHelper();
            this._app = uIHelper.StartApp(this._apkFileLocation, this._deviceSerial, AppDataMode.DoNotClear);

            //set the default URL
            this.SetDefaultEndpointURL();

            //Login with test user account
            var changeUserButtonVisible = loginPage.WaitForBasicLoginPageToLoad().GetChangeUserButtonVisibility();
            if (changeUserButtonVisible)
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .TapChangeUserButton();

                warningPopup
                    .WaitForWarningPopupToLoad()
                    .TapOnYesButton();

                loginPage
                   .WaitForLoginPageToLoad()
                   .InsertUserName("Mobile_Test_User_1")
                   .InsertPassword("Passw0rd_!")
                   .TapLoginButton();

                //if the offline mode warning is displayed, then close it
                warningPopup.TapNoButtonIfPopupIsOpen();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }
            else
            {
                //Login with test user account
                loginPage
                    .WaitForBasicLoginPageToLoad()
                    .InsertUserName("Mobile_Test_User_1")
                    .InsertPassword("Passw0rd_!")
                    .TapLoginButton();

                //Set the PIN Code
                pinPage
                    .WaitForPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK()
                    .WaitForConfirmationPinPageToLoad()
                    .TapButton1()
                    .TapButton2()
                    .TapButton3()
                    .TapButton4()
                    .TapButtonOK();

                //wait for the homepage to load
                homePage
                    .WaitForHomePageToLoad();
            }

        }

        [SetUp]
        public void TestInitializationMethod()
        {

            if (this.IgnoreSetUp)
                return;

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //if a lookup is open then close it
            lookupPopup.ClosePopupIfOpen();

            //if the pick-list is open close it
            pickList.ClosePicklistIfOpen();



            //navigate to the Settings page
            mainMenu.NavigateToAppointmentsPage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //if a lookup is open then close it
            lookupPopup.ClosePopupIfOpen();

            //if the pick-list is open close it
            pickList.ClosePicklistIfOpen();
        }

        #region Appointments page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6395")]
        [Description("UI Test for the Appointments page - 001 - " +
            "Navigate to the Appointments Page (user has 1 activity appointment for today) - Validate that the activity appointment information is displayed - Tap on the toogle button - validate that the remaining information is displayed")]
        public void AppointmentsPage_TestMethod001()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 

            //remove all activity appointments
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            //remove all health appointments
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                //remove any Additional Health professional for the appointment
                foreach (Guid additionalRecord in PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);
                
                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            //create one activity appointment
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid activityOutcomeID = new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"); //More information needed
            Guid appointmentTypeID = new Guid("08759c8a-a4cc-e811-80dc-0050560502cc"); //Conference
            Guid activitycategoryid = new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"); //Advice
            Guid activityreasonid = new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"); //Assessment
            var basedate = DateTime.Now.Date;
            DateTime appointmentDate = new DateTime(basedate.Year, basedate.Month, basedate.Day);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);

            Guid appointmentID = PlatformServicesHelper.appointment.CreateAppointment("Appointment 001", mobileTeam1, responsibleuserid, activitycategoryid, null, activityOutcomeID, activityreasonid, null, personID, null, appointmentTypeID, personID, "Maria Tsatsouline", "person", "Notes ...", "location info ...", appointmentDate, startTime, appointmentDate, endTime);
            PlatformServicesHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(appointmentID, personID, "person", "Maria Tsatsouline");
            PlatformServicesHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(appointmentID, responsibleuserid, "systemuser", "Mobile Test User 1");


            string appointmentMonthAndDayInfo = appointmentDate.ToString("MMM d").ToUpper();
            string appointmentYearInfo = appointmentDate.ToString("yyyy").ToUpper();

            appointmentsPage
                .WaitForAppointmentsPageToLoad()
                .TapRefreshButton()
                .WaitForLoadSymbolToBeRemoved()
                
                .ValidateActivityAppointmentRecordMonthAndDateInfo(appointmentID.ToString(), appointmentMonthAndDayInfo)
                .ValidateActivityAppointmentRecordYearInfo(appointmentID.ToString(), appointmentYearInfo)
                
                .ValidateActivityAppointmentSubjectLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentStartTimeLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentEndTimeLabelVisibility(appointmentID.ToString(), false)
                .ValidateActivityAppointmentStartDateLabelVisibility(appointmentID.ToString(), false)
                .ValidateActivityAppointmentCategoryLabelVisibility(appointmentID.ToString(), false)
                .ValidateActivityAppointmentReasonLabelVisibility(appointmentID.ToString(), false)
                .ValidateActivityAppointmentLocationLabelVisibility(appointmentID.ToString(), false)
                .ValidateActivityAppointmentResponsibleTeamLabelVisibility(appointmentID.ToString(), false)
                .ValidateActivityAppointmentResponsibleUserLabelVisibility(appointmentID.ToString(), false)

                .ValidateActivityAppointmentSubjectValue(appointmentID.ToString(), "Appointment 001")
                .ValidateActivityAppointmentStartTimeValue(appointmentID.ToString(), "09:00")

                .TapOnActivityAppointmenToogleIcon(appointmentID.ToString())

                .ValidateActivityAppointmentSubjectLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentStartTimeLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentEndTimeLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentStartDateLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentCategoryLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentReasonLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentLocationLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentResponsibleTeamLabelVisibility(appointmentID.ToString(), true)
                .ValidateActivityAppointmentResponsibleUserLabelVisibility(appointmentID.ToString(), true)

                .ValidateActivityAppointmentSubjectValue(appointmentID.ToString(), "Appointment 001")
                .ValidateActivityAppointmentStartTimeValue(appointmentID.ToString(), "09:00")
                .ValidateActivityAppointmentEndTimeValue(appointmentID.ToString(), "09:05")
                .ValidateActivityAppointmentStartDateValue(appointmentID.ToString(), "" + appointmentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateActivityAppointmentCategoryValue(appointmentID.ToString(), "Advice")
                .ValidateActivityAppointmentReasonValue(appointmentID.ToString(), "Assessment")
                .ValidateActivityAppointmentLocationValue(appointmentID.ToString(), "location info ...")
                .ValidateActivityAppointmentResponsibleTeamValue(appointmentID.ToString(), "Mobile Team 1")
                .ValidateActivityAppointmentResponsibleUserValue(appointmentID.ToString(), "Mobile Test User 1");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6396")]
        [Description("UI Test for the Appointments page - 002 - " +
            "Navigate to the Appointments Page (user has 1 activity appointment for today) - Tap on the appointment record - Validate that the user is redirected to the appointments page")]
        public void AppointmentsPage_TestMethod002()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 

            //remove all activity appointments
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            //remove all health appointments
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                //remove any Additional Health professional for the appointment
                foreach (Guid additionalRecord in PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            //create one activity appointment
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid activityOutcomeID = new Guid("a9000a29-9e45-e911-a2c5-005056926fe4"); //More information needed
            Guid appointmentTypeID = new Guid("08759c8a-a4cc-e811-80dc-0050560502cc"); //Conference
            Guid activitycategoryid = new Guid("79a81b8a-9d45-e911-a2c5-005056926fe4"); //Advice
            Guid activityreasonid = new Guid("3e9831f8-5f75-e911-a2c5-005056926fe4"); //Assessment
            DateTime appointmentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);

            Guid appointmentID = PlatformServicesHelper.appointment.CreateAppointment("Appointment 001", mobileTeam1, responsibleuserid, activitycategoryid, null, activityOutcomeID, activityreasonid, null, personID, null, appointmentTypeID, personID, "Maria Tsatsouline", "person", "Notes ...", "location info ...", appointmentDate, startTime, appointmentDate, endTime);
            PlatformServicesHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(appointmentID, personID, "person", "Maria Tsatsouline");
            PlatformServicesHelper.appointmentRequiredAttendee.CreateAppointmentRequiredAttendee(appointmentID, responsibleuserid, "systemuser", "Mobile Test User 1");

            appointmentsPage
                .WaitForAppointmentsPageToLoad()
                .TapRefreshButton()
                .WaitForLoadSymbolToBeRemoved()
                .TapOnActivityAppointmentRecord(appointmentID.ToString());

            personAppointmentRecordPage
                .WaitForPersonAppointmentRecordPageToLoad("APPOINTMENT: Appointment 001");
        }


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6397")]
        [Description("UI Test for the Appointments page - 003 - " +
            "Navigate to the Appointments Page (user has 1 health appointment for today) - Validate that the health appointment information is displayed - Tap on the toogle button - validate that the remaining information is displayed")]
        public void AppointmentsPage_TestMethod003()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 

            //remove all activity appointments
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            //remove all health appointments
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                //remove any Additional Health professional for the appointment
                foreach (Guid additionalRecord in PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }


            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);
            bool cancelappointment = false;
            Guid? wholedtheappointmentid = null;
            Guid? healthappointmentoutcometypeid = null;
            int? cancellationreasontypeid = null;
            int? nonattendancetypeid = null;
            Guid? WhoCancelledTheAppointmentId = null;
            string WhoCancelledTheAppointmentIdName = "";
            string WhoCancelledTheAppointmentIdTableName = "";
            string whocancelledtheappointmentfreetext = "";
            DateTime? dateunavailablefrom = null;
            DateTime? dateavailablefrom = null;
            Guid? healthappointmentabsencereasonid = null;
            DateTime? cnanotificationdate = null;
            bool additionalprofessionalrequired = false;
            bool addtraveltimetoappointment = false;
            bool returntobaseafterappointment = false;

            Guid healthAppointmentID = this.PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            string appointmentMonthAndDayInfo = appointmentStartDate.ToString("MMM d").ToUpper();
            string appointmentYearInfo = appointmentStartDate.ToString("yyyy").ToUpper();

            appointmentsPage
                .WaitForAppointmentsPageToLoad()
                .TapRefreshButton()
                .WaitForLoadSymbolToBeRemoved()

                .ValidateHealthAppointmentRecordMonthAndDateInfo(healthAppointmentID.ToString(), appointmentMonthAndDayInfo)
                .ValidateHealthAppointmentRecordYearInfo(healthAppointmentID.ToString(), appointmentYearInfo)

                .ValidateHealthAppointmentPersonLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentStartTimeLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentEndTimeLabelVisibility(healthAppointmentID.ToString(), false)
                .ValidateHealthAppointmentStartDateLabelVisibility(healthAppointmentID.ToString(), false)
                .ValidateHealthAppointmentFirstNameLabelVisibility(healthAppointmentID.ToString(), false)
                .ValidateHealthAppointmentlastNameLabelVisibility(healthAppointmentID.ToString(), false)
                .ValidateHealthAppointmentLocationTypeLabelVisibility(healthAppointmentID.ToString(), false)
                .ValidateHealthAppointmentLocationLabelVisibility(healthAppointmentID.ToString(), false)
                .ValidateHealthAppointmentReasonLabelVisibility(healthAppointmentID.ToString(), false)
                .ValidateHealthAppointmentAdditionalProfessionalRequiredLabelVisibility(healthAppointmentID.ToString(), false)
                .ValidateHealthAppointmentAppointmentReasonLabelVisibility(healthAppointmentID.ToString(), false)

                .ValidateHealthAppointmentPersonValue(healthAppointmentID.ToString(), "Maria Tsatsouline")
                .ValidateHealthAppointmentStartTimeValue(healthAppointmentID.ToString(), "10:00")

                .TapOnHealthAppointmentToogleIcon(healthAppointmentID.ToString())

                .ValidateHealthAppointmentPersonLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentStartTimeLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentEndTimeLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentStartDateLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentFirstNameLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentlastNameLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentLocationTypeLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentLocationLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentReasonLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentAdditionalProfessionalRequiredLabelVisibility(healthAppointmentID.ToString(), true)
                .ValidateHealthAppointmentAppointmentReasonLabelVisibility(healthAppointmentID.ToString(), true)

                .ValidateHealthAppointmentPersonValue(healthAppointmentID.ToString(), "Maria Tsatsouline")
                .ValidateHealthAppointmentStartTimeValue(healthAppointmentID.ToString(), "10:00")
                .ValidateHealthAppointmentEndTimeValue(healthAppointmentID.ToString(), "10:05")
                .ValidateHealthAppointmentStartDateValue(healthAppointmentID.ToString(), "" + appointmentStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateHealthAppointmentFirstNameValue(healthAppointmentID.ToString(), "Maria")
                .ValidateHealthAppointmentlastNameValue(healthAppointmentID.ToString(), "Tsatsouline")
                .ValidateHealthAppointmentLocationTypeValue(healthAppointmentID.ToString(), "Clients or patients home")
                .ValidateHealthAppointmentLocationValue(healthAppointmentID.ToString(), "")
                .ValidateHealthAppointmentReasonValue(healthAppointmentID.ToString(), "")
                .ValidateHealthAppointmentAdditionalProfessionalRequiredValue(healthAppointmentID.ToString(), "No")
                .ValidateHealthAppointmentAppointmentReasonValue(healthAppointmentID.ToString(), "Assessment");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6398")]
        [Description("UI Test for the Appointments page - 004 - " +
            "Navigate to the Appointments Page (user has 1 health appointment for today) - Tap on the appointment record - Validate that the user is redirected to the health appointments page")]
        public void AppointmentsPage_TestMethod004()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 

            //remove all activity appointments
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            //remove all health appointments
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                //remove any Additional Health professional for the appointment
                foreach (Guid additionalRecord in PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }


            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(10, 5, 0);
            bool cancelappointment = false;
            Guid? wholedtheappointmentid = null;
            Guid? healthappointmentoutcometypeid = null;
            int? cancellationreasontypeid = null;
            int? nonattendancetypeid = null;
            Guid? WhoCancelledTheAppointmentId = null;
            string WhoCancelledTheAppointmentIdName = "";
            string WhoCancelledTheAppointmentIdTableName = "";
            string whocancelledtheappointmentfreetext = "";
            DateTime? dateunavailablefrom = null;
            DateTime? dateavailablefrom = null;
            Guid? healthappointmentabsencereasonid = null;
            DateTime? cnanotificationdate = null;
            bool additionalprofessionalrequired = false;
            bool addtraveltimetoappointment = false;
            bool returntobaseafterappointment = false;

            Guid healthAppointmentID = this.PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            appointmentsPage
                .WaitForAppointmentsPageToLoad()
                .TapRefreshButton()
                .WaitForLoadSymbolToBeRemoved()
                .TapOnHealthAppointmentRecord(healthAppointmentID.ToString());


            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home");
        }


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6399")]
        [Description("UI Test for the Appointments page - 005 - " +
            "Navigate to the Appointments Page (user has 1 additional health professional appointment for today) - Validate that the additional health professional appointment information is displayed - Tap on the toogle button - validate that the remaining information is displayed")]
        public void AppointmentsPage_TestMethod005()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 

            //remove all activity appointments
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            //remove all health appointments
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                //remove any Additional Health professional for the appointment
                foreach (Guid additionalRecord in PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }


            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4");  //mobile_test_user_2
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4");//mobile_test_user_2
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(11, 0, 0);
            TimeSpan endTime = new TimeSpan(11, 30, 0);
            bool cancelappointment = false;
            Guid? wholedtheappointmentid = null;
            Guid? healthappointmentoutcometypeid = null;
            int? cancellationreasontypeid = null;
            int? nonattendancetypeid = null;
            Guid? WhoCancelledTheAppointmentId = null;
            string WhoCancelledTheAppointmentIdName = "";
            string WhoCancelledTheAppointmentIdTableName = "";
            string whocancelledtheappointmentfreetext = "";
            DateTime? dateunavailablefrom = null;
            DateTime? dateavailablefrom = null;
            Guid? healthappointmentabsencereasonid = null;
            DateTime? cnanotificationdate = null;
            bool additionalprofessionalrequired = true;
            bool addtraveltimetoappointment = false;
            bool returntobaseafterappointment = false;

            Guid healthAppointmentID = PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);


            Guid healthprofessionalid2 = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile Test User 1

            Guid additionalProfessionalRecord = PlatformServicesHelper.healthAppointmentAdditionalProfessional.CreateHealthAppointmentAdditionalProfessional(
                ownerid, healthAppointmentID, healthprofessionalid2, personid, caseID, appointmentStartDate, startTime, appointmentStartDate, endTime, true, false, false);


            string appointmentMonthAndDayInfo = appointmentStartDate.ToString("MMM d").ToUpper();
            string appointmentYearInfo = appointmentStartDate.ToString("yyyy").ToUpper();

            appointmentsPage
                .WaitForAppointmentsPageToLoad()
                .TapRefreshButton()
                .WaitForLoadSymbolToBeRemoved()

                .ValidateAdditionalHealthProfessionalRecordMonthAndDateInfo(additionalProfessionalRecord.ToString(), appointmentMonthAndDayInfo)
                .ValidateAdditionalHealthProfessionalRecordYearInfo(additionalProfessionalRecord.ToString(), appointmentYearInfo)

                .ValidateAdditionalHealthProfessionalPersonLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalStartTimeLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalEndTimeLabelVisibility(additionalProfessionalRecord.ToString(), false)
                .ValidateAdditionalHealthProfessionalStartDateLabelVisibility(additionalProfessionalRecord.ToString(), false)
                .ValidateAdditionalHealthProfessionalEndDateLabelVisibility(additionalProfessionalRecord.ToString(), false)
                .ValidateAdditionalHealthProfessionalCommunityClinicAppointmentLabelVisibility(additionalProfessionalRecord.ToString(), false)
                .ValidateAdditionalHealthProfessionalResponsibleTeamLabelVisibility(additionalProfessionalRecord.ToString(), false)
                .ValidateAdditionalHealthProfessionalLeadProfessionalLabelVisibility(additionalProfessionalRecord.ToString(), false)

                .ValidateAdditionalHealthProfessionalPersonValue(additionalProfessionalRecord.ToString(), "Maria Tsatsouline")
                .ValidateAdditionalHealthProfessionalStartTimeValue(additionalProfessionalRecord.ToString(), "11:00")

                .TapOnAdditionalHealthProfessionalRecordToogleIcon(additionalProfessionalRecord.ToString())

                .ValidateAdditionalHealthProfessionalPersonLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalStartTimeLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalEndTimeLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalStartDateLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalEndDateLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalCommunityClinicAppointmentLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalResponsibleTeamLabelVisibility(additionalProfessionalRecord.ToString(), true)
                .ValidateAdditionalHealthProfessionalLeadProfessionalLabelVisibility(additionalProfessionalRecord.ToString(), true)

                .ValidateAdditionalHealthProfessionalPersonValue(additionalProfessionalRecord.ToString(), "Maria Tsatsouline")
                .ValidateAdditionalHealthProfessionalStartTimeValue(additionalProfessionalRecord.ToString(), "11:00")
                .ValidateAdditionalHealthProfessionalEndTimeValue(additionalProfessionalRecord.ToString(), "11:30")
                .ValidateAdditionalHealthProfessionalStartDateValue(additionalProfessionalRecord.ToString(), "" + appointmentStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateAdditionalHealthProfessionalEndDateValue(additionalProfessionalRecord.ToString(), "" + appointmentStartDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateAdditionalHealthProfessionalCommunityClinicAppointmentValue(additionalProfessionalRecord.ToString(), "Maria Tsatsouline, Assessment, Clients or patients home")
                .ValidateAdditionalHealthProfessionalResponsibleTeamValue(additionalProfessionalRecord.ToString(), "Mobile Team 1")
                .ValidateAdditionalHealthProfessionalLeadProfessionalValue(additionalProfessionalRecord.ToString(), "Mobile Test User 2");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6400")]
        [Description("UI Test for the Appointments page - 006 - " +
            "Navigate to the Appointments Page (user has 1 additional health professional appointment for today) - Tap on the appointment record - Validate that the user is redirected to the additional health professional appointments page")]
        public void AppointmentsPage_TestMethod006()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Maria Tsatsouline 

            //remove all activity appointments
            foreach (Guid recordID in this.PlatformServicesHelper.appointment.GetAppointmentByPersonID(personID))
            {
                foreach (var requiredID in PlatformServicesHelper.appointmentRequiredAttendee.GetAppointmentRequiredAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentRequiredAttendee.DeleteAppointmentRequiredAttendee(requiredID);

                foreach (var optional in PlatformServicesHelper.appointmentOptionalAttendee.GetAppointmentOptionalAttendeeByAppointmentID(recordID))
                    PlatformServicesHelper.appointmentOptionalAttendee.DeleteAppointmentOptionalAttendee(optional);

                PlatformServicesHelper.appointment.DeleteAppointment(recordID);
            }

            //remove all health appointments
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                //remove any Additional Health professional for the appointment
                foreach (Guid additionalRecord in PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }


            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4");  //mobile_test_user_2
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4");//mobile_test_user_2
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(11, 0, 0);
            TimeSpan endTime = new TimeSpan(11, 30, 0);
            bool cancelappointment = false;
            Guid? wholedtheappointmentid = null;
            Guid? healthappointmentoutcometypeid = null;
            int? cancellationreasontypeid = null;
            int? nonattendancetypeid = null;
            Guid? WhoCancelledTheAppointmentId = null;
            string WhoCancelledTheAppointmentIdName = "";
            string WhoCancelledTheAppointmentIdTableName = "";
            string whocancelledtheappointmentfreetext = "";
            DateTime? dateunavailablefrom = null;
            DateTime? dateavailablefrom = null;
            Guid? healthappointmentabsencereasonid = null;
            DateTime? cnanotificationdate = null;
            bool additionalprofessionalrequired = true;
            bool addtraveltimetoappointment = false;
            bool returntobaseafterappointment = false;

            Guid healthAppointmentID = PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);


            Guid healthprofessionalid2 = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile Test User 1

            Guid additionalProfessionalRecord = PlatformServicesHelper.healthAppointmentAdditionalProfessional.CreateHealthAppointmentAdditionalProfessional(
                ownerid, healthAppointmentID, healthprofessionalid2, personid, caseID, appointmentStartDate, startTime, appointmentStartDate, endTime, true, false, false);


            string appointmentMonthAndDayInfo = appointmentStartDate.ToString("MMM d").ToUpper();
            string appointmentYearInfo = appointmentStartDate.ToString("yyyy").ToUpper();

            appointmentsPage
                .WaitForAppointmentsPageToLoad()
                .TapRefreshButton()
                .WaitForLoadSymbolToBeRemoved()
                .TapOnAdditionalHealthProfessionalRecord(additionalProfessionalRecord.ToString());

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .ValidateCommunityClinicAppointmentFieldTitleVisible(true)
                .ValidateCaseFieldTitleVisible(true)
                .ValidateProfessionalRemainingForFullDurationFieldTitleVisible(true);

        }


        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}

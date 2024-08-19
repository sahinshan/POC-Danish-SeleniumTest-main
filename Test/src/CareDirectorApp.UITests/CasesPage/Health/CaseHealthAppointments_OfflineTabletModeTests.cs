using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Cases.Health
{
    /// <summary>
    /// This class contains all test methods for cases health appointments validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class CaseHealthAppointments_OfflineTabletModeTests : TestBase
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

            //close the lookup popup if it is open
            lookupPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //navigate to the settings page
            mainMenu.NavigateToSettingsPage();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();

            //if the APP is in offline mode change it to online mode
            settingsPage.SetTheAppInOnlineMode();
        }

        #region case Health Appointments page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6477")]
        [Description("UI Test for Health Appointments - 0002 - " +
            "Navigate to the cases Health Appointments area (case contains Health Appointment records) - Validate the page content")]
        public void CaseHealthAppointments_OfflineTestMethod002()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid appointmentID = new Guid("ade9ebb8-1db6-ea11-a2cd-005056926fe4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .ValidateResponsibleTeamCellText("Mobile Team 1", appointmentID.ToString())
                .ValidateStartTimeCellText("09:00", appointmentID.ToString())
                .ValidateEndTimeCellText("09:05", appointmentID.ToString())
                .ValidateStartDateCellText("01/06/2020", appointmentID.ToString())
                .ValidateFirstNameCellText("Pavel", appointmentID.ToString())
                .ValidateLastNameCellText("MCNamara", appointmentID.ToString())
                .ValidateRelatedCaseCellText("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]", appointmentID.ToString())
                .ValidateLocationTypeCellText("Clients or patients home", appointmentID.ToString())
                .ValidateLocationCellText("Bridgend - Adoption - Provider", appointmentID.ToString())
                .ValidateReasonCellText("", appointmentID.ToString())
                .ValidateAdditionalProfessionalRequeiredCellText("Yes", appointmentID.ToString())
                .ValidateAppointmentReasonCellText("Assessment", appointmentID.ToString())
                .ValidateOutcomeCellText("", appointmentID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6478")]
        [Description("UI Test for Health Appointments - 0004 - " +
            "Navigate to the cases Health Appointments area - Open a cases Health Appointment record - Validate that the Health Appointment record page fields and titles are displayed (appointment not outcomed)")]
        public void CaseHealthAppointments_OfflineTestMethod004()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string appointmentDate = "01/06/2020";


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();


            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .TapOnRecord(appointmentDate);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .ValidateAppointmentReasonFieldTitleVisible(true)
                .ValidateCommunityClinicTeamFieldTitleVisible(true)
                .ValidateRelatedCaseFieldTitleVisible(true)
                .ValidateContactTypeFieldTitleVisible(true)
                .ValidateTranslatorRequiredFieldTitleVisible(false)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidatePreferredLanguageFieldTitleVisible(false)

                .ValidateAppointmentInformationFieldTitleVisible(true)
                .ValidateAllowConcurrentAppointmentFieldTitleVisible(false)
                .ValidateBypassRestrictionsFieldTitleVisible(false)
                .ValidateOutOfHoursFieldTitleVisible(false)

                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateStartTimeFieldTitleVisible(true)
                .ValidateEndTimeFieldTitleVisible(true)

                .ValidateLocationTypeFieldTitleVisible(true)
                .ValidateAdditionalProfessionalRequiredFieldTitleVisible(true)
                .ValidateLeadProfessionalFieldTitleVisible(true)
                .ValidateClientsUsualPlaceOfResidenceFieldTitleVisible(true)

                .ValidateCancelAppointmentFieldTitleVisible(true)
                .ValidateOutcomeFieldTitleVisible(true)
                .ValidateWhoLedTheAppointmentFieldTitleVisible(true)
                .ValidateReasonFieldTitleVisible(false)
                .ValidateWhoCancelledTheAppointmentFieldTitleVisible(false)
                .ValidateDateUnavailableFromFieldTitleVisible(false)
                .ValidateAbsenceReasonFieldTitleVisible(false)
                .ValidateDNACNAWNBFieldTitleVisible(false)
                .ValidateWhoCancelledTheAppointmentFreeTextFieldTitleVisible(false)
                .ValidateDateAvailableFromFieldTitleVisible(false)
                .ValidateCNANotificationDateFieldTitleVisible(false)

                .ValidateAddTravelTimeToAppointmentFieldTitleVisible(false)
                .ValidateReturnToBaseAfterAppointmentFieldTitleVisible(false)
                .ValidateTravelTimeInMinutesFieldTitleVisible(false)
                .ValidateTravelTimeBackToBaseInMinutesFieldTitleVisible(false)


                .ValidateAppointmentReasonFieldText("Assessment")
                .ValidateCommunityClinicTeamFieldText("Mobile Test Clinic Team")
                .ValidateRelatedCaseFieldText("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .ValidateContactTypeLookupEntryText("Face To Face")
                .ValidateResponsibleReadonlyTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserLookupEntryText("Mobile Test User 1")
                .ValidatePersonReadonlyFieldText("Pavel MCNamara")

                .ValidateAppointmentInformationFieldText("Appointment Information ...")

                .ValidateStartDateFieldText("01/06/2020")
                .ValidateEndDateFieldText("01/06/2020")
                .ValidateStartTimeFieldText("09:00")
                .ValidateEndTimeFieldText("09:05")

                .ValidateLocationTypeFieldText("Clients or patients home")
                .ValidateAdditionalProfessionalRequiredFieldText("Yes")
                .ValidateLeadProfessionalFieldText("Mobile Test User 1")
                .ValidateClientsUsualPlaceOfResidenceFieldText("Yes")

                .ValidateCancelAppointmentFieldText("No")
                .ValidateOutcomeLookupEntryText("")
                .ValidateWhoLedTheAppointmentLookupEntryText("");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6479")]
        [Description("UI Test for Health Appointments - 0004 - " +
            "Navigate to the cases Health Appointments area - Open a cases Health Appointment record - " +
            "Validate that the Health Appointment record page field titles are displayed (cancel appointment set to yes)")]
        public void CaseHealthAppointments_OfflineTestMethod004_1()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string appointmentDate = "01/06/2020";


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Pavel MCNamara", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .TapOnRecord(appointmentDate);

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .TapCancelAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .TapReasonField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .TapDNACNAWNBField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .ValidateAppointmentReasonFieldTitleVisible(true)
                .ValidateCommunityClinicTeamFieldTitleVisible(true)
                .ValidateRelatedCaseFieldTitleVisible(true)
                .ValidateContactTypeFieldTitleVisible(true)
                .ValidateTranslatorRequiredFieldTitleVisible(false)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidatePreferredLanguageFieldTitleVisible(false)

                .ValidateAppointmentInformationFieldTitleVisible(true)
                .ValidateAllowConcurrentAppointmentFieldTitleVisible(false)
                .ValidateBypassRestrictionsFieldTitleVisible(false)
                .ValidateOutOfHoursFieldTitleVisible(false)

                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateStartTimeFieldTitleVisible(true)
                .ValidateEndTimeFieldTitleVisible(true)

                .ValidateLocationTypeFieldTitleVisible(true)
                .ValidateAdditionalProfessionalRequiredFieldTitleVisible(true)
                .ValidateLeadProfessionalFieldTitleVisible(true)
                .ValidateClientsUsualPlaceOfResidenceFieldTitleVisible(true)

                .ValidateCancelAppointmentFieldTitleVisible(true)
                .ValidateWhoLedTheAppointmentFieldTitleVisible(false)
                .ValidateReasonFieldTitleVisible(true)
                .ValidateWhoCancelledTheAppointmentFieldTitleVisible(true)
                .ValidateDateUnavailableFromFieldTitleVisible(true)
                .ValidateAbsenceReasonFieldTitleVisible(true)
                .ValidateDNACNAWNBFieldTitleVisible(true)
                .ValidateWhoCancelledTheAppointmentFreeTextFieldTitleVisible(true)
                .ValidateDateAvailableFromFieldTitleVisible(true)
                .ValidateCNANotificationDateFieldTitleVisible(true)

                .ValidateAddTravelTimeToAppointmentFieldTitleVisible(false)
                .ValidateReturnToBaseAfterAppointmentFieldTitleVisible(false)
                .ValidateTravelTimeInMinutesFieldTitleVisible(false)
                .ValidateTravelTimeBackToBaseInMinutesFieldTitleVisible(false);
        }

        #endregion

        #region Create New Record


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6480")]
        [Description("UI Test for Health Appointments - 0010 - " +
            "Navigate to the cases Health Appointments area - Tap on the add button - Validate that the user is prevented from creating a new record in offline mode")]
        public void CaseHealthAppointments_OfflineTestMethod010()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            string appointmentDate = DateTime.Now.ToString("dd/MM/yyyy");

            //remove any Health Appointment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (Guid additionalRecordID in this.PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    this.PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecordID);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnAddNewRecordButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Connectivity", "This device is currently offline. An appointment cannot be added while offline.").TapOnOKButton();

        }



        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6481")]
        [Description("UI Test for Health Appointments - 0022 - Create a new cases Health Appointment using the main APP web services" +
            "Navigate to the cases Health Appointments area - open the Health Appointment record - Outcome Appointment (Cancel Appointment = No) - " +
            "Validate that the record is correctly saved and synced")]
        public void CaseHealthAppointments_OfflineTestMethod022()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Ms Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);
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


            //remove any Health Appointment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (Guid additionalRecordID in this.PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    this.PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecordID);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            Guid healthAppointmentID = this.PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Attended - Follow up required");

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapOnSaveAndCloseButton();

            caseHealthAppointmentsPage
              .WaitForCaseHealthAppointmentsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            healthappointmentoutcometypeid = new Guid("bb5ef45b-b1cb-e811-80dc-0050560502cc"); //Attended - Follow up required

            var fields = this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID, 
                "cancelappointment", "wholedtheappointmentid", "healthappointmentoutcometypeid", "cancellationreasontypeid", "nonattendancetypeid", "WhoCancelledTheAppointmentId", 
                "WhoCancelledTheAppointmentIdName", "WhoCancelledTheAppointmentIdTableName", "whocancelledtheappointmentfreetext", "dateunavailablefrom", "dateavailablefrom", "healthappointmentabsencereasonid", "cnanotificationdate");
            
            Assert.AreEqual(false, (bool)fields["cancelappointment"]);
            Assert.AreEqual(responsibleuserid, (Guid)fields["wholedtheappointmentid"]);
            Assert.AreEqual(healthappointmentoutcometypeid, (Guid)fields["healthappointmentoutcometypeid"]);

            Assert.IsFalse(fields.ContainsKey("cancellationreasontypeid"));
            Assert.IsFalse(fields.ContainsKey("nonattendancetypeid"));
            Assert.IsFalse(fields.ContainsKey("WhoCancelledTheAppointmentId"));
            Assert.IsFalse(fields.ContainsKey("WhoCancelledTheAppointmentIdName"));
            Assert.IsFalse(fields.ContainsKey("WhoCancelledTheAppointmentIdTableName"));
            Assert.IsFalse(fields.ContainsKey("whocancelledtheappointmentfreetext"));
            Assert.IsFalse(fields.ContainsKey("dateunavailablefrom"));
            Assert.IsFalse(fields.ContainsKey("dateavailablefrom"));
            Assert.IsFalse(fields.ContainsKey("healthappointmentabsencereasonid"));
            Assert.IsFalse(fields.ContainsKey("cnanotificationdate"));

            ;
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6482")]
        [Description("UI Test for Health Appointments - 0023 - Create a new cases Health Appointment using the main APP web services" +
            "Navigate to the cases Health Appointments area - open the Health Appointment record - " +
            "Outcome Appointment (Cancel Appointment = Yes, Reason = Not Attended, DNA/CNA/WNB = Could Not Attend, Absence Reason = 'Medical Condition', set Date fields, set Who Cancel the Appointment fields)" +
            "Tap the Save button - Validate that the record is correctly saved and synced")]
        public void CaseHealthAppointments_OfflineTestMethod023()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);
            bool cancelappointment = false;
            Guid? wholedtheappointmentid = null;
            Guid? healthappointmentoutcometypeid = null;
            int? cancellationreasontypeid = null;
            int? nonattendancetypeid = null;
            Guid? WhoCancelledTheAppointmentId = null;
            string WhoCancelledTheAppointmentIdName = "";
            string WhoCancelledTheAppointmentIdTableName = "";
            string whocancelledtheappointmentfreetext = "";
           DateTime? dateunavailablefrom=null ;
           DateTime? dateavailablefrom=null;
            Guid? healthappointmentabsencereasonid = null;
            DateTime? cnanotificationdate = null;
            bool additionalprofessionalrequired = false;
            bool addtraveltimetoappointment = false;
            bool returntobaseafterappointment = false;


            //remove any Health Appointment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (Guid additionalRecordID in this.PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    this.PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecordID);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            Guid healthAppointmentID = this.PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

             dateunavailablefrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
         
            String dateunavailablefromtxt = dateunavailablefrom.Value.ToString("dd'/'MM'/'yyyy");
             dateavailablefrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
            String dateavailablefromtxt = dateavailablefrom.Value.ToString("dd'/'MM'/'yyyy");
            cnanotificationdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-1);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapCancelAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapReasonField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapDNACNAWNBField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapAbsenceReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Medical Condition");

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapWhoCancelledTheAppointment_LookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Mobile Test User 1");

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .InsertWhoCancelledTheAppointmentFreeText("who canceled ...")
                .InsertDateUnavailableFrom(dateunavailablefrom.Value.ToString("dd'/'MM'/'yyyy"))
                .InsertDateAvailableFrom(dateavailablefrom.Value.ToString("dd'/'MM'/'yyyy"))
                .InsertCNANotificationDate(cnanotificationdate.Value.ToString("dd'/'MM'/'yyyy"))
                .TapOnSaveAndCloseButton();

            caseHealthAppointmentsPage
              .WaitForCaseHealthAppointmentsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            cancellationreasontypeid = 2; //Not Attended
            nonattendancetypeid = 2; //Could Not Attend
            WhoCancelledTheAppointmentId = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1;
            WhoCancelledTheAppointmentIdName = "Mobile Test User 1";
            WhoCancelledTheAppointmentIdTableName = "systemuser";
            whocancelledtheappointmentfreetext = "who canceled ...";
            healthappointmentabsencereasonid = new Guid("18d0363a-00cb-e811-80dc-0050560502cc"); //Medical Condition

            var fields = this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID,
                "cancelappointment", "wholedtheappointmentid", "healthappointmentoutcometypeid", "cancellationreasontypeid", "nonattendancetypeid", "WhoCancelledTheAppointmentId",
                "WhoCancelledTheAppointmentIdName", "WhoCancelledTheAppointmentIdTableName", "whocancelledtheappointmentfreetext", "dateunavailablefrom", "dateavailablefrom", "healthappointmentabsencereasonid", "cnanotificationdate");

            var DateUnavailable = PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID, "dateunavailablefrom");
            var DateAvailable = PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID, "dateavailablefrom");

            // internal string aTime_UTC = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local).ToString("hh:mm");

            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();
            var dateConverted1 = usersettings.ConvertTimeFromUtc((DateTime)DateUnavailable["dateunavailablefrom"]);
            var dateConverted2 = usersettings.ConvertTimeFromUtc((DateTime)DateAvailable["dateavailablefrom"]);

            string DateUnAvailableFromValue = dateConverted1.Value.ToString("dd'/'MM'/'yyyy");
            string DateAvailableFromValue = dateConverted2.Value.ToString("dd'/'MM'/'yyyy");


            Assert.AreEqual(true, (bool)fields["cancelappointment"]);
            Assert.IsFalse(fields.ContainsKey("wholedtheappointmentid"));
            Assert.IsFalse(fields.ContainsKey("healthappointmentoutcometypeid"));

            Assert.AreEqual(cancellationreasontypeid, (int)fields["cancellationreasontypeid"]);
            Assert.AreEqual(nonattendancetypeid, (int)fields["nonattendancetypeid"]);
            Assert.AreEqual(WhoCancelledTheAppointmentId, (Guid)fields["whocancelledtheappointmentid"]);
            Assert.AreEqual(WhoCancelledTheAppointmentIdName, (string)fields["whocancelledtheappointmentidname"]);
            Assert.AreEqual(WhoCancelledTheAppointmentIdTableName, (string)fields["whocancelledtheappointmentidtablename"]);
            Assert.AreEqual(whocancelledtheappointmentfreetext, (string)fields["whocancelledtheappointmentfreetext"]);
            Assert.AreEqual(dateunavailablefromtxt, DateUnAvailableFromValue);
            Assert.AreEqual(dateavailablefromtxt, DateAvailableFromValue);
            Assert.AreEqual(healthappointmentabsencereasonid, (Guid)fields["healthappointmentabsencereasonid"]);
            Assert.AreEqual(cnanotificationdate, (DateTime)fields["cnanotificationdate"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6483")]
        [Description("UI Test for Health Appointments - 0024 - Create a new cases Health Appointment using the main APP web services" +
            "Navigate to the cases Health Appointments area - open the Health Appointment record - " +
            "Outcome Appointment (Cancel Appointment = Yes, Reason = Not Attended, DNA/CNA/WNB = Did Not Attend, Absence Reason = 'Forgot the appointment was due', set Who Cancel the Appointment fields) - Validate that the date fields are not displayed" +
            "Tap the Save button - Validate that the record is correctly saved and synced")]
        public void CaseHealthAppointments_OfflineTestMethod024()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);
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


            //remove any Health Appointment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            Guid healthAppointmentID = this.PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapCancelAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapReasonField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapDNACNAWNBField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapAbsenceReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Forgot the appointment was due");

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapWhoCancelledTheAppointment_LookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Mobile Test User 1");

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .InsertWhoCancelledTheAppointmentFreeText("who canceled ...")
                .ValidateDateUnavailableFromFieldVisible(false)
                .ValidateDateAvailableFromFieldVisible(false)
                .ValidateCNANotificationDateFieldVisible(false)
                .TapOnSaveAndCloseButton();

            caseHealthAppointmentsPage
              .WaitForCaseHealthAppointmentsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            cancellationreasontypeid = 2; //Not Attended
            nonattendancetypeid = 1; //Did Not Attend
            WhoCancelledTheAppointmentId = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1;
            WhoCancelledTheAppointmentIdName = "Mobile Test User 1";
            WhoCancelledTheAppointmentIdTableName = "systemuser";
            whocancelledtheappointmentfreetext = "who canceled ...";
            healthappointmentabsencereasonid = new Guid("f94624e9-01cb-e811-80dc-0050560502cc"); //Forgot the appointment was due

            var fields = this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID,
                "cancelappointment", "wholedtheappointmentid", "healthappointmentoutcometypeid", "cancellationreasontypeid", "nonattendancetypeid", "WhoCancelledTheAppointmentId",
                "WhoCancelledTheAppointmentIdName", "WhoCancelledTheAppointmentIdTableName", "whocancelledtheappointmentfreetext", "dateunavailablefrom", "dateavailablefrom", "healthappointmentabsencereasonid", "cnanotificationdate");

            Assert.AreEqual(true, (bool)fields["cancelappointment"]);
            Assert.IsFalse(fields.ContainsKey("wholedtheappointmentid"));
            Assert.IsFalse(fields.ContainsKey("healthappointmentoutcometypeid"));

            Assert.AreEqual(cancellationreasontypeid, (int)fields["cancellationreasontypeid"]);
            Assert.AreEqual(nonattendancetypeid, (int)fields["nonattendancetypeid"]);
            Assert.AreEqual(WhoCancelledTheAppointmentId, (Guid)fields["whocancelledtheappointmentid"]);
            Assert.AreEqual(WhoCancelledTheAppointmentIdName, (string)fields["whocancelledtheappointmentidname"]);
            Assert.AreEqual(WhoCancelledTheAppointmentIdTableName, (string)fields["whocancelledtheappointmentidtablename"]);
            Assert.AreEqual(whocancelledtheappointmentfreetext, (string)fields["whocancelledtheappointmentfreetext"]);
            Assert.IsFalse(fields.ContainsKey("dateunavailablefrom"));
            Assert.IsFalse(fields.ContainsKey("dateavailablefrom"));
            Assert.AreEqual(healthappointmentabsencereasonid, (Guid)fields["healthappointmentabsencereasonid"]);
            Assert.IsFalse(fields.ContainsKey("cnanotificationdate"));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6484")]
        [Description("UI Test for Health Appointments - 0025 - Create a new cases Health Appointment using the main APP web services" +
            "Navigate to the cases Health Appointments area - open the Health Appointment record - " +
            "Outcome Appointment (Cancel Appointment = Yes, Reason = Not Attended, DNA/CNA/WNB = Was Not Brought, Absence Reason = 'Forgot the appointment was due', set Date fields, set Who Cancel the Appointment fields)" +
            "Tap the Save button - Validate that the record is correctly saved and synced")]
        public void CaseHealthAppointments_OfflineTestMethod025()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);
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


            //remove any Health Appointment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            Guid healthAppointmentID = this.PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            dateunavailablefrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            String dateunavailablefromtxt = dateunavailablefrom.Value.ToString("dd'/'MM'/'yyyy");
            dateavailablefrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
            String dateavailablefromtxt = dateavailablefrom.Value.ToString("dd'/'MM'/'yyyy");
            cnanotificationdate = DateTime.Now.Date.AddDays(-1);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapCancelAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapReasonField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapDNACNAWNBField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(3).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapAbsenceReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Forgot the appointment was due");

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapWhoCancelledTheAppointment_LookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Mobile Test User 1");

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .InsertWhoCancelledTheAppointmentFreeText("who canceled ...")
                .InsertDateUnavailableFrom(dateunavailablefrom.Value.ToString("dd'/'MM'/'yyyy"))
                .InsertDateAvailableFrom(dateavailablefrom.Value.ToString("dd'/'MM'/'yyyy"))
                .InsertCNANotificationDate(cnanotificationdate.Value.ToString("dd'/'MM'/'yyyy"))
                .TapOnSaveAndCloseButton();

            caseHealthAppointmentsPage
              .WaitForCaseHealthAppointmentsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            cancellationreasontypeid = 2; //Not Attended
            nonattendancetypeid = 3; //Was Not Brought
            WhoCancelledTheAppointmentId = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1;
            WhoCancelledTheAppointmentIdName = "Mobile Test User 1";
            WhoCancelledTheAppointmentIdTableName = "systemuser";
            whocancelledtheappointmentfreetext = "who canceled ...";
            healthappointmentabsencereasonid = new Guid("f94624e9-01cb-e811-80dc-0050560502cc"); //Forgot the appointment was due

            var fields = this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID,
                "cancelappointment", "wholedtheappointmentid", "healthappointmentoutcometypeid", "cancellationreasontypeid", "nonattendancetypeid", "WhoCancelledTheAppointmentId",
                "WhoCancelledTheAppointmentIdName", "WhoCancelledTheAppointmentIdTableName", "whocancelledtheappointmentfreetext", "dateunavailablefrom", "dateavailablefrom", "healthappointmentabsencereasonid", "cnanotificationdate");

            var DateUnavailable = PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID, "dateunavailablefrom");
            var DateAvailable = PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID, "dateavailablefrom");

            // internal string aTime_UTC = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local).ToString("hh:mm");

            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();
            var dateConverted1 = usersettings.ConvertTimeFromUtc((DateTime)DateUnavailable["dateunavailablefrom"]);
            var dateConverted2 = usersettings.ConvertTimeFromUtc((DateTime)DateAvailable["dateavailablefrom"]);

            string DateUnAvailableFromValue = dateConverted1.Value.ToString("dd'/'MM'/'yyyy");
            string DateAvailableFromValue = dateConverted2.Value.ToString("dd'/'MM'/'yyyy");


            Assert.AreEqual(true, (bool)fields["cancelappointment"]);
            Assert.IsFalse(fields.ContainsKey("wholedtheappointmentid"));
            Assert.IsFalse(fields.ContainsKey("healthappointmentoutcometypeid"));

            Assert.AreEqual(cancellationreasontypeid, (int)fields["cancellationreasontypeid"]);
            Assert.AreEqual(nonattendancetypeid, (int)fields["nonattendancetypeid"]);
            Assert.AreEqual(WhoCancelledTheAppointmentId, (Guid)fields["whocancelledtheappointmentid"]);
            Assert.AreEqual(WhoCancelledTheAppointmentIdName, (string)fields["whocancelledtheappointmentidname"]);
            Assert.AreEqual(WhoCancelledTheAppointmentIdTableName, (string)fields["whocancelledtheappointmentidtablename"]);
            Assert.AreEqual(whocancelledtheappointmentfreetext, (string)fields["whocancelledtheappointmentfreetext"]);
            Assert.AreEqual(dateunavailablefromtxt, DateUnAvailableFromValue);
            Assert.AreEqual(dateavailablefromtxt, DateAvailableFromValue);
            Assert.AreEqual(healthappointmentabsencereasonid, (Guid)fields["healthappointmentabsencereasonid"]);
            Assert.AreEqual(cnanotificationdate, (DateTime)fields["cnanotificationdate"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6485")]
        [Description("UI Test for Health Appointments - 0026 - Create a new cases Health Appointment using the main APP web services" +
            "Navigate to the cases Health Appointments area - open the Health Appointment record - " +
            "Outcome Appointment (Cancel Appointment = Yes, Reason = Recorded in Error, set Who Cancel the Appointment fields - Validate that DNA/CNA/WNB, Absence Reason and Date fields are not displayed" +
            "Tap the Save button - Validate that the record is correctly saved and synced")]
        public void CaseHealthAppointments_OfflineTestMethod026()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid ownerid = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4");  //Mobile Team 1Mobile Team 1
            Guid personid = new Guid("0E62DA4B-4591-EA11-A2CD-005056926FE4");   //Maria Tsatsouline
            Guid dataformid = new Guid("904696C5-D8A4-E611-80D3-0050560502CC"); //Appointments
            Guid contacttypeid = new Guid("A295ABD4-A7CB-E811-80DC-0050560502CC");  //Face To Face
            Guid healthappointmentreasonid = new Guid("22C2DB0A-583A-E911-A2C5-005056926FE4");  //Assessment
            Guid responsibleuserid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1
            Guid communityandclinicteamid = new Guid("6140E2FD-4E9E-E911-A2C6-005056926FE4");   //Mobile Test Clinic Team
            Guid healthappointmentlocationtypeid = new Guid("8BA43FEC-AACB-E811-80DC-0050560502CC");    //Clients or patients home
            Guid healthprofessionalid = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");//mobile_test_user_1
            DateTime appointmentStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);
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


            //remove any Health Appointment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            Guid healthAppointmentID = this.PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapCancelAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapReasonField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapWhoCancelledTheAppointment_LookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Mobile Test User 1");

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .InsertWhoCancelledTheAppointmentFreeText("who canceled ...")
                .ValidateDateUnavailableFromFieldVisible(false)
                .ValidateDateAvailableFromFieldVisible(false)
                .ValidateCNANotificationDateFieldVisible(false)
                .ValidateAbsenceReasonElementsVisible(false)
                .ValidateDNACNAWNBElementsVisible(false)
                .TapOnSaveAndCloseButton();

            caseHealthAppointmentsPage
              .WaitForCaseHealthAppointmentsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            cancellationreasontypeid = 1; //Recorded in Error
            WhoCancelledTheAppointmentId = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4");  //mobile_test_user_1;
            WhoCancelledTheAppointmentIdName = "Mobile Test User 1";
            WhoCancelledTheAppointmentIdTableName = "systemuser";
            whocancelledtheappointmentfreetext = "who canceled ...";

            var fields = this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByID(healthAppointmentID,
                "cancelappointment", "wholedtheappointmentid", "healthappointmentoutcometypeid", "cancellationreasontypeid", "nonattendancetypeid", "WhoCancelledTheAppointmentId",
                "WhoCancelledTheAppointmentIdName", "WhoCancelledTheAppointmentIdTableName", "whocancelledtheappointmentfreetext", "dateunavailablefrom", "dateavailablefrom", "healthappointmentabsencereasonid", "cnanotificationdate");

            Assert.AreEqual(true, (bool)fields["cancelappointment"]);
            Assert.IsFalse(fields.ContainsKey("wholedtheappointmentid"));
            Assert.IsFalse(fields.ContainsKey("healthappointmentoutcometypeid"));

            Assert.AreEqual(cancellationreasontypeid, (int)fields["cancellationreasontypeid"]);
            Assert.IsFalse(fields.ContainsKey("nonattendancetypeid"));
            Assert.AreEqual(WhoCancelledTheAppointmentId, (Guid)fields["whocancelledtheappointmentid"]);
            Assert.AreEqual(WhoCancelledTheAppointmentIdName, (string)fields["whocancelledtheappointmentidname"]);
            Assert.AreEqual(WhoCancelledTheAppointmentIdTableName, (string)fields["whocancelledtheappointmentidtablename"]);
            Assert.AreEqual(whocancelledtheappointmentfreetext, (string)fields["whocancelledtheappointmentfreetext"]);
            Assert.IsFalse(fields.ContainsKey("dateunavailablefrom"));
            Assert.IsFalse(fields.ContainsKey("dateavailablefrom"));
            Assert.IsFalse(fields.ContainsKey("healthappointmentabsencereasonid"));
            Assert.IsFalse(fields.ContainsKey("cnanotificationdate"));

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6486")]
        [Description("UI Test for Health Appointments - 0026 - Create a new cases Health Appointment using the main APP web services" +
            "Navigate to the cases Health Appointments area - open the Health Appointment record - " +
            "Set Cancel Appointment = Yes, Reason = Not Attended, DNA/CNA/WNB = Could Not Attend, Absence Reason = Medical Condition " +
            "Change the Reason = Recorded In Error  - Validate that the DNA/CNA/WNB, Absence Reason and Date fields are not displayed after reseting the Reason field")]
        public void CaseHealthAppointments_OfflineTestMethod027()
        {
            Guid caseID = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
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
            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(9, 5, 0);
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


            //remove any Health Appointment for the case
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            Guid healthAppointmentID = this.PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);

            dateunavailablefrom = DateTime.Now.Date;
            dateavailablefrom = DateTime.Now.Date.AddDays(1);
            cnanotificationdate = DateTime.Now.Date.AddDays(-1);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToCasesPage();

            casesPage
                .WaitForCasesPageToLoad()
                .TapOnCaseRecordButton("Maria Tsatsouline", caseID.ToString());

            casePage
                .WaitForCasePageToLoad("Tsatsouline, Maria - (01/05/1960) [CAS-000004-6176]")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapHealthAppointmentsIcon_RelatedItems();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad()
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapCancelAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapReasonField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapDNACNAWNBField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapAbsenceReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Medical Condition");

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapReasonField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton(); //RESET THE REASON FIELD

            caseHealthAppointmentRecordPage
               .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
               .TapWhoCancelledTheAppointment_LookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Mobile Test User 1");

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .InsertWhoCancelledTheAppointmentFreeText("who canceled ...")
                .ValidateDateUnavailableFromFieldVisible(false)
                .ValidateDateAvailableFromFieldVisible(false)
                .ValidateCNANotificationDateFieldVisible(false)
                .ValidateAbsenceReasonElementsVisible(false)
                .ValidateDNACNAWNBElementsVisible(false);

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

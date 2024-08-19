using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Cases.Health
{
    /// <summary>
    /// This class contains all test methods for cases Additional Health Professionals validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class AdditionalHealthProfessionals_OfflineTabletModeTests : TestBase
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

        #region case Additional Health Professionals page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6459")]
        [Description("UI Test for Additional Health Professionals (Offline Mode) - 0002 - " +
            "Navigate to the cases Additional Health Professionals area (case contains Additional Health Professional records) - Validate the page content")]
        public void AdditionalHealthProfessionals_OfflineTestMethod002()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid additionalHealthProfessionalID = new Guid("d93edec2-1db6-ea11-a2cd-005056926fe4");
            
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
                .TapOnRecord("01/06/2020");

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .TapRelatedItemsButton()
                .TapRelatedItems_LeftMenuSubArea()
                .TapAdditionalHealthProfessionalsLink();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad()
                .ValidateHealthProfessionalCellText("Mobile Test User 2", additionalHealthProfessionalID.ToString())
                .ValidateStartDateCellText("01/06/2020", additionalHealthProfessionalID.ToString())
                .ValidateStartTimeCellText("09:00", additionalHealthProfessionalID.ToString())
                .ValidateEndDateCellText("01/06/2020", additionalHealthProfessionalID.ToString())
                .ValidateEndTimeCellText("09:05", additionalHealthProfessionalID.ToString())
                .ValidateProfessionalRemainingForFullDurationCellText("Yes", additionalHealthProfessionalID.ToString())
                .ValidateReturnToBaseAfterAppointmentCellText("Yes", additionalHealthProfessionalID.ToString())
                .ValidateTravelTimeInMinutesCellText("5", additionalHealthProfessionalID.ToString())
                .ValidateTravelTimeBackToBaseInMinutesCellText("10", additionalHealthProfessionalID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6460")]
        [Description("UI Test for Additional Health Professionals (Offline Mode) - 0004 - " +
            "Navigate to the cases Additional Health Professionals area - Open a cases Additional Health Professional record - Validate that the Additional Health Professional record page fields and titles are displayed ")]
        public void AdditionalHealthProfessionals_OfflineTestMethod004()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

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
                .TapOnRecord("01/06/2020");

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Mr Pavel MCNamara, Assessment, Clients or patients home")
                .TapRelatedItemsButton()
                .TapRelatedItems_LeftMenuSubArea()
                .TapAdditionalHealthProfessionalsLink();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad()
                .TapOnRecord("Mobile Test User 2");

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONAL: Mobile Test User 2 Added to Mr Pavel MCNamara, Assessment, Clients or patients home")
                .ValidateCommunityClinicAppointmentFieldTitleVisible(true)
                .ValidateCaseFieldTitleVisible(true)
                .ValidateProfessionalRemainingForFullDurationFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateAddTravelTimeToAppointmentFieldTitleVisible(true)
                .ValidateTravelTimeInMinutesFieldTitleVisible(true)
                .ValidateProfessionalFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateStartTimeFieldTitleVisible(true)
                .ValidateEndTimeFieldTitleVisible(true)
                .ValidateReturnToBaseAfterAppointmentFieldTitleVisible(true)
                .ValidateTravelTimeBackToBaseInMinutesFieldTitleVisible(true)

                .ValidateCommunityClinicAppointmentFieldText("Mr Pavel MCNamara, Assessment, Clients or patients home")
                .ValidateCaseFieldText("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                .ValidateProfessionalRemainingForFullDurationFieldText("Yes")
                .ValidateStartDateFieldText("01/06/2020")
                .ValidateEndDateFieldText("01/06/2020")
                .ValidateAddTravelTimeToAppointmentFieldText("Yes")
                .ValidateTravelTimeInMinutesFieldText("5")
                .ValidateProfessionalFieldText("Mobile Test User 2")
                .ValidatePersonFieldText("Pavel MCNamara")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateStartTimeFieldText("09:00")
                .ValidateEndTimeFieldText("09:05")
                .ValidateReturnToBaseAfterAppointmentFieldText("Yes")
                .ValidateTravelTimeBackToBaseInMinutesFieldText("10");
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6461")]
        [Description("UI Test for Additional Health Professionals (Offline Mode) - 0010 - " +
            "Navigate to the cases Additional Health Professionals area - Tap on the add button - Validate that the user is prevented from saving the record")]
        public void AdditionalHealthProfessionals_OfflineTestMethod010()
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
            TimeSpan endTime = new TimeSpan(9, 30, 0);
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


            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (Guid additionalRecord in PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

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
                .TapRelatedItemsButton()
                .TapRelatedItems_LeftMenuSubArea()
                .TapAdditionalHealthProfessionalsLink();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad()
                .TapOnAddNewRecordButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Connectivity", "This device is currently offline. An additional person cannot be added or updated while offline.").TapOnOKButton();
        }

        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6462")]
        [Description("UI Test for Additional Health Professionals (Offline Mode) - 0021 - Create a new Additional Health Professional using the main APP web services" +
            "Navigate to the cases Additional Health Professionals area - open the Additional Health Professional record - Edit any field and that on the save button - Validate that the user is prevented from saving the record in offline mode")]
        public void AdditionalHealthProfessionals_OfflineTestMethod021()
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
            TimeSpan endTime = new TimeSpan(9, 30, 0);
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


            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointment.GetHealthAppointmentByCaseID(caseID))
            {
                foreach (Guid additionalRecord in PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(recordID))
                    PlatformServicesHelper.healthAppointmentAdditionalProfessional.DeleteHealthAppointmentAdditionalProfessional(additionalRecord);

                foreach (var caseActionID in PlatformServicesHelper.caseAction.GetByHealthAppointmentID(recordID))
                    PlatformServicesHelper.caseAction.DeleteCaseAction(caseActionID);

                this.PlatformServicesHelper.healthAppointment.DeleteHealthAppointment(recordID);
            }

            Guid healthAppointmentID = PlatformServicesHelper.healthAppointment.CreateHealthAppointment(
                ownerid, personid, "Maria Tsatsouline", dataformid, contacttypeid, healthappointmentreasonid, "Assessment", caseID, responsibleuserid,
                communityandclinicteamid, healthappointmentlocationtypeid, "Clients or patients home", healthprofessionalid,
                "appointment information ...", appointmentStartDate, startTime, endTime, appointmentStartDate,
                cancelappointment, wholedtheappointmentid, healthappointmentoutcometypeid,
                cancellationreasontypeid, nonattendancetypeid, WhoCancelledTheAppointmentId, WhoCancelledTheAppointmentIdName, WhoCancelledTheAppointmentIdTableName, whocancelledtheappointmentfreetext, dateunavailablefrom, dateavailablefrom, healthappointmentabsencereasonid, cnanotificationdate,
                additionalprofessionalrequired, addtraveltimetoappointment, returntobaseafterappointment);


            Guid healthprofessionalid2 = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //Mobile Test User 2

            Guid additionalProfessionalRecord = PlatformServicesHelper.healthAppointmentAdditionalProfessional.CreateHealthAppointmentAdditionalProfessional(
                ownerid, healthAppointmentID, healthprofessionalid2, personid, caseID, appointmentStartDate, startTime, appointmentStartDate, endTime, true, false, false);

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
                .TapRelatedItemsButton()
                .TapRelatedItems_LeftMenuSubArea()
                .TapAdditionalHealthProfessionalsLink();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad()
                .TapOnRecord("Mobile Test User 2");

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .TapAddTravelTimeToAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .TapReturnToBaseAfterAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad()
                .InsertTravelTimeInMinutes("5")
                .InsertTravelTimeBackToBaseInMinutes("10")
                .TapOnSaveAndCloseButton();

            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "This device is currently offline. An additional person cannot be added or updated while offline.").TapOnOKButton();


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

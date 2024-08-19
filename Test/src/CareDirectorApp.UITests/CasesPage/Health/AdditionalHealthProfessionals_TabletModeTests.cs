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
    [Category("Mobile_TabletMode_Online")]
    public class AdditionalHealthProfessionals_TabletModeTests : TestBase
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
            mainMenu.NavigateToCasesPage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();
        }

        #region case Additional Health Professionals page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6463")]
        [Description("UI Test for Additional Health Professionals - 0001 - " +
            "Navigate to the cases Additional Health Professionals area (Additional Health Professional do not contains Additional Health Professional records) - Validate the page content")]
        public void AdditionalHealthProfessionals_TestMethod001()
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
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapRelatedItemsButton()
                .TapRelatedItems_LeftMenuSubArea()
                .TapAdditionalHealthProfessionalsLink();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6464")]
        [Description("UI Test for Additional Health Professionals - 0002 - " +
            "Navigate to the cases Additional Health Professionals area (case contains Additional Health Professional records) - Validate the page content")]
        public void AdditionalHealthProfessionals_TestMethod002()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid additionalHealthProfessionalID = new Guid("d93edec2-1db6-ea11-a2cd-005056926fe4");

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
        [Property("JiraIssueID", "CDV6-6465")]
        [Description("UI Test for Additional Health Professionals - 0003 - " +
            "Navigate to the cases Additional Health Professionals area - Open a cases Additional Health Professional record - Validate that the Additional Health Professional record page is displayed")]
        public void AdditionalHealthProfessionals_TestMethod003()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

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
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONAL: Mobile Test User 2 Added to Mr Pavel MCNamara, Assessment, Clients or patients home");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6466")]
        [Description("UI Test for Additional Health Professionals - 0004 - " +
            "Navigate to the cases Additional Health Professionals area - Open a cases Additional Health Professional record - Validate that the Additional Health Professional record page field titles are displayed ")]
        public void AdditionalHealthProfessionals_TestMethod004()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

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
                .ValidateTravelTimeBackToBaseInMinutesFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6467")]
        [Description("UI Test for Additional Health Professionals - 0005 - " +
            "Navigate to the cases Additional Health Professionals area - Open a cases Additional Health Professional record - Validate that the Additional Health Professional record page fields are correctly displayed")]
        public void AdditionalHealthProfessionals_TestMethod005()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

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

        #region New Record page - Validate content

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6468")]
        [Description("UI Test for Additional Health Professionals - 0007 - " +
            "Navigate to the cases Additional Health Professionals area - Tap on the add button - Validate that the new record page is displayed and all field titles are visible ")]
        public void AdditionalHealthProfessionals_TestMethod007()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

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
                .TapOnAddNewRecordButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .ValidateCommunityClinicAppointmentFieldTitleVisible(true)
                .ValidateCaseFieldTitleVisible(false)
                .ValidateProfessionalRemainingForFullDurationFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateAddTravelTimeToAppointmentFieldTitleVisible(true)
                .ValidateTravelTimeInMinutesFieldTitleVisible(false)
                .ValidateProfessionalFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(false)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateStartTimeFieldTitleVisible(true)
                .ValidateEndTimeFieldTitleVisible(true)
                .ValidateReturnToBaseAfterAppointmentFieldTitleVisible(true)
                .ValidateTravelTimeBackToBaseInMinutesFieldTitleVisible(false);
        }


        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6469")]
        [Description("UI Test for Additional Health Professionals - 0009 - " +
            "Navigate to the cases Additional Health Professionals area - Tap on the add button - Set data in all Mandatory fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void AdditionalHealthProfessionals_TestMethod009()
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
                .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapRelatedItemsButton()
                .TapRelatedItems_LeftMenuSubArea()
                .TapAdditionalHealthProfessionalsLink();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad()
                .TapOnAddNewRecordButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .TapProfessionalOpenLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Mobile Test User 2");

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .TapOnSaveAndCloseButton();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad();


            Guid additionalProfessionalRecordID = PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(healthAppointmentID)[0];
            var fields = PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByID(additionalProfessionalRecordID, "ownerid", "caseid", "healthappointmentid", "healthprofessionalid", "personid", "professionalremainingforfullduration", "startdate", "starttime", "enddate", "endtime", "addtraveltimetoappointment", "traveltimeinminutes", "returntobaseafterappointment", "traveltimebacktobaseinminutes", "homevisit", "syncedwithmailbox", "inactive");

            Guid healthprofessionalid2 = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //Mobile Test User 2

            Assert.AreEqual(ownerid, (Guid)fields["ownerid"]);
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(healthAppointmentID, (Guid)fields["healthappointmentid"]);
            Assert.AreEqual(healthprofessionalid2, (Guid)fields["healthprofessionalid"]);
            Assert.AreEqual(personid, (Guid)fields["personid"]);
            Assert.AreEqual(true, (bool)fields["professionalremainingforfullduration"]);
            Assert.AreEqual(appointmentStartDate, (DateTime)fields["startdate"]);
            Assert.AreEqual(startTime, (TimeSpan)fields["starttime"]);
            Assert.AreEqual(appointmentStartDate, (DateTime)fields["enddate"]);
            Assert.AreEqual(endTime, (TimeSpan)fields["endtime"]);
            Assert.AreEqual(false, (bool)fields["addtraveltimetoappointment"]);
            Assert.IsFalse(fields.ContainsKey("traveltimeinminutes"));
            Assert.AreEqual(false, (bool)fields["returntobaseafterappointment"]);
            Assert.IsFalse(fields.ContainsKey("traveltimebacktobaseinminutes"));
            Assert.AreEqual(true, (bool)fields["homevisit"]);
            Assert.AreEqual(false, (bool)fields["syncedwithmailbox"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6470")]
        [Description("UI Test for Additional Health Professionals - 0010 - " +
            "Navigate to the cases Additional Health Professionals area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void AdditionalHealthProfessionals_TestMethod010()
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
                 .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapRelatedItemsButton()
                .TapRelatedItems_LeftMenuSubArea()
                .TapAdditionalHealthProfessionalsLink();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad()
                .TapOnAddNewRecordButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .TapProfessionalOpenLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Mobile Test User 2");


            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .TapProfessionalRemainingForFullDurationField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .TapAddTravelTimeToAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .TapReturnToBaseAfterAppointmentField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .InsertStartTime("09:10")
                .InsertEndTime("09:20")
                .InsertTravelTimeInMinutes("5")
                .InsertTravelTimeBackToBaseInMinutes("10")
                .TapOnSaveAndCloseButton();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad();


            Guid additionalProfessionalRecordID = PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(healthAppointmentID)[0];
            var fields = PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByID(additionalProfessionalRecordID, "ownerid", "caseid", "healthappointmentid", "healthprofessionalid", "personid", "professionalremainingforfullduration", "startdate", "starttime", "enddate", "endtime", "addtraveltimetoappointment", "traveltimeinminutes", "returntobaseafterappointment", "traveltimebacktobaseinminutes", "homevisit", "syncedwithmailbox", "inactive");

            Guid healthprofessionalid2 = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //Mobile Test User 2

            Assert.AreEqual(ownerid, (Guid)fields["ownerid"]);
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(healthAppointmentID, (Guid)fields["healthappointmentid"]);
            Assert.AreEqual(healthprofessionalid2, (Guid)fields["healthprofessionalid"]);
            Assert.AreEqual(personid, (Guid)fields["personid"]);
            Assert.AreEqual(false, (bool)fields["professionalremainingforfullduration"]);
            Assert.AreEqual(appointmentStartDate, (DateTime)fields["startdate"]);
            Assert.AreEqual(new TimeSpan(9, 10, 0), (TimeSpan)fields["starttime"]);
            Assert.AreEqual(appointmentStartDate, (DateTime)fields["enddate"]);
            Assert.AreEqual(new TimeSpan(9, 20, 0), (TimeSpan)fields["endtime"]);
            Assert.AreEqual(true, (bool)fields["addtraveltimetoappointment"]);
            Assert.AreEqual(5, fields["traveltimeinminutes"]);
            Assert.AreEqual(true, (bool)fields["returntobaseafterappointment"]);
            Assert.AreEqual(10, fields["traveltimebacktobaseinminutes"]);
            Assert.AreEqual(false, (bool)fields["syncedwithmailbox"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6471")]
        [Description("UI Test for Additional Health Professionals - 0013 - " +
            "Navigate to the cases Additional Health Professionals area - Tap on the add button - Set data only in mandatory fields except for Professional - " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void AdditionalHealthProfessionals_TestMethod013()
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
                 .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
                .TapOnRecord(appointmentStartDate.ToString("dd'/'MM'/'yyyy"));

            caseHealthAppointmentRecordPage
                .WaitForCaseHealthAppointmentRecordPageToLoad("HEALTH APPOINTMENT: Maria Tsatsouline, Assessment, Clients or patients home")
                .TapRelatedItemsButton()
                .TapRelatedItems_LeftMenuSubArea()
                .TapAdditionalHealthProfessionalsLink();

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad()
                .TapOnAddNewRecordButton();

            communityClinicAdditionalHealthProfessionalRecordPage
                .WaitForCommunityClinicAdditionalHealthProfessionalRecordPageToLoad("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Professional' is required")
                .TapOnOKButton();

        }

        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6472")]
        [Description("UI Test for Additional Health Professionals - 0021 - Create a new Additional Health Professional using the main APP web services" +
            "Navigate to the cases Additional Health Professionals area - open the Additional Health Professional record - validate that all fields are correctly synced")]
        public void AdditionalHealthProfessionals_TestMethod021()
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
                 .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            caseHealthAppointmentsPage
                .WaitForCaseHealthAppointmentsPageToLoad("Active Appointments")
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

            communityClinicAdditionalHealthProfessionals
                .WaitForCommunityClinicAdditionalHealthProfessionalsToLoad();


            var fields = PlatformServicesHelper.healthAppointmentAdditionalProfessional.GetHealthAppointmentAdditionalProfessionalByID(additionalProfessionalRecord, "ownerid", "caseid", "healthappointmentid", "healthprofessionalid", "personid", "professionalremainingforfullduration", "startdate", "starttime", "enddate", "endtime", "addtraveltimetoappointment", "traveltimeinminutes", "returntobaseafterappointment", "traveltimebacktobaseinminutes", "homevisit", "syncedwithmailbox", "inactive");

            Assert.AreEqual(ownerid, (Guid)fields["ownerid"]);
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(healthAppointmentID, (Guid)fields["healthappointmentid"]);
            Assert.AreEqual(healthprofessionalid2, (Guid)fields["healthprofessionalid"]);
            Assert.AreEqual(personid, (Guid)fields["personid"]);
            Assert.AreEqual(true, (bool)fields["professionalremainingforfullduration"]);
            Assert.AreEqual(appointmentStartDate, (DateTime)fields["startdate"]);
            Assert.AreEqual(startTime, (TimeSpan)fields["starttime"]);
            Assert.AreEqual(appointmentStartDate, (DateTime)fields["enddate"]);
            Assert.AreEqual(endTime, (TimeSpan)fields["endtime"]);
            Assert.AreEqual(true, (bool)fields["addtraveltimetoappointment"]);
            Assert.AreEqual(5, fields["traveltimeinminutes"]);
            Assert.AreEqual(true, (bool)fields["returntobaseafterappointment"]);
            Assert.AreEqual(10, fields["traveltimebacktobaseinminutes"]);
            Assert.AreEqual(true, (bool)fields["homevisit"]);
            Assert.AreEqual(false, (bool)fields["syncedwithmailbox"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);

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

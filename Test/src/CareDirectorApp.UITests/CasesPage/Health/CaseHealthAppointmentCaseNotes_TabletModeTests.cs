using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.Cases.Health
{
    /// <summary>
    /// This class contains all test methods for Health Appointments Case Notes validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class CaseHealthAppointmentCaseNotes_TabletModeTests : TestBase
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

            //if the cases case note injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases case note review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();



            //navigate to the Settings page
            mainMenu.NavigateToCasesPage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();
        }



        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6475")]
        [Description("UI Test for Health Appointments Case Notes - 0001 - " +
            "Navigate to the Health Appointments Case Notes area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void HealthAppointmentsCaseNotes_TestMethod01()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid healthAppointmentID = new Guid("ade9ebb8-1db6-ea11-a2cd-005056926fe4");
            string appointmentDate = "01/06/2020";


            //remove any Task for the case
            foreach (Guid recordID in this.PlatformServicesHelper.healthAppointmentCaseNote.GetCaseNoteByHealthAppointmentID(healthAppointmentID))
                this.PlatformServicesHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(recordID);

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
                .TapRelatedItemsButton()
                .TapCaseNotesLink();



            healthAppointmentCaseNotesPage
                .WaitForHealthAppointmentCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            healthAppointmentCaseNoteRecordPage
                .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTES")
                .InsertSubject("Case Note 001")
                .InsertDescription("Case Note 001 description")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Assessment");

            healthAppointmentCaseNoteRecordPage
                .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTES")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Normal");

            healthAppointmentCaseNoteRecordPage
                .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTES")
                .InserDate("20/05/2020", "09:00")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: More information needed");

            healthAppointmentCaseNoteRecordPage
                .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTES")
                .TapCareInterventionLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Therapy group");

            healthAppointmentCaseNoteRecordPage
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTES")
               .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Mobile Test User 1");

            healthAppointmentCaseNoteRecordPage
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTES")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Advice");

            healthAppointmentCaseNoteRecordPage
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTES")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Home Support");

            healthAppointmentCaseNoteRecordPage
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTES")
               .TapOnSaveButton()
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTE: Case Note 001");


            var caseNotes = this.PlatformServicesHelper.healthAppointmentCaseNote.GetCaseNoteByHealthAppointmentID(healthAppointmentID);
            Assert.AreEqual(1, caseNotes.Count);
            var fields = this.PlatformServicesHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByID(caseNotes[0], "ownerid", "responsibleuserid", "caseid", "personid", "healthappointmentid", "communitycliniccareinterventionid", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "casenotedate", "subject", "statusid", "notes", "informationbythirdparty", "issignificantevent", "inactive");

            var datefield = this.PlatformServicesHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByID(caseNotes[0], "casenotedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["casenotedate"]);
            string Actualcasenotedate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");

            DateTime ExpectedCaseNoteDate = new DateTime(2020, 5, 20, 9, 0, 0);
            String ExpectedCaseNoteDateField = ExpectedCaseNoteDate.ToString("dd'/'MM'/'yyyy HH:mm");



            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4");
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            Guid careinterventionid = new Guid("9f3c9e0a-f4ab-ea11-a2cd-005056926fe4"); //Therapy group


            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(healthAppointmentID, (Guid)fields["healthappointmentid"]);
            Assert.AreEqual(careinterventionid, (Guid)fields["communitycliniccareinterventionid"]);
            Assert.AreEqual(activitycategoryid, (Guid)fields["activitycategoryid"]);
            Assert.AreEqual(activitysubcategoryid, (Guid)fields["activitysubcategoryid"]);
            Assert.AreEqual(activityoutcomeid, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual(activityreasonid, (Guid)fields["activityreasonid"]);
            Assert.AreEqual(activitypriorityid, (Guid)fields["activitypriorityid"]);
            Assert.AreEqual(ExpectedCaseNoteDateField, Actualcasenotedate);
            Assert.AreEqual("Case Note 001", (string)fields["subject"]);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual("<div>Case Note 001 description</div>", (string)fields["notes"]);
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);

        }


        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6476")]
        [Description("UI Test for Health Appointments Case Notes - 002 - Create a new cases case note using the main APP web services" +
            "Navigate to the Health Appointments Case Notes area - open the case note record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void HealthAppointmentsCaseNotes_TestMethod02()
        {
            Guid caseID = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid healthAppointmentID = new Guid("ade9ebb8-1db6-ea11-a2cd-005056926fe4");
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);
            Guid careinterventionid = new Guid("9f3c9e0a-f4ab-ea11-a2cd-005056926fe4"); //Therapy group


            //remove any cases case note for the case
            foreach (Guid caseNoteID in this.PlatformServicesHelper.healthAppointmentCaseNote.GetCaseNoteByHealthAppointmentID(healthAppointmentID))
                this.PlatformServicesHelper.healthAppointmentCaseNote.DeleteHealthAppointmentCaseNote(caseNoteID);

            Guid healthAppointmentCaseNoteID = this.PlatformServicesHelper.healthAppointmentCaseNote.CreateHealthAppointmentCaseNote(
                mobileTeam1, mobile_test_user_1UserID, caseID, personID, healthAppointmentID, careinterventionid, activitycategoryid, activitysubcategoryid, activityoutcomeid,
                activityreasonid, activitypriorityid, date, "Case Note 001", 1, "Case Note 001 description", false, false, false);

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
                .TapCaseNotesLink();


            healthAppointmentCaseNotesPage
                .WaitForHealthAppointmentCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            healthAppointmentCaseNoteRecordPage
                .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTE: Case Note 001")
                .InsertSubject("Case Note 001 updated")
                .InsertDescription("Case Note 001 description updated")
                .InserDate("21/05/2020", "09:30")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("First").TapSearchButtonQuery().TapOnRecord("Name: First Response");

            healthAppointmentCaseNoteRecordPage
                .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTE: Case Note 001")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: High");

            healthAppointmentCaseNoteRecordPage
                .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTE: Case Note 001")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Completed");

            healthAppointmentCaseNoteRecordPage
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTE: Case Note 001")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Assessment");

            healthAppointmentCaseNoteRecordPage
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTE: Case Note 001")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Health Assessment");

            healthAppointmentCaseNoteRecordPage
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTE: Case Note 001")
               .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Name: Mobile Test User 2");

            healthAppointmentCaseNoteRecordPage
               .WaitForHealthAppointmentCaseNoteRecordPageToLoad("HEALTH APPOINTMENT CASE NOTE: Case Note 001")
               .TapOnSaveAndCloseButton();

            healthAppointmentCaseNotesPage
                .WaitForHealthAppointmentCaseNotesPageToLoad();


            var fields = this.PlatformServicesHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByID(healthAppointmentCaseNoteID, "ownerid", "responsibleuserid", "caseid", "personid", "healthappointmentid", "communitycliniccareinterventionid", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "casenotedate", "subject", "statusid", "notes", "informationbythirdparty", "issignificantevent", "inactive");


            var datefield = this.PlatformServicesHelper.healthAppointmentCaseNote.GetHealthAppointmentCaseNoteByID(healthAppointmentCaseNoteID, "casenotedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["casenotedate"]);
            string Actualcasenotedate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");

            DateTime ExpectedCaseNoteDate = new DateTime(2020, 5, 21, 9, 30, 0);
            String ExpectedCaseNoteDateField = ExpectedCaseNoteDate.ToString("dd'/'MM'/'yyyy HH:mm");


            Guid mobile_test_user_2UserID = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //mobile_test_user_2
            Guid updated_activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            Guid updated_activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            Guid updated_activityoutcomeid = new Guid("4C2BEC1C-9E45-E911-A2C5-005056926FE4"); // Completed
            Guid updated_activityreasonid = new Guid("B9EC74E3-9C45-E911-A2C5-005056926FE4"); //First response
            Guid updated_activitypriorityid = new Guid("1E164C51-9D45-E911-A2C5-005056926FE4"); //High

            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_2UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(caseID, (Guid)fields["caseid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(healthAppointmentID, (Guid)fields["healthappointmentid"]);
            Assert.AreEqual(careinterventionid, (Guid)fields["communitycliniccareinterventionid"]);
            Assert.AreEqual(updated_activitycategoryid, (Guid)fields["activitycategoryid"]);
            Assert.AreEqual(updated_activitysubcategoryid, (Guid)fields["activitysubcategoryid"]);
            Assert.AreEqual(updated_activityoutcomeid, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual(updated_activityreasonid, (Guid)fields["activityreasonid"]);
            Assert.AreEqual(updated_activitypriorityid, (Guid)fields["activitypriorityid"]);
            Assert.AreEqual(ExpectedCaseNoteDateField, Actualcasenotedate);
            Assert.AreEqual("Case Note 001 updated", (string)fields["subject"]);
            Assert.AreEqual(1, (int)fields["statusid"]);
            Assert.AreEqual("<div>Case Note 001 description updated</div>", (string)fields["notes"]);
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
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

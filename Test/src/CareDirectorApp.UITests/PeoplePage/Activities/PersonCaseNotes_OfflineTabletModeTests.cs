using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.Activities
{
    /// <summary>
    /// This class contains all test methods for person case notes validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class PersonCaseNotes_OfflineTabletModeTests : TestBase
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

        #region Person Case Notes page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6672")]
        [Description("UI Test for Person Case Notes (Offline Mode)- 0002 - " +
            "Navigate to the person Case Notes area (person contains person case note records) - Validate the page content")]
        public void PersonCaseNotes_OfflineTestMethod2()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid caseNoteID = new Guid("1550185e-a49a-ea11-a2cd-005056926fe4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateSubjectCellText("Case Note 001", caseNoteID.ToString())
                .ValidateCreatedOnCellText("20/05/2020 15:15", caseNoteID.ToString())
                .ValidateResponsibleUserCellText("Mobile Test User 1", caseNoteID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", caseNoteID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", caseNoteID.ToString())
                .ValidateModifiedOnCellText("20/05/2020 15:15", caseNoteID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6673")]
        [Description("UI Test for Person Case Notes (Offline Mode)- 0004 - " +
            "Navigate to the person Case Notes area - Open a person case note record - Validate that the case note record page fields and field titles are displayed")]
        public void PersonCaseNotes_OfflineTestMethod4()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteSubject = "Case Note 001";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnRecord(CaseNoteSubject);

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .ValidateSubjectFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true)

                .ValidateReasonFieldTitleVisible(true)
                .ValidatePriorityFieldTitleVisible(true)
                .ValidateDateFieldTitleVisible(true)
                .ValidateOutcomeFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)
                .ValidateCategoryFieldTitleVisible(true)
                .ValidateSubCategoryFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)

                .ValidateSubjectFieldText(CaseNoteSubject)
                .ValidateDescriptionRichFieldText("Case Note 001 description") //for this record the description is displayed as rich text

                .ValidateReasonFieldText("Other")
                .ValidatePriorityFieldText("Low")
                .ValidateDateFieldText("20/05/2020", "07:00")
                .ValidateOutcomeFieldText("More information needed")
                .ValidateStatusFieldText("Open")
                .ValidateCategoryFieldText("Advice")
                .ValidateSubCategoryFieldText("Home Support")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("Mobile Test User 1");
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6674")]
        [Description("UI Test for Person Case Notes (Offline Mode)- 0010 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void PersonCaseNotes_OfflineTestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
                .InsertSubject("Case Note 001")
                .InsertDescription("Case Note 001 description")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Assessment");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Normal");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
                .InserDate("20/05/2020", "09:00")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("More information needed");

            personCaseNoteRecordPage
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Advice");

            personCaseNoteRecordPage
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Home Support");

            personCaseNoteRecordPage
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
               .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Mobile Test User 1");

            personCaseNoteRecordPage
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
               .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .ValidateSubjectFieldText("Case Note 001")
                .ValidateDescriptionFieldText("Case Note 001 description")

                .ValidateReasonFieldText("Assessment")
                .ValidatePriorityFieldText("Normal")
                .ValidateDateFieldText("20/05/2020", "09:00")
                .ValidateOutcomeFieldText("More information needed")
                .ValidateStatusFieldText("Open")

                .ValidateCategoryFieldText("Advice")
                .ValidateSubCategoryFieldText("Home Support")
               // .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("Mobile Test User 1");


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var caseNotes = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID);
            Assert.AreEqual(1, caseNotes.Count);
            var fields = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "personid", "casenotedate", "statusid");

            var datefield = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "casenotedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["casenotedate"]);
            string casenotedate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");


            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid businessUnitID = new Guid("BA5B9FE6-419E-E911-A2C6-005056926FE4"); //Mobile Business Unit
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal


            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Case Note 001", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual("<div>Case Note 001 description</div>", (string)fields["notes"]);
            Assert.AreEqual(activitycategoryid, (Guid)fields["activitycategoryid"]);
            Assert.AreEqual(activitysubcategoryid, (Guid)fields["activitysubcategoryid"]);
            Assert.AreEqual(activityoutcomeid, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual(activityreasonid, (Guid)fields["activityreasonid"]);
            Assert.AreEqual(activitypriorityid, (Guid)fields["activitypriorityid"]);
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(new DateTime(2020, 5, 20, 9, 0, 0).ToString("dd'/'MM'/'yyyy HH:mm"), casenotedate);
            Assert.AreEqual(1, (int)fields["statusid"]);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6675")]
        [Description("UI Test for Person Case Notes (Offline Mode)- 0013 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Set data only in mandatory fields except for Subject- " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonCaseNotes_OfflineTestMethod13()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
                //.InsertSubject("Case Note 001")
                .InserDate("20/05/2020", "09:00")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Subject' is required")
                .TapOnOKButton();
        }


        #endregion

        #region Update Record


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6676")]
        [Description("UI Test for Person Case Notes (Offline Mode)- 0017 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonCaseNotes_OfflineTestMethod17()
        {
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


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            Guid personCaseNoteID = this.PlatformServicesHelper.personCaseNote.CreatePersonCaseNote("Case Note 001", "Case Note 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, personID, date);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .InsertSubject("Case Note 001 updated")
                .InsertDescription("Case Note 001 description updated")
                .InserDate("21/05/2020", "09:30")
                .TapReasonLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("First").TapSearchButtonQuery().TapOnRecord("First Response");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .TapPriorityLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("High");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .TapOutcomeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Completed");

            personCaseNoteRecordPage
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
               .TapCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Assessment");

            personCaseNoteRecordPage
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
               .TapSubCategoryLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Health Assessment");

            personCaseNoteRecordPage
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
               .TapResponsibleUserLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapOnRecord("Mobile Test User 2");

            personCaseNoteRecordPage
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
               .TapOnSaveAndCloseButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var caseNotes = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID);
            Assert.AreEqual(1, caseNotes.Count);


            Guid mobile_test_user_2UserID = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //mobile_test_user_2
            Guid updated_activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            Guid updated_activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            Guid updated_activityoutcomeid = new Guid("4C2BEC1C-9E45-E911-A2C5-005056926FE4"); // Completed
            Guid updated_activityreasonid = new Guid("B9EC74E3-9C45-E911-A2C5-005056926FE4"); //First response
            Guid updated_activitypriorityid = new Guid("1E164C51-9D45-E911-A2C5-005056926FE4"); //High

            var fields = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "personid", "casenotedate", "statusid");

            var datefield = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "casenotedate");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var dateConverted = usersettings.ConvertTimeFromUtc((DateTime)datefield["casenotedate"]);
            string casenotedate = dateConverted.Value.ToString("dd'/'MM'/'yyyy HH:mm");


            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_2UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Case Note 001 updated", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual("<div>Case Note 001 description updated</div>", (string)fields["notes"]);
            Assert.AreEqual(updated_activitycategoryid, (Guid)fields["activitycategoryid"]);
            Assert.AreEqual(updated_activitysubcategoryid, (Guid)fields["activitysubcategoryid"]);
            Assert.AreEqual(updated_activityoutcomeid, (Guid)fields["activityoutcomeid"]);
            Assert.AreEqual(updated_activityreasonid, (Guid)fields["activityreasonid"]);
            Assert.AreEqual(updated_activitypriorityid, (Guid)fields["activitypriorityid"]);
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(new DateTime(2020, 5, 21, 9, 30, 0).ToString("dd'/'MM'/'yyyy HH:mm"), casenotedate);
            Assert.AreEqual(1, (int)fields["statusid"]);
        }


        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6677")]
        [Description("UI Test for Person Case Notes (Offline Mode)- 0020 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void PersonCaseNotes_OfflineTestMethod20()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //mobile_test_user_1U
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid activitycategoryid = new Guid("79A81B8A-9D45-E911-A2C5-005056926FE4"); //Advice
            Guid activitysubcategoryid = new Guid("1515DFDD-9D45-E911-A2C5-005056926FE4"); //Home Support
            Guid activityoutcomeid = new Guid("A9000A29-9E45-E911-A2C5-005056926FE4"); //More information needed
            Guid activityreasonid = new Guid("3E9831F8-5F75-E911-A2C5-005056926FE4"); //Assessment
            Guid activitypriorityid = new Guid("5246A13F-9D45-E911-A2C5-005056926FE4"); //Normal
            DateTime date = new DateTime(2020, 5, 20, 9, 0, 0);


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            Guid personCaseNoteID = this.PlatformServicesHelper.personCaseNote.CreatePersonCaseNote("Case Note 001", "Case Note 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, personID, date);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            var records = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID);
            Assert.AreEqual(0, records.Count);
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

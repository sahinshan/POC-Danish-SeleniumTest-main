using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;
//using Microsoft.Graph.Models;

namespace CareDirectorApp.UITests.People.Activities
{
    /// <summary>
    /// This class contains all test methods for person case notes validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonCaseNotes_TabletModeTests : TestBase
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

            //if the person body map injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the person body map review pop-up is open then close it 
            personBodyMapReviewPopup.ClosePopupIfOpen();

            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();



            //navigate to the Settings page
            mainMenu.NavigateToPeoplePage();



            //if the error pop-up is open close it
            errorPopup.ClosePopupIfOpen();

            //if the warning pop-up is open close it
            warningPopup.CloseWarningPopupIfOpen();
        }

        #region Person Case Notes page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6678")]
        [Description("UI Test for Dashboards - 0001 - " +
            "Navigate to the person Case Notes area (person do not contains person case note records) - Validate the page content")]
        public void PersonCaseNotes_TestMethod01()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6679")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the person Case Notes area (person contains person case note records) - Validate the page content")]
        public void PersonCaseNotes_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid caseNoteID = new Guid("1550185e-a49a-ea11-a2cd-005056926fe4");

            peoplePage
                .WaitForPeoplePageToLoad()
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
        [Property("JiraIssueID", "CDV6-6680")]
        [Description("UI Test for Dashboards - 0003 - " +
            "Navigate to the person Case Notes area - Open a person case note record - Validate that the case note record page is displayed")]
        public void PersonCaseNotes_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteSubject = "Case Note 001";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                ;
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6681")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the person Case Notes area - Open a person case note record - Validate that the case note record page field titles are displayed")]
        public void PersonCaseNotes_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteSubject = "Case Note 001";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .ValidateResponsibleUserFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6682")]
        [Description("UI Test for Dashboards - 0005 - " +
            "Navigate to the person Case Notes area - Open a person case note record - Validate that the case note record page fields are correctly displayed")]
        public void PersonCaseNotes_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteSubject = "Case Note 001";

            peoplePage
                .WaitForPeoplePageToLoad()
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

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6683")]
        [Description("UI Test for Dashboards - 0006 - " +
            "Navigate to the person Case Notes area - Open a person case note record (with only the mandatory information set) - Validate that the case note record page fields are correctly displayed")]
        public void PersonCaseNotes_TestMethod06()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string CaseNoteSubject = "Case Note 002";

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 002")
                .ValidateSubjectFieldText(CaseNoteSubject)
                .ValidateDescriptionFieldText("")

                .ValidateReasonFieldText("")
                .ValidatePriorityFieldText("")
                .ValidateDateFieldText("21/05/2020", "10:30")
                .ValidateOutcomeFieldText("")
                .ValidateStatusFieldText("Open")

                .ValidateCategoryFieldText("")
                .ValidateSubCategoryFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("Mobile Test User 1");
        }

        #endregion

        #region New Record page - Validate content

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6684")]
        [Description("UI Test for Dashboards - 0007 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Validate that the new record page is displayed and all field titles are visible ")]
        public void PersonCaseNotes_TestMethod07()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
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
                .ValidateResponsibleUserFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6685")]
        [Description("UI Test for Dashboards - 0008 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Validate that the new record page is displayed but the delete button is not displayed")]
        public void PersonCaseNotes_TestMethod08()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapActivitiesArea_RelatedItems()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnAddNewRecordButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTES")
                .WaitForDeleteButtonNotVisible();
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6686")]
        [Description("UI Test for Dashboards - 0009 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void PersonCaseNotes_TestMethod09()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
               .TapOnSaveButton()
               .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001");


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
        [Property("JiraIssueID", "CDV6-6687")]
        [Description("UI Test for Dashboards - 0010 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void PersonCaseNotes_TestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonCaseNotesPageToLoad();


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
        [Property("JiraIssueID", "CDV6-6688")]
        [Description("UI Test for Dashboards - 0011 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - " +
            "Re-Open the record - Validate that all fields are correctly set after saving the record")]
        public void PersonCaseNotes_TestMethod11()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .WaitForPersonCaseNotesPageToLoad();


            var caseNotes = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID);
            Assert.AreEqual(1, caseNotes.Count);

            personCaseNotesPage
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
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("Mobile Test User 1");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6689")]
        [Description("UI Test for Dashboards - 0012 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Set data only in mandatory fields - " +
            "Tap on the Save button - Validate that the data is sync to the web platform")]
        public void PersonCaseNotes_TestMethod12()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .InserDate("20/05/2020", "09:00")
                .TapOnSaveButton()
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001");


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


            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Case Note 001", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual(false, fields.ContainsKey("notes"));
            Assert.IsFalse(fields.ContainsKey("activitycategoryid"));
            Assert.IsFalse(fields.ContainsKey("activitysubcategoryid"));
            Assert.IsFalse(fields.ContainsKey("activityoutcomeid"));
            Assert.IsFalse(fields.ContainsKey("activityreasonid"));
            Assert.IsFalse(fields.ContainsKey("activitypriorityid"));
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
        [Property("JiraIssueID", "CDV6-6690")]
        [Description("UI Test for Dashboards - 0013 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Set data only in mandatory fields except for Subject- " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonCaseNotes_TestMethod13()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            peoplePage
                .WaitForPeoplePageToLoad()
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

            var caseNotes = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID);
            Assert.AreEqual(0, caseNotes.Count);

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6691")]
        [Description("UI Test for Dashboards - 0014 - " +
            "Navigate to the person Case Notes area - Tap on the add button - Set data only in mandatory fields except for Date- " +
            "Tap on the Save button - Validate that the user is prevented from saving the record")]
        public void PersonCaseNotes_TestMethod14()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            peoplePage
                .WaitForPeoplePageToLoad()
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
                //.InserDate("20/05/2020", "09:00")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The field 'Date' is required")
                .TapOnOKButton();

            var caseNotes = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID);
            Assert.AreEqual(0, caseNotes.Count);

        }

        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6692")]
        [Description("UI Test for Dashboards - 0015 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - validate that all fields are correctly synced")]
        public void PersonCaseNotes_TestMethod15()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .ValidateSubjectFieldText("Case Note 001")
                .ValidateDescriptionFieldText("Case Note 001 description")

                .ValidateReasonFieldText("Assessment")
                .ValidatePriorityFieldText("Normal")
                .ValidateDateFieldText("20/05/2020", "09:00")
                .ValidateOutcomeFieldText("More information needed")
                .ValidateStatusFieldText("Open")

                .ValidateCategoryFieldText("Advice")
                .ValidateSubCategoryFieldText("Home Support")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateResponsibleUserFieldText("Mobile Test User 1");

        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6693")]
        [Description("UI Test for Dashboards - 0016 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - clear all non mandatory fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonCaseNotes_TestMethod16()
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

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapCaseNotesIcon_RelatedItems();

            personCaseNotesPage
                .WaitForPersonCaseNotesPageToLoad()
                .TapOnRecord("Case Note 001");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .InsertDescription("")
                .TapReasonRemoveButton()
                .TapPriorityRemoveButton()
                .ValidateDateFieldText("20/05/2020", "09:00")
                .TapOutcomeRemoveButton()
                .TapSubCategoryRemoveButton()
                .TapCategoryRemoveButton()
                .TapResponsibleUserRemoveButton()
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
              .WaitForPersonCaseNotesPageToLoad();


            var caseNotes = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID);
            Assert.AreEqual(1, caseNotes.Count);
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

            var fields = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "personid", "casenotedate", "statusid");
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["createdby"]);
            Assert.IsNotNull((DateTime)fields["createdon"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["modifiedby"]);
            Assert.IsNotNull((DateTime)fields["modifiedon"]);
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.IsFalse(fields.ContainsKey("responsibleuserid"));
            Assert.AreEqual(businessUnitID, (Guid)fields["owningbusinessunitid"]);
            Assert.AreEqual("Case Note 001", (string)fields["subject"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
            Assert.AreEqual("<div></div>", fields["notes"]);
            Assert.IsFalse(fields.ContainsKey("activitycategoryid"));
            Assert.IsFalse(fields.ContainsKey("activitysubcategoryid"));
            Assert.IsFalse(fields.ContainsKey("activityoutcomeid"));
            Assert.IsFalse(fields.ContainsKey("activityreasonid"));
            Assert.IsFalse(fields.ContainsKey("activitypriorityid"));
            Assert.AreEqual(false, (bool)fields["informationbythirdparty"]);
            Assert.AreEqual(false, (bool)fields["issignificantevent"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(date.ToString("dd'/'MM'/'yyyy HH:mm:ss"), usersettings.ConvertTimeFromUtc((DateTime)fields["casenotedate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm:ss"));
            Assert.AreEqual(1, (int)fields["statusid"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6694")]
        [Description("UI Test for Dashboards - 0017 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonCaseNotes_TestMethod17()
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
            DateTime date = new DateTime(2020, 5, 21, 9, 30, 0);


            //remove any person body map for the person
            foreach (Guid caseNoteID in this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID))
                this.PlatformServicesHelper.personCaseNote.DeletePersonCaseNote(caseNoteID);

            Guid personCaseNoteID = this.PlatformServicesHelper.personCaseNote.CreatePersonCaseNote("Case Note 001", "Case Note 001 description", mobileTeam1, mobile_test_user_1UserID, activitycategoryid, activitysubcategoryid, activityoutcomeid, activityreasonid, activitypriorityid, personID, date);

            peoplePage
                .WaitForPeoplePageToLoad()
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


            var caseNotes = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByPersonID(personID);
            Assert.AreEqual(1, caseNotes.Count);


            Guid mobile_test_user_2UserID = new Guid("3AB63B6A-5D9E-E911-A2C6-005056926FE4"); //mobile_test_user_2
            Guid updated_activitycategoryid = new Guid("1d2b78b8-9d45-e911-a2c5-005056926fe4"); //Assessment
            Guid updated_activitysubcategoryid = new Guid("eec317f4-9d45-e911-a2c5-005056926fe4"); //Health Assessment
            Guid updated_activityoutcomeid = new Guid("4C2BEC1C-9E45-E911-A2C5-005056926FE4"); // Completed
            Guid updated_activityreasonid = new Guid("B9EC74E3-9C45-E911-A2C5-005056926FE4"); //First response
            Guid updated_activitypriorityid = new Guid("1E164C51-9D45-E911-A2C5-005056926FE4"); //High

            var fields = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(caseNotes[0], "createdby", "createdon", "modifiedby", "modifiedon", "ownerid", "responsibleuserid", "owningbusinessunitid", "subject", "inactive", "notes", "activitycategoryid", "activitysubcategoryid", "activityoutcomeid", "activityreasonid", "activitypriorityid", "informationbythirdparty", "issignificantevent", "personid", "casenotedate", "statusid");
            var usersettings = PlatformServicesHelper.GetMetadataUserSettings();

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
            Assert.AreEqual(date.ToString("dd'/'MM'/'yyyy HH:mm:ss"), usersettings.ConvertTimeFromUtc((DateTime)fields["casenotedate"]).Value.ToString("dd'/'MM'/'yyyy HH:mm:ss"));
            Assert.AreEqual(1, (int)fields["statusid"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6695")]
        [Description("UI Test for Dashboards - 0018 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - Set the status to Completed - Save the record - Validate that the record gets deactivated")]
        public void PersonCaseNotes_TestMethod18()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(1)
                .TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
               .WaitForPersonCaseNotesPageToLoad()
               .ValidateNoRecordsMessageVisibility(true);


            var fields = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID,"inactive");
            Assert.AreEqual(true, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6696")]
        [Description("UI Test for Dashboards - 0019 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - Set the status to Cancelled - Save the record - Validate that the record gets deactivated")]
        public void PersonCaseNotes_TestMethod19()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(2)
                .TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
               .WaitForPersonCaseNotesPageToLoad()
               .ValidateNoRecordsMessageVisibility(true);


            var fields = this.PlatformServicesHelper.personCaseNote.GetPersonCaseNoteByID(personCaseNoteID, "inactive");
            Assert.AreEqual(true, (bool)fields["inactive"]);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6697")]
        [Description("UI Test for Dashboards - 0021 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - Set the status to Cancelled - Save the record - Validate that the record is no longer editable")]
        public void PersonCaseNotes_TestMethod21()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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
                .TapStatusPicklist();

            pickList
                .WaitForPickListToLoad()
                .ScrollUpPicklist(2)
                .TapOKButton();

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .TapOnSaveAndCloseButton();

            personCaseNotesPage
               .WaitForPersonCaseNotesPageToLoad()
               .ValidateNoRecordsMessageVisibility(true)
               .TapOnViewPicker();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            personCaseNotesPage
               .WaitForPersonCaseNotesPageToLoad("All Case Notes")
               .TapOnRecord("Case Note 001");

            personCaseNoteRecordPage
                .WaitForPersonCaseNoteRecordPageToLoad("PERSON CASE NOTE: Case Note 001")
                .ValidateReasonRemoveButtonVisible(false)
                .ValidatePriorityRemoveButtonVisible(false)
                .ValidateOutcomeRemoveButtonVisible(false)
                .ValidateCategoryRemoveButtonVisible(false)
                .ValidateSubCategoryRemoveButtonVisible(false)
                .ValidateResponsibleUserRemoveButtonVisible(false)
                .ValidateReasonLookupButtonVisible(false)
                .ValidatePriorityLookupButtonVisible(false)
                .ValidateOutcomeLookupButtonVisible(false)
                .ValidateCategoryLookupButtonVisible(false)
                .ValidateSubCategoryLookupButtonVisible(false)
                .ValidateResponsibleUserLookupButtonVisible(false)
                ;
        }

        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6698")]
        [Description("UI Test for Dashboards - 0020 - Create a new person case note using the main APP web services" +
            "Navigate to the person Case Notes area - open the case note record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void PersonCaseNotes_TestMethod20()
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

            peoplePage
                .WaitForPeoplePageToLoad()
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

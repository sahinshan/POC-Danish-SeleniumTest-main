using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.RelatedItems
{
    /// <summary>
    /// This class contains all test methods for Person Forms validations while the app is displaying in tablet mode
    /// </summary>
    [TestFixture]
    [Category("MobileOffline")]
    public class PersonForms_OfflineTabletModeTests : TestBase
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

        #region person Forms page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7200")]
        [Description("UI Test for Person Form records (Offline Mode) - 0002 - " +
            "Navigate to the person Forms area (person contains Form records) - Validate the page content")]
        public void PersonForms_OfflineTestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid personFormID = new Guid("dc59e270-d3a4-ea11-a2cd-005056926fe4");

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .ValidateFormTypeCellText("Mobile - Person Form", personFormID.ToString())
                .ValidateResponsibleUserCellText("Mobile Test User 1", personFormID.ToString())
                .ValidateStartDateCellText("01/06/2020", personFormID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", personFormID.ToString())
                .ValidateCreatedOnCellText("02/06/2020 14:17", personFormID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", personFormID.ToString())
                .ValidateModifiedOnCellText("03/06/2020 11:26", personFormID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7201")]
        [Description("UI Test for Person Form records (Offline Mode) - 0004 - " +
            "Navigate to the person Forms area - Open a person Form record - Validate that the Form record page fields and titles are displayed")]
        public void PersonForms_OfflineTestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateFormTypeFieldTitleVisible(true)
                .ValidateResponsibleUserFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateStatusFieldTitleVisible(true)

                .ValidateStartDateFieldTitleVisible(true)
                .ValidateDueDateFieldTitleVisible(true)
                .ValidateReviewDateFieldTitleVisible(true)
                .ValidatePersonFieldTitleVisible(true)

                .ValidateCompletedByFieldTitleVisible(false)
                .ValidateCompletionDateFieldTitleVisible(false)
                .ValidateSignedOffByFieldTitleVisible(true)
                .ValidateSignedOffDateFieldTitleVisible(true)

                .ValidateFormTypeFieldText("Mobile - Person Form")
                .ValidateResponsibleUserFieldText("Mobile Test User 1")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateStatusFieldText("In Progress")

                .ValidateStartDateFieldText("01/06/2020")
                .ValidateDueDateFieldText("30/06/2020")
                .ValidateReviewDateFieldText("23/06/2020")
                .ValidatePersonFieldText("Pavel MCNamara")

                //.ValidateCompletedByFieldText("")
                //.ValidateCompletionDateFieldText("")
                .ValidateSignedOffByFieldText("")
                .ValidateSignedOffDateFieldText("");
        }

        #endregion

        #region Create New Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7202")]
        [Description("UI Test for Person Form records (Offline Mode) - 0010 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all fields - " +
            "Tap on the Save & Close button - Validate that the data is sync to the web platform")]
        public void PersonForms_OfflineTestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .InsertDueDate("02/06/2020")
                .InsertReviewDate("03/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateFormTypeFieldText("Mobile - Person Form")
                .ValidateResponsibleUserFieldText("Mobile Test User 1")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateStatusFieldText("In Progress")

                .ValidateStartDateFieldText("01/06/2020")
                .ValidateDueDateFieldText("02/06/2020")
                .ValidateReviewDateFieldText("03/06/2020")
                .ValidatePersonFieldText("Maria Tsatsouline")

                .ValidateCompletedByFieldText("")
                .ValidateCompletionDateFieldText("")
                .ValidateSignedOffByFieldText("")
                .ValidateSignedOffDateFieldText("");

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);
            Assert.AreEqual(1, forms.Count);
            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "ownerid", "personid", "responsibleuserid", "caseid", "documentid", "assessmentstatusid", "startdate", "duedate", "reviewdate", "sdeexecuted", "answersinitialized", "inactive");

            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);

            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(documentid, (Guid)fields["documentid"]);
            Assert.AreEqual(assessmentstatusid, (int)fields["assessmentstatusid"]);
            Assert.AreEqual(startdate, (DateTime)fields["startdate"]);
            Assert.AreEqual(duedate, (DateTime)fields["duedate"]);
            Assert.AreEqual(reviewdate, (DateTime)fields["reviewdate"]);
            Assert.AreEqual(false, (bool)fields["sdeexecuted"]);
            Assert.AreEqual(true, (bool)fields["answersinitialized"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);

        }

        #endregion

        #region Update Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7203")]
        [Description("UI Test for Person Form records (Offline Mode) - 0017 - Create a new person Form using the main APP web services" +
            "Navigate to the person Forms area - open the Form record - update all fields - Tap on the save button - " +
            "Validate that the record is correctly saved and synced")]
        public void PersonForms_OfflineTestMethod17()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);



            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();


            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertStartDate("04/06/2020")
                .InsertDueDate("05/06/2020")
                .InsertReviewDate("06/06/2020")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            startdate = new DateTime(2020, 6, 4);
            duedate = new DateTime(2020, 6, 5);
            reviewdate = new DateTime(2020, 6, 6);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(personFormID, "ownerid", "personid", "responsibleuserid", "caseid", "documentid", "assessmentstatusid", "startdate", "duedate", "reviewdate", "sdeexecuted", "answersinitialized", "inactive");
            Assert.AreEqual(mobileTeam1, (Guid)fields["ownerid"]);
            Assert.AreEqual(personID, (Guid)fields["personid"]);
            Assert.AreEqual(mobile_test_user_1UserID, (Guid)fields["responsibleuserid"]);
            Assert.IsFalse(fields.ContainsKey("caseid"));
            Assert.AreEqual(documentid, (Guid)fields["documentid"]);
            Assert.AreEqual(assessmentstatusid, (int)fields["assessmentstatusid"]);
            Assert.AreEqual(startdate, (DateTime)fields["startdate"]);
            Assert.AreEqual(duedate, (DateTime)fields["duedate"]);
            Assert.AreEqual(reviewdate, (DateTime)fields["reviewdate"]);
            Assert.AreEqual(false, (bool)fields["sdeexecuted"]);
            Assert.AreEqual(false, (bool)fields["answersinitialized"]);
            Assert.AreEqual(false, (bool)fields["inactive"]);
        }

        #endregion

        #region Delete record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7204")]
        [Description("UI Test for Person Form records (Offline Mode) - 0020 - Create a new person Form using the main APP web services" +
            "Navigate to the person Forms area - open the Form record - Tap on the delete button - " +
            "Validate that the record is deleted from the web app database")]
        public void PersonForms_OfflineTestMethod20()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);



            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnDeleteButton();

            warningPopup
                .WaitForWarningPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Delete", "Are you sure you want to delete this record?")
                .TapOnYesButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();

            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            var records = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);
            Assert.AreEqual(0, records.Count);
        }

        #endregion

        #region Assessment window


        #region Open Assessment


        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7205")]
        [Description("UI Test for Assessment records - 0002 - " +
            "Navigate to the person Forms area - Open a person Form record - Tap on the edit assessment button - Validate that the Assessment record page fields and titles are displayed")]
        public void PersonForms_EditAssessment_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldTitleVisible(true)
                .ValidateDecimalFieldTitleVisible(true)
                .ValidateMultipleChoiceFieldTitleVisible(true)
                .ValidateMultipleResponseFieldTitlesVisible(true)
                .ValidateNumericFieldTitleVisible(true)
                .ValidateParagraphExplanationFieldTitleVisible(true)
                .ValidateFrequencyFieldTitleVisible(true)
                .ValidateShortAnswerFieldTitleVisible(true)
                .ValidateTitleFieldVisible(true)
                .ValidateAuthorisationFieldTitleVisible(true)
                .ValidateTimeQuestionFieldTitleVisible(true)
                .ValidateYesNoFieldTitleVisible(true)

                .ValidateDateOfBirthFieldText("01/06/2020")
                .ValidateDecimalFieldText("19.25")
                .ValidateMultipleChoiceFieldText("MC3")
                .ValidateMultipleResponseOption1FieldText("Yes")
                .ValidateMultipleResponseOption2FieldText("No")
                .ValidateMultipleResponseOption3FieldText("Yes")
                .ValidateNumericFieldText("54")
                .ValidateParagraphExplanationFieldText("value 1\nvalue 2")
                .ValidateFrequencyFieldText("Daily")
                .ValidateShortAnswerFieldText("V 1")
                .ValidateSignatureFieldVisible(true)
                .ValidateTimeQuestionFieldText("08:30")
                .ValidateYesNoFieldText("Yes");
        }

        #endregion


        #region Update Assessment

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7206")]
        [Description("UI Test for Assessment records - 0008 - Create and save a new person form record - " +
            "After saving tap on the edit assessment button - set data in all answers - Save and re-open the assessment " +
            "Validate that all answers are correctly saved")]
        public void PersonForms_EditAssessment_TestMethod08()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapOpenHierarchyButton()
               .TapSection2HierarchyWindow()//there is an issue with scrolling to the answers. we have to scroll to the end of the document and then set the answers from the end to the beginning
               .TapOpenHierarchyButton()
               .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .InsertTimeQuestionAnswer("15:25")
               .InsertShortAnswer("V 1")
               .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(3).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertParagraphExplanationAnswer("value 1\nvalue 2")
                .InsertNumericAnswer("96")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
               .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
               .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDecimalAnswer("10.52")
                .InsertDateOfBirthAnswer("10/06/2020")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");


            //GO BACK ONLINE AND SYNC THE UPDATE TO THE ASSESSMENT
            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();


            //GO BACK AND RE-OPEN THE ASSESSMENT IN ONLINE MODE TO VALIDATE THE SYNC
            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldText("10/06/2020")
                .ValidateDecimalFieldText("10.52")
                .ValidateMultipleChoiceFieldText("MC3")
                .ValidateMultipleResponseOption1FieldText("Yes")
                .ValidateMultipleResponseOption2FieldText("No")
                .ValidateMultipleResponseOption3FieldText("Yes")
                .ValidateNumericFieldText("96")
                .ValidateParagraphExplanationFieldText("value 1\nvalue 2")
                .ValidateFrequencyFieldText("Hourly")
                .ValidateShortAnswerFieldText("V 1")
                .ValidateSignatureFieldVisible(true)
                .ValidateTimeQuestionFieldText("15:25")
                .ValidateYesNoFieldText("Yes");

        }
        
        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7207")]
        [Description("UI Test for Assessment records - 0010 - Open existing person form record - " +
            "Tap on the edit assessment button - Set data in the Decimal question - Leave the Date of Birth question empty (this question is mandatory) - " +
            "tap on the save button - validate that the user is prevented from saving the record with an empty mandatory question")]
        public void PersonForms_EditAssessment_TestMethod10()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("c0dc65d7-bea4-ea11-a2cd-005056926fe4"); //Mobile - Person Form
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form", assessmentstatusid, startdate, duedate, reviewdate);


            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord("01/06/2020");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormEditAssessmentPage
                .WaitForMobilePersonFormEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDecimalAnswer("10.52")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Error", "The question 'Date of Birth' is mandatory and must be answered before saving")
                .TapOnOKButton();
        }



        #endregion


        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-6136

        [Test]
        [Property("JiraIssueID", "CDV6-7208")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Open a person Form record (Source Case is set) - Validate that the Source Case Form field is displayed")]
        public void PersonForms_SourceCase_OfflineTestMethod001()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara 
            string personFormStartDate = "01/06/2020";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateSourceCaseFieldTitleVisible(true)
                .ValidateSourceCaseLookupEntryFieldText("MCNamara, Pavel - (01/05/1960) [CAS-000004-6174]")
                ;
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7209")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Open a person Form record (Source Case is NOT set) - Validate that the Source Case Form field is displayed empty")]
        public void PersonForms_SourceCase_OfflineTestMethod002()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Pavel MCNamara 
            string personFormStartDate = "02/06/2020";

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnRecord(personFormStartDate);

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form for Mr Pavel MCNamara Starting 02/06/2020 created by Mobile Test User 1")
                .ValidateSourceCaseFieldTitleVisible(true)
                .ValidateSourceCaseLookupEntryFieldText("")
                ;
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7210")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all mandatory fields - Tap on the Source Case lookup button - " +
            "Select a Case record linked to the person - Save the record - Validate that the source case information is correctly saved")]
        public void PersonForms_SourceCase_OfflineTestMethod003()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            var caseid = new Guid("0e22b6e2-9a93-ea11-a2cd-005056926fe4");

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapSourceCaseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("CAS-000004").TapSearchButtonQuery().TapOnRecord("CAS-000004-6176");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);
            Assert.AreEqual(1, forms.Count);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "caseid");
            Assert.AreEqual(caseid, (Guid)fields["caseid"]);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7211")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all mandatory fields - do not select a Source Case - " +
            "Save the record - Validate that the source case field in the DB has no data")]
        public void PersonForms_SourceCase_OfflineTestMethod004()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapOnSaveAndCloseButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad();


            mainMenu.NavigateToSettingsPage();

            settingsPage.SetTheAppInOnlineMode();

            var forms = this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID);

            Assert.AreEqual(1, forms.Count);

            var fields = this.PlatformServicesHelper.personForm.GetPersonFormByID(forms[0], "caseid");
            Assert.AreEqual(false, fields.ContainsKey("caseid"));
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7212")]
        [Description("JIRA Story Id : https://advancedcsg.atlassian.net/browse/CDV6-6136 - " +
            "Navigate to the person Forms area - Tap on the add button - Set data in all mandatory fields - Tap on the Source Case lookup button - " +
            "Search for a Case Record that do not belong to the person - Validate that no information is displayed")]
        public void PersonForms_SourceCase_OfflineTestMethod005()
        {
            var personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            var caseid = new Guid("2b992f0d-4791-ea11-a2cd-005056926fe4");

            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            settingsPage.SetTheAppInOfflineMode();

            mainMenu.NavigateToPeoplePage();

            peoplePage
                .WaitForPeoplePageToLoad("My Pinned People")
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonFormsIcon_RelatedItems();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .TapOnAddNewRecordButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .InsertStartDate("01/06/2020")
                .TapFormTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().InsertSearchQuery("Mobile").TapSearchButtonQuery().TapOnRecord("Mobile - Person Form");

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORMS (PERSON)")
                .TapSourceCaseLookupButton();

            lookupPopup.WaitForLookupPopupToLoad()
                .InsertSearchQuery("CAS-000004-616")
                .TapSearchButtonQuery()
                .ValidateElementNotPresent("CAS-000004-6160")
                .ValidateElementNotPresent("CAS-000004-6161")
                .ValidateElementNotPresent("CAS-000004-6162")
                .ValidateElementNotPresent("CAS-000004-6163")
                .ValidateElementNotPresent("CAS-000004-6164")
                .ValidateElementNotPresent("CAS-000004-6165")
                .ValidateElementNotPresent("CAS-000004-6166")
                .ValidateElementNotPresent("CAS-000004-6167")
                .ValidateElementNotPresent("CAS-000004-6168")
                .ValidateElementNotPresent("CAS-000004-6169");

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

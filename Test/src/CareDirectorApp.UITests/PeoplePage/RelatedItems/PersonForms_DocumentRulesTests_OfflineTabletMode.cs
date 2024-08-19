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
    public class PersonForms_DocumentRulesTests_OfflineTabletMode : TestBase
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

        


        #region 2 - Condition Operator Targets

        [Test]
        [Property("JiraIssueID", "CDV6-6967")]
        [Description("UI Test for Condition Operator Targets - 001 - Step 1 - Target - Question - Date (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod001()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3DateOfBirthAnswer("01/01/2000")
                .InsertShortAnswer("Document Rule Targets Step 1")
                .InsertDateOfBirthAnswer("01/01/2000")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 1 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6968")]
        [Description("UI Test for Condition Operator Targets - 001 - Step 1 - Target - Question - Date (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod001_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3DateOfBirthAnswer("01/01/2000")
                .InsertShortAnswer("Document Rule Targets Step 1")
                .InsertDateOfBirthAnswer("02/01/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-6969")]
        [Description("UI Test for Condition Operator Targets - 002 - Step 2 - Target - Question - Decimal (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod002()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3DecimalAnswer("9.3")
                .InsertShortAnswer("Document Rule Targets Step 2")
                .InsertDecimalAnswer("9.3")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 2 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6970")]
        [Description("UI Test for Condition Operator Targets - 002 - Step 2 - Target - Question - Decimal (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod002_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3DecimalAnswer("9.3")
                .InsertShortAnswer("Document Rule Targets Step 2")
                .InsertDecimalAnswer("8.3")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-6971")]
        [Description("UI Test for Condition Operator Targets - 003 - Step 3 - Target - Question - Numeric (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod003()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3NumericAnswer("9")
                .InsertShortAnswer("Document Rule Targets Step 3")
                .InsertNumericAnswer("9")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 3 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6972")]
        [Description("UI Test for Condition Operator Targets - 003 - Step 3 - Target - Question - Numeric (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod003_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3NumericAnswer("8")
                .InsertShortAnswer("Document Rule Targets Step 3")
                .InsertNumericAnswer("9")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-6973")]
        [Description("UI Test for Condition Operator Targets - 004 - Step 4 - Target - Question - Paragraph (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod004()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3ParagraphExplanationAnswer("value 1 \n value 2")
                .InsertShortAnswer("Document Rule Targets Step 4")
                .InsertParagraphExplanationAnswer("value 1 \n value 2")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 4 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6974")]
        [Description("UI Test for Condition Operator Targets - 004 - Step 4 - Target - Question - Paragraph (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod004_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3ParagraphExplanationAnswer("value 1 \n value 3")
                .InsertShortAnswer("Document Rule Targets Step 4")
                .InsertParagraphExplanationAnswer("value 1 \n value 2")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-6975")]
        [Description("UI Test for Condition Operator Targets - 005 - Step 5 - Target - Question - Short Answer (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod005()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3ShortAnswer("value 1")
                .InsertShortAnswer("value 1")
                .InsertParagraphExplanationAnswer("Document Rule Targets Step 5")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 5 Activated (1)")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6976")]
        [Description("UI Test for Condition Operator Targets - 005 - Step 5 - Target - Question - Short Answer (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod005_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3ParagraphExplanationAnswer("value 1")
                .InsertShortAnswer("value 1")
                .InsertParagraphExplanationAnswer("Document Rule Targets Step 5")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 5 Activated (2)")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6977")]
        [Description("UI Test for Condition Operator Targets - 005 - Step 5 - Target - Question - Short Answer (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod005_2()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3TimeQuestionAnswer("09:00")
                .InsertShortAnswer("09:00")
                .InsertParagraphExplanationAnswer("Document Rule Targets Step 5")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 5 Activated (3)")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6978")]
        [Description("UI Test for Condition Operator Targets - 005 - Step 5 - Target - Question - Short Answer (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod005_3()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3TimeQuestionAnswer("09:18")
                .InsertSection3ShortAnswer("09:16")
                .InsertSection3ParagraphExplanationAnswer("09:17")
                .InsertShortAnswer("09:15")
                .InsertParagraphExplanationAnswer("Document Rule Targets Step 5")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-6979")]
        [Description("UI Test for Condition Operator Targets - 006 - Step 6 - Target - Question - Time (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod006()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()               
                .InsertSection3TimeQuestionAnswer("09:15")
                .InsertTimeQuestionAnswer("09:15")
                .InsertShortAnswer("Document Rule Targets Step 6")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 6 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6980")]
        [Description("UI Test for Condition Operator Targets - 006 - Step 6 - Target - Question - Time (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod006_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3TimeQuestionAnswer("09:16")
                .InsertTimeQuestionAnswer("09:15")
                .InsertShortAnswer("Document Rule Targets Step 6")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-6981")]
        [Description("UI Test for Condition Operator Targets - 007 - Step 7 - Target - Question - Yes/No (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod007()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 7")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapSection3YesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 7 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6982")]
        [Description("UI Test for Condition Operator Targets - 007 - Step 7 - Target - Question - Yes/No (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod007_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 7")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapSection3YesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(2).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-6983")]
        [Description("UI Test for Condition Operator Targets - 008 - Step 8 - Target - Placeholder - Date (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod008()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 8")
                .InsertDateOfBirthAnswer("01/01/2000")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 8 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6984")]
        [Description("UI Test for Condition Operator Targets - 008 - Step 8 - Target - Placeholder - Date (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod008_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 8")
                .InsertDateOfBirthAnswer("02/01/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-6985")]
        [Description("UI Test for Condition Operator Targets - 009 - Step 9 - Target - Placeholder - Decimal (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod009()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 9")
                .InsertDecimalAnswer("1.5")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 9 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6986")]
        [Description("UI Test for Condition Operator Targets - 009 - Step 9 - Target - Placeholder - Decimal (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod009_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 9")
                .InsertDecimalAnswer("1.51")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-6987")]
        [Description("UI Test for Condition Operator Targets - 010 - Step 10 - Target - Placeholder - Numeric (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod010()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 10")
                .InsertNumericAnswer("2")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 10 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6988")]
        [Description("UI Test for Condition Operator Targets - 010 - Step 10 - Target - Placeholder - Numeric (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod010_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 10")
                .InsertNumericAnswer("1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-6989")]
        [Description("UI Test for Condition Operator Targets - 011 - Step 11 - Target - Placeholder - Text (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod011()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 11")
                .InsertParagraphExplanationAnswer("default placeholder value")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 11 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6990")]
        [Description("UI Test for Condition Operator Targets - 011 - Step 11 - Target - Placeholder - Text (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod011_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 11")
                .InsertParagraphExplanationAnswer("default placeholder value X")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-6991")]
        [Description("UI Test for Condition Operator Targets - 012 - Step 12 - Target - Placeholder - Boolean (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod012()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 12")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rule Targets - Step 12 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-6992")]
        [Description("UI Test for Condition Operator Targets - 012 - Step 12 - Target - Placeholder - Boolean (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_ConditionOperatorTargets_TestMethod012_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Targets Step 12")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }

        #endregion


        #region 3 - Document Rule Actions


        [Test]
        [Property("JiraIssueID", "CDV6-6993")]
        [Description("UI Test for Document Rules Actions - 002 - Step 2 - Empty Field (Positive scenario)")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod002()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertTimeQuestionAnswer("10:05")
                .InsertShortAnswer("Document Rule Actions 2")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertParagraphExplanationAnswer("value 1")
                .InsertNumericAnswer("2")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleResponseOption2Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDecimalAnswer("5.7")
                .InsertDateOfBirthAnswer("01/01/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldText("")
                .ValidateDecimalFieldText("")
                .ValidateMultipleChoiceFieldText("")
                .ValidateMultipleResponseOption1FieldText("No")
                .ValidateMultipleResponseOption2FieldText("No")
                .ValidateMultipleResponseOption3FieldText("No")
                .ValidateNumericFieldText("")
                .ValidateParagraphExplanationFieldText("")
                .ValidateFrequencyFieldText("")
                .ValidateShortAnswerFieldText("")
                .ValidateTimeQuestionFieldText("")
                .ValidateYesNoFieldText(" ");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6994")]
        [Description("UI Test for Document Rules Actions - 002 - Step 2 - Empty Field (Negative scenario)")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod002_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertTimeQuestionAnswer("10:05")
                .InsertShortAnswer("X Document Rule Actions 2 X")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertParagraphExplanationAnswer("value 1")
                .InsertNumericAnswer("2")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleResponseOption2Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDecimalAnswer("5.70")
                .InsertDateOfBirthAnswer("01/01/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateDateOfBirthFieldText("01/01/2000")
                .ValidateDecimalFieldText("5.70")
                .ValidateMultipleChoiceFieldText("MC1")
                .ValidateMultipleResponseOption1FieldText("Yes")
                .ValidateMultipleResponseOption2FieldText("Yes")
                .ValidateMultipleResponseOption3FieldText("Yes")
                .ValidateNumericFieldText("2")
                .ValidateParagraphExplanationFieldText("value 1")
                .ValidateFrequencyFieldText("Bi-Weekly")
                .ValidateShortAnswerFieldText("X Document Rule Actions 2 X")
                .ValidateTimeQuestionFieldText("10:05")
                .ValidateYesNoFieldText("Yes");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-6995")]
        [Description("UI Test for Document Rules Actions - 003 - Step 3 - Evaluate Formula (Divide scenario)")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod003()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 3 (1)")
                .InsertNumericAnswer("4")
                .InsertDecimalAnswer("25.5")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .ValidateSection3DecimalFieldText("6.375");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-6996")]
        [Description("UI Test for Document Rules Actions - 003 - Step 3 - Evaluate Formula (Multiply scenario)")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod003_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                 .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                 .TapOpenHierarchyButton()
                 .TapSection4HierarchyWindow()
                 .TapOpenHierarchyButton()
                 .InsertShortAnswer("Document Rule Actions 3 (2)")
                 .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .ValidateDecimalFieldText("143.1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-6997")]
        [Description("UI Test for Document Rules Actions - 004 - Step 4 - Focus Field")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod004()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 4")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .ValidateSection3ShortAnswerIsFocused();

        }



        [Test]
        [Property("JiraIssueID", "CDV6-6998")]
        [Description("UI Test for Document Rules Actions - 005 - Step 5 - Hide Section / Show Section")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod005()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 5")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .ValidateSection_1_1_VisibleHierarchyWindow(false) //validate that section 1.1 is not visible
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 5 Updated") //change the short answer value so that the section 1.1 is displayed
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .ValidateSection_1_1_VisibleHierarchyWindow(true); //section 1.1 should be visible now

        }



        [Test]
        [Property("JiraIssueID", "CDV6-6999")]
        [Description("UI Test for Document Rules Actions - 006 - Step 6 - Hide Section Question / Show Section Question")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod006()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 6")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTimeQuestionVisibility(false)
                .InsertShortAnswer("Document Rule Actions 6 Updated") //change the short answer value so that the section 1.1 is displayed
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTimeQuestionVisibility(true);
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7000")]
        [Description("UI Test for Document Rules Actions - 007 - Step 7 - Hide Single Question / Show Single Question")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod007()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 7")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTimeQuestionVisibility(false)
                .InsertShortAnswer("Document Rule Actions 7 Updated") //change the short answer value so that the section 1.1 is displayed
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .ValidateTimeQuestionVisibility(true);
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7001")]
        [Description("UI Test for Document Rules Actions - 008 - Step 8 - Run Another Rule")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod008()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 8")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "On Demand Document Rule Action - Step 1 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7002")]
        [Description("UI Test for Document Rules Actions - 009 - Step 9.1 - Set Placeholder")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod009_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                
                .InsertShortAnswer("Document Rule Actions 9 (1)")
                .TapOnSaveButton()
                
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                
                .ValidateYesNoFieldText("Yes")
                .ValidateParagraphExplanationFieldText("v1 v2")
                .ValidateNumericFieldText("7")
                .ValidateDecimalFieldText("9.1")
                .ValidateDateOfBirthFieldText("01/06/2020");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-7003")]
        [Description("UI Test for Document Rules Actions - 009 - Step 9.1 - Set Placeholder")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod009_2()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapSection3YesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()

                .InsertSection3ParagraphExplanationAnswer("v3 v4")
                .InsertSection3NumericAnswer("5")
                .InsertSection3DecimalAnswer("1.50")
                .InsertSection3DateOfBirthAnswer("01/06/1999")
                .InsertShortAnswer("Document Rule Actions 9 (2)")
                .TapOnSaveButton()

                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()

                .ValidateYesNoFieldText("Yes")
                .ValidateParagraphExplanationFieldText("v3 v4")
                .ValidateNumericFieldText("5")
                .ValidateDecimalFieldText("1.50")
                .ValidateDateOfBirthFieldText("01/06/1999");

        }

        [Test]
        [Property("JiraIssueID", "CDV6-7004")]
        [Description("UI Test for Document Rules Actions - 009 - Step 9.1 - Set Placeholder")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod009_3()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()

                .InsertShortAnswer("Document Rule Actions 9 (3)")
                .TapOnSaveButton()

                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()

                .ValidateYesNoFieldText("Yes")
                .ValidateParagraphExplanationFieldText("Default Text")
                .ValidateNumericFieldText("9")
                .ValidateDecimalFieldText("15.9")
                .ValidateDateOfBirthFieldText("01/01/2019");

        }


        [Test]
        [Property("JiraIssueID", "CDV6-7005")]
        [Description("UI Test for Document Rules Actions - 010 - Step 10 - Set Question Mandatory / Set Question Non-Mandatory")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod010()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()

                .InsertShortAnswer("Document Rule Actions 10")
                .TapOnSaveButton()
                .InsertDecimalAnswer("1.5")
                .TapOnSaveButton(); //on the 2nd save the date of birth is not set, therefore we should get an error


            errorPopup.WaitForErrorPopupToLoad().ValidateErrorMessageTitleAndMessage("Error", "The question 'Date of Birth' is mandatory and must be answered before saving").TapOnOKButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .InsertDateOfBirthAnswer("01/01/2000")
                
                .TapOnSaveButton() //on this save the date of birth is set, therefore we should get no error

                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions X") //at this point we change the short answer value
                .TapOnSaveButton() //after saving and re-loading the document the date of birth should no longer be mandatory
                .InsertDateOfBirthAnswer("") //we remove the date of birth value
                .TapOnSaveAndCloseButton(); //and save the assessment again. this time we should get no error

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7006")]
        [Description("UI Test for Document Rules Actions - 011 - Step 11 - Set Question Read Only / Set Question Read Writable")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod011()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 11")
                .TapOnSaveButton()

                .ValidateDateOfBirthFieldReadOnly(true)
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions X") //changing the short answer and saving and re-loading the document should make the date question editable again
                .TapOnSaveButton()

                .ValidateDateOfBirthFieldReadOnly(false)
                .InsertDateOfBirthAnswer("01/01/2000")
                .TapOnSaveButton();


        }


        [Test]
        [Property("JiraIssueID", "CDV6-7007")]
        [Description("UI Test for Document Rules Actions - 012 - Step 12 - Set Question Value")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod012_1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rule Actions 12 (1)")
                .TapOnSaveButton()

                .ValidateDateOfBirthFieldText("01/06/2020")
                .ValidateDecimalFieldText("9.1")
                .ValidateMultipleChoiceFieldText("Mc2")
                .ValidateMultipleResponseOption1FieldText("No")
                .ValidateMultipleResponseOption2FieldText("No")
                .ValidateMultipleResponseOption3FieldText("Yes")
                .ValidateNumericFieldText("11")
                .ValidateParagraphExplanationFieldText("value 1\nvalue 2")
                .ValidateFrequencyFieldText("Weekly")
                .ValidateShortAnswerFieldText("v1 v2")
                .ValidateTimeQuestionFieldText("13:35")
                .ValidateYesNoFieldText("Yes");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7008")]
        [Description("UI Test for Document Rules Actions - 012 - Step 12 - Set Question Value")]
        public void PersonForms_DocumentRules_Offline_Actions_TestMethod012_2()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobile_test_user_1UserID = new Guid("2B16C2F3-459E-E911-A2C6-005056926FE4"); //Mobile_test_user_1
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid documentid = new Guid("2e230b53-0fa7-ea11-a2cd-005056926fe4"); //Mobile - Person Form (Document Rules)
            int assessmentstatusid = 1; //In Progress
            DateTime startdate = new DateTime(2020, 6, 1);
            DateTime duedate = new DateTime(2020, 6, 2);
            DateTime reviewdate = new DateTime(2020, 6, 3);


            //remove any Form for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personForm.GetPersonFormByPersonID(personID))
                this.PlatformServicesHelper.personForm.DeletePersonForm(recordID);

            Guid personFormID = this.PlatformServicesHelper.personForm.CreatePersonForm(mobileTeam1, personID, "Maria Tsatsouline", mobile_test_user_1UserID, null, documentid, "Mobile - Person Form (Document Rules)", assessmentstatusid, startdate, duedate, reviewdate);

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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()

                .TapSection3YesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3TimeQuestionAnswer("12:25")
                .InsertSection3ShortAnswer("value 1 2")
                .TapSection3FrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3ParagraphExplanationAnswer("value 3 4")
                .InsertSection3NumericAnswer("3")
                .TapSection3MultipleResponseOption2Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapSection3MultipleChoiceField();


            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection4HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertSection3DecimalAnswer("4.40")
                .InsertSection3DateOfBirthAnswer("15/10/1999")

                .InsertShortAnswer("Document Rule Actions 12 (2)")
                .TapOnSaveButton()

                .ValidateDateOfBirthFieldText("15/10/1999")
                .ValidateDecimalFieldText("4.40")
                .ValidateNumericFieldText("3")
                .ValidateParagraphExplanationFieldText("value 3 4")
                .ValidateShortAnswerFieldText("value 1 2")
                .ValidateTimeQuestionFieldText("12:25")
                .ValidateYesNoFieldText("Yes");
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

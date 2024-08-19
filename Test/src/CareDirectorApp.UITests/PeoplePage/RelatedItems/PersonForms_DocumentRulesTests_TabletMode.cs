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
    [Category("Mobile_TabletMode_Online")]
    public class PersonForms_DocumentRulesTests_TabletMode : TestBase
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
                   .InsertUserName("mobile_test_user_5")
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

            //if the cases Form injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases Form review pop-up is open then close it 
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

        #region 1 - Condition Operator Rules


        [Test]
        [Property("JiraIssueID", "CDV6-7023")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 001 - Step 1 - Date Question - Does Not Contains Data (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod001()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 1")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 1 Activated")
                .TapOnOKButton();

        }



        [Test]
        [Property("JiraIssueID", "CDV6-7024")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 001.1 - Step 1 - Date Question - Does Not Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod001_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 1")
                .InsertDateOfBirthAnswer("01/01/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");




        }


        [Test]
        [Property("JiraIssueID", "CDV6-7025")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 002 - Step 2 - Date Question - Contains Data (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod002()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 2")
                .InsertDateOfBirthAnswer("01/01/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 2 Activated")
                .TapOnOKButton();

        }



        [Test]
        [Property("JiraIssueID", "CDV6-7026")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 002 - Step 2 - Date Question - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod002_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 2")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");




        }



        [Test]
        [Property("JiraIssueID", "CDV6-7027")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 003 - Step 3 - Date Question - Equals (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod003()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 3")
                .InsertDateOfBirthAnswer("01/06/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 3 Activated")
                .TapOnOKButton();

        }



        [Test]
        [Property("JiraIssueID", "CDV6-7028")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 003 - Step 3 - Date Question - Equals (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod003_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 3")
                .InsertDateOfBirthAnswer("02/06/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");


        }




        [Test]
        [Property("JiraIssueID", "CDV6-7029")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 004 - Step 4 - Date Question - Does Not Equal (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod004()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 4")
                .InsertDateOfBirthAnswer("02/06/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 4 Activated")
                .TapOnOKButton();

        }



        [Test]
        [Property("JiraIssueID", "CDV6-7030")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 004 - Step 4 - Date Question - Does Not Equal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod004_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 4")
                .InsertDateOfBirthAnswer("01/06/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 4 NOT Activated")
                .TapOnOKButton();


        }



        [Test]
        [Property("JiraIssueID", "CDV6-7031")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 005 - Step 5 - Date Question - Is Greater Than (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod005()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 5")
                .InsertDateOfBirthAnswer("02/06/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 5 Activated")
                .TapOnOKButton();

        }



        [Test]
        [Property("JiraIssueID", "CDV6-7032")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 005 - Step 5 - Date Question - Is Greater Than (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod005_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 5")
                .InsertDateOfBirthAnswer("01/06/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");

        }



        [Test]
        [Property("JiraIssueID", "CDV6-7033")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 006 - Step 6 - Date Question - Is Greater Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod006()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 6")
                .InsertDateOfBirthAnswer("02/06/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 6 Activated")
                .TapOnOKButton();

        }


        [Test]
        [Property("JiraIssueID", "CDV6-7034")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 006 - Step 6 - Date Question - Is Greater Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod006_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 6")
                .InsertDateOfBirthAnswer("01/06/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 6 Activated")
                .TapOnOKButton();

        }


        [Test]
        [Property("JiraIssueID", "CDV6-7035")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 006 - Step 6 - Date Question - Is Greater Than (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod006_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 6")
                .InsertDateOfBirthAnswer("01/05/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7036")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 007 - Step 7 - Date Question - Is Less Than (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod007()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 7")
                .InsertDateOfBirthAnswer("01/05/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 7 Activated")
                .TapOnOKButton();
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7037")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 007 - Step 7 - Date Question - Is Less Than (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod007_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 7")
                .InsertDateOfBirthAnswer("01/06/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7038")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 008 - Step 8 - Date Question - Is Less Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod008()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 8")
                .InsertDateOfBirthAnswer("01/05/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 8 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7039")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 008 - Step 8 - Date Question - Is Less Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod008_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 8")
                .InsertDateOfBirthAnswer("01/06/2000")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 8 Activated")
                .TapOnOKButton();
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7040")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 008 - Step 8 - Date Question - Is Less Than or Equal To (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod008_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 8")
                .InsertDateOfBirthAnswer("02/06/2000")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7041")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 009 - Step 9 - Decimal Question - Equals (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod009()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 9")
                .InsertDecimalAnswer("6.4")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 9 Activated")
                .TapOnOKButton();
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7042")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 009 - Step 9 - Decimal Question - Equals (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod009_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 9")
                .InsertDecimalAnswer("6.3")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7043")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0010 - Step 10 - Decimal Question - Does Not Equal (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod010()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 10")
                .InsertDecimalAnswer("11.8")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 10 Activated")
                .TapOnOKButton();
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7044")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0010 - Step 10 - Decimal Question - Does Not Equal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod010_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 10")
                .InsertDecimalAnswer("11.9")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7045")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0011 - Step 11 - Decimal Question - Does Not Contain Data (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod011()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 11")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 11 Activated")
                .TapOnOKButton();
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7046")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0011 - Step 11 - Decimal Question - Does Not Contain Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod011_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 11")
                .InsertDecimalAnswer("11.9")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7047")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0012 - Step 12 - Decimal Question - Contains Data (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod012()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 12")
                .InsertDecimalAnswer("0.1")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 12 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7048")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0012 - Step 12 - Decimal Question - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod012_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 12")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7049")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0013 - Step 13 - Decimal Question - Between (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod013()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 13")
                .InsertDecimalAnswer("9.2")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 13 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7050")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0013 - Step 13 - Decimal Question - Between (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod013_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 13")
                .InsertDecimalAnswer("10")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 13 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7051")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0013 - Step 13 - Decimal Question - Between (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod013_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 13")
                .InsertDecimalAnswer("11.7")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 13 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7052")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0013 - Step 13 - Decimal Question - Between (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod013_3()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 13")
                .InsertDecimalAnswer("9.1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7053")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0013 - Step 13 - Decimal Question - Between (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod013_4()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 13")
                .InsertDecimalAnswer("11.8")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7054")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0014 - Step 14 - Decimal Question - Not Between (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod014()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 14")
                .InsertDecimalAnswer("6.5")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 14 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7055")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0014 - Step 14 - Decimal Question - Not Between (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod014_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 14")
                .InsertDecimalAnswer("6.91")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 14 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7056")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0014 - Step 14 - Decimal Question - Not Between (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod014_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 14")
                .InsertDecimalAnswer("6.21")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7057")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0015 - Step 15 - Decimal Question - Is Greater Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod015()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 15")
                .InsertDecimalAnswer("5.5")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 15 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7058")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0015 - Step 15 - Decimal Question - Is Greater Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod015_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 15")
                .InsertDecimalAnswer("5.6")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 15 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7059")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0015 - Step 15 - Decimal Question - Is Greater Than or Equal To (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod015_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 15")
                .InsertDecimalAnswer("5.4")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7060")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0016 - Step 16 - Decimal Question - Is Greater Than (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod016()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 16")
                .InsertDecimalAnswer("7.91")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 16 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7061")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0016 - Step 16 - Decimal Question - Is Greater Than (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod016_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 16")
                .InsertDecimalAnswer("7.9")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7062")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0017 - Step 17 - Decimal Question - Is Less Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod017()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 17")
                .InsertDecimalAnswer("0.90")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 17 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7063")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0017 - Step 17 - Decimal Question - Is Less Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod017_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 17")
                .InsertDecimalAnswer("0.89")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 17 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7064")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0017 - Step 17 - Decimal Question - Is Less Than or Equal To (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod017_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 17")
                .InsertDecimalAnswer("0.91")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7065")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0018 - Step 18 - Decimal Question - Is Less Than or Equal To (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod018()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 18")
                .InsertDecimalAnswer("0.49")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 18 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7066")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0018 - Step 18 - Decimal Question - Is Less Than or Equal To (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod018_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 18")
                .InsertDecimalAnswer("0.5")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7067")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0019 - Step 19 - Frequency - In (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod019()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 19")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 19 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7068")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0019 - Step 19 - Frequency - In (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod019_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 19")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 19 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7069")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0019 - Step 19 - Frequency - In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod019_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 19")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7070")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0019 - Step 19 - Frequency - In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod019_3()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 19")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7071")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0020 - Step 20 - Frequency - Not In (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod020()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 20")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(3).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 20 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7072")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0020 - Step 20 - Frequency - Not In (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod020_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 20")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(4).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();


            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 20 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7073")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0020 - Step 20 - Frequency - Not In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod020_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 20")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7074")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0020 - Step 20 - Frequency - Not In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod020_3()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 20")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7075")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0021 - Step 21 - Frequency - Does Not Contain Data (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod021()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 21")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 21 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7076")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0021 - Step 21 - Frequency - Does Not Contain Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod021_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 21")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7077")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0022 - Step 22 - Frequency - Contain Data (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod022()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 22")
                .TapFrequencyField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 22 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7078")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0022 - Step 22 - Frequency - Contain Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod022_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 22")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7079")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0023 - Step 23 - Multiple Choice - In (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod023()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 23")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 23 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7080")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0023 - Step 23 - Multiple Choice - In (positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod023_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 23")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 23 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7081")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0023 - Step 23 - Multiple Choice - In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod023_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 23")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }






        [Test]
        [Property("JiraIssueID", "CDV6-7082")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0024 - Step 24 - Multiple Choice - Not In (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod024()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 24")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 24 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7083")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0024 - Step 24 - Multiple Choice - Not In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod024_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 24")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7084")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0024 - Step 24 - Multiple Choice - Not In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod024_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 24")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(2).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7085")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0025 - Step 25 - Multiple Choice - Does Not Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod025()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 25")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 25 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7086")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0025 - Step 25 - Multiple Choice - Does Not Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod025_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 25")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7087")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0026 - Step 26 - Multiple Choice - Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod026()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 26")
                .TapMultipleChoiceField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 26 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7088")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0026 - Step 26 - Multiple Choice - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod026_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 26")

                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7089")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0027 - Step 27 - Multiple Response - In (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod027()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 27")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 27 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7090")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0027 - Step 27 - Multiple Response - In (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod027_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 27")
                .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 27 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7091")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0027 - Step 27 - Multiple Response - In (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod027_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 27")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 27 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7092")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0027 - Step 27 - Multiple Response - In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod027_3()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 27")
                .TapMultipleResponseOption2Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }






        [Test]
        [Property("JiraIssueID", "CDV6-7093")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0028 - Step 28 - Multiple Response - Not In (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod028()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 28")
                .TapMultipleResponseOption2Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 28 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7094")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0028 - Step 28 - Multiple Response - Not In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod028_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 28")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7095")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0028 - Step 28 - Multiple Response - Not In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod028_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 28")
                .TapMultipleResponseOption1Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7096")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0028 - Step 28 - Multiple Response - Not In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod028_3()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 28")
                .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }






        [Test]
        [Property("JiraIssueID", "CDV6-7097")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0029 - Step 29 - Multiple Response - Does Not Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod029()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 29")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 29 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7098")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0029 - Step 29 - Multiple Response - Does Not Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod029_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 29")
                .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7099")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0030 - Step 30 - Multiple Response - Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod030()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 30")
                .TapMultipleResponseOption3Field();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 30 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7100")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0030 - Step 30 - Multiple Response - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod030_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 30")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7101")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0031 - Step 31 - Numeric - Equals (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod031()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 31")
                .InsertNumericAnswer("9")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 31 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7102")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0031 - Step 31 - Numeric - Equals (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod031_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 31")
                .InsertNumericAnswer("10")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7103")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0032 - Step 32 - Numeric - Does Not Equal (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod032()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 32")
                .InsertNumericAnswer("10")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 32 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7104")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0032 - Step 32 - Numeric - Does Not Equal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod032_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 32")
                .InsertNumericAnswer("9")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }






        [Test]
        [Property("JiraIssueID", "CDV6-7105")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0033 - Step 33 - Numeric - Does Not Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod033()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 33")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 33 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7106")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0033 - Step 33 - Numeric - Does Not Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod033_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 33")
                .InsertNumericAnswer("9")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7107")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0034 - Step 34 - Numeric - Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod034()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 34")
                .InsertNumericAnswer("7")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 34 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7108")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0034 - Step 34 - Numeric - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod034_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 34")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7109")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0035 - Step 35 - Numeric - Between (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod035()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 35")
                .InsertNumericAnswer("3")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 35 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7110")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0035 - Step 35 - Numeric - Between (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod035_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 35")
                .InsertNumericAnswer("5")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 35 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7111")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0035 - Step 35 - Numeric - Between (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod035_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 35")
                .InsertNumericAnswer("6")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 35 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7112")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0035 - Step 35 - Numeric - Between (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod035_3()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 35")
                .InsertNumericAnswer("2")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7113")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0035 - Step 35 - Numeric - Between (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod035_4()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 35")
                .InsertNumericAnswer("7")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }






        [Test]
        [Property("JiraIssueID", "CDV6-7114")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0036 - Step 36 - Numeric - Not Between (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod036()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 36")
                .InsertNumericAnswer("2")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 36 Activated")
                .TapOnOKButton();
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7115")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0036 - Step 36 - Numeric - Not Between (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod036_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 36")
                .InsertNumericAnswer("7")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 36 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7116")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0036 - Step 36 - Numeric - Not Between (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod036_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 36")
                .InsertNumericAnswer("3")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-7117")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0036 - Step 36 - Numeric - Not Between (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod036_3()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 36")
                .InsertNumericAnswer("4")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7118")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0036 - Step 36 - Numeric - Not Between (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod036_4()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 36")
                .InsertNumericAnswer("6")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7119")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0037 - Step 37 - Numeric - Is Greater Than or Equal To (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod037()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 37")
                .InsertNumericAnswer("5")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 37 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7120")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0037 - Step 37 - Numeric - Is Greater Than or Equal To (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod037_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 37")
                .InsertNumericAnswer("6")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 37 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7121")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0037 - Step 37 - Numeric - Is Greater Than or Equal To (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod037_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 37")
                .InsertNumericAnswer("4")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7122")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0038 - Step 38 - Numeric - Is Greater Than (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod038()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 38")
                .InsertNumericAnswer("6")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 38 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7123")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0038 - Step 38 - Numeric - Is Greater Than (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod038_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 38")
                .InsertNumericAnswer("5")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7124")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0039 - Step 39 - Numeric - Is Less Than or Equal To (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod039()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 39")
                .InsertNumericAnswer("5")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 39 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7125")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0039 - Step 39 - Numeric - Is Less Than or Equal To (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod039_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 39")
                .InsertNumericAnswer("4")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 39 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7126")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0039 - Step 39 - Numeric - Is Less Than or Equal To (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod039_2()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 39")
                .InsertNumericAnswer("6")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7127")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0040 - Step 40 - Numeric - Is Less Than (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod040()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 40")
                .InsertNumericAnswer("4")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 40 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7128")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0040 - Step 40 - Numeric - Is Less Than (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod040_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 40")
                .InsertNumericAnswer("5")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7129")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0041 - Step 41 - Paragraph - Does Not Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod041()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 41")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 41 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7130")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0041 - Step 41 - Paragraph - Does Not Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod041_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 41")
                .InsertParagraphExplanationAnswer("value 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7131")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0042 - Step 42 - Paragraph - Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod042()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 42")
                .InsertParagraphExplanationAnswer("value 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 42 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7132")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0042 - Step 42 - Paragraph - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod042_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 42")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7133")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0043 - Step 43 - Paragraph - Equals (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod043()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 43")
                .InsertParagraphExplanationAnswer("value 1\nvalue 2")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 43 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7134")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0043 - Step 43 - Paragraph - Equals (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod043_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 43")
                .InsertParagraphExplanationAnswer("value 1\nvalue 3")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7135")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0044 - Step 44 - Paragraph - Does Not Equal (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod044()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 44")
                .InsertParagraphExplanationAnswer("value 1\nvalue 3")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 44 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7136")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0044 - Step 44 - Paragraph - Does Not Equal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod044_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 44")
                .InsertParagraphExplanationAnswer("value 1\nvalue 2")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7137")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0045 - Step 45 - Short Answer - Does Not Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod045()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertParagraphExplanationAnswer("Document Rules Step 45")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 45 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7138")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0045 - Step 45 - Short Answer - Does Not Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod045_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertParagraphExplanationAnswer("Document Rules Step 45")
                .InsertShortAnswer("value 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7139")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0046 - Step 46 - Short Answer - Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod046()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertParagraphExplanationAnswer("Document Rules Step 46")
                .InsertShortAnswer("value 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 46 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7140")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0046 - Step 46 - Short Answer - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod046_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertParagraphExplanationAnswer("Document Rules Step 46")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7141")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0047 - Step 47 - Short Answer - Equals (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod047()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertParagraphExplanationAnswer("Document Rules Step 47")
                .InsertShortAnswer("value 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 47 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7142")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0047 - Step 47 - Short Answer - Equals (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod047_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertParagraphExplanationAnswer("Document Rules Step 47")
                .InsertShortAnswer("value 2")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7143")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0048 - Step 48 - Short Answer - Does Not Equal (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod048()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertParagraphExplanationAnswer("Document Rules Step 48")
                .InsertShortAnswer("value 2")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 48 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7144")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0048 - Step 48 - Short Answer - Does Not Equal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod048_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertParagraphExplanationAnswer("Document Rules Step 48")
                .InsertShortAnswer("value 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7145")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0049 - Step 49 - Time Question - Equals (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod049()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 49")
                .InsertTimeQuestionAnswer("08:20")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 49 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7146")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0049 - Step 49 - Time Question - Equals (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod049_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 49")
                .InsertTimeQuestionAnswer("08:21")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7147")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0050 - Step 50 - Time Question - Does Not Equal (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod050()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 50")
                .InsertTimeQuestionAnswer("08:21")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 50 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7148")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0050 - Step 50 - Time Question - Does Not Equal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod050_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 50")
                .InsertTimeQuestionAnswer("08:20")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7149")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0051 - Step 51 - Time Question - Does Not Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod051()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 51")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 51 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7150")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0051 - Step 51 - Time Question - Does Not Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod051_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 51")
                .InsertTimeQuestionAnswer("08:20")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7151")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0052 - Step 52 - Time Question - Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod052()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 52")
                .InsertTimeQuestionAnswer("08:21")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 52 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7152")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0052 - Step 52 - Time Question - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod052_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 52")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7153")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0053 - Step 53 - YesNo - Does Not Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod053()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 53")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(2).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")

                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 53 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7154")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0053 - Step 53 - YesNo - Does Not Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod053_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 53")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7155")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0054 - Step 54 - YesNo - Contains Data (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod054()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 54")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 54 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7156")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0054 - Step 54 - YesNo - Contains Data (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod054_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 54")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(2).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }



        [Test]
        [Property("JiraIssueID", "CDV6-7157")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0055 - Step 55 - YesNo - Equals (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod055()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 55")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollUpPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 55 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7158")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0055 - Step 55 - YesNo - Equals (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod055_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 55")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7159")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0056 - Step 56 - YesNo - Does Not Equal (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod056()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 56")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 56 Activated")
                .TapOnOKButton();
        }

        [Test]
        [Property("JiraIssueID", "CDV6-7160")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0056 - Step 56 - YesNo - Does Not Equal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod056_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 56")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .TapYesNoField();

            pickList.WaitForPickListToLoad().ScrollDownPicklist(1).TapOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7161")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0057 - Step 57 - Security Profile - In (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod057_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 57")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1");
        }




        [Test]
        [Property("JiraIssueID", "CDV6-7162")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0058 - Step 58 - Security Profile - Not In (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorRules_TestMethod058()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 1")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 58")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 58 Activated")
                .TapOnOKButton();
        }





        #endregion


        #region 2 - Condition Operator Targets

        [Test]
        [Property("JiraIssueID", "CDV6-7163")]
        [Description("UI Test for Condition Operator Targets - 001 - Step 1 - Target - Question - Date (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod001()
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
        [Property("JiraIssueID", "CDV6-7164")]
        [Description("UI Test for Condition Operator Targets - 001 - Step 1 - Target - Question - Date (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod001_1()
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
        [Property("JiraIssueID", "CDV6-7165")]
        [Description("UI Test for Condition Operator Targets - 002 - Step 2 - Target - Question - Decimal (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod002()
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
        [Property("JiraIssueID", "CDV6-7166")]
        [Description("UI Test for Condition Operator Targets - 002 - Step 2 - Target - Question - Decimal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod002_1()
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
        [Property("JiraIssueID", "CDV6-7167")]
        [Description("UI Test for Condition Operator Targets - 003 - Step 3 - Target - Question - Numeric (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod003()
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
        [Property("JiraIssueID", "CDV6-7168")]
        [Description("UI Test for Condition Operator Targets - 003 - Step 3 - Target - Question - Numeric (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod003_1()
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
        [Property("JiraIssueID", "CDV6-7169")]
        [Description("UI Test for Condition Operator Targets - 004 - Step 4 - Target - Question - Paragraph (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod004()
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
        [Property("JiraIssueID", "CDV6-7170")]
        [Description("UI Test for Condition Operator Targets - 004 - Step 4 - Target - Question - Paragraph (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod004_1()
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
        [Property("JiraIssueID", "CDV6-7171")]
        [Description("UI Test for Condition Operator Targets - 005 - Step 5 - Target - Question - Short Answer (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod005()
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
        [Property("JiraIssueID", "CDV6-7172")]
        [Description("UI Test for Condition Operator Targets - 005 - Step 5 - Target - Question - Short Answer (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod005_1()
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
        [Property("JiraIssueID", "CDV6-7173")]
        [Description("UI Test for Condition Operator Targets - 005 - Step 5 - Target - Question - Short Answer (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod005_2()
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
        [Property("JiraIssueID", "CDV6-7174")]
        [Description("UI Test for Condition Operator Targets - 005 - Step 5 - Target - Question - Short Answer (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod005_3()
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
        [Property("JiraIssueID", "CDV6-7175")]
        [Description("UI Test for Condition Operator Targets - 006 - Step 6 - Target - Question - Time (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod006()
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
        [Property("JiraIssueID", "CDV6-7176")]
        [Description("UI Test for Condition Operator Targets - 006 - Step 6 - Target - Question - Time (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod006_1()
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
        [Property("JiraIssueID", "CDV6-7177")]
        [Description("UI Test for Condition Operator Targets - 007 - Step 7 - Target - Question - Yes/No (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod007()
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
        [Property("JiraIssueID", "CDV6-7178")]
        [Description("UI Test for Condition Operator Targets - 007 - Step 7 - Target - Question - Yes/No (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod007_1()
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
        [Property("JiraIssueID", "CDV6-7179")]
        [Description("UI Test for Condition Operator Targets - 008 - Step 8 - Target - Placeholder - Date (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod008()
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
        [Property("JiraIssueID", "CDV6-7180")]
        [Description("UI Test for Condition Operator Targets - 008 - Step 8 - Target - Placeholder - Date (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod008_1()
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
        [Property("JiraIssueID", "CDV6-7181")]
        [Description("UI Test for Condition Operator Targets - 009 - Step 9 - Target - Placeholder - Decimal (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod009()
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
        [Property("JiraIssueID", "CDV6-7182")]
        [Description("UI Test for Condition Operator Targets - 009 - Step 9 - Target - Placeholder - Decimal (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod009_1()
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
        [Property("JiraIssueID", "CDV6-7183")]
        [Description("UI Test for Condition Operator Targets - 010 - Step 10 - Target - Placeholder - Numeric (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod010()
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
        [Property("JiraIssueID", "CDV6-7184")]
        [Description("UI Test for Condition Operator Targets - 010 - Step 10 - Target - Placeholder - Numeric (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod010_1()
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
        [Property("JiraIssueID", "CDV6-7185")]
        [Description("UI Test for Condition Operator Targets - 011 - Step 11 - Target - Placeholder - Text (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod011()
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
        [Property("JiraIssueID", "CDV6-7186")]
        [Description("UI Test for Condition Operator Targets - 011 - Step 11 - Target - Placeholder - Text (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod011_1()
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
        [Property("JiraIssueID", "CDV6-7187")]
        [Description("UI Test for Condition Operator Targets - 012 - Step 12 - Target - Placeholder - Boolean (Positive scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod012()
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
        [Property("JiraIssueID", "CDV6-7188")]
        [Description("UI Test for Condition Operator Targets - 012 - Step 12 - Target - Placeholder - Boolean (Negative scenario)")]
        public void PersonForms_DocumentRules_ConditionOperatorTargets_TestMethod012_1()
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
        [Property("JiraIssueID", "CDV6-7189")]
        [Description("UI Test for Document Rules Actions - 002 - Step 2 - Empty Field (Positive scenario)")]
        public void PersonForms_DocumentRules_Actions_TestMethod002()
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
        [Property("JiraIssueID", "CDV6-7190")]
        [Description("UI Test for Document Rules Actions - 002 - Step 2 - Empty Field (Negative scenario)")]
        public void PersonForms_DocumentRules_Actions_TestMethod002_1()
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
                .InsertDecimalAnswer("5.7")
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
        [Property("JiraIssueID", "CDV6-7009")]
        [Description("UI Test for Document Rules Actions - 003 - Step 3 - Evaluate Formula (Divide scenario)")]
        public void PersonForms_DocumentRules_Actions_TestMethod003()
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
        [Property("JiraIssueID", "CDV6-7010")]
        [Description("UI Test for Document Rules Actions - 003 - Step 3 - Evaluate Formula (Multiply scenario)")]
        public void PersonForms_DocumentRules_Actions_TestMethod003_1()
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
        [Property("JiraIssueID", "CDV6-7011")]
        [Description("UI Test for Document Rules Actions - 004 - Step 4 - Focus Field")]
        public void PersonForms_DocumentRules_Actions_TestMethod004()
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
                //.ValidateSection3ShortAnswerIsFocused()
                ;

        }



        [Test]
        [Property("JiraIssueID", "CDV6-7012")]
        [Description("UI Test for Document Rules Actions - 005 - Step 5 - Hide Section / Show Section")]
        public void PersonForms_DocumentRules_Actions_TestMethod005()
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
        [Property("JiraIssueID", "CDV6-7013")]
        [Description("UI Test for Document Rules Actions - 006 - Step 6 - Hide Section Question / Show Section Question")]
        public void PersonForms_DocumentRules_Actions_TestMethod006()
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
        [Property("JiraIssueID", "CDV6-7014")]
        [Description("UI Test for Document Rules Actions - 007 - Step 7 - Hide Single Question / Show Single Question")]
        public void PersonForms_DocumentRules_Actions_TestMethod007()
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
        [Property("JiraIssueID", "CDV6-7015")]
        [Description("UI Test for Document Rules Actions - 008 - Step 8 - Run Another Rule")]
        public void PersonForms_DocumentRules_Actions_TestMethod008()
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
        [Property("JiraIssueID", "CDV6-7016")]
        [Description("UI Test for Document Rules Actions - 009 - Step 9.1 - Set Placeholder")]
        public void PersonForms_DocumentRules_Actions_TestMethod009_1()
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
        [Property("JiraIssueID", "CDV6-7017")]
        [Description("UI Test for Document Rules Actions - 009 - Step 9.1 - Set Placeholder")]
        public void PersonForms_DocumentRules_Actions_TestMethod009_2()
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
        [Property("JiraIssueID", "CDV6-7018")]
        [Description("UI Test for Document Rules Actions - 009 - Step 9.1 - Set Placeholder")]
        public void PersonForms_DocumentRules_Actions_TestMethod009_3()
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
        [Property("JiraIssueID", "CDV6-7019")]
        [Description("UI Test for Document Rules Actions - 010 - Step 10 - Set Question Mandatory / Set Question Non-Mandatory")]
        public void PersonForms_DocumentRules_Actions_TestMethod010()
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
        [Property("JiraIssueID", "CDV6-7020")]
        [Description("UI Test for Document Rules Actions - 011 - Step 11 - Set Question Read Only / Set Question Read Writable")]
        public void PersonForms_DocumentRules_Actions_TestMethod011()
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
        [Property("JiraIssueID", "CDV6-7021")]
        [Description("UI Test for Document Rules Actions - 012 - Step 12 - Set Question Value")]
        public void PersonForms_DocumentRules_Actions_TestMethod012_1()
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
        [Property("JiraIssueID", "CDV6-7022")]
        [Description("UI Test for Document Rules Actions - 012 - Step 12 - Set Question Value")]
        public void PersonForms_DocumentRules_Actions_TestMethod012_2()
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
                .InsertSection3DecimalAnswer("4.4")
                .InsertSection3DateOfBirthAnswer("15/10/1999")

                .InsertShortAnswer("Document Rule Actions 12 (2)")
                .TapOnSaveButton()

                .ValidateDateOfBirthFieldText("15/10/1999")
                .ValidateDecimalFieldText("4.4")
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

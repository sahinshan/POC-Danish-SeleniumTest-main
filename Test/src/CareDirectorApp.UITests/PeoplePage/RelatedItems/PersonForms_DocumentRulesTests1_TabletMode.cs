using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.RelatedItems
{
    /// <summary>
    /// This class contains all test methods for Person Forms validations while the APP is displaying in tablet mode
    /// All tests in this class are used to validate the "Security Profile" scenarios only
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonForms_DocumentRulesTests1_TabletMode : TestBase
    {
        static UIHelper uIHelper;

        [TestFixtureSetUp]
        public void ClassInitializationMethod()
        {
            if (this.IgnoreTestFixtureSetUp)
                return;

            //authenticate a user against the platform services
            this.PlatformServicesHelper = new PlatformServicesHelper("mobile_test_user_4", "Passw0rd_!");

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
        [Property("JiraIssueID", "CDV6-7198")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0057 - Step 57 - Security Profile - In (Positive scenario)")]
        public void PersonForms_DocumentRulesTestMethod0057()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 4")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 4")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 57")
                .TapOnSaveButton();

            errorPopup
                .WaitForErrorPopupToLoad()
                .ValidateErrorMessageTitleAndMessage("Message", "Condition Operator Rules - Step 57 Activated")
                .TapOnOKButton();

            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 4");
        }





        [Test]
        [Property("JiraIssueID", "CDV6-7199")]
        [Description("UI Test for Condition Operator Rules (Document Rules) - 0058 - Step 58 - Security Profile - Not In (Negative scenario)")]
        public void PersonForms_DocumentRulesTestMethod0058_1()
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
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 4")
                .TapEditAssessmentButton();


            mobilePersonFormDocumentRulesEditAssessmentPage
                .WaitForMobilePersonFormDocumentRulesEditAssessmentPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 4")
                .TapOpenHierarchyButton()
                .TapSection_1_1_HierarchyWindow()
                .TapOpenHierarchyButton()
                .InsertShortAnswer("Document Rules Step 58")
                .TapOnSaveAndCloseButton();

            personFormRecordPage
                .WaitForPersonFormRecordPageToLoad("FORM (PERSON): Mobile - Person Form (Document Rules) for Maria Tsatsouline Starting 01/06/2020 created by Mobile Test User 4");
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

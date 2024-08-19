using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.Health
{
    /// <summary>
    /// This class contains all test methods for Person Allergic Reactions sub page while the app is displaying in Tablet mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonAllergicReactions_TabletModeTests : TestBase
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

            //if the cases PersonAllergy injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases PersonAllergy review pop-up is open then close it 
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

        #region Person Allergic Reactions page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6852")]
        [Description("UI Test for Dashboards - 0001 - " +
            "Navigate to the Person Allergic Reactions area - Allergy record do not contains any Allergic Reaction record) - Validate the page content")]
        public void PersonPersonAllergies_TestMethod01()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonAllergiesIcon_RelatedItems();

            personAllergyPage
                .WaitForPersonAllergyPageToLoad()
                .TapOnRecord("Augmentin");

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Augmentin, 01/05/2020")
                .TapRelatedItemsButton()
                .TapAllergicReactionsButton();

            personAllergicReactionsPage
                .WaitForPersonAllergicReactionsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6853")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the Person Allergic Reactions area - Allergy record contains Allergic Reactions records) - Validate the page content")]
        public void PersonPersonAllergies_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid reactionRecordID = new Guid("594f5cdf-68b1-ea11-a2cd-005056926fe4"); //

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonAllergiesIcon_RelatedItems();

            personAllergyPage
                .WaitForPersonAllergyPageToLoad()
                .TapOnRecord("Dust");

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Dust, 05/05/2020")
                .TapRelatedItemsButton()
                .TapAllergicReactionsButton();

            personAllergicReactionsPage
                .WaitForPersonAllergicReactionsPageToLoad()
                .ValidateNoRecordsMessageVisibility(false)
                .ValidateReactionCellText("Anxiety disorder of childhood OR adolescence (disorder)", reactionRecordID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", reactionRecordID.ToString())
                .ValidateCreatedOnCellText("18/06/2020 14:37", reactionRecordID.ToString())
                .ValidateModifiedByCellText("Mobile Test User 1", reactionRecordID.ToString())
                .ValidateModifiedOnCellText("18/06/2020 14:37", reactionRecordID.ToString());


        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6854")]
        [Description("UI Test for Dashboards - 0003 - " +
            "Navigate to the Person Allergic Reactions area - Open a Allergic Reaction record - Validate that the Allergic Reaction record page is displayed")]
        public void PersonPersonAllergies_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonAllergiesIcon_RelatedItems();

            personAllergyPage
                .WaitForPersonAllergyPageToLoad()
                .TapOnRecord("Dust");

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Dust, 05/05/2020")
                .TapRelatedItemsButton()
                .TapAllergicReactionsButton();

            personAllergicReactionsPage
                .WaitForPersonAllergicReactionsPageToLoad()
                .TapOnRecord("Anxiety disorder of childhood OR adolescence (disorder)");

            personAllergicReactionRecordPage
                .WaitForPersonAllergicReactionRecordPageToLoad("ALLERGIC REACTION: Mr Pavel MCNamara, Dust, 05/05/2020, Anxiety disorder of childhood OR adolescence (disorder)");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6855")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the Person Allergic Reactions area - Open a Allergic Reaction record - Validate that the Allergic Reaction record page field titles are displayed")]
        public void PersonPersonAllergies_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonAllergiesIcon_RelatedItems();

            personAllergyPage
                .WaitForPersonAllergyPageToLoad()
                .TapOnRecord("Dust");

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Dust, 05/05/2020")
                .TapRelatedItemsButton()
                .TapAllergicReactionsButton();

            personAllergicReactionsPage
                .WaitForPersonAllergicReactionsPageToLoad()
                .TapOnRecord("Anxiety disorder of childhood OR adolescence (disorder)");

            personAllergicReactionRecordPage
                .WaitForPersonAllergicReactionRecordPageToLoad("ALLERGIC REACTION: Mr Pavel MCNamara, Dust, 05/05/2020, Anxiety disorder of childhood OR adolescence (disorder)")
                .ValidateReactionFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6856")]
        [Description("UI Test for Dashboards - 0005 - " +
            "Navigate to the Person Allergic Reactions area - Open a Allergic Reaction record - Validate that the Allergic Reaction record page fields are correctly displayed")]
        public void PersonPersonAllergies_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonAllergiesIcon_RelatedItems();

            personAllergyPage
                .WaitForPersonAllergyPageToLoad()
                .TapOnRecord("Dust");

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Dust, 05/05/2020")
                .TapRelatedItemsButton()
                .TapAllergicReactionsButton();

            personAllergicReactionsPage
                .WaitForPersonAllergicReactionsPageToLoad()
                .TapOnRecord("Anxiety disorder of childhood OR adolescence (disorder)");

            personAllergicReactionRecordPage
                .WaitForPersonAllergicReactionRecordPageToLoad("ALLERGIC REACTION: Mr Pavel MCNamara, Dust, 05/05/2020, Anxiety disorder of childhood OR adolescence (disorder)")
                .ValidateReactionFieldText("Anxiety disorder of childhood OR adolescence (disorder)");
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

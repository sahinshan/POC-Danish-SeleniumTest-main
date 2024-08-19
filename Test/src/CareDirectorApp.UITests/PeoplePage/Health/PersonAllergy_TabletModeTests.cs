using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.Health
{
    /// <summary>
    /// This class contains all test methods for Person Allergy validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonAllergy_TabletModeTests : TestBase
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

        #region Person Allergy page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6859")]
        [Description("UI Test for Dashboards - 0001 - " +
            "Navigate to the Person Allergy area (do not contains PersonAllergy records) - Validate the page content")]
        public void PersonAllergies_TestMethod01()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any PersonAllergy for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAllergy.GetPersonAllergyByPersonID(personID))
                this.PlatformServicesHelper.personAllergy.DeletePersonAllergy(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonAllergiesIcon_RelatedItems();

            personAllergyPage
                .WaitForPersonAllergyPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6860")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the Person Allergy area (person contains PersonAllergy records) - Validate the page content")]
        public void PersonAllergies_TestMethod02()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid allergyID = new Guid("3ce63e3e-9ca1-ea11-a2cd-005056926fe4");

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
                .ValidateAllergyTypeCellText("Augmentin", allergyID.ToString())
                .ValidateStartDateCellText("01/05/2020", allergyID.ToString())
                .ValidateLevelCellText("Adverse Drug Reaction", allergyID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", allergyID.ToString())
                .ValidateCreatedOnCellText("29/05/2020 12:05", allergyID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", allergyID.ToString())
                .ValidateModifiedOnCellText("29/05/2020 12:05", allergyID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6861")]
        [Description("UI Test for Dashboards - 0003 - " +
            "Navigate to the Person Allergy area - Open a person PersonAllergy record - Validate that the Person Allergy record page is displayed")]
        public void PersonAllergies_TestMethod03()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personAllergyType = "Augmentin";

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
                .TapOnRecord(personAllergyType);

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Augmentin, 01/05/2020");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6862")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the Person Allergy area - Open a person PersonAllergy record - Validate that the Person Allergy record page field titles are displayed")]
        public void PersonAllergies_TestMethod04()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personAllergyType = "Augmentin";

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
                .TapOnRecord(personAllergyType);

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Augmentin, 01/05/2020")
                .ValidateAllergyTypeFieldTitleVisible(true)
                .ValidateLevelFieldTitleVisible(true)
                .ValidateAllergenWhatSubstanceCausedTheReactionFieldTitleVisible(true)
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6863")]
        [Description("UI Test for Dashboards - 0005 - " +
            "Navigate to the Person Allergy area - Open a person PersonAllergy record - Validate that the Person Allergy record page fields are correctly displayed")]
        public void PersonAllergies_TestMethod05()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personAllergyType = "Augmentin";

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
                .TapOnRecord(personAllergyType);

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Augmentin, 01/05/2020")
                .ValidateAllergyTypeFieldText(personAllergyType)
                .ValidateLevelFieldText("Adverse Drug Reaction")
                .ValidateAllergenWhatSubstanceCausedTheReactionFieldText("Substance information")
                .ValidateStartDateFieldText("01/05/2020")
                .ValidateEndDateFieldText("02/05/2020")
                .ValidateDescriptionFieldText("Description information");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6864")]
        [Description("UI Test for Dashboards - 0006 - " +
            "Navigate to the Person Allergy area - Open a person PersonAllergy record (with only the mandatory information set) - Validate that the Person Allergy record page fields are correctly displayed")]
        public void PersonAllergies_TestMethod06()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string personAllergyType = "Dust";

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
                .TapOnRecord(personAllergyType);

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Mr Pavel MCNamara, Dust, 05/05/2020")
                .ValidateAllergyTypeFieldText(personAllergyType)
                .ValidateLevelFieldText("Confirmed")
                .ValidateAllergenWhatSubstanceCausedTheReactionFieldText("Allergen information")
                .ValidateStartDateFieldText("05/05/2020")
                .ValidateEndDateFieldText("")
                .ValidateDescriptionFieldText("");
        }

        #endregion

        #region Sync Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-6865")]
        [Description("UI Test for Dashboards - 0015 - Create a new person PersonAllergy using the main APP web services" +
            "Navigate to the Person Allergy area - open the Person Allergy record - validate that all fields are correctly synced")]
        public void PersonAllergies_TestMethod15()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid allergyType = new Guid("77e05b21-c3cf-e911-a2c7-005056926fe4"); //Dust
            int allergyLevel = 3; //Hypersensitivity


            //remove any PersonAllergy for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personAllergy.GetPersonAllergyByPersonID(personID))
                this.PlatformServicesHelper.personAllergy.DeletePersonAllergy(recordID);

            this.PlatformServicesHelper.personAllergy.CreatePersonAllergy(mobileTeam1, false, personID, "Maria Tsatsouline", allergyType, "Dust", "allergen details", new DateTime(2020,5,5), new DateTime(2020, 5, 10), "description information", allergyLevel);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapHealthArea_RelatedItems()
                .TapPersonAllergiesIcon_RelatedItems();

            personAllergyPage
                .WaitForPersonAllergyPageToLoad()
                .TapOnRecord("Dust");

            personAllergyRecordPage
                .WaitForPersonAllergyRecordPageToLoad("PERSON ALLERGY: Maria Tsatsouline, Dust, 05/05/2020")
                .ValidateAllergyTypeFieldText("Dust")
                .ValidateLevelFieldText("Hypersensitivity")
                .ValidateAllergenWhatSubstanceCausedTheReactionFieldText("allergen details")
                .ValidateStartDateFieldText("05/05/2020")
                .ValidateEndDateFieldText("10/05/2020")
                .ValidateDescriptionFieldText("description information");

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

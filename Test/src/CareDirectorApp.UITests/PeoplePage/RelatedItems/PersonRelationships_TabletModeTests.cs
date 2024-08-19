using System;
using NUnit.Framework;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using Xamarin.UITest.Configuration;

namespace CareDirectorApp.UITests.People.RelatedItems
{
    /// <summary>
    /// This class contains all test methods for Person Relationship validations while the app is displaying in mobile mode
    /// </summary>
    [TestFixture]
    [Category("Mobile_TabletMode_Online")]
    public class PersonRelationships_TabletModeTests : TestBase
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

            //if the cases PersonRelationship injury description pop-up is open then close it 
            personBodyInjuryDescriptionPopup.ClosePopupIfOpen();

            //if the cases PersonRelationship review pop-up is open then close it 
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

        #region Person Relationship page

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7258")]
        [Description("UI Test for Dashboards - 0001 - " +
            "Navigate to the Person Relationship area (do not contains PersonRelationship records) - Validate the page content")]
        public void PersonRelationships_TestMethod1()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 

            //remove any PersonRelationship for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personRelationship.GetPersonRelationshipByPersonID(personID))
                this.PlatformServicesHelper.personRelationship.DeletePersonRelationship(recordID);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonRelationshipsIcon_RelatedItems();

            personRelationshipsPage
                .WaitForPersonRelationshipsPageToLoad()
                .ValidateNoRecordsMessageVisibility(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7259")]
        [Description("UI Test for Dashboards - 0002 - " +
            "Navigate to the Person Relationship area (person contains PersonRelationship records) - Validate the page content")]
        public void PersonRelationships_TestMethod2()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            Guid relationshipID = new Guid("d6221e18-1cb2-ea11-a2cd-005056926fe4"); //Audrey Abbott

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonRelationshipsIcon_RelatedItems();

            personRelationshipsPage
                .WaitForPersonRelationshipsPageToLoad()
                .ValidateRelatedPersonCellText("Audrey Abbott", relationshipID.ToString())
                .ValidateRelationshipCellText("Friend", relationshipID.ToString())
                .ValidateCreatedByCellText("Mobile Test User 1", relationshipID.ToString())
                .ValidateCreatedOnCellText("19/06/2020 12:00", relationshipID.ToString())
                .ValidateModifiedByText("Mobile Test User 1", relationshipID.ToString())
                .ValidateModifiedOnCellText("19/06/2020 12:00", relationshipID.ToString());
        }

        #endregion

        #region Open existing records

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7260")]
        [Description("UI Test for Dashboards - 0003 - " +
            "Navigate to the Person Relationship area - Open a person PersonRelationship record - Validate that the Person Relationship record page is displayed")]
        public void PersonRelationships_TestMethod3()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string relatedPerson = "Audrey Abbott";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonRelationshipsIcon_RelatedItems();

            personRelationshipsPage
                .WaitForPersonRelationshipsPageToLoad()
                .TapOnRecord(relatedPerson);

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad("PERSON RELATIONSHIP: Person Relationship for Mr Pavel MCNamara created by Mobile Test User 1 on 19/06/2020 12:00:33");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7261")]
        [Description("UI Test for Dashboards - 0004 - " +
            "Navigate to the Person Relationship area - Open a person PersonRelationship record - Validate that the Person Relationship record page field titles are displayed")]
        public void PersonRelationships_TestMethod4()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string relatedPerson = "Audrey Abbott";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonRelationshipsIcon_RelatedItems();

            personRelationshipsPage
                .WaitForPersonRelationshipsPageToLoad()
                .TapOnRecord(relatedPerson);

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad("PERSON RELATIONSHIP: Person Relationship for Mr Pavel MCNamara created by Mobile Test User 1 on 19/06/2020 12:00:33")
                .ValidatePrimaryPersonFieldTitleVisible(true)
                .ValidateRelationshipFieldTitleVisible(true)
                .ValidateRelatedPersonFieldTitleVisible(true)

                .ValidatePersonFieldTitleVisible(true)
                .ValidateRelatedRelationshipFieldTitleVisible(true)
                .ValidateToFieldTitleVisible(true)
                
                .ValidateStartDateFieldTitleVisible(true)
                .ValidateEndDateFieldTitleVisible(true)
                .ValidateResponsibleTeamFieldTitleVisible(true)
                .ValidateDescriptionFieldTitleVisible(true)

                .ValidateInsideHouseholdFieldTitleVisible(true)
                .ValidateFamilyMemberFieldTitleVisible(true)
                .ValidateNextofKinFieldTitleVisible(true)
                .ValidateAdvocateFieldTitleVisible(true)
                .ValidateIsBirthParentFieldTitleVisible(true)
                .ValidatePowersOfAttorneyFieldTitleVisible(true)
                .ValidateFinancialRepresentativeFieldTitleVisible(true)
                .ValidatePrimaryCaseWorkerExternalContactFieldTitleVisible(true)
                .ValidateEmergencyContactFieldTitleVisible(true)
                .ValidateKeyHolderFieldTitleVisible(true)
                .ValidateLegalGuardianFieldTitleVisible(true)
                .ValidateMHANearestRelativeFieldTitleVisible(true)
                .ValidatePrimaryCarerFieldTitleVisible(true)
                .ValidateSecondaryCaregiverFieldTitleVisible(true)
                .ValidateHasParentalResponsibilityFieldTitleVisible(true)
                .ValidatePCHRFieldTitleVisible(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7262")]
        [Description("UI Test for Dashboards - 0005 - " +
            "Navigate to the Person Relationship area - Open a person PersonRelationship record - Validate that the Person Relationship record page fields are correctly displayed")]
        public void PersonRelationships_TestMethod5()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string relatedPerson = "Audrey Abbott";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonRelationshipsIcon_RelatedItems();

            personRelationshipsPage
                .WaitForPersonRelationshipsPageToLoad()
                .TapOnRecord(relatedPerson);

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad("PERSON RELATIONSHIP: Person Relationship for Mr Pavel MCNamara created by Mobile Test User 1 on 19/06/2020 12:00:33")
                .ValidatePrimaryPersonFieldText("Pavel MCNamara")
                .ValidateRelationshipFieldText("Friend")
                .ValidateRelatedPersonFieldText("Audrey Abbott")

                .ValidatePersonFieldText("Audrey Abbott")
                .ValidateRelatedRelationshipFieldText("Friend")
                .ValidateToFieldText("Pavel MCNamara")

                .ValidateStartDateFieldText("19/06/2020")
                .ValidateEndDateFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateDescriptionFieldText("Desc ....")

                .ValidateInsideHouseholdFieldText("Yes")
                .ValidateFamilyMemberFieldText("No")
                .ValidateNextofKinFieldText("No")
                .ValidateAdvocateFieldText("No")
                .ValidateIsBirthParentFieldText("No")
                .ValidatePowersOfAttorneyFieldText("No")
                .ValidateFinancialRepresentativeFieldText("No")
                .ValidatePrimaryCaseWorkerExternalContactFieldText("No")
                .ValidateEmergencyContactFieldText("No")
                .ValidateKeyHolderFieldText("No")
                .ValidateLegalGuardianFieldText("No")
                .ValidateMHANearestRelativeFieldText("No")
                .ValidatePrimaryCarerFieldText("No")
                .ValidateSecondaryCaregiverFieldText("No")
                .ValidateHasParentalResponsibilityFieldText("No")
                .ValidatePCHRFieldText("No");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7263")]
        [Description("UI Test for Dashboards - 0006 - " +
            "Navigate to the Person Relationship area - Open a person PersonRelationship record (with only the mandatory information set) - Validate that the Person Relationship record page fields are correctly displayed")]
        public void PersonRelationships_TestMethod6()
        {
            Guid personID = new Guid("3301e8c2-4591-ea11-a2cd-005056926fe4"); //Mr Pavel MCNamara 
            string relatedPerson = "Angel Abbott";

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Pavel MCNamara", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Pavel MCNamara")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonRelationshipsIcon_RelatedItems();

            personRelationshipsPage
                .WaitForPersonRelationshipsPageToLoad()
                .TapOnRecord(relatedPerson);

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad("PERSON RELATIONSHIP: Person Relationship for Mr Pavel MCNamara created by Mobile Test User 1 on 19/06/2020 12:00:52")
                .ValidatePrimaryPersonFieldText("Pavel MCNamara")
                .ValidateRelationshipFieldText("Friend")
                .ValidateRelatedPersonFieldText("Angel Abbott")

                .ValidatePersonFieldText("Angel Abbott")
                .ValidateRelatedRelationshipFieldText("Friend")
                .ValidateToFieldText("Pavel MCNamara")

                .ValidateStartDateFieldText("19/06/2020")
                .ValidateEndDateFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateDescriptionFieldText("")

                .ValidateInsideHouseholdFieldText("")
                .ValidateFamilyMemberFieldText("")
                .ValidateNextofKinFieldText("")
                .ValidateAdvocateFieldText("")
                .ValidateIsBirthParentFieldText("")
                .ValidatePowersOfAttorneyFieldText("")
                .ValidateFinancialRepresentativeFieldText("")
                .ValidatePrimaryCaseWorkerExternalContactFieldText("")
                .ValidateEmergencyContactFieldText("")
                .ValidateKeyHolderFieldText("")
                .ValidateLegalGuardianFieldText("")
                .ValidateMHANearestRelativeFieldText("")
                .ValidatePrimaryCarerFieldText("")
                .ValidateSecondaryCaregiverFieldText("")
                .ValidateHasParentalResponsibilityFieldText("")
                .ValidatePCHRFieldText("");
        }

        #endregion

        #region Sync Record

        /// <summary>
        /// 
        /// </summary>
        [Test]
        [Property("JiraIssueID", "CDV6-7264")]
        [Description("UI Test for Dashboards - 0015 - Create a new person PersonRelationship using the main APP web services" +
            "Navigate to the Person Relationship area - open the Person Relationship record - validate that all fields are correctly synced")]
        public void PersonRelationships_TestMethod15()
        {
            Guid personID = new Guid("0e62da4b-4591-ea11-a2cd-005056926fe4"); //Ms Maria Tsatsouline 
            Guid mobileTeam1 = new Guid("0BFFB4B6-429E-E911-A2C6-005056926FE4"); //Mobile Team 1
            Guid relationshipType = new Guid("aa8eee7b-4899-e811-9962-461ca8e2ff4b"); //Friend
            Guid relatedPersonID = new Guid("e454909f-00fc-4fdf-92e4-a945e1d9fe50"); //Arnold Abbott 
            DateTime startDate = new DateTime(2020, 6, 19);


            //remove any PersonRelationship for the person
            foreach (Guid recordID in this.PlatformServicesHelper.personRelationship.GetPersonRelationshipByPersonID(personID))
                this.PlatformServicesHelper.personRelationship.DeletePersonRelationship(recordID);

            this.PlatformServicesHelper.personRelationship.CreatePersonRelationship(mobileTeam1, 
                personID, "Maria Tsatsouline", 
                relationshipType, "Friend", 
                relatedPersonID, "Arnold Abbott", 
                relationshipType, "Friend",
                startDate, "desc ...", 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, false);

            peoplePage
                .WaitForPeoplePageToLoad()
                .TapOnPersonRecordButton("Maria Tsatsouline", personID.ToString());

            personPage
                .WaitForPersonPageToLoad("Maria Tsatsouline")
                .TapRelatedItemsButton()
                .TapRelatedItemsArea_RelatedItems()
                .TapPersonRelationshipsIcon_RelatedItems();

            personRelationshipsPage
                .WaitForPersonRelationshipsPageToLoad()
                .TapOnRecord("Arnold Abbott");

            personRelationshipRecordPage
                .WaitForPersonRelationshipRecordPageToLoad()
                .ValidatePrimaryPersonFieldText("Maria Tsatsouline")
                .ValidateRelationshipFieldText("Friend")
                .ValidateRelatedPersonFieldText("Arnold Abbott")

                .ValidatePersonFieldText("Arnold Abbott")
                .ValidateRelatedRelationshipFieldText("Friend")
                .ValidateToFieldText("Maria Tsatsouline")

                .ValidateStartDateFieldText("19/06/2020")
                .ValidateEndDateFieldText("")
                .ValidateResponsibleTeamFieldText("Mobile Team 1")
                .ValidateDescriptionFieldText("desc ...")

                .ValidateInsideHouseholdFieldText("Yes")
                .ValidateFamilyMemberFieldText("No")
                .ValidateNextofKinFieldText("No")
                .ValidateAdvocateFieldText("No")
                .ValidateIsBirthParentFieldText("No")
                .ValidatePowersOfAttorneyFieldText("No")
                .ValidateFinancialRepresentativeFieldText("No")
                .ValidatePrimaryCaseWorkerExternalContactFieldText("No")
                .ValidateEmergencyContactFieldText("No")
                .ValidateKeyHolderFieldText("No")
                .ValidateLegalGuardianFieldText("No")
                .ValidateMHANearestRelativeFieldText("No")
                .ValidatePrimaryCarerFieldText("No")
                .ValidateSecondaryCaregiverFieldText("No")
                .ValidateHasParentalResponsibilityFieldText("No")
                .ValidatePCHRFieldText("No");

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
